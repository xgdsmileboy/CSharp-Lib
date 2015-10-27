using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8DigitalProblem
{
    public partial class PlayForm : Form 
    {
        /// <注释>
        /// 初始状态和目标状态
        /// </summary>
        private Node begainNode; 
        private Node targetNode;
        private Stack<string> outputStack;


        /// <summary>
        /// 在设置状态时用的计数器以及标志量
        /// 
        /// param:
        ///     InputCount
        ///         计数变量，用于在图形输入时记录已经输入的最大数字
        ///     stepCount
        ///         计数变量，用于记录求解后，需要移动的步数
        ///    
        /// flag:
        ///     setBegainNode
        ///         标记量，用于标记当前是否在输入开始状态 =true 已输入 =false 未输入
        ///     setTargetNode
        ///         标记量，用于标记当前是否在输入目标状态 =true 已输入 =false 未输入
        ///     alreadySetStartNode
        ///         标记量，用于标记当前初始状态是否已经设置完成 =true 完成 =false 未完成
        /// </summary>

        private Int32 InputCount;
        private Int32 stepCount;
        private bool setBegainNode;
        private bool setTargetNode;
        private bool alreadySetStartNode;

        /// <summary>
        /// 界面元素
        /// 
        /// widget:
        ///      startLabel
        ///             初始状态展示
        ///      endLabel
        ///             目标状态展示
        ///      btn
        ///             用于演示和输入的按钮
        ///       
        /// </summary>
        private Label[,] startLabel = new Label[3, 3];
        private Label[,] endLabel = new Label[3, 3];
        private Button[,] btn = new Button[3,3];

        /// <summary>
        /// 枚举类型，移动的方向
        /// </summary>
        public enum Direction
        {
            noAct = 0,     //定界
            up,
            down,
            left,
            right,
            maxAct  //边界，定界用，其他无用，习惯
        }

        /// <summary>
        /// 搜索状态节点类
        /// </summary>
        public class Node : IComparable
        {
            public Int32[,] data;
            public string state;
            public Int32 value;
            public Int32 deepth;
            public Int32 col_0;
            public Int32 row_0;
            public Direction cannotAct;

            public Node()
            {
                this.data = new Int32[3, 3];
                this.state = "";
                this.value = 0;
                this.deepth = 0;
                this.col_0 = 0;
                this.row_0 = 0;
                this.cannotAct = Direction.noAct;
            }

            public Node(Node node)
            {
                this.data = new Int32[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        this.data[i, j] = node.data[i, j];
                    }
                }
                this.state = node.state;
                this.value = node.value;
                this.deepth = node.deepth;
                this.col_0 = node.col_0;
                this.row_0 = node.row_0;
                this.cannotAct = node.cannotAct;
            }

            public new void ToString()
            {
                this.state = "";
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        this.state += this.data[i, j].ToString();
                    }
                }
            }

            public void ToRec()
            {
                int k = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        this.data[i, j] = Int32.Parse(this.state.ElementAt(k).ToString());
                        k++;
                    }
                }
            }

            public int CompareTo(Object T)
            {
                if (!(T is Node))
                {
                    Console.WriteLine("比较类型错误！");
                }

                return (this.value + this.deepth).CompareTo(((Node)T).value + ((Node)T).deepth);
            }
        } 



        //以下是窗体控制内容

        public PlayForm() 
        {
            InitializeComponent();
            outputStack = new Stack<string>();
            begainNode = new Node(); 
            targetNode = new Node();
            AddWeghit();
            InitMyData();
            InitMyParam();
            UnEnableMyForm();
            resetStartEndState();
        }

        private void AddWeghit()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].Dock = DockStyle.Fill; 
                    btn[i, j].UseVisualStyleBackColor = true;
                    btn[i, j].Click += new System.EventHandler(this.btn_Click);
                    this.tableLayoutPanel2.Controls.Add(this.btn[i, j], i, j);

                    startLabel[i, j] = new Label();
                    startLabel[i, j].Dock = DockStyle.Fill;
                    startLabel[i, j].Text = "未设";
                    startLabel[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    this.tableLayoutPanel7.Controls.Add(this.startLabel[i, j], i, j);

                    endLabel[i, j] = new Label();
                    endLabel[i, j].Dock = DockStyle.Fill;
                    endLabel[i, j].Text = "未设";
                    endLabel[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    this.tableLayoutPanel8.Controls.Add(this.endLabel[i, j], i, j);
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (setTargetNode && setBegainNode && InputCount == 9)
            {
                return;
            }
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (sender.Equals(btn[i, j]))
                        {
                            if (this.btn[i,j].BackColor == SystemColors.Control)
                            {
                                if (InputCount == 9)
                                {
                                    MessageBox.Show("设置完成，请单击确定完成设置");
                                    return;
                                }

                                btn[i, j].Text = InputCount.ToString();
                                btn[i, j].BackColor = SystemColors.Highlight;
                                if (setBegainNode && !setTargetNode)
                                {
                                    begainNode.data[i, j] = InputCount;
                                }
                                else
                                {
                                    targetNode.data[i, j] = InputCount;
                                }
                                InputCount += 1;
                                return;
                            }
                            else
                            {
                                btn[i, j].BackColor = SystemColors.Control;
                                if (setBegainNode && !setTargetNode)
                                {
                                    begainNode.data[i, j] = 0;
                                }
                                else
                                {
                                    targetNode.data[i, j] = 0;
                                }
                                InputCount -= 1;
                                updateNumber(Int32.Parse(btn[i, j].Text));
                                btn[i, j].Text = "未设";
                                return;
                            }
                        }
                    }
                }
                if (setBegainNode && !setTargetNode)
                {
                    begainNode.ToString();
                }
                else
                {
                    targetNode.ToString();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace.ToString());
            }
        }

        private void InitMyData()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    begainNode.data[i, j] = 0;
                    targetNode.data[i, j] = 0;
                }
            }
        }

        private void InitMyParam()
        {
            InputCount = 1;
            stepCount = 0;
            setBegainNode = false;
            setTargetNode = false;
            this.btnNext.Enabled = false;
            alreadySetStartNode = false;
        }

        private void UnEnableMyForm()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    btn[i, j].Enabled = false;
                    btn[i, j].Text = "未设";
                    btn[i, j].BackColor = SystemColors.Control;
                }
            }
        }

        private void updateNumber(Int32 lowNum)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (btn[i, j].BackColor == SystemColors.Highlight && Int32.Parse(btn[i, j].Text) > lowNum)
                    {
                        btn[i, j].Text = (Int32.Parse(btn[i, j].Text) - 1).ToString();
                    }
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (InputCount != 1 && InputCount != 9)
            {
                MessageBox.Show("您还未设完成");
                return;
            }

            if (this.startLabel[0,0].BackColor == SystemColors.Highlight || this.startLabel[1,1].BackColor == SystemColors.Highlight)
            {
                MessageBox.Show("您已完成初态设置！若想重设请点重置");
                return;
            }

            setBegainNode = true;
            setTargetNode = false;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.btn[i, j].Enabled = true;
                }
            }

            this.Text = "请按顺序单击按钮设置初态";
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (!alreadySetStartNode)
            {
                MessageBox.Show("请先完成初态设置");
                return;
            }
            if (this.endLabel[0,0].BackColor == SystemColors.Highlight || this.endLabel[1,1].BackColor == SystemColors.Highlight)
            {
                MessageBox.Show("您已完成末态设置！若想重设请点重置");
                return;
            }


            setTargetNode = true;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.btn[i, j].Enabled = true;
                }
            }

            this.Text = "请按循序设置末状态";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (InputCount != 9)
            {
                if (setTargetNode || setBegainNode)
                {
                    MessageBox.Show("请完成当前设置");
                    return;
                }
                else
                {
                    MessageBox.Show("请设置始末状态");
                    return;
                }
            }

            if (setBegainNode & !setTargetNode)
            {
                InputCount = 1;
                setBegainNode = true;
                alreadySetStartNode = true;
                UnEnableMyForm();
                this.Text = "8数码问题求解演示";
                showStartState();
                begainNode.ToString();

            }
            else if (setTargetNode && setBegainNode)
            {
                setTargetNode = true;
                this.Text = "8数码问题求解演示";
                showEndState();
                targetNode.ToString();
                dynamicShowState(begainNode);
                MessageBox.Show("完成设置，请单击演示开始");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Text = "8数码问题求解演示";
            InitMyData();
            InitMyParam();
            UnEnableMyForm();
            resetStartEndState();
        }

        private void showStartState()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (begainNode.data[i, j] != 0)
                    {
                        startLabel[i, j].Text = begainNode.data[i, j].ToString();
                        startLabel[i, j].BackColor = SystemColors.Highlight;
                    }
                    else
                    {
                        begainNode.col_0 = i;
                        begainNode.row_0 = j;
                        startLabel[i, j].Text = "空";
                        startLabel[i, j].BackColor = SystemColors.Control;
                    }

                }
            }
        }

        private void showEndState()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (targetNode.data[i, j] != 0)
                    {
                        endLabel[i, j].Text = targetNode.data[i, j].ToString();
                        endLabel[i, j].BackColor = SystemColors.Highlight;
                    }
                    else
                    {
                        targetNode.col_0 = i;
                        targetNode.row_0 = j;
                        endLabel[i, j].Text = "空";
                        endLabel[i, j].BackColor = SystemColors.Control;
                    }

                }
            }
        }

        private void resetStartEndState()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.startLabel[i, j].Text = "未设";
                    this.endLabel[i, j].Text = "未设";
                    this.startLabel[i, j].BackColor = SystemColors.Control;
                    this.endLabel[i, j].BackColor = SystemColors.Control;
                }
            }
        }

        private void dynamicShowState(Node curNode)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (curNode.data[i, j] != 0)
                    {
                        btn[i, j].Text = curNode.data[i, j].ToString();
                        btn[i, j].BackColor = SystemColors.Highlight;
                    }
                    else
                    {
                        btn[i, j].Text = "空";
                        btn[i, j].BackColor = SystemColors.Control;
                    }
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (outputStack.Count <= 0)
            {
                this.btnPlay.Enabled = true;
                this.btnNext.Enabled = false;
                return;
            }
            this.stepCount--;
            if (stepCount <= 0)
            {
                this.Text = "移动完成";
                this.btnNext.Enabled = false;
            }
            else
            {
                this.Text = "还要移动" + stepCount.ToString() + "步";
            }
            showInForm(this.outputStack.Pop());
        }

        private void btnSetString_Click(object sender, EventArgs e)
        {
            if (this.btnSetString.Text == "字符串")
            {
                this.btn_OK.Enabled = true;
                this.btnSetString.Text = "图形设置";
                this.textBoxStart.Text = "";
                this.textBoxTarget.Text = "";
                this.textBoxStart.Enabled = true;
                this.textBoxTarget.Enabled = true;

                this.btnOK.Enabled = false;
                this.btnPlay.Enabled = false;
                this.btnReset.Enabled = false;
                this.btnStart.Enabled = false;
                this.btnEnd.Enabled = false;
            }
            else
            {
                this.btn_OK.Enabled = false;
                this.btnSetString.Text = "字符串";
                this.textBoxStart.Text = "起始状态";
                this.textBoxTarget.Text = "目标状态";
                this.textBoxStart.Enabled = false;
                this.textBoxTarget.Enabled = false;

                this.btnOK.Enabled = true; ;
                this.btnPlay.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnStart.Enabled = true;
                this.btnEnd.Enabled = true;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (textBoxStart.Text.Length < 9 || textBoxTarget.Text.Length < 9)
            {
                MessageBox.Show("输入错误！");
                return;
            }

            this.begainNode.state = this.textBoxStart.Text;
            this.targetNode.state = this.textBoxTarget.Text;
            this.begainNode.ToRec();
            this.targetNode.ToRec();
            begainNode.row_0 = begainNode.state.IndexOf('0') / 3;
            begainNode.row_0 = begainNode.state.IndexOf('0') % 3;
            this.btnPlay.Enabled = true;
            this.setBegainNode = true;
            this.setTargetNode = true;
            this.alreadySetStartNode = true;
            this.btn_OK.Enabled = false;
            showStartState();
            showEndState();
            dynamicShowState(begainNode);
            MessageBox.Show("设置完成！");
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (!setBegainNode || !setTargetNode)
            {
                MessageBox.Show("请先设置始末状态");
                return;
            }
            else
            {
                this.btnOK.Enabled = false;
                this.btnPlay.Enabled = false;
                this.btnReset.Enabled = false;
                this.btnStart.Enabled = false;
                this.btnEnd.Enabled = false;
                dynamicShowState(begainNode);

                search();

                this.btnOK.Enabled = true;
                this.btnPlay.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnStart.Enabled = true;
                this.btnEnd.Enabled = true;
                this.textBoxStart.Text = "起始状态";
                this.textBoxTarget.Text = "目标状态";
                this.textBoxStart.Enabled = false;
                this.textBoxTarget.Enabled = false;
                this.btn_OK.Enabled = false;
            }
        }


        //以下是搜索策略函数部分

        public void search()
        {
            Dictionary<string, string> pathMap;
            pathMap = new Dictionary<string, string>();
            pathMap.Add(begainNode.state, "");

            if (getRev(begainNode.state) % 2 != getRev(targetNode.state) % 2)
            {
                this.btnNext.Enabled = false;
                MessageBox.Show("初末态逆序数奇偶性不同，无解！");
                return;
            }

            if (searchPath(pathMap))
            {
                string str;
                this.outputStack.Clear();
                this.outputStack.Push(targetNode.state);
                do
                {
                    str = outputStack.Peek();
                    if (!pathMap.ContainsKey(str))
                    {
                        this.btnNext.Enabled = false;
                        MessageBox.Show("求解出错！");
                        return;
                    }
                    this.outputStack.Push(pathMap[str]);
                } while (str != begainNode.state);

                outputStack.Pop();
                outputStack.Pop();
                stepCount = outputStack.Count;
                if (stepCount <= 0)
                {
                    MessageBox.Show("初态和末态相同");
                }
                else
                {
                    MessageBox.Show("求解成功！共需要移动" + stepCount.ToString() + "步");
                    this.Text = "一共需要移动" + stepCount.ToString() + "步";
                }
                this.btnNext.Enabled = true;
                this.btnPlay.Enabled = false;
            }
            else
            {
                MessageBox.Show("无解");
            }
        }

        public bool searchPath(Dictionary<string, string> pathMap)
        {
            PriorityQueue<Node> priorityQueue;
            priorityQueue = new PriorityQueue<Node>();

            priorityQueue.Push(this.begainNode);

            while (!priorityQueue.Empty())
            {
                Node topNode = priorityQueue.Pop();

                #region 判断是否找到目状态
                if (matched(topNode, this.targetNode))
                {
                    MessageBox.Show("Finished!");
                    return true;
                }
                #endregion

                int row = topNode.row_0;
                int col = topNode.col_0;

                if (row > 0 && topNode.cannotAct != Direction.up)
                {
                    Node curNode = new Node(topNode);

                    exchange(curNode, row, col, row - 1, col);
                    curNode.ToString();
                    curNode.cannotAct = Direction.down;

                    if (!pathMap.ContainsKey(curNode.state))
                    {
                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);
                        curNode.row_0 = row - 1;
                        curNode.col_0 = col;
                        priorityQueue.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }

                if (row < 2 && topNode.cannotAct != Direction.down)
                {
                    Node curNode = new Node(topNode);

                    exchange(curNode, row, col, row + 1, col);
                    curNode.ToString();
                    curNode.cannotAct = Direction.up;

                    if (!pathMap.ContainsKey(curNode.state))
                    {
                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);
                        curNode.row_0 = row + 1;
                        curNode.col_0 = col;
                        priorityQueue.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }

                if (col > 0 && topNode.cannotAct != Direction.left)
                {
                    Node curNode = new Node(topNode);

                    exchange(curNode, row, col, row, col - 1);
                    curNode.ToString();
                    curNode.cannotAct = Direction.left;
                    if (!pathMap.ContainsKey(curNode.state))
                    {
                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);
                        curNode.row_0 = row;
                        curNode.col_0 = col - 1;
                        priorityQueue.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }

                if (col < 2 && topNode.cannotAct != Direction.right)
                {
                    Node curNode = new Node(topNode);
                    exchange(curNode, row, col, row, col + 1);
                    curNode.ToString();
                    curNode.cannotAct = Direction.right;
                    if (!pathMap.ContainsKey(curNode.state))
                    {
                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);
                        curNode.row_0 = row;
                        curNode.col_0 = col + 1;
                        priorityQueue.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }
            }

            return false;
        }

        public void exchange(Node curNode, Int32 row, Int32 col, Int32 row2, Int32 col2)
        {
            Int32 temp = curNode.data[row, col];
            curNode.data[row, col] = curNode.data[row2, col2];
            curNode.data[row2, col2] = temp;
        }

        public bool matched(Node curNode, Node tarNode)
        {
            if (curNode.state == tarNode.state)
            {
                return true;
            }
            return false;
        }

        private Int32 getValue(Node curNode, Node targNode)
        {
            Int32 value = 0;
            for (int i = 0; i < curNode.state.Length; i++)
            {
                if (curNode.state.ElementAt(i) != targNode.state.ElementAt(i) && curNode.state.ElementAt(i) != 0)
                {
                    value++;
                }
            }
            return value;
        }

        private void printState(Node node)
        {
            string str = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (node.data[i, j] == 0)
                    {
                        str += " " + "   ";
                    }
                    else
                    {
                        str += node.data[i, j].ToString() + "   ";
                    }
                }
                str += Environment.NewLine;
            }
            Console.Write(str);
        }

        private void printString(string str)
        {
            string temp = "";
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (str.ElementAt(k) != '0')
                    {
                        temp += str.ElementAt(k).ToString() + " ";
                    }
                    else
                    {
                        temp += " " + " ";
                    }
                    k++;
                }
                temp += Environment.NewLine;
            }
            Console.Write(temp);
        }

        private void showInForm(string str)
        {
            Node curNode = new Node();
            curNode.state = str;
            curNode.ToRec();
            dynamicShowState(curNode);
        }

        int getRev(string s)  //逆序数
        {
            int Rev = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (s[j] > s[i] && s[i] != '0')
                        Rev++;
                }
            }
            return Rev;
        }

    }
}
