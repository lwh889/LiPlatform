using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.Form
{
    public class GeneralEventModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]
        public int id { set; get; }

        /// <summary>
        /// 容器名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("事件类型")]
        [Description("")]
        public string eventType { set; get; }

        /// <summary>
        /// 事件名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("事件名称")]
        [Description("")]
        public string eventName { set; get; }

        /// <summary>
        /// 全名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("全名称")]
        [Description("")]
        public string eventFullName { set; get; }

        /// <summary>
        /// 程序集
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("程序集")]
        [Description("")]
        public string eventAssemblyName { set; get; }
    }
}
