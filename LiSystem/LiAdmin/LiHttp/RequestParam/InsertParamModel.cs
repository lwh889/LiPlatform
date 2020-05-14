using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 插入参数
    /// </summary>
    public class InsertParamModel : IParamModel
    {
        /// <summary>
        /// 插入类型
        /// </summary>
        public string type { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 插入实体
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 插入参数，列名-值
        /// </summary>
        public Dictionary<string, object> datas { set; get; }
    }
}
