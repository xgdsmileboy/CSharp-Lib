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
    public partial class OwnFileForm : Form
    {

        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";

        private string name;
        private string sex;
        private string ID;
        private string birthday;
        private string userClassification;
        private string depart;
        private string major;
        private bool systemLogin;


        #region 构造函数，用户的个人资料窗口
        public OwnFileForm(string loginID, bool isSys)
        {
            InitializeComponent();
            InitMyOwnFrame();

            name = "";
            sex = "";
            ID = loginID;
            birthday = "";
            userClassification = "";
            depart = "";
            major = "";
            this.systemLogin = isSys;

            if (isSys)
            {
                getSystemInfo(loginID);
            }
            else
            {
                getUserInfo(loginID);
            }

            showMyInfo();
        }
        #endregion

        public void getUserInfo(string loginID)
        {
            if (loginID.Equals("")) { return; }

            SqlConnection sqlconnection = new SqlConnection(conStr);
            string comStr;

            comStr = "select Student_NAME, Student_SEX, Student_BIRTHDAY, Student_TYPE,Student_DEPT, Student_MAJOR from dboStudent where Student_IID = " + loginID;
             
            DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "dboStudent", sqlconnection);

            name = dataSet.Tables[0].Rows[0][0].ToString().Trim();
            sex = dataSet.Tables[0].Rows[0][1].ToString().Trim();
            ID = loginID;
            birthday = dataSet.Tables[0].Rows[0][2].ToString().Trim();
            birthday =  birthday.Substring(0, 10);
            userClassification = dataSet.Tables[0].Rows[0][3].ToString().Trim();
            depart = dataSet.Tables[0].Rows[0][4].ToString().Trim();
            major = dataSet.Tables[0].Rows[0][5].ToString().Trim();

        }

        public void getSystemInfo(string loginID)
        {
            if (loginID.Equals("")) { return; }

            SqlConnection sqlconnection = new SqlConnection(conStr);
            string comStr;

            comStr = "select Sys_NAME, Sys_SEX, Sys_BIRTHDAY from dboSys where Sys_IID = " + loginID;

            DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "dboStudent", sqlconnection);

            name = dataSet.Tables[0].Rows[0]["Sys_NAME"].ToString().Trim();
            sex = dataSet.Tables[0].Rows[0]["Sys_SEX"].ToString().Trim();
            ID = loginID;
            birthday = dataSet.Tables[0].Rows[0]["Sys_BIRTHDAY"].ToString().Trim();
            birthday = birthday.Substring(0,8);
            userClassification = "管理员";
            depart = "";
            major = "";
        }

        public void showMyInfo()
        {
            this.FileUserName_Value.Text = name;
            this.FileSex_Value.Text = sex;
            this.FileNum_Value.Text = ID;
            this.FileBirth_Value.Text = birthday;
            this.FileClassification_Value.Text = userClassification;
            this.FileDepartment_Value.Text = depart;
            this.FileMajor_Value.Text = major;
        }

        #region 初始化自定义窗口组件
        private void InitMyOwnFrame()
        {
            this.splitContainer1.Dock = DockStyle.Fill;
            updateControlPosition();

            //姓名和学号,学院和专业不可更改
            this.FileUserName_Value.Enabled = false;
            this.FileNum_Value.Enabled = false;
            this.FileClassification_Value.Enabled = false;
            this.FileDepartment_Value.Enabled = false;
            this.FileMajor_Value.Enabled = false;

            //以下可更改，初始值为不可更改
            this.FileSex_Value.Enabled = false;
            this.FileBirth_Value.Enabled = false;

            this.monthCalendar1.Visible = false;
        }
        #endregion

        #region 更新页面上控件的位置信息
        private void updateControlPosition()
        {
            int FormX = this.Location.X;
            int space = 40;
            //用户图片
            this.FilePic.Location = new Point(FormX + this.Width / 20, 20);
            //用户图片大小
            this.FilePic.Size = new System.Drawing.Size(this.Width/7, this.Width/7);
            //"个人资料" lable
            this.FileMsg.Location = new Point(FormX + this.Width / 20, 30 + this.Width / 7);
            //"个人资料"Lable大小修改
            this.FileMsg.Font = new Font("宋体", (float)FilePic.Width/8, this.FileMsg.Font.Style);
            //修改个人资料
            this.FilebtnChange.Location = new Point(FormX + this.Width / 20, 30 + this.Width / 5);

            //姓名
            this.FileUserName.Location = new Point(FormX + this.Width / 10, 20);
            this.FileUserName_Value.Location = new Point(FormX + this.Width / 10 + space, 20);
            //性别
            this.FileSex.Location = new Point(FormX + this.Width / 3, 20);
            this.FileSex_Value.Location = new Point(FormX + this.Width / 3 + space, 20);
            //学号
            this.FileNum.Location = new Point(FormX + this.Width / 10, 50);
            this.FileNum_Value.Location = new Point(FormX + this.Width / 10 + space, 50);
            //生日
            this.FileBirth.Location = new Point(FormX + this.Width / 3, 50);
            this.FileBirth_Value.Location = new Point(FormX + this.Width / 3 + space, 50);
            //用户类别
            this.FileClassification.Location = new Point(FormX + this.Width / 10, 110);
            this.FileClassification_Value.Location = new Point(FormX + this.Width / 10 + space + 20, 110);
            //学院
            this.FileDepartment.Location = new Point(FormX + this.Width / 10, 140);
            this.FileDepartment_Value.Location = new Point(FormX + this.Width / 10 + space, 140);
            //专业
            this.FileMajor.Location = new Point(FormX + this.Width / 10, 170);
            this.FileMajor_Value.Location = new Point(FormX + this.Width / 10 + space, 170);
        }
        #endregion

        #region 页面大小变化触发
        private void OwnFileForm_SizeChanged(object sender, EventArgs e)
        {
            updateControlPosition();
        }
        #endregion

        #region 修改个人资料按钮触发
        private void FilebtnChange_Click(object sender, EventArgs e)
        {
            this.FileSex_Value.DropDownStyle = ComboBoxStyle.DropDownList;
            if (sex.Equals("男"))
            {
                this.FileSex_Value.SelectedIndex = 0;
            }
            else
            {
                this.FileSex_Value.SelectedIndex = 1;
            }
            this.FileBirth_Value.BorderStyle = BorderStyle.Fixed3D;

            this.FileSex_Value.Enabled = true;
            this.FileBirth_Value.Enabled = true;

            this.FileSex_Value.Focus();
            this.FileSex_Value.SelectAll();

            this.FilebtnSave.Enabled = true;
            this.FilebtnSave.Enabled = true;
            this.FilebtnCancel.Enabled = true;
            this.FilebtnCancel.Enabled = true;
        }
        #endregion

        #region 双击照片后触发   ----------未完成
        private void FilePic_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        private void FileBirth_Value_TextChanged(object sender, EventArgs e)
        {
            if (this.FileBirth_Value.Focused)
            {
                this.monthCalendar1.Visible = true;
                this.monthCalendar1.Focus();
            }
            else
            {
                this.monthCalendar1.Visible = false;
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.FileBirth_Value.Text = this.monthCalendar1.SelectionStart.ToString("yyyy/MM/dd");
        }

        private void FilebtnSave_Click(object sender, EventArgs e)
        {
            bool finished = true;
            string ssex = this.FileSex_Value.Text.Trim();
            string birthday = this.FileBirth_Value.Text.Trim();

            SqlConnection sqlconnection = new SqlConnection(conStr);
            string comStr;

            if (systemLogin)
            {
                comStr = string.Format("update dboSys set Sys_SEX = \'{0}\',Sys_BIRTHDAY = \'{1}\' where Sys_IID = \'{2}\'", ssex, birthday, ID);
            }
            else
            {
                comStr = string.Format("update dboStudent set Student_SEX = \'{0}\',Student_BIRTHDAY = \'{1}\' where Student_IID = \'{2}\'", ssex, birthday, ID);
            }
            try
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand(comStr, sqlconnection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                finished = false;
                MessageBox.Show("操作数据库出错！", "操作演示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }
            if (finished)
            {
                MessageBox.Show("修改成功");
                this.FileBirth_Value.Enabled = false;
                this.FileSex_Value.Enabled = false;
                this.FilebtnSave.Enabled = false;
                this.FilebtnCancel.Enabled = false;
            }
            else
            {
                MessageBox.Show("请重新设置");
            }
            

           /* DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "dboStudent", sqlconnection);*/

//             name = dataSet.Tables[0].Rows[0][0].ToString().Trim();
//             sex = dataSet.Tables[0].Rows[0][1].ToString().Trim();
          /*  ID = loginID;*/
//             birthday = dataSet.Tables[0].Rows[0][2].ToString().Trim();
//             birthday = birthday.Substring(0, birthday.IndexOf(" "));
//             userClassification = "学生";
//             depart = dataSet.Tables[0].Rows[0][3].ToString().Trim();
//             major = dataSet.Tables[0].Rows[0][4].ToString().Trim();

        }

        private void FilebtnCancel_Click(object sender, EventArgs e)
        {
            this.FileSex_Value.Text = sex;
            this.FileBirth_Value.Text = birthday;
            this.FileSex_Value.Enabled = false;
            this.FileBirth_Value.Enabled = false;
            this.FilebtnCancel.Enabled = false;
            this.FilebtnSave.Enabled = false;
            this.monthCalendar1.Visible = false;
        }
    }
}
