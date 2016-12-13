using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    public static class ServerHelper
    {

        #region 保存接收到的消息
        /// <summary>
        /// 保存接收到的消息
        /// </summary>
        public static byte[] ReceiveBuffer = new byte[1024 * 1024];
        #endregion

        #region 全部的客户端IP地址
        /// <summary>
        /// 全部的客户端IP地址
        /// </summary>
        public static IList<string> IPList
        {
            get
            {
                IList<string> list = new List<string>();
                if (ConnSockets != null)
                {
                    ConnSockets.Keys.CopyTo(list.ToArray(), 0);
                    return list;
                }
                else return list;
            }
        }
        #endregion

        #region 监听Socket
        /// <summary>
        /// 监听Socket
        /// </summary>
        public static Socket ListenSocket;
        #endregion


        #region 当前链接的Socket
        /// <summary>
        /// 当前链接的Socket
        /// </summary>
        public static Hashtable ConnSockets { get; set; }
        #endregion


    }
}
