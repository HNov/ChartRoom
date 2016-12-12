namespace SocketClient
{
    partial class UserList
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
            this.cbList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.myIp = new System.Windows.Forms.Label();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.btnChangeFile = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnDd = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnSender = new System.Windows.Forms.Button();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.tbHis = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbList
            // 
            this.cbList.FormattingEnabled = true;
            this.cbList.Location = new System.Drawing.Point(309, 12);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(196, 20);
            this.cbList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "我的IP信息:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户列表:";
            // 
            // myIp
            // 
            this.myIp.AutoSize = true;
            this.myIp.Location = new System.Drawing.Point(89, 15);
            this.myIp.Name = "myIp";
            this.myIp.Size = new System.Drawing.Size(17, 12);
            this.myIp.TabIndex = 3;
            this.myIp.Text = "IP";
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(14, 254);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(295, 21);
            this.tbFile.TabIndex = 5;
            // 
            // btnChangeFile
            // 
            this.btnChangeFile.Location = new System.Drawing.Point(338, 251);
            this.btnChangeFile.Name = "btnChangeFile";
            this.btnChangeFile.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFile.TabIndex = 6;
            this.btnChangeFile.Text = "选择文件";
            this.btnChangeFile.UseVisualStyleBackColor = true;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(430, 251);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(75, 23);
            this.btnSendFile.TabIndex = 7;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            // 
            // btnDd
            // 
            this.btnDd.Location = new System.Drawing.Point(338, 280);
            this.btnDd.Name = "btnDd";
            this.btnDd.Size = new System.Drawing.Size(75, 23);
            this.btnDd.TabIndex = 9;
            this.btnDd.Text = "抖动";
            this.btnDd.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(338, 397);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "关闭";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btnSender
            // 
            this.btnSender.Location = new System.Drawing.Point(430, 397);
            this.btnSender.Name = "btnSender";
            this.btnSender.Size = new System.Drawing.Size(75, 23);
            this.btnSender.TabIndex = 11;
            this.btnSender.Text = "发送";
            this.btnSender.UseVisualStyleBackColor = true;
            // 
            // tbContent
            // 
            this.tbContent.Location = new System.Drawing.Point(14, 307);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(491, 78);
            this.tbContent.TabIndex = 12;
            // 
            // tbHis
            // 
            this.tbHis.Location = new System.Drawing.Point(14, 52);
            this.tbHis.Multiline = true;
            this.tbHis.Name = "tbHis";
            this.tbHis.Size = new System.Drawing.Size(491, 181);
            this.tbHis.TabIndex = 13;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(14, 392);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 14;
            this.btnLogin.Text = "链接服务";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // UserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 432);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbHis);
            this.Controls.Add(this.tbContent);
            this.Controls.Add(this.btnSender);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnDd);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnChangeFile);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.myIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbList);
            this.Name = "UserList";
            this.Text = "客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label myIp;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Button btnChangeFile;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnDd;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnSender;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.TextBox tbHis;
        private System.Windows.Forms.Button btnLogin;
    }
}

