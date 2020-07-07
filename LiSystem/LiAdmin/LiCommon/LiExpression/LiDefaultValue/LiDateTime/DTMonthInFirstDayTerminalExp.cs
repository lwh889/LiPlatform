using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiExpression.LiDefaultValue
{
    public class DTMonthInFirstDayTerminalExp : IDefaultValueExpression
    {
        public object Interpret(string info)
        {
            return Convert.ToDateTime(string.Format("yyyy-MM-01", DateTime.Now.Year, DateTime.Now.Month));
        }
    }
}
