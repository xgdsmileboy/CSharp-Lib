using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace QLearning
{
    /// <summary>
    /// 定义一个状态动作对所对应的Q元素类
    /// 该类包含一个Q值和一个对该Q的访问次数
    /// </summary>
    class Qclass
    {
        public double Qvalue;//状态动作对所对应的Q值
        public int visttime;//对当前Q值的访问次数
        public int flag;//区分有效和无效的Q值
    }

    class Program
    {
        //对单个相位的排队长度进行划分，定义为5个状态
        public enum OState
        {
            os0, os1, os2, os3, os4
            //<5 /<10           />20
        }
        //对单个相位可能采取的动作进行定义，共有5个可能的动作
        public static int[] OAction = new int[OActionLength] { 20, 25, 30, 35, 40 };
        //每个相位的绿灯时间 
        const int OStateLength = 5;//枚举类型的长度
        const int OActionLength = 5;//动作集中元素个数

        public static int T = 10000;//退火系数   公式中参数，10000->递减

        public static double discount = 0.99;//折扣因子 公式中参数 自己设置

        public static TcpServer server = null;
        public static Thread ListenThread;
        public static MyTcpClient newClient = null;
        public static ManualResetEvent Manuevent = new ManualResetEvent(false);

        public static byte[] bytefour = new byte[4];  //接收发送缓存

        public static int queuetime = 0;//表示接收到排队长度的次序

        //定义一个从排队长度到对应状态的转换函数
        public static OState IntToOState(int k)
        {
            if (k <= 5)
                return OState.os0;
            else if (k <= 10)
                return OState.os1;
            else if (k <= 15)
                return OState.os2;
            else if (k <= 20)
                return OState.os3;
            else
                return OState.os4;

        }//end of IntToOState

        //随机地产生一个随机数组，没用！
        public static int[] MyRandom(int Num, int MinValue, int MaxValue)
        {
            int temp = 0;
            int[] array = new int[Num];
            Random ra = new Random();
            for (int i = 0; i < Num; i++)
            {
                temp = ra.Next(MinValue, MaxValue);
                array[i] = temp;
            }
            return array;
        }
        //将一个四元素数组转换成相应的状态四元组，把四个排队长度转换成一个状态(os1/os2/os3/os4)
        public static OState[] IntArrToOStableArr(int[] IntArr, int Num)
        {
            OState[] S = new OState[Num];
            int i = 0;
            foreach (int j in IntArr)
            {
                S[i++] = IntToOState(j);
            }
            return S;
        }

        //求一个状态四元组是状态List中的第几个元素
        public static int IndexOfState(OState[] S)
        {
            int MyIndex = 0;
            foreach (OState k in S)
            {
                MyIndex = MyIndex * OStateLength + (int)k;
            }
            return MyIndex;
        }

        /// <summary>
        /// 将状态所属的动作List的Q值转化成相应的概率
        /// List的最后一个元素为所有概率的和
        /// 此处的概率都是指真实概率乘以10000000后的整数
        /// </summary>
        /// <param name="Qarray">以类Qclass为元素的数组</param>
        /// <returns>与Q值对应的相应于概率的数组</returns>
        /// 过程由公式计算，《基于模糊Q学习的多机器人系统研究》 480页，公式3
        public static List<int> QvalueToP(List<Qclass> Qarray)
        {
            //退火系数由高变低
            if (T > 10)
                T = T - 10;
            else if (T > 1)
                T = T - 1;
            else
                T = 1;
            List<int> Array = new List<int>();
            double sum1 = 0;
            int sum2 = 0;
            foreach (Qclass i in Qarray)
            {
                sum1 += (Math.Exp(i.Qvalue / T) * i.flag);
            }
            /******************************************/
            if (sum1 == 0)
            {
                Console.WriteLine("sum1==0!!!!!!!!!!!!");
                Console.ReadLine();
            }
            /******************************************/
            foreach (Qclass i in Qarray)
            {
                sum2 += (int)(10000000 * (Math.Exp(i.Qvalue / T) * i.flag / sum1));
            }
            /***************************************/
            if (sum2 == 0)
            {
                Console.WriteLine("sum2==0!!!!!!!!!!!!!!!!");
                Console.ReadLine();
            }
            /****************************************/
            foreach (Qclass i in Qarray)
            {
                int k = (int)(10000000 * (Math.Exp(i.Qvalue / T) * i.flag / sum1));
                Array.Add(k);
            }
            Array.Add(sum2);//用于产生随机数的上限
            return Array;
        }

        /// <summary>
        /// 该函数用来描述每一个状态数组的特征，即四个相位排队长度的大小差异
        /// </summary>
        /// <param name="Array">枚举型状态数组转化的整形数组</param>
        /// <param name="Num">数组的元素个数</param>
        /// <returns>一个反应各相位排队差异的数组</returns>
        /// 初始化时判断 一个状态的四个元素大小趋势
        public static int[] sortArray(int[] Array, int Num)
        {
            int[] sortedArr = new int[4] { 1, 1, 1, 1 };
            for (int i = 0; i < Num; i++)
            {
                int k;
                for (k = 0; k < i; k++)
                {
                    if (Array[k] < Array[i])
                        sortedArr[i]++;
                    else
                        continue;

                }
                for (k = (i + 1); k < Num; k++)
                {
                    if (Array[k] < Array[i])
                        sortedArr[i]++;
                    else
                        continue;
                }
            }
            return sortedArr;
        }

        /// <summary>
        /// 判断状态动作对所对应的Q的有效性
        /// </summary>
        /// <param name="sortedArr">状态特征数组</param>
        /// <param name="queue">排队长度数组</param>
        /// <param name="Num">数组元素的个数</param>
        /// <returns>Q的有效与否</returns>
        public static bool boolOfQvalue(int[] sortedArr, int[] queue, int Num)
        {
            //数组a的元素均初始化为-1，由于排队长度均大于0
            //所以可以以此来判断a的值是否已被改变
            int[] a = new int[4] { -1, -1, -1, -1 };
            int temp;
            int j;
            for (int i = 0; i < Num; i++)
            {
                if (a[sortedArr[i] - 1] == -1)
                {
                    a[sortedArr[i] - 1] = queue[i];
                    continue;
                }
                //有两个状态相同时
                else if (sortedArr[i] < 4 && a[sortedArr[i]] == -1)
                {
                    //对相同状态对应的动作排序
                    if (queue[i] < a[sortedArr[i] - 1])
                    {
                        temp = a[sortedArr[i] - 1];
                        a[sortedArr[i] - 1] = queue[i];
                        a[sortedArr[i]] = temp;
                    }
                    else
                        a[sortedArr[i]] = queue[i];
                    continue;
                }
                //有三个状态相同时
                else if ((sortedArr[i] + 1) < 4 && a[sortedArr[i] + 1] == -1)
                {
                    //a[sortedArr[i] + 1] = queue[i];
                    //对相同的状态对应的动作排序
                    for (j = sortedArr[i] - 1; j < sortedArr[i] + 1; j++)
                    {
                        if (a[j] <= queue[i])
                            continue;
                        else
                            break;
                    }
                    for (int jj = sortedArr[i]; jj >= j; jj--)
                    {
                        a[jj + 1] = a[jj];
                    }
                    a[j] = queue[i];
                    continue;
                }
                //有四个状态相同时
                else if ((sortedArr[i] + 2) < 4 && a[sortedArr[i] + 2] == -1)
                {
                    //a[sortedArr[i] + 2] = queue[i];
                    for (j = sortedArr[i] - 1; j < sortedArr[i] + 2; j++)
                    {
                        if (a[j] <= queue[i])
                            continue;
                        else
                            break;
                    }
                    for (int jj = sortedArr[i] + 1; jj >= j; jj--)
                    {
                        a[jj + 1] = a[jj];
                    }
                    a[j] = queue[i];
                    continue;

                }//end of for循环

            }
            int t = 0;
            for (t = 0; t < Num - 1; t++)
            {
                if (a[t] <= a[t + 1])
                    continue;
                else
                    break;
            }

            if (t == Num - 1)
                return true;
            else
                return false;

        }

        /// <summary>
        /// 以概率选择一个动作，并返回动作的序号
        /// 用产生的随机数来选择
        /// </summary>
        /// <param name="random">随机数</param>
        /// <param name="QP">相应的概率数组</param>
        /// <returns>动作的序号</returns>
        public static int IndexOfAction(int random, List<int> QP)
        {
            int i;
            int sum3 = 0;
            for (i = 0; i < QP.Count - 1; i++)
            {
                sum3 += QP[i];
                if (random <= sum3)
                    break;
                else
                    continue;
            }
            return i;
        }

        /// <summary>
        /// 由动作的序号到具体动作的映射函数
        /// 返回的是一个动作，即一组配时
        /// </summary>
        /// <param name="MyIndexOfA">动作序号</param>
        /// <returns>与动作序号相应的动作数组</returns>
        public static int[] IndexToAction(int MyIndexOfA)
        {
            int[] array = new int[4];
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                j = MyIndexOfA / (int)(Math.Pow(OActionLength, (3.0 - i)));
                array[i] = OAction[j];
                MyIndexOfA = MyIndexOfA % (int)(Math.Pow(OActionLength, (3.0 - i)));
            }
            array[3] = OAction[MyIndexOfA];
            return array;
        }

        /// <summary>
        /// 由状态的序号到具体状态的映射函数
        /// 此处的状态是与枚举型相对应的整数
        /// </summary>
        /// <param name="MyIndexOfS">状态序号</param>
        /// <returns>与枚举型对应的整形数组</returns>
        public static int[] IndexToOstate(int MyIndexOfS)
        {
            int[] array = new int[4];
            int j = 0;
            for (int i = 0; i < 3; i++)
            {
                j = MyIndexOfS / (int)(Math.Pow(OStateLength, (3.0 - i)));
                array[i] = j;
                MyIndexOfS = MyIndexOfS % (int)(Math.Pow(OStateLength, (3.0 - i)));
            }
            array[3] = MyIndexOfS;
            return array;
        }

        //延时程序
        //private static void TimeDelay(int iInterval)
        //{
        //    DateTime now = DateTime.Now;
        //    while (now.AddMilliseconds(iInterval) > DateTime.Now)
        //    {
        //    }
        //    return;
        //}

        /// <summary>
        /// 求属于某个状态的Qvalue的最大值
        /// </summary>
        /// <param name="Q">以Qclass类为元素的List数组</param>
        /// <returns>该数组中元素的Qvalue值的最大值</returns>
        public static double MaxQOfS(List<Qclass> Q)
        {
            double max = Q[0].Qvalue;
            for (int i = 1; i < Q.Count; i++)
            {
                if (Q[i].Qvalue > max)
                    max = Q[i].Qvalue;
                else
                    continue;
            }
            return max;
        }

        /// <summary>
        /// 根据采取动作前后的排队长度计算奖赏值
        /// </summary>
        /// <param name="IntArr">采取新的配时前的排队长度数组</param>
        /// <param name="NextIntArr">采取新的配时后的排队长度数组</param>
        /// <param name="Num">排队长度数组的维数</param>
        /// <returns>针对所采取动作的奖赏值</returns>
        public static double Reward(int[] IntArr, int[] NextIntArr, int Num)
        {
            double sum1 = 0.0;
            for (int i = 0; i < Num; i++)
            {
                sum1 += (IntArr[i] - NextIntArr[i]);
            }
            return (sum1 * 10);
        }

        /// <summary>
        /// Q值单步更新函数
        /// </summary>
        /// <param name="InQ">以类Qclass为元素的List数组</param>
        /// <param name="index">动作序号</param>
        /// <param name="V">下一个状态所对应的最大的Q值</param>
        /// <param name="R">奖赏值</param>
        /// 过程：
        public static void UpdateQtable(List<Qclass> InQ, int index, double V, double R)
        {
            double learnrate = 1.0 / (double)(1 + InQ[index].visttime);
            Qclass testq = new Qclass();
            testq.Qvalue = (1.0 - learnrate) * (InQ[index].Qvalue) + learnrate * (R + discount * V);
            testq.visttime = InQ[index].visttime + 1;
            InQ[index] = testq;
        }

        /// <summary>
        /// listen thread function
        /// </summary>
        public static void RunServer()
        {
            IPHostEntry ipHost = null;
            try
            {
                ipHost = Dns.Resolve(Dns.GetHostName());
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取主机信息出现异常！");
            }
            IPAddress ipAddr = ipHost.AddressList[0];
            server = new TcpServer(ipAddr, 1006);
            server.Listen();
        }

        /// <summary>
        /// 将每一次接收到的排队长度写入文件
        /// </summary>
        /// <param name="byteArray">收到的排队长度数组</param>
        public static void MyWrite(byte[] byteArray)
        {
            try
            {
                StreamWriter m_Sw = new StreamWriter(@"C:\Myfile.text", true);
                if (queuetime == 0)
                    m_Sw.WriteLine("实验开始，接受的排队长度如下：");

                m_Sw.Write("第{0}个排队长度： ", queuetime++);
                foreach (byte b in byteArray)
                {
                    m_Sw.Write(b);
                    m_Sw.Write(" ");
                }
                m_Sw.WriteLine("\r\n");
                m_Sw.Close();
            }
            catch (IOException zqh)
            {
                Console.WriteLine("There is an IO exception!");
                Console.WriteLine(zqh.Message);
                return;
            }
        }


        public void InitialQTable(List<List<Qclass>> Qtable)
        {
            //初始化Q表  Richard
            for (int i = 0; i < Math.Pow(OStateLength, 4.0); i++)
            {
                List<Qclass> InQtable = new List<Qclass>();
                int[] osArray = IndexToOstate(i);
                int[] stArray = sortArray(osArray, 4);
                for (int j = 0; j < Math.Pow(OActionLength, 4.0); j++)
                {
                    int[] actArray = IndexToAction(j);
                    Qclass t = new Qclass();
                    t.Qvalue = 0;
                    t.visttime = 0;
                    if (boolOfQvalue(stArray, actArray, 4))
                        t.flag = 1;
                    else
                        t.flag = 0;

                    //////////////////////////////////////////////////////////////////////////
                    //Richard start
                    int temp = 1;
                    for (int k = 0; k < osArray.Length; k++)
                    {
                        for (int l = 0; l < stArray.Length; l++)
                        {
                            temp += Math.Abs(osArray[k] - stArray[l]);
                        }
                    }
                    temp = 1 / temp;
                    t.Qvalue = temp;
                    //Richard end
                    //////////////////////////////////////////////////////////////////////////


                    InQtable.Add(t);
                }
                Qtable.Add(InQtable);
            }
        }


        static void Main(string[] args)
        {


            List<OState[]> State = new List<OState[]>();//瀹氫箟涓€涓姸鎬侀泦鏁扮粍
            List<int[]> Action = new List<int[]>();//瀹氫箟涓€涓姩浣滈泦鏁扮粍
            List<List<Qclass>> Qtable = new List<List<Qclass>>();//瀹氫箟浜嗕竴涓猀琛?
            Console.WriteLine("The message below is:");

            Manuevent.Reset();


            //下面的for循环是初始化状态数组
            for (int i = 0; i < OStateLength; i++)
            {
                for (int j = 0; j < OStateLength; j++)
                {
                    for (int m = 0; m < OStateLength; m++)
                    {
                        for (int n = 0; n < OStateLength; n++)
                        {
                            OState[] S = { (OState)(i), (OState)(j), (OState)(m), (OState)(n) };
                            State.Add(S);
                        }//end of 一重for
                    }//end of 二重for
                }//end of 三重for
            }//end of 四重for

            //下面的for循环是初始化动作集数组
            for (int i = 0; i < OActionLength; i++)
            {
                for (int j = 0; j < OActionLength; j++)
                {
                    for (int m = 0; m < OActionLength; m++)
                    {
                        for (int n = 0; n < OActionLength; n++)
                        {
                            int[] A = new int[4] { OAction[i], OAction[j], OAction[m], OAction[n] };
                            Action.Add(A);
                        }//end of 一重for
                    }//end of 二重for
                }//end of 三重for
            }//end of 四重for


            ////初始化Q表
            //for (int i = 0; i < Math.Pow(OStateLength, 4.0); i++)
            //{
            //    List<Qclass> InQtable = new List<Qclass>();
            //    int[] osArray = IndexToOstate(i);
            //    int[] stArray = sortArray(osArray, 4);
            //    for (int j = 0; j < Math.Pow(OActionLength, 4.0); j++)
            //    {
            //        int[] actArray = IndexToAction(j);
            //        Qclass t = new Qclass();
            //        t.Qvalue = 0;
            //        t.visttime = 0;
            //        if (boolOfQvalue(stArray, actArray, 4))
            //            t.flag = 1;
            //        else
            //            t.flag = 0;
            //        InQtable.Add(t);
            //    }
            //    Qtable.Add(InQtable);
            //}

            //////////////////////////////////////////////////////////////////////////
            //初始化Q表  Richard start
            for (int i = 0; i < Math.Pow(OStateLength, 4.0); i++)
            {
                List<Qclass> InQtable = new List<Qclass>();
                int[] osArray = IndexToOstate(i);
                int[] stArray = sortArray(osArray, 4);
                for (int j = 0; j < Math.Pow(OActionLength, 4.0); j++)
                {
                    int[] actArray = IndexToAction(j);
                    Qclass t = new Qclass();
                    t.Qvalue = 0;
                    t.visttime = 0;
                    if (boolOfQvalue(stArray, actArray, 4))
                        t.flag = 1;
                    else
                        t.flag = 0;

                    //////////////////////////////////////////////////////////////////////////
                    //Richard start
                    //int actSum = 0;
                    //int osSum = 0;
                    //for (int k = 0; k < actArray.Length; k++)
                    //{
                    //    actSum += actArray[k];
                    //}

                    //for (int k = 0; k < osArray.Length; k++)
                    //{
                    //    osSum += osArray[k];
                    //}

                    double temp = 1;
                    for (int k = 0; k < osArray.Length; k++)
                    {
                        for (int l = 0; l < actArray.Length; l++)
                        {
                            temp += Math.Abs(osArray[k] - actArray[l]);
                        }
                    }
                    temp = 1 / temp;
                    t.Qvalue = temp;
                    //Richard end
                    //////////////////////////////////////////////////////////////////////////


                    InQtable.Add(t);
                }
                Qtable.Add(InQtable);
            }
            //初始化Q表  Richard end

            //////////////////////////////////////////////////////////////////////////
            //初始化Q表  Richard
            //InitialQTable(Qtable);


            Program.ListenThread = new Thread(RunServer);
            ListenThread.Start();


            /*************************************************/
            int[] NextIntArr = new int[4];//= MyRandom(4, 1, 25);
            Manuevent.WaitOne();
            Manuevent.Reset();
            int k_1 = 0;
            foreach (byte j in bytefour)
            {
                NextIntArr[k_1] = (int)j;
                k_1++;
            }
            MyWrite(bytefour);
            /**************************************************/
            Random ra = new Random();
            int count = 10000;//循环次数
            int ii = 0;
            while ((ii++) < count)
            {
                //获取当前的状态
                int[] IntArr = NextIntArr;
                //Console.Write("当前的排队长度为：");
                //foreach (int d in IntArr)
                //    Console.Write(d.ToString()+" ");
                //Console.WriteLine("\n");
                //进行排队长度到当前状态的转换
                OState[] CurrentState = IntArrToOStableArr(IntArr, 4);
                //当前状态的序号
                int MyIndexOfS = IndexOfState(CurrentState);
                List<int> QP = QvalueToP(Qtable[MyIndexOfS]);
                //Console.WriteLine("{0}",ra.Next(1,25));
                //要采取动作的序号
                int MyIndexOfA = IndexOfAction(ra.Next(0, QP[QP.Count - 1]), QP);
                int[] CurrntAction = IndexToAction(MyIndexOfA);
                //输出一组配时
                Console.Write("输出一组配时：");
                foreach (int ss in CurrntAction)
                {
                    Console.Write("{0}  ", ss);
                }
                Console.WriteLine("\n");

                int k_2 = 0;
                foreach (int ca in CurrntAction)
                {
                    bytefour[k_2] = (byte)ca;
                    k_2++;
                }
                newClient.Send(bytefour);
                //等待几个周期
                //采取配时后产生一组新的排队长度

                /***************************************/
                Console.WriteLine("等待下组排队长度!");
                Manuevent.WaitOne();
                Manuevent.Reset();

                int k_3 = 0;
                foreach (byte j in bytefour)
                {
                    NextIntArr[k_3] = (int)j;
                    k_3++;
                }
                MyWrite(bytefour);
                /******************************************/

                OState[] NextState = IntArrToOStableArr(NextIntArr, 4);     //把排队长度换成排队状态
                int MyIndexOfNextS = IndexOfState(NextState);//下一个状态的序号--获取排队状态的序列号
                double V = MaxQOfS(Qtable[MyIndexOfNextS]);//下一个状态所对应的Q值的最大值
                double R = Reward(IntArr, NextIntArr, 4);//奖赏函数
                //更新Q值
                UpdateQtable(Qtable[MyIndexOfS], MyIndexOfA, V, R);

            }
        }//end of Main()
    }
}
