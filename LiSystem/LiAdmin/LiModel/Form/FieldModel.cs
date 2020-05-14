using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.LiAttribute;
using System.Windows.Forms;
using LiModel.LiEnum;

namespace LiModel.Form
{
    public class FieldModel : GridlookUpEditModel
    {
        public static Dictionary<string, object> dataSourceDict = new Dictionary<string, object>();

        public static void InitDataSource( FormModel formModel)
        {
            FieldModel.clearDataSource(formModel.name);

            foreach (PanelModel panelModel in formModel.panels)
            {
                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel control in controlGroupModel.controls)
                    {
                        FieldModel fieldModelTemp = new FieldModel();

                        fieldModelTemp.columnFieldName = string.Format("Li{0}_{1}", panelModel.tableName, control.name);
                        fieldModelTemp.fieldName = control.name;
                        fieldModelTemp.code = fieldModelTemp.columnFieldName;
                        fieldModelTemp.name = control.text;
                        fieldModelTemp.sEntityCode = panelModel.name;
                        fieldModelTemp.sEntityName = panelModel.text;
                        fieldModelTemp.iColumnWidth = control.width;
                        fieldModelTemp.bColumnDisplay = control.bVisible;
                        fieldModelTemp.bQuery = false;
                        fieldModelTemp.bRange = false;
                        fieldModelTemp.sColumnControlType = control.controltype;
                        fieldModelTemp.sRefTypeCode = "";
                        fieldModelTemp.sJudgeSymbol = JudgmentSymbol.Equal;

                        FieldModel.AddItemInDataSource(formModel.name, fieldModelTemp);
                    }
                }
            }
        }

        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("name"))
            {
                DisplayColumns.Add("name");
            }
            return DisplayColumns;
        }

        public static void AddItemInDataSource(string entityKey, string code, string name)
        {
            if (!dataSourceDict.ContainsKey(entityKey))
            {
                dataSourceDict.Add(entityKey, new List<FieldModel>());
            }

            List<FieldModel> _dataSource = dataSourceDict[entityKey] as List<FieldModel>;
            _dataSource.Add(new FieldModel() { code = code, name = name});
        }

        public static void AddItemInDataSource(string entityKey, FieldModel fieldModel)
        {
            if (!dataSourceDict.ContainsKey(entityKey))
            {
                dataSourceDict.Add(entityKey, new List<FieldModel>());
            }

            List<FieldModel> _dataSource = dataSourceDict[entityKey] as List<FieldModel>;
            _dataSource.Add(fieldModel);
        }

        public static void clearDataSource(string entityKey)
        {
            if (!dataSourceDict.ContainsKey(entityKey))
            {
                dataSourceDict.Add(entityKey, new List<FieldModel>());
            }

            List<FieldModel> _dataSource = dataSourceDict[entityKey] as List<FieldModel>;
            _dataSource.Clear();
        }

        public static List<FieldModel> getDataSource(string entityKey)
        {
            return dataSourceDict[entityKey] as List<FieldModel>;
        }

        [NotCopy]
        public int id { get; set; }
        [NotCopy]
        public int querySchemeId { get; set; }
        /// <summary>
        /// 转换关系
        /// </summary>
        [NotCopy]
        public int convertId { get; set; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string sEntityCode { set; get; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string sEntityName { set; get; }


        /// <summary>
        /// 字段名称
        /// </summary>
        public string fieldName { set; get; }

        /// <summary>
        /// 列名名称
        /// </summary>
        public string columnFieldName { set; get; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int iColumnWidth { set; get; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool bColumnDisplay { set; get; }

        /// <summary>
        /// 快速查询
        /// </summary>
        public bool bQuery { set; get; }

        /// <summary>
        /// 区间查询
        /// </summary>
        public bool bRange { set; get; }
        
        /// <summary>
        /// 数值类型
        /// </summary>
        public string sColumnControlType { set; get; }

        /// <summary>
        /// 引用类型
        /// </summary>
        public string sRefTypeCode { set; get; }

        /// <summary>
        /// 判断符号
        /// </summary>
        public string sJudgeSymbol { set; get; }


    }
}
