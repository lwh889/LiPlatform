using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Basic
{
    /// <summary>
    /// 存储过程参数
    /// </summary>
    public class ProcedureParamModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int fid { set; get; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string paramName { set; get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string paramType { set; get; }

        /// <summary>
        /// 参数长度
        /// </summary>
        public string paramLength { set; get; }
    }
}
