using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Basic
{
    /// <summary>
    /// 存储过程
    /// </summary>
    public class ProcedureModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 所属数据库
        /// </summary>
        public string dataBaseName { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }
        /// <summary>
        /// 单据主键
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 表名
        /// </summary>
        public string procedureName { set; get; }


        public List<LiModel.Basic.ProcedureParamModel> datas { set; get; }
    }
}
