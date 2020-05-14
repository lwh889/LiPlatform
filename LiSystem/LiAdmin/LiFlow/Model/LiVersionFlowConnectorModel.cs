using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using LiModel.LiAttribute;

namespace LiFlow.Model
{
    public class LiVersionFlowConnectorModel
    {
        public static LiVersionFlowConnectorModel getInstance()
        {
            return new LiVersionFlowConnectorModel() { conditions = new List<LiVersionFlowConditionModel>() };
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
        public int flowNodeId { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public string flowNodeCodeTo { set; get; }


        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public string flowNodeNameTo { set; get; }

        /// <summary>
        /// 目标节点哪个锚点
        /// </summary>
        [Browsable(false)]
        public int flowNodeToIndex { set; get; }

        /// <summary>
        /// 源节点哪个锚点
        /// </summary>
        [Browsable(false)]
        public int flowNodeFormIndex { set; get; }

        /// <summary>
        /// 判断条件
        /// </summary>
        [Browsable(false)]
        public List<LiVersionFlowConditionModel> conditions;
    }
}
