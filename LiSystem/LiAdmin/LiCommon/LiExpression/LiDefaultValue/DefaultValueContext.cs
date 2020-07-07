using LiCommon.LiEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiExpression.LiDefaultValue
{
    public class DefaultValueContext
    {
        private List<string> controlTypes;
        private Dictionary<string, IDefaultValueExpression> values = new Dictionary<string, IDefaultValueExpression>();

        private IDefaultValueExpression defaultValueExpression;
        public DefaultValueContext()
        {
            this.controlTypes = ControlType.getControlTyps();
            //根据控件类型计算
            values.Add("CalcEdit", new DecimalTerminalExp());
            values.Add("DecimalEdit", new DecimalTerminalExp());
            values.Add("IntEdit", new IntEditTerminalExp());
            values.Add("TextEdit", new TextTerminalExp());

            //根据表达式计算
            values.Add("今天", new DTToDayTerminalExp());
            values.Add("昨天", new DTYesterdayTerminalExp());
            values.Add("明天", new DTTomorrowTerminalExp());
            values.Add("本月第一天", new DTMonthInFirstDayTerminalExp());

            IDefaultValueExpression controlTypeExp = new DefaultValueTerminalExpression(controlTypes);
            defaultValueExpression = new DefaultValueExpression(controlTypeExp,values);

        }

        public object getDefaultValue(string defaultValue)
        {
            return defaultValueExpression.Interpret(defaultValue);
        }
    }
}
