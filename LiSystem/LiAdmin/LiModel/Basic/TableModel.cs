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
    public class TableModel : AGridlookUpEditModelBase
    {
        public override TEntity getInstance<TEntity>()
        {
            return new TEntity();
        }

        public static TableModel getInstanceByBasic()
        {
            TableModel tableModel = new TableModel();
            tableModel.dataBaseName = "LiSystem";
            tableModel.entityType = "Basic";
            tableModel.entityOrder = "master";
            tableModel.className = "JsonModel";
            tableModel.modifyDate = DateTime.Now;
            tableModel.datas = new List<ColumnModel>();

            return tableModel;
        }

        public override  string getValueMember()
        {
            return "entityKey";
        }

        public override string getDisplayMember()
        {
            return "entityName";
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("entityName"))
            {
                SearchColumns.Add("entityName");
            }
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("entityKey"))
            {
                DisplayColumns.Add("entityKey");
            }
            if (!DisplayColumns.Contains("entityName"))
            {
                DisplayColumns.Add("entityName");
            }
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public override Dictionary<string, string> getDictModelDesc()
        {
            if (!DictModelDesc.ContainsKey("entityKey"))
            {
                DictModelDesc.Add("entityKey", "编码");
            }
            if (!DictModelDesc.ContainsKey("entityName"))
            {
                DictModelDesc.Add("entityName", "名称");
            }
            return DictModelDesc;
        }

        /// <summary>
        /// 
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 所属数据库
        /// </summary>
        public string dataBaseName { set; get; }

        /// <summary>
        /// 实体类型，单据？档案？
        /// </summary>
        public string entityType { set; get; }

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode { set; get; }

        /// <summary>
        /// 比如单据Key
        /// </summary>
        public string entityKey { set; get; }

        /// <summary>
        /// 比如单据名称
        /// </summary>
        public string entityName { set; get; }

        /// <summary>
        /// 是否是主表master
        /// </summary>
        public string entityOrder { set; get; }

        /// <summary>
        /// 对应主键上的字段名
        /// </summary>
        public string entityColumnName { set; get; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string tableName { set; get; }

        /// <summary>
        /// 表别名
        /// </summary>
        public string tableAliasName { set; get; }

        /// <summary>
        /// 表中文名
        /// </summary>
        public string tableAbbName { set; get; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string tableDesc { set; get; }

        /// <summary>
        /// 类名
        /// </summary>
        public string className { set; get; }

        /// <summary>
        /// 主键名
        /// </summary>
        public string keyName { set; get; }

        /// <summary>
        /// 子表实体名
        /// </summary>
        public string childTableEntityColumnName { set; get; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime modifyDate { set; get; }

        public List<ColumnModel> datas { set; get; }
    }
}
