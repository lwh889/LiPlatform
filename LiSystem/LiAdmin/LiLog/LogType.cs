using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiLog
{
    public enum LogType
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 信息
        /// </summary>
        Info = 2,
        /// <summary>
        /// 一般错误
        /// </summary>
        Error = 3,
        /// <summary>
        /// 警告
        /// </summary>
        Warn = 4,
        /// <summary>
        /// 严重
        /// </summary>
        Fatal = 5
    }
}
