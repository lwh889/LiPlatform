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
    public class LiVersionFlowModel
    {
        public static LiVersionFlowModel getInstance()
        {
            return new LiVersionFlowModel() { nodes = new List<LiVersionFlowNodeModel>() };
        }

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
        /// 是否默认
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("是否默认")]
        [Description("")]
        public bool bDefault { set; get; }

        /// <summary>
        /// 开启状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("开启状态")]
        [Description("")]
        public string openStatus { set; get; }

        /// <summary>
        /// 流程日期
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程日期")]
        [Description("")]
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime flowVersionDate { set; get; }

        /// <summary>
        /// 节点
        /// </summary>
        [Browsable(false)]
        public List<LiVersionFlowNodeModel> nodes;
    }
}
