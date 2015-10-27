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
    
    public partial class NewAdminForm : Form
    {

        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";
        
        public NewAdminForm()
        {
            InitializeComponent();
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            bool finished = true;

            string adminID = this.textBoxID.Text.Trim();
            string adminName = this.textBoxName.Text.Trim();
            string adminSex = this.textBoxSex.Text.Trim();
            string adminBirth = this.textBoxBirth.Text.Substring(0, 10).Trim();

            if (adminID == "" || adminName == "" || adminSex == "" || adminBirth == "")
            {
                MessageBox.Show("请输入完整信息","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            if (this.textBoxPassword1.Text.Trim() != this.textBoxPassword2.Text.Trim())
            {
                MessageBox.Show("两次输入密码不一致","错误信息",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.textBoxPassword1.Text = "";
                this.textBoxPassword2.Text = "";
                return;
            }
            string adminPassword = this.textBoxPassword1.Text.Trim();
            string comStr = string.Format("select * from dboSys where Sys_IID = \'{0}\'", adminID);
            SqlConnection sqlconnection=new SqlConnection(conStr);
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = SqlCommondClass.GetDataSet(comStr, "result", sqlconnection);
            }
            catch (System.Exception)
            {
                finished = false;
                MessageBox.Show("打开数据库失败", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                sqlconnection.Close();
            }

            if (dataSet.Tables[0].Rows.Count >= 1)
            {
                MessageBox.Show("该管理员编号已经存在，可以直接登录", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                comStr = string.Format("insert into dboSys(Sys_IID,Sys_NAME,Sys_SEX,Sys_BIRTHDAY,Sys_PASSWORD) values(\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\')",
                                                           adminID,adminName,adminSex,adminBirth, coding(adminPassword));
                MessageBox.Show(comStr);
                try
                {
                    sqlconnection.Open();
                    SqlCommand command = new SqlCommand(comStr, sqlconnection);
                    MessageBox.Show(command.ToString());
                    command.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    finished = false;
                    MessageBox.Show("创建管理员失败", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    sqlconnection.Close();
                }
                if (finished)
                {
                    MessageBox.Show("创建管理员成功");
                }

            }

        }

        private string coding(string password)
        {
            return password;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.textBoxID.Text = "";
            this.textBoxName.Text = "";
            this.textBoxSex.Text = "";
            this.textBoxBirth.Text = "";
            this.textBoxPassword1.Text = "";
            this.textBoxPassword2.Text = "";
        }
    }
}
