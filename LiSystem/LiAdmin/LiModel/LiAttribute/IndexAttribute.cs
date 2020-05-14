using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiAttribute
{
    /// <summary>
    /// 索引
    /// </summary>
    public class IndexAttribute  : SingleAttribute
    {
        public IndexAttribute(int value)
            : base(value)
        {
        }

    }
}
