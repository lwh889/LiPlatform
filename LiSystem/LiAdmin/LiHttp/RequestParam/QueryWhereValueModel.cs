using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 条件基本类
    /// </summary>
    public class QueryWhereValueModel : IParamModel
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string columnName { set; get; }

        /// <summary>
        /// 值
        /// </summary>
        public object value { set; get; }
    }
}
