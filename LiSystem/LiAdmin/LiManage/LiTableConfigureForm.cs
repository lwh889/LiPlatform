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

namespace LiManage
{
    public partial class LiTableConfigureForm : DevExpress.XtraBars.Ribbon.RibbonForm
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
        private ControlTypeModel controlTypeModel = new ControlTypeModel();
        private DictGroupModel dictGroupModel = new DictGroupModel();
        private TableModel tableModel = new TableModel();
        private GridlookUpEditModel gridlookUpEditModel = new GridlookUpEditModel();
        private SystemInfoModel systemInfoModel = new SystemInfoModel();
        private ClassName className = new ClassName();
        private ColumnType columnType = new ColumnType();
        private DatabaseGeneratedType databaseGeneratedType = new DatabaseGeneratedType();
        private EntityOrder entityOrder = new EntityOrder();
        private EntityType entityType = new EntityType();
        private RelationshipType relationshipType = new RelationshipType();
        #endregion

        /// <summary>
        /// 
        /// 数据
        /// </summary>
        TableModel formData;

        private string formID = "LiTableConfigureForm";

        public Dictionary<string, GridlookUpEditShowModel> refControls;
        public Dictionary<string, DataTable> refDatas;

        /// <summary>
        /// 所有字段字典
        /// </summary>
        List<GridlookUpEditModel> basicInfoControls = new List<GridlookUpEditModel>();
        public LiTableConfigureForm(TableModel formData)
        {
            InitializeComponent();
            this.formData = formData;
            Init();
        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {
            dictGroupModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).getEntityList<DictGroupModel>());
            tableModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>("master", "entityOrder"));
            systemInfoModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.SystemInfo).getEntityList<SystemInfoModel>());

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


            refControls = LiContexts.LiContext.getHttpEntity(LiEntityKey.SysDatabases).getRefControls<GridlookUpEditShowModel>();
            refDatas = LiContexts.LiContext.getHttpEntity(LiEntityKey.SysDatabases).getRefControlDatas();
        }

        public void InitControl()
        {
            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.VALUE, systemInfoModel.getValueMember(), systemInfoModel.getDisplayMember(), systemInfoModel.getSearchColumns(), systemInfoModel.getDisplayColumns(), systemInfoModel.getDictModelDesc(), gridLookUpEdit_systemCode, this, systemInfoModel.getDataSource<List<SystemInfoModel>>());

            GridlookUpEditUtil.InitDefaultComboBoxControl(className.getValueMember(), className.getDisplayMember(), className.getSearchColumns(), className.getDisplayColumns(), gridLookUpEdit_className, this, className.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(), gridlookUpEditModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoRelationFieldName, this, basicInfoControls);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoType, this, tableModel.getDataSource<List<TableModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, dictGroupModel.getValueMember(), dictGroupModel.getDisplayMember(), dictGroupModel.getSearchColumns(), dictGroupModel.getDisplayColumns(), dictGroupModel.getDictModelDesc(), repositoryItemGridLookUpEdit_dictInfoType, this, dictGroupModel.getDataSource<List<DictGroupModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(controlTypeModel.getValueMember(), controlTypeModel.getDisplayMember(), controlTypeModel.getSearchColumns(), controlTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_controlType, this, controlTypeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(columnType.getValueMember(), columnType.getDisplayMember(), columnType.getSearchColumns(), columnType.getDisplayColumns(), repositoryItemGridLookUpEdit_columnType, this, columnType.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(databaseGeneratedType.getValueMember(), databaseGeneratedType.getDisplayMember(), databaseGeneratedType.getSearchColumns(), databaseGeneratedType.getDisplayColumns(), repositoryItemGridLookUpEdit_databaseGeneratedType, this, databaseGeneratedType.getDataSource());

            GridlookUpEditUtil.InitDefaultComboBoxControl(entityOrder.getValueMember(), entityOrder.getDisplayMember(), entityOrder.getSearchColumns(), entityOrder.getDisplayColumns(), gridLookUpEdit_entityOrder, this, entityOrder.getDataSource());

            GridlookUpEditUtil.InitDefaultComboBoxControl(entityType.getValueMember(), entityType.getDisplayMember(), entityType.getSearchColumns(), entityType.getDisplayColumns(), gridLookUpEdit_entityType, this, entityType.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(relationshipType.getValueMember(), relationshipType.getDisplayMember(), relationshipType.getSearchColumns(), relationshipType.getDisplayColumns(), repositoryItemGridLookUpEdit_relationshipType, this, relationshipType.getDataSource());

            GridlookUpEditShowModel gridlookUpEditShowModelComboBox_ComboBox = refControls["sysDatabases"];
            DataTable liComboBoxData = refDatas["sysDatabases"];
            GridlookUpEditUtil.InitDefaultComboBoxControl("name", "name", gridlookUpEditShowModelComboBox_ComboBox.searchColumns, gridlookUpEditShowModelComboBox_ComboBox.displayColumns, gridLookUpEdit_dataBaseName, this, liComboBoxData);


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
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<TableModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
                {
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<TableModel>(kvp.Key, formData), kvp.Value);
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
                LiModel.Util.ModelUtil.setModelValue<TableModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<TableModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

        }

        /// <summary>
        /// 获取当前表格引用档案控件
        /// </summary>
        /// <returns></returns>
        public List<GridlookUpEditModel> getBasicInfoFieldNames()
        {
            List<ColumnModel> columns = gridControl1.DataSource as List<ColumnModel>;
            if (columns != null)
            {
                if (columns.Count != basicInfoControls.Count)
                {
                    basicInfoControls.Clear();
                    foreach (ColumnModel column in columns)
                    {
                        basicInfoControls.Add(new GridlookUpEditModel() { code = column.columnName, name = column.columnAbbName });
                    }
                }
            }
            return basicInfoControls;

        }
        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            formData = TableModel.getInstanceByBasic();
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
            if (formData.id > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).saveEntity(true, formData);
            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).saveEntity(false, formData);
            }

            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).tipStr, "温馨提示");

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

        private void LiTableConfigureForm_Load(object sender, EventArgs e)
        {
            loadData();
            getBasicInfoFieldNames();
        }

        private void btnAddRowWhere_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.AddRow<ColumnModel>(gridControl1);
        }

        private void btnInsertRowWhere_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.InsertRow<ColumnModel>(gridControl1, gridView1);
        }

        private void btnDeleteRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.DeleteRow<ColumnModel>(gridControl1, gridView1);
        }

        private void BtnGetTableInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEdit_tableName.Text))
            {
                MessageUtil.Show("表名不能为空！", "温馨提示");
                return;
            }

            if (gridLookUpEdit_dataBaseName.EditValue == null)
            {
                MessageUtil.Show("数据库名不能为空！", "温馨提示");
                return;
            }

            string resultContent;

            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            paramDict.Add("dataBaseName", gridLookUpEdit_dataBaseName.EditValue);
            paramDict.Add("tableName", textEdit_tableName.Text);
            //ProcedureParamModel paramModel = LiHttpProcedure.getProcedureParamModel("GetTableInfo", paramDict);

            DataTable dt = LiContexts.LiContext.getHttpEntity("GetTableInfo").execProcedure_DataTable(paramDict);
            if (LiContexts.LiContext.getHttpEntity("GetTableInfo").bSuccess)
            {
                //DataTable dt = DataUtil.DictionaryToTable(JsonUtil.GetDictionaryToList(resultContent));

                List<ColumnModel> listColumns;
                if (gridControl1.DataSource == null)
                {
                    gridControl1.DataSource = new List<ColumnModel>();
                }
                listColumns = (List<ColumnModel>)gridControl1.DataSource;
                listColumns.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    ColumnModel column = new ColumnModel();
                    column.columnName = Convert.ToString(dr["columnName"]);
                    column.columnType = Convert.ToString(dr["columnType"]);
                    column.length = Convert.ToInt32(dr["columnLength"]);
                    column.primaryKey = Convert.ToBoolean(dr["bPrimaryKey"]);
                    column.columnOrder = Convert.ToInt32(dr["columnOrder"]);
                    column.columnScale = Convert.ToInt32(dr["columnScale"]);
                    column.columnIsNull = Convert.ToBoolean(dr["bIsNull"]);
                    column.bDisplayColumn = false;
                    column.bSearchColumns = false;

                    listColumns.Add(column);
                }
                gridControl1.DataSource = listColumns;
                gridControl1.RefreshDataSource();

                textEdit_keyName.Text = getPrimaryKey(listColumns);
            }
        }
        public string getPrimaryKey(List<ColumnModel> listColumns)
        {
            ColumnModel columnModel = listColumns.Where(m => m.primaryKey == true).FirstOrDefault();
            if (columnModel != null)
            {
                return columnModel.columnName;
            }
            else
            {
                return string.Empty;
            }
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
                    List<ColumnModel> listColumns = (List<ColumnModel>)gridControl1.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string columnName = r.Replace(Convert.ToString(dr["ColumnName"]).Trim(), "").Replace(" ", "");
                        ColumnModel columnModel = listColumns.Where(m => m.columnName == columnName).FirstOrDefault();

                        if (columnModel != null)
                        {
                            columnModel.columnAbbName = r.Replace(Convert.ToString(dr["Description"]), "").Replace(" ", "");
                        }
                    }
                    gridControl1.RefreshDataSource();
                }
            }
        }

        private void RepositoryItemGridLookUpEdit_basicInfoRelationFieldName_BeforePopup(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEdit_basicInfoRelationFieldName.DataSource = getBasicInfoFieldNames();
        }

        private void GridView1_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

            ColumnModel columnModel = gridView1.GetFocusedRow() as ColumnModel;
            TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == Convert.ToString(columnModel.basicInfoType)).FirstOrDefault();
            switch (e.Column.FieldName)
            {
                case "basicInfoShowFieldName":
                    if(tableModelTemp != null)
                    {
                        e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);
                        GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, tableModelTemp.datas);
                    }
                    break;
                case "basicInfoKeyFieldName":
                    if (tableModelTemp != null)
                    {
                        e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);
                        GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, tableModelTemp.datas);
                    }
                    break;
            }
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.RowHandle < 0) return;

            switch (e.Column.FieldName)
            {
                case "basicInfoType":
                    ColumnModel columnModel = gridView1.GetFocusedRow() as ColumnModel;
                    TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == Convert.ToString(columnModel.basicInfoType)).FirstOrDefault();

                    if (tableModelTemp != null)
                    {
                        columnModel.basicInfoKeyFieldName = columnModel.primaryKeyName;
                    }
                    else
                    {
                        columnModel.basicInfoKeyFieldName = string.Empty;
                    }
                    break;
            }
        }
    }
}