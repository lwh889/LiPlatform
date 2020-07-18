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
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraGrid.Columns;

using LiModel.Form;
using LiModel.Users;
using LiCommon.Util;
using LiHttp.RequestParam;
using LiForm.Dev.Util;
using LiHttp.Enum;
using LiContexts;
using LiControl.Form;
using LiControl.Util;
using LiModel.Util;


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LiModel.LiModelFactory;

namespace LiManage
{
    public partial class LiRoleForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        private RoleModel _currentRole;
        /// <summary>
        /// 当前的角色
        /// </summary>
        public RoleModel currentRole { set { _currentRole = value; } get { return _currentRole; } }

        /// <summary>
        /// 角色数据源
        /// </summary>
        private List<RoleModel> userModels;

        /// <summary>
        /// 单据控件状态，用于更新
        /// </summary>
        private List<ControlStatusModel> newControlStatusModels = new List<ControlStatusModel>();

        /// <summary>
        /// 单据表头控件
        /// </summary>
        Dictionary<string, Control> controlRole_Item;

        /// <summary>
        /// 单据上的按钮
        /// </summary>
        Dictionary<string, BarButtonItem> buttonDict = new Dictionary<string, DevExpress.XtraBars.BarButtonItem>();

        /// <summary>
        /// 单据上的表头控件标题
        /// </summary>
        Dictionary<string, LayoutControlItem> layoutControlItemDict = new Dictionary<string, LayoutControlItem>();

        /// <summary>
        /// 单据上的表头控件
        /// </summary>
        Dictionary<string, Control> controlDict = new Dictionary<string, Control>();

        /// <summary>
        /// 表体列内容
        /// </summary>
        Dictionary<string, GridColumn> gridColumnDict = new Dictionary<string, GridColumn>();

        /// <summary>
        /// 单据表头控件标题
        /// </summary>
        Dictionary<string, LayoutControlItem> layoutControlItemRole_Item;

        /// <summary>
        /// 单据标识
        /// </summary>
        string formID = "liRoles";
        /// <summary>
        /// 表单的状态
        /// </summary>
        public string formStatusCode { set; get; }

        public void setNewStatus()
        {
            formStatusCode = "NewStatus";
        }

        public void setEditStatus()
        {
            formStatusCode = "EditStatus";
        }

        public void setShowStatus()
        {
            formStatusCode = "ShowStatus";
        }

        /// <summary>
        /// 浮动窗口的流式布局
        /// </summary>
        LayoutControlGroup layoutControlGroup1;
        /// <summary>
        /// 浮动窗口的流式布局
        /// </summary>
        LayoutControl layoutControl1;

        public LiRoleForm()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            InitData();

        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            controlRole_Item = new Dictionary<string, Control>();
            layoutControlItemRole_Item = new Dictionary<string, LayoutControlItem>();

            InitDockPanel();


            //读取Form上的控件
            DevFormUtil.getControlInForm(formID + ".", layoutControlItemDict, controlDict, this);
            DevFormUtil.getGridColumnInForm(formID + ".", gridColumnDict, this);
            DevFormUtil.getBarButtonItemInForm(formID + ".", buttonDict, this);

            //获取单据状态
            List<VoucherStatusModel> voucherStatusModels = LiSetReadOnlyForm.getEntitys(this.Name);
            if (voucherStatusModels.Count > 0)
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
        /// 初始化浮动窗口
        /// </summary>
        private void InitDockPanel()
        {

            EventHandler saveEventHandler = new EventHandler(this.btnSave_Click);
            EventHandler closeEventHandler = new EventHandler(this.btnClose_Click);

            layoutControl1 = FormUtil.setDockPanelByBasicInfo(saveEventHandler, closeEventHandler, this.dockPanel1_Container);
            layoutControlGroup1 = layoutControl1.Root;

            DictGroupModel dictGroupModel = new DictGroupModel();
            RoleModel userModel = new RoleModel();

            InitControlToDockPanel(userModel, controlRole_Item, layoutControlItemRole_Item, layoutControlGroup1);
        }

        /// <summary>
        /// 初始化浮动窗口的控件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="controlDict"></param>
        /// <param name="layoutControlItemDict"></param>
        /// <param name="layoutControlGroup"></param>
        public void InitControlToDockPanel(object entity, Dictionary<string, Control> controlDict, Dictionary<string, LayoutControlItem> layoutControlItemDict, LayoutControlGroup layoutControlGroup)
        {
            List<ControlModel> controls = ControlFactory.getInstancesByModel(entity);

            List<ControlModel> lists = controls.OrderBy(m => m.row).ToList();
            foreach (ControlModel customerControlModel in lists)
            {
                LayoutControlItem layoutControlItem = layoutControlGroup.AddItem();
                layoutControlItem.Control = ControlModelUtil.getControl(customerControlModel.controltype);


                layoutControlItem.Text = customerControlModel.text;
                layoutControlItem.Name = customerControlModel.name;

                controlDict.Add(customerControlModel.name, layoutControlItem.Control);
                layoutControlItemDict.Add(customerControlModel.name, layoutControlItem);
            }

            layoutControlGroup.Clear();
        }


        /// <summary>
        /// 加载控件到浮动窗口
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="controlDict"></param>
        /// <param name="layoutControlItemDict"></param>
        /// <param name="layoutControlGroup"></param>
        private void loadControlInDockPanel<TEntity>(object entity, Dictionary<string, Control> controlDict, Dictionary<string, LayoutControlItem> layoutControlItemDict, LayoutControlGroup layoutControlGroup) where TEntity : class
        {
            layoutControlGroup.Clear();
            foreach (KeyValuePair<string, LayoutControlItem> kvp in layoutControlItemDict)
            {
                layoutControlGroup.AddItem(kvp.Value);
            }

            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<TEntity>(kvp.Key, entity), kvp.Value);
            }
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            userModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.Role).getEntityList<RoleModel>();

            gridControl1.DataSource = userModels;
        }

        /// <summary>
        /// 获取控件数据
        /// </summary>
        public void getData<TEntity>(object entity, Dictionary<string, Control> controlDict) where TEntity : class
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<TEntity>(kvp.Key, DevControlUtil.getControlData(kvp.Value), entity);
            }
        }

        /// <summary>
        /// 显示浮动窗体
        /// </summary>
        /// <param name="bShow"></param>
        private void setShowDockPanel(bool bShow)
        {
            if (bShow)
            {
                dockPanel1.Show();
            }
            else
            {
                dockPanel1.Hide();
            }
        }

        /// <summary>
        /// 保存字典
        /// </summary>
        private void saveDict()
        {
            bool bSuccess = false;

            string resultContent = string.Empty;
            string tipStr = string.Empty;

            getData<RoleModel>(currentRole, controlRole_Item);

            LiContexts.LiContext.getHttpEntity(LiEntityKey.Role).saveEntity(currentRole.id > 0, currentRole);
            //bSuccess = RoleEntity.saveEntity(currentRole.id > 0, "liRoles", currentRole, out tipStr, out resultContent);

            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.Role).tipStr, "温馨提示");

            loadData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveDict();

            loadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dockPanel1.Hide();
        }

        private void LiRoleForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            currentRole = gridView1.GetFocusedRow() as RoleModel;
        }

        private void btnAddRole_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentRole = new RoleModel();

            loadControlInDockPanel<RoleModel>(currentRole, controlRole_Item, layoutControlItemRole_Item, layoutControlGroup1);
            setShowDockPanel(true);
            setNewStatus();
        }

        private void btnEditRole_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (currentRole == null)
            {
                MessageUtil.Show("请选择角色！", "系统提示");
                return;
            }

            loadControlInDockPanel<RoleModel>(currentRole, controlRole_Item, layoutControlItemRole_Item, layoutControlGroup1);
            setShowDockPanel(true);
            setEditStatus();
        }

        private void btnDeleteRole_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool bSuccess = false;

            string resultContent = string.Empty;
            string tipStr = string.Empty;

            LiContexts.LiContext.getHttpEntity(LiEntityKey.Role).deleteEntity(currentRole);
            //bSuccess = RoleEntity.deleteEntity("liRoles", currentRole, out tipStr, out resultContent);
            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.Role).bSuccess)
            {
                MessageUtil.Show(string.Format("角色【{0}】已删除", currentRole.roleName), "系统提示");
                loadData();
            }
            else
            {
                MessageUtil.Show("角色删除失败！" + resultContent, "系统提示");
            }
        }

        private void btnSetStatus_ItemClick(object sender, ItemClickEventArgs e)
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

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnSetAuth_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiAuthForm liAuthForm = new LiAuthForm(currentRole);
            liAuthForm.ShowDialog();
        }
    }
}