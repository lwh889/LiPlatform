using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using LiModel.Form;

namespace LiModel.Basic
{
    public class ColumnModel :AGridlookUpEditModelBase
    {
        public override TEntity getInstance<TEntity>()
        {
            return new TEntity();
        }

        public override string getValueMember()
        {
            return "columnName";
        }

        public override string getDisplayMember()
        {
            return "columnAbbName";
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("columnAbbName"))
            {
                SearchColumns.Add("columnAbbName");
            }
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("columnName"))
            {
                DisplayColumns.Add("columnName");
            }
            if (!DisplayColumns.Contains("columnAbbName"))
            {
                DisplayColumns.Add("columnAbbName");
            }
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public override Dictionary<string, string> getDictModelDesc()
        {
            if (!DictModelDesc.ContainsKey("columnName"))
            {
                DictModelDesc.Add("columnName", "列编码");
            }
            if (!DictModelDesc.ContainsKey("columnAbbName"))
            {
                DictModelDesc.Add("columnAbbName", "列名称");
            }
            return DictModelDesc;
        }

        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int fid { set; get; }

        /// <summary>
        /// 列名
        /// </summary>
        public string columnName { set; get; }

        /// <summary>
        /// 列名简称
        /// </summary>
        public string columnAbbName { set; get; }

        /// <summary>
        /// 列类型
        /// </summary>
        public string columnType { set; get; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public string controlType { set; get; }

        /// <summary>
        /// 长度
        /// </summary>
        public int length { set; get; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int columnWidth { set; get; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool primaryKey { set; get; }

        /// <summary>
        /// 是否外键
        /// </summary>
        public bool foreignKey { set; get; }

        /// <summary>
        /// 关系类型，一对多，还是一对一
        /// </summary>
        public int relationshipType { set; get; }

        /// <summary>
        /// 自增类型
        /// </summary>
        public int databaseGeneratedType { set; get; }

        /// <summary>
        /// 对应主表主键列名称
        /// </summary>
        public string primaryKeyName { set; get; }

        /// <summary>
        /// 对应主表主键列，自增类型
        /// </summary>
        public int primaryKeyDatabaseGenerated { set; get; }

        /// <summary>
        /// 对应主表表名称
        /// </summary>
        public string primaryKeyTableName { set; get; }

        /// <summary>
        /// 列顺序
        /// </summary>
        public int columnOrder { set; get; }

        /// <summary>
        /// 列小数位
        /// </summary>
        public int columnScale { set; get; }

        /// <summary>
        /// 列是否为空
        /// </summary>
        public bool columnIsNull { set; get; }

        /// <summary>
        /// 列默认值
        /// </summary>
        public object columnDefaultValue { set; get; }

        /// <summary>
        /// 该列是否可搜索
        /// </summary>
        public bool bSearchColumns { set; get; }

        /// <summary>
        /// 该列是否显示
        /// </summary>
        public bool bDisplayColumn { set; get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool bVisible { set; get; }

        /// <summary>
        /// 基础档案类型
        /// </summary>
        public string basicInfoType { set; get; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string dictInfoType { set; get; }

        /// <summary>
        /// 基础档案显示属性
        /// </summary>
        public string basicInfoShowFieldName { set; get; }

        /// <summary>
        /// 基础档案关联字段名
        /// </summary>
        public string basicInfoRelationFieldName { set; get; }

        /// <summary>
        /// 基础档案主键字段
        /// </summary>
        public string basicInfoKeyFieldName { set; get; }

        /// <summary>
        /// 显示配置
        /// </summary>
        public string gridlookUpEditShowModelJson { set; get; }

        private DateTime _modifyDate;

        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime modifyDate { get { _modifyDate = DateTime.Now; return _modifyDate; } set { _modifyDate = value; } }
    }
}
