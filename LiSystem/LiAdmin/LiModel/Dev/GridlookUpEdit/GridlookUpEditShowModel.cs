using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Dev.GridlookUpEdit
{
    public class GridlookUpEditShowModel
    {
        /// <summary>
        /// 显示模式
        /// </summary>
        public string showMode { set; get; }

        /// <summary>
        /// 保存值字段名
        /// </summary>
        public string valueMember { set; get; }

        /// <summary>
        /// 显示名字段名
        /// </summary>
        public string displayMember { set; get; }

        /// <summary>
        /// 允许搜索列
        /// </summary>
        public List<string> searchColumns { set; get; }

        /// <summary>
        /// 允许查看列
        /// </summary>
        public List<string> displayColumns { set; get; }

        /// <summary>
        /// 列名与字段名对应
        /// </summary>
        public Dictionary<string, string> dictModelDesc { set; get; }
    }
}
