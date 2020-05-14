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
using DevExpress.Utils;

using LiModel.Form;
using LiHttp.Enum;
using LiContexts;
using LiCommon.Util;
using LiModel.Basic;
using LiForm.Dev.Common;
using LiForm.Dev.Util;
using LiContexts.Model;
using LiFlow;
using LiHttp.RequestParam;

namespace LiManage
{
    public partial class LiManageForm2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private DateTime m_LastClick = System.DateTime.Now;
        private XtraMdiTabPage m_lastPage = null;
        private int tabCount = 1;

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

        public LiManageForm2()
        {
            InitializeComponent();

            InitMenu();
        }

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public void InitMenu()
        {
            reloadMenu();
            InitContext();
            InitSearchControl1();
        }

        private void InitContext()
        {

        }

        public void reloadMenu()
        {
            //string resultContent = string.Empty;

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), LiHttpQuery.getQueryParamModelAll("queryBy", "liManageMeum"), out resultContent))
            //{
            //    _listTreeData = JsonUtil.GetEntityToList<TreeDataModel>(resultContent);
            //}
            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).getQueryParamModel_ShowAllColumn();

            QueryComplexWhereModel queryComplexWhereModel = QueryComplexWhereModel.OR();
            queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.OR("systemCode", LiContext.SystemCode));
            queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.OR("systemCode", "LiSystem"));

            paramModel.complexWheres = queryComplexWhereModel;
            _listTreeData = LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).getEntityList<TreeDataModel>(paramModel);
            this.treeList1.DataSource = _listTreeData;
            //this.treeList1.DataSource = listTreeData;
            this.treeList1.RefreshDataSource();
            this.treeList1.ExpandAll();
        }

        // 打开子窗体方法
        private void AddPageMdi(PageFormModel pageFormModel)
        {
            if (!LiContexts.LiContext.pageFormModels.ContainsKey(pageFormModel.ToString()))
            {
                LiContexts.LiContext.pageFormModels.Add(pageFormModel.ToString(), pageFormModel);

            }

            pageFormModel = LiContexts.LiContext.pageFormModels[pageFormModel.ToString()];
            if(pageFormModel.liForm.MdiParent == null)
                pageFormModel.liForm.MdiParent = this;

            pageFormModel.liForm.Show();
            // 设置当前 tab页的 图标,我这里也默认取navBar中的Item中的图标
            //xtraTabbedMdiManager1.Pages[childForm].Image = navItem.SmallImage;
        }

        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabbedMdiManager1_MouseDown(object sender, MouseEventArgs e)
        {
            XtraMdiTabPage curPage = (sender as XtraTabbedMdiManager).SelectedPage;

            if (e.Button == MouseButtons.Left)
            {

                DateTime dt = DateTime.Now;
                TimeSpan span = dt.Subtract(m_LastClick);
                if (span.TotalMilliseconds < 300)  //如果两次点击的时间间隔小于300毫秒，则认为是双击
                {


                    if (this.MdiChildren.Length > 1)
                    {

                        // 限制只有在同一个页签上双击才能关闭.(规避两个页签切换时点太快导致意外关闭页签)
                        if (curPage.Equals(m_lastPage))
                        {
                            //if (this.ActiveMdiChild != m_MapForm)
                            //{
                            this.ActiveMdiChild.Close();
                            //}

                        }
                    }
                    m_LastClick = dt.AddMinutes(-1);
                }
                else
                {
                    m_LastClick = dt;
                    m_lastPage = curPage;
                }
            }
        }

        #region 合并事件
        /// <summary>
        /// 合并事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbon_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {

            RibbonControl parentRRibbon = sender as RibbonControl;
            RibbonControl childRibbon = e.MergedChild;
            parentRRibbon.StatusBar.MergeStatusBar(childRibbon.StatusBar);
            //parentRRibbon.MergeRibbon(childRibbon, true);

        }

        /// <summary>
        /// 解除合并事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbon_UnMerge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {

            RibbonControl parentRRibbon = sender as RibbonControl;
            RibbonControl childRibbon = e.MergedChild;
            parentRRibbon.StatusBar.UnMergeStatusBar();
            //parentRRibbon.UnMergeRibbon();
        }
        #endregion

        /// <summary>
        /// 实现树节点的过滤查询
        /// </summary>
        private void InitSearchControl()
        {
            //this.searchControl1.Client = this.treeList1;
            ////this.treeList1.FilterNode += (object sender, DevExpress.XtraTreeList.FilterNodeEventArgs e) =>
            ////{
            ////    if (treeList1.DataSource == null)
            ////        return;

            ////    string nodeText = e.Node.GetDisplayText("Name");//参数填写FieldName  
            ////    if (string.IsNullOrWhiteSpace(nodeText))
            ////        return;

            ////    bool isExist = nodeText.IndexOf(searchControl1.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            ////    if (isExist)
            ////    {
            ////        var node = e.Node.ParentNode;
            ////        while (node != null)
            ////        {
            ////            if (!node.Visible)
            ////            {
            ////                node.Visible = true;
            ////                node = node.ParentNode;
            ////            }
            ////            else
            ////                break;
            ////        }
            ////    }
            ////    e.Node.Visible = isExist;
            ////    e.Handled = true;
            ////};
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
        /// 加载就触发了
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void FocusedNodeChanged(object s, FocusedNodeChangedEventArgs e)
        {
            var tree = (TreeList)s;
            if (tree != null && tree.FocusedNode != null)
            {
                //选中节点文本
                var text = tree.FocusedNode.GetDisplayText(0);
                switch (text)
                {
                    case "采血":
                        //ChildWinManagement.LoadMdiForm(this, typeof(FrmOrganization));
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 图标加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            if (e.Node == null) return;
            TreeListNode node = e.Node;

            int ID = (int)node.GetValue("ID");
            if (ID == 1)
                e.NodeImageIndex = 0;
            else
                e.NodeImageIndex = 1;
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
            if (e.Button == MouseButtons.Right&& ModifierKeys == Keys.None && treeList1.State == TreeListState.Regular)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tree.SetFocusedNode(hitInfo.Node);
                }

                if (tree.FocusedNode != null)
                {
                    popupMenuTree.ShowPopup(p);
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

                    if (currentTreeDataModel.Code == "LiDictDesign")
                    {
                        LiManageDict ribbonForm = new LiManageDict();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code,"",false));
                    }

                    if (rootTreeDataModel.Code == "LiU8BasicInfoDesign")
                    {
                        TableModel tableModel;
                        List<TableModel> tableModels = LiManageBasicInfoInOutDesin.getEntitys(currentTreeDataModel.Code);
                        if (tableModels.Count <= 0)
                        {
                            tableModel = TableModel.getInstanceByBasic();
                            tableModel.entityKey = currentTreeDataModel.Code;
                            tableModel.entityName = currentTreeDataModel.Name;
                            tableModels.Add(tableModel);
                        }
                        tableModel = tableModels[0];

                        LiManageBasicInfoInOutDesin ribbonForm = new LiManageBasicInfoInOutDesin(tableModel);
                        ribbonForm.setNewStatus();
                        AddPageMdi(PageFormModel.getInstance(tableModel.id,  ribbonForm, tableModel.entityKey,"",false));
                    }

                    if (rootTreeDataModel.Code == "LiVoucherDesign" && !currentTreeDataModel.isGroup)
                    {
                        
                        FormModel formModel;
                        List<FormModel> formModels = LiContext.getFormModelList(currentTreeDataModel.Code, LiContext.SystemCode);
                        if (formModels.Count <= 0)
                        {
                            formModel = FormModel.getInstance();
                            formModel.name = currentTreeDataModel.Code;
                            formModel.text = currentTreeDataModel.Name;
                            formModel.systemCode = LiContext.SystemCode;
                            formModels.Add(formModel);
                        }
                        formModel = formModels[0];

                        LiManageVoucherDesign ribbonForm = new LiManageVoucherDesign(formModel);
                        AddPageMdi(PageFormModel.getInstance(formModel.id, ribbonForm, formModel.name,"",false));
                    }


                    if (rootTreeDataModel.Code == "LiBasicInfoDesign")
                    {

                        FormModel formModel;
                        List<FormModel> formModels = LiContext.getFormModelList(currentTreeDataModel.Code,LiContext.SystemCode);
                        if (formModels.Count <= 0)
                        {
                            formModel = FormModel.getInstance();
                            formModel.name = currentTreeDataModel.Code;
                            formModel.text = currentTreeDataModel.Name;
                            formModel.formType = "基础档案";
                            formModels.Add(formModel);
                        }
                        formModel = formModels[0];

                        LiManageVoucherDesign ribbonForm = new LiManageVoucherDesign(formModel);
                        AddPageMdi(PageFormModel.getInstance(formModel.id,  ribbonForm, formModel.name,"",false));
                    }

                    if (currentTreeDataModel.Code == "LiUsers")
                    {
                        LiUserForm ribbonForm = new LiUserForm();
                        AddPageMdi(PageFormModel.getInstance(0,  ribbonForm, currentTreeDataModel.Code,"",false));
                    }

                    if (currentTreeDataModel.Code == "LiRoles")
                    {
                        LiRoleForm ribbonForm = new LiRoleForm();
                        AddPageMdi(PageFormModel.getInstance(0,  ribbonForm, currentTreeDataModel.Code,"",false));
                    }

                    if (currentTreeDataModel.Code == "LiConvert")
                    {
                        LiManageConvertForm ribbonForm = new LiManageConvertForm();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false));
                    }

                    if (currentTreeDataModel.Code == "LiFlow")
                    {
                        LiFlowListForm ribbonForm = new LiFlowListForm();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false));
                    }

                    if (currentTreeDataModel.Code == "LiVersionFlow")
                    {
                        LiVersionFlowListForm ribbonForm = new LiVersionFlowListForm();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false));
                    }

                    if (currentTreeDataModel.Code == "LiVoucherFlowManage")
                    {
                        LiFlowManageForm ribbonForm = new LiFlowManageForm();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false));
                    }

                    if (currentTreeDataModel.Code == "LiTableConfigure")
                    {
                        LiTableConfigureListForm ribbonForm = new LiTableConfigureListForm();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false));
                    }

                    if (currentTreeDataModel.Code == "LiProcedureConfigure")
                    {
                        LiProcedureConfigureListForm ribbonForm = new LiProcedureConfigureListForm();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false));
                    }

                    if (currentTreeDataModel.Code == "LiSystemInfoForm")
                    {

                        LiSystemInfoListForm ribbonForm = new LiSystemInfoListForm();
                        AddPageMdi(PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false));

                    }


                }
            }
        }


        private void btnAddMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!currentTreeDataModel.isGroup)
            {
                MessageUtil.Show("选择的菜单不是组", "系统提示");
                return;
            }
            TreeDataModel treeDataModel = new TreeDataModel();
            treeDataModel.isGroup = false;
            treeDataModel.iOrder = 1;
            treeDataModel.ParentID = Convert.ToInt32(currentTreeDataModel.ID);

            LiManageMenuEditForm ribbonForm = new LiManageMenuEditForm(treeDataModel, this);
            ribbonForm.setNewStatus();
            AddPageMdi(PageFormModel.getInstance(treeDataModel.ID, ribbonForm, ribbonForm.Name));
        }

        private void btnDeleteU8BasicInfo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnDeleteVoucher_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnDeleteBasicInfo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnDeleteMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentTreeDataModel.isSystem)
            {
                MessageUtil.Show("系统菜单不能删除", "系统提示");
                return;
            }

            if (currentTreeDataModel.isGroup)
            {
                MessageUtil.Show("菜单组不能选择，请选择菜单！", "系统提示");
                return;
            }

            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).deleteEntity(currentTreeDataModel))
            {
                MessageUtil.Show(string.Format("菜单【{0}】已删除", currentTreeDataModel.Name), "系统提示");
            }
            else
            {
                MessageUtil.Show("菜单删除失败！" + LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).resultContent, "系统提示");
            }

            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).bSuccess)
            {
                reloadMenu();
            }
        }

        private void btnAddU8BasicInfo_ItemClick(object sender, ItemClickEventArgs e)
        {

            TableModel tableModel;

            tableModel = TableModel.getInstanceByBasic();
            tableModel.entityKey = currentTreeDataModel.Code;
            tableModel.entityName = currentTreeDataModel.Name;


            LiManageBasicInfoInOutDesin ribbonForm = new LiManageBasicInfoInOutDesin(tableModel);
            ribbonForm.setNewStatus();
            AddPageMdi(PageFormModel.getInstance(tableModel.id, ribbonForm, tableModel.entityKey));
        }

        private void btnAddVoucher_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnAddBasicInfo_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnEditMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentTreeDataModel.isSystem)
            {
                MessageUtil.Show("系统菜单不能编辑", "系统提示");
                return;
            }

            LiManageMenuEditForm ribbonForm = new LiManageMenuEditForm(currentTreeDataModel, this);
            ribbonForm.setEditStatus();
            PageFormModel pageFormModel = new PageFormModel();
            AddPageMdi(PageFormModel.getInstance(currentTreeDataModel.ID, ribbonForm, currentTreeDataModel.Code));
        }

        private void btnAddMenuGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!currentTreeDataModel.isGroup)
            {
                MessageUtil.Show("选择的菜单不是组", "系统提示");
                return;
            }
            TreeDataModel treeDataModel = new TreeDataModel();
            treeDataModel.isGroup = true;
            treeDataModel.iOrder = 1;
            treeDataModel.ParentID = Convert.ToInt32(currentTreeDataModel.ID);


            LiManageMenuEditForm ribbonForm = new LiManageMenuEditForm(treeDataModel, this);
            ribbonForm.setNewStatus();
            AddPageMdi(PageFormModel.getInstance(treeDataModel.ID, ribbonForm, treeDataModel.Code));
        }

        private void btnEditMenuGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentTreeDataModel.isSystem)
            {
                MessageUtil.Show("系统菜单不能编辑", "系统提示");
                return;
            }

            if (!currentTreeDataModel.isGroup)
            {
                MessageUtil.Show("菜单不能删除，请选择菜单组！", "系统提示");
                return;
            }

            LiManageMenuEditForm ribbonForm = new LiManageMenuEditForm(currentTreeDataModel, this);
            ribbonForm.setEditStatus();

            AddPageMdi(PageFormModel.getInstance(currentTreeDataModel.ID,  ribbonForm, currentTreeDataModel.Code));
        }

        private void btnDeleteMenuGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentTreeDataModel.isSystem)
            {
                MessageUtil.Show("系统菜单不能删除", "系统提示");
                return;
            }


            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).deleteEntity(currentTreeDataModel))
            {
                MessageUtil.Show(string.Format("菜单组【{0}】已删除", currentTreeDataModel.Name), "系统提示");
            }
            else
            {
                MessageUtil.Show("菜单组删除失败！" + LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).resultContent, "系统提示");
            }

            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).bSuccess)
            {
                reloadMenu();
            }
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, MdiTabPageEventArgs e)
        {
            XtraTabbedMdiManager xtraTabbedMdiManager = ((XtraTabbedMdiManager)sender);
            PageFormModel deletePageFormModel = null;
            foreach (KeyValuePair<string, PageFormModel> kvp in LiContexts.LiContext.pageFormModels)
            {
                if (xtraTabbedMdiManager.Pages[kvp.Value.liForm] ==null)
                {
                    deletePageFormModel = kvp.Value;
                }
            }

            if(deletePageFormModel != null)
                LiContexts.LiContext.pageFormModels.Remove(deletePageFormModel.ToString());
        }
    }


}