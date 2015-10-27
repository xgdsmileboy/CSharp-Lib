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

    public partial class NewBookForm : Form
    {
        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";
        
        public NewBookForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool finished = true;

            if (this.textBoxISBN.Text.Trim() == "" || this.textBoxBookName.Text.Trim() == "" || this.textBoxPrice.Text.Trim()==""||
               this.textBoxMainAuthor.Text.Trim() == "" || this.textBoxPublisher.Text.Trim() == "" ||
               this.textBoxClass.Text.Trim() == "" || this.textBoxIndex.Text.Trim() == "" ||
               this.textBoxNum.Text.Trim() == "" || this.textBoxIntroduction.Text.Trim() == "")
            {
                MessageBox.Show("请完整填写图书信息！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            SqlConnection sqlconnection = new SqlConnection(conStr);

            try
            {

                string comStr = string.Format("insert into dboBook(Book_ISBN,Book_NAME,Book_MAINAUTHOR,Book_AUTHOR1,"
                           + " Book_AUTHOR2,Book_AUTHOR3,Book_SEARCHID,Book_CLASS,Book_PRICE,Book_PUBLISHER,Book_INTRODUCTION,"
                           + " Book_TOTAL,Book_REMAIN) values(\'{0}\',\'{1}\',\'{2}\',\'{3}\',\'{4}\',\'{5}\',\'{6}\',\'{7}\',{8},"
                           + " \'{9}\',\'{10}\',{11},{12})", textBoxISBN.Text.Trim(), textBoxBookName.Text.Trim(), textBoxMainAuthor.Text.Trim(),
                           textBoxAuthor2.Text.Trim(), textBoxAuthor3.Text.Trim(), textBoxAuthor4.Text.Trim(), textBoxIndex.Text.Trim(), textBoxClass.Text.Trim(),
                           float.Parse(textBoxPrice.Text.Trim()), textBoxPublisher.Text.Trim(), textBoxIntroduction.Text.Trim(), Int32.Parse(textBoxNum.Text.Trim()), Int32.Parse(textBoxNum.Text.Trim()));

                sqlconnection.Open();
                SqlCommand command = new SqlCommand(comStr, sqlconnection);
                command.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                finished = false;
                MessageBox.Show("录入信息失败！请重新录入");
            }
            finally
            {
                sqlconnection.Close();
            }
            if (finished)
            {
                MessageBox.Show("录入书籍信息成功");
            }
        }
        

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.textBoxISBN.Text = "";
            this.textBoxBookName.Text = "";
            this.textBoxMainAuthor.Text = "";
            this.textBoxPublisher.Text = "";
            this.textBoxClass.Text = "";
            this.textBoxIndex.Text = "";
            this.textBoxAuthor2.Text = "";
            this.textBoxAuthor3.Text = "";
            this.textBoxAuthor4.Text = "";
            this.textBoxNum.Text = "";
            this.textBoxPrice.Text = "";
            this.textBoxIntroduction.Text = "";
        }

        
    }
}
