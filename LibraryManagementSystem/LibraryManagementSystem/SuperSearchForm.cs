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
    public partial class SuperSearchForm : Form
    {
        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";
        private DataSet dataSet;

        #region 构造函数
        public SuperSearchForm()
        {
            InitializeComponent();
            dataSet = new DataSet();
        }
        #endregion

        #region 高级搜索点击搜索按钮触发
        private void SuperbtnSearch_Click(object sender, EventArgs e)
        {
            string comStr = "select Book_NAME 图书名,Book_MAINAUTHOR 主编,Book_SEARCHID 检索号,Book_CLASS 图书类别,Book_PUBLISHER 出版社,Book_INTRODUCTION 图书简介,Book_REMAIN 剩余量"
                                   + " from dboBook where ";

            string bookName = this.textBookName.Text.Trim();
            string authorName = this.textAuthorName.Text.Trim();
            string subjectName = this.textSubjectName.Text.Trim();
            string publishName = this.textPublishName.Text.Trim();
            string isbnName = this.textISBNName.Text.Trim();

            if (bookName.Length > 1) {
                comStr += " Book_NAME = '" + bookName + "'";
            }

            if (authorName.Length > 1){
                comStr += "and Book_MAINAUTHOR = '" + authorName + "'";
            }

            if (subjectName.Length > 1){
                comStr += "and Book_CLASS = '" + subjectName + "'";
            }

            if (publishName.Length > 1){
                comStr += "and Book_PUBLISHER = '" + publishName + "'";
            }

            if (isbnName.Length > 1){
                comStr += "and Book_ISBN = '" + isbnName + "'";
            }

            SqlConnection sqlconnection = new SqlConnection(conStr);

            dataSet.Clear();
            dataSet = SqlCommondClass.GetDataSet(comStr, "dboBook", sqlconnection);

            if (dataSet.Tables[0].Rows.Count < 20)
            {
                for (int i = dataSet.Tables[0].Rows.Count; i<20; i++)
                {
                    dataSet.Tables[0].Rows.Add();
                }
            }
            this.dataGridView1.DataSource = dataSet.Tables[0];
        }
        #endregion

        #region 在高级搜索中点击重置按钮触发
        private void SuperbtnReset_Click(object sender, EventArgs e)
        {
            this.textBookName.Text = "";
            this.textAuthorName.Text = "";
            this.textSubjectName.Text = "";
            this.textPublishName.Text = "";
            this.textISBNName.Text = "";
        }
        #endregion
    }
}
