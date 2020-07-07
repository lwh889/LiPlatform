using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiExpression.LiDefaultValue
{
    public class DefaultValueTerminalExpression : IDefaultValueExpression
    {
        private List<string> terminalValues;

        public DefaultValueTerminalExpression(List<string> terminalValues)
        {
            this.terminalValues = terminalValues;
        }

        public object Interpret(string info)
        {
            if (terminalValues.Contains(info))
            {
                return info;
            }
            return null;
        }
    }
}
