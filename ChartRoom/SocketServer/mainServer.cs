using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketServer
{
    public partial class mainServer : Form
    {

        /*********************协议说明***********************/
        //根据协议解码
        /*
         * 自定义协议规则
         *  [ 命令(1) | 内容(30) | ip(22)| 响应(....) | ...]
           命令：0-文件 1-文字 2-震动
         * 内容：
         * 文件（长度，文件名字+后缀名） 响应(文件的字节)
         * 文字(内容)
         * 震动()
         * ip(自己的ip和对方的ip)
         * 
         * [ 命令(1) | ip(22) | 内容(30)| 响应(....) | ...]
         * 
         * 文件：
         *  [命令(0)| ip(自己的ip和对方的ip)| 内容(文件大小和文件全名)|响应(文件内容) | ...]
         * 文字：
         *  [命令(1)|对方的ip和自己的ip 50位)| 内容(文字) | ...]
         * 震动
         *  [命令(2)| ip(对方的ip)| ...]
         * 更新在线人数
         * [命令(3)| ip(自己的ip和对方的ip)| ...]
         * 第一次登陆获取在线(好友)人数
         * [命令(10)| ip(自己的ip和所有的ip)| ...]
         * 有人下线
         *  [命令(11)| ip(下线的ip)| ...]
         * 有人上线
         *  [命令(12)| ip(上线的ip)| ...]
         *  0
         *  1
         *  2
         *  3
         *  4
         *  5
         * 
         */

        public mainServer()
        {
            InitializeComponent();
            Init();
        }

        #region 初始化datagridview属性
        /// <summary>
        /// 初始化datagridview属性
        /// </summary>
        public void Init()
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

            //窗体加载事件
            //this.Load += new EventHandler(mainServer_Load);

            //启动服务器按钮
            this.btnStart.Click += new EventHandler(btnStart_Click);

            //关闭服务器按钮
            this.btnStop.Click += new EventHandler(btnStop_Click);
        } 
        #endregion

        private void dgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvList.RowHeadersWidth = 60;
            for (int i = 0; i < dgvList.Rows.Count; i++)
            {
                int j = i + 1;
                dgvList.Rows[i].HeaderCell.Value = j.ToString();
            }
        }

        #region 关闭服务器
        /// <summary>
        /// 关闭服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStop_Click(object sender, EventArgs e)
        {
            if (ServiceCommon.ListenSocket != null)
            {
                ServiceCommon.ListenSocket.Close();
                ServiceCommon.ListenSocket = null;
                btnStart.Enabled = true;
                //底部提示消息
                tssMsg.Text = "服务器已经关闭";
            }
        } 
        #endregion

        #region 启动服务器
        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnStart_Click(object sender, EventArgs e)
        {
            StartServer();
        } 
        #endregion

        #region  窗体加载事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mainServer_Load(object sender, EventArgs e)
        {
            //启动时间
            startTime.Text = DateTime.Now.ToString();

            //启动服务器
            StartServer();
        } 
        #endregion

        #region 打开服务器
        /// <summary>
        /// 打开服务器
        /// </summary>
        void StartServer()
        {
            try
            {
                string _ip = tbIp.Text;
                int _point = int.Parse(tbPoint.Text);

                this.lbIP.Text = _ip;
                this.lbPoint.Text = _point.ToString();
                //创建监听客户端请求的socket
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //监听用的ip和端口
                IPAddress address = IPAddress.Parse(_ip);
                IPEndPoint point = new IPEndPoint(address, _point);

                //绑定
                socket.Bind(point);
                socket.Listen(10);


                //异步 开始监听
                socket.BeginAccept(new AsyncCallback(Listen), socket);


                //禁用当前按钮
                btnStart.Enabled = false;

                //启动时间
                startTime.Text = DateTime.Now.ToString();

                //底部提示消息
                tssMsg.Text = "服务器已经启动";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region 开始监听
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="result"></param>
        void Listen(IAsyncResult result)
        {

            try
            {
                //获取监听的socket
                Socket clientSocket = result.AsyncState as Socket;
                //与服务器通信的socket
                Socket connSocket = clientSocket.EndAccept(result);

                string ip = connSocket.RemoteEndPoint.ToString();
                //连接成功。保存信息
                if (!ServiceCommon.ConnSocket.ContainsKey(ip))
                    ServiceCommon.ConnSocket.Add(ip, connSocket);

                //连接成功，更新服务器信息
                changeList(connSocket);


                //等待新的客户端连接 ，相当于循环调用
                clientSocket.BeginAccept(new AsyncCallback(Listen), clientSocket);

                byte[] buffer = new byte[1024 * 1024];


                //接收来自客户端信息 ，相当于循环调用
                connSocket.BeginReceive(ServiceCommon.ReceiveBuffer, 0, ServiceCommon.ReceiveBuffer.Length, 0, new AsyncCallback(Receive), connSocket);

                //用户第一次登陆。获取所有在线用户  如果有好友功能。则获取他的好友
                SendesClient(connSocket);
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message);
            }


        } 
        #endregion
      
        #region 接收来自客户端信息
        /// <summary>
        /// 接收来自客户端信息
        /// </summary>
        /// <param name="result"></param>
        void Receive(IAsyncResult result)
        {

            //与客户端通信的socket
            Socket clientSocket = result.AsyncState as Socket;

            try
            {
                //获取实际的长度值
                int num = clientSocket.EndReceive(result);
                if (num > 0)
                {
                    byte[] data = new byte[num];
                    //复制实际的长度到data字节数组中
                    Array.Copy(ServiceCommon.ReceiveBuffer, 0, data, 0, num);

                    //判断协议位
                    int command = data[0];

                    //获取内容
                    string source = Encoding.UTF8.GetString(data, 1, num - 1);

                    //获取接收者的ip
                    string receiveIp = source.Split(',')[0];


                    if (command == 1) //说明发送的是文字
                    {
                        /*协议：
                         * //[命令(1)|对方的ip和自己的ip 50位)| 内容(文字) | ...]
                         */
                        //获取接收者通信连接的socket
                        if (ServiceCommon.ConnSocket.ContainsKey(receiveIp))
                        {
                            ServiceCommon.ConnSocket[receiveIp].Send(data);
                        }
                    }
                    else if (command == 0) //说明是发送的文件
                    {
                        /*协议: 这里50位不知道是否理想。
                         * [命令(0)| ip(对方的ip和自己的ip 50位)| 内容(文件大小和文件全名 30位)|响应(文件内容) | ...]
                         */

                        //获取接收者通信连接的socket
                        if (ServiceCommon.ConnSocket.ContainsKey(receiveIp))
                        {
                            ServiceCommon.ConnSocket[receiveIp].Send(data);
                        }
                    }
                    else if (command == 2)//抖动一下
                    {
                        //协议
                        //震动
                        //[命令(2)| 对方的ip和自己的ip 50位| ...]

                        //获取接收者通信连接的socket
                        if (ServiceCommon.ConnSocket.ContainsKey(receiveIp))
                        {
                            ServiceCommon.ConnSocket[receiveIp].Send(data);
                        }
                    }

                    //string msg = Encoding.UTF8.GetString(data);
                    //MessageBox.Show(msg);


                    //接收其他信息
                    clientSocket.BeginReceive(ServiceCommon.ReceiveBuffer, 0, ServiceCommon.ReceiveBuffer.Length, 0, new AsyncCallback(Receive), clientSocket);

                }
                else //客户端断开
                {
                    clientOff(clientSocket);
                }
            }
            catch (Exception ex)
            {
                clientOff(clientSocket);
            }

        } 
        #endregion

        #region 客户端关闭
        /// <summary>
        /// 客户端关闭
        /// </summary>
        void clientOff(Socket clientSocket)
        {
            //从集合删除下线的ip
            string outIp = clientSocket.RemoteEndPoint.ToString();
            if (ServiceCommon.ConnSocket.ContainsKey(outIp))
                ServiceCommon.ConnSocket.Remove(outIp);

            //更新服务器在线人数
            changOnlineCount(false);

            this.Invoke(new Action(() =>
            {
                //更新列表
                //删除退出的ip
                for (int i = 0; i < dgvList.Rows.Count; i++)
                {
                    if (dgvList.Rows[i].Tag.ToString() == outIp)
                    {
                        dgvList.Rows.RemoveAt(i);
                        break;
                    }
                }
            }));

            clientSocket.Shutdown(SocketShutdown.Receive);
            clientSocket.Close();

            //通知所有在线用户。有人下线了。需要更新列表，如果是qq是通知我的好友。不知道是不是这样
            /*这里有点疑问：
             * 是客户端定时到服务器获取在线用户？
             * 还是服务器通知客户端
             
             */

            //有人下线 协议
            //[命令(11)| ip(下线的ip)| ...]

            //我这里通知客户端吧
            foreach (KeyValuePair<string, Socket> item in ServiceCommon.ConnSocket)
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

        #region 把上线的人发送到客户端
        /// <summary>
        /// 把上线的人发送到客户端
        /// </summary>
        /// <param name="connSocket">当前连接的客户端</param>
        void SendesClient(Socket connSocket)
        {
            //自定义协议：[命令 2位]
            /*
             * 第一位：10代表是首次登陆获取所有好友，把自己的ip放最后一位
             * 好像这里默认已经是最后一位了？？
             */
            string key = connSocket.RemoteEndPoint.ToString();
            //把自己的ip删除
            if (ServiceCommon.ConnSocket.ContainsKey(key))
                ServiceCommon.ConnSocket.Remove(key);

            //把自己的key添加到最后一位
            if (!ServiceCommon.ConnSocket.ContainsKey(key))
                ServiceCommon.ConnSocket.Add(key, connSocket);

            //发送到客户端
            byte[] clientByte = Encoding.UTF8.GetBytes(string.Join(",", ServiceCommon.ConnSocket.Keys));


            //List<byte> bbb = new List<byte>();
            //bbb.Add(1);
            //bbb.AddRange(clientByte);

            List<byte> li = clientByte.ToList();
            li.Insert(0, 10);//第一位插入10 代表是获取好友

            //把当前在线ip发送给自己
            connSocket.Send(li.ToArray());

            //告诉其他在线的用户。我上线啦，求勾搭
            //var online = from onn in Common.connSocket
            //             where !onn.Key.Contains(connSocket.RemoteEndPoint.ToString())  //筛选，不包含自己的ip
            //             select onn;

            //if (online.Count() <= 0) return; //当前没有上线的

            foreach (KeyValuePair<string, Socket> item in ServiceCommon.ConnSocket)
            {
                //不需要给自己发送。因为当自己上线的时候。就已经获取了在线的ip
                if (item.Key == connSocket.RemoteEndPoint.ToString()) continue;
                //多线程通知在线用户。
                Thread thread = new Thread(() =>
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(connSocket.RemoteEndPoint.ToString());
                    List<byte> list = buffer.ToList();
                    //有人上线
                    //[命令(12)| ip(上线的ip)| ...]
                    list.Insert(0, 12);//说明有人上线
                    item.Value.Send(list.ToArray());
                });
                thread.IsBackground = true;
                thread.Start();
            }

            //判断当前用户是否还处于连接状态
            //if (Common.connSocket.ContainsKey(connSocket.RemoteEndPoint.ToString()))
            //connSocket.Send(buffer);

            //有人下线。就通知所有在线的人数
            //如果是qq我想应该是通知我的好友。不是知道是不是

            /*
             * 如果在线有 A B C 
             * 那么A下线
             * 是不是要通知B C
             * 还是在B C 定时访问服务器来获取在线人数呢？
             * 待解决
             */
        }
        
        #endregion
        
        #region 更新列表
        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name="socket"></param>
        void changeList(Socket socket)
        {
            //获取客户端信息 ip和端口号
            string ip = socket.RemoteEndPoint.ToString();
            //客户端登陆时间
            string time = DateTime.Now.ToString();

            //跨线程操作ui
            this.Invoke(new Action(() =>
            {
                //新增一行
                dgvList.Rows.Add();

                //获取当前dgvList的行
                int rows = dgvList.Rows.Count;

                //赋值
                dgvList.Rows[rows - 1].Cells[0].Value = ip;
                dgvList.Rows[rows - 1].Cells[1].Value = time;

                //把ip当作当前行的tag标记一下，为了删除行的时候可以找到该行
                dgvList.Rows[rows - 1].Tag = ip;

                //更新在线人数
                //lbCount.Text = int.Parse(lbCount.Text) + 1 + "";//后面加空字符串。转为字符串
                //或者
                //lbCount.Text = (int.Parse(lbCount.Text) + 1).ToString();

                //foreach (DataGridViewRow item in dgvList.Rows)
                //{
                //   if(item.Tag==ip)item.

                //}


                //dgvList.DataSource = Common.connSocket;

                //更新在线人数
                changOnlineCount(true);
            }));

        } 
        #endregion

        #region 更新在线人数
        /// <summary>
        /// 更新在线人数
        /// </summary>
        /// <param name="tag">true=>+ false=>-</param>
        void changOnlineCount(bool tag)
        {
            int num = 0;
            if (tag) num = int.Parse(lbCount.Text) + 1;
            else num = int.Parse(lbCount.Text) - 1;

            this.Invoke(new Action(() =>
            {
                //更新在线人数
                lbCount.Text = num.ToString();
                if (num == 0) ServiceCommon.ConnSocket.Clear();

            }));
        } 
        #endregion

    }
}