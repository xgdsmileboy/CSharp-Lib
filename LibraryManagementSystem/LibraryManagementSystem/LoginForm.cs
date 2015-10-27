using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace LibraryManagementSystem
{
    public partial class LoginForm : Form
    {
        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";
        
        #region 字段    是否登录成功标志量
        private bool canLogin;
        #endregion

        #region 字段    是否为管理员登录
        private bool sysLogin;
        #endregion

        #region 字段    登录用户名
        private string loginID;
        #endregion

        #region 字段    用户姓名信息
        private string loginName;
        #endregion

        #region 字段    登录系统时的查询结果表
        private DataSet dataSet;
        #endregion

        #region 构造函数 初始化登录窗口
        public LoginForm()  
        {
            canLogin = false;  //初始化登录验证失败
            loginID = "";
            loginName = "";
            InitializeComponent();  //初始化窗口
            this.Focus();
        }
        #endregion

        #region 点击  登录  进行验证时触发
        private void btnlogin_Click(object sender, EventArgs e)
        {
            bool loginCheckOut = false;
            if (rbtnUser.Checked)  //以用户身份登录验证
            {
                loginCheckOut = user_checkOut(textLogin.Text.Trim(), textPassworld.Text);
            }
            else  //以管理员身份登录验证
            {
                loginCheckOut = admin_checkOut(textLogin.Text.Trim(), textPassworld.Text);
            }
            if (loginCheckOut)  //验证成功
            {
                loginID = textLogin.Text.Trim();
                canLogin = true;
                this.Close();
            }
            else   //验证失败
            {
                canLogin = false;
                MessageBox.Show("你输入的登录名或密码有误，请重新输入！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textLogin.Focus();
                textLogin.SelectAll();
            }
 
        }
        #endregion

        #region 用户  登录验证函数
        private bool user_checkOut(string loginID, string password)
        {
            bool login = false;
            SqlConnection sqlconnection = new SqlConnection(conStr);

            string comStr = string.Format("select Student_REGISTRED,Student_PASSWORD, Student_NAME from dboStudent where Student_IID = \'{0}\'", loginID);

            dataSet = new DataSet();
            dataSet.Clear();
            dataSet = SqlCommondClass.GetDataSet(comStr, "dboStudent", sqlconnection);
            if (dataSet.Tables[0].Rows.Count == 0)
            {
                login = false;
                return login;
            }

            if (dataSet.Tables[0].Rows[0][0].ToString().Trim().Equals("True"))
            {
                if (dataSet.Tables[0].Rows[0][1].ToString().Trim().Equals(password.Trim()))
                {
                    this.loginName = dataSet.Tables[0].Rows[0][2].ToString().Trim();
                    login = true;
                    return login;
                }
            }
            else
            {
                MessageBox.Show("您还没有注册登账户!");
            }

            return login;
        }
        #endregion

        #region 管理员  登录验证
        private bool admin_checkOut(string loginID, string password)
        {
            bool login = false;
            SqlConnection sqlconnection = new SqlConnection(conStr);

            string comStr = string.Format("select Sys_NAME, Sys_PASSWORD from dboSys where Sys_IID = \'{0}\'" ,loginID);

            dataSet = new DataSet();
            dataSet.Clear();
            dataSet = SqlCommondClass.GetDataSet(comStr, "dboSys", sqlconnection);

            if (dataSet.Tables[0].Rows.Count == 0)
            {
                login = false;
                return login;
            }
            if (dataSet.Tables[0].Rows[0][1].ToString().Trim().Equals(password.Trim()))
            {
                this.sysLogin = true;
                this.loginName = dataSet.Tables[0].Rows[0][0].ToString().Trim();
                login = true;
                return login;
            }

            return login;
        }
        #endregion

        #region 返回验证是否成功信息，作为主窗口的登录判断条件
        public bool CheckOutRight()
        {
            return this.canLogin;
        }
        #endregion

        #region 返回是否为管理员登录信息
        public bool SystemLogin()
        {
            return this.sysLogin;
        }
        #endregion

        #region 获取登录用户姓名信息
        public string getLoginName()
        {
            return this.loginName;
        }
        #endregion

        #region 获取登录用户ID信息
        public string getLoginID()
        {
            return this.loginID;
        }
        #endregion

        #region 点击  取消  按钮操作函数
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm RegisterFm = new RegisterForm();
            RegisterFm.ShowDialog();
        }

    }
  
}
