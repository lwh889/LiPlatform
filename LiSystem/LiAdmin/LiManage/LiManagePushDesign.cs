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
using LiModel.LiPush;
using LiModel.Form;
using LiHttp.Enum;
using LiContexts;
using LiControl.Util;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraLayout;
using LiControl.Form;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraVerticalGrid;
using LiCommon.Util;

namespace LiManage
{
    public partial class LiManagePushDesign : DevExpress.XtraBars.Ribbon.RibbonForm
    {
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
        /// 用户控件
        /// </summary>
        List<GridlookUpEditModel> userControls = new List<GridlookUpEditModel>();

        /// <summary>
        /// 时间控件
        /// </summary>
        List<GridlookUpEditModel> dateControls = new List<GridlookUpEditModel>();

        /// <summary>
        /// 状态控件
        /// </summary>
        List<GridlookUpEditModel> statusControls = new List<GridlookUpEditModel>();

        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        /// <summary>
        /// 当前选择的表格数据
        /// </summary>
        object selectedGridRowData = null;

        /// <summary>
        /// 单据模型
        /// </summary>
        PushFormModel pushFormModel = null;

        /// <summary>
        /// 设计单据状态
        /// </summary>
        VoucherStatusModel voucherDesignStatusModel;
        #region 状态
        /// <summary>
        /// 表单的状态
        /// </summary>
        public string formStatusCode { set; get; }

        /// <summary>
        /// 新增状态
        /// </summary>
        public void setNewStatus()
        {
            formStatusCode = "NewStatus";
        }


        /// <summary>
        /// 编辑状态
        /// </summary>
        public void setEditStatus()
        {
            formStatusCode = "EditStatus";
        }


        /// <summary>
        /// 浏览状态
        /// </summary>
        public void setShowStatus()
        {
            formStatusCode = "ShowStatus";
        }

        /// <summary>
        /// 表单ID
        /// </summary>
        private string formID = "LiManagePushDesign";
        #endregion

        public LiManagePushDesign(PushFormModel pushFormModel)
        {
            InitializeComponent();
            this.pushFormModel = pushFormModel;

            Init();
        }

        private void LiManagePushDesign_Load(object sender, EventArgs e)
        {
            loadData();
            setFormStatus(formStatusCode);
        }
        private void Init()
        {
            InitData();
            InitControl();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        public void InitControl()
        {

        }


        /// <summary>
        /// /初始化数据
        /// </summary>
        private void InitData()
        {

            List<VoucherStatusModel> voucherDesignStatusModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, LiContext.SystemCode).getEntityList<VoucherStatusModel>(pushFormModel.name, "code");  
            if (voucherDesignStatusModels.Count <= 0)
            {
                voucherDesignStatusModel = new VoucherStatusModel() { code = pushFormModel.name, name = pushFormModel.text, dataStatuss = new List<StatusModel>() };
            }
            else
            {
                voucherDesignStatusModel = voucherDesignStatusModels[0];
            }

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
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            loadpropertyGridControl(pushFormModel, null, propertyGridControl1);
            loadForm(pushFormModel);
        }


        #region 加载


        /// <summary>
        /// 属性表格加载显示
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <param name="gridView"></param>
        /// <param name="propertyGridControl"></param>
        public void loadpropertyGridControl(object selectedObject, GridView gridView, DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl)
        {
            propertyGridControl.RowHeaderWidth = 100;//设置自定义属性左边属性名称的宽度
            propertyGridControl.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            propertyGridControl.SelectedObject = selectedObject;//设置要展示属性的对象
            propertyGridControl.ExpandAllRows();//展开所有属性(包括子属性)
            propertyGridControl.Tag = gridView;
        }

        /// <summary>
        /// 加载单据模型
        /// </summary>
        /// <param name="formModel"></param>
        public void loadForm(PushFormModel pushFormModel)
        {
            if (pushFormModel == null) return;
            gridControl6.DataSource = null;
            gridControl7.DataSource = null;

            loadGridData(pushFormModel.events, gridView6, gridControl6);

            loadGridData(pushFormModel.listButtons, gridView7, gridControl7);


        }


        public void loadListButtonGrid(ListButtonModel listButtonModel)
        {
            if (listButtonModel == null) return;

            gridControl6.DataSource = null;

            loadGridData(listButtonModel.events, gridView6, gridControl6);

        }
        /// <summary>
        /// 数据源加载到表格，并刷新
        /// </summary>
        /// <param name="lists"></param>
        /// <param name="gridView"></param>
        /// <param name="gridControl"></param>
        public void loadGridData(object lists, GridView gridView, GridControl gridControl)
        {
            gridControl.DataSource = lists;
            gridView.BestFitColumns();
            gridView.RefreshData();
        }

        #endregion

        private void GridView6_Click(object sender, EventArgs e)
        {
            EventModel eventModel = (EventModel)gridView6.GetFocusedRow();

            loadpropertyGridControl(eventModel, gridView6, propertyGridControl2);
        }

        private void GridView7_Click(object sender, EventArgs e)
        {
            ListButtonModel listButtonModel = (ListButtonModel)gridView7.GetFocusedRow();
            loadListButtonGrid(listButtonModel);

            loadpropertyGridControl(listButtonModel, gridView7, propertyGridControl2);
        }

        private void PropertyGridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                selectedGridRowData = pushFormModel;
                popupPushFormMenu.ShowPopup(p);
            }
        }

        private void PropertyGridControl1_Click(object sender, EventArgs e)
        {
            loadForm(pushFormModel);
            loadpropertyGridControl(pushFormModel, null, propertyGridControl2);
        }

        private void PropertyGridControl2_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            PropertyGridControl propertyGridControl = (PropertyGridControl)sender;
            GridView gridView = (GridView)propertyGridControl.Tag;
            if (gridView != null)
            {
                gridView.RefreshData();
            }
        }

        private void GridView7_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupListButtonMenu, gridView7, e);
        }

        private void GridView6_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupEventMenu, gridView6, e);
        }

        private void showPopupGridMenu(PopupMenu popupMenu, GridView gridView, MouseEventArgs e)
        {
            selectedGridRowData = gridView.GetFocusedRow();
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                popupMenu.ShowPopup(p);
            }
        }

        private void BtnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            pushFormModel = PushFormModel.getInstance();
            loadData();
            setNewStatus();
            setFormStatus(formStatusCode);
        }

        private void BtnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadData();
            setEditStatus();
            setFormStatus(formStatusCode);
        }

        private void BtnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            string resultContent = string.Empty;

            if (pushFormModel.id > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.PushFormModel).updateEntity(pushFormModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.PushFormModel).tipStr, "温馨提示");

            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.PushFormModel).newEntity(pushFormModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.PushFormModel).tipStr, "温馨提示");

            }

            LiContext.getFormModelList(pushFormModel.name, LiContext.SystemCode);
            loadData();

            formStatusCode = "ShowStatus";
            setFormStatus(formStatusCode);

        }

        private void BtnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void BtnStatus_ItemClick(object sender, ItemClickEventArgs e)
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

            LiSetReadOnlyForm form = new LiSetReadOnlyForm(newControlStatusModels, userControls, dateControls, statusControls, voucherStatusModel);
            form.Show();
        }

        private void BtngAddGneralEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiGeneralEventForm frm = new LiGeneralEventForm();
            if (DialogResult.Yes == frm.ShowDialog())
            {
                List<GeneralEventModel> drs = frm.SelectRows;

                PushEventModel eventModel;
                foreach (GeneralEventModel model in drs)
                {
                    switch (selectedGridRowData.GetType().Name)
                    {

                        case "FormModel":
                            PushFormModel pushFormModel = (PushFormModel)selectedGridRowData;
                            eventModel = PushEventModel.getInstance(pushFormModel.id, 0);
                            break;
                        case "ListButtonModel":
                            PushListButtonModel pushListButtonModel = (PushListButtonModel)selectedGridRowData;
                            eventModel = PushEventModel.getInstance(0, pushListButtonModel.id);
                            break;
                        default:
                            eventModel = PushEventModel.getInstance(0, 0);
                            break;
                    }
                    eventModel.assemblyName = model.eventAssemblyName;
                    eventModel.fullName = model.eventFullName;

                    DevControlUtil.addRowInGridView<PushEventModel>(eventModel, gridControl6);
                }

                gridControl6.RefreshDataSource();
            }
        }

        private void BtnAddPluginEvent_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            string filename = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "DLL文件|*.dll|所有文件|*.*";
            openFileDialog1.Title = "请选择Excel文件";
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.DefaultExt = "所有文件|*.*";
            openFileDialog1.DefaultExt = "*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LiPlugInForm frm = new LiPlugInForm(openFileDialog1.FileName);
                if (DialogResult.Yes == frm.ShowDialog())
                {
                    List<DataRow> drs = frm.SelectRows;

                    PushEventModel eventModel;
                    foreach (DataRow dr in drs)
                    {
                        switch (selectedGridRowData.GetType().Name)
                        {

                            case "FormModel":
                                PushFormModel pushFormModel = (PushFormModel)selectedGridRowData;
                                eventModel = PushEventModel.getInstance(pushFormModel.id, 0);
                                break;
                            case "ListButtonModel":
                                PushListButtonModel pushListButtonModel = (PushListButtonModel)selectedGridRowData;
                                eventModel = PushEventModel.getInstance(0, pushListButtonModel.id);
                                break;
                            default:
                                eventModel = PushEventModel.getInstance(0,  0);
                                break;
                        }
                        eventModel.assemblyName = Convert.ToString(dr["eventAssemblyName"]);
                        eventModel.fullName = Convert.ToString(dr["eventFullName"]);

                        DevControlUtil.addRowInGridView<PushEventModel>(eventModel, gridControl6);
                    }

                }
                gridControl6.RefreshDataSource();
            }
        }

        private void BtnDeleteEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            //deleteGridRow(selectedGridRowData, gridControl6);
            gridView6.RefreshData();
        }
    }
}