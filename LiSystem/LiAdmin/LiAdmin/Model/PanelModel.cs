using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiAdmin.Model
{
    public class PanelModel
    {
        public int id { set; get; }
        public int formModelId { set; get; }
        /// <summary>
        /// 容器布局
        /// </summary>
        public string dock { set; get; }

        /// <summary>
        /// Basic,Grid，容器类型
        /// </summary>
        public string type { set; get; }

        /// <summary>
        /// 容器名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 容器标题
        /// </summary>
        public string text { set; get; }

        /// <summary>
        /// 容器高度
        /// </summary>
        public int height { set; get; }

        /// <summary>
        /// 容器宽度
        /// </summary>
        public int width { set; get; }

        /// <summary>
        /// 控件组的组
        /// </summary>
        public List<ControlGroupModel> controlGroups;

        /// <summary>
        /// 工具栏按钮
        /// </summary>
        public List<Model.ButtonGroupModel> buttonGroups;
    }
}
