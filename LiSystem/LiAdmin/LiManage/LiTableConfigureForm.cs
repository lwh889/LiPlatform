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
            GridlookUpEditUtil.InitDefaultComboBoxControl(className.getValueMember(), className.getDisplayMember(), className.getSearchColumns(), className.getDisplayColumns(), gridLookUpEdit_className, this, className.getDataSource());

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
                    DevControlUtil.setContorlData(ModelUtil.getModelValue<TableModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
                {
                    DevControlUtil.setContorlData(ModelUtil.getModelValue<TableModel>(kvp.Key, formData), kvp.Value);
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
                ModelUtil.setModelValue<TableModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }


            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                ModelUtil.setModelValue<TableModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

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
    }
}