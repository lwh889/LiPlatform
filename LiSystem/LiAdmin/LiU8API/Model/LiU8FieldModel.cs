using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class LiU8FieldModel
    {
        public int id { set; get; }

        public int fid { set; get; }

        /// <summary>
        /// 实体类型
        /// </summary>
        public string fieldEntityType { set; get; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string fieldName { set; get; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string fieldDesc { set; get; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string fieldType { set; get; }

        /// <summary>
        /// 是否必输
        /// </summary>
        public bool fieldIsRequire { set; get; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string fieldMaxValue { set; get; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string fieldMinValue { set; get; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string fieldDefaultValue { set; get; }

        /// <summary>
        /// 最大长度
        /// </summary>
        public int fieldLength { set; get; }
        /// <summary>
        /// 是否已默认值
        /// </summary>
        public bool fieldbDefault { set; get; }
    }
}
