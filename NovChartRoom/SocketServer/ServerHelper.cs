using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                if (CoontSockets != null)
                {
                    CoontSockets.Keys.CopyTo(list.ToArray(), 0);
                    return list;
                }
                else return list;
            }
        }
        #endregion

        #region 当前链接的Socket
        /// <summary>
        /// 当前链接的Socket
        /// </summary>
        public static Hashtable CoontSockets { get; set; }
        #endregion


    }
}
