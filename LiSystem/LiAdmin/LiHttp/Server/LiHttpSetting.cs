using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiCommon.Util;

namespace LiHttp.Server
{
    /// <summary>
    /// Http连接符配置类
    /// 如http://192.168.0.134:8002/ormadmin/LiQuery/query
    /// serverUrl =http://192.168.0.134:8002 serverName = ormadmin serverController=LiQuery serverExecName=query
    /// </summary>
    public class LiHttpSetting
    {
        /// <summary>
        /// 服务器IP
        /// </summary>
        public static string URL = ConfigUtil.GetKey("serverUrl");

        /// <summary>
        /// 微服务名称
        /// </summary>
        public static string NAME = "ormadmin";

        /// <summary>
        /// IP地址
        /// </summary>
        public string serverUrl { set; get; }
        /// <summary>
        /// 微服务名
        /// </summary>
        public string serverName { set; get; }
        /// <summary>
        /// 控制器节点
        /// </summary>
        public string serverController { set; get; }
        /// <summary>
        /// 执行函数名
        /// </summary>
        public string serverExecName { set; get; }

    }
}
