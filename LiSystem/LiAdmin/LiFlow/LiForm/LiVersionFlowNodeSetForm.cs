using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;

using DevExpress.XtraBars;

using LiModel.Form;
using LiHttp.Server;
using LiHttp.RequestParam;
using LiCommon.Util;
using LiModel.LiEnum;
using LiFlow.Model;
using LiControl.Form;
using LiModel.Users;
//using LiForm.Dev.Util;
using LiModel.Basic;
using LiModel.Util;
using LiControl.Util;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

namespace LiFlow.LiForm
{
    public partial class LiVersionFlowNodeSetForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        /// <summary>
        /// 单据控件状态，用于更新
        /// </summary>
        private List<ControlStatusModel> newControlStatusModels = new List<ControlStatusModel>();

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

        /// <summary>
        /// 表格引用辅助控件列
        /// </summary>
        private Dictionary<string, List<GridColumn>> gridColumnRefAssistDict = new Dictionary<string, List<GridColumn>>();

        /// <summary>
        /// 数据
        /// </summary>
        private LiVersionFlowNodeModel formData;

        /// <summary>
        /// 单据表信息
        /// </summary>
        private TableModel tableModel;
        
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

        private string formID = "LiVersionFlowNodeSetForm";

        #region 引用数据源
        private UserModel userModel = new UserModel();
        private ColumnModel columnModel = new ColumnModel();
        private BracketsAfterModel bracketsAfterModel = new BracketsAfterModel();
        private BracketsBeforeModel bracketsBeforeModel = new BracketsBeforeModel();
        private JudgmentSymbolModel judgmentSymbolModel = new JudgmentSymbolModel();
        private JoinModel joinModel = new JoinModel();
        #endregion

        public LiVersionFlowNodeSetForm(LiVersionFlowNodeModel formData, TableModel tableModel)
        {
            InitializeComponent();

            this.formData = formData;
            this.tableModel = tableModel;

            Init();
        }

        public void Init()
        {
            InitData();
            InitControl();

        }

        public void InitControl()
        {
            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.VALUE, userModel.getValueMember(), userModel.getDisplayMember(), userModel.getSearchColumns(), userModel.getDisplayColumns(), userModel.getDictModelDesc(), repositoryItemGridLookUpEdit_userCode, this, userModel.getDataSource<DataTable>());


            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(bracketsAfterModel.getValueMember(), bracketsAfterModel.getDisplayMember(), bracketsAfterModel.getSearchColumns(), bracketsAfterModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sBracketsAfter, this, bracketsAfterModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(bracketsBeforeModel.getValueMember(), bracketsBeforeModel.getDisplayMember(), bracketsBeforeModel.getSearchColumns(), bracketsBeforeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sBracketsBefore, this, bracketsBeforeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(judgmentSymbolModel.getValueMember(), judgmentSymbolModel.getDisplayMember(), judgmentSymbolModel.getSearchColumns(), judgmentSymbolModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sJudgmentSymbol, this, judgmentSymbolModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(joinModel.getValueMember(), joinModel.getDisplayMember(), joinModel.getSearchColumns(), joinModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sJoin, this, joinModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_sFieldName, this, columnModel.getDataSource<List<ColumnModel>>());

        }

        /// <summary>
        /// /初始化数据
        /// </summary>
        private void InitData()
        {
            ///加载基础档案
            List<string> entityKeys = new List<string>();
            entityKeys.Add("liUsers");
            LiContexts.LiContext.addRefDataDataTable(entityKeys);
            userModel.setDataSource(LiContexts.LiContext.getRefDataDataTable("liUsers"));

            columnModel.setDataSource(tableModel.datas);

            //引用辅助档案
            gridColumnRefAssistDict.Add("userCode", new List<GridColumn>());
            gridColumnRefAssistDict["userCode"].Add(this.gridColumn2);
            //userModel.setDataSource(LiContexts.LiContext.getHttpEntity("User").getEntityList<UserModel>());

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
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                DevControlUtil.setContorlData(ModelUtil.getModelValue<LiVersionFlowNodeModel>(kvp.Key, formData), kvp.Value);
            }

            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                DevControlUtil.setContorlData(ModelUtil.getModelValue<LiVersionFlowNodeModel>(kvp.Key, formData), kvp.Value);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                ModelUtil.setModelValue<LiVersionFlowNodeModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }


            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                ModelUtil.setModelValue<LiVersionFlowNodeModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }
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
        /// 显示状态窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void LiVersionFlowNodeSetForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void gridView2_Click(object sender, EventArgs e)
        {
            LiVersionFlowConnectorModel entity = gridView2.GetFocusedRow() as LiVersionFlowConnectorModel;
            if (entity != null)
            {
                gridControl3.DataSource = entity.conditions;
            }
            else
            {
                gridControl3.DataSource = null;
            }

        }
    }
}