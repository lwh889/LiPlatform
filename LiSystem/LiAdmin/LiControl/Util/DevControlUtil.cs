using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

using DevExpress.XtraBars;
using LiModel.Util;
using LiModel.Form;
using LiCommon.Util;

namespace LiControl.Util
{
   /// <summary>
   /// Dev控件工具类
   /// </summary>
    public class DevControlUtil
    {

        public static void AddRow<TEntity>(GridControl gridControl) where TEntity : new()
        {
            List<TEntity> sList = gridControl.DataSource as List<TEntity>;
            TEntity model = new TEntity();

            sList.Add(model);
            gridControl.RefreshDataSource();
        }

        public static void InsertRow<TEntity>(GridControl gridControl, GridView gridView) where TEntity : new()
        {
            List<TEntity> sList = gridControl.DataSource as List<TEntity>;
            TEntity model = new TEntity();

            sList.Insert(gridView.GetSelectedRows()[0], model);

            gridControl.RefreshDataSource();
        }

        public static void DeleteRow<TEntity>(GridControl gridControl, GridView gridView) where TEntity : new()
        {
            List<TEntity> sList = gridControl.DataSource as List<TEntity>;
            TEntity model = (TEntity)gridView.GetFocusedRow();
            DataUtil.deleteInList(model, sList);

            gridControl.RefreshDataSource();
            gridView.RefreshData();
        }
        /// <summary>
        /// 带出表格的引用值到辅助控件
        /// </summary>
        /// <param name="basicInfoKey">引用档案</param>
        /// <param name="basicInfoTableKey">模型对象</param>
        /// <param name="basicInfoAssistFieldName">模型对象</param>
        /// <param name="entity">模型对象</param>
        /// <param name="liGridColumnRefAssistDict">引用辅助控件的列</param>
        /// <param name="liRefDataDataTable">基础档案数据源</param>
        /// <param name="controlModel">引用控件实体</param>
        /// <param name="gridColumn">当前列，引用控件列</param>
        public static void bringGridRefAssistValue<TEntity>(string basicInfoKey, string basicInfoTableKey, string basicInfoAssistFieldName, TEntity entity, Dictionary<string, GridColumn> liGridColumnRefDict, Dictionary<string, List<GridColumn>> liGridColumnRefAssistDict, Dictionary<string, DataTable> liRefDataDataTable, GridColumn gridColumn) where TEntity : class
        {
            if (liGridColumnRefDict.ContainsKey(gridColumn.FieldName))
            {

                object value = ModelUtil.getModelValue<TEntity>(gridColumn.FieldName, entity);

                DataRow drInfo = getBasicInfoValue(basicInfoKey, basicInfoTableKey, value, liRefDataDataTable);


                List<GridColumn> gridColumnAssists = liGridColumnRefAssistDict[basicInfoTableKey];
                foreach (GridColumn gridColumnAssist in gridColumnAssists)
                {
                    if (drInfo != null)
                    {
                        ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, drInfo[basicInfoAssistFieldName], entity);
                    }
                    else
                    {
                        ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, DBNull.Value, entity);
                    }
                }
            }

        }

        /// <summary>
        /// 带出表格的引用值到辅助控件
        /// </summary>
        /// <param name="entity">模型对象</param>
        /// <param name="liGridColumnRefAssistDict">引用辅助控件的列</param>
        /// <param name="liRefDataDataTable">基础档案数据源</param>
        /// <param name="controlModel">引用控件实体</param>
        /// <param name="gridColumn">当前列，引用控件列</param>
        public static void bringGridRefAssistValue<TEntity>(ControlModel controlModel, TEntity entity, Dictionary<string, GridColumn> liGridColumnRefDict, Dictionary<string, List<GridColumn>> liGridColumnRefAssistDict, Dictionary<string, DataTable> liRefDataDataTable, GridColumn gridColumn) where TEntity : class
        {
            if (!string.IsNullOrEmpty(controlModel.basicInfoKey) && liGridColumnRefAssistDict.ContainsKey(controlModel.basicInfoKey))
            {
                if (liGridColumnRefDict.ContainsKey(gridColumn.FieldName))
                {

                    object value = ModelUtil.getModelValue<TEntity>(gridColumn.FieldName, entity);

                    DataRow drInfo = getBasicInfoValue(controlModel, value, liRefDataDataTable);


                    List<GridColumn> gridColumnAssists = liGridColumnRefAssistDict[controlModel.basicInfoKey];
                    foreach (GridColumn gridColumnAssist in gridColumnAssists)
                    {
                        if (drInfo != null)
                        {
                            ControlModel controlAssistModel = (ControlModel)gridColumnAssist.Tag;
                            ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, drInfo[controlAssistModel.basicInfoAssistFieldName], entity);
                        }
                        else
                        {
                            ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, DBNull.Value, entity);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 带出表格的引用值到辅助控件
        /// </summary>
        /// <param name="dr">当前行</param>
        /// <param name="liGridColumnRefAssistDict">引用辅助控件的列</param>
        /// <param name="liRefDataDataTable">基础档案数据源</param>
        /// <param name="controlModel">引用控件实体</param>
        /// <param name="gridColumn">当前列，引用控件列</param>
        public static void bringGridRefAssistValue(ControlModel controlModel, DataRow dr, Dictionary<string, GridColumn> liGridColumnRefDict, Dictionary<string, List<GridColumn>> liGridColumnRefAssistDict, Dictionary<string, DataTable> liRefDataDataTable, GridColumn gridColumn)
        {
            if (!string.IsNullOrEmpty(controlModel.basicInfoKey) && liGridColumnRefAssistDict.ContainsKey(controlModel.basicInfoKey))
            {
                if (liGridColumnRefDict.ContainsKey(gridColumn.FieldName))
                {

                    object value = dr[gridColumn.FieldName];

                    DataRow drInfo = getBasicInfoValue(controlModel, value, liRefDataDataTable);


                    List<GridColumn> gridColumnAssists = liGridColumnRefAssistDict[controlModel.basicInfoKey];
                    foreach (GridColumn gridColumnAssist in gridColumnAssists)
                    {
                        if (drInfo != null)
                        {
                            ControlModel controlAssistModel = (ControlModel)gridColumnAssist.Tag;
                            dr[gridColumnAssist.FieldName] = drInfo[controlAssistModel.basicInfoAssistFieldName];
                        }
                        else
                        {
                            dr[gridColumnAssist.FieldName] = DBNull.Value;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// 带出引用控件的辅助控件值
        /// </summary>
        /// <param name="liControlRefAssistDict">所有辅助控件</param>
        /// <param name="liRefDataDataTable">所有基础档案</param>
        /// <param name="controlModel">引用控件实体</param>
        /// <param name="control">引用控件</param>
        public static void bringRefAssistValue(Dictionary<string, List<Control>> liControlRefAssistDict, Dictionary<string, DataTable> liRefDataDataTable, ControlModel controlModel, Control control)
        {
            if (liControlRefAssistDict.ContainsKey(controlModel.basicInfoKey))
            {
                object value = DevControlUtil.getControlData(control);

                DataRow drInfo = getBasicInfoValue(controlModel, value, liRefDataDataTable);

                List<Control> controlAssists = liControlRefAssistDict[controlModel.basicInfoKey];
                foreach (Control controlAssist in controlAssists)
                {
                    if (drInfo != null)
                    {
                        ControlModel controlAssistModel = (ControlModel)controlAssist.Tag;
                        DevControlUtil.setContorlData(drInfo[controlAssistModel.basicInfoAssistFieldName], controlAssist);
                    }
                    else
                    {
                        DevControlUtil.setContorlData("", controlAssist);
                    }
                }
            }
        }

        /// <summary>
        /// 获取基础档案数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="controlModel">引用控件实体</param>
        /// <param name="liRefDataDataTable">基础档案数据源</param>
        /// <returns></returns>
        public static DataRow getBasicInfoValue(ControlModel controlModel, object keyValue, Dictionary<string, DataTable> liRefDataDataTable)
        {
            return getBasicInfoValue(controlModel.basicInfoKey, controlModel.basicInfoTableKey, keyValue, liRefDataDataTable);

        }


        /// <summary>
        /// 获取基础档案数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="controlModel">引用控件实体</param>
        /// <param name="liRefDataDataTable">基础档案数据源</param>
        /// <returns></returns>
        public static DataRow getBasicInfoValue(string basicInfoKey, string basicInfoTableKey, object keyValue, Dictionary<string, DataTable> liRefDataDataTable)
        {
            DataTable liDataTable = liRefDataDataTable[basicInfoKey];
            DataRow[] drs = liDataTable.Select(string.Format(" {0} = '{1}' ", basicInfoTableKey, keyValue));

            if (drs != null && drs.Length > 0)
            {
                return drs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="e"></param>
        public static void customDrawRowIndicator(RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle > -1)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        public static BarItemVisibility getBarItemVisibility(bool bVisible)
        {
            if (bVisible)
            {
                return BarItemVisibility.Always;
            }
            else
            {
                return BarItemVisibility.Never;
            }
        }
        /// <summary>
        /// 删除表格行
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="entity">删除行</param>
        /// <param name="gridControl"></param>
        public static void deleteRowInGridView<TEntity>(TEntity entity, GridControl gridControl)
        {
            List<TEntity> entitys = (List<TEntity>)gridControl.DataSource;
            if (entitys != null)
            {
                entitys.Remove(entity);
            }
        }

        /// <summary>
        /// 增加表格行
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity">增加行</param>
        /// <param name="gridControl"></param>
        public static void addRowInGridView<TEntity>(TEntity entity, GridControl gridControl)
        {
            List<TEntity> entitys = (List<TEntity>)gridControl.DataSource;
            if (entitys != null)
            {
                entitys.Add(entity);
            }
        }

        /// <summary>
        /// 获取控件值
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static object getControlData(Control control)
        {
            switch (control.GetType().Name)
            {
                case "TextEdit":
                    TextEdit textEdit = (TextEdit)control;
                    return textEdit.Text;
                    break;
                case "CheckEdit":
                    CheckEdit checkEdit = (CheckEdit)control;
                    return checkEdit.EditValue;
                    break;
                case "MemoEdit":
                    MemoEdit memoEdit = (MemoEdit)control;
                    return memoEdit.Text;
                    break;
                case "CalcEdit":
                    CalcEdit calcEdit = (CalcEdit)control;
                    return calcEdit.EditValue;
                    break;
                case "DateEdit":
                    DateEdit dateEdit = (DateEdit)control;
                    return dateEdit.DateTime.ToString("yyyy-MM-dd hh:mm:ss.fff");
                    break;
                case "GridLookUpEdit":
                    GridLookUpEdit gridLookUpEdit = (GridLookUpEdit)control;
                    return gridLookUpEdit.EditValue;
                    break;
                case "TreeListLookUpEdit":
                    TreeListLookUpEdit treeListLookUpEdit = (TreeListLookUpEdit)control;
                    return treeListLookUpEdit.EditValue;
                    break;
                case "GridControl":
                    GridControl gridControl = (GridControl)control;
                    return gridControl.DataSource;
                    break;
                case "PictureEdit":
                    PictureEdit pictureEdit = (PictureEdit)control;
                    return pictureEdit.Image;
                    break;
            }
            return null;
        }

        /// <summary>
        /// 设置控件值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="control"></param>
        public static void setContorlData(object value, Control control )
        {
            switch (control.GetType().FullName)
            {
                case "DevExpress.XtraEditors.DateEdit":
                    DateEdit dateEdit = (DateEdit)control;
                    if (value != null && value.GetType().Name == "String")
                    {
                        dateEdit.DateTime = Convert.ToDateTime(value);
                    }
                    else
                    {
                        dateEdit.DateTime = value == null ? new DateTime() : (DateTime)value;
                        //c.DataBindings.Add("DateTime", voucherDataModel, sTag.Substring(sTag.IndexOf(".") + 1));
                    }
                    break;
                case "DevExpress.XtraEditors.TreeListLookUpEdit":
                    TreeListLookUpEdit treeListLookUpEdit = (TreeListLookUpEdit)control;
                    DataTable dt = treeListLookUpEdit.Properties.DataSource as DataTable;
                    if (dt == null || value == null || value == DBNull.Value) return;
                    treeListLookUpEdit.EditValue = Convert.ChangeType(value, dt.Columns[treeListLookUpEdit.Properties.ValueMember].DataType);
                    //treeListLookUpEdit.RefreshEditValue();
                    //treeListLookUpEdit.Refresh();
                    //treeListLookUpEdit.Text = "as";
                    break;
                case "DevExpress.XtraGrid.GridControl":
                    DevExpress.XtraGrid.GridControl gridControl = (DevExpress.XtraGrid.GridControl)control;
                    gridControl.DataSource = value;
                    break;
                case "DevExpress.XtraEditors.CheckEdit":
                    CheckEdit checkEdit = (CheckEdit)control;
                    checkEdit.EditValue = value;
                    break;
                case "DevExpress.XtraEditors.PictureEdit":
                    PictureEdit pictureEdit = (PictureEdit)control;
                    pictureEdit.Image = value as Image;
                    break;
                default:
                    control.Text = Convert.ToString(value);
                    break;
            }
        }

        /// <summary>
        /// 设置控件只读状态
        /// </summary>
        /// <param name="value"></param>
        /// <param name="control"></param>
        public static void setContorlReadOnly(bool value, Control control)
        {
            switch (control.GetType().FullName)
            {
                case "DateEdit":
                    DateEdit dateEdit = (DateEdit)control;
                    dateEdit.Properties.ReadOnly = value;
                    break;
                default:
                    TextEdit textEdit = (TextEdit)control;
                    textEdit.Properties.ReadOnly = value;
                    break;
            }
        }



        /// <summary>
        /// 获取表格控件选择所有行
        /// </summary>
        /// <param name="sSign">勾选的字段名</param>
        /// <param name="gridView"></param>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        public static List<TEntity> getSelectDatas<TEntity>( GridView gridView) where TEntity : class
        {
            List<TEntity> drs = new List<TEntity>();
            gridView.CloseEditor();

            int[] selectRows = gridView.GetSelectedRows();

            foreach (int iRow in selectRows)
            {
                drs.Add(gridView.GetRow(iRow) as TEntity);
            }

            return drs;
        }


        /// <summary>
        /// 获取表格控件选择所有行
        /// </summary>
        /// <param name="sSign">勾选的字段名</param>
        /// <param name="gridView"></param>
        /// <param name="gridControl"></param>
        /// <returns></returns>
        public static List<DataRow> getSelectDataRows(  GridView gridView, GridControl gridControl)
        {
            List<DataRow> drs = new List<DataRow>();
            gridView.CloseEditor();
            gridControl.Focus();

            DataTable dt = gridControl.DataSource as DataTable;
            int[] selectRows = gridView.GetSelectedRows();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (int iRow in selectRows)
                {
                    drs.Add(gridView.GetDataRow(iRow));
                }
            }

            return drs;
        }
    }
}
