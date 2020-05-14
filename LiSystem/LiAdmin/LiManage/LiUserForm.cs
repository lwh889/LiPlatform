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

namespace LiManage
{
    public partial class LiUserForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        private UserModel _currentUser;
        /// <summary>
        /// 当前的用户
        /// </summary>
        public UserModel currentUser { set { _currentUser = value; } get { return _currentUser; } }

        /// <summary>
        /// 用户数据源
        /// </summary>
        private List<UserModel> userModels;

        /// <summary>
        /// 单据表头控件
        /// </summary>
        Dictionary<string, Control> controlUser_Item;

        /// <summary>
        /// 单据控件状态，用于更新
        /// </summary>
        private List<ControlStatusModel> newControlStatusModels = new List<ControlStatusModel>();

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
        Dictionary<string, LayoutControlItem> layoutControlItemUser_Item;

        /// <summary>
        /// 角色表
        /// </summary>
        DataTable roleTable;

        /// <summary>
        /// 已有角色
        /// </summary>
        List<UserRoleModel> userRoleList;
        /// <summary>
        /// 单据标识
        /// </summary>
        string formID = "liUsers";

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

        public LiUserForm()
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
            controlUser_Item = new Dictionary<string, Control>();
            layoutControlItemUser_Item = new Dictionary<string, LayoutControlItem>();

            roleTable = new DataTable();
            roleTable.Columns.Add(new DataColumn("sel", Type.GetType("System.Boolean")));
            roleTable.Columns.Add(new DataColumn("roleCode", Type.GetType("System.String")));
            roleTable.Columns.Add(new DataColumn("roleName", Type.GetType("System.String")));
            gridControl2.DataSource = roleTable;

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
            UserModel userModel = new UserModel();

            InitControlToDockPanel(userModel, controlUser_Item, layoutControlItemUser_Item, layoutControlGroup1);
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
            List<ControlModel> controls = ControlModel.getInstancesByModel(entity);

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
                DevControlUtil.setContorlData(ModelUtil.getModelValue<TEntity>(kvp.Key, entity), kvp.Value);
            }
        }


        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            roleTable.Rows.Clear();
            List<RoleModel> roleList = LiContexts.LiContext.getHttpEntity(LiEntityKey.Role).getEntityList<RoleModel>();

            foreach (RoleModel roleModel in roleList)
            {
                DataRow dr = roleTable.NewRow();
                dr["sel"] = false;
                dr["roleCode"] = roleModel.roleCode;
                dr["roleName"] = roleModel.roleName;

                roleTable.Rows.Add(dr);
            }

            gridControl2.RefreshDataSource();

            userModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.User).getEntityList<UserModel>();

            gridControl1.DataSource = userModels;
        }

        /// <summary>
        /// 获取控件数据
        /// </summary>
        public void getData<TEntity>(object entity, Dictionary<string, Control> controlDict) where TEntity : class
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                ModelUtil.setModelValue<TEntity>(kvp.Key, DevControlUtil.getControlData(kvp.Value), entity);
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

            getData<UserModel>(currentUser, controlUser_Item);

            LiContexts.LiContext.getHttpEntity(LiEntityKey.User).saveEntity(currentUser.id > 0, currentUser);
            //bSuccess = UserEntity.saveEntity(currentUser.id > 0, "liUsers", currentUser, out tipStr, out resultContent);

            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.User).tipStr, "温馨提示");

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

        private void LiUserForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            currentUser = gridView1.GetFocusedRow() as UserModel;
        }

        private void btnAddUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentUser = new UserModel();

            loadControlInDockPanel<UserModel>(currentUser, controlUser_Item, layoutControlItemUser_Item, layoutControlGroup1);
            setShowDockPanel(true);
            setNewStatus();
        }

        private void btnEditUser_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (currentUser == null)
            {
                MessageUtil.Show("请选择用户！", "系统提示");
                return;
            }

            loadControlInDockPanel<UserModel>(currentUser, controlUser_Item, layoutControlItemUser_Item, layoutControlGroup1);
            setShowDockPanel(true);
            setEditStatus();
        }

        private void btnDeleteUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            //bool bSuccess = false;

            //string resultContent = string.Empty;
            //string tipStr = string.Empty;

            LiContexts.LiContext.getHttpEntity(LiEntityKey.User).deleteEntity(currentUser);
            //bSuccess = UserEntity.deleteEntity("liUsers", currentUser, out tipStr, out resultContent);
            if ( LiContexts.LiContext.getHttpEntity(LiEntityKey.User).bSuccess)
            {
                MessageUtil.Show(string.Format("用户【{0}】已删除", currentUser.userName), "系统提示");
                loadData();
            }
            else
            {
                MessageUtil.Show("用户删除失败！" +  LiContexts.LiContext.getHttpEntity(LiEntityKey.User).resultContent, "系统提示");
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

        private void btnSetRole_ItemClick(object sender, ItemClickEventArgs e)
        {
            userRoleList = LiContexts.LiContext.getHttpEntity(LiEntityKey.UserRole).getEntityList<UserRoleModel>(currentUser.userCode, "userCode");
            //userRoleList = UserRoleEntity.getEntityList<UserRoleModel>(currentUser.userCode, "userCode", "liUserRole");

            foreach (DataRow dr in roleTable.Rows)
            {
                UserRoleModel entity = userRoleList.Where(m => m.roleCode == Convert.ToString(dr["roleCode"])).FirstOrDefault();
                if (entity != null)
                {
                    dr["sel"] = true;
                }
                else
                {
                    dr["sel"] = false;
                }
            }

            this.dockPanel2.Show();
        }

        private void btnRoleSave_Click(object sender, EventArgs e)
        {
            List<UserRoleModel> deleteds = new List<UserRoleModel>();
            foreach (DataRow dr in roleTable.Rows)
            {
                if (Convert.ToBoolean(dr["sel"]))
                {
                    UserRoleModel entity = userRoleList.Where(m => m.roleCode == Convert.ToString(dr["roleCode"])).FirstOrDefault();
                    if (entity == null)
                    {
                        userRoleList.Add(new UserRoleModel() { userCode = currentUser.userCode, roleCode = Convert.ToString(dr["roleCode"]) });
                    }
                }
                else
                {
                    UserRoleModel entity = userRoleList.Where(m => m.roleCode == Convert.ToString(dr["roleCode"])).FirstOrDefault();
                    if (entity != null)
                    {
                        deleteds.Add(entity);
                    }
                }

            }

            foreach (UserRoleModel entity in deleteds)
            {
                DataUtil.deleteInList(entity, userRoleList);
            }
            
            bool bSuccess = false;

            string resultContent = string.Empty;
            string tipStr = string.Empty;

            LiContexts.LiContext.getHttpEntity(LiEntityKey.UserRole).batchSaveEntity(true, userRoleList);
            //UserRoleEntity.batchSaveEntity(true, "liUserRole", userRoleList,out tipStr,out resultContent);
            MessageUtil.Show(tipStr, "温馨提示");
        }

        private void btnRoleClose_Click(object sender, EventArgs e)
        {
            this.dockPanel2.Hide();
        }

    }
}