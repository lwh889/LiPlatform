using LiHttp.GetEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp
{
    public class LiHttpUtil
    {
        /// <summary>
        /// 默认系统数据库名称
        /// </summary>
        public const string SYSTEMCODE_DEFAULT = "LiSystem";

        public static Dictionary<string, AHttpEntity> httpEntitys = new Dictionary<string, AHttpEntity>();

        public static AHttpEntity getHttpEntity(string entityKey, string systemCode = SYSTEMCODE_DEFAULT)
        {
            string keyStr = string.Concat(entityKey, "_", systemCode);
            if (!httpEntitys.ContainsKey(keyStr))
            {
                httpEntitys.Add(keyStr, new HttpEntity(entityKey, systemCode));
            }
            AHttpEntity entity = httpEntitys[keyStr];
            return entity;

        }
        public static void addHttpEntity(string key, AHttpEntity httpEntity)
        {
            httpEntitys.Add(key, httpEntity);
        }

    }
}
