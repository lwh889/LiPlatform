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

using LiContexts.Model;
using LiModel.Dev.GridlookUpEdit;
using LiModel.Form;
using LiHttp.Enum;
using LiContexts;
using LiCommon.Util;
using LiForm.Dev.Common;
using LiForm.Dev;
using LiForm.Dev.Util;
using Newtonsoft.Json;
using LiHttp.GetEntity;

namespace LiAdmin
{
    public partial class LiAdminForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private DateTime m_LastClick = System.DateTime.Now;
        private XtraMdiTabPage m_lastPage = null;

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


        public LiAdminForm()
        {
            InitializeComponent();
        }

        
        private void Init()
        {
            InitData();
            InitControl();
        }

        private void InitData()
        {

        }

        private void InitControl()
        {
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
            //LiContext.currentForm = this;
        }

        public void reloadMenu()
        {
            //string resultContent = string.Empty;

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), LiHttpQuery.getQueryParamModelAll("queryBy", "liAdminMeum"), out resultContent))
            //{
            //    _listTreeData = JsonUtil.GetEntityToList<TreeDataModel>(resultContent);
            //}

            this.treeList1.DataSource = LiContext.getHttpEntity(LiHttp.Enum.LiEntityKey.AdminMeum, LiContext.SystemCode).getEntityList<TreeDataModel>();
            //this.treeList1.DataSource = listTreeData;
            this.treeList1.RefreshDataSource();

        }
        
        // 打开子窗体方法
        public bool AddPageMdi(PageFormModel pageFormModel)
        {
            return LiContexts.LiContext.AddPageMdi(pageFormModel, this);
        }

        public bool ContainPageMdi(PageFormModel pageFormModel)
        {
            return LiContexts.LiContext.ContainPageMdi(pageFormModel);
        }

        // 清除子窗体方法
        public void ClearPageMdi()
        {
            LiContexts.LiContext.ClearPageMdi();
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
        private void InitSearchControl1()
        {

            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.EnableAppearanceOddRow = true;
            this.treeList1.OptionsView.EnableAppearanceEvenRow = true;
            //this.treeList1.OptionsView.RowIma = RowImagesShowMode.InCell;//紧凑型图标
            //this.treeList1.RowHeight = 20;
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

                    if (rootTreeDataModel.Code == "LiBusinessManage" && !currentTreeDataModel.isGroup)
                    {
                        if (currentTreeDataModel.Code.Substring(currentTreeDataModel.Code.Length - 4, 4) == "List")
                        {
                            RibbonForm ribbonForm = FormUtil.getVoucherList(currentTreeDataModel.Code.Substring(0, currentTreeDataModel.Code.Length - 4), LiContext.SystemCode);
                            ribbonForm.Text = currentTreeDataModel.Name;
                            PageFormModel pageFormModel = PageFormModel.getInstance(0, ribbonForm, currentTreeDataModel.Code, "", false);
                            
                            AddPageMdi(pageFormModel);
                        }
                        else
                        {
                            LiForm.Dev.LiForm liForm = FormUtil.getVoucher(currentTreeDataModel.Code) as LiForm.Dev.LiForm;
                            liForm.Text = currentTreeDataModel.Name;
                            if (AddPageMdi(PageFormModel.getInstance(0, liForm, currentTreeDataModel.Code)))
                            {
                                liForm.setVoucherNewStatus();
                            }
                        }
                    }

                }
            }
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, MdiTabPageEventArgs e)
        {
            XtraTabbedMdiManager xtraTabbedMdiManager = ((XtraTabbedMdiManager)sender);
            PageFormModel deletePageFormModel = null;
            foreach (KeyValuePair<string, PageFormModel> kvp in LiContexts.LiContext.pageFormModels)
            {
                if (xtraTabbedMdiManager.Pages[kvp.Value.liForm] == null)
                {
                    deletePageFormModel = kvp.Value;
                }
            }

            if (deletePageFormModel != null)
                LiContexts.LiContext.pageFormModels.Remove(deletePageFormModel.ToString());
        }

        private void LiAdminForm_Load(object sender, EventArgs e)
        {
            Init();

            LiMessageForm ribbonForm = new LiMessageForm();
            AddPageMdi(PageFormModel.getInstance(0, ribbonForm, ribbonForm.Name, "", false));

            setShowInfo();
        }

        private void BtnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(MessageUtil.ShowMsgBox("是否退出系统!","温馨提示", MsgType.YesNo) == DialogResult.Yes)
                this.Close();
        }

        private void SkinRibbonGalleryBarItem1_GalleryItemClick(object sender, GalleryItemClickEventArgs e)
        {
            string skinName = (e.Item.Tag == null) ? e.Item.Caption : e.Item.Tag.ToString();
            LiContexts.LiContext.updateUserSkin(skinName);
        }

        private void setShowInfo()
        {
            btnUserInfo.Caption = string.Format("当前用户：({0}){1}", LiContexts.LiContext.userInfo.userCode, LiContexts.LiContext.userInfo.userName);
            btnDate.Caption = string.Concat( LiContext.LoginData.ToString("yyyy年MM月dd日 dddd"), " ", DateTimeUtil.GetChineseDateTime(LiContext.LoginData));

            btnCompany.Caption = LiContext.SystemInfo.companyName;
            this.Text = LiContext.SystemInfo.systemTitle;
        }

        private void BtnOut_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(MessageUtil.ShowMsgBox("您确定注销登录吗？","温馨提示", MsgType.YesNo) == DialogResult.Yes)
            {
                Application.Restart();

            }

        }

        private void TreeList1_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            if (e.Node == null) return;
            TreeListNode node = e.Node;

            TreeDataModel treeDataModel = (TreeDataModel)treeList1.GetRow(node.Id);
            e.NodeImageIndex = treeDataModel.imageIndex;
        }

        private void BtnModifyPwd_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiForm.LiModifyPwdForm ribbonForm = new LiForm.LiModifyPwdForm(LiContext.userInfo);
            ribbonForm.ShowDialog();

        }
    }
}