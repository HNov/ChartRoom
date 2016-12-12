using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketCommon
{
    /****************************************
     * 与服务器通信的类
    ****************************************/
    public class SocketHelper
    {
        /// <summary>
        /// 与服务器通信的socket
        /// </summary>
        public static Socket ConnSocket;

        /// <summary>
        /// 保存服务器来的byte
        /// </summary>
        public static byte[] buffer = new byte[1024 * 1024];

        /// <summary>
        /// 保存当登陆成功后。从服务器获取的所有用ip
        /// </summary>
        public static string Ip;

        /// <summary>
        /// time计时器
        /// </summary>
        public static System.Windows.Forms.Timer time;

        /// <summary>
        /// 当前是否连接到服务器
        /// </summary>
        public static bool IsConnect = false;

        /// <summary>
        /// 保存文件的文件名和扩展名 xxx.png
        /// </summary>
        public static string SafeFileName;
    }
}