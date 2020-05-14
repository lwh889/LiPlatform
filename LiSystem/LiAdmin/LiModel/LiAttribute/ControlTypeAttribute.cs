using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiAttribute
{
    /// <summary>
    /// 控件类型
    /// </summary>
    public class ControlTypeAttribute  : SingleAttribute
    {
        public ControlTypeAttribute(string value)
            : base(value)
        {
        }

    }
}
