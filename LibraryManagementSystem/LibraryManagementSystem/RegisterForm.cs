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
    public partial class RegisterForm : Form
    {
        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            bool finished = true;

            if (this.textBoxId.Text == "" || this.textBoxName.Text == "" || this.textBoxPassword1.Text == "")
            {
                MessageBox.Show("请输入完整信息！");
                return;
            }

            if (this.textBoxPassword1.Text.Trim() != this.textBoxPassword2.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致，请重新输入！");
                return;
            }
            else
            {
                string registerID = this.textBoxId.Text.Trim();
                string registerPassword = coding(this.textBoxPassword1.Text.Trim());
                string comStr = string.Format("update dboStudent set Student_PASSWORD = \'{0}\' where Student_ID = \'{1}\'", registerPassword, registerID);
                    
                SqlConnection sqlconnection = new SqlConnection(conStr);
                try
                {
                    sqlconnection.Open();
                    SqlCommand command = new SqlCommand(comStr, sqlconnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                { 
                    finished = false;
                    MessageBox.Show("注册失败");
                }
                finally
                {
                    sqlconnection.Close();
                }
                if (finished)
                {
                    MessageBox.Show("注册成功,请登录");
                    this.Close();
                }
            }
            
        }

        private string coding(string password)
        {
            return password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBoxId.Text = "";
            this.textBoxId.Focus();
            this.textBoxName.Text = "";
            this.textBoxPassword1.Text = "";
            this.textBoxPassword2.Text = "";
        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            string registerId = this.textBoxId.Text.Trim();
            string registerName=this.textBoxName.Text.Trim();
            string comStr = string.Format("select Student_SEX, Student_BIRTHDAY, Student_DEPT, Student_MAJOR from dboStudent where Student_IID = \'{0}\' and Student_NAME = \'{1}\'",registerId,registerName);
            SqlConnection sqlconnection = new SqlConnection(conStr);
            try
            {
                sqlconnection.Open();
                DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "dboStudent", sqlconnection);
                if (dataSet.Tables[0].Rows.Count >= 1)
                {
                    this.textBoxSex.Text = dataSet.Tables[0].Rows[0]["Student_SEX"].ToString().Trim();
                    this.textBoxBirth.Text = dataSet.Tables[0].Rows[0]["Student_BIRTHDAY"].ToString().Substring(0, dataSet.Tables[0].Rows[0]["Student_BIRTHDAY"].ToString().IndexOf(" "));
                    this.textBoxDepart.Text = dataSet.Tables[0].Rows[0]["Student_DEPT"].ToString().Trim();
                    this.textBoxMajor.Text = dataSet.Tables[0].Rows[0]["Student_MAJOR"].ToString().Trim();
                }
            }
            catch (Exception ) { }
            finally
            {
                sqlconnection.Close();
            }
        }
    }
}
