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
using LiModel.LiConvert;
using LiU8API.Model;
using LiHttp.RequestParam;

namespace LiManage
{
    public partial class LiManageConvertForm2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        bool bInitData = true;

        List<TableModel> tableAllModelList;
        List<TableModel> tableModelList;
        List<TableModel> tableBasicInfoModelList;
        List<LiU8VoucherModel> u8VoucherModels;

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
        private LiU8FieldModel liU8FieldModel = new LiU8FieldModel();
        private ConvertDestTypeModel convertDestTypeModel = new ConvertDestTypeModel();
        private ConvertSourceTypeModel convertSourceTypeModel = new ConvertSourceTypeModel();
        private ConvertRelation convertRelation = new ConvertRelation();
        private TableModel tableModel = new TableModel();
        private LiU8VoucherModel liU8VoucherModel = new LiU8VoucherModel();
        private ColumnModel columnModel = new ColumnModel();
        private JudgmentSymbolModel judgmentSymbolModel = new JudgmentSymbolModel();
        private ControlTypeModel controlTypeModel = new ControlTypeModel();
        #endregion

        /// <summary>
        /// 
        /// 数据
        /// </summary>
        LiConvertHeadModel formData;

        private string formID = "LiManageConvertForm";

        public LiManageConvertForm2(LiConvertHeadModel formData)
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

            //获取所有基础表信息
            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityOrder", "master", "systemCode", LiContext.SystemCode);
            tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

            tableAllModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>();

            paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityType", "Basic", "systemCode", LiContext.SystemCode);
            tableBasicInfoModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

            u8VoucherModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntityList<LiU8VoucherModel>();

        }

        public void InitControl()
        {
            GridlookUpEditUtil.InitDefaultComboBoxControl(convertDestTypeModel.getValueMember(), convertDestTypeModel.getDisplayMember(), convertDestTypeModel.getSearchColumns(), convertDestTypeModel.getDisplayColumns(), gridLookUpEdit_convertDestType, this, convertDestTypeModel.getDataSource());

            GridlookUpEditUtil.InitDefaultComboBoxControl(convertSourceTypeModel.getValueMember(), convertSourceTypeModel.getDisplayMember(), convertSourceTypeModel.getSearchColumns(), convertSourceTypeModel.getDisplayColumns(), gridLookUpEdit_convertSourceType, this, convertSourceTypeModel.getDataSource());

            GridlookUpEditUtil.InitDefaultComboBoxControl(convertRelation.getValueMember(), convertRelation.getDisplayMember(), convertRelation.getSearchColumns(), convertRelation.getDisplayColumns(), gridLookUpEdit_convertRelation, this, convertRelation.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_refBasicInfoType, this, tableBasicInfoModelList);

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(judgmentSymbolModel.getValueMember(), judgmentSymbolModel.getDisplayMember(), judgmentSymbolModel.getSearchColumns(), judgmentSymbolModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sJudgeSymbol, this, judgmentSymbolModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(controlTypeModel.getValueMember(), controlTypeModel.getDisplayMember(), controlTypeModel.getSearchColumns(), controlTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_controlType, this, controlTypeModel.getDataSource());

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
                    DevControlUtil.setContorlData(ModelUtil.getModelValue<LiConvertHeadModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
                {
                    DevControlUtil.setContorlData(ModelUtil.getModelValue<LiConvertHeadModel>(kvp.Key, formData), kvp.Value);
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
                ModelUtil.setModelValue<LiConvertHeadModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                ModelUtil.setModelValue<LiConvertHeadModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

        }

        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            formData = LiConvertHeadModel.getInstance();
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
                LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).saveEntity(true, formData);
            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).saveEntity(false, formData);
            }

            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).tipStr, "温馨提示");

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

        private void LiManageConvertForm2_Load(object sender, EventArgs e)
        {
            bInitData = true;
            loadData();
            bInitData = false;
        }

        private void btnAddRowWhere_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.AddRow<LiConvertBodyModel>(gridControl1);
        }

        private void btnInsertRowWhere_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.InsertRow<LiConvertBodyModel>(gridControl1, gridView1);
        }

        private void btnDeleteRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            DevControlUtil.DeleteRow<LiConvertBodyModel>(gridControl1, gridView1);
        }


        private void GridView1_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.RowHandle < 0) return;

            switch (e.Column.FieldName)
            {
                case "basicInfoType":
                    break;
            }
        }

        private void GridLookUpEdit_convertDest_EditValueChanged(object sender, EventArgs e)
        {
            changeRepositoryItemGridLookUpEditDataSource_ConvertField(Convert.ToString(gridLookUpEdit_convertDestType.EditValue), Convert.ToString(gridLookUpEdit_convertDest.EditValue), repositoryItemGridLookUpEdit_convertDestField);

            if (!bInitData)
            {
                switch (Convert.ToString(gridLookUpEdit_convertDestType.EditValue))
                {
                    case ConvertDestTypeModel.System:
                        getLiConvertBodysByTableModel(Convert.ToString(gridLookUpEdit_convertDest.EditValue), formData.datas);
                        break;
                    case ConvertDestTypeModel.U8:
                        getLiConvertBodysByLiU8Field(Convert.ToString(gridLookUpEdit_convertDest.EditValue), formData.datas);
                        break;
                }
                gridControl1.DataSource = formData.datas;
                gridControl1.RefreshDataSource();
                //gridControl1.Refresh();
            }
        }

        private void GridLookUpEdit_convertSource_EditValueChanged(object sender, EventArgs e)
        {
            changeGridLookUpEditDataSource_ConvertField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeIDField);
            changeRepositoryItemGridLookUpEditDataSource_ConvertField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), repositoryItemGridLookUpEdit_convertSourceField);

            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertPushField);
            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeField);
            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeTextField);
            if (!bInitData)
            {
                getFieldModelByTableModel(Convert.ToString(gridLookUpEdit_convertSource.EditValue), formData.queryFields);
                gridControl2.DataSource = formData.queryFields;
                gridControl2.RefreshDataSource();
                //gridControl2.Refresh();
            }
        }

        /// <summary>
        /// 根据TableModel获取转换的表体
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="liConvertBodyModels"></param>
        private void getLiConvertBodysByTableModel(string entityKey, List<LiConvertBodyModel> liConvertBodyModels)
        {
            liConvertBodyModels.Clear();

            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == entityKey).ToList();

            foreach (TableModel tableModel in tableModelList)
            {
                foreach (ColumnModel columnModel in tableModel.datas)
                {
                    LiConvertBodyModel bodyModel = new LiConvertBodyModel();
                    bodyModel.convertDestType = tableModel.tableName;
                    bodyModel.convertDestField = columnModel.columnName;
                    bodyModel.convertDCollection = tableModel.entityColumnName;
                    liConvertBodyModels.Add(bodyModel);
                }
            }
        }

        /// <summary>
        /// 根据LiU8Field获取转换的表体
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="liConvertBodyModels"></param>
        private void getLiConvertBodysByLiU8Field(string entityKey, List<LiConvertBodyModel> liConvertBodyModels)
        {
            liConvertBodyModels.Clear();
            List<LiU8FieldModel> liU8Fields = getLiU8FieldsByLiU8Voucher(entityKey);
            foreach (LiU8FieldModel liU8Field in liU8Fields)
            {
                LiConvertBodyModel bodyModel = new LiConvertBodyModel();
                bodyModel.convertDestType = liU8Field.fieldEntityType;
                bodyModel.convertDestField = liU8Field.fieldName;
                bodyModel.convertDCollection = getBodyFieldNameByLiU8Voucher(entityKey);
                liConvertBodyModels.Add(bodyModel);
            }
        }

        /// <summary>
        /// 根据TableModel获取查询条件
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="fieldModels"></param>
        private void getFieldModelByTableModel(string entityKey, List<FieldModel> fieldModels)
        {
            List<TableModel> tableDestModelList = tableAllModelList.Where(m => m.entityKey == entityKey).ToList();

            fieldModels.Clear();
            foreach (TableModel tableModel in tableDestModelList)
            {

                foreach (ColumnModel columnModel in tableModel.datas)
                {

                    FieldModel fieldModel = new FieldModel();
                    fieldModel.code = string.Format("{0}_{1}", tableModel.tableName, columnModel.columnName);
                    fieldModel.name = columnModel.columnAbbName;
                    fieldModel.sEntityCode = tableModel.tableName;
                    fieldModel.sEntityName = tableModel.tableAbbName;
                    fieldModel.fieldName = columnModel.columnName;
                    fieldModel.columnFieldName = columnModel.columnAbbName;
                    fieldModel.iColumnWidth = 100;
                    fieldModel.bColumnDisplay = true;
                    fieldModel.bQuery = false;
                    fieldModel.bRange = false;
                    fieldModel.sColumnControlType = ControlType.TextEdit;
                    fieldModel.sRefTypeCode = "";
                    fieldModel.sJudgeSymbol = JudgmentSymbol.Like;

                    fieldModels.Add(fieldModel);
                }

            }
        }

        /// <summary>
        /// 获取U8API同步的字段集合
        /// </summary>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        private List<LiU8FieldModel> getLiU8FieldsByLiU8Voucher(string entityKey)
        {
            LiU8VoucherModel liU8VoucherModel = u8VoucherModels.Where(m => m.code == entityKey).FirstOrDefault();
            if (liU8VoucherModel != null && liU8VoucherModel.operations != null && liU8VoucherModel.operations.Count > 0)
            {
                LiU8OperationModel liU8Operation = liU8VoucherModel.operations.Where(m => m.operationCode == "New").FirstOrDefault();
                if (liU8Operation != null)
                {

                    return liU8Operation.fields;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取表体字段名
        /// </summary>
        /// <returns></returns>
        private string getBodyFieldNameByLiU8Voucher(string entityKey)
        {
            LiU8VoucherModel liU8VoucherModel = u8VoucherModels.Where(m => m.code == entityKey).FirstOrDefault();
            LiU8OperationModel liU8Operation = liU8VoucherModel.operations.Where(m => m.operationCode == "New").FirstOrDefault();
            if (liU8Operation != null)
            {
                LiU8ParamModel liU8Param = liU8Operation.paramModels.Where(m => m.paramBoType == "1").FirstOrDefault();
                if (liU8Param != null) return liU8Param.paramName;

            }

            return string.Empty;
        }

        public void changeGridLookUpEditDataSource_ConvertPushField(string convertType, string convertRelation, string convertSource, GridLookUpEdit gridlookUpEdit)
        {
            switch (convertRelation)
            {
                case ConvertRelation.PUSHCUMULATIVE:
                    switch (convertType)
                    {
                        case ConvertDestTypeModel.System:

                            List<ColumnModel> columnRelationModelList = new List<ColumnModel>();
                            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == convertSource).ToList();
                            foreach (TableModel tableModel in tableModelList)
                            {
                                columnRelationModelList.AddRange(tableModel.datas.Where(m => m.columnType == "decimal" || m.columnType == "float" || m.columnType == "int" || m.columnType == "numeric" || m.columnType == "smallint" || m.columnType == "tinyint").ToArray());
                            }

                            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), gridlookUpEdit, this, columnRelationModelList);
                            break;
                    }
                    break;
                case ConvertRelation.ONE:
                    switch (convertType)
                    {
                        case ConvertDestTypeModel.System:
                            List<ColumnModel> columnRelationModelList = new List<ColumnModel>();
                            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == convertSource).ToList();
                            foreach (TableModel tableModel in tableModelList)
                            {
                                columnRelationModelList.AddRange(tableModel.datas.Where(m => m.columnType == "nvarchar" ).ToArray());
                            }

                            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), gridlookUpEdit, this, columnRelationModelList);
                            break;
                    }
                    break;

            }
        }

        public void changeGridLookUpEditDataSource_ConvertType(string convertType, GridLookUpEdit gridlookUpEdit)
        {
            switch (convertType)
            {
                case ConvertDestTypeModel.System:
                    GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), gridlookUpEdit, this, tableModelList);
                    break;
                case ConvertDestTypeModel.U8:
                    GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, liU8VoucherModel.getValueMember(), liU8VoucherModel.getDisplayMember(), liU8VoucherModel.getSearchColumns(), liU8VoucherModel.getDisplayColumns(), liU8VoucherModel.getDictModelDesc(), gridlookUpEdit, this, u8VoucherModels);
                    break;
            }
            gridlookUpEdit.Refresh();
        }

        public void changeGridLookUpEditDataSource_ConvertField(string convertType, string convertName, GridLookUpEdit gridLookUpEdit)
        {
            switch (convertType)
            {
                case ConvertDestTypeModel.System:
                    List<ColumnModel> columnModels = getColumnModelsByTableModel(convertName);
                    GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), gridLookUpEdit, this, columnModels);
                    break;
                case ConvertDestTypeModel.U8:
                    List<LiU8FieldModel> liU8Fields = getLiU8FieldsByLiU8Voucher(convertName);
                    GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, liU8FieldModel.getValueMember(), liU8FieldModel.getDisplayMember(), liU8FieldModel.getSearchColumns(), liU8FieldModel.getDisplayColumns(), liU8FieldModel.getDictModelDesc(), gridLookUpEdit, this, liU8Fields);
                    break;
            }
        }

        public void changeRepositoryItemGridLookUpEditDataSource_ConvertField(string convertType, string convertName, RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit)
        {
            switch (convertType)
            {
                case ConvertDestTypeModel.System:
                    List<ColumnModel> columnModels = getColumnModelsByTableModel(convertName);
                    GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit, this, columnModels);
                    break;
                case ConvertDestTypeModel.U8:
                    List<LiU8FieldModel> liU8Fields = getLiU8FieldsByLiU8Voucher(convertName);
                    GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, liU8FieldModel.getValueMember(), liU8FieldModel.getDisplayMember(), liU8FieldModel.getSearchColumns(), liU8FieldModel.getDisplayColumns(), liU8FieldModel.getDictModelDesc(), repositoryItemGridLookUpEdit, this, liU8Fields);
                    break;
            }
        }
        private void GridLookUpEdit_convertDestType_EditValueChanged(object sender, EventArgs e)
        {
            //改变表头
            changeGridLookUpEditDataSource_ConvertType(Convert.ToString(gridLookUpEdit_convertDestType.EditValue), gridLookUpEdit_convertDest);

            //改变表体
            changeRepositoryItemGridLookUpEditDataSource_ConvertField(Convert.ToString(gridLookUpEdit_convertDestType.EditValue), Convert.ToString(gridLookUpEdit_convertDest.EditValue), repositoryItemGridLookUpEdit_convertDestField);
        }

        private void GridLookUpEdit_convertSourceType_EditValueChanged(object sender, EventArgs e)
        {
            //改变表头
            changeGridLookUpEditDataSource_ConvertType(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), gridLookUpEdit_convertSource);

            //改变表体
            changeRepositoryItemGridLookUpEditDataSource_ConvertField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), repositoryItemGridLookUpEdit_convertSourceField);

            changeGridLookUpEditDataSource_ConvertField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeIDField);

            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertPushField);
            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeField);
            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeTextField);
        }

        private List<ColumnModel> getColumnModelsByTableModel(string entityKey)
        {
            List<ColumnModel> columnModels = new List<ColumnModel>();

            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == entityKey).ToList();

            foreach (TableModel tableModel in tableModelList)
            {
                columnModels.AddRange(tableModel.datas.ToArray());
            }
            return columnModels;
        }

        private void GridView1_CustomRowCellEditForEditing_1(object sender, CustomRowCellEditEventArgs e)
        {

            if (e.RowHandle < 0) return;

            LiConvertBodyModel entity = gridView1.GetFocusedRow() as LiConvertBodyModel;

            switch (e.Column.FieldName)
            {
                case "refBasicInfoValueField":
                case "refBasicInfoField":
                    if (!string.IsNullOrEmpty(entity.refBasicInfoType))
                    {
                        List<ColumnModel> columnModels = getColumnModelsByTableModel(entity.refBasicInfoType);
                        e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                        GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, columnModels);
                    }
                    break;
            }
        }

        private void GridLookUpEdit_convertRelation_EditValueChanged(object sender, EventArgs e)
        {
            changeGridLookUpEditDataSource_ConvertField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeIDField);

            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertPushField);
            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeField);
            changeGridLookUpEditDataSource_ConvertPushField(Convert.ToString(gridLookUpEdit_convertSourceType.EditValue), Convert.ToString(gridLookUpEdit_convertRelation.EditValue), Convert.ToString(gridLookUpEdit_convertSource.EditValue), gridLookUpEdit_convertCumulativeTextField);
        }

        private void GridView1_CellValueChanged_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            LiConvertBodyModel entityBody = gridView1.GetFocusedRow() as LiConvertBodyModel;
            if (entityBody == null  ) return;

            switch (e.Column.FieldName)
            {
                case "reverseCodeFieldName":
                case "reverseIdFieldName":
                case "convertSourceField":
                    switch (Convert.ToString(gridLookUpEdit_convertSourceType.EditValue))
                    {
                        case ConvertSourceTypeModel.System:

                            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == Convert.ToString(gridLookUpEdit_convertSource.EditValue)).ToList();
                            foreach (TableModel tableModel in tableModelList)
                            {
                                ColumnModel columnModel = tableModel.datas.Where(m => m.columnName == Convert.ToString(e.Value)).FirstOrDefault();
                                if (columnModel != null)
                                {
                                    entityBody.convertSourceType = tableModel.tableName;
                                    break;
                                }
                            }

                            break;
                        case ConvertSourceTypeModel.U8:
                            List<LiU8FieldModel> liU8Fields = getLiU8FieldsByLiU8Voucher(Convert.ToString(gridLookUpEdit_convertSource.EditValue));
                            LiU8FieldModel liU8FieldModel = liU8Fields.Where(m => m.fieldName == Convert.ToString(e.Value)).FirstOrDefault();

                            if (liU8FieldModel != null)
                            {
                                entityBody.convertSourceType = liU8FieldModel.fieldEntityType;
                            }
                            break;
                    }
                    break;
            }
        }
    }
}