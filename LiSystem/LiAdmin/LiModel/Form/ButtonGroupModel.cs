using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.Form
{
    public class ButtonGroupModel
    {
        public static ButtonGroupModel getInstance(int formId, int panelModelId, int controlGroupId)
        {
            return  new ButtonGroupModel() { id = 0, formId = formId, panelModelId = panelModelId, controlGroupId = controlGroupId, text = "按钮组1", name="buttonGroup1", allowMinimize = true, allowTextClipping = true ,buttons = new List<ButtonModel>()};
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public int id { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public int formId { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public int panelModelId { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public int controlGroupId { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题")]
        public string text { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        public string name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public bool allowMinimize { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]  
        public bool allowTextClipping { set; get; }

        /// <summary>
        /// 按钮
        /// </summary>
        [Browsable(false)]  
        public List<ButtonModel> buttons { set; get; }
    }
}
