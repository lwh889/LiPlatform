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
using DevExpress.XtraNavBar;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraEditors;
using DevExpress.Utils;

using LiContexts.Model;
using LiModel.Dev.GridlookUpEdit;
using LiModel.Form;
using LiHttp.Enum;
using LiContexts;
using LiCommon.Util;
using LiForm.Dev.Common;
using LiModel.Users;

namespace LiManage
{
    public partial class LiAuthForm : DevExpress.XtraEditors.XtraForm
    {
        public VoucherType voucherType = VoucherType.Voucher;

        private RoleModel roleModel;

        private DateTime m_LastClick = System.DateTime.Now;
        private XtraMdiTabPage m_lastPage = null;

        private List<AuthModel> newAuthList = new List<AuthModel>();
        private List<AuthDataModel> newAuthDataList = new List<AuthDataModel>();

        private List<AuthModel> authList = new List<AuthModel>();
        private List<AuthDataModel> authDataList = new List<AuthDataModel>();
        /// <summary>
        /// 菜单
        /// </summary>
        private List<TreeDataModel> _listTreeData;
        /// <summary>
        /// 菜单
        /// </summary>
        public List<TreeDataModel> listTreeData { get { return _listTreeData; } }

        /// <summary>
        /// 当前选中的菜单节点
        /// </summary>
        private TreeDataModel _currentTreeDataModel;
        /// <summary>
        /// 当前选中的菜单节点
        /// </summary>
        public TreeDataModel currentTreeDataModel { get { return _currentTreeDataModel; } }


        public LiAuthForm(RoleModel roleModel)
        {
            InitializeComponent();

            this.roleModel = roleModel;

            Init();

        }

        public void Init()
        {
            InitData();
            InitControl();
            InitMenu();
        }

        public void InitData()
        {
            
        }

        public void InitControl()
        {
            this.txtRoleName.Text = roleModel.roleName;
        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitMenu()
        {
            reloadMenu();
            InitSearchControl1();
        }

        public void reloadMenu()
        {
            //string resultContent = string.Empty;

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), LiHttpQuery.getQueryParamModelAll("queryBy", "liAdminMeum"), out resultContent))
            //{
            //    _listTreeData = JsonUtil.GetEntityToList<TreeDataModel>(resultContent);
            //}

            _listTreeData = LiContexts.LiContext.getHttpEntity(LiEntityKey.AdminMeum, LiContext.SystemCode).getEntityList<TreeDataModel>();
            this.treeList1.DataSource = listTreeData;
            this.treeList1.RefreshDataSource();

        }

        /// <summary>
        /// 实现树节点的过滤查询
        /// </summary>
        private void InitSearchControl1()
        {

            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.EnableAppearanceOddRow = true;
            this.treeList1.OptionsView.EnableAppearanceEvenRow = true;
            //this.treeList1.OptionsView.i = RowImagesShowMode.InCell;//紧凑型图标
            this.treeList1.ExpandAll();

            this.treeList1.OptionsView.ShowAutoFilterRow = true;//显示过滤行            
            this.treeList1.OptionsBehavior.EnableFiltering = true;//开启过滤功能
            this.treeList1.ColumnFilterChanged += (s, e) => //自定义过滤事件
            {
                var tree = (TreeList)s;
                if (tree != null && tree.ActiveEditor != null)
                {
                    string newKey = tree.ActiveEditor.EditValue.ToString();
                    tree.FilterNodes();

                    var operation = new FilterNodeOperation(newKey ?? "");
                    tree.NodesIterator.DoOperation(operation);
                }
            };

            ////初始化树节点选择事件
            //this.treeList1.FocusedNodeChanged += (s, e) =>
            //{
            //    this.FocusedNodeChanged(s, e);
            //};
        }

        /// <summary>
        /// 菜单显示，左右键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;
            //右键菜单
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None && treeList1.State == TreeListState.Regular)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tree.SetFocusedNode(hitInfo.Node);
                }

                if (tree.FocusedNode != null)
                {
                    //popupMenuTree.ShowPopup(p);
                    _currentTreeDataModel = (TreeDataModel)tree.GetRow(tree.FocusedNode.Id);
                }
            }

            //左键编辑
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.None && treeList1.State == TreeListState.NodePressed)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tree.SetFocusedNode(hitInfo.Node);
                }

                if (tree.FocusedNode != null && tree.FocusedNode.ParentNode != null)
                {
                    _currentTreeDataModel = (TreeDataModel)tree.GetRow(tree.FocusedNode.Id);

                    TreeDataModel rootTreeDataModel = (TreeDataModel)tree.GetRow(tree.FocusedNode.RootNode.Id);

                    //if (currentTreeDataModel.Code == "LiDictDesign")
                    //{
                    //    LiManageDict ribbonForm = new LiManageDict();
                    //    AddPageMdi(PageFormModel.getInstance(99999, "1234567", ribbonForm));
                    //}

                    if (rootTreeDataModel.Code == "LiBusinessManage")
                    {
                        txtMenuName.Text = currentTreeDataModel.Name;

                        if (currentTreeDataModel.Code.Substring(currentTreeDataModel.Code.Length - 4, 4) == "List")
                        {
                            loadData(currentTreeDataModel.Code.Substring(0, currentTreeDataModel.Code.Length - 4), LiContext.SystemCode);
                        }
                        else
                        {
                            loadData(currentTreeDataModel.Code, LiContext.SystemCode);
                        }
                    }

                }
            }
        }

        private void loadData(string entityKey, string systemCode)
        {
            FormModel formModel = LiContext.getFormModel(entityKey, systemCode);
            //FormModel formModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel, LiContext.SystemCode).getEntitySingle<FormModel>(entityKey,"name");
            //FormModel formModel = FormModelEntity.getEntitySingle_FormModel(entityKey);

            if (voucherType == VoucherType.Voucher)
            {
                loadAuthInVoucher(newAuthList, newAuthDataList, formModel);
            }
            else
            {
                loadAuthInVoucherList(newAuthList, newAuthDataList, formModel);
            }
            Dictionary<string, object> whereDict = new Dictionary<string, object>();
            whereDict.Add("roleCode", roleModel.roleCode);
            whereDict.Add("entityKey", entityKey);
            authList = LiContexts.LiContext.getHttpEntity(LiEntityKey.Auth).getEntityList<AuthModel>(whereDict);
            //authList = AuthEntity.getEntityList<AuthModel>(whereDict,"liAuth");
            authDataList = LiContexts.LiContext.getHttpEntity(LiEntityKey.AuthData).getEntityList<AuthDataModel>(whereDict);
            //authDataList = AuthDataEntity.getEntityList<AuthDataModel>(whereDict, "liAuthData");
            updateAuth(authList, newAuthList);
            updateAuthData(authDataList, newAuthDataList);

            gridControl1.DataSource = authList;
            gridControl2.DataSource = authDataList;

            gridControl1.RefreshDataSource();
            gridControl2.RefreshDataSource();
        }

        private void updateAuth(List<AuthModel> authList, List<AuthModel> newAuthList)
        {
            List<AuthModel> deleteds = new List<AuthModel>();
            foreach (AuthModel authModel in authList)
            {
                AuthModel newAuthModel = newAuthList.Where(m => m.code == authModel.code).FirstOrDefault();
                if (newAuthModel == null)
                {
                    deleteds.Add(authModel);
                }
                else
                {
                    authModel.name = newAuthModel.name;
                }
            }

            foreach (AuthModel deleted in deleteds)
            {
                DataUtil.deleteInList(deleted, authList);
            }

            foreach (AuthModel newAuthModel in newAuthList)
            {
                AuthModel authModel = authList.Where(m => m.code == newAuthModel.code).FirstOrDefault();
                if (authModel == null)
                {
                    authList.Add(newAuthModel);
                }
            }
        }


        private void updateAuthData(List<AuthDataModel> authDataList, List<AuthDataModel> newAuthDataList)
        {
            List<AuthDataModel> deleteds = new List<AuthDataModel>();
            foreach (AuthDataModel authDataModel in authDataList)
            {
                AuthDataModel newAuthDataModel = newAuthDataList.Where(m => m.code == authDataModel.code).FirstOrDefault();
                if (newAuthDataModel == null)
                {
                    deleteds.Add(authDataModel);
                }
                else
                {
                    authDataModel.name = newAuthDataModel.name;
                }
            }

            foreach (AuthDataModel deleted in deleteds)
            {
                DataUtil.deleteInList(deleted, authDataList);
            }

            foreach (AuthDataModel newAuthDataModel in newAuthDataList)
            {
                AuthDataModel authDataModel = authDataList.Where(m => m.code == newAuthDataModel.code).FirstOrDefault();
                if (authDataModel == null)
                {
                    authDataList.Add(newAuthDataModel);
                }
            }
        }


        private void loadAuthInVoucher(List<AuthModel> newAuthList, List<AuthDataModel> newAuthDataList, FormModel formModel)
        {
            newAuthList.Clear();
            newAuthDataList.Clear();

            foreach (ButtonGroupModel buttonGroupModel in formModel.buttonGroups)
            {
                foreach (ButtonModel buttonModel in buttonGroupModel.buttons)
                {
                    newAuthList.Add(new AuthModel() { code = buttonModel.name, name = buttonModel.caption, entityKey = formModel.name, roleCode = roleModel.roleCode, bShow = true });
                }

            }

            foreach (PanelModel panelModel in formModel.panels)
            {

                foreach (ButtonGroupModel buttonGroupModel in panelModel.buttonGroups)
                {
                    foreach (ButtonModel buttonModel in buttonGroupModel.buttons)
                    {
                        newAuthList.Add(new AuthModel() { code = buttonModel.name, name = buttonModel.caption, entityKey = formModel.name, roleCode = roleModel.roleCode, bShow = true });
                    }
                }

                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ButtonGroupModel buttonGroupModel in controlGroupModel.buttonGroups)
                    {
                        foreach (ButtonModel buttonModel in buttonGroupModel.buttons)
                        {
                            newAuthList.Add(new AuthModel() { code = buttonModel.name, name = buttonModel.caption, entityKey = formModel.name, roleCode = roleModel.roleCode, bShow = true });
                        }
                    }

                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {
                        newAuthDataList.Add(new AuthDataModel() { code = controlModel.name, name = controlModel.text, entityKey = formModel.name, roleCode = roleModel.roleCode, bShow = true, bEdit = true });
                    }
                }
            }

        }
        private void loadAuthInVoucherList(List<AuthModel> newAuthList, List<AuthDataModel> newAuthDataList, FormModel formModel)
        {
            newAuthList.Clear();
            newAuthDataList.Clear();

            foreach (ListButtonModel listButton in formModel.listButtons)
            {
                newAuthList.Add(new AuthModel() { code = listButton.name, name = listButton.caption, entityKey = formModel.name, roleCode = roleModel.roleCode, bShow = true });
            }

            foreach (PanelModel panelModel in formModel.panels)
            {
                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {
                        newAuthDataList.Add(new AuthDataModel() { code = controlModel.name, name = controlModel.text, entityKey = formModel.name, roleCode = roleModel.roleCode, bShow = true, bEdit = true });
                    }
                }
            }

        }

        private void dockPanel2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiContexts.LiContext.getHttpEntity(LiEntityKey.Auth).batchSaveEntity<AuthModel>(true, authList);
            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.Auth).bSuccess)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.AuthData).batchSaveEntity<AuthDataModel>(true, authDataList);
                if (LiContexts.LiContext.getHttpEntity(LiEntityKey.AuthData).bSuccess)
                {
                    MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.AuthData).tipStr, "温馨提示");
                }
                else
                {
                    MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.AuthData).tipStr, "温馨提示");
                }
            }
            else
            {
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.AuthData).tipStr, "温馨提示");
            }

        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
    }

    public enum VoucherType
    {
        Voucher,
        VoucherList
    }
}