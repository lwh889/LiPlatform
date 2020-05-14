using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LiModel.LiAttribute;
using LiModel.Converter;

namespace LiModel.Basic
{
    /// <summary>
    /// 消息列表
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("消息类型")]
        [Description("")]
        public string messageType { set; get; }

        /// <summary>
        /// 日期
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("消息日期")]
        [Description("")]
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime messageDate { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("消息内容")]
        [Description("")]
        public string messageContent { set; get; }

        /// <summary>
        /// 消息已阅
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("消息已阅")]
        [Description("")]
        [DefaultValue(false)]
        public bool messageRead { set; get; }

        /// <summary>
        /// 单据流程ID
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据流程ID")]
        [Description("")]
        public int voucherFlowId { set; get; }

        /// <summary>
        /// 流程版本ID
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程版本ID")]
        [Description("")]
        public int flowVersionId { set; get; }

        /// <summary>
        /// 流程版本号
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程版本号")]
        [Description("")]
        public string flowVersionNumber { set; get; }

        /// <summary>
        /// 流程编码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程编码")]
        [Description("")]
        public string flowCode { set; get; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程名称")]
        [Description("")]
        public string flowName { set; get; }

        /// <summary>
        /// 单据编码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据编码")]
        [Description("")]
        public string entityKey { set; get; }

        /// <summary>
        /// 单据名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据名称")]
        [Description("")]
        public string entityName { set; get; }

        /// <summary>
        /// 单据ID
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据ID")]
        [Description("")]
        public string voucherId { set; get; }

        /// <summary>
        /// 单据编号
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("单据编号")]
        [Description("")]
        public string voucherCode { set; get; }

        /// <summary>
        /// 用户编码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("用户编码")]
        [Description("")]
        public string userCode { set; get; }
    }
}
