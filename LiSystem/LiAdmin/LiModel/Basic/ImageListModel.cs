using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LiModel.Basic
{
    /// <summary>
    /// 图标列表模型
    /// </summary>
    public class ImageListModel
    {
        /// <summary>
        /// 搜索列
        /// </summary>
        public static List<string> searchColumns = new List<string>();

        /// <summary>
        /// 显示列
        /// </summary>
        public static List<string> displayColumns = new List<string>();

        /// <summary>
        /// 列名映射
        /// </summary>
        public static Dictionary<string, string> dictModelDesc = new Dictionary<string, string>();

        /// <summary>
        /// 图标名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public Image image { get; set; }

        /// <summary>
        /// 获取搜索列
        /// </summary>
        public static List<string> getSearchColumns()
        {
            if (!searchColumns.Contains("name"))
            {
                searchColumns.Add("name");
            }
            return searchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public static List<string> getDisplayColumns()
        {
            if (!displayColumns.Contains("image"))
            {
                displayColumns.Add("image");
            }
            if (!displayColumns.Contains("name"))
            {
                displayColumns.Add("name");
            }
            return displayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public static Dictionary<string, string> getDictModelDesc()
        {
            if (!dictModelDesc.ContainsKey("image"))
            {
                dictModelDesc.Add("image", "图像");
            }
            if (!dictModelDesc.ContainsKey("name"))
            {
                dictModelDesc.Add("name", "名称");
            }
            return dictModelDesc;
        }

    }
}
