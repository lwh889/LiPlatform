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

namespace LiFlow.Model
{
    public class LiVoucherFlowModel
    {
        [JsonIgnore]
        [DefaultValue(false)]
        public bool sel { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Browsable(false)]
        public int id { set; get; }

        /// <summary>
        /// 流程版本ID
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程版本号")]
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
        /// 流程状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程状态")]
        [Description("")]
        public string flowStatus { set; get; }

        /// <summary>
        /// 流程标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程标题")]
        [Description("")]
        public string flowTitle { set; get; }

        /// <summary>
        /// 流程开始时间
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程开始时间")]
        [Description("")]
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime flowStartDate { get; set; }

        /// <summary>
        /// 流程结束时间
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程结束时间")]
        [Description("")]
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime flowEndDate { get; set; }

        /// <summary>
        /// 流程步骤
        /// </summary>
        [Browsable(false)]
        public List<LiVoucherFlowStepModel> datas { set; get; }


    }
}
