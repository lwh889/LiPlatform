using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Columns;
using LiContexts;
using LiModel.LiEnum;
using LiModel.LiReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.Dev.Util
{
    public class ReportFormUtil
    {
        /// <summary>
        /// 获取单据列表Form
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static RibbonForm getReportForm(string key, string systemCode)
        {
            LiReportModel reportModel = LiContext.getReportModel(key, systemCode);

            LiReportForm liListForm = new LiReportForm(reportModel);


            return liListForm;

        }
        /// <summary>
        /// 根据TableModel信息获取列
        /// </summary>
        /// <param name="tableModelList"></param>
        /// <param name="liGridColumnDict"></param>
        /// <param name="IsAddSelField"></param>
        public static void getGridColumn(List<LiReportFieldModel> liReportFields, Dictionary<string, GridColumn> liGridColumnDict,  bool IsAddSelField = true)
        {
            if (IsAddSelField)
            {
                GridColumn gridColumnSel = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumnSel.Caption = "选择";
                gridColumnSel.Name = "LiSel_GridColumn";
                gridColumnSel.FieldName = "sel";
                gridColumnSel.Visible = true;
                gridColumnSel.VisibleIndex = 0;
                gridColumnSel.Width = 80;
                gridColumnSel.OptionsColumn.AllowEdit = true;

                liGridColumnDict.Add("sel", gridColumnSel);
            }

            foreach (LiReportFieldModel liReportField in liReportFields)
            {
                GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumn.Caption = liReportField.columnCaption;

                gridColumn.Name = string.Format("Li{0}", liReportField.columnName);
                gridColumn.FieldName = liReportField.columnName;

                gridColumn.Visible = liReportField.bColumnDisplay;
                gridColumn.VisibleIndex = liReportField.iColumnIndex;
                gridColumn.Width = liReportField.iColumnWidth;
                gridColumn.OptionsColumn.AllowEdit = false;

                gridColumn.Tag = liReportField;

                liGridColumnDict.Add(gridColumn.FieldName, gridColumn);
            }
        }
    }
}
