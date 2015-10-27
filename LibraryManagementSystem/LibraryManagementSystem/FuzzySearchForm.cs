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
    public partial class FuzzySearchForm : Form
    {
        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";

        public FuzzySearchForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
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

            comStr = "select Book_NAME 图书名,Book_MAINAUTHOR 主编,Book_SEARCHID 检索号,Book_CLASS 图书类别,Book_PUBLISHER 出版社,Book_INTRODUCTION 图书简介,Book_REMAIN 剩余量"
                                + " from dboBook where Book_MAINAUTHOR Like " + searchStr
                                + " OR Book_AUTHOR1 Like " + searchStr
                                + " OR Book_AUTHOR2 Like " + searchStr
                                + " OR Book_AUTHOR3 Like " + searchStr
                                + " OR Book_SEARCHID Like " + searchStr
                                + " OR Book_CLASS Like " + searchStr
                                + " OR Book_PUBLISHER Like " + searchStr
                                + " OR Book_INTRODUCTION Like " + searchStr;


            DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "dboBook", sqlconnection);
            this.dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.Rows[e.RowIndex].ToString());
        }
    }
}
