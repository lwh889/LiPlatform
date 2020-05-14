using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 查询参数类
    /// </summary>
    public class QueryParamModel : IParamModel
    {

        /// <summary>
        /// 查询类型
        /// </summary>
        public string type { set; get; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string option { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 查询实体
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 是否显示所有列
        /// </summary>
        public bool showAllColumn { set; get; }

        /// <summary>
        /// 显示列名集合
        /// </summary>
        public List<string> columns { set; get; }

        /// <summary>
        /// 实例Keys，用于获取基础档案
        /// </summary>
        public List<string> entityKeys { set; get; }

        /// <summary>
        /// 查询条件集合
        /// </summary>
        public List<QueryWhereModel> wheres { set; get; }

        /// <summary>
        /// 多层次查询条件集合
        /// </summary>
        public QueryComplexWhereModel complexWheres { set; get; }

    }
}
