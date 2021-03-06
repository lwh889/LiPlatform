﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiConvert
{
    /// <summary>
    /// 转换类型表体
    /// </summary>
    public class LiConvertBodyModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        public int fid { set; get; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public string convertDestType { set; get; }

        /// <summary>
        /// 目标集合名称
        /// </summary>
        public string convertDCollection { set; get; }

        /// <summary>
        /// 目标字段
        /// </summary>
        public string convertDestField { set; get; }

        /// <summary>
        /// 源类型
        /// </summary>
        public string convertSourceType { set; get; }

        /// <summary>
        /// 源集合名称
        /// </summary>
        public string convertSCollection { set; get; }

        /// <summary>
        /// 源字段
        /// </summary>
        public string convertSourceField { set; get; }

        /// <summary>
        /// 是否引用
        /// </summary>
        public bool bRef { set; get; }

        /// <summary>
        /// 引用基础档案
        /// </summary>
        public string refBasicInfoType { set; get; }

        /// <summary>
        /// 引用关联字段
        /// </summary>
        public string refBasicInfoField { set; get; }
        /// <summary>
        /// 引用值字段
        /// </summary>
        public string refBasicInfoValueField { set; get; }
        /// <summary>
        /// 默认值字段
        /// </summary>
        public string defaultValue { set; get; }
        /// <summary>
        /// 是否使用默认值
        /// </summary>
        public bool bDefault { set; get; }
        /// <summary>
        /// 是否反写ID
        /// </summary>
        public string reverseIdFieldName { set; get; }
        /// <summary>
        /// 是否反写编码
        /// </summary>
        public string reverseCodeFieldName { set; get; }
        /// <summary>
        /// 累计关联数量字段
        /// </summary>
        public bool bCumulativeRelationQty { set; get; }
        /// <summary>
        /// 累计关联ID字段
        /// </summary>
        public bool bCumulativeRelationID { set; get; }
        /// <summary>
        /// 反写类型
        /// </summary>
        public bool bReverseType { set; get; }
        
    }
}
