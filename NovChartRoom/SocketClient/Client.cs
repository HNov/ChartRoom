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
            //判断是否有选择用户
            string sendUser = cbList.Text;
            //获取聊天的内容
            string content = tbContent.Text;
            if (string.IsNullOrEmpty(sendUser))
            {
                tbHis.AppendText("请选择要发送的用户...\n");   //提示
                return;
            }
            else if (string.IsNullOrEmpty(content))
            {
                tbHis.AppendText("你不打算输入点什么吗...\n");   //提示
                return;
            }
            SendMsg(content);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseClient();
        }

        private void btnSelete_Click(object sender, EventArgs e)
        {
            SeleteFile();
        }
        private void btnDd_Click(object sender, EventArgs e)
        {
            //判断是否有选择用户
            string sendUser = cbList.Text;
            if (string.IsNullOrEmpty(sendUser))
            {
                tbHis.AppendText("请选择要抖动的用户...\n");//提示
                return;
            }
            SendDd();
        }
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            //判断是否有选择用户
            string sendUser = cbList.Text;
            if (string.IsNullOrEmpty(sendUser))
            {
                //提示
                tbHis.AppendText("请选择要发送的用户...\n");
                return;
            }
            //判断是否选择了文件
            else if (string.IsNullOrEmpty(tbFile.Text))
            {
                tbHis.AppendText("请选择文件\n");
                tbHis.AppendText("\n");
                return;
            }
            SendFile();
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
        public void SendMsg(string content)
        {
            try
            {
                //文字： ip设置最大值为 50 位
                //[命令(1)|对方的ip和自己的ip 50位)| 内容(文字) | ...]
                //这里把对方的ip放前面是为了在服务端好获取
                //把自己的ip和对方的ip转为byte

                //如果是独立电脑应该可以这样获取
                //Common.connSocket.LocalEndPoint
                //如果此处不发送自己的ip。那么也可以在服务端获取

                string allIp = string.Format("{0},{1}", cbList.Text, myIp.Text);


                byte[] sendIp = Encoding.UTF8.GetBytes(allIp);
                byte[] buffer = Encoding.UTF8.GetBytes(content);

                List<byte> list = sendIp.ToList();
                list.Insert(0, 1);//添加协议位

                //sendIp 不够50位
                if (sendIp.Length < 50)
                {
                    for (int i = 0; i < 50 - sendIp.Length; i++)
                    {
                        list.Add(0);
                    }
                }

                //把内容添加到末尾
                list.AddRange(buffer);

                //开始发送
                SocketHelper.ConnSocket.Send(list.ToArray());
                tbContent.Clear();

                //把发送的内容显示在上面
                tbHis.AppendText(string.Format("时间：{0}\n", DateTime.Now.ToString()));
                tbHis.AppendText(string.Format("我对{0}说：\n", cbList.Text));
                tbHis.AppendText(content + "\n");
                tbHis.AppendText("\n");
                //清空输入框
                tbContent.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 选择文件
        /// <summary>
        /// 选择文件
        /// </summary>
        public void SeleteFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbFile.Text = ofd.FileName;
                //保存文件名和扩展名
                SocketHelper.SafeFileName = ofd.SafeFileName;
            }
        }
        #endregion

        #region 发送文件
        /// <summary>
        /// 发送文件
        /// </summary>
        public void SendFile()
        {
            //开始读取文件
            using (FileStream fs = new FileStream(tbFile.Text, FileMode.Open, FileAccess.Read))
            {
                //大文件会内存溢出
                //引发类型为“System.OutOfMemoryException”的异常。
                //所以大文件只能 续传。像QQ一样在线接收的方式。
                byte[] buffer = new byte[fs.Length];
                //获取实际的字节数，如果有需要的话。
                int num = fs.Read(buffer, 0, buffer.Length);

                /*协议: 这里50位不知道是否理想？？
                 * 是不是可以修改为：第一位 协议 第二位标记ip的长度 第三位标记内容的长度？？
                 * [命令(0)| ip(对方的ip和自己的ip 50位)| 内容(文件大小和文件全名 30)|响应(文件内容) | ...]
                 */
                string allIp = string.Format("{0},{1}", cbList.Text, myIp.Text);
                byte[] sendIp = Encoding.UTF8.GetBytes(allIp);

                List<byte> list = sendIp.ToList();

                //sendIp 不够50位
                if (sendIp.Length < 50)
                {
                    for (int i = 0; i < 50 - sendIp.Length; i++)
                    {
                        list.Add(0);
                    }
                }
                list.Insert(0, 0); //添加协议位
                //添加内容
                byte[] fileByte = Encoding.UTF8.GetBytes(SocketHelper.SafeFileName);
                list.AddRange(fileByte);
                //内容是否够30
                if (fileByte.Length < 30)
                {
                    for (int i = 0; i < 30 - fileByte.Length; i++)
                    {
                        list.Add(0);
                    }
                }
                //添加响应
                list.AddRange(buffer);

                //开始发送
                SocketHelper.ConnSocket.Send(list.ToArray());
            }
        }
        #endregion

        #region 发送抖动
        /// <summary>
        /// 发送抖动
        /// </summary>
        public void SendDd()
        {
            //协议
            //震动
            //[命令(2)| 对方的ip和自己的ip 50位| ...]
            string allIp = string.Format("{0},{1}", cbList.Text, myIp.Text);

            byte[] sendIp = Encoding.UTF8.GetBytes(allIp);

            List<byte> list = sendIp.ToList();
            list.Insert(0, 2);//添加协议位

            //sendIp 不够50位
            if (sendIp.Length < 50)
            {
                for (int i = 0; i < 50 - sendIp.Length; i++)
                {
                    list.Add(0);
                }
            }
            //开始发送
            SocketHelper.ConnSocket.Send(list.ToArray());
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
