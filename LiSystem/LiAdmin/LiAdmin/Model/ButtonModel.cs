using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiAdmin.Model
{
    public class ButtonModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        public int buttonGroupId { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        public string caption { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 图标大小,Default,Large,SmallWithText,SmallWithoutText,All
        /// </summary>
        public string iconsize { set; get; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public string categoryGuid { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon { set; get; }
    }
}
