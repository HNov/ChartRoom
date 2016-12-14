namespace SocketClient
{
    partial class Client
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
            this.label1 = new System.Windows.Forms.Label();
            this.myIp = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbHis = new System.Windows.Forms.TextBox();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDd = new System.Windows.Forms.Button();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.btnChangeFile = new System.Windows.Forms.Button();
            this.tbFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "我的IP信息：";
            // 
            // myIp
            // 
            this.myIp.AutoSize = true;
            this.myIp.Location = new System.Drawing.Point(90, 9);
            this.myIp.Name = "myIp";
            this.myIp.Size = new System.Drawing.Size(59, 12);
            this.myIp.TabIndex = 1;
            this.myIp.Text = "127.0.0.1";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(15, 386);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 25;
            this.btnLogin.Text = "链接服务";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // tbHis
            // 
            this.tbHis.Location = new System.Drawing.Point(15, 46);
            this.tbHis.Multiline = true;
            this.tbHis.Name = "tbHis";
            this.tbHis.Size = new System.Drawing.Size(491, 181);
            this.tbHis.TabIndex = 24;
            // 
            // tbContent
            // 
            this.tbContent.Location = new System.Drawing.Point(15, 301);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(491, 78);
            this.tbContent.TabIndex = 23;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(431, 391);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 22;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(339, 391);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDd
            // 
            this.btnDd.Location = new System.Drawing.Point(339, 274);
            this.btnDd.Name = "btnDd";
            this.btnDd.Size = new System.Drawing.Size(75, 23);
            this.btnDd.TabIndex = 20;
            this.btnDd.Text = "抖动";
            this.btnDd.UseVisualStyleBackColor = true;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(431, 245);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(75, 23);
            this.btnSendFile.TabIndex = 19;
            this.btnSendFile.Text = "发送文件";
            this.btnSendFile.UseVisualStyleBackColor = true;
            // 
            // btnChangeFile
            // 
            this.btnChangeFile.Location = new System.Drawing.Point(339, 245);
            this.btnChangeFile.Name = "btnChangeFile";
            this.btnChangeFile.Size = new System.Drawing.Size(75, 23);
            this.btnChangeFile.TabIndex = 18;
            this.btnChangeFile.Text = "选择文件";
            this.btnChangeFile.UseVisualStyleBackColor = true;
            // 
            // tbFile
            // 
            this.tbFile.Location = new System.Drawing.Point(15, 248);
            this.tbFile.Name = "tbFile";
            this.tbFile.Size = new System.Drawing.Size(295, 21);
            this.tbFile.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "用户列表:";
            // 
            // cbList
            // 
            this.cbList.FormattingEnabled = true;
            this.cbList.Location = new System.Drawing.Point(310, 6);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(196, 20);
            this.cbList.TabIndex = 15;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 425);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbHis);
            this.Controls.Add(this.tbContent);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDd);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.btnChangeFile);
            this.Controls.Add(this.tbFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbList);
            this.Controls.Add(this.myIp);
            this.Controls.Add(this.label1);
            this.Name = "Client";
            this.Text = "客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label myIp;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbHis;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDd;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Button btnChangeFile;
        private System.Windows.Forms.TextBox tbFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbList;
    }
}

