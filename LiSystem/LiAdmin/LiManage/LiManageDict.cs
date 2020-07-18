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

using LiModel.Form;
using LiCommon.Util;
using LiHttp.RequestParam;
using LiForm.Dev.Util;
using LiHttp.Server;
using LiForm.Dev.Common;
using LiControl.Util;
using LiModel.Util;
using LiHttp.Enum;
using LiContexts;
using LiModel.LiModelFactory;

namespace LiManage
{

    public partial class LiManageDict : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        /// <summary>
        /// 编辑哪个类型档案
        /// </summary>
        private DictEditType currentEditType {set;get;}

        private DictGroupModel _currentDictGroup;
        /// <summary>
        /// 当前的字典组
        /// </summary>
        public DictGroupModel currentDictGroup { set { _currentDictGroup = value; } get { return _currentDictGroup; } }

        private DictModel _currentDict;
        /// <summary>
        /// 当前的字典
        /// </summary>
        public DictModel currentDict { set { _currentDict = value; } get { return _currentDict; } }
        


        /// <summary>
        /// 字典组
        /// </summary>
        private List<DictGroupModel> dictGroupModels;

        /// <summary>
        /// 字典
        /// </summary>
        private List<DictModel> dictModels;

        ///// <summary>
        ///// 单据控件状态，用于更新
        ///// </summary>
        //private List<ControlStatusModel> newControlStatusModels_Group;
        ///// <summary>
        ///// 单据控件状态，用于更新
        ///// </summary>
        //private List<ControlStatusModel> newControlStatusModels_Item;

        ///// <summary>
        ///// 单据表头控件信息
        ///// </summary>
        //Dictionary<string, ControlModel> controlModelDict_Group;

        ///// <summary>
        ///// 单据表头控件信息
        ///// </summary>
        //Dictionary<string, ControlModel> controlModelDict_Item;

        /// <summary>
        /// 单据表头控件
        /// </summary>
        Dictionary<string, Control> controlDict_Group;

        /// <summary>
        /// 单据表头控件
        /// </summary>
        Dictionary<string, Control> controlDict_Item;


        /// <summary>
        /// 单据表头控件标题
        /// </summary>
        Dictionary<string, LayoutControlItem> layoutControlItemDict_Group;

        /// <summary>
        /// 单据表头控件标题
        /// </summary>
        Dictionary<string, LayoutControlItem> layoutControlItemDict_Item;

        /// <summary>
        /// 单据标识
        /// </summary>
        string formID = "LiManageDict";

        /// <summary>
        /// 浮动窗口的流式布局
        /// </summary>
        LayoutControlGroup layoutControlGroup1;
        /// <summary>
        /// 浮动窗口的流式布局
        /// </summary>
        LayoutControl layoutControl1;

        public LiManageDict()
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

            InitSearchControl1();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            controlDict_Group = new Dictionary<string, Control>();
            layoutControlItemDict_Group = new Dictionary<string, LayoutControlItem>();

            controlDict_Item = new Dictionary<string, Control>();
            layoutControlItemDict_Item = new Dictionary<string, LayoutControlItem>();

            InitDockPanel();


        }

        /// <summary>
        /// 初始化浮动窗口
        /// </summary>
        private void InitDockPanel()
        {

            EventHandler saveEventHandler = new EventHandler(this.btnSave_Click);
            EventHandler closeEventHandler = new EventHandler(this.btnClose_Click);

            layoutControl1 = FormUtil.setDockPanelByBasicInfo(saveEventHandler, closeEventHandler, this.dockPanel3_Container);
            layoutControlGroup1 = layoutControl1.Root;

            DictGroupModel dictGroupModel = new DictGroupModel();
            DictModel dictModel = new DictModel();

            InitControlToDockPanel(dictGroupModel, controlDict_Group, layoutControlItemDict_Group, layoutControlGroup1);
            InitControlToDockPanel(dictModel, controlDict_Item, layoutControlItemDict_Item, layoutControlGroup1);
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
        /// 加载数据到树形控件
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="treeListLookUpEdit"></param>
        private void loadTreeListLookUpEditDataSource(object dataSource, TreeListLookUpEdit treeListLookUpEdit)
        {
            treeListLookUpEdit.Properties.DataSource = dataSource;
            treeListLookUpEdit.Refresh();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {

            dictGroupModels = getEntitys_Group();

            this.treeList1.DataSource = dictGroupModels;
            this.treeList1.RefreshDataSource();

            loadTreeListLookUpEditDataSource(dictGroupModels, (TreeListLookUpEdit)controlDict_Group["ParentID"]);
            loadTreeListLookUpEditDataSource(dictGroupModels, (TreeListLookUpEdit)controlDict_Item["dictParentID"]);
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
        /// 获取服务端实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<DictGroupModel> getEntitys_Group()
        {
            //string resultContent;
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "liDictGroup");

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            //{
            //    return JsonUtil.GetEntityToList<DictGroupModel>(resultContent);
            //}

            return LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).getEntityList<DictGroupModel>();
            //return null;
        }

        /// <summary>
        /// 获取服务端实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<DictModel> getEntitys_Item(object key)
        {
            //string resultContent;
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "liDict");
            //paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single("dictParentID", key));

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            //{
            //    return JsonUtil.GetEntityToList<DictModel>(resultContent);
            //}

            return LiContexts.LiContext.getHttpEntity(LiEntityKey.Dict, LiContext.SystemCode).getEntityList<DictModel>(key, "dictParentID");
            //return null;
        }

        /// <summary>
        /// 显示浮动窗体
        /// </summary>
        /// <param name="bShow"></param>
        private void setShowDockPanel(bool bShow)
        {
            if(bShow){
                dockPanel3.Show();
            }else{
                dockPanel3.Hide();
            }
        }

        /// <summary>
        /// 保存字典组
        /// </summary>
        private void saveDictGroup()
        {
            bool bSuccess = false;

            string resultContent = string.Empty;

            getData<DictGroupModel>(currentDictGroup, controlDict_Group);

            if (currentDictGroup.ID > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).updateEntity(currentDictGroup);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).tipStr, "温馨提示");
                //if (LiContext.liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdate(), LiHttpUpdate.getUpdateParamModel("liDictGroup", currentDictGroup), out resultContent))
                //{
                //    MessageUtil.Show("修改成功！", "温馨提示");
                //    bSuccess = true;
                //}
                //else
                //{
                //    MessageUtil.Show("修改失败！" + resultContent, "温馨提示");
                //}
            }
            else
            {

                LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).newEntity(currentDictGroup);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).tipStr, "温馨提示");
                //if (LiContext.liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsert(), LiHttpInsert.getInsertParamModel("liDictGroup", currentDictGroup), out resultContent))
                //{
                //    MessageUtil.Show("保存成功！", "温馨提示");
                //    bSuccess = true;

                //}
                //else
                //{
                //    MessageUtil.Show("保存失败！" + resultContent, "温馨提示");
                //}
            }
        }

        /// <summary>
        /// 保存字典
        /// </summary>
        private void saveDict()
        {
            bool bSuccess = false;

            string resultContent = string.Empty;

            getData<DictModel>(currentDict, controlDict_Item);

            if (currentDict.id > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.Dict, LiContext.SystemCode).updateEntity(currentDict);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.Dict, LiContext.SystemCode).tipStr, "温馨提示");

                //if (LiContext.liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdate(), LiHttpUpdate.getUpdateParamModel("liDict", currentDict), out resultContent))
                //{
                //    MessageUtil.Show("修改成功！", "温馨提示");
                //    bSuccess = true;
                //}
                //else
                //{
                //    MessageUtil.Show("修改失败！" + resultContent, "温馨提示");
                //}
            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.Dict, LiContext.SystemCode).newEntity(currentDict);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.Dict, LiContext.SystemCode).tipStr, "温馨提示");

                //if (LiContext.liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsert(), LiHttpInsert.getInsertParamModel("liDict", currentDict), out resultContent))
                //{
                //    MessageUtil.Show("保存成功！", "温馨提示");
                //    bSuccess = true;

                //}
                //else
                //{
                //    MessageUtil.Show("保存失败！" + resultContent, "温馨提示");
                //}
            }


            gridControl1.DataSource = getEntitys_Item(_currentDictGroup.ID);
            gridControl1.RefreshDataSource();
        }

        private void btnAddDictGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentEditType = DictEditType.DictGroup;
            currentDictGroup = new DictGroupModel();
            loadControlInDockPanel<DictGroupModel>(currentDictGroup, controlDict_Group, layoutControlItemDict_Group, layoutControlGroup1);
            setShowDockPanel(true);
            //dockPanelGroup.Show();
        }

        private void btnEditDictGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentDictGroup == null)
            {
                MessageUtil.Show("请选择字典组！", "系统提示");
                return;
            }

            currentEditType = DictEditType.DictGroup;
            loadControlInDockPanel<DictModel>(currentDictGroup, controlDict_Group, layoutControlItemDict_Group, layoutControlGroup1);
            setShowDockPanel(true);
        }

        private void btnDeleteDictGroup_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).deleteEntity(currentDictGroup))
            {
                MessageUtil.Show(string.Format("字典组【{0}】已删除", currentDictGroup.Name), "系统提示");
            }
            else
            {
                MessageUtil.Show("字典组删除失败！" + LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).resultContent, "系统提示");
            }
        }

        private void btnAddDict_ItemClick(object sender, ItemClickEventArgs e)
        {
            currentEditType = DictEditType.Dict;
            currentDict = new DictModel();
            currentDict.dictParentID = currentDictGroup.ID;

            loadControlInDockPanel<DictModel>(currentDict, controlDict_Item, layoutControlItemDict_Item, layoutControlGroup1);
            setShowDockPanel(true);
            //dockPanelItem.Show();
        }

        private void btnEditDict_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentDict == null)
            {
                MessageUtil.Show("请选择字典！", "系统提示");
                return;
            }

            loadControlInDockPanel<DictModel>(currentDict, controlDict_Item, layoutControlItemDict_Item, layoutControlGroup1);
            setShowDockPanel(true);
            //dockPanelItem.ShowSliding();
        }

        private void btnDeleteDict_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.Dict, LiContext.SystemCode).deleteEntity(currentDict))
            {
                MessageUtil.Show(string.Format("字典【{0}】已删除", currentDictGroup.Name), "系统提示");
            }
            else
            {
                MessageUtil.Show("字典删除失败！" + LiContexts.LiContext.getHttpEntity(LiEntityKey.Dict, LiContext.SystemCode).resultContent, "系统提示");
            }
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void LiManageDict_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            TreeList tree = sender as TreeList;  
            //左键编辑
            if (e.Button == MouseButtons.Left && ModifierKeys == Keys.None && treeList1.State == TreeListState.NodePressed)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                TreeListHitInfo hitInfo = tree.CalcHitInfo(e.Location);
                if (hitInfo.HitInfoType == HitInfoType.Cell)
                {
                    tree.SetFocusedNode(hitInfo.Node);
                }

                if (tree.FocusedNode != null )
                {
                    _currentDictGroup = (DictGroupModel)tree.GetRow(tree.FocusedNode.Id);
                    loadControlInDockPanel<DictGroupModel>(_currentDictGroup, controlDict_Group, layoutControlItemDict_Group, layoutControlGroup1);

                    gridControl1.DataSource = getEntitys_Item(_currentDictGroup.ID);
                    gridControl1.RefreshDataSource();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (currentEditType == DictEditType.DictGroup)
            {
                saveDictGroup();
            }
            else
            {
                saveDict();
            }

            loadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            dockPanel3.Hide();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            currentDict = gridView1.GetFocusedRow() as DictModel;

        }
    }

    /// <summary>
    /// 当前编辑档案类型
    /// </summary>
    public enum DictEditType
    {
        Dict,
        DictGroup
    }

}