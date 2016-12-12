using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class ServiceCommon
    {
        /// <summary>
        /// 保存服务器来的消息
        /// </summary>
        public static byte[] ReceiveBuffer = new byte[1024 * 1024];

        #region 监听Socket
        /// <summary>
        /// 监听Socket
        /// </summary>
        public static Socket ListenSocket;
        #endregion

        #region 当前全部正在通讯的Socket
        /// <summary>
        /// 当前全部正在通讯的Socket,保存所有负责通信用是Socket
        /// </summary>
        public static Dictionary<string, Socket> ConnSocket = new Dictionary<string, Socket>();

        #endregion
    }
}
