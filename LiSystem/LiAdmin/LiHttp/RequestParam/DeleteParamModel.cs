using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 删除参数
    /// </summary>
    public class DeleteParamModel : IParamModel
    {
        /// <summary>
        /// 删除类型
        /// </summary>
        public string type { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 删除实体
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 删除参数，列名-值
        /// </summary>
        public Dictionary<string, object> datas { set; get; }
    }
}
