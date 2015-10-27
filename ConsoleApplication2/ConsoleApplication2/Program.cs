using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    enum Direction
    {
            noAct = 0,
            up,
            down,
            left,
            right,
            maxAct
    };

    class Node: IComparable
    {
        public Int32[,] data;
        public string state;
        public Int32 value;
        public Int32 deepth;
        public Int32 col_0;
        public Int32 row_0;
        public Direction cannotAct;
        public Node father;
        public Node next;

        public Node()
        {
            this.data = new Int32[3,3];
            this.state = "";
            this.value = 0;
            this.deepth = 0;
            this.col_0 = 0;
            this.row_0 = 0;
            this.cannotAct = Direction.noAct;
            this.father = null;
            this.next = null;
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
            this.father = null;
            this.next = null;
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

            return (this.value+this.deepth).CompareTo(((Node)T).value+((Node)T).deepth);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            search begainSearch = new search();

            //测试优先队列功能
            PriorityQueue<int> que;
            que = new PriorityQueue<int>();
            que.Push(12);
            que.Push(132);
            que.Push(123);
            que.Push(212);
            que.Push(322);
            que.Push(126);
            que.Push(13);
            que.Push(189);
            while (!que.Empty())
            {
                Console.Write(que.Pop().ToString()+" ");
            }
        }

    }

    public class search
    {
        private Node begainNode;
        private Node targetNode;

        public search()
        {
            begainNode = new Node();
            targetNode = new Node();
            begainNode.row_0 = 2;
            begainNode.col_0 = 2;
            begainNode.state = "253741680";
            targetNode.state = "123804765";
            begainNode.ToRec();
            targetNode.ToRec();

            printState(begainNode);
            printState(targetNode);

            Dictionary<string, string> pathMap;
            pathMap = new Dictionary<string, string>();
            pathMap.Add(begainNode.state, "");

            Stack<string> outputStack;
            outputStack = new Stack<string>();
            Node result;
            result = null;
            if (searchPath(pathMap, result))
            {
                Console.WriteLine("原图");
                printString(begainNode.state);
                Console.WriteLine(begainNode.state);
                string str;
                outputStack.Push(targetNode.state);
                do
                {
                    str = outputStack.Peek();
                    outputStack.Push(pathMap[str]);
                 //   Console.WriteLine(str);
                } while (str != begainNode.state);

                outputStack.Pop();
                outputStack.Pop();
                int count = 0;

                Console.WriteLine("一共移动"+outputStack.Count.ToString()+"步");
                while (outputStack.Count > 0)
                {
                    count++;
                    Console.WriteLine("第 "+count.ToString()+" 步");
                    printString(outputStack.Peek());
                    outputStack.Pop();
                }
            }
        }

        bool searchPath(Dictionary<string, string> pathMap, Node result)
        {
            PriorityQueue<Node> priorityQueue;
            Stack<Node> pathStack;
            priorityQueue = new PriorityQueue<Node>();
            pathStack = new Stack<Node>();

            priorityQueue.Push(this.begainNode);
            pathStack.Push(this.begainNode);

            int cycle = 0;

            while (!priorityQueue.Empty())
            {
                cycle++;
           //     Console.WriteLine("第 "+cycle.ToString()+" 步");
            //    Console.WriteLine("队列中的元素  " + priorityQueue.Count);
                Node topNode = priorityQueue.Top();

                priorityQueue.Pop();

                #region 判断是否找到目状态
                if (matched(topNode, this.targetNode))
                {
                    printState(targetNode);
                    Console.WriteLine("搜索完成");
                    printState(topNode);
                    result = topNode;
                    return true;
                }
                #endregion

                int row = topNode.row_0;
                int col = topNode.col_0;

                if (row > 0 && topNode.cannotAct != Direction.up)
                {
                    Node curNode = new Node(topNode);

               //     Console.WriteLine("当前状态");
              //      printState(topNode);
               //     Console.WriteLine(row.ToString()+" "+col.ToString()+"   空格上移后状态");

                    exchange(curNode, row, col, row - 1, col);
                    curNode.ToString();
                    curNode.cannotAct = Direction.down;
                    if (!pathMap.ContainsKey(curNode.state))
                    {
                //        printState(curNode);

                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);

                //        Console.WriteLine("当前代价值："+(curNode.value + curNode.deepth).ToString());

                        curNode.father = topNode;
                        curNode.row_0 = row - 1;
                        curNode.col_0 = col;
                        priorityQueue.Push(curNode);
                        pathStack.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }

                if (row < 2 && topNode.cannotAct != Direction.down)
                {
                    Node curNode = new Node(topNode);

                //    Console.WriteLine("当前状态");
                //    printState(topNode);
                //    Console.WriteLine(row.ToString()+" "+col.ToString()+"    下移后状态");

                    exchange(curNode, row, col, row + 1, col);
                    curNode.ToString();
                    curNode.cannotAct = Direction.up;
                    if (!pathMap.ContainsKey(curNode.state))
                    {
                //        printState(curNode);

                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);

                 //       Console.WriteLine("当前代价值："+(curNode.value + curNode.deepth).ToString());

                        curNode.father = topNode;
                        curNode.row_0 = row + 1;
                        curNode.col_0 = col;
                        priorityQueue.Push(curNode);
                        pathStack.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }

                if (col > 0 && topNode.cannotAct != Direction.left)
                {
                    Node curNode = new Node(topNode);

                 //   Console.WriteLine("当前状态");
                 //   printState(topNode);
                //    Console.WriteLine(row.ToString()+" "+col.ToString()+"    左移之后的状态");

                    exchange(curNode, row, col, row, col - 1);
                    curNode.ToString();
                    curNode.cannotAct = Direction.left;
                    if (!pathMap.ContainsKey(curNode.state))
                    {
                //        printState(curNode);

                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);

                 //       Console.WriteLine("当前代价值："+(curNode.value + curNode.deepth).ToString());

                        curNode.father = topNode;
                        curNode.row_0 = row;
                        curNode.col_0 = col - 1;
                        priorityQueue.Push(curNode);
                        pathStack.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }

                if (col < 2 && topNode .cannotAct != Direction.right)
                {
                    Node curNode = new Node(topNode);

                  //  Console.WriteLine("当前状态");
                //    printState(topNode);
                  //  Console.WriteLine(row.ToString()+" "+col.ToString()+"     右移后状态");

                    exchange(curNode, row, col, row, col + 1);
                    curNode.ToString();
                    curNode.cannotAct = Direction.right;
                    if (!pathMap.ContainsKey(curNode.state))
                    {
                 //       printState(curNode);

                        curNode.deepth = topNode.deepth + 1;
                        curNode.value = getValue(curNode, this.targetNode);

                     //   Console.WriteLine("当前代价值："+(curNode.value+curNode.deepth).ToString());

                        curNode.father = topNode;
                        curNode.row_0 = row;
                        curNode.col_0 = col + 1;
                        priorityQueue.Push(curNode);
                        pathStack.Push(curNode);
                        pathMap.Add(curNode.state, topNode.state);
                    }
                }
            }

            return false;
        }

        void exchange(Node curNode, Int32 row, Int32 col, Int32 row2, Int32 col2)
        {
            Int32 temp = curNode.data[row, col];
            curNode.data[row, col] = curNode.data[row2, col2];
            curNode.data[row2, col2] = temp;
        }

        bool matched(Node curNode, Node tarNode)
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

    }

}
 