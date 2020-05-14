using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiAdmin.Model
{
    public class ButtonGroupModel
    {
        public int id { set; get; }
        public int formId { set; get; }
        public int panelModelId { set; get; }
        public int controlGroupId { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string text { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public bool allowMinimize { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public bool allowTextClipping { set; get; }

        /// <summary>
        /// 按钮
        /// </summary>
        public List<ButtonModel> buttons { set; get; }
    }
}
