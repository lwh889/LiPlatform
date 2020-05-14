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
    public class LiVersionFlowNodeModel
    {
        public static LiVersionFlowNodeModel getInstance()
        {
            return new LiVersionFlowNodeModel() { connectors = new List<LiVersionFlowConnectorModel>(), users = new List<LiVersionFlowUserModel>() };
        }
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
        public int flowId { set; get; }

        /// <summary>
        /// 流程节点编码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程节点编码")]
        [Description("")]
        public string flowNodeCode { set; get; }

        /// <summary>
        /// 流程节点名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("流程节点名称")]
        [Description("")]
        public string flowNodeName { set; get; }

        /// <summary>
        /// 流程节点类型
        /// </summary>
        [Browsable(false), Category("基本属性"), DisplayName("流程节点类型")]
        [Description("")]
        public string flowNodeType { set; get; }

        /// <summary>
        /// 流程信息
        /// </summary>
        [Browsable(false), Category("基本属性"), DisplayName("流程信息")]
        [Description("")]
        public string flowNodeInformation { set; get; }

        /// <summary>
        /// X
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("X")]
        [Description("")]
        public int X { set; get; }

        /// <summary>
        /// Y
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("Y")]
        [Description("")]
        public int Y { set; get; }

        /// <summary>
        /// 宽度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("宽度")]
        [Description("")]
        public int width { set; get; }

        /// <summary>
        /// 高度
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("高度")]
        [Description("")]
        public int height { set; get; }

        /// <summary>
        /// 发起用户
        /// </summary>
        [Browsable(false)]
        [JsonIgnore()]
        public List<LiVersionFlowUserModel> Users { set { users = value; } get { return users; } }

        /// <summary>
        /// 发起用户
        /// </summary>
        [Browsable(false)]
        public List<LiVersionFlowUserModel> users;

        /// <summary>
        /// 发起用户
        /// </summary>
        [Browsable(false)]
        [JsonIgnore()]
        public List<LiVersionFlowConnectorModel> Connectors { set { connectors = value; } get { return connectors; } }

        /// <summary>
        /// 发起用户
        /// </summary>
        [Browsable(false)]
        public List<LiVersionFlowConnectorModel> connectors;
    }
}
