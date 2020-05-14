using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.Form
{
    public class EventModel
    {
        public static EventModel getInstance(int formId, int panelModelId, int controlGroupId, int buttonId, int listButtonId)
        {
            return new EventModel() { formId = formId, panelModelId = panelModelId, controlGroupId = controlGroupId, buttonId = buttonId, listButtonId = listButtonId };
        }
        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int id { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int buttonId { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int controlGroupId { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int panelModelId { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int formId { set; get; }
        
        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int listButtonId { set; get; }

        /// <summary>
        /// 全名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("全名称")]
        public string fullName { set; get; }

        /// <summary>
        /// 程序集
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("程序集")]
        public string assemblyName { set; get; }
        
        /// <summary>
        /// 程序集
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("备注")]
        public string eventMemo { set; get; }
        
    }
}
