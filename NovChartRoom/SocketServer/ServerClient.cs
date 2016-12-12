using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
        }

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            InitDGV();


        }
        #endregion

        #region 初始化DataGridView
        /// <summary>
        /// 初始化DataGridView 
        /// </summary>
        public void InitDGV()
        {
            ///dgvList

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
            this.IP = this.tbIP.Text.Trim();
            this.Point = this.tbPoint.Text.Trim();
            if (string.IsNullOrEmpty(IP) || string.IsNullOrEmpty(Point))
            {
                MessageBox.Show("请检查服务地址是否正确!");
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
                ServerHelper.IPList.Add(ip.Trim());
                ServerHelper.CoontSockets.Add(ip, connSocket);
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

            Socket tempCoonSocket = result.AsyncState as Socket;
            try
            {
                int length = tempCoonSocket.EndReceive(result);
                if (length > 0)
                {
                    byte[] data = new byte[length];

                }
                else
                {


                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        #endregion

        #region 客户端上线
        /// <summary>
        /// 客户端上线
        /// </summary>
        public void OnLine(Socket connSocket)
        {
            RefreshList(connSocket);
            SendClient(connSocket);


        }
        #endregion

        /// <summary>
        /// 客户端下线
        /// </summary>
        /// <param name="clientSocket"></param>
        public void ClientOff(Socket clientSocket)
        {
            clientSocket.re

        }

        #region 刷新在线的客户端信息
        /// <summary>
        /// 刷新在线的客户端信息
        /// </summary>
        public void RefreshList(Socket connSocket)
        {
            if (!ServerHelper.CoontSockets.ContainsKey(connSocket.RemoteEndPoint))
            {
                ServerHelper.CoontSockets.Add(connSocket.RemoteEndPoint, connSocket);
            }
        }
        #endregion

        #region 通知全部客户端新上线的客户端
        /// <summary>
        /// 通知全部客户端新上线的客户端
        /// </summary>
        /// <param name="socket"></param>
        public void SendClient(Socket socket)
        {


        }
        #endregion

    }
}
