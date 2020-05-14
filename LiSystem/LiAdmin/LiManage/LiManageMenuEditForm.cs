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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;


using LiModel.Form;
using LiHttp.Enum;
using LiContexts;
using LiCommon.Util;
using LiModel.LiEnum;
using LiForm.Dev.Util;
using LiForm.Dev;
using LiControl.Util;
using LiModel.Util;
using LiControl.Form;


using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

namespace LiManage
{
    public partial class LiManageMenuEditForm : DevExpress.XtraBars.Ribbon.RibbonForm
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
        /// 表体列内容
        /// </summary>
        Dictionary<string, GridColumn> gridColumnDict = new Dictionary<string, GridColumn>();
        /// <summary>
        /// 状态，用于只读控制
        /// </summary>
        private Dictionary<string, Dictionary<string, bool>> _statusDict = new Dictionary<string, Dictionary<string, bool>>();

        /// <summary>
        /// 菜单模型
        /// </summary>
        private TreeDataModel treeDataModel;

        private LiManageForm2 manageForm;
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

        private string formID = "LiManageMenuEditForm";

        public LiManageMenuEditForm(TreeDataModel treeDataModel, LiManageForm2 manageForm)
        {
            InitializeComponent();

            this.treeDataModel = treeDataModel;
            this.manageForm = manageForm;
            InitMenu();

            InitData();

        }

        /// <summary>
        /// /初始化数据
        /// </summary>
        private void InitData()
        {
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
        /// 初始化树
        /// </summary>
        /// <returns></returns>
        private void InitMenu()
        {
            this.listEditParentID.Properties.ValueMember = "ID";
            this.listEditParentID.Properties.DisplayMember = "Name";

            this.listEditParentID.Properties.TreeList.KeyFieldName = "ID";

            this.listEditParentID.Properties.TreeList.ParentFieldName = "ParentID";
            this.listEditParentID.Properties.DataSource = manageForm.listTreeData;

            var tree = this.treeListLookUpEdit1TreeList;
            tree.Columns.Clear();
            tree.Columns.Add(new TreeListColumn() { Caption = "名称", FieldName = "Name", Name = "treeListLookUpEdit1TreeList1",Visible = true,VisibleIndex=0 });

            this.listEditParentID.EditValue = manageForm.currentTreeDataModel.ID;
            ////设置树的图标集合及逐级图标
            //tree.SelectImageList = this.imageList1;//要有值，才能触发事件
            //tree.CustomDrawNodeImages += (object sender, CustomDrawNodeImagesEventArgs e) =>
            //{
            //    e.SelectImageIndex = 1;
            //};
        }

        private void InitImageList()
        {
            //GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, "name", "name", ImageUtil.ImageListModel.getSearchColumns(), ImageUtil.ImageListModel.getDisplayColumns(), ImageUtil.ImageListModel.getDictModelDesc(), gr, this, ImageUtil.images);

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                DevControlUtil.setContorlData(ModelUtil.getModelValue<TreeDataModel>(kvp.Key, treeDataModel), kvp.Value);
            }


        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                ModelUtil.setModelValue<TreeDataModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), treeDataModel);
            }
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<TreeDataModel> getEntitys(string key)
        {
            //string resultContent;
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "liManageMeum");
            //paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single("Code", key));

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            //{
            //    return JsonUtil.GetEntityToList<TreeDataModel>(resultContent);
            //}

            //return null;
            return LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).getEntityList<TreeDataModel>(key, "Code");
        }

        /// <summary>
        /// 重新加载菜单
        /// </summary>
        private void reloadMenu()
        {
            manageForm.reloadMenu();
            this.listEditParentID.Properties.DataSource = manageForm.listTreeData.Where(m => m.isGroup == true).ToList();
            this.listEditParentID.Refresh();
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

        private void LiManageMenuEditForm_Load(object sender, EventArgs e)
        {
            //reloadMenu();
            loadData();
            setFormStatus(formStatusCode);

        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            getData();

            if (treeDataModel.ID > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).updateEntity(treeDataModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).tipStr, "温馨提示");

                //if (LiContext.liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdate(), LiHttpUpdate.getUpdateParamModel("liManageMeum", treeDataModel), out resultContent))
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
                LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).newEntity(treeDataModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.ManageMeum).tipStr, "温馨提示");

                //if (LiContext.liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsert(), LiHttpInsert.getInsertParamModel("liManageMeum", treeDataModel), out resultContent))
                //{
                //    MessageUtil.Show("保存成功！", "温馨提示");
                //    bSuccess = true;

                //}
                //else
                //{
                //    MessageUtil.Show("保存失败！" + resultContent, "温馨提示");
                //}
            }

            reloadMenu();

            getEntitys(treeDataModel.Code);
            loadData();

            formStatusCode = "ShowStatus";
            setFormStatus(formStatusCode);

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

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            TreeDataModel newTreeDataModel = new TreeDataModel();
            newTreeDataModel.isGroup = true;
            newTreeDataModel.iOrder = 1;
            newTreeDataModel.ParentID = Convert.ToInt32(treeDataModel.ParentID);

            this.treeDataModel = newTreeDataModel;
            loadData();
            setNewStatus();
            setFormStatus(formStatusCode);
        }

        private void btnModify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadData();
            setEditStatus();
            setFormStatus(formStatusCode);
        }

    }
}