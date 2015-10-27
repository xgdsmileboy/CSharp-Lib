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
    public partial class HomeForm : Form
    {
        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";

        public HomeForm()
        {
            InitializeComponent();
            InitMyOwnForm();
        }

        private void InitMyOwnForm()
        {
            this.AcceptButton = this.butSearch;
            this.Focus();
            this.radioButton1.Select();
            this.textBox1.Focus();
            this.textBox1.SelectAll();
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            string textStr = this.textBox1.Text.Trim();
            string searchStr = "'";

            if (textStr.Equals("")) { return; }

            for (int Strlen = 0; Strlen < textStr.Length; Strlen++)
            {
                searchStr += "%" + textStr.ElementAt(Strlen);
            }
            searchStr += "%'";

            SqlConnection sqlconnection = new SqlConnection(conStr);
            string comStr;

            if (this.radioButton1.Checked) //按照书名检索
            {
                comStr = "select Book_NAME 图书名,Book_MAINAUTHOR 主编,Book_SEARCHID 检索号,Book_CLASS 图书类别,Book_PUBLISHER 出版社,Book_INTRODUCTION 图书简介,Book_REMAIN 剩余量"
                                   + " from dboBook where Book_NAME Like " + searchStr;
            }
            else  //按照作者名进行查找
            {
                comStr = "select Book_NAME 图书名,Book_MAINAUTHOR 主编,Book_SEARCHID 检索号,Book_CLASS 图书类别,Book_PUBLISHER 出版社,Book_INTRODUCTION 图书简介,Book_REMAIN 剩余量"
                                   + " from dboBook where Book_MAINAUTHOR Like " + searchStr
                                   + " OR Book_AUTHOR1 Like " + searchStr
                                   + " OR Book_AUTHOR2 Like " + searchStr
                                   + " Or Book_AUTHOR3 Like " + searchStr;
            }
            DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "dboBook", sqlconnection);
            this.dataGridView1.DataSource = dataSet.Tables[0];
        }

        #region 访问我的网站
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.baidu.com");
        }
        #endregion
    }
}
