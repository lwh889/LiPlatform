using LiCommon.LiEnum;
using LiCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiExpression.LiDefaultValue
{
    public class DefaultValueExpression : IDefaultValueExpression
    {
        private IDefaultValueExpression controlTypeExp = null;
        private Dictionary<string, IDefaultValueExpression> valueExp = null;

        public DefaultValueExpression(IDefaultValueExpression controlTypeExp, Dictionary<string, IDefaultValueExpression> valueExp)
        {
            this.controlTypeExp = controlTypeExp;
            this.valueExp = valueExp;
        }

        public object Interpret(string info)
        {
            List<string> placeholders = RegexUtil.CollectStrings(@"\[(.*?)\]", info);

            foreach (string placeholder in placeholders)
            {
                string[] fieldNames = placeholder.Split(':');
                if (fieldNames.Length <= 1) continue;

                if(controlTypeExp.Interpret(fieldNames[0]) != null)
                {
                    if(valueExp.ContainsKey(fieldNames[1]))
                    {
                        return valueExp[fieldNames[1]].Interpret(fieldNames[1]);
                    }
                    else
                    {
                        if (valueExp.ContainsKey(fieldNames[0]))
                        {
                            return valueExp[fieldNames[0]].Interpret(fieldNames[1]);
                        }
                        else
                        {
                            return valueExp[ControlType.TextEdit].Interpret(fieldNames[1]);
                        }
                    }
                }
                else
                {
                    return DBNull.Value;
                }
            }

            return DBNull.Value;
        }
    }
}
