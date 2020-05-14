using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiHttp.Enum
{
    public struct JudgeSymbol
    {
        /// <summary>
        /// 等于
        /// </summary>
        public const int Equal = 0;
        /// <summary>
        /// 不等于
        /// </summary>
        public const int NotEqual = 1;
        /// <summary>
        /// 不相等
        /// </summary>
        public const int Not = 2;
        /// <summary>
        /// 不为空
        /// </summary>
        public const int IsNotNull = 3;
        /// <summary>
        /// 为空
        /// </summary>
        public const int IsNull = 4;
        /// <summary>
        /// 大于
        /// </summary>
        public const int Greater = 5;
        /// <summary>
        /// 小于
        /// </summary>
        public const int Less = 6;
        /// <summary>
        /// 大于等于
        /// </summary>
        public const int GreaterEqual = 7;
        /// <summary>
        /// 小于等于
        /// </summary>
        public const int LessEqual = 8;
        /// <summary>
        /// 相似
        /// </summary>
        public const int Like = 9;
        /// <summary>
        /// 两者之间
        /// </summary>
        public const int BetweenAnd = 10;
        /// <summary>
        /// 包含
        /// </summary>
        public const int In = 11;
    }
}
