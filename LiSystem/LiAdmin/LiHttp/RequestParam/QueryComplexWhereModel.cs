using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.Enum;

namespace LiHttp.RequestParam
{
    /// <summary>
    /// 多层次的查询模型，{"connect":"AND","wheres":[{"columnName":"ABC","columnValue":"123","connect":"AND","wheres":[]},{"columnName":"ASD","columnValue":"456","connect":"AND","wheres":[]},{"columnName":"","columnValue":"","connect":"OR","wheres":[{"columnName":"ASD","columnValue":"456","connect":"OR","wheres":[]},{"columnName":"ASD","columnValue":"456","connect":"OR","wheres":[]}]}]}
    /// </summary>
    public class QueryComplexWhereModel
    {
        /// <summary>
        /// 连接符
        /// </summary>
        public string connect { set; get; }

        /// <summary>
        /// 列名
        /// </summary>
        public string columnName { set; get; }

        /// <summary>
        /// 判断符号
        /// </summary>
        public int judgeSymbol { set; get; }
        /// <summary>
        /// 值
        /// </summary>
        public object columnValue { set; get; }

        /// <summary>
        /// 值
        /// </summary>
        public object columnValue2 { set; get; }

        /// <summary>
        /// 值
        /// </summary>
        public List<object> columnValues { set; get; }

        /// <summary>
        /// 子查询
        /// </summary>
        public List<QueryComplexWhereModel> wheres { set; get; }

        public static QueryComplexWhereModel AND(){
            return new QueryComplexWhereModel() { connect = LogicalOperator.AND, wheres = new List<QueryComplexWhereModel>()};
        }

        public static QueryComplexWhereModel AND(string columnName, object columnValue)
        {
            return AND(columnName, columnValue, JudgeSymbol.Equal);
        }

        public static QueryComplexWhereModel AND(string columnName, object columnValue, int judgeSymbol)
        {
            return new QueryComplexWhereModel() { connect = LogicalOperator.AND, columnName = columnName, columnValue = columnValue,judgeSymbol = judgeSymbol, columnValues = new List<object>() };
        }

        public static QueryComplexWhereModel OR()
        {
            return new QueryComplexWhereModel() { connect = LogicalOperator.OR, wheres = new List<QueryComplexWhereModel>() };
        }

        public static QueryComplexWhereModel OR(string columnName, object columnValue)
        {
            return OR(columnName, columnValue, JudgeSymbol.Equal);
        }

        public static QueryComplexWhereModel OR(string columnName, object columnValue, int judgeSymbol)
        {
            return new QueryComplexWhereModel() { connect = LogicalOperator.OR, columnName = columnName, columnValue = columnValue,judgeSymbol = judgeSymbol, columnValues = new List<object>() };
        }
    }
}
