using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Form
{
    public class VoucherCodeModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 实体Key
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 默认方案
        /// </summary>
        public bool bDefault { set; get; }

        /// <summary>
        /// 方案名称
        /// </summary>
        public string name { set; get; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string prefixName { set; get; }

        /// <summary>
        /// 单据文本字段
        /// </summary>
        public string fieldTextName { set; get; }

        /// <summary>
        /// 单据日期字段
        /// </summary>
        public string fieldDateName { set; get; }

        /// <summary>
        /// 日期格式
        /// </summary>
        public string dateTimeFormat { set; get; }

        /// <summary>
        /// 左边补0
        /// </summary>
        public int iZero { set; get; }

        /// <summary>
        /// 流水号步位
        /// </summary>
        public int iStep { set; get; }

        /// <summary>
        /// 流水号范围
        /// </summary>
        public string flowNoRange { set; get; }
    }
}
