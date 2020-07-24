using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;

using LiModel.Form;
using LiHttp.Enum;
using LiContexts;
using LiCommon.Util;
using LiModel.LiEnum;
using LiFlow.Model;
using LiControl.Form;
using LiControl.Util;
using LiModel.Util;
using LiModel.Dev.GridlookUpEdit;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

using LiHttp.GetEntity;
using LiModel.Basic;
using LiModel.LiTable;
using System.Text.RegularExpressions;
using LiForm.Dev.Util;
using DevExpress.XtraEditors.Repository;
using LiCommon.LiEnum;
using LiModel.LiReport;
using LiHttp;
using LiModel.LiModelFactory;

namespace LiManage
{
    public partial class LiReportDesignForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 状态

        /// <summary>
        /// 表单的状态
        /// </summary>
        public string formStatusCode { set; get; }

        public void setNewStatus()
        {
            formStatusCode = "NewStatus";
            setFormStatus(formStatusCode);
        }

        public void setEditStatus()
        {
            formStatusCode = "EditStatus";
            setFormStatus(formStatusCode);
        }

        public void setShowStatus()
        {
            formStatusCode = "ShowStatus";
            setFormStatus(formStatusCode);
        }

        /// <summary>
        /// /设置单据状态
        /// </summary>
        /// <param name="statusCode"></param>
        private void setFormStatus(string statusCode)
        {
            StatusModel statusModel = voucherStatusModel.dataStatuss.Where(m => m.code == statusCode).FirstOrDefault();
            if (statusModel != null)
            {
                foreach (ControlStatusModel controlStatusModel in statusModel.dataControlStatuss)
                {
                    if (buttonDict.ContainsKey(controlStatusModel.code))
                    {
                        BarButtonItem button = buttonDict[controlStatusModel.code];
                        button.Visibility = DevControlUtil.getBarItemVisibility(controlStatusModel.bVisibe);
                        button.Enabled = !controlStatusModel.bReadOnly;
                    }
                    if (controlDict.ContainsKey(controlStatusModel.code))
                    {
                        Control control = controlDict[controlStatusModel.code];
                        control.Visible = controlStatusModel.bVisibe;
                        control.Enabled = !controlStatusModel.bReadOnly;
                    }
                    if (gridColumnDict.ContainsKey(controlStatusModel.code))
                    {
                        GridColumn gridColumn = gridColumnDict[controlStatusModel.code];
                        gridColumn.Visible = controlStatusModel.bVisibe;
                        gridColumn.OptionsColumn.AllowEdit = !controlStatusModel.bReadOnly;
                    }
                }
            }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        /// <summary>
        /// 单据控件状态，用于更新
        /// </summary>
        private List<ControlStatusModel> newControlStatusModels = new List<ControlStatusModel>();
        #endregion

        #region 表单控件信息
        /// <summary>
        /// 单据上的按钮
        /// </summary>
        Dictionary<string, BarButtonItem> buttonDict = new Dictionary<string, DevExpress.XtraBars.BarButtonItem>();

        /// <summary>
        /// 单据上的表头控件
        /// </summary>
        Dictionary<string, Control> controlDict = new Dictionary<string, Control>();

        /// <summary>
        /// 单据上的表头控件标题
        /// </summary>
        Dictionary<string, LayoutControlItem> layoutControlItemDict = new Dictionary<string, LayoutControlItem>();

        /// <summary>
        /// 表格控件
        /// </summary>
        private Dictionary<string, GridControl> gridControlDict = new Dictionary<string, GridControl>();

        /// <summary>
        /// 表格控件视图
        /// </summary>
        private Dictionary<string, GridView> gridViewDict = new Dictionary<string, GridView>();

        /// <summary>
        /// 表体列内容
        /// </summary>
        Dictionary<string, GridColumn> gridColumnDict = new Dictionary<string, GridColumn>();

        #endregion

        #region 引用数据源
        private SystemInfoModel systemInfoModel = new SystemInfoModel();
        private ControlTypeModel controlTypeModel = new ControlTypeModel();
        private DictGroupModel dictGroupModel = new DictGroupModel();
        private TableModel tableModel = new TableModel();
        private ColumnModel columnModel = new ColumnModel();
        private OrderBy orderBy = new OrderBy();
        private GridlookUpEditShowModeModel gridlookUpEditShowModeModel = new GridlookUpEditShowModeModel();
        #endregion

        /// <summary>
        /// 报表参数
        /// </summary>
        private List<Dictionary<string, object>> paramMaps = new List<Dictionary<string, object>>();

        public Dictionary<string, GridlookUpEditShowModel> refControls;
        public Dictionary<string, DataTable> refDatas;
        /// <summary>
        /// 数据
        /// </summary>
        LiReportModel formData;

        private string formID = "LiReportDesignForm";

        private LiManageForm2 manageForm;
        /// <summary>
        /// 所有字段字典
        /// </summary>
        List<GridlookUpEditModel> basicInfoControls = new List<GridlookUpEditModel>();
        public LiReportDesignForm(LiReportModel formData, LiManageForm2 manageForm)
        {
            InitializeComponent();
            this.formData = formData;
            this.manageForm = manageForm;
            Init();
        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {
            systemInfoModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.SystemInfo).getEntityList<SystemInfoModel>());
            tableModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>("Basic", "entityType"));          
            dictGroupModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).getEntityList<DictGroupModel>());

            //读取Form上的控件
            DevFormUtil.getControlInForm(formID + ".", layoutControlItemDict, controlDict, this);
            DevFormUtil.getGridColumnInForm(formID + ".", gridColumnDict, this);
            DevFormUtil.getBarButtonItemInForm(formID + ".", buttonDict, this);
            DevFormUtil.getGridControlInForm(formID + ".", gridControlDict, gridViewDict, this);

            //获取单据状态
            List<VoucherStatusModel> voucherStatusModels = LiSetReadOnlyForm.getEntitys(this.Name);
            if (voucherStatusModels != null && voucherStatusModels.Count > 0)
            {
                voucherStatusModel = voucherStatusModels[0];
            }
            else
            {
                voucherStatusModel = new VoucherStatusModel();
                voucherStatusModel.code = this.Name;
                voucherStatusModel.name = this.Text;
                voucherStatusModel.dataStatuss = new List<StatusModel>();
            }
            setNewStatus();
            setReportSqlParam();

            refControls = LiContexts.LiContext.getHttpEntity(LiEntityKey.SysDatabases).getRefControls<GridlookUpEditShowModel>();
            refDatas = LiContexts.LiContext.getHttpEntity(LiEntityKey.SysDatabases).getRefControlDatas();
        }

        public void InitControl()
        {
            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.VALUE, systemInfoModel.getValueMember(), systemInfoModel.getDisplayMember(), systemInfoModel.getSearchColumns(), systemInfoModel.getDisplayColumns(), systemInfoModel.getDictModelDesc(), gridLookUpEdit_systemCode, this, systemInfoModel.getDataSource<List<SystemInfoModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoKey, this, tableModel.getDataSource<List<TableModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(controlTypeModel.getValueMember(), controlTypeModel.getDisplayMember(), controlTypeModel.getSearchColumns(), controlTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_controlType, this, controlTypeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, dictGroupModel.getValueMember(), dictGroupModel.getDisplayMember(), dictGroupModel.getSearchColumns(), dictGroupModel.getDisplayColumns(), dictGroupModel.getDictModelDesc(), repositoryItemGridLookUpEdit_dictInfoType, this, dictGroupModel.getDataSource<List<DictGroupModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoShowFieldName, this, columnModel.getDataSource<List<ColumnModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(gridlookUpEditShowModeModel.getValueMember(), gridlookUpEditShowModeModel.getDisplayMember(), gridlookUpEditShowModeModel.getSearchColumns(), gridlookUpEditShowModeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_basicInfoShowMode, this, gridlookUpEditShowModeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(orderBy.getValueMember(), orderBy.getDisplayMember(), orderBy.getSearchColumns(), orderBy.getDisplayColumns(), repositoryItemGridLookUpEdit_orderBy, this, orderBy.getDataSource());

            GridlookUpEditShowModel gridlookUpEditShowModelComboBox_ComboBox = refControls["sysDatabases"];
            DataTable liComboBoxData = refDatas["sysDatabases"];
            GridlookUpEditUtil.InitDefaultComboBoxControl("name", "name", gridlookUpEditShowModelComboBox_ComboBox.searchColumns, gridlookUpEditShowModelComboBox_ComboBox.displayColumns, gridLookUpEdit_dataBaseName, this, liComboBoxData);

            InitMenu();
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <returns></returns>
        private void InitMenu()
        {
            this.treeListLookUpEdit_menuCode.Properties.ValueMember = "ID";
            this.treeListLookUpEdit_menuCode.Properties.DisplayMember = "Name";

            this.treeListLookUpEdit_menuCode.Properties.TreeList.KeyFieldName = "ID";

            this.treeListLookUpEdit_menuCode.Properties.TreeList.ParentFieldName = "ParentID";
            this.treeListLookUpEdit_menuCode.Properties.DataSource = manageForm.listTreeData;

            var tree = this.treeListLookUpEdit1TreeList;
            tree.Columns.Clear();
            tree.Columns.Add(new TreeListColumn() { Caption = "名称", FieldName = "Name", Name = "treeListLookUpEdit1TreeList1", Visible = true, VisibleIndex = 0 });

            //this.treeListLookUpEdit_menuCode.EditValue = manageForm.currentTreeDataModel.ID;
            ////设置树的图标集合及逐级图标
            //tree.SelectImageList = this.imageList1;//要有值，才能触发事件
            //tree.CustomDrawNodeImages += (object sender, CustomDrawNodeImagesEventArgs e) =>
            //{
            //    e.SelectImageIndex = 1;
            //};
        }

        /// <summary>
        /// 设置报表参数
        /// </summary>
        private void setReportSqlParam()
        {
            Dictionary<string, object> paramMapDict = new Dictionary<string, object>();
            paramMapDict.Add("paramName", "whereSql");
            paramMapDict.Add("paramType", "nvarchar(max)");
            paramMaps.Add(paramMapDict);
            paramMapDict = new Dictionary<string, object>();
            paramMapDict.Add("paramName", "orderBySql");
            paramMapDict.Add("paramType", "nvarchar(max)");
            paramMaps.Add(paramMapDict); paramMapDict = new Dictionary<string, object>();
            paramMapDict.Add("paramName", "rangeSql");
            paramMapDict.Add("paramType", "nvarchar(max)");
            paramMaps.Add(paramMapDict);

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            try
            {

                foreach (KeyValuePair<string, Control> kvp in controlDict)
                {
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<LiReportModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
                {
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<LiReportModel>(kvp.Key, formData), kvp.Value);
                }


            }
            catch (Exception ex)
            {
                MessageUtil.Show("加载失败：" + ex.ToString(), "系统提示");
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<LiReportModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<LiReportModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

        }


        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            formData = LiReportModel.getInstance();
            loadData();
            setNewStatus();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            setEditStatus();
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            getData();
            handleGridlookUpEditShowModel();
            if (formData.id > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.LiReport).saveEntity(true, formData);
            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.LiReport).saveEntity(false, formData);
            }

            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.LiReport).tipStr, "温馨提示");

        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnStatus_ItemClick(object sender, ItemClickEventArgs e)
        {

            //设置新的控件状态
            foreach (KeyValuePair<string, GridColumn> kvp in gridColumnDict)
            {
                if (newControlStatusModels.Where(m => m.code == kvp.Key).FirstOrDefault() == null)
                {
                    newControlStatusModels.Add(new ControlStatusModel() { code = kvp.Key, name = kvp.Value.Caption, groupName = "表体", bReadOnly = false, bVisibe = true });
                }
            }
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                if (newControlStatusModels.Where(m => m.code == kvp.Key).FirstOrDefault() == null)
                {
                    LayoutControlItem layoutControlItem = layoutControlItemDict[kvp.Key];
                    newControlStatusModels.Add(new ControlStatusModel() { code = kvp.Key, name = layoutControlItem.Text, groupName = "表头", bReadOnly = false, bVisibe = true });
                }
            }
            foreach (KeyValuePair<string, BarButtonItem> kvp in buttonDict)
            {
                if (newControlStatusModels.Where(m => m.code == kvp.Key).FirstOrDefault() == null)
                {
                    newControlStatusModels.Add(new ControlStatusModel() { code = kvp.Key, name = kvp.Value.Caption, groupName = "按钮", bReadOnly = false, bVisibe = true });
                }
            }

            LiSetReadOnlyForm form = new LiSetReadOnlyForm(newControlStatusModels, null, null, null, voucherStatusModel);
            form.Show();
        }

        private void LiReportDesignForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAddRowWhere_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.AddRow<LiReportFieldModel>(gridControl1);
        }

        private void btnInsertRowWhere_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.InsertRow<LiReportFieldModel>(gridControl1, gridView1);
        }

        private void btnDeleteRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.DeleteRow<LiReportFieldModel>(gridControl1, gridView1);
        }

        private void BtnGetTableInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(memoEdit_reportSql.Text))
            {
                MessageUtil.ShowByWarmTip("报表SQL为空！");
                return;
            }
            Dictionary<string, object> paramValues = new Dictionary<string, object>();
            paramValues.Add("whereSql", "");
            paramValues.Add("orderBySql", "");
            paramValues.Add("rangeSql", " and iPageRow > 0 and iPageRow <= 1");

            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            paramDict.Add("dataBaseName", gridLookUpEdit_dataBaseName.EditValue);
            paramDict.Add("procedureName", memoEdit_reportSql.EditValue);
            paramDict.Add("paramMaps", paramMaps);
            paramDict.Add("paramValues", paramValues);

            DataTable dt = LiContexts.LiContext.getHttpEntity(LiEntityKey.sp_Currency).execProcedureByMap_DataTable(paramDict);

            if(dt == null || dt.Rows.Count <= 0)
            {
                MessageUtil.ShowByWarmTip("查询内容为空！");
                return;
            }

            List<LiReportFieldModel> listColumns;
            if (gridControl1.DataSource == null)
            {
                gridControl1.DataSource = new List<LiReportFieldModel>();
            }
            listColumns = (List<LiReportFieldModel>)gridControl1.DataSource;

            List<LiReportFieldModel> deleteLiReportFields = new List<LiReportFieldModel>();

            foreach(LiReportFieldModel liReportField in listColumns)
            {
                if (!dt.Columns.Contains(liReportField.columnName))
                {
                    deleteLiReportFields.Add(liReportField);
                }
            }

            foreach(LiReportFieldModel liReportField in deleteLiReportFields)
            {
                listColumns.Remove(liReportField);
            }

            int iIndex = listColumns.Count == 0 ? 0 :listColumns.Max(m=>m.iColumnIndex);
            foreach(DataColumn dc in dt.Columns)
            {
                LiReportFieldModel liReportField = listColumns.Where(m => m.columnName == dc.ColumnName).FirstOrDefault();
                if (liReportField != null) continue;

                LiReportFieldModel column = LiReportFieldModel.getInstance();
                column.columnName = dc.ColumnName;
                column.columnType = dc.DataType.FullName;
                column.iColumnIndex = ++iIndex;
                column.reportId = formData.id;

                listColumns.Add(column);
            }

            gridControl1.DataSource = listColumns;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }


        private void BtnGetOutInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            RegexOptions ops = RegexOptions.Multiline;
            Regex r = new Regex(@"\(\S*\)", ops);

            string filename = "";
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
                filename = openFileDialog1.FileName;
                if (filename == null || filename == "")
                {
                    MessageBox.Show("请选择导入文件！", "用友提示");
                    return;
                }
                DataTable dt = ExcelUtil.ExcelToDataTable(filename, "Sheet1", true);
                if (dt != null)
                {
                    List<LiReportFieldModel> listColumns = (List<LiReportFieldModel>)gridControl1.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string columnName = r.Replace(Convert.ToString(dr["ColumnName"]).Trim(), "").Replace(" ", "");
                        LiReportFieldModel LiReportFieldModel = listColumns.Where(m => m.columnName == columnName).FirstOrDefault();

                        if (LiReportFieldModel != null)
                        {
                            //LiReportFieldModel.columnAbbName = r.Replace(Convert.ToString(dr["Description"]), "").Replace(" ", "");
                        }
                    }
                    gridControl1.RefreshDataSource();
                }
            }
        }

        private void BtnButtonAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiReportButtonModel liReportButtonModel = LiReportButtonModel.getInstance(formData.id, formData.buttons.Count <= 0 ? Guid.NewGuid().ToString() : formData.buttons[0].categoryGuid);
            DevControlUtil.addRowInGridView<LiReportButtonModel>(liReportButtonModel, gridControl2);
            gridView2.RefreshData();
        }

        private void BtnButtonDelete_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            LiReportButtonModel deleteValue = gridView2.GetFocusedRow() as LiReportButtonModel;
            DevControlUtil.deleteRowInGridView<LiReportButtonModel>((LiReportButtonModel)deleteValue, gridControl2);
            gridView2.RefreshData();
        }

        private void BtngAddGneralEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiReportButtonModel buttonModel = gridView2.GetFocusedRow() as LiReportButtonModel;
            if (buttonModel == null) return;

            LiGeneralEventForm frm = new LiGeneralEventForm();
            if (DialogResult.Yes == frm.ShowDialog())
            {
                List<GeneralEventModel> drs = frm.SelectRows;

                LiReportEventModel eventModel;
                foreach (GeneralEventModel model in drs)
                {

                    eventModel = LiReportEventModel.getInstance(buttonModel.id);
                    eventModel.assemblyName = model.eventAssemblyName;
                    eventModel.fullName = model.eventFullName;

                    DevControlUtil.addRowInGridView<LiReportEventModel>(eventModel, gridControl3);
                }

                gridControl3.RefreshDataSource();
            }
        }

        private void BtnAddPluginEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiReportButtonModel buttonModel = gridView2.GetFocusedRow() as LiReportButtonModel;
            if (buttonModel == null) return;

            string filename = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "DLL文件|*.dll|所有文件|*.*";
            openFileDialog1.Title = "请选择Excel文件";
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.DefaultExt = "所有文件|*.*";
            openFileDialog1.DefaultExt = "*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LiPlugInForm frm = new LiPlugInForm(openFileDialog1.FileName);
                if (DialogResult.Yes == frm.ShowDialog())
                {
                    List<DataRow> drs = frm.SelectRows;

                    LiReportEventModel eventModel;
                    foreach (DataRow dr in drs)
                    {
                        eventModel = LiReportEventModel.getInstance(  buttonModel.id );
                        eventModel.assemblyName = Convert.ToString(dr["eventAssemblyName"]);
                        eventModel.fullName = Convert.ToString(dr["eventFullName"]);

                        DevControlUtil.addRowInGridView<LiReportEventModel>(eventModel, gridControl3);
                    }

                }
                gridControl3.RefreshDataSource();
            }
        }

        private void BtnDeleteEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiReportEventModel eventModel = gridView3.GetFocusedRow() as LiReportEventModel;
            if (eventModel == null) return;

            DevControlUtil.deleteRowInGridView<LiReportEventModel>(eventModel, gridControl3);
            gridView3.RefreshData();
        }

        private void GridView2_Click(object sender, EventArgs e)
        {
            LiReportButtonModel buttonModel = (LiReportButtonModel)gridView2.GetFocusedRow();

            if (buttonModel == null) return;

            gridControl3.DataSource = null;

            gridControl3.DataSource = buttonModel.events;
            gridView3.BestFitColumns();
            gridView3.RefreshData();
        }

        private void addButton(string reportButtonType)
        {
            LiReportButtonModel liReportButton = ButtonFactory.getReportButtonModel(reportButtonType, formData.id, formData.buttons.Count <= 0 ? Guid.NewGuid().ToString() : formData.buttons[0].categoryGuid);
            DevControlUtil.addRowInGridView<LiReportButtonModel>(liReportButton, gridControl2);
            gridView2.RefreshData();
        }

        private void BtnAddButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(Convert.ToString(e.Item.Tag));
        }

        private void BtnRelease_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void handleGridlookUpEditShowModel()
        {
            foreach (LiReportFieldModel liReportFieldModel in formData.datas)
            {
                switch (liReportFieldModel.controlType)
                {
                    case "UserEdit":
                    case "GridLookUpEditRef":
                        TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == liReportFieldModel.basicInfoKey).FirstOrDefault();

                        liReportFieldModel.gridlookUpEditShowModelJson = DevControlUtil.getGridLookUpEditRefInfo(tableModelTemp, liReportFieldModel.basicInfoShowMode, liReportFieldModel.basicInfoTableKey, liReportFieldModel.basicInfoShowFieldName);
                        break;
                    case "GridLookUpEditComboBox":
                        liReportFieldModel.gridlookUpEditShowModelJson = DevControlUtil.getGridLookUpEditDictInfo();
                        break;
                }
            }
        }
        private void GridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            LiReportFieldModel liReportFieldModel = gridView1.GetFocusedRow() as LiReportFieldModel;


            switch (e.Column.FieldName)
            {
                case "basicInfoKey":
                    TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == Convert.ToString(e.Value)).FirstOrDefault();
                    if (tableModelTemp != null)
                    {
                        liReportFieldModel.basicInfoTableKey = tableModelTemp.keyName;
                    }
                    else
                    {
                        liReportFieldModel.basicInfoTableKey = string.Empty;
                    }
                    break;
            }

        }

        private void RepositoryItemGridLookUpEdit_basicInfoShowFieldName_BeforePopup(object sender, EventArgs e)
        {
            LiReportFieldModel liReportFieldModel = gridView1.GetFocusedRow() as LiReportFieldModel;
            TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == liReportFieldModel.basicInfoKey).FirstOrDefault();
            if (tableModelTemp != null)
            {
                repositoryItemGridLookUpEdit_basicInfoShowFieldName.DataSource = tableModelTemp.datas;
            }
            else
            {
                repositoryItemGridLookUpEdit_basicInfoShowFieldName.DataSource = columnModel.getDataSource<List<ColumnModel>>();
            }
        }
    }
}