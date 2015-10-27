namespace LibraryManagementSystem
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.loginLable = new System.Windows.Forms.Label();
            this.textLogin = new System.Windows.Forms.TextBox();
            this.passwordLable = new System.Windows.Forms.Label();
            this.textPassworld = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.systemLable2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbtnAdmin = new System.Windows.Forms.RadioButton();
            this.rbtnUser = new System.Windows.Forms.RadioButton();
            this.systemLable1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginLable
            // 
            this.loginLable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginLable.AutoSize = true;
            this.loginLable.BackColor = System.Drawing.Color.Transparent;
            this.loginLable.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginLable.Location = new System.Drawing.Point(93, 96);
            this.loginLable.Name = "loginLable";
            this.loginLable.Size = new System.Drawing.Size(67, 15);
            this.loginLable.TabIndex = 9;
            this.loginLable.Text = "登录名：";
            // 
            // textLogin
            // 
            this.textLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLogin.Location = new System.Drawing.Point(157, 93);
            this.textLogin.Name = "textLogin";
            this.textLogin.Size = new System.Drawing.Size(156, 21);
            this.textLogin.TabIndex = 0;
            // 
            // passwordLable
            // 
            this.passwordLable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordLable.AutoSize = true;
            this.passwordLable.BackColor = System.Drawing.Color.Transparent;
            this.passwordLable.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordLable.Location = new System.Drawing.Point(93, 132);
            this.passwordLable.Name = "passwordLable";
            this.passwordLable.Size = new System.Drawing.Size(68, 15);
            this.passwordLable.TabIndex = 10;
            this.passwordLable.Text = "密  码：";
            // 
            // textPassworld
            // 
            this.textPassworld.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textPassworld.Location = new System.Drawing.Point(157, 129);
            this.textPassworld.Name = "textPassworld";
            this.textPassworld.PasswordChar = '*';
            this.textPassworld.Size = new System.Drawing.Size(156, 21);
            this.textPassworld.TabIndex = 1;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(82, 177);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(69, 23);
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "用户注册";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(157, 177);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(75, 23);
            this.btnlogin.TabIndex = 2;
            this.btnlogin.Text = "登录";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // systemLable2
            // 
            this.systemLable2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.systemLable2.AutoSize = true;
            this.systemLable2.BackColor = System.Drawing.Color.Transparent;
            this.systemLable2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.systemLable2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.systemLable2.Location = new System.Drawing.Point(116, 22);
            this.systemLable2.Name = "systemLable2";
            this.systemLable2.Size = new System.Drawing.Size(110, 24);
            this.systemLable2.TabIndex = 8;
            this.systemLable2.Text = "管理系统";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.Location = new System.Drawing.Point(238, 177);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbtnAdmin
            // 
            this.rbtnAdmin.AutoSize = true;
            this.rbtnAdmin.BackColor = System.Drawing.Color.Transparent;
            this.rbtnAdmin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnAdmin.Location = new System.Drawing.Point(235, 67);
            this.rbtnAdmin.Name = "rbtnAdmin";
            this.rbtnAdmin.Size = new System.Drawing.Size(62, 16);
            this.rbtnAdmin.TabIndex = 6;
            this.rbtnAdmin.Text = "管理员";
            this.rbtnAdmin.UseVisualStyleBackColor = false;
            // 
            // rbtnUser
            // 
            this.rbtnUser.AutoSize = true;
            this.rbtnUser.BackColor = System.Drawing.Color.Transparent;
            this.rbtnUser.Checked = true;
            this.rbtnUser.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbtnUser.Location = new System.Drawing.Point(166, 66);
            this.rbtnUser.Name = "rbtnUser";
            this.rbtnUser.Size = new System.Drawing.Size(49, 16);
            this.rbtnUser.TabIndex = 5;
            this.rbtnUser.TabStop = true;
            this.rbtnUser.Text = "用户";
            this.rbtnUser.UseVisualStyleBackColor = false;
            // 
            // systemLable1
            // 
            this.systemLable1.AutoSize = true;
            this.systemLable1.BackColor = System.Drawing.Color.Transparent;
            this.systemLable1.Font = new System.Drawing.Font("方正舒体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.systemLable1.ForeColor = System.Drawing.Color.MediumBlue;
            this.systemLable1.Location = new System.Drawing.Point(12, 12);
            this.systemLable1.Name = "systemLable1";
            this.systemLable1.Size = new System.Drawing.Size(122, 37);
            this.systemLable1.TabIndex = 11;
            this.systemLable1.Text = "图书馆";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.systemLable1);
            this.panel1.Location = new System.Drawing.Point(-6, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 72);
            this.panel1.TabIndex = 12;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnlogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(414, 249);
            this.Controls.Add(this.systemLable2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbtnUser);
            this.Controls.Add(this.rbtnAdmin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.textPassworld);
            this.Controls.Add(this.passwordLable);
            this.Controls.Add(this.textLogin);
            this.Controls.Add(this.loginLable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户登录";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label loginLable;
        private System.Windows.Forms.TextBox textLogin;
        private System.Windows.Forms.Label passwordLable;
        private System.Windows.Forms.TextBox textPassworld;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Label systemLable2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbtnAdmin;
        private System.Windows.Forms.RadioButton rbtnUser;
        private System.Windows.Forms.Label systemLable1;
        private System.Windows.Forms.Panel panel1;
    }
}

