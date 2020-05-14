using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LiModel.LiAttribute;


namespace LiFlow.Model
{
    public class LiFlowConditionModel
    {

        [NotCopy]
        public int id { get; set; }
        [NotCopy]
        public int flowConnectorId { get; set; }
        /// <summary>
        /// 前括号
        /// </summary>
        public string sBracketsBefore { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public string sFieldName { get; set; }

        /// <summary>
        /// 判断符号
        /// </summary>
        public string sJudgmentSymbol { get; set; }

        /// <summary>
        /// 查询值
        /// </summary>
        public object oQueryValue { get; set; }

        /// <summary>
        /// 连接条件
        /// </summary>
        public string sJoin { get; set; }

        /// <summary>
        /// 后括号
        /// </summary>
        public string sBracketsAfter { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string sFieldType { get; set; }

        /// <summary>
        /// 档案编码
        /// </summary>
        public string sBasicCode { get; set; }
    }
}
