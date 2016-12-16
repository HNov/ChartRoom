namespace SocketServer
{
    partial class ServerClient
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
            this.tbIP = new System.Windows.Forms.TextBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.lbStarteTine = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.lbPoint = new System.Windows.Forms.Label();
            this.lbIP = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lable2 = new System.Windows.Forms.Label();
            this.lable3 = new System.Windows.Forms.Label();
            this.tbPoint = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(64, 307);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(162, 21);
            this.tbIP.TabIndex = 0;
            this.tbIP.Text = "127.0.0.1";
            // 
            // dgvList
            // 
            this.dgvList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.LoginDate});
            this.dgvList.Location = new System.Drawing.Point(13, 12);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(522, 161);
            this.dgvList.TabIndex = 1;
            // 
            // Address
            // 
            this.Address.HeaderText = "地址";
            this.Address.Name = "Address";
            // 
            // LoginDate
            // 
            this.LoginDate.HeaderText = "登录时间";
            this.LoginDate.Name = "LoginDate";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.lbStarteTine);
            this.groupBox.Controls.Add(this.lbCount);
            this.groupBox.Controls.Add(this.lbPoint);
            this.groupBox.Controls.Add(this.lbIP);
            this.groupBox.Controls.Add(this.label4);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Location = new System.Drawing.Point(13, 180);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(522, 121);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "服务信息";
            // 
            // lbStarteTine
            // 
            this.lbStarteTine.AutoSize = true;
            this.lbStarteTine.Location = new System.Drawing.Point(80, 93);
            this.lbStarteTine.Name = "lbStarteTine";
            this.lbStarteTine.Size = new System.Drawing.Size(53, 12);
            this.lbStarteTine.TabIndex = 7;
            this.lbStarteTine.Text = "00:00:00";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(80, 69);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(11, 12);
            this.lbCount.TabIndex = 6;
            this.lbCount.Text = "0";
            // 
            // lbPoint
            // 
            this.lbPoint.AutoSize = true;
            this.lbPoint.Location = new System.Drawing.Point(80, 43);
            this.lbPoint.Name = "lbPoint";
            this.lbPoint.Size = new System.Drawing.Size(0, 12);
            this.lbPoint.TabIndex = 5;
            // 
            // lbIP
            // 
            this.lbIP.AutoSize = true;
            this.lbIP.Location = new System.Drawing.Point(80, 21);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(0, 12);
            this.lbIP.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "启动时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "在线人数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "端口号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // lable2
            // 
            this.lable2.AutoSize = true;
            this.lable2.Location = new System.Drawing.Point(31, 310);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(29, 12);
            this.lable2.TabIndex = 3;
            this.lable2.Text = "IP：";
            // 
            // lable3
            // 
            this.lable3.AutoSize = true;
            this.lable3.Location = new System.Drawing.Point(235, 310);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(53, 12);
            this.lable3.TabIndex = 5;
            this.lable3.Text = "端口号：";
            // 
            // tbPoint
            // 
            this.tbPoint.Location = new System.Drawing.Point(294, 307);
            this.tbPoint.Name = "tbPoint";
            this.tbPoint.Size = new System.Drawing.Size(69, 21);
            this.tbPoint.TabIndex = 4;
            this.tbPoint.Text = "50134";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(379, 307);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "开启服务";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(460, 307);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "关闭服务";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.Location = new System.Drawing.Point(31, 339);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(0, 12);
            this.lbMsg.TabIndex = 8;
            // 
            // ServerClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 360);
            this.Controls.Add(this.lbMsg);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.lable3);
            this.Controls.Add(this.tbPoint);
            this.Controls.Add(this.lable2);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.tbIP);
            this.Name = "ServerClient";
            this.Text = "服务端";
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label lable2;
        private System.Windows.Forms.Label lable3;
        private System.Windows.Forms.TextBox tbPoint;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStarteTine;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label lbPoint;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginDate;
    }
}

