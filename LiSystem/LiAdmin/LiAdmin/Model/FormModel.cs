using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiAdmin.Model
{
    public class FormModel
    {
        public int id { set; get; }
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

        public List<Model.PanelModel> panels { set; get; }

        /// <summary>
        /// 工具栏按钮
        /// </summary>
        public List<Model.ButtonGroupModel> buttonGroups;

    }
}
