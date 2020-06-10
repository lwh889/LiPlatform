using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.LiPush
{
    public class PushListButtonModel
    {

        public static PushListButtonModel getInstance(int formId, string categoryGuid)
        {
            return new PushListButtonModel() { id = 0, formId = formId, caption = "按钮1", name = "button1", iconsize = "Large", categoryGuid = categoryGuid, events = new List<PushEventModel>() };
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
        public int formId { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题")]
        public string caption { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        public string name { set; get; }

        /// <summary>
        /// 图标大小,Default,Large,SmallWithText,SmallWithoutText,All
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "Default", "Large", "SmallWithText", "SmallWithoutText", "All" })]
        [Description("")]
        public string iconsize { set; get; }

        /// <summary>
        /// 类别ID
        /// </summary>
        [Browsable(false)]  
        public string categoryGuid { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("图标"), Editor]
        public string icon { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("状态"), Editor]
        public string voucherStatus { set; get; }

        /// <summary>
        /// 顺序
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("顺序")]
        public int iIndex { set; get; }

        /// <summary>
        /// 事件组
        /// </summary>
        [Browsable(false)]
        public List<PushEventModel> events;
    }
}
