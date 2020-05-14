using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiLog
{
    public class LogEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 意外
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 主机IP
        /// </summary>
        public string HostIP { get; set; }
        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }
    }
}
