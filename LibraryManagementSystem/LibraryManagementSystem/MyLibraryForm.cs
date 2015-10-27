using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public partial class MylibraryForm : Form
    {
        /// <summary>
        /// 字段部分
        /// </summary>

        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";
        
        #region 字段    保存当前的用户是否是登录状态
        private bool loginMode;
        #endregion

        #region 字段    保存当前用户的登录状态
        private bool sysLogin;
        #endregion

        #region 字段    保存登录用户姓名
        private string loginName;
        #endregion

        #region 字段    保存登录ID
        private string loginID;
        #endregion

        #region 窗口字段    主窗口
        private HomeForm HomeFm;
        #endregion

        #region 窗口字段    登录子窗口界面
        private LoginForm LoginFm;
        #endregion

        #region 窗口字段    个人资料窗口
        private OwnFileForm OwnFileFm;
        #endregion

        #region 窗口字段    高级检索窗口
        private SuperSearchForm SuperSearchFm;
        #endregion

        #region 窗口字段    模糊检索窗口
        private FuzzySearchForm FuzzySearchFm;
        #endregion

        #region 窗口字段    最对查询窗口
        private PublicSearchForm MostSearchFm;
        #endregion

        #region 窗口字段    历史查询记录窗口
        private PublicSearchForm HistotySearchFm;
        #endregion

        #region 窗口字段    正在借阅记录窗口
        private PublicSearchForm BorrowingSearchFm;
        #endregion

        #region 窗口字段    我的查询记录窗口
        private PublicSearchForm MyHistorySearchFm;
        #endregion


        private enum searchType
        {
            searchHistory,
            borrowHistory,
            borrowNow
        }

        /// <summary>
        /// 函数信息部分
        /// </summary>

        #region 构造函数，初始化系统主窗口
        public MylibraryForm()
        {
            InitializeComponent();
            InitMyFrame();
        }
        #endregion

        #region 初始化自定义的窗口
        private void InitMyFrame()
        {
            loginName = "";
            loginID = "";
            loginMode = false;
            sysLogin = false;
            this.IsMdiContainer = true;
            InitHomeFrame();
            StartAndDisposeFms();
            this.systemLoginLable.ContextMenuStrip = loginPopMenu;
        }
        #endregion

        #region 在加载程序时初始化各窗口变量
        private void StartAndDisposeFms()
        {
            LoginFm = new LoginForm();
            LoginFm.Dispose();

            OwnFileFm = new OwnFileForm("",false);
            OwnFileFm.Dispose();

            SuperSearchFm = new SuperSearchForm();
            SuperSearchFm.Dispose();

            FuzzySearchFm = new FuzzySearchForm();
            FuzzySearchFm.Dispose();

            MostSearchFm = new PublicSearchForm();
            MostSearchFm.Dispose();

            HistotySearchFm = new PublicSearchForm();
            HistotySearchFm.Dispose();

            BorrowingSearchFm = new PublicSearchForm();
            BorrowingSearchFm.Dispose();

            MyHistorySearchFm = new PublicSearchForm();
            MyHistorySearchFm.Dispose();
        }
        #endregion

        #region 初始化主窗口
        private void InitHomeFrame()
        {
            HomeFm = new HomeForm();
            HomeFm.TopLevel = false;
            HomeFm.MdiParent = this;
            HomeFm.Dock = DockStyle.Fill;
            HomeFm.Show();
        }
        #endregion

        #region 切换窗口时  关闭不用的窗口
        private void KillUselessFms()
        {
            if (!this.HomeFm.IsDisposed)
            {
                this.HomeFm.Dispose();
            }
            if (!this.SuperSearchFm.IsDisposed)
            {
                this.SuperSearchFm.Dispose();
            }
            if (!this.OwnFileFm.IsDisposed)
            {
                this.OwnFileFm.Dispose();
            }
            if (!this.FuzzySearchFm.IsDisposed)
            {
                this.FuzzySearchFm.Dispose();
            }
            if (!this.MostSearchFm.IsDisposed)
            {
                this.MostSearchFm.Dispose();
            }
            if (!this.HistotySearchFm.IsDisposed)
            {
                this.HistotySearchFm.Dispose();
            }
            if (!this.BorrowingSearchFm.IsDisposed)
            {
                this.BorrowingSearchFm.Dispose();
            }
        }
        #endregion

        #region 单击  关于  菜单项触发
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("图书馆管理系统 V1.0\nCopyright(c) 姜佳君", "软件信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region 单击  帮助  菜单项触发
        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(new Control(), "../../LibrarySystemHelp.doc"); 
        }
        #endregion

        #region 单击  登录系统  按钮
        private void systemLoginLable_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(this.systemLoginLable.Text.EndsWith("登录系统"))
            {
                LoginFm = new LoginForm();
                LoginFm.StartPosition = FormStartPosition.CenterScreen;
                LoginFm.Show();
            }
        }
        #endregion

        #region 主窗口激活时
        private void MylibraryForm_Activated(object sender, EventArgs e)
        {
            if (!LoginFm.IsDisposed)
            {
                loginMode = LoginFm.CheckOutRight();
                loginName = LoginFm.getLoginName();
                loginID = LoginFm.getLoginID();
                sysLogin = LoginFm.SystemLogin();
                LoginFm.Dispose();
            }
            if (loginMode)
            {
                this.systemLoginLable.Text = loginName + " 欢迎";
            }
        }
        #endregion

        #region 登录成功后   右键登录用户名  弹出菜单
        private void systemLoginLable_MouseClick(object sender, MouseEventArgs e)
        {
            if (loginMode && e.Button == MouseButtons.Right)
            {
                this.loginPopMenu.Show();
            }
        }
        #endregion

        #region 单击菜单栏中和弹出菜单中的  登出  按钮时
        private void 退出登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult re;
            if (!loginMode)
            {
                re = MessageBox.Show("您还没有登录系统,是否登录系统？",
                                      "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (re == DialogResult.Yes)
                {
                    systemLoginLable_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(new LinkLabel.Link()));
                }
                return;
            }
            re = MessageBox.Show("确定登出？","提示信息",
                                   MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                this.systemLoginLable.Text = "登录系统";
                loginMode = false;
                loginName = "";
            }
        }
        #endregion

        #region 单击菜单中的  退出  按钮时
        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("您还没有登录系统,是否登录系统？", 
                                              "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                this.Close();
            }
        }
        #endregion

        #region 单击菜单栏中的 和弹出菜单中的  个人资料  按钮时
        private void 个人资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loginMode)
            {
                DialogResult re = MessageBox.Show("您还没有登录系统,是否登录系统？", 
                                                  "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (re == DialogResult.Yes)
                {
                    systemLoginLable_LinkClicked(sender, new LinkLabelLinkClickedEventArgs(new LinkLabel.Link()));
                }
                return;
            }
            if (!OwnFileFm.IsDisposed)
            {
                return;
            }

            KillUselessFms();

            OwnFileFm = new OwnFileForm(loginID,sysLogin);
            OwnFileFm.TopLevel = false;
            OwnFileFm.MdiParent = this;
            //OwnFileFm.Parent = this.tableLayoutPanel2;
            OwnFileFm.Dock = DockStyle.Fill;
            OwnFileFm.Show();

        }
        #endregion

        #region 单击菜单栏中的  首页   按钮时
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!HomeFm.IsDisposed)
            {
                return;
            }

            KillUselessFms();

            HomeFm = new HomeForm();
            HomeFm.TopLevel = false;
            HomeFm.MdiParent = this;
            HomeFm.Dock = DockStyle.Fill;
            HomeFm.Show();
        }
        #endregion

        private void 高级检索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SuperSearchFm.IsDisposed)
            {
                return;
            }

            KillUselessFms();

            SuperSearchFm = new SuperSearchForm();
            SuperSearchFm.TopLevel = false;
            SuperSearchFm.MdiParent = this;
            SuperSearchFm.Dock = DockStyle.Fill;
            SuperSearchFm.Show();


        }

        private void 分类检索ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!FuzzySearchFm.IsDisposed)
            {
                return;
            }

            KillUselessFms();

            FuzzySearchFm = new FuzzySearchForm();
            FuzzySearchFm.TopLevel = false;
            FuzzySearchFm.MdiParent = this;
            FuzzySearchFm.Dock = DockStyle.Fill;
            FuzzySearchFm.Show();
        }

        private void 最多查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loginMode)
            {
                MessageBox.Show("你还没登录系统，请登录系统后查看", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.MostSearchFm.IsDisposed)
            {
                return;
            }

            KillUselessFms();
            MostSearchFm = new PublicSearchForm();
            MostSearchFm.TopLevel = false;
            MostSearchFm.MdiParent = this;
            MostSearchFm.Dock = DockStyle.Fill;
            MostSearchFm.lable1 = "我的查询记录信息";
            MostSearchFm.Show();
            search(searchType.searchHistory);

        }

        private void 历史借阅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loginMode)
            {
                MessageBox.Show("你还没登录系统，请登录系统后查看", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!this.HistotySearchFm.IsDisposed)
            {
                return;
            }

            KillUselessFms();

            HistotySearchFm = new PublicSearchForm();
            HistotySearchFm.TopLevel = false;
            HistotySearchFm.MdiParent = this;
            HistotySearchFm.Dock = DockStyle.Fill;
            HistotySearchFm.lable1 = "历史借阅图书信息";
            HistotySearchFm.Show();
            search(searchType.borrowHistory);
        }

        private void 正在借阅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loginMode)
            {
                MessageBox.Show("你还没登录系统，请登录系统后查看","提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!this.BorrowingSearchFm.IsDisposed)
            {
                return;
            }

            KillUselessFms();

            BorrowingSearchFm = new PublicSearchForm();
            BorrowingSearchFm.TopLevel = false;
            BorrowingSearchFm.MdiParent = this;
            BorrowingSearchFm.Dock = DockStyle.Fill;
            BorrowingSearchFm.lable1 = "正在借阅图书信息";
            BorrowingSearchFm.Show();
            search(searchType.borrowNow);
        }

        private void search(searchType searchtype)
        {

            SqlConnection sqlconnection = new SqlConnection(conStr);

            switch (searchtype)
            {
                case searchType.searchHistory:
                    {
                        try
                        {
                            sqlconnection.Open();
                            string comStr = string.Format("select Book_ISBN 图书ISBN,Book_NAME 图书名,Book_MAINAUTHOR 主编, Book_SEARCHID 检索号,Book_CLASS 图书类别,Book_PUBLISHER 出版社,Book_INTRODUCTION 图书简介,Book_REMAIN 剩余量"
                                                        + " from dboBook,dboSearchRecord"
                                                        + " where dboBook.Book_ISBN=dboSearchRecord.SearchRecord_Book_ISBN and dboSearchRecord.SearchRecord_user_IID = \'{0}\'"
                                                        + " order by (dboSearchRecord.SearchRecord_COUNT)", loginID);

                            DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "result", sqlconnection);
                            MostSearchFm.dataGridView1.DataSource = dataSet.Tables[0];
                        }
                        catch (System.Exception)
                        {

                        }
                        finally
                        {
                            sqlconnection.Close();
                        }
                        break;
                    }
                case searchType.borrowHistory:
                    {
                        try
                        {
                            sqlconnection.Open();
                            string comStr = string.Format("select Book_NAME 图书名,Borrow_BORROW_TIME 借书日期,Borrow_RETURN_TIME 还书日期, Book_MAINAUTHOR 主编, Book_SEARCHID 检索号,Book_CLASS 图书类别,Book_PUBLISHER 出版社,Book_INTRODUCTION 图书简介,Book_REMAIN 剩余量"
                                                        + " from dboBook,dboBorrow"
                                                        + " where dboBook.Book_ISBN = dboBorrow.Borrow_Book_ISBN and dboBorrow.Borrow_user_IID = \'{0}\' and dboBorrow.Borrow_BORROW_STATE = 1"
                                                        + " order by (dboBorrow.Borrow_BORROW_TIME)", loginID);

                            DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "result", sqlconnection);
                            HistotySearchFm.dataGridView1.DataSource = dataSet.Tables[0];
                        }
                        catch (System.Exception )
                        {

                        }
                        finally
                        {
                            sqlconnection.Close();
                        }
                        break;
                    }
                case searchType.borrowNow:
                    {
                        try
                        {
                            string nowTime = DateTime.Now.Date.ToString().Substring(0,10);
                            sqlconnection.Open();
                            string comStr = string.Format("select Book_NAME 图书名, DateDiff(day, Borrow_BORROW_TIME, '" + nowTime +"') AS 距还书日期_天, Book_MAINAUTHOR 主编,Book_SEARCHID 检索号,Book_CLASS 图书类别,Book_PUBLISHER 出版社,Book_INTRODUCTION 图书简介" 
                                                        + " from dboBook,dboBorrow"
                                                        +" where dboBook.Book_ISBN=dboBorrow.Borrow_Book_ISBN and dboBorrow.Borrow_user_IID = \'{0}\' and dboBorrow.Borrow_BORROW_STATE=0"
                                                        +" order by DateDiff(day, Borrow_BORROW_TIME, Borrow_RETURN_TIME) asc", loginID);

                            DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "result", sqlconnection);
                            BorrowingSearchFm.dataGridView1.DataSource = dataSet.Tables[0];
                        }
                        catch (System.Exception )
                        {
                            MessageBox.Show("error");
                        }
                        finally
                        {
                            sqlconnection.Close();
                        }
                        break;
                    }
            }
            
            

        }

        private void 录入新书CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loginMode)
            {
                MessageBox.Show("请先以管理员身份登录系统","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if (!sysLogin)
            {
                MessageBox.Show("您不能进行此操作", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;                 
            }
            NewBookForm NewBookFm = new NewBookForm();
            NewBookFm.ShowDialog();
        }

        private void 创建管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loginMode || !sysLogin)
            {
                MessageBox.Show("只能在管理员登录状态下才可以创建管理员用户","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            NewAdminForm NewAdminFm = new NewAdminForm();
            NewAdminFm.ShowDialog();
        }

        private void 选项OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!loginMode || !sysLogin)
            {
                MessageBox.Show("只有管理员有此权限", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DeleteBookForm DeleteBookFm = new DeleteBookForm();
            DeleteBookFm.ShowDialog();
        }

    }
}
