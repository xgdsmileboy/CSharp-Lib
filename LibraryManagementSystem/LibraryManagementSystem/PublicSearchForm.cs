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
    public partial class PublicSearchForm : Form
    {

        private const string conStr = @"server=127.0.0.1;database=LibraryManagementSystemDataBase;uid=jiangjiajun;pwd=smileboy";
        
        public PublicSearchForm()
        {
            InitializeComponent();
        }

        public string lable1
        {
            get{ return label1.Text; }
            set { label1.Text = value;  }
        }

    }
}
