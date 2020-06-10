using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.Form;

namespace LiModel.LiConvert
{
    /// <summary>
    /// 转换类型表头
    /// </summary>
    public class LiConvertHeadModel
    {
        public static LiConvertHeadModel getInstance(){

            return new LiConvertHeadModel() { datas = new List<LiConvertBodyModel>(),queryFields = new List<FieldModel>() };
        }

        /// <summary>
        /// 搜索列
        /// </summary>
        private static List<string> searchColumns = new List<string>();

        /// <summary>
        /// 显示列
        /// </summary>
        protected static List<string> displayColumns = new List<string>();

        /// <summary>
        /// 列名映射
        /// </summary>
        private static Dictionary<string, string> dictModelDesc = new Dictionary<string, string>();

        public static string getValueMember()
        {
            return "convertCode";
        }

        public static string getDisplayMember()
        {
            return "convertName";
        }

        public static List<string> getSearchColumns()
        {

            if (!searchColumns.Contains("convertName"))
            {
                searchColumns.Add("convertName");
            }
            return searchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public static List<string> getDisplayColumns()
        {
            if (!displayColumns.Contains("convertCode"))
            {
                displayColumns.Add("convertCode");
            }
            if (!displayColumns.Contains("convertName"))
            {
                displayColumns.Add("convertName");
            }
            return displayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public static Dictionary<string, string> getDictModelDesc()
        {
            if (!dictModelDesc.ContainsKey("convertCode"))
            {
                dictModelDesc.Add("convertCode", "转换编码");
            }
            if (!dictModelDesc.ContainsKey("convertName"))
            {
                dictModelDesc.Add("convertName", "转换名称");
            }
            return dictModelDesc;
        }
        /// <summary>
        /// ID
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 转换编码
        /// </summary>
        public string convertCode { set; get; }

        /// <summary>
        /// 转换名称
        /// </summary>
        public string convertName { set; get; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public string convertDestType { set; get; }

        /// <summary>
        /// 目标
        /// </summary>
        public string convertDest { set; get; }

        /// <summary>
        /// 目标表头名称
        /// </summary>
        public string convertDestHeadName { set; get; }

        /// <summary>
        /// 目标表体名称
        /// </summary>
        public string convertDestBodyName { set; get; }

        /// <summary>
        /// 源类型
        /// </summary>
        public string convertSourceType { set; get; }

        /// <summary>
        /// 源
        /// </summary>
        public string convertSource { set; get; }

        /// <summary>
        /// 转换关系
        /// </summary>
        public string convertRelation { set; get; }

        /// <summary>
        /// 转换关系字段
        /// </summary>
        public string convertRelationField { set; get; }


        /// <summary>
        /// 列表按钮
        /// </summary>
        public List<LiConvertBodyModel> datas;

        /// <summary>
        /// 查询内容
        /// </summary>
        public List<FieldModel> queryFields;
    }
}
