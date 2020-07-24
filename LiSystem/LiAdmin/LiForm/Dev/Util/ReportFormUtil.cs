using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
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
        /// 加载基础档案
        /// </summary>
        /// <param name="formModel"></param>
        public static void loadBasicInfo(LiReportModel liReportModel)
        {
            ///获取基础档案Key，然后加载
            List<string> entityKeys = new List<string>();
            List<string> dictKeys = new List<string>();
            foreach (LiReportFieldModel liReportField in liReportModel.datas)
            {
                if (!entityKeys.Contains(liReportField.basicInfoKey))
                {
                    switch (liReportField.controlType)
                    {
                        case "UserEdit":
                        case "GridLookUpEditRef":
                        case "TreeListLookUpEdit":
                            entityKeys.Add(liReportField.basicInfoKey);
                            break;
                        case "StatusEdit":
                        case "GridLookUpEditComboBox":
                            dictKeys.Add(liReportField.dictInfoType);
                            break;
                    }
                }
            }

            ///加载基础档案
            LiContexts.LiContext.addRefDataDataTable(entityKeys);
            LiContexts.LiContext.addDictDataTable(dictKeys);
        }

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
        public static void getBandedGridColumn(List<LiReportFieldModel> liReportFields, Dictionary<string, BandedGridColumn> liBandedGridColumnDict, Dictionary<string, GridBand> liGridBandDict, Dictionary<string, GridBand> liGridBandParentDict, Dictionary<BandedGridColumn, GridBand> liBandedGridColumnAndGridBandDict, BandedGridView bandedGridView, bool IsAddSelField = true)
        {
            if (IsAddSelField)
            {
                BandedGridColumn gridColumnSel = new BandedGridColumn();
                gridColumnSel.Caption = "选择";
                gridColumnSel.Name = "LiSel_GridColumn";
                gridColumnSel.FieldName = "sel";
                gridColumnSel.Visible = true;
                gridColumnSel.VisibleIndex = 0;
                gridColumnSel.Width = 80;
                gridColumnSel.OptionsColumn.AllowEdit = true;

                liBandedGridColumnDict.Add("sel", gridColumnSel);
            }
            //分组
            //获取&分隔线
            List<LiReportFieldModel> groupList = liReportFields.Where(m => m.columnCaption.Contains("&")).ToList();

            if (groupList != null && groupList.Count > 0)
            {
                //汇总的列组名
                List<string> sGroupNameList = new List<string>();
                foreach (LiReportFieldModel model in groupList)
                {
                    string sGroupName = model.columnCaption.Split('&')[0];
                    if (!sGroupNameList.Contains(sGroupName)) sGroupNameList.Add(sGroupName);
                }

                //循环生成列组名
                foreach (string sGroupName in sGroupNameList)
                {
                    if (sGroupName.Contains("|"))
                    {
                        string[] sGroupNameArray = sGroupName.Split('|');

                        //第一层的组
                        GridBand gridBandParent;
                        if (liGridBandParentDict.ContainsKey(sGroupNameArray[0]))
                        {
                            gridBandParent = liGridBandParentDict[sGroupNameArray[0]];
                        }
                        else
                        {
                            gridBandParent = new GridBand();
                            gridBandParent.Caption = sGroupNameArray[0];
                            gridBandParent.Name = string.Format("gridBand_{0}", sGroupNameArray[0]);
                            gridBandParent.Width = 350;

                            liGridBandParentDict.Add(sGroupNameArray[0], gridBandParent);
                        }

                        //中间层的组
                        for (int i = 1; i < sGroupNameArray.Length - 1; i++)
                        {
                            string[] sGroupNameTempArray = new string[i + 1];
                            Array.Copy(sGroupNameArray, sGroupNameTempArray, i + 1);

                            GridBand gridBand;
                            if (liGridBandParentDict.ContainsKey(string.Join("|", sGroupNameTempArray)))
                            {
                                gridBand = liGridBandParentDict[string.Join("|", sGroupNameTempArray)];
                            }
                            else
                            {
                                gridBand = new GridBand();
                                gridBand.Caption = sGroupNameArray[i];
                                gridBand.Name = string.Format("gridBand_{0}", string.Join("|", sGroupNameTempArray));
                                gridBand.Width = 350;

                                liGridBandParentDict.Add(string.Join("|", sGroupNameTempArray), gridBandParent);

                                gridBandParent.Children.Add(gridBand);

                            }

                            gridBandParent = gridBand;


                        }

                        string[] sGroupNameLastTempArray = new string[sGroupNameArray.Length];
                        Array.Copy(sGroupNameArray, sGroupNameLastTempArray, sGroupNameArray.Length);

                        GridBand gridBandLast = new GridBand();
                        gridBandLast.Caption = sGroupNameArray[sGroupNameArray.Length - 1];
                        gridBandLast.Name = string.Format("gridBand_{0}", string.Join("|", sGroupNameLastTempArray));
                        gridBandLast.Width = 350;

                        gridBandParent.Children.Add(gridBandLast);

                        liGridBandDict.Add(string.Join("|", sGroupNameLastTempArray), gridBandLast);
                    }
                    else
                    {
                        GridBand gridBand1 = new GridBand();
                        gridBand1.Caption = sGroupName;
                        gridBand1.Name = string.Format("gridBand_{0}", sGroupName);
                        gridBand1.Width = 350;

                        liGridBandDict.Add(sGroupName, gridBand1);
                        liGridBandParentDict.Add(sGroupName, gridBand1);
                    }
                }
            }


            //初始化表格列信息
            BandedGridColumn gridcolumn = null;
            foreach (LiReportFieldModel model in liReportFields)
            {
                string sGroupName = model.columnCaption.Contains("&") ? model.columnCaption.Split('&')[0] : "";
                string sFieldName = model.columnCaption.Contains("&") ? model.columnCaption.Split('&')[1] : "";

                gridcolumn = new BandedGridColumn();

                gridcolumn.Caption = string.IsNullOrEmpty(sGroupName) ? model.columnCaption : sFieldName;
                gridcolumn.Name = string.Format("gridColumn_{0}", model.columnName);
                gridcolumn.FieldName = model.columnName;
                gridcolumn.Visible = model.bColumnDisplay;
                gridcolumn.VisibleIndex = model.bColumnDisplay == false ? -1 : model.iColumnIndex;
                gridcolumn.Width = model.iColumnWidth;
                gridcolumn.OptionsColumn.AllowEdit = false;
                gridcolumn.Tag = model;
                gridcolumn.GroupIndex = -1;

                gridcolumn.DisplayFormat.FormatString = model.displayFormat;
                if (model.bColumnGroup) gridcolumn.GroupFormat.FormatString = model.displayFormat;
                switch (model.iDisplayFormatType)
                {
                    case 0:
                        gridcolumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                        if (model.bColumnGroup ) gridcolumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.None;
                        break;
                    case 1:
                        gridcolumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        if (model.bColumnGroup ) gridcolumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        break;
                    case 2:
                        gridcolumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        if (model.bColumnGroup ) gridcolumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        break;
                    case 3:
                        gridcolumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        if (model.bColumnGroup ) gridcolumn.GroupFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                        break;
                }

                if (model.bColumnGroup  )
                {
                    bandedGridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, model.columnName, gridcolumn, model.columnGroupFormat);
                    gridcolumn.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] { new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, model.columnName, model.columnGroupFormat) });
                }

                //bandedGridView1.Columns.Add(gridcolumn);
                if (!string.IsNullOrEmpty(sGroupName))
                {
                    GridBand gridBand1 = liGridBandDict[sGroupName];
                    gridBand1.Columns.Add(gridcolumn);
                    liBandedGridColumnAndGridBandDict.Add(gridcolumn, gridBand1);
                }

                liBandedGridColumnDict.Add(gridcolumn.FieldName, gridcolumn);
            }

        }


        /// <summary>
        /// 根据TableModel信息获取列
        /// </summary>
        /// <param name="tableModelList"></param>
        /// <param name="liGridColumnDict"></param>
        /// <param name="IsAddSelField"></param>
        public static void getGridColumn(List<LiReportFieldModel> liReportFields, Dictionary<string, GridColumn> liGridColumnDict , bool IsAddSelField = true)
        {
            if (IsAddSelField)
            {
                BandedGridColumn gridColumnSel = new BandedGridColumn();
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
                GridColumn gridColumn = new GridColumn();
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
