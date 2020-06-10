using LiModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class LiU8FieldModel : AGridlookUpEditModelBase
    {
        public override TEntity getInstance<TEntity>()
        {
            return new TEntity();
        }
        public override string getValueMember()
        {
            return "fieldName";
        }

        public override string getDisplayMember()
        {
            return "fieldDesc";
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("fieldName"))
            {
                SearchColumns.Add("fieldName");
            }
            if (!SearchColumns.Contains("fieldDesc"))
            {
                SearchColumns.Add("fieldDesc");
            }
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("fieldName"))
            {
                DisplayColumns.Add("fieldName");
            }
            if (!DisplayColumns.Contains("fieldDesc"))
            {
                DisplayColumns.Add("fieldDesc");
            }
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public override Dictionary<string, string> getDictModelDesc()
        {
            if (!DictModelDesc.ContainsKey("fieldName"))
            {
                DictModelDesc.Add("fieldName", "字段名");
            }
            if (!DictModelDesc.ContainsKey("fieldDesc"))
            {
                DictModelDesc.Add("fieldDesc", "字段描述");
            }
            return DictModelDesc;
        }

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
