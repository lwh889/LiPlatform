﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiExpression.LiDefaultValue
{
    public class DTTomorrowTerminalExp : IDefaultValueExpression
    {
        public object Interpret(string info)
        {
            return DateTime.Now.AddDays(1);
        }
    }
}
