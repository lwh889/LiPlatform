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
    public class LiVersionFlowUserModel
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
        public int flowNodeId { set; get; }

        /// <summary>
        /// 用户编码
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("用户编码")]
        [Description("")]
        public string userCode { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("用户名称")]
        [Description("")]
        public string userName { set; get; }
    }
}
