using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 更新参数类
    /// </summary>
    public class UpdateParamModel : IParamModel
    {
        /// <summary>
        /// 更新类型
        /// </summary>
        public string type { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 更新实体
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 更新参数，列名-值
        /// </summary>
        public Dictionary<string, object> datas { set; get; }
    }
}
