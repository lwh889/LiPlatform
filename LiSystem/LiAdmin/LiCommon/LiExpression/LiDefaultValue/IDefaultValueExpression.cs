using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiExpression.LiDefaultValue
{
    public interface IDefaultValueExpression
    {
        object Interpret(string info);
    }
}
