using DevExpress.XtraGrid.Columns;
using LiCommon.Util;
using LiContexts;
using LiContexts.Model;
using LiHttp.Enum;
using LiModel.Basic;
using LiModel.LiConvert;
using LiModel.LiEnum;
using LiModel.LiTable;
using LiU8API.Model;
using LiVoucherConvert;
using LiVoucherConvert.Model;
using LiVoucherConvert.Service.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiForm.Dev.Util
{
    public class ListFormUtil
    {
        /// <summary>
        /// 单据下推，列表
        /// </summary>
        public static bool pushVoucher(string entityKey, LiConvertHeadModel liConvertHeadModel, TableModel tableModel, List<TableModel> tableModelList, DataRow drHead, List<DataRow> drsBody, Form parentForm)
        {
            bool bSuccess = false;
            LiReponseModel liReponse;

            //根据转换关系判断数据
            switch (liConvertHeadModel.convertRelation)
            {
                case ConvertRelation.PUSHCUMULATIVE:
                    LiRefQuantityForm liRefQuantityForm = new LiRefQuantityForm(liConvertHeadModel, tableModelList, drsBody);

                    if (liRefQuantityForm.ShowDialog() == DialogResult.Yes)
                    {
                        drsBody = liRefQuantityForm.SelectDataRows;
                    }
                    else
                    {
                        return bSuccess;
                    }
                    break;
                case ConvertRelation.ONE:
                    List<LiConvertBodyModel> liConvertBodyModels = liConvertHeadModel.datas.Where(m => string.IsNullOrWhiteSpace(m.reverseIdFieldName)).ToList();
                    if (liConvertBodyModels != null)
                    {
                        foreach (LiConvertBodyModel liConvertBody in liConvertBodyModels)
                        {
                            string idFieldName = string.Format("Li{0}_{1}", liConvertBody.convertSourceType, tableModel.keyName);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(drsBody[0][idFieldName])))
                            {
                                MessageUtil.ShowByWarmTip("已下推！");
                                return bSuccess;
                            }
                        }
                    }
                    break;
            }

            //转换到目标
            switch (liConvertHeadModel.convertDestType)
            {
                case ConvertDestTypeModel.System:
                    LiForm liFormDest = FormUtil.getVoucher(liConvertHeadModel.convertDest) as LiForm;
                    LiSystemConvert liVoucherConvert = LiVoucherConvertUtil.getVoucherConvert(liConvertHeadModel.convertDestType, liConvertHeadModel.convertCode) as LiSystemConvert;
                    liVoucherConvert.convertData = drsBody;
                    liVoucherConvert.formDataDict = liFormDest.formDataDict;
                    liVoucherConvert.liConvertHead = liConvertHeadModel;

                    liReponse = LiVoucherConvertUtil.pushVoucher(liConvertHeadModel.convertDestType, liConvertHeadModel.convertCode);
                    bSuccess = liReponse.bSuccess;
                    if (bSuccess)
                    {
                        LiContext.AddPageMdi(PageFormModel.getInstance(0, liFormDest, liConvertHeadModel.convertDest), parentForm);
                        liFormDest.setVoucherNewStatus();
                        bSuccess = true;
                    }
                    else
                    {
                        MessageUtil.ShowByWarmTip("下推失败！" + liReponse.result);
                    }

                    break;
                case ConvertDestTypeModel.U8:
                    U8VoucherConvert u8VoucherConvert = LiVoucherConvertUtil.getVoucherConvert(liConvertHeadModel.convertDestType, liConvertHeadModel.convertCode) as U8VoucherConvert;
                    u8VoucherConvert.liU8Voucher = LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntitySingle<LiU8VoucherModel>(liConvertHeadModel.convertDest, "code");
                    u8VoucherConvert.convertData = drsBody;
                    u8VoucherConvert.liConvertHead = liConvertHeadModel;
                    u8VoucherConvert.oriVoucherType = tableModel.entityKey;
                    u8VoucherConvert.tableModelList = tableModelList;

                    liReponse = LiVoucherConvertUtil.pushVoucher(liConvertHeadModel.convertDestType, liConvertHeadModel.convertCode);
                    bSuccess = liReponse.bSuccess;
                    if (bSuccess)
                    {
                        MessageUtil.ShowByWarmTip("下推成功！");
                    }
                    else
                    {
                        MessageUtil.ShowByWarmTip("下推失败！" + liReponse.result);
                    }
                    break;
            }
            return bSuccess;
        }

        /// <summary>
        /// 单据参照，列表
        /// </summary>
        public static bool refVoucher(string entityKey, LiConvertHeadModel liConvertHeadModel, TableModel tableModel, List<TableModel> tableModelList, Form parentForm, LiForm liFormDest)
        {
            bool bSuccess = false;


            List<DataRow> drs = null;
            LiReponseModel liReponse;

            //获取参照数据
            switch (liConvertHeadModel.convertSourceType)
            {
                case ConvertSourceTypeModel.Excel:

                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.Filter = "Excel文件|*.xls;*.xlsx;*.csv|所有文件|*.*";
                    openFileDialog1.Title = "请选择Excel文件";
                    openFileDialog1.CheckPathExists = true;
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.Multiselect = false;
                    openFileDialog1.DefaultExt = "所有文件|*.*";
                    openFileDialog1.DefaultExt = "*";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        DataTable dt = ExcelUtil.ExcelToDataTable(openFileDialog1.FileName);
                        drs = dt.Select("1=1").ToList();
                    }
                    break;
                default:
                    LiRefForm liRefForm = new LiRefForm(liConvertHeadModel);
                    if (liRefForm.ShowDialog() == DialogResult.Yes)
                    {

                        drs = liRefForm.SelectDataRows;

                    }
                    break;
            }

            if (drs == null || drs.Count <= 0)
            {
                MessageUtil.ShowByWarmTip("没有转换数据!");
                return bSuccess;
            }
            //转换关系判断数据
            switch (liConvertHeadModel.convertRelation)
            {
                case ConvertRelation.PUSHCUMULATIVE:
                    LiRefQuantityForm liRefQuantityForm = new LiRefQuantityForm(liConvertHeadModel, tableModelList, drs);

                    if (liRefQuantityForm.ShowDialog() == DialogResult.Yes)
                    {
                        drs = liRefQuantityForm.SelectDataRows;
                    }
                    else
                    {
                        return bSuccess;
                    }
                    break;
                case ConvertRelation.ONE:
                    List<LiConvertBodyModel> liConvertBodyModels = liConvertHeadModel.datas.Where(m => string.IsNullOrWhiteSpace(m.reverseIdFieldName)).ToList();
                    if (liConvertBodyModels != null)
                    {
                        foreach (LiConvertBodyModel liConvertBody in liConvertBodyModels)
                        {
                            string idFieldName = string.Format("Li{0}_{1}", liConvertBody.convertSourceType, tableModel.keyName);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(drs[0][idFieldName])))
                            {
                                MessageUtil.ShowByWarmTip("已下推！");
                                return bSuccess;
                            }
                        }
                    }
                    break;
            }

            //转换到目标单据
            switch (liConvertHeadModel.convertDestType)
            {
                case ConvertDestTypeModel.System:
                    bool isNewForm = false;
                    if (liFormDest == null)
                    {
                        liFormDest = FormUtil.getVoucher(liConvertHeadModel.convertDest) as LiForm;
                        isNewForm = true;
                    }
                    LiSystemConvert liVoucherConvert = LiVoucherConvertUtil.getVoucherConvert(liConvertHeadModel.convertDestType, liConvertHeadModel.convertCode) as LiSystemConvert;
                    liVoucherConvert.convertData = drs;
                    liVoucherConvert.formDataDict = liFormDest.formDataDict;
                    liVoucherConvert.liConvertHead = liConvertHeadModel;

                    liReponse = LiVoucherConvertUtil.pushVoucher(liConvertHeadModel.convertDestType, liConvertHeadModel.convertCode);
                    bSuccess = liReponse.bSuccess;
                    if (bSuccess)
                    {
                        if (isNewForm)
                        {
                            LiContext.AddPageMdi(PageFormModel.getInstance(0, liFormDest, liConvertHeadModel.convertDest), parentForm);
                            liFormDest.setVoucherNewStatus();
                        }
                        else
                        {
                            liFormDest.loadData();
                        }
                        bSuccess = true;
                    }
                    else
                    {
                        MessageUtil.ShowByWarmTip("下推失败！" + liReponse.result);
                    }

                    break;
                case ConvertDestTypeModel.U8:
                    //不会参照U8
                    break;
            }
            return bSuccess;
        }

        public static string getFieldNameFormat(string tableName, string columnName)
        {
            return string.Format("Li{0}_{1}", tableName, columnName);
        }
        /// <summary>
        /// 根据TableModel信息获取列
        /// </summary>
        /// <param name="tableModelList"></param>
        /// <param name="liGridColumnDict"></param>
        /// <param name="IsAddSelField"></param>
        public static void getGridColumn( List<TableModel> tableModelList, Dictionary<string, GridColumn> liGridColumnDict,string showGridColumnMode = ShowGridColumnMode.ShowAll, bool IsAddSelField = true)
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

            foreach (TableModel tableModel in tableModelList)
            {
                if (ShowGridColumnMode.ShowHead == showGridColumnMode && tableModel.entityOrder == EntityOrderType.Slave) continue;

                if (ShowGridColumnMode.ShowBody == showGridColumnMode && tableModel.entityOrder == EntityOrderType.Master) continue;

                foreach (ColumnModel columnModel in tableModel.datas)
                {
                    if (columnModel.columnType == ColumnType.Collection) continue;

                    GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                    gridColumn.Caption = columnModel.columnAbbName;
                    switch (columnModel.controlType)
                    {
                        case ControlType.StatusEdit:
                        case ControlType.GridLookUpEditComboBox:
                            gridColumn.Name = string.Format("Li{0}{1}_GridColumn_Name", tableModel.tableName, columnModel.columnName);
                            gridColumn.FieldName = string.Format("Li{0}_{1}_Name", tableModel.tableName, columnModel.columnName);
                            break;
                        default:
                            gridColumn.Name = string.Format("Li{0}{1}_GridColumn", tableModel.tableName, columnModel.columnName);
                            gridColumn.FieldName = string.Format("Li{0}_{1}", tableModel.tableName, columnModel.columnName);
                            break;
                    }
                    gridColumn.Visible = columnModel.bDisplayColumn;
                    gridColumn.VisibleIndex = columnModel.bDisplayColumn ? columnModel.columnOrder : -1;
                    gridColumn.Width = columnModel.columnWidth;
                    gridColumn.OptionsColumn.AllowEdit = false;

                    gridColumn.Tag = columnModel;

                    liGridColumnDict.Add(gridColumn.FieldName, gridColumn);

                }
            }
        }
    }
}
