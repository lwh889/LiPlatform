using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiFlow.Model;
using LiModel.LiEnum;

namespace LiFlow.Util
{
    public class CommonUtil
    {
        /// <summary>
        /// 流程
        /// </summary>
        /// <param name="queryList"></param>
        /// <param name="IsProcedure"></param>
        /// <returns></returns>
        public static string getFlowWhereStr(List<LiVersionFlowConditionModel> queryList, bool IsProcedure = false)
        {
            StringBuilder sSqlWhere = new StringBuilder();

            if (queryList.Count > 0) sSqlWhere.Append(" and ");
            foreach (LiVersionFlowConditionModel model in queryList)
            {
                sSqlWhere.Append(string.Format(" {0} {1} {2} {3} {4} {5}", model.sBracketsBefore, model.sFieldName, JudgmentSymbol.getJudeSymbol(model.sJudgmentSymbol), getFlowWhereValue(model, IsProcedure), model.sJoin, model.sBracketsAfter));
            }

            return sSqlWhere.ToString();
        }


        public static string getFlowWhereValue(LiVersionFlowConditionModel model, bool IsProcedure = false)
        {
            string value = string.Empty;

            switch (model.sFieldType)
            {
                case "bit":
                case "CheckEdit":
                    value = string.Format("{1}{0}{1}", Convert.ToBoolean(model.oQueryValue), IsProcedure == true ? "''" : "'");
                    break;
                case "dateTime":
                case "datetime2":
                case "timestamp":
                case "DateTimeEdit":
                case "TimeEdit":
                case "DateEdit":
                    value = string.Format("{1}{0}{1}", Convert.ToDateTime(model.oQueryValue).ToString("yyyy-MM-dd"), IsProcedure == true ? "''" : "'");
                    break;
                case "decimal":
                case "float":
                case "int":
                case "smallint":
                case "tinyint":
                case "numeric":
                case "IntEdit":
                case "DecimalEdit":
                case "CalcEdit":
                    value = Convert.ToString(model.oQueryValue);
                    break;
                default:
                    switch (model.sJudgmentSymbol)
                    {
                        case JudgmentSymbol.Like:
                            value = string.Format("{1}%{0}%{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                        case JudgmentSymbol.LikeLeft:
                            value = string.Format("{1}%{0}{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                        case JudgmentSymbol.LikeRight:
                            value = string.Format("{1}{0}%{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                        default:
                            value = string.Format("{1}{0}{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                    }

                    break;
            }


            return value;
        }
    }
}
