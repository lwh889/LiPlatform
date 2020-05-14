using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryWhereModel : IParamModel
    {
        /// <summary>
        /// 逻辑符，AND，OR
        /// </summary>
        public string logicalOperators { set; get; }

        /// <summary>
        /// 列名与值的集合
        /// </summary>
        public List<QueryWhereValueModel> values { set; get; }
    }
}
