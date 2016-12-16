using SocketCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SocketClient
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            Init();
        }

        #region 初始化
        public void Init()
        {
            InitEvent();
        }


        public void InitEvent()
        {
            this.btnLogin.Click += this.btnLogin_Click;
            this.btnSend.Click += this.btnSend_Click;
            this.btnClose.Click += this.btnClose_Click;
            this.btnChangeFile.Click += this.btnSelete_Click;
            this.btnSendFile.Click += this.btnSendFile_Click;
            this.btnDd.Click += this.btnDd_Click;
        }



        #endregion

        #region 事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            ConnectServer();
        }
        private void btnSend_Click(object sender, EventArgs e)
        {

        }
        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

        }

        private void btnSelete_Click(object sender, EventArgs e)
        {

        }
        private void btnDd_Click(object sender, EventArgs e)
        {

        }
        private void btnSendFile_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region 事件方法
        #region 链接服务
        /// <summary>
        /// 链接服务
        /// </summary>
        public void ConnectServer()
        {
            string ip = "127.0.0.1";
            int _point = 50134;
            try
            {
                SocketHelper.ConnSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress address = IPAddress.Parse(ip);
                IPEndPoint point = new IPEndPoint(address, _point);
                SocketHelper.ConnSocket.Connect(point);
                //连接成功，获取服务器发来的消息
                SocketHelper.ConnSocket.BeginReceive(SocketHelper.buffer, 0, SocketHelper.buffer.Length, 0, new AsyncCallback(Receive), SocketHelper.ConnSocket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 发送信息
        /// <summary>
        /// 发送信息
        /// </summary>
        public void SendMsg()
        {

        }
        #endregion

        #region 选择文件
        /// <summary>
        /// 选择文件
        /// </summary>
        public void SeleteFile()
        {
        }
        #endregion

        #region 发送文件
        /// <summary>
        /// 发送文件
        /// </summary>
        public void SendFile()
        {

        }
        #endregion

        #region 发送抖动
        /// <summary>
        /// 发送抖动
        /// </summary>
        public void SendDd()
        {

        }
        #endregion

        #region 关闭客户端
        /// <summary>
        /// 关闭客户端
        /// </summary>
        public void CloseClient()
        {

        }
        #endregion
        #endregion

        #region 接收来自服务器的消息
        /// <summary>
        /// 接收来自服务器的消息
        /// </summary>
        public void Receive(IAsyncResult result)
        {
            Socket clientSocket = result.AsyncState as Socket;
            try
            {
                int length = clientSocket.EndReceive(result);
                if (length > 0)
                {
                    //MessageBox.Show(num.ToString());
                    byte[] buffer = new byte[length];
                    Array.Copy(SocketHelper.buffer, 0, buffer, 0, length); //复制数据到data
                    //string ip = Encoding.UTF8.GetString(data);
                    //string ip = Encoding.UTF8.GetString(data);


                    //以下是客户端=》服务器==》服务器
                    /*
                     * 当if else 超过3个
                     * 
                     * 建议用switch case语句
                     */

                    //获取口令
                    int command = buffer[0];
                    //说明是获取好友
                    if (command == 10)
                    {
                        ReceiveFriends(buffer,length);
                    }
                    else if (command == 11) //说明是有人下线
                    {
                        FriendOffLine(buffer,length);
                    }
                    else if (command == 12) //有人上线了
                    {
                        FriendOnLine(buffer,length);
                    }
                    //以下是客户端=》服务器==》客户端
                    else if (command == 1)
                    {
                        DealMsg(buffer, length);
                    }
                    else if (command == 0) //发送的文件
                    {
                        ReceiveFile(buffer, length);
                    }
                    else if (command == 2) //发送抖动
                    {
                        DdFun();
                    }
                    //连接成功，再一次获取服务器发来的消息
                    clientSocket.BeginReceive(SocketHelper.buffer, 0, SocketHelper.buffer.Length, 0, new AsyncCallback(Receive), clientSocket);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region 回调方法
        #region 接收好友
        public void ReceiveFriends(byte[] buffer,int length)
        {
            //协议说明
            //第一次登陆获取在线(好友)人数
            //[命令(10)| ip(自己的ip和所有好友的ip)| ...]


            //其实用户本地ip也可以这样获取
            //string cy = clientSocket.LocalEndPoint;

            string allIp = Encoding.UTF8.GetString(buffer, 1, length - 1);
            string[] temp = allIp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //跨线程操作UI
            this.Invoke(new Action(() =>
            {
                myIp.Text = temp.Length > 0 ? temp[temp.Length - 1] : "";
                //排除自己的ip
                var other = from i in temp where !i.Contains(myIp.Text) select i;
                cbList.Items.Clear();//清空
                cbList.Items.AddRange(other.ToArray()); //绑定列表
                if (cbList.Items.Count > 0)
                    cbList.SelectedIndex = 0;//默认选中第一个
            }));
        }

        #endregion
        #region 好友下线
        public void FriendOffLine(byte[] buffer,int length)
        {
            //协议说明
            // 有人下线
            //[命令(11)| ip(下线的ip)| ...]

            //获取下线的ip
            string outIp = Encoding.UTF8.GetString(buffer, 1, length - 1);

            this.Invoke(new Action(() =>
            {
                //删除下线的ip
                cbList.Items.Remove(outIp);
                if (cbList.Items.Count > 0)
                    cbList.SelectedIndex = 0;//默认选中第一个
            }));
        }
        #endregion
        #region 好友上线
        public void FriendOnLine(byte[] buffer,int length)
        {
            //协议说明
            // 有人上线
            //[命令(12)| ip(上线的ip)| ...]

            //获取上线的ip
            string onlineIp = Encoding.UTF8.GetString(buffer, 1, length - 1);
            //添加上线的ip

            this.Invoke(new Action(() =>
            {
                //添加上线的ip
                cbList.Items.Add(onlineIp);
                if (cbList.Items.Count > 0)
                    cbList.SelectedIndex = 0;//默认选中第一个
            }));
        }
        #endregion
        #region 处理消息
        public void DealMsg(byte[] buffer,int length)
        {
            //协议：
            //[命令(1)|对方的ip和自己的ip 50位)| 内容(文字) | ...]

            //获取ip段
            string[] sourceIp = Encoding.UTF8.GetString(buffer, 1, 50).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //发消息来的ip
            string fromIp = sourceIp[1];
            //获取内容
            string content = Encoding.UTF8.GetString(buffer, 50 + 1, length - 50 - 1);
            this.Invoke(new Action(() =>
            {
                //列表框中选择当前的ip
                cbList.Text = fromIp.ToString();

                //显示内容
                tbHis.AppendText(string.Format("时间：{0}\n", DateTime.Now.ToString()));
                //tbHis.AppendText(string.Format("提示{0}对我说：", fromIp)); //我操。这样怎么就不行
                tbHis.AppendText(fromIp + "\n");
                tbHis.AppendText("对你说：\n");
                tbHis.AppendText(content + "\n");
                tbHis.AppendText("\n");
            }));
        }

        #endregion
        #region 抖动的方法
        /// <summary>
        /// 抖动的方法
        /// </summary>
        public void DdFun()
        {
            //如果窗口在任务栏。则显示
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            int n = -1;
            for (int i = 0; i < 10; i++)
            {
                n = -n;
                this.Location = new Point(this.Location.X + 10 * n, this.Location.Y + 10 * n);
                this.TopMost = true;//在所有窗口的顶部
                System.Threading.Thread.Sleep(50);
            }
            //抖动完成。结束顶层显示
            this.TopMost = false;

        }
        #endregion
        #region 接收文件
        /// <summary>
        /// 接收文件
        /// </summary>
        public void ReceiveFile(byte[] buffer,int length)
        {
            /*协议: 这里50位不知道是否理想。
                            * [命令(0)| ip(对方的ip和自己的ip 50位)| 内容(文件大小和文件全名 30位)|响应(文件内容) | ...]
                            */

            //这里有冗余代码

            //获取ip段
            string[] sourceIp = Encoding.UTF8.GetString(buffer, 1, 50).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //发消息来的ip
            string fromIp = sourceIp[1];
            this.Invoke(new Action(() =>
            {
                //列表框中选择当前的ip
                cbList.Text = fromIp.ToString();
            }));
            //获取内容
            string content = Encoding.UTF8.GetString(buffer, 50 + 1, 30);
            //获取响应
            //string pass = Encoding.UTF8.GetString(buffer, 50 + 30 + 1, num - 50 - 30 - 1);
            //显示
            //tbHis.AppendText(string.Format("{0}给你发了一个文件：{1}\n", fromIp, content));
            tbHis.AppendText(fromIp);
            tbHis.AppendText("给你发了一个文件");
            tbHis.AppendText(content + "\n");
            tbHis.AppendText("\n");
            //提示用户是否接收文件
            if (MessageBox.Show("是否接受文件\n" + content, "接收文件", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //开始保存
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = content;

                //获取文件类型
                string ex = content.Split('.')[1];

                //保存文件类型
                sfd.Filter = "|*." + ex;
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(buffer, 50 + 30 + 1, length - 50 - 30 - 1);
                    }
                }

            }
        }
        #endregion

        #endregion

    }
}
