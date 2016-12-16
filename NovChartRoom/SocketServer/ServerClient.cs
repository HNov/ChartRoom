using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SocketServer
{
    public partial class ServerClient : Form
    {
        public string IP { get; set; }
        public string Point { get; set; }

        #region 服务Socket
        /// <summary>
        /// 服务Socket
        /// </summary>
        public Socket ServerSocket { get; set; }
        #endregion

        public ServerClient()
        {
            InitializeComponent();
            Init();
            StartServer();
        }

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            InitDGV();
            InitEvent();

        }

        #region 初始化DataGridView
        /// <summary>
        /// 初始化DataGridView 
        /// </summary>
        public void InitDGV()
        {
            #region datagridview一些属性设置
            dgvList.AllowUserToAddRows = false;
            dgvList.AllowUserToDeleteRows = false;
            dgvList.AllowUserToResizeColumns = false;
            dgvList.AllowUserToResizeRows = false;
            dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvList.MultiSelect = false;
            dgvList.ReadOnly = true;
            dgvList.RowHeadersVisible = false;
            dgvList.BackgroundColor = Color.White;
            dgvList.ScrollBars = ScrollBars.Vertical;
            dgvList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvList.DataBindingComplete += dgvList_DataBindingComplete;
            #endregion
        }

        #endregion
        #region 初始化事件
        public void InitEvent()
        {
            this.btnOpen.Click += btnOpen_Click;
            this.btnClose.Click += btnClose_Click;
        }
        #endregion
        #endregion

        #region 事件

        private void btnOpen_Click(object sender, EventArgs e)
        {
            StartServer();
            StopServer();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvList.RowHeadersWidth = 60;
            for (int i = 0; i < dgvList.Rows.Count; i++)
            {
                int j = i + 1;
                dgvList.Rows[i].HeaderCell.Value = j.ToString();
            }
        }


        #endregion

        #region 初始化服务Socket
        /// <summary>
        /// 初始化服务Socket
        /// </summary>
        public void InitSocket()
        {
            this.lbIP.Text = "127.0.0.1";
            this.lbPoint.Text = "8000";
        }
        #endregion

        #region 启动服务
        /// <summary>
        /// 启动服务
        /// </summary>
        public void StartServer()
        {
            ServerHelper.ConnSockets = new Dictionary<string, Socket>();
            this.IP = this.tbIP.Text.Trim();
            this.Point = this.tbPoint.Text.Trim();
            if (string.IsNullOrEmpty(IP) || string.IsNullOrEmpty(Point))
            {
                MessageBox.Show("请检查服务地址是否正确!");
                this.lbMsg.Text = "请检查服务地址是否正确!";
                return;
            }

            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(this.IP);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, Convert.ToInt32(this.Point));
            ServerSocket.Bind(ipEndPoint);
            ServerSocket.Listen(100);
            //开始监听链接请求
            ServerSocket.BeginAccept(new AsyncCallback(Accept), ServerSocket);

            this.btnOpen.Enabled = false;
            this.lbIP.Text = this.IP;
            this.lbPoint.Text = this.Point;
            this.lbCount.Text = "0";
            this.lbStarteTine.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.lbMsg.Text = "服务已启动!";
        }
        #endregion

        #region 关闭服务
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void StopServer()
        {

            if (ServerHelper.ListenSocket != null)
            {
                ServerHelper.ListenSocket.Close();
                ServerHelper.ListenSocket = null;
                this.btnOpen.Enabled = true;
                //底部提示消息
                this.lbMsg.Text = "服务器已经关闭";
            }
            else
            {
                this.lbMsg.Text = "服务器未关闭";
            }
        }
        #endregion

        #region 监听连接方法
        /// <summary>
        /// 监听连接方法
        /// </summary>
        public void Accept(IAsyncResult result)
        {
            Socket clientSocket = result.AsyncState as Socket;
            //当前连接Socket
            Socket connSocket = clientSocket.EndAccept(result);
            string ip = connSocket.RemoteEndPoint.ToString();
            if (!string.IsNullOrEmpty(ip) && !ServerHelper.IPList.Contains(ip))
            {
                ServerHelper.ConnSockets.Add(ip, connSocket);
            }
            //再次开始监听
            ServerSocket.BeginAccept(new AsyncCallback(Accept), ServerSocket);
            connSocket.BeginReceive(ServerHelper.ReceiveBuffer, 0, ServerHelper.ReceiveBuffer.Length, 0, new AsyncCallback(Receive), connSocket);
            OnLine(connSocket);
        }
        #endregion

        #region 接收消息
        /// <summary>
        /// 接收消息
        /// </summary>
        public void Receive(IAsyncResult result)
        {

            Socket clientSocket = result.AsyncState as Socket;
            try
            {
                int length = clientSocket.EndReceive(result);
                if (length > 0)
                {
                    byte[] data = new byte[length];
                    //复制实际的长度到data字节数组中
                    Array.Copy(ServerHelper.ReceiveBuffer, 0, data, 0, length);
                    //判断协议位
                    int command = data[0];
                    //获取内容
                    string source = Encoding.UTF8.GetString(data, 1, length - 1);
                    //获取接收者的ip
                    string receiveIp = source.Split(',')[0];
                    if (command == 1) //说明发送的是文字
                    {
                        /*协议：
                         * //[命令(1)|对方的ip和自己的ip 50位)| 内容(文字) | ...]
                         */
                        //获取接收者通信连接的socket
                        if (ServerHelper.ConnSockets.ContainsKey(receiveIp))
                        {
                            ((Socket)(ServerHelper.ConnSockets[receiveIp])).Send(data);
                        }
                    }
                    else if (command == 0) //说明是发送的文件
                    {
                        /*协议: 这里50位不知道是否理想。
                         * [命令(0)| ip(对方的ip和自己的ip 50位)| 内容(文件大小和文件全名 30位)|响应(文件内容) | ...]
                         */

                        //获取接收者通信连接的socket
                        if (ServerHelper.ConnSockets.ContainsKey(receiveIp))
                        {
                            ((Socket)(ServerHelper.ConnSockets[receiveIp])).Send(data);
                        }
                    }
                    else if (command == 2)//抖动一下
                    {
                        //协议
                        //震动
                        //[命令(2)| 对方的ip和自己的ip 50位| ...]

                        //获取接收者通信连接的socket
                        if (ServerHelper.ConnSockets.ContainsKey(receiveIp))
                        {
                            ((Socket)(ServerHelper.ConnSockets[receiveIp])).Send(data);
                        }
                    }

                    //接收其他信息
                    clientSocket.BeginReceive(ServerHelper.ReceiveBuffer, 0, ServerHelper.ReceiveBuffer.Length, 0, new AsyncCallback(Receive), clientSocket);
                }
                else
                {
                    //MessageBox.Show(string.Format("{0},发送消息为空", clientSocket.RemoteEndPoint));
                    ClientOff(clientSocket);
                }
            }
            catch (Exception ex)
            {
                ClientOff(clientSocket);
            }


        }
        #endregion

        #region 客户端上线
        /// <summary>
        /// 客户端上线
        /// </summary>
        public void OnLine(Socket connSocket)
        {
            RefreshSocketList(connSocket);
            SendOnClient(connSocket);
        }
        #endregion

        #region 客户端下线
        /// <summary>
        /// 客户端下线
        /// </summary>
        /// <param name="clientSocket"></param>
        public void ClientOff(Socket clientSocket)
        {
            //从当前连接的集合删除下线的ip
            string outIp = clientSocket.RemoteEndPoint.ToString();
            //移除下线的IP
            RemoveOffLileIP(outIp);
            SendLeaveClient(outIp);
        }
        #endregion

        #region 刷新在线的客户端信息
        /// <summary>
        /// 刷新在线的客户端信息
        /// </summary>
        public void RefreshSocketList(Socket connSocket)
        {
            if (!ServerHelper.ConnSockets.ContainsKey(connSocket.RemoteEndPoint.ToString()))
            {
                ServerHelper.ConnSockets.Add(connSocket.RemoteEndPoint.ToString(), connSocket);
            }
        }
        #endregion

        #region 通知全部客户端新上线的客户端
        /// <summary>
        /// 通知全部客户端新上线的客户端
        /// </summary>
        /// <param name="connSocket"></param>
        public void SendOnClient(Socket connSocket)
        {
            //自定义协议：[命令 2位]
            /*
             * 第一位：10代表是首次登陆获取所有好友，把自己的ip放最后一位
             * 好像这里默认已经是最后一位了？？
             */
            //string key = connSocket.RemoteEndPoint.ToString();
            ////把自己的ip删除
            //if (ServiceCommon.ConnSocket.ContainsKey(key))
            //    ServiceCommon.ConnSocket.Remove(key);

            ////把自己的key添加到最后一位
            //if (!ServiceCommon.ConnSocket.ContainsKey(key))
            //    ServiceCommon.ConnSocket.Add(key, connSocket);

            //发送到客户端
            byte[] clientByte = Encoding.UTF8.GetBytes(string.Join(",", ServerHelper.ConnSockets.Keys));

            List<byte> li = clientByte.ToList();
            li.Insert(0, 10);//第一位插入10 代表是获取好友

            //把当前在线ip发送给自己
            connSocket.Send(li.ToArray());
            SendLoginClient(connSocket.RemoteEndPoint.ToString());
        }
        #region 通知全部客户端新上线的客户端的方法
        /// <summary>
        /// 通知全部客户端新上线的客户端的方法
        /// </summary>
        /// <param name="loginIP"></param>
        public void SendLoginClient(string loginIP)
        {
            foreach (KeyValuePair<string, Socket> item in ServerHelper.ConnSockets)
            {
                //判断新上线的客户是否还在线
                if (!ServerHelper.ConnSockets.ContainsKey(loginIP))
                    return;

                //不需要给自己发送。因为当自己上线的时候。就已经获取了在线的ip
                if (item.Key == loginIP)
                    continue;
                //多线程通知在线用户。
                Thread thread = new Thread(() =>
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(loginIP);
                    List<byte> list = buffer.ToList();
                    //有人上线
                    //[命令(12)| ip(上线的ip)| ...]
                    list.Insert(0, 12);//说明有人上线
                    ((Socket)item.Value).Send(list.ToArray());
                });
                thread.IsBackground = true;
                thread.Start();
            }

        }
        #endregion
        #endregion

        #region 通知全部客户端下线的客户端
        /// <summary>
        /// 通知全部客户端下线的客户端
        /// </summary>
        /// <param name="socket"></param>
        public void SendLeaveClient(string outIp)
        {

            //有人下线 协议
            //[命令(11)| ip(下线的ip)| ...]

            //我这里通知客户端吧
            foreach (KeyValuePair<string, Socket> item in ServerHelper.ConnSockets)
            {
                Thread thread = new Thread(() =>
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(outIp);
                    List<byte> list = buffer.ToList();
                    list.Insert(0, 11);//添加协议位
                    item.Value.Send(list.ToArray());
                });
                thread.IsBackground = true;
                thread.Start();


                //多线程。通知每个在线用户。更新列表
                //ThreadPool.QueueUserWorkItem(new WaitCallback(new Action<object>((o) =>
                //{
                //    string result = string.Join(",", Common.connSocket.Keys);
                //    byte[] buffer = Encoding.UTF8.GetBytes(result);
                //    try
                //    {
                //        //客户端关闭，则发送报异常
                //        item.Value.Send(buffer);
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //})));
                //string result = string.Join(",", Common.connSocket.Keys);
                //byte[] buffer = Encoding.UTF8.GetBytes(result);
                //item.Value.Send(buffer);

            }
        }
        #endregion

        #region 更新在线人数
        /// <summary>
        /// 更新在线人数
        /// </summary>
        public void ChangOnlineCount()
        {
            this.Invoke(new Action(() =>
            {
                this.lbCount.Text = ServerHelper.IPList.Count.ToString();
            }));
        }
        #endregion

        #region 移除下线的客户端IP
        /// <summary>
        /// 移除下线的客户端IP
        /// </summary>
        /// <param name="ip"></param>
        public void RemoveOffLileIP(string ip)
        {
            if (ServerHelper.ConnSockets.ContainsKey(ip))
                ServerHelper.ConnSockets.Remove(ip);
            ChangOnlineCount();
            this.Invoke(new Action(() =>
            {
                //更新列表
                //删除退出的ip
                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    if (dgvList.Rows[i].Tag.ToString() == ip)
                    {
                        dgvList.Rows.RemoveAt(i);
                        break;
                    }
                }
            }));

        }
        #endregion
    }
}
