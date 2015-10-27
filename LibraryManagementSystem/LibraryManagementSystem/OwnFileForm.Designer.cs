namespace LibraryManagementSystem
{
    partial class OwnFileForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OwnFileForm));
            this.FileUserName = new System.Windows.Forms.Label();
            this.FileSex = new System.Windows.Forms.Label();
            this.FilePic = new System.Windows.Forms.Panel();
            this.FileClassification = new System.Windows.Forms.Label();
            this.FileDepartment = new System.Windows.Forms.Label();
            this.FileMajor = new System.Windows.Forms.Label();
            this.FileNum = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FilebtnChange = new System.Windows.Forms.Button();
            this.FileMsg = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.FileSex_Value = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FilebtnSave = new System.Windows.Forms.Button();
            this.FilebtnCancel = new System.Windows.Forms.Button();
            this.FileMajor_Value = new System.Windows.Forms.TextBox();
            this.FileDepartment_Value = new System.Windows.Forms.TextBox();
            this.FileClassification_Value = new System.Windows.Forms.TextBox();
            this.FileBirth_Value = new System.Windows.Forms.TextBox();
            this.FileNum_Value = new System.Windows.Forms.Label();
            this.FileUserName_Value = new System.Windows.Forms.Label();
            this.FileBirth = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileUserName
            // 
            this.FileUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileUserName.AutoSize = true;
            this.FileUserName.Location = new System.Drawing.Point(20, 27);
            this.FileUserName.Name = "FileUserName";
            this.FileUserName.Size = new System.Drawing.Size(41, 12);
            this.FileUserName.TabIndex = 0;
            this.FileUserName.Text = "姓名：";
            // 
            // FileSex
            // 
            this.FileSex.AutoSize = true;
            this.FileSex.Location = new System.Drawing.Point(205, 27);
            this.FileSex.Name = "FileSex";
            this.FileSex.Size = new System.Drawing.Size(41, 12);
            this.FileSex.TabIndex = 0;
            this.FileSex.Text = "性别：";
            // 
            // FilePic
            // 
            this.FilePic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.FilePic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("FilePic.BackgroundImage")));
            this.FilePic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FilePic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.FilePic.Location = new System.Drawing.Point(127, 106);
            this.FilePic.Name = "FilePic";
            this.FilePic.Size = new System.Drawing.Size(51, 50);
            this.FilePic.TabIndex = 1;
            this.FilePic.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FilePic_MouseDoubleClick);
            // 
            // FileClassification
            // 
            this.FileClassification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileClassification.AutoSize = true;
            this.FileClassification.Location = new System.Drawing.Point(21, 135);
            this.FileClassification.Name = "FileClassification";
            this.FileClassification.Size = new System.Drawing.Size(65, 12);
            this.FileClassification.TabIndex = 0;
            this.FileClassification.Text = "用户类别：";
            // 
            // FileDepartment
            // 
            this.FileDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileDepartment.AutoSize = true;
            this.FileDepartment.Location = new System.Drawing.Point(21, 166);
            this.FileDepartment.Name = "FileDepartment";
            this.FileDepartment.Size = new System.Drawing.Size(41, 12);
            this.FileDepartment.TabIndex = 0;
            this.FileDepartment.Text = "学院：";
            // 
            // FileMajor
            // 
            this.FileMajor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileMajor.AutoSize = true;
            this.FileMajor.Location = new System.Drawing.Point(21, 197);
            this.FileMajor.Name = "FileMajor";
            this.FileMajor.Size = new System.Drawing.Size(41, 12);
            this.FileMajor.TabIndex = 0;
            this.FileMajor.Text = "专业：";
            // 
            // FileNum
            // 
            this.FileNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileNum.AutoSize = true;
            this.FileNum.Location = new System.Drawing.Point(21, 73);
            this.FileNum.Name = "FileNum";
            this.FileNum.Size = new System.Drawing.Size(41, 12);
            this.FileNum.TabIndex = 0;
            this.FileNum.Text = "学号：";
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 131);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.FilebtnChange);
            this.splitContainer1.Panel1.Controls.Add(this.FileMsg);
            this.splitContainer1.Panel1.Controls.Add(this.FilePic);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.splitContainer1.Panel2.Controls.Add(this.monthCalendar1);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.FileSex_Value);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.FileMajor_Value);
            this.splitContainer1.Panel2.Controls.Add(this.FileDepartment_Value);
            this.splitContainer1.Panel2.Controls.Add(this.FileClassification_Value);
            this.splitContainer1.Panel2.Controls.Add(this.FileBirth_Value);
            this.splitContainer1.Panel2.Controls.Add(this.FileNum_Value);
            this.splitContainer1.Panel2.Controls.Add(this.FileUserName_Value);
            this.splitContainer1.Panel2.Controls.Add(this.FileBirth);
            this.splitContainer1.Panel2.Controls.Add(this.FileSex);
            this.splitContainer1.Panel2.Controls.Add(this.FileNum);
            this.splitContainer1.Panel2.Controls.Add(this.FileUserName);
            this.splitContainer1.Panel2.Controls.Add(this.FileMajor);
            this.splitContainer1.Panel2.Controls.Add(this.FileClassification);
            this.splitContainer1.Panel2.Controls.Add(this.FileDepartment);
            this.splitContainer1.Panel2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(912, 402);
            this.splitContainer1.SplitterDistance = 301;
            this.splitContainer1.TabIndex = 3;
            // 
            // FilebtnChange
            // 
            this.FilebtnChange.Location = new System.Drawing.Point(76, 166);
            this.FilebtnChange.Name = "FilebtnChange";
            this.FilebtnChange.Size = new System.Drawing.Size(70, 23);
            this.FilebtnChange.TabIndex = 2;
            this.FilebtnChange.Text = "修改资料";
            this.FilebtnChange.UseVisualStyleBackColor = true;
            this.FilebtnChange.Click += new System.EventHandler(this.FilebtnChange_Click);
            // 
            // FileMsg
            // 
            this.FileMsg.AutoSize = true;
            this.FileMsg.Location = new System.Drawing.Point(18, 89);
            this.FileMsg.Name = "FileMsg";
            this.FileMsg.Size = new System.Drawing.Size(53, 12);
            this.FileMsg.TabIndex = 0;
            this.FileMsg.Text = "个人资料";
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(232, 94);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 6;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // FileSex_Value
            // 
            this.FileSex_Value.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FileSex_Value.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileSex_Value.FormattingEnabled = true;
            this.FileSex_Value.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.FileSex_Value.Items.AddRange(new object[] {
            "男",
            "女"});
            this.FileSex_Value.Location = new System.Drawing.Point(253, 24);
            this.FileSex_Value.Name = "FileSex_Value";
            this.FileSex_Value.Size = new System.Drawing.Size(51, 20);
            this.FileSex_Value.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.FilebtnSave);
            this.panel1.Controls.Add(this.FilebtnCancel);
            this.panel1.Location = new System.Drawing.Point(109, 327);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 23);
            this.panel1.TabIndex = 4;
            // 
            // FilebtnSave
            // 
            this.FilebtnSave.Enabled = false;
            this.FilebtnSave.Location = new System.Drawing.Point(-1, 0);
            this.FilebtnSave.Name = "FilebtnSave";
            this.FilebtnSave.Size = new System.Drawing.Size(60, 23);
            this.FilebtnSave.TabIndex = 0;
            this.FilebtnSave.Text = "保存";
            this.FilebtnSave.UseVisualStyleBackColor = true;
            this.FilebtnSave.Click += new System.EventHandler(this.FilebtnSave_Click);
            // 
            // FilebtnCancel
            // 
            this.FilebtnCancel.Enabled = false;
            this.FilebtnCancel.Location = new System.Drawing.Point(135, 0);
            this.FilebtnCancel.Name = "FilebtnCancel";
            this.FilebtnCancel.Size = new System.Drawing.Size(60, 23);
            this.FilebtnCancel.TabIndex = 0;
            this.FilebtnCancel.Text = "取消";
            this.FilebtnCancel.UseVisualStyleBackColor = true;
            this.FilebtnCancel.Click += new System.EventHandler(this.FilebtnCancel_Click);
            // 
            // FileMajor_Value
            // 
            this.FileMajor_Value.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FileMajor_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileMajor_Value.Location = new System.Drawing.Point(92, 194);
            this.FileMajor_Value.Name = "FileMajor_Value";
            this.FileMajor_Value.Size = new System.Drawing.Size(104, 14);
            this.FileMajor_Value.TabIndex = 3;
            // 
            // FileDepartment_Value
            // 
            this.FileDepartment_Value.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FileDepartment_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileDepartment_Value.Location = new System.Drawing.Point(92, 162);
            this.FileDepartment_Value.Name = "FileDepartment_Value";
            this.FileDepartment_Value.Size = new System.Drawing.Size(104, 14);
            this.FileDepartment_Value.TabIndex = 3;
            // 
            // FileClassification_Value
            // 
            this.FileClassification_Value.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FileClassification_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileClassification_Value.Location = new System.Drawing.Point(92, 135);
            this.FileClassification_Value.Name = "FileClassification_Value";
            this.FileClassification_Value.Size = new System.Drawing.Size(76, 14);
            this.FileClassification_Value.TabIndex = 3;
            // 
            // FileBirth_Value
            // 
            this.FileBirth_Value.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FileBirth_Value.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileBirth_Value.Location = new System.Drawing.Point(253, 64);
            this.FileBirth_Value.Name = "FileBirth_Value";
            this.FileBirth_Value.Size = new System.Drawing.Size(102, 14);
            this.FileBirth_Value.TabIndex = 3;
            this.FileBirth_Value.TextChanged += new System.EventHandler(this.FileBirth_Value_TextChanged);
            // 
            // FileNum_Value
            // 
            this.FileNum_Value.AutoSize = true;
            this.FileNum_Value.Location = new System.Drawing.Point(68, 73);
            this.FileNum_Value.Name = "FileNum_Value";
            this.FileNum_Value.Size = new System.Drawing.Size(41, 12);
            this.FileNum_Value.TabIndex = 2;
            this.FileNum_Value.Text = "label1";
            // 
            // FileUserName_Value
            // 
            this.FileUserName_Value.AutoSize = true;
            this.FileUserName_Value.Location = new System.Drawing.Point(67, 27);
            this.FileUserName_Value.Name = "FileUserName_Value";
            this.FileUserName_Value.Size = new System.Drawing.Size(41, 12);
            this.FileUserName_Value.TabIndex = 2;
            this.FileUserName_Value.Text = "label1";
            // 
            // FileBirth
            // 
            this.FileBirth.AutoSize = true;
            this.FileBirth.Location = new System.Drawing.Point(205, 73);
            this.FileBirth.Name = "FileBirth";
            this.FileBirth.Size = new System.Drawing.Size(41, 12);
            this.FileBirth.TabIndex = 1;
            this.FileBirth.Text = "生日：";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.8806F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.1194F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(918, 536);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(912, 122);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(421, 238);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(189, 140);
            this.panel3.TabIndex = 7;
            // 
            // OwnFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(918, 536);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OwnFileForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "个人资料";
            this.SizeChanged += new System.EventHandler(this.OwnFileForm_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label FileUserName;
        private System.Windows.Forms.Label FileSex;
        private System.Windows.Forms.Panel FilePic;
        private System.Windows.Forms.Label FileClassification;
        private System.Windows.Forms.Label FileDepartment;
        private System.Windows.Forms.Label FileMajor;
        private System.Windows.Forms.Label FileNum;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label FileMsg;
        private System.Windows.Forms.Label FileBirth;
        private System.Windows.Forms.TextBox FileMajor_Value;
        private System.Windows.Forms.TextBox FileDepartment_Value;
        private System.Windows.Forms.TextBox FileClassification_Value;
        private System.Windows.Forms.TextBox FileBirth_Value;
        private System.Windows.Forms.Label FileNum_Value;
        private System.Windows.Forms.Label FileUserName_Value;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button FilebtnSave;
        private System.Windows.Forms.Button FilebtnCancel;
        private System.Windows.Forms.Button FilebtnChange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox FileSex_Value;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel panel3;
    }
}