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
    public partial class DeleteBookForm : Form
    {
        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";
        private bool existBook;

        public DeleteBookForm()
        {
            InitializeComponent();
            existBook = false;
        }

        private void textBoxISBN_TextChanged(object sender, EventArgs e)
        {
            string ISBN = this.textBoxISBN.Text.Trim();

            string comStr = string.Format("select Book_NAME, Book_MAINAUTHOR, Book_AUTHOR1,Book_AUTHOR2,Book_AUTHOR2,Book_SEARCHID, Book_CLASS,"
                                         + "Book_PRICE,Book_PUBLISHER,Book_TOTAL,Book_REMAIN,Book_INTRODUCTION from dboBook "
                                         + "where Book_ISBN = \'{0}\'", ISBN);
            SqlConnection sqlconnection = new SqlConnection(conStr);
            try
            {
                sqlconnection.Open();
                DataSet dataSet = SqlCommondClass.GetDataSet(comStr, "result", sqlconnection);
                if (dataSet.Tables[0].Rows.Count >= 1)
                {
                    existBook = true;
                    this.textBoxBookName.Text = dataSet.Tables[0].Rows[0]["Book_NAME"].ToString();
                    this.textBoxMainAuthor.Text = dataSet.Tables[0].Rows[0]["Book_MAINAUTHOR"].ToString();
                    this.textBoxOtherAuthor.Text = dataSet.Tables[0].Rows[0]["Book_AUTHOR1"].ToString() + " "
                                                 + dataSet.Tables[0].Rows[0]["Book_AUTHOR2"].ToString() + " "
                                                 + dataSet.Tables[0].Rows[0]["Book_AUTHOR2"].ToString();
                    this.textBoxSearchId.Text = dataSet.Tables[0].Rows[0]["Book_SEARCHID"].ToString();
                    this.textBoxBookClass.Text = dataSet.Tables[0].Rows[0]["Book_CLASS"].ToString();
                    this.textBoxPrice.Text = dataSet.Tables[0].Rows[0]["Book_PRICE"].ToString();
                    this.textBoxPublisher.Text = dataSet.Tables[0].Rows[0]["Book_PUBLISHER"].ToString();
                    this.textBoxTotal.Text = dataSet.Tables[0].Rows[0]["Book_TOTAL"].ToString();
                    this.textBoxRemain.Text = dataSet.Tables[0].Rows[0]["Book_REMAIN"].ToString();
                    this.textBoxBriefInfo.Text = dataSet.Tables[0].Rows[0]["Book_INTRODUCTION"].ToString();
                }
                else
                {
                    existBook = false;
                    this.textBoxBookName.Text = "";
                    this.textBoxMainAuthor.Text = "";
                    this.textBoxOtherAuthor.Text = "";
                    this.textBoxSearchId.Text = "";
                    this.textBoxBookClass.Text = "";
                    this.textBoxPrice.Text = "";
                    this.textBoxPublisher.Text = "";
                    this.textBoxTotal.Text = "";
                    this.textBoxRemain.Text = "";
                    this.textBoxBriefInfo.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据库操作失败!", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("确定删除该图书？","警告信息",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
            MessageBox.Show(re.ToString());
            if (re == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            MessageBox.Show(existBook.ToString());
            if (existBook)
            {
                int count = 0;
                SqlConnection sqlconnection = new SqlConnection(conStr);
                string ISBN = this.textBoxISBN.Text.Trim();
                
                try
                {
                    string comStr = "up_delete_book";
                    SqlParameter param = new SqlParameter("@bookISBN", ISBN);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlconnection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(param);
                    cmd.CommandText = comStr;
                    sqlconnection.Open();
                    count = (int)cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("数据库操作失败","错误信息", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                finally
                {
                    sqlconnection.Close();
                }
                if (count > 0)
                {
                    MessageBox.Show("删除成功!");
                }
                else
                {
                    MessageBox.Show("删除失败!");
                }
                
            }
            else
            {
                MessageBox.Show("图书馆中没有此书记录","错误信息",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
