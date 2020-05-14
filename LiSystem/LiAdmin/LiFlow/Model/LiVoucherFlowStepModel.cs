using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LiModel.LiAttribute;
using Newtonsoft.Json;

namespace LiFlow.Model
{
    public class LiVoucherFlowStepModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Browsable(false)]
        public int id { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public int flowVoucherId { set; get; }

        /// <summary>
        /// 流程顺序
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程顺序")]
        [Description("")]
        public int flowSeq { set; get; }

        /// <summary>
        /// 流程节点ID
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程节点ID")]
        [Description("")]
        public int flowVersionNodeId { set; get; }

        /// <summary>
        /// 流程下一步
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程下一步")]
        [Description("")]
        public int flowVersionNextStepNodeId { set; get; }

        /// <summary>
        /// 流程用户编码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程用户编码")]
        [Description("")]
        public string flowUserCode { set; get; }

        /// <summary>
        /// 流程用户姓名
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程用户姓名")]
        [Description("")]
        public string flowUserName { set; get; }

        /// <summary>
        /// 审批类型
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("审批类型")]
        [Description("")]
        public string flowApprovalType { set; get; }

        /// <summary>
        /// 流程内容
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程内容")]
        [Description("")]
        public string flowContent { set; get; }

        /// <summary>
        /// 流程状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程状态")]
        [Description("")]
        public string flowStatus { set; get; }

        /// <summary>
        /// 流程时间
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程时间")]
        [Description("")]
        public DateTime flowDate { set; get; }
    }
}
