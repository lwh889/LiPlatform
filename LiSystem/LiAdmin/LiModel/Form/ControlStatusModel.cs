using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Form
{
    /// <summary>
    /// 控件状态
    /// </summary>
    public class ControlStatusModel : ICloneable 
    {
        public int id { set; get; }
        public int fid { set; get; }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string code { set; get; }
        /// <summary>
        /// 控件名称
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 控件类别
        /// </summary>
        public string groupName { set; get; }

        /// <summary>
        /// 只读
        /// </summary>
        public bool bReadOnly { set; get; }
        /// <summary>
        /// 可视
        /// </summary>
        public bool bVisibe { set; get; }
        /// <summary>
        /// 默认值
        /// </summary>
        public object defaultValue { set; get; }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public ControlStatusModel Clone()
        {
            return (ControlStatusModel)this.MemberwiseClone();
        } 
    }
}
