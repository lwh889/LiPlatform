using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 获取空壳数据
    /// </summary>
    public class GetNewDataParamModel : IParamModel
    {
        /// <summary>
        /// 获取空壳数据类型
        /// </summary>
        public string type { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 获取空壳数据 实体
        /// </summary>
        public string entityKey { set; get; }

    }
}
