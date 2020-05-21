using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.Form
{
    public class ControlGroupModel
    {
        public static ControlGroupModel getInstance(int panelModelId)
        {
            return new ControlGroupModel() { id = 0, panelModelId = panelModelId, name = "controlGroup", text = "控件组1", rowFieldName = "iRow", autoAllocation = false, controls = new List<ControlModel>(), buttonGroups = new List<ButtonGroupModel>(), events = new List<EventModel>() };
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
        public int panelModelId { set; get; }
        /// <summary>
        /// 控件组的名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        public string name { set; get; }

        /// <summary>
        /// 控件组的标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题")]
        public string text { set; get; }

        /// <summary>
        /// 是否自动分配宽度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("是否自动宽度")]
        public bool autoAllocation { set; get; }

        /// <summary>
        /// 行号字段名
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("行号字段名")]
        public string rowFieldName { set; get; }
        /// <summary>
        /// 控件组
        /// </summary>
        [Browsable(false)]  
        public List<ControlModel> controls;

        /// <summary>
        /// 工具栏按钮
        /// </summary>
        [Browsable(false)]  
        public List<ButtonGroupModel> buttonGroups;

        /// <summary>
        /// 事件组
        /// </summary>
        [Browsable(false)]
        public List<EventModel> events;
    }
}
