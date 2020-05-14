using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiAdmin.Model
{
    public class ControlModel
    {
        public int id { set; get; }
        public int controlGroupId { set; get; }
        /// <summary>
        /// 控件名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 控件标题
        /// </summary>
        public string text { set; get; }

        /// <summary>
        /// 控件长度
        /// </summary>
        public int length { set; get; }

        /// <summary>
        /// 控件高度
        /// </summary>
        public int height { set; get; }

        /// <summary>
        /// 第几列
        /// </summary>
        public int col { set; get; }

        /// <summary>
        /// 第几行
        /// </summary>
        public int row { set; get; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public string controltype { set; get; }


    }
}
