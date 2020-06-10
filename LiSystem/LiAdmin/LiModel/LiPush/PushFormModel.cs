using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LiModel.LiAttribute;
using LiModel.LiEnum;

namespace LiModel.LiPush
{
    public class PushFormModel
    {
        public static PushFormModel getInstance(string formTemplateType = FormTemplateType.SINGLEVOUCHER)
        {

            return new PushFormModel() { id = 0, name = "LiPushForm1", text="下推列表标题1",  height = 100, width = 800,  events = new List<PushEventModel>(), listButtons = new List<PushListButtonModel>()};
        }

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Browsable(false)]  
        public int id { set; get; }

        /// <summary>
        /// 容器名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        [Description("")]
        public string name { set; get; }

        /// <summary>
        /// 容器标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题")]
        public string text { set; get; }

        /// <summary>
        /// 容器高度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("高度")]
        public int height { set; get; }

        /// <summary>
        /// 容器宽度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("宽度")]
        public int width { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("系统代码")]
        public string systemCode { set; get; }

        /// <summary>
        /// 事件组
        /// </summary>
        [Browsable(false)]
        public List<PushEventModel> events;

        /// <summary>
        /// 列表按钮
        /// </summary>
        [Browsable(false)]
        public List<PushListButtonModel> listButtons;
    }
}
