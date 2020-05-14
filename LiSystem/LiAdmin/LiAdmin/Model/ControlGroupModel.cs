using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiAdmin.Model
{
    public class ControlGroupModel
    {
        public int id { set; get; }
        public int panelModelId { set; get; }
        /// <summary>
        /// 控件组的名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 控件组的标题
        /// </summary>
        public string text { set; get; }

        /// <summary>
        /// 是否自动分配宽度
        /// </summary>
        public bool autoAllocation { set; get; }

        /// <summary>
        /// 控件组
        /// </summary>
        public List<ControlModel> controls;

        /// <summary>
        /// 工具栏按钮
        /// </summary>
        public List<Model.ButtonGroupModel> buttonGroups;
    }
}
