using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.Form
{
    public class ControlEventModel
    {
        public static ControlEventModel getInstance(int controlId)
        {
            return new ControlEventModel() { controlId = controlId };
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
        public int controlId { set; get; }

        /// <summary>
        /// 事件类型
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("事件类型")]
        public string eventType { set; get; }

        /// <summary>
        /// 事件表达式
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("事件表达式")]
        public string eventExpression { set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("是否启用")]
        public bool bEnable { set; get; }
        /// <summary>
        /// 事件备注
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("事件备注")]
        public string eventMemo { set; get; }
    }
}
