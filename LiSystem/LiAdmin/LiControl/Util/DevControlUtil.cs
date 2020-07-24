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
using LiCommon.LiEnum;
using LiModel.Basic;
using LiModel.Dev.GridlookUpEdit;
using DevExpress.XtraGrid.Views.BandedGrid;

namespace LiControl.Util
{
   /// <summary>
   /// Dev控件工具类
   /// </summary>
    public class DevControlUtil
    {
        public static GridView getGridView(GridControl gridControl,Dictionary<string, Dictionary<string, object>> gridViewInfo)
        {
            gridControl.ViewCollection.Clear();

            GridView gridView = new GridView();
            gridControl.MainView = gridView;
            gridView.GridControl = gridControl;
            gridView.Name = "gridView1";
            foreach (KeyValuePair<string, Dictionary<string, object>> keyValues in gridViewInfo)
            {
                switch (keyValues.Key)
                {
                    case "OptionsView":
                        foreach (KeyValuePair<string, object> keyValue in keyValues.Value)
                        {
                            LiCommon.Util.ModelUtil.setValue<GridOptionsView>(keyValue.Key, keyValue.Value, gridView.OptionsView);
                        }
                        break;
                }
            }

            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});
            return gridView;
        }
        public static BandedGridView getBandedGridView(GridControl gridControl, Dictionary<string, Dictionary<string, object>> gridViewInfo)
        {
            gridControl.ViewCollection.Clear();

            BandedGridView bandedGridView = new BandedGridView();
            gridControl.MainView = bandedGridView;
            bandedGridView.GridControl = gridControl;
            bandedGridView.Name = "bandedGridView1";
            foreach (KeyValuePair<string, Dictionary<string, object>> keyValues in gridViewInfo)
            {
                switch (keyValues.Key)
                {
                    case "OptionsView":
                        foreach(KeyValuePair<string, object> keyValue in keyValues.Value)
                        {
                            LiCommon.Util.ModelUtil.setValue<BandedGridOptionsView>(keyValue.Key, keyValue.Value, bandedGridView.OptionsView);
                        }
                        break;
                }
            }

            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            bandedGridView});
            return bandedGridView;
        }
        /// <summary>
        /// 生成字典控件配置信息
        /// </summary>
        /// <returns></returns>
        public static string getGridLookUpEditDictInfo()
        {
            GridlookUpEditShowModel gridlookUpEditShowComboBox = new GridlookUpEditShowModel();

            List<string> displayColumnComboBoxs = new List<string>();
            displayColumnComboBoxs.Add("dictName");

            gridlookUpEditShowComboBox.displayColumns = displayColumnComboBoxs;
            gridlookUpEditShowComboBox.searchColumns = displayColumnComboBoxs;

            gridlookUpEditShowComboBox.displayMember = "dictName";
            gridlookUpEditShowComboBox.valueMember = "dictCode";

            return JsonUtil.GetJson(gridlookUpEditShowComboBox);
        }

        /// <summary>
        /// 生成引用档案控件配置信息
        /// </summary>
        /// <param name="basicTableModel"></param>
        /// <param name="basicInfoShowMode"></param>
        /// <param name="basicInfoTableKey"></param>
        /// <param name="basicInfoShowFieldName"></param>
        /// <returns></returns>
        public static string getGridLookUpEditRefInfo(TableModel basicTableModel, string basicInfoShowMode, string basicInfoTableKey,string basicInfoShowFieldName )
        {

            List<ColumnModel> columnList = basicTableModel.datas;

            GridlookUpEditShowModel gridlookUpEditShowModel = new GridlookUpEditShowModel();
            gridlookUpEditShowModel.showMode = basicInfoShowMode;

            List<string> displayColumns = new List<string>();
            displayColumns.Add(basicInfoTableKey);
            displayColumns.Add(basicInfoShowFieldName);

            gridlookUpEditShowModel.displayColumns = displayColumns;
            gridlookUpEditShowModel.searchColumns = displayColumns;

            gridlookUpEditShowModel.displayMember = basicInfoShowFieldName;
            gridlookUpEditShowModel.valueMember = basicInfoTableKey;

            Dictionary<string, string> dictModelDesc = new Dictionary<string, string>();
            dictModelDesc.Add(basicInfoTableKey, columnList.Where(m => m.columnName == basicInfoTableKey).FirstOrDefault().columnAbbName);
            if (!dictModelDesc.ContainsKey(basicInfoShowFieldName))
            {
                dictModelDesc.Add(basicInfoShowFieldName, columnList.Where(m => m.columnName == basicInfoShowFieldName).FirstOrDefault().columnAbbName);
            }
            gridlookUpEditShowModel.dictModelDesc = dictModelDesc;

            return JsonUtil.GetJson(gridlookUpEditShowModel);
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <typeparam name="rowNoFieldName"></typeparam>
        /// <param name="gridControl"></param>
        public static void ResetRowNo(string rowNoFieldName,GridControl gridControl)
        {
            DataTable dt = gridControl.DataSource as DataTable;

            if (dt != null && !string.IsNullOrEmpty(rowNoFieldName))
            {
                int iRow = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    dr[rowNoFieldName] = iRow++;
                }
            }

            gridControl.RefreshDataSource();
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="gridView"></param>
        public static void UpDataRow(GridView gridView, GridControl gridControl)
        {
            DataTable dt = gridControl.DataSource as DataTable;

            int[] iSelectRows = gridView.GetSelectedRows();

            if (dt != null && iSelectRows != null && iSelectRows.Length > 0)
            {
                foreach (int iRow in iSelectRows)
                {
                    DataRow newdata = dt.NewRow();
                    newdata.ItemArray = dt.Rows[iRow].ItemArray;
                    dt.Rows.RemoveAt(iRow);
                    dt.Rows.InsertAt(newdata, iRow - 1);
                    dt.AcceptChanges();
                }


                foreach (int iRow in iSelectRows)
                {
                    if (iRow == 0) continue;

                    gridView.SelectRow(iRow - 1);
                }
            }

            gridControl.RefreshDataSource();
        }

        public static void DownDataRow(GridView gridView, GridControl gridControl)
        {
            DataTable dt = gridControl.DataSource as DataTable;

            int[] iSelectRows = gridView.GetSelectedRows();
            Array.Reverse(iSelectRows);
            if (dt != null && iSelectRows != null && iSelectRows.Length > 0)
            {
                foreach (int iRow in iSelectRows)
                {
                    if (iRow < dt.Rows.Count - 1)
                    {
                        DataRow newdata = dt.NewRow();
                        newdata.ItemArray = dt.Rows[iRow].ItemArray;
                        dt.Rows.RemoveAt(iRow);
                        dt.Rows.InsertAt(newdata, iRow + 1);
                        dt.AcceptChanges();
                    }
                }

                foreach (int iRow in iSelectRows)
                {
                    if (iRow == dt.Rows.Count - 1) continue;

                    gridView.SelectRow(iRow + 1);
                }
            }

            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="gridView"></param>
        public static void UpRow<TEntity>(GridView gridView) where TEntity : new()
        {

            int[] iSelectRows = gridView.GetSelectedRows();

            if (iSelectRows != null && iSelectRows.Length > 0)
            {
                var tempList = gridView.DataSource as List<TEntity>;
                foreach (int iRow in iSelectRows)
                {
                    if (iRow == 0) continue;

                    TEntity model = tempList[iRow];

                    tempList.RemoveAt(iRow);
                    gridView.UnselectRow(iRow);
                    //  tempList.Add(model);
                    tempList.Insert(iRow - 1, model);

                }


                foreach (int iRow in iSelectRows)
                {
                    if (iRow == 0) continue;

                    gridView.SelectRow(iRow - 1);
                }
            }
        }


        public static void DownRow<TEntity>(GridView gridView) where TEntity : new()
        {

            int[] iSelectRows = gridView.GetSelectedRows();
            Array.Reverse(iSelectRows);
            if (iSelectRows != null && iSelectRows.Length > 0)
            {
                var tempList = gridView.DataSource as List<TEntity>;
                foreach (int iRow in iSelectRows)
                {
                    if (iRow == tempList.Count - 1) continue;

                    TEntity model = tempList[iRow];

                    tempList.RemoveAt(iRow);
                    gridView.UnselectRow(iRow);
                    //  tempList.Add(model);
                    tempList.Insert(iRow + 1, model);


                }

                foreach (int iRow in iSelectRows)
                {
                    if (iRow == tempList.Count - 1) continue;

                    gridView.SelectRow(iRow + 1);
                }
            }
        }

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

                object value = LiModel.Util.ModelUtil.getModelValue<TEntity>(gridColumn.FieldName, entity);

                DataRow drInfo = getBasicInfoValue(basicInfoKey, basicInfoTableKey, value, liRefDataDataTable);


                List<GridColumn> gridColumnAssists = liGridColumnRefAssistDict[basicInfoTableKey];
                foreach (GridColumn gridColumnAssist in gridColumnAssists)
                {
                    if (drInfo != null)
                    {
                        LiModel.Util.ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, drInfo[basicInfoAssistFieldName], entity);
                    }
                    else
                    {
                        LiModel.Util.ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, DBNull.Value, entity);
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

                    object value = LiModel.Util.ModelUtil.getModelValue<TEntity>(gridColumn.FieldName, entity);

                    DataRow drInfo = getBasicInfoValue(controlModel, value, liRefDataDataTable);


                    List<GridColumn> gridColumnAssists = liGridColumnRefAssistDict[controlModel.basicInfoKey];
                    foreach (GridColumn gridColumnAssist in gridColumnAssists)
                    {
                        if (drInfo != null)
                        {
                            ControlModel controlAssistModel = (ControlModel)gridColumnAssist.Tag;
                            LiModel.Util.ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, drInfo[controlAssistModel.basicInfoAssistFieldName], entity);
                        }
                        else
                        {
                            LiModel.Util.ModelUtil.setModelValue<TEntity>(gridColumnAssist.FieldName, DBNull.Value, entity);
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
                            DataColumn dc = drInfo.Table.Columns[controlAssistModel.basicInfoAssistFieldName];
                            if(dc != null)
                            {
                                dr[gridColumnAssist.FieldName] = drInfo[controlAssistModel.basicInfoAssistFieldName];
                            }
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


        #region GridControl选择函数
        /// <summary>
        /// GridControl所有行全选

        /// </summary>
        /// <param name="field">选择列的字段名</param>
        /// <param name="dt">GridControl的Table</param>
        /// <param name="flag">是否选择，True为选择，False为不选择</param>
        /// <param name="gridcontrol">所操作的GridControl</param>
        /// <param name="gridview">所操作的GridView</param>
        public static void CheckAll(string field, DataTable dt, bool flag, GridControl gridcontrol, GridView gridview)
        {
            if (!string.IsNullOrEmpty(field))
            {

                if (dt == null)
                    return;

                gridview.CloseEditor();

                //gridview.SelectAll();
                ////int[] rows = gridview.GetSelectedRows();
                ////foreach (int i in rows)
                ////{
                ////    DataRow dr = gridview.GetDataRow(i);
                ////    gridview.UnselectRow(i);
                ////}
                for (int i = 0; i < gridview.DataRowCount; i++)
                {
                    DataRow dr = gridview.GetDataRow(i);
                    dr[field] = flag;
                }

                gridview.RefreshData();
                //gridcontrol.RefreshDataSource();
            }
            else
            {
                if (flag)
                {
                    gridview.SelectAll();
                }
                else
                {
                    for (int i = 0; i < gridview.RowCount; i++)
                    {
                        gridview.UnselectRow(i);
                    }
                }
                gridview.RefreshData();
            }
        }

        /// <summary>
        /// GridControl所有行反选

        /// </summary>
        /// <param name="field">选择列的字段名</param>
        /// <param name="dt">GridControl的Table</param>
        /// <param name="gridcontrol">所操作的GridControl</param>
        /// <param name="gridview">所操作的GridView</param>
        public static void ReCheckAll(string field, DataTable dt, GridControl gridcontrol, GridView gridview)
        {
            if (!string.IsNullOrEmpty(field))
            {

                if (dt == null)
                    return;

                gridview.CloseEditor();

                for (int i = 0; i < gridview.DataRowCount; i++)
                {
                    DataRow dr = gridview.GetDataRow(i);
                    dr[field] = !bool.Parse(dr[field].ToString());
                }

                //gridcontrol.RefreshDataSource();
                gridview.RefreshData();
            }
            else
            {
                for (int i = 0; i < gridview.RowCount; i++)
                {
                    if (gridview.IsRowSelected(i))
                    {
                        gridview.UnselectRow(i);
                    }
                    else
                    {
                        gridview.SelectRow(i);
                    }
                }
                gridview.RefreshData();

            }
        }
        #endregion

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
                case ControlType.TextEdit:
                    TextEdit textEdit = (TextEdit)control;
                    return textEdit.Text;
                case ControlType.CheckEdit:
                    CheckEdit checkEdit = (CheckEdit)control;
                    return checkEdit.EditValue == DBNull.Value || checkEdit.EditValue == null || Convert.ToBoolean(checkEdit.EditValue) == false ? false :true;
                case ControlType.MemoEdit:
                    MemoEdit memoEdit = (MemoEdit)control;
                    return memoEdit.Text;
                case ControlType.CalcEdit:
                    CalcEdit calcEdit = (CalcEdit)control;
                    return calcEdit.EditValue == DBNull.Value || calcEdit.EditValue == null || string.IsNullOrWhiteSpace(Convert.ToString(calcEdit.EditValue)) ? 0 : Convert.ToDouble(calcEdit.EditValue);
                case ControlType.DateEdit:
                    DateEdit dateEdit = (DateEdit)control;
                    //return dateEdit.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    return dateEdit.DateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                case ControlType.GridLookUpEdit:
                    GridLookUpEdit gridLookUpEdit = (GridLookUpEdit)control;
                    return gridLookUpEdit.EditValue;
                case ControlType.TreeListLookUpEdit:
                    TreeListLookUpEdit treeListLookUpEdit = (TreeListLookUpEdit)control;
                    return treeListLookUpEdit.EditValue;
                case ControlType.GridControl:
                    GridControl gridControl = (GridControl)control;
                    return gridControl.DataSource;
                case ControlType.PictureEdit:
                    PictureEdit pictureEdit = (PictureEdit)control;
                    return pictureEdit.Image;
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
                    if (treeListLookUpEdit.Properties.DataSource == null || value == null || value == DBNull.Value) return;
                    treeListLookUpEdit.EditValue = value;
                    //treeListLookUpEdit.EditValue = Convert.ChangeType(value, dt.Columns[treeListLookUpEdit.Properties.ValueMember].DataType);
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
