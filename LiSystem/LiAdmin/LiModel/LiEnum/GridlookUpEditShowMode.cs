using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiEnum
{
    /// <summary>
    /// 显示模式
    /// </summary>
    public enum GridlookUpEditShowMode
    {
        /// <summary>
        /// 显示值
        /// </summary>
        VALUE,
        /// <summary>
        /// 显示名称
        /// </summary>
        NAME,
        /// <summary>
        /// 显示值-名称
        /// </summary>
        VALUE_NAME,
        /// <summary>
        /// 显示名称-值
        /// </summary>
        NAME_VALUE

    }

    public class GridlookUpEditShowModeUtil
    {

        public static GridlookUpEditShowMode getEnum(string key)
        {
            switch (key)
            {
                case "VALUE":
                    return GridlookUpEditShowMode.VALUE;
                case "NAME":
                    return GridlookUpEditShowMode.NAME;
                case "NAME_VALUE":
                    return GridlookUpEditShowMode.NAME_VALUE;
                case "VALUE_NAME":
                    return GridlookUpEditShowMode.VALUE_NAME;
                default:
                    return GridlookUpEditShowMode.NAME;
                    break;
            }
        }
    }
}
