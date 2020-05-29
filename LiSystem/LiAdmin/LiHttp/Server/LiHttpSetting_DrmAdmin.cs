using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiCommon.Util;

namespace LiHttp.Server
{
    /// <summary>
    /// DrmAdmin微服务配置类
    /// 每个控制点一个
    /// </summary>
    public class LiHttpSetting_DrmAdmin : LiHttpSetting
    {

        /// <summary>
        /// 新空壳实体字典
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpGetNewDataDict = new Dictionary<string, LiHttpSetting>();

        /// <summary>
        /// 获取新空壳实体配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpGetNewData(string serverExecName)
        {
            if (!httpGetNewDataDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiGetNewData";
                liHttpSetting.serverExecName = serverExecName;

                httpGetNewDataDict.Add(serverExecName, liHttpSetting);
            }

            return httpGetNewDataDict[serverExecName];
        }

        /// <summary>
        /// 查询字典
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpQueryDict = new Dictionary<string, LiHttpSetting>();

        
        /// <summary>
        /// 获取查询配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpQuery()
        {
            return getHttpQuery("query");
        }
        /// <summary>
        /// 获取查询配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpQuery(string serverExecName)
        {
            if (!httpQueryDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiQuery";
                liHttpSetting.serverExecName = serverExecName;

                httpQueryDict.Add(serverExecName, liHttpSetting);
            }

            return httpQueryDict[serverExecName];
        }

        /// <summary>
        /// 插入字典
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpInsertDict = new Dictionary<string, LiHttpSetting>();

        /// <summary>
        /// 获取更新配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpInsertBatch()
        {
            return getHttpInsert("insertBatch");
        }

        /// <summary>
        /// 获取插入配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpInsert()
        {
            return getHttpInsert("insert");
        }
        /// <summary>
        /// 获取插入配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpInsert(string serverExecName)
        {
            if (!httpInsertDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiInsert";
                liHttpSetting.serverExecName = serverExecName;

                httpInsertDict.Add(serverExecName, liHttpSetting);
            }

            return httpInsertDict[serverExecName];
        }

        /// <summary>
        /// 插入字典
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpDeleteDict = new Dictionary<string, LiHttpSetting>();

        /// <summary>
        /// 获取删除配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpDelete()
        {
            return getHttpDelete("delete");
        }
        /// <summary>
        /// 获取删除配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpDelete(string serverExecName)
        {
            if (!httpDeleteDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiDelete";
                liHttpSetting.serverExecName = serverExecName;

                httpDeleteDict.Add(serverExecName, liHttpSetting);
            }

            return httpDeleteDict[serverExecName];
        }

        /// <summary>
        /// 更新字典
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpUpdateDict = new Dictionary<string, LiHttpSetting>();

        
        /// <summary>
        /// 获取更新配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpUpdate()
        {
            return getHttpUpdate("update");
        }


        /// <summary>
        /// 获取更新配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpUpdateBatch()
        {
            return getHttpUpdate("updateBatch");
        }

        /// <summary>
        /// 获取更新配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpDeleteBatch()
        {
            return getHttpDelete("deleteBatch");
        }
        /// <summary>
        /// 获取更新配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpUpdate(string serverExecName)
        {
            if (!httpUpdateDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiUpdate";
                liHttpSetting.serverExecName = serverExecName;

                httpUpdateDict.Add(serverExecName, liHttpSetting);
            }

            return httpUpdateDict[serverExecName];
        }

        /// <summary>
        /// 查询控件信息配置
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpQueryControlInfoDict = new Dictionary<string, LiHttpSetting>();
        /// <summary>
        /// 获取控件信息配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpQueryControlInfo(string serverExecName)
        {
            if (!httpQueryControlInfoDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiQueryControlInfo";
                liHttpSetting.serverExecName = serverExecName;

                httpQueryControlInfoDict.Add(serverExecName, liHttpSetting);
            }

            return httpQueryControlInfoDict[serverExecName];
        }

        /// <summary>
        /// 控件数据配置字典
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpQueryControlDataDict = new Dictionary<string, LiHttpSetting>();

        /// <summary>
        /// 获取控件数据配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpQueryControlData(string serverExecName)
        {
            if (!httpQueryControlDataDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiQueryControlData";
                liHttpSetting.serverExecName = serverExecName;

                httpQueryControlDataDict.Add(serverExecName, liHttpSetting);
            }

            return httpQueryControlDataDict[serverExecName];
        }

        /// <summary>
        /// 存储过程字典
        /// </summary>
        private static Dictionary<string, LiHttpSetting> httpProcedureDict = new Dictionary<string, LiHttpSetting>();
        /// <summary>
        /// 获取存储过程配置类
        /// </summary>
        /// <param name="serverExecName"></param>
        /// <returns></returns>
        public static LiHttpSetting getHttpProcedure(string serverExecName)
        {
            if (!httpProcedureDict.ContainsKey(serverExecName))
            {
                LiHttpSetting liHttpSetting = new LiHttpSetting();
                liHttpSetting.serverUrl = URL;
                liHttpSetting.serverName = NAME;
                liHttpSetting.serverController = "LiProcedure";
                liHttpSetting.serverExecName = serverExecName;

                httpProcedureDict.Add(serverExecName, liHttpSetting);
            }

            return httpProcedureDict[serverExecName];
        }
    }
}
