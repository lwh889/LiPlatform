using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 存储过程参数类
    /// </summary>
    public class ProcedureParamModel : IParamModel
    {
        /// <summary>
        /// 查询类型
        /// </summary>
        public string type { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 查询实体
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public Dictionary<string, object> datas { set; get; }
    }
}
