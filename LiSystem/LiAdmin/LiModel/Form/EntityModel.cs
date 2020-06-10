using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.Basic;
using LiModel.LiAttribute;
using LiModel.LiEnum;
namespace LiModel.Form
{
    public class EntityModel
    {
        public static Dictionary<string, object> dataSourceDict = new Dictionary<string, object>();

        public static void InitDataSource(TableModel mainTableModel, List<TableModel> tableModelList)
        {
            EntityModel.clearDataSource(mainTableModel.entityKey);

            foreach (TableModel tableModel in tableModelList)
            {
                EntityModel entityModel = new EntityModel() { sEntityCode = tableModel.tableName, sEntityType = tableModel.entityOrder == "master" ? PanelType.BASIC : PanelType.GRID, sEntityName = tableModel.tableAbbName, sTableName = tableModel.tableName, iShow = tableModel.entityOrder == "master" ? true : false };
                EntityModel.AddItemInDataSource(mainTableModel.entityKey, entityModel);
            }
        }
        public static void InitDataSource( FormModel formModel)
        {
            EntityModel.clearDataSource(formModel.name);

            foreach (PanelModel panelModel in formModel.panels)
            {
                EntityModel entityModel = new EntityModel() { sEntityCode = panelModel.name, sEntityType = panelModel.type, sEntityName = panelModel.text, sTableName = panelModel.tableName, iShow = panelModel.type == PanelType.BASIC ? true : false };
                EntityModel.AddItemInDataSource(formModel.name, entityModel);

            }
        }

        public static void AddItemInDataSource(string entityKey, EntityModel entityModel)
        {
            if (!dataSourceDict.ContainsKey(entityKey))
            {
                dataSourceDict.Add(entityKey, new List<EntityModel>());
            }

            List<EntityModel> _dataSource = dataSourceDict[entityKey] as List<EntityModel>;
            _dataSource.Add(entityModel);
        }

        public static List<EntityModel> getDataSource(string entityKey)
        {
            return dataSourceDict[entityKey] as List<EntityModel>;
        }

        public static void clearDataSource(string entityKey)
        {
            if (!dataSourceDict.ContainsKey(entityKey))
            {
                dataSourceDict.Add(entityKey, new List<EntityModel>());
            }

            List<EntityModel> _dataSource = dataSourceDict[entityKey] as List<EntityModel>;
            _dataSource.Clear();
        }
        [NotCopy]
        public int id { get; set; }
        [NotCopy]
        public int querySchemeId { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string sEntityType { set; get; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string sEntityCode { set; get; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string sEntityName { set; get; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string sTableName { set; get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool iShow { set; get; }

    }
}
