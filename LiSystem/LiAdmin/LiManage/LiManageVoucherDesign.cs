﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

using LiModel.Form;
using LiModel.Dev;
using LiCommon.Util;
using LiHttp.Enum;
using LiContexts;
using LiModel.LiEnum;
using LiModel.Interface;
using LiModel.Basic;
using LiForm.Dev;
using LiModel.Dev.GridlookUpEdit;
using LiControl.Util;
using LiModel.Util;
using LiControl.Form;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraLayout;
using LiHttp.RequestParam;
using LiModel.LiModelFactory;

namespace LiManage
{
    public partial class LiManageVoucherDesign : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo;
        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        /// <summary>
        /// 单据模型
        /// </summary>
        FormModel formModel = null;

        /// <summary>
        /// 当前选择的表格数据
        /// </summary>
        object selectedGridRowData = null;

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
        TreeDataModel treeDataModel;

        /// <summary>
        /// 设计单据状态
        /// </summary>
        VoucherStatusModel voucherDesignStatusModel;

        /// <summary>
        /// 引用档案控件
        /// </summary>
        List<GridlookUpEditModel> basicInfoControls = new List< GridlookUpEditModel>();

        /// <summary>
        /// 单据状态控件
        /// </summary>
        List<GridlookUpEditModel> voucherStatusControls = new List<GridlookUpEditModel>();

        /// <summary>
        /// 
        /// </summary>
        //List<AGridlookUpEditModel> basicInfoFieldControls = new List<AGridlookUpEditModel>();
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
        private string formID = "LiManageVoucherDesign";
        #endregion

        #region 引用数据源
        private StatusModel statusModel = new StatusModel();
        private TableModel tableModel = new TableModel();
        private DictGroupModel dictGroupModel = new DictGroupModel();
        private ColumnModel columnModel = new ColumnModel();
        private GridlookUpEditModel gridlookUpEditModel = new GridlookUpEditModel();
        #endregion

        public LiManageVoucherDesign(FormModel formModel)
        {
            InitializeComponent();
            this.formModel = formModel;

            Init();
        }

        private void LiManageVoucherDesign_Load(object sender, EventArgs e)
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
            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, "name", "name", ImageUtil.ImageListModel.getSearchColumns(), ImageUtil.ImageListModel.getDisplayColumns(), ImageUtil.ImageListModel.getDictModelDesc(), repositoryItemGridLookUpEdit_icon1, this, ImageUtil.images);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, "name", "name", ImageUtil.ImageListModel.getSearchColumns(), ImageUtil.ImageListModel.getDisplayColumns(), ImageUtil.ImageListModel.getDictModelDesc(), repositoryItemGridLookUpEdit_Icon, this, ImageUtil.images);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, statusModel.getValueMember(), statusModel.getDisplayMember(), statusModel.getSearchColumns(), statusModel.getDisplayColumns(), statusModel.getDictModelDesc(), repositoryItemGridLookUpEdit_previousVoucherStatus, this, voucherDesignStatusModel.dataStatuss);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, statusModel.getValueMember(), statusModel.getDisplayMember(), statusModel.getSearchColumns(), statusModel.getDisplayColumns(), statusModel.getDictModelDesc(), repositoryItemGridLookUpEdit_voucherStatus, this, voucherDesignStatusModel.dataStatuss);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, statusModel.getValueMember(), statusModel.getDisplayMember(), statusModel.getSearchColumns(), statusModel.getDisplayColumns(), statusModel.getDictModelDesc(), repositoryItemGridLookUpEdit_voucherStatusList, this, voucherDesignStatusModel.dataStatuss);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoKey, this, tableModel.getDataSource<List<TableModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, dictGroupModel.getValueMember(), dictGroupModel.getDisplayMember(), dictGroupModel.getSearchColumns(), dictGroupModel.getDisplayColumns(), dictGroupModel.getDictModelDesc(), repositoryItemGridLookUpEdit_dictInfoType, this, dictGroupModel.getDataSource<List<DictGroupModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoShowFieldName, this, columnModel.getDataSource<List<ColumnModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(), gridlookUpEditModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoAssistType, this, basicInfoControls);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_basicInfoAssistFieldName, this, columnModel.getDataSource<List<ColumnModel>>());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(), gridlookUpEditModel.getDictModelDesc(), repositoryItemGridLookUpEdit_statusFieldName, this, voucherStatusControls);

        }

        /// <summary>
        /// /初始化数据
        /// </summary>
        private void InitData()
        {

            List<VoucherStatusModel> voucherDesignStatusModels = getEntitys_VoucherStatusModel(formModel.name);
            if (voucherDesignStatusModels.Count <= 0)
            {
                voucherDesignStatusModel = new VoucherStatusModel() { code = formModel.name, name = formModel.text, dataStatuss = new List<StatusModel>() };
            }
            else
            {
                voucherDesignStatusModel = voucherDesignStatusModels[0];
            }

            tableModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>("Basic", "entityType"));
            dictGroupModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.DictGroup, LiContext.SystemCode).getEntityList<DictGroupModel>());
            
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
            loadpropertyGridControl(formModel, null, propertyGridControl1);
            loadForm(formModel);
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
        public void loadForm(FormModel formModel)
        {
            if (formModel == null) return;
            gridControl1.DataSource = null;
            gridControl2.DataSource = null;
            gridControl3.DataSource = null;
            gridControl4.DataSource = null;
            gridControl5.DataSource = null;
            gridControl6.DataSource = null;
            gridControl7.DataSource = null;
            gridControl17.DataSource = null;

            loadGridData(formModel.panels, gridView1, gridControl1);

            loadGridData(formModel.buttonGroups, gridView5, gridControl5);

            loadGridData(formModel.events, gridView6, gridControl6);

            loadGridData(formModel.listButtons, gridView7, gridControl7);

            if (formModel.panels.Count > 0)
            {
                loadGridData(formModel.panels[0].controlGroups, gridView2, gridControl2);

                if (formModel.panels[0].controlGroups.Count > 0)
                {
                    loadGridData(formModel.panels[0].controlGroups[0].controls, gridView3, gridControl3);
                }
                else
                {
                    loadGridData(null, gridView3, gridControl3);
                }

            }
            else
            {
                loadGridData(null, gridView2, gridControl2);
            }


            if (formModel.buttonGroups.Count > 0)
            {
                loadGridData(formModel.buttonGroups[0].buttons, gridView4, gridControl4);
            }
            else
            {
                loadGridData(null, gridView4, gridControl4);
            }

        }

        public void loadPanelGrid(PanelModel panelModel)
        {
            if (panelModel == null) return;

            gridControl2.DataSource = null;
            gridControl3.DataSource = null;
            gridControl4.DataSource = null;
            gridControl5.DataSource = null;
            gridControl6.DataSource = null;

            loadGridData(panelModel.controlGroups, gridView2, gridControl2);
            loadGridData(panelModel.buttonGroups, gridView5, gridControl5);
            loadGridData(panelModel.events, gridView6, gridControl6);

            if (panelModel.controlGroups.Count > 0)
            {
                loadGridData(panelModel.controlGroups[0].controls, gridView3, gridControl3);
            }
            else
            {
                loadGridData(null, gridView3, gridControl3);
            }


            if (panelModel.buttonGroups.Count > 0)
            {
                loadGridData(panelModel.buttonGroups[0].buttons, gridView4, gridControl4);
            }
            else
            {
                loadGridData(null, gridView4, gridControl4);
            }

        }

        public void loadControlGroupGrid(ControlGroupModel controlGroupModel)
        {
            if (controlGroupModel == null) return;

            gridControl3.DataSource = null;
            gridControl4.DataSource = null;
            gridControl5.DataSource = null;
            gridControl6.DataSource = null;

            loadGridData(controlGroupModel.controls, gridView3, gridControl3);
            loadGridData(controlGroupModel.buttonGroups, gridView5, gridControl5);
            loadGridData(controlGroupModel.events, gridView6, gridControl6);


            if (controlGroupModel.buttonGroups.Count > 0)
            {
                loadGridData(controlGroupModel.buttonGroups[0].buttons, gridView4, gridControl4);
            }
            else
            {
                loadGridData(null, gridView4, gridControl4);
            }

        }

        public void loadButtonGroupGrid(ButtonGroupModel buttonGroupModel)
        {
            if (buttonGroupModel == null) return;

            gridControl4.DataSource = null;

            loadGridData(buttonGroupModel.buttons, gridView4, gridControl4);

        }

        public void loadButtonGrid(ButtonModel buttonModel)
        {
            if (buttonModel == null) return;

            gridControl6.DataSource = null;

            loadGridData(buttonModel.events, gridView6, gridControl6);

        }

        public void loadControlGrid(ControlModel controlModel)
        {
            if (controlModel == null) return;

            gridControl17.DataSource = null;

            loadGridData(controlModel.controlEvents, gridView17, gridControl17);

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
        public void loadGridData( object lists,GridView gridView, GridControl gridControl)
        {
            gridControl.DataSource = lists;
            gridView.BestFitColumns();
            gridView.RefreshData();
        }

        #endregion

        /// <summary>
        /// 初始化表格
        /// </summary>
        /// <param name="gridColumnDict"></param>
        /// <param name="gridView"></param>
        public void InitGrid( Dictionary<string, string> gridColumnDict, GridView gridView)
        {
            gridView.Columns.Clear();
            //加载列
            int iIndex = 1;
            foreach (KeyValuePair<string, string> kvp in gridColumnDict)
            {
                GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumn.Caption = kvp.Value;
                gridColumn.Name = string.Format("{0}{1}", gridView1.Name, kvp.Key);
                gridColumn.FieldName = kvp.Key;
                gridColumn.Visible = true;
                gridColumn.VisibleIndex = iIndex++;
                gridColumn.Width = 100;


                gridView.Columns.Add(gridColumn);
            }
        }

        #region 表格单击事件
        private void gridView1_Click(object sender, EventArgs e)
        {
            PanelModel panelModel = (PanelModel)gridView1.GetFocusedRow();

            loadPanelGrid(panelModel);
            loadpropertyGridControl(panelModel, gridView1, propertyGridControl2);
        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            ControlGroupModel controlGroupModel = (ControlGroupModel)gridView2.GetFocusedRow();
            loadControlGroupGrid(controlGroupModel);


            loadpropertyGridControl(controlGroupModel, gridView2, propertyGridControl2);
        }

        private void gridView5_Click(object sender, EventArgs e)
        {
            ButtonGroupModel buttonGroupModel = (ButtonGroupModel)gridView5.GetFocusedRow();
            loadButtonGroupGrid(buttonGroupModel);

            loadpropertyGridControl(buttonGroupModel, gridView5, propertyGridControl2);
        }

        private void gridView4_Click(object sender, EventArgs e)
        {
            ButtonModel buttonModel = (ButtonModel)gridView4.GetFocusedRow();
            loadButtonGrid(buttonModel);

            loadpropertyGridControl(buttonModel, gridView4, propertyGridControl2);
        }

        private void gridView7_Click(object sender, EventArgs e)
        {
            ListButtonModel listButtonModel = (ListButtonModel)gridView7.GetFocusedRow();
            loadListButtonGrid(listButtonModel);

            loadpropertyGridControl(listButtonModel, gridView7, propertyGridControl2);
        }

        private void gridView3_Click(object sender, EventArgs e)
        {
            ControlModel controlModel = (ControlModel)gridView3.GetFocusedRow();
            loadControlGrid(controlModel);

            loadpropertyGridControl(controlModel, gridView3, propertyGridControl2);
        }

        private void gridView6_Click(object sender, EventArgs e)
        {
            EventModel eventModel = (EventModel)gridView6.GetFocusedRow();

            loadpropertyGridControl(eventModel, gridView6, propertyGridControl2);

        }

        #endregion

        #region 表格属性
        /// <summary>
        /// 单据头右键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertyGridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ModifierKeys == Keys.None)
            {
                Point p = new Point(Cursor.Position.X, Cursor.Position.Y);
                selectedGridRowData = formModel;
                popupFormMenu.ShowPopup(p);
            }
        }

        /// <summary>
        /// 单击表头，加载数据到其他表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertyGridControl1_Click(object sender, EventArgs e)
        {
            loadForm(formModel);
            loadpropertyGridControl(formModel, null, propertyGridControl2);
        }

        /// <summary>
        /// 属性表格值变更，更新到对应的表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertyGridControl2_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            PropertyGridControl propertyGridControl = (PropertyGridControl)sender;
            GridView gridView = (GridView)propertyGridControl.Tag;
            if (gridView != null)
            {
                gridView.RefreshData();
            }
        }
        #endregion

        #region 增加事件
        private void btnAddPanel_ItemClick(object sender, ItemClickEventArgs e)
        {
            PanelModel panelModel = PanelModel.getInstance(formModel.id);
            DevControlUtil.addRowInGridView<PanelModel>(panelModel, gridControl1);
            gridView1.RefreshData();
        }

        private void btnAddButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (selectedGridRowData != null && selectedGridRowData.GetType().Name == "ButtonGroupModel")
            {
                ButtonGroupModel buttonGroupModel = (ButtonGroupModel)selectedGridRowData;
                ButtonModel buttonModel = ButtonModel.getInstance(buttonGroupModel.id, buttonGroupModel.buttons.Count<=0 ? Guid.NewGuid().ToString() : buttonGroupModel.buttons[0].categoryGuid);
                DevControlUtil.addRowInGridView<ButtonModel>(buttonModel, gridControl4);
                gridView4.RefreshData();
            }
        }

        private void btnAddControlGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (selectedGridRowData != null && selectedGridRowData.GetType().Name == "PanelModel")
            {
                PanelModel panelModel = (PanelModel)selectedGridRowData;
                ControlGroupModel controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
                DevControlUtil.addRowInGridView<ControlGroupModel>(controlGroupModel, gridControl2);
                gridView2.RefreshData();
            }
        }

        private void btnAddButtonGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            ButtonGroupModel buttonGroupModel;

            if (selectedGridRowData != null)
            {
                switch (selectedGridRowData.GetType().Name)
                {

                    case "FormModel":
                        FormModel formModel = (FormModel)selectedGridRowData;
                        buttonGroupModel = ButtonGroupModel.getInstance(formModel.id, 0, 0);
                        DevControlUtil.addRowInGridView<ButtonGroupModel>(buttonGroupModel, gridControl5);
                        break;
                    case "PanelModel":
                        PanelModel panelModel = (PanelModel)selectedGridRowData;
                        buttonGroupModel = ButtonGroupModel.getInstance(0, panelModel.id, 0);
                        DevControlUtil.addRowInGridView<ButtonGroupModel>(buttonGroupModel, gridControl5);
                        break;
                    case "ControlGroupModel":
                        ControlGroupModel controlGroupModel = (ControlGroupModel)selectedGridRowData;
                        buttonGroupModel = ButtonGroupModel.getInstance(0, 0, controlGroupModel.id);
                        DevControlUtil.addRowInGridView<ButtonGroupModel>(buttonGroupModel, gridControl5);
                        break;
                }
                gridView5.RefreshData();
            }
        }

        private void btnAddControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (selectedGridRowData != null && selectedGridRowData.GetType().Name == "ControlGroupModel")
            {
                ControlGroupModel controlGroupModel = (ControlGroupModel)selectedGridRowData;
                ControlModel controlModel = ControlModel.getInstance(controlGroupModel.id);
                DevControlUtil.addRowInGridView<ControlModel>(controlModel, gridControl3);
                gridView3.RefreshData();
            }
        }
        #endregion

        #region 删除事件
        private void deleteGridRow(object deleteValue, GridControl gridControl)
        {
            if (deleteValue != null)
            {
                switch (deleteValue.GetType().Name)
                {
                    case "PanelModel":
                        DevControlUtil.deleteRowInGridView<PanelModel>((PanelModel)deleteValue, gridControl);
                        break;
                    case "ControlGroupModel":
                        DevControlUtil.deleteRowInGridView<ControlGroupModel>((ControlGroupModel)deleteValue, gridControl);
                        break;
                    case "ButtonGroupModel":
                        DevControlUtil.deleteRowInGridView<ButtonGroupModel>((ButtonGroupModel)deleteValue, gridControl);
                        break;
                    case "ControlModel":
                        DevControlUtil.deleteRowInGridView<ControlModel>((ControlModel)deleteValue, gridControl);
                        break;
                    case "ButtonModel":
                        DevControlUtil.deleteRowInGridView<ButtonModel>((ButtonModel)deleteValue, gridControl);
                        break;
                    case "EventModel":
                        DevControlUtil.deleteRowInGridView<EventModel>((EventModel)deleteValue, gridControl);
                        break;
                }
            }
        }

        private void btnDeletePanel_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl1);
            gridView1.RefreshData();
        }

        private void btnDeleteButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl4);
            gridView4.RefreshData();
        }

        private void btnDeleteControlGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl2);
            gridView2.RefreshData();
        }

        private void btnDeleteButtonGroup_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl5);
            gridView5.RefreshData();
        }

        private void btnDeleteControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl3);
            gridView3.RefreshData();
        }
        #endregion

        #region 右键菜单
        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupPanelMenu, gridView1, e);
        }

        private void gridControl2_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupControlGroupMenu, gridView2, e);
        }

        private void gridControl5_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupButtonGroupMenu, gridView5, e);
        }

        private void gridControl3_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupControlMenu, gridView3, e);
        }

        private void gridControl4_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupButtonMenu, gridView4, e);
        }

        private void gridView6_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupEventMenu, gridView6, e);
        }

        private void gridView7_MouseUp(object sender, MouseEventArgs e)
        {
            showPopupGridMenu(popupListButtonMenu, gridView7, e);
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
        #endregion

        #region 其他按钮事件
        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            formModel = FormModel.getInstance();
            loadData();
            setNewStatus();
            setFormStatus(formStatusCode);
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadData();
            setEditStatus();
            setFormStatus(formStatusCode);
        }

        private void handleGridlookUpEditShowModel()
        {
            foreach (PanelModel panelModel in formModel.panels)
            {
                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {

                        switch (controlModel.controltype)
                        {
                            case "UserEdit":
                            case "GridLookUpEditRef":
                                TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == controlModel.basicInfoKey).FirstOrDefault();
                         

                                controlModel.gridlookUpEditShowModelJson = DevControlUtil.getGridLookUpEditRefInfo(tableModelTemp, controlModel.basicInfoShowMode, controlModel.basicInfoTableKey, controlModel.basicInfoShowFieldName);
                                break;
                            case "GridLookUpEditComboBox":


                                controlModel.gridlookUpEditShowModelJson = DevControlUtil.getGridLookUpEditDictInfo();
                                break;
                        }
                    }
                }
            }
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {

            bool bSuccess = false;

            string resultContent = string.Empty;

            handleGridlookUpEditShowModel();

            if (formModel.id > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).updateEntity(formModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).tipStr, "温馨提示");

            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).newEntity(formModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).tipStr, "温馨提示");

            }

            LiContext.getFormModelList(formModel.name, LiContext.SystemCode);
            loadData();

            formStatusCode = "ShowStatus";
            setFormStatus(formStatusCode);

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

            LiSetReadOnlyForm form = new LiSetReadOnlyForm(newControlStatusModels,userControls, dateControls,statusControls, voucherStatusModel);
            form.Show();
        }

        #endregion

        private void btngAddGneralEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiGeneralEventForm frm = new LiGeneralEventForm();
            if (DialogResult.Yes == frm.ShowDialog())
            {
                List<GeneralEventModel> drs = frm.SelectRows;

                EventModel eventModel;
                foreach (GeneralEventModel model in drs)
                {
                    switch (selectedGridRowData.GetType().Name)
                    {

                        case "FormModel":
                            FormModel formModel = (FormModel)selectedGridRowData;
                            eventModel = EventModel.getInstance(formModel.id, 0, 0, 0, 0);
                            break;
                        case "PanelModel":
                            PanelModel panelModel = (PanelModel)selectedGridRowData;
                            eventModel = EventModel.getInstance(0, panelModel.id, 0, 0, 0);
                            break;
                        case "ControlGroupModel":
                            ControlGroupModel controlGroupModel = (ControlGroupModel)selectedGridRowData;
                            eventModel = EventModel.getInstance(0, 0, controlGroupModel.id, 0, 0);
                            break;
                        case "ButtonModel":
                            ButtonModel buttonModel = (ButtonModel)selectedGridRowData;
                            eventModel = EventModel.getInstance(0, 0, 0, buttonModel.id, 0);
                            break;
                        case "ListButtonModel":
                            ListButtonModel listButtonModel = (ListButtonModel)selectedGridRowData;
                            eventModel = EventModel.getInstance(0, 0, 0, 0, listButtonModel.id);
                            break;
                        default:
                            eventModel = EventModel.getInstance(0, 0, 0, 0, 0);
                            break;
                    }
                    eventModel.assemblyName = model.eventAssemblyName;
                    eventModel.fullName = model.eventFullName;

                    DevControlUtil.addRowInGridView<EventModel>(eventModel, gridControl6);
                }

                gridControl6.RefreshDataSource();
            }
        }

        private void btnAddPluginEvent_ItemClick(object sender, ItemClickEventArgs e)
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

                    EventModel eventModel;
                    foreach (DataRow dr in drs)
                    {
                        switch (selectedGridRowData.GetType().Name)
                        {

                            case "FormModel":
                                FormModel formModel = (FormModel)selectedGridRowData;
                                eventModel = EventModel.getInstance(formModel.id, 0, 0, 0, 0);
                                break;
                            case "PanelModel":
                                PanelModel panelModel = (PanelModel)selectedGridRowData;
                                eventModel = EventModel.getInstance(0, panelModel.id, 0, 0, 0);
                                break;
                            case "ControlGroupModel":
                                ControlGroupModel controlGroupModel = (ControlGroupModel)selectedGridRowData;
                                eventModel = EventModel.getInstance(0, 0, controlGroupModel.id, 0, 0);
                                break;
                            case "ButtonModel":
                                ButtonModel buttonModel = (ButtonModel)selectedGridRowData;
                                eventModel = EventModel.getInstance(0, 0, 0, buttonModel.id, 0);
                                break;
                            case "ListButtonModel":
                                ListButtonModel listButtonModel = (ListButtonModel)selectedGridRowData;
                                eventModel = EventModel.getInstance(0, 0, 0, 0, listButtonModel.id);
                                break;
                            default:
                                eventModel = EventModel.getInstance(0, 0, 0, 0, 0);
                                break;
                        }
                        eventModel.assemblyName = Convert.ToString(dr["eventAssemblyName"]);
                        eventModel.fullName = Convert.ToString(dr["eventFullName"]);

                        DevControlUtil.addRowInGridView<EventModel>(eventModel, gridControl6);
                    }

                }
                gridControl6.RefreshDataSource();
            }
        }

        private void btnDeleteEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl6);
            gridView6.RefreshData();
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<VoucherStatusModel> getEntitys_VoucherStatusModel(string key)
        {
            //string resultContent;
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "liVoucherStatus");
            //paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single("code", key));

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            //{
            //    return JsonUtil.GetEntityToList<VoucherStatusModel>(resultContent);
            //}

            //return null;
            return LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, LiContext.SystemCode).getEntityList<VoucherStatusModel>(key, "code");
        }

        /// <summary>
        /// 状态设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVoucherStatus_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            List<ControlStatusModel> newControlStatusModels = new List<ControlStatusModel>();
            //设置新的控件状态
            foreach (PanelModel panelModel in formModel.panels)
            {
                string panelType = panelModel.type;
                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {

                    foreach (ButtonGroupModel buttonGroupModel in controlGroupModel.buttonGroups)
                    {
                        foreach (ButtonModel buttonModel in buttonGroupModel.buttons)
                        {
                            newControlStatusModels.Add(new ControlStatusModel() { code = buttonModel.name, name = buttonModel.caption, groupName = "按钮", bReadOnly = false, bVisibe = true });
                        }

                    }

                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {
                        if (panelType == "Basic")
                        {
                            newControlStatusModels.Add(new ControlStatusModel() { code = controlModel.name, name = controlModel.text, groupName = "表头", bReadOnly = false, bVisibe = true });
                        }
                        else
                        {
                            newControlStatusModels.Add(new ControlStatusModel() { code = controlModel.name, name = controlModel.text, groupName = "表体", bReadOnly = false, bVisibe = true });
                        }
                    }

                }

                foreach (ButtonGroupModel buttonGroupModel in panelModel.buttonGroups)
                {

                    foreach (ButtonModel buttonModel in buttonGroupModel.buttons)
                    {
                        newControlStatusModels.Add(new ControlStatusModel() { code = buttonModel.name, name = buttonModel.caption, groupName = "按钮", bReadOnly = false, bVisibe = true });
                    }
                }
            }

            foreach (ButtonGroupModel buttonGroupModel in formModel.buttonGroups)
            {

                foreach (ButtonModel buttonModel in buttonGroupModel.buttons)
                {
                    newControlStatusModels.Add(new ControlStatusModel() { code = buttonModel.name, name = buttonModel.caption, groupName = "按钮", bReadOnly = false, bVisibe = true });
                }
            }

            getControlsToDataSource();
            LiSetReadOnlyForm form = new LiSetReadOnlyForm(newControlStatusModels,userControls,dateControls,statusControls, voucherDesignStatusModel);
            form.Show();
        }

        private void btnAddListButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (formModel.listButtons == null) formModel.listButtons = new List<ListButtonModel>();

            ListButtonModel listButtonModel = ListButtonModel.getInstance(formModel.id, formModel.listButtons.Count <= 0 ? Guid.NewGuid().ToString() : formModel.listButtons[0].categoryGuid);
            DevControlUtil.addRowInGridView<ListButtonModel>(listButtonModel, gridControl7);
            gridView7.RefreshData();
        }

        private void btnDeleteListButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl7);
            gridView7.RefreshData();
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            ControlModel controlModel = gridView3.GetFocusedRow() as ControlModel;

            switch (e.Column.FieldName)
            {
                case "basicInfoKey":
                    TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == Convert.ToString(e.Value)).FirstOrDefault();
                    if (tableModelTemp != null)
                    {
                        controlModel.basicInfoTableKey = tableModelTemp.keyName;
                        repositoryItemGridLookUpEdit_basicInfoShowFieldName.DataSource = tableModelTemp.datas;
                    }
                    else
                    {
                        controlModel.basicInfoTableKey = string.Empty;
                        repositoryItemGridLookUpEdit_basicInfoShowFieldName.DataSource = columnModel.getDataSource<List<ColumnModel>>();
                    }
                    break;
            }

            gridView1.RefreshRow(e.RowHandle);
        }

        private void repositoryItemGridLookUpEdit_basicInfoShowFieldName_BeforePopup(object sender, EventArgs e)
        {
            ControlModel controlModel = gridView3.GetFocusedRow() as ControlModel;
            TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == controlModel.basicInfoKey).FirstOrDefault();
            if (tableModelTemp != null)
            {
                repositoryItemGridLookUpEdit_basicInfoShowFieldName.DataSource = tableModelTemp.datas;
            }
            else
            {
                repositoryItemGridLookUpEdit_basicInfoShowFieldName.DataSource = columnModel.getDataSource<List<ColumnModel>>();
            }
        }

        private void repositoryItemGridLookUpEdit_basicInfoAssistType_BeforePopup(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEdit_basicInfoAssistType.DataSource = getBasicInfoControls();

        }

        /// <summary>
        /// 获取当前表格引用档案控件
        /// </summary>
        /// <returns></returns>
        public List<GridlookUpEditModel> getBasicInfoControls()
        {
            basicInfoControls.Clear();
            List<ControlModel> controlList = gridControl3.DataSource as List<ControlModel>;
            if (controlList != null)
            {
                foreach (ControlModel control in controlList)
                {
                    basicInfoControls.Add(new GridlookUpEditModel() { code = control.name, name = control.text });
                }
            }
            return basicInfoControls;

        }

        /// <summary>
        /// 获取当前表格单据状态控件
        /// </summary>
        /// <returns></returns>
        public List<GridlookUpEditModel> getVoucherStatusControls()
        {
            voucherStatusControls.Clear();
            List<ControlModel> controlList = gridControl3.DataSource as List<ControlModel>;
            if (controlList != null)
            {
                foreach (ControlModel control in controlList)
                {
                    voucherStatusControls.Add(new GridlookUpEditModel() { code = control.name, name = control.text });
                }
            }
            return voucherStatusControls;

        }

        /// <summary>
        /// 获取控件做为数据源
        /// </summary>
        private void getControlsToDataSource()
        {
            userControls.Clear();
            dateControls.Clear();
            statusControls.Clear();
            foreach (PanelModel panelModel in formModel.panels)
            {
                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {
                        switch (controlModel.controltype)
                        {
                            case "UserEdit":
                                userControls.Add(new GridlookUpEditModel() { code = controlModel.name, name = controlModel.text });
                                break;
                            case "DateEdit":
                                dateControls.Add(new GridlookUpEditModel() { code = controlModel.name, name = controlModel.text });
                                break;
                            case "StatusEdit":
                                statusControls.Add(new GridlookUpEditModel() { code = controlModel.name, name = controlModel.text });
                                break;
                        }
                    }
                }
            }

        }

        private void repositoryItemGridLookUpEdit_basicInfoAssistFieldName_BeforePopup(object sender, EventArgs e)
        {
            List<ControlModel> controlList = gridControl3.DataSource as List<ControlModel>;
            ControlModel controlModel = gridView3.GetFocusedRow() as ControlModel;

            ControlModel sourceControlModel = controlList.Where(m => m.name == controlModel.basicInfoAssistType).FirstOrDefault();
            if (sourceControlModel != null)
            {
                TableModel tableModelTemp = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == sourceControlModel.basicInfoKey).FirstOrDefault();
                if (tableModelTemp != null)
                {
                    repositoryItemGridLookUpEdit_basicInfoAssistFieldName.DataSource = tableModelTemp.datas;
                    controlModel.basicInfoKey = sourceControlModel.basicInfoKey;
                }
                else
                {
                    repositoryItemGridLookUpEdit_basicInfoAssistFieldName.DataSource = columnModel.getDataSource<List<ColumnModel>>();
                    controlModel.basicInfoKey = "";
                }

            }
            else
            {
                repositoryItemGridLookUpEdit_basicInfoAssistFieldName.DataSource = columnModel.getDataSource<List<ColumnModel>>();
                controlModel.basicInfoKey = "";
            }
        }

        private void btnVoucherCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiVoucherCodeForm liVoucherCodeForm = new LiVoucherCodeForm(formModel);
            liVoucherCodeForm.ShowDialog();
        }

        private void btnPublish_ItemClick(object sender, ItemClickEventArgs e)
        {
            Dictionary<string, object> paramDict  = new Dictionary<string,object>();
            paramDict.Add("formId", formModel.id);
            LiContexts.LiContext.getHttpEntity("sp_DeployLiSystem").execProcedureNoResult( paramDict);

            if (LiContexts.LiContext.getHttpEntity("sp_DeployLiSystem").bSuccess)
            {
                MessageUtil.Show("发布成功", "系统提示");
            }
            else
            {
                MessageUtil.Show("发布失败:" + LiContexts.LiContext.getHttpEntity("sp_CreateTable").resultContent, "系统提示");
            }
        }

        private void btnAddVoucherCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getVoucherCode());
        }

        public void addControl(ControlModel controlModel)
        {
            if (selectedGridRowData != null && selectedGridRowData.GetType().Name == "ControlGroupModel")
            {
                ControlGroupModel controlGroupModel = (ControlGroupModel)selectedGridRowData;
                controlModel.controlGroupId = controlGroupModel.id;
                DevControlUtil.addRowInGridView<ControlModel>(controlModel, gridControl3);
                gridView3.RefreshData();
            }
        }

        private void btnAddMaker_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getUserControlMaker());
        }

        private void btnAddMakeDate_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getDateControlMakerDate());
        }

        private void btnAddModifer_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getUserControlModifer());
        }

        private void btnAddModifyDate_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getDateControlModifyDate());
        }

        private void btnAddSumbit_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getUserControlSubmit());
        }

        private void btnAddSumbitDate_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getDateControlSubmitDate());
        }

        private void btnAddAuditor_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getUserControlAuditor());
        }

        private void btnAddAuditDate_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getDateControlAuditDate());
        }

        private void btnAddUserControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getUserControl());
        }

        private void btnAddDateControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getDateControl());
        }

        private void repositoryItemGridLookUpEdit_statusFieldName_BeforePopup(object sender, EventArgs e)
        {
            repositoryItemGridLookUpEdit_statusFieldName.DataSource = getVoucherStatusControls();
        }

        public void addButton(string buttonType, string entityKey = "")
        {

            if (selectedGridRowData != null && selectedGridRowData.GetType().Name == "ButtonGroupModel")
            {
                ButtonGroupModel buttonGroupModel = (ButtonGroupModel)selectedGridRowData;
                ButtonModel buttonModel = ButtonFactory.getFormButtonModel(buttonType, buttonGroupModel.id, buttonGroupModel.buttons.Count <= 0 ? Guid.NewGuid().ToString() : buttonGroupModel.buttons[0].categoryGuid, entityKey);

                DevControlUtil.addRowInGridView<ButtonModel>(buttonModel, gridControl4);
                gridView4.RefreshData();
            }
            else if (selectedGridRowData != null && selectedGridRowData.GetType().Name == "FormModel")
            {
                if (formModel.listButtons == null) formModel.listButtons = new List<ListButtonModel>();

                ListButtonModel listButtonModel = ButtonFactory.getListButtonModel(buttonType, formModel.id, formModel.listButtons.Count <= 0 ? Guid.NewGuid().ToString() : formModel.listButtons[0].categoryGuid, entityKey);
                DevControlUtil.addRowInGridView<ListButtonModel>(listButtonModel, gridControl7);
                gridView7.RefreshData();
            }
        }

        private void BtnNEWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNNEW);
        }

        private void BtnEDITButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNEDIT);
        }

        private void BtnDELETE_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNDELETE);
        }

        private void BtnSAVEButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNSAVE);
        }

        private void BtnSUBMITButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNSUBMIT);
        }

        private void BtnUNSUBMITButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNUNSUBMIT);
        }

        private void BtnAUDITButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNAUDIT);
        }

        private void BtnUNAUDITButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNUNAUDIT);
        }

        private void BtnREFButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNREF);
        }

        private void BtnPUSHButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNPUSH);

        }

        private void BtnEXITButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNEXIT);
        }

        private void BtnADDROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNADDROW);
        }

        private void BtnDELETEROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNDELETEROW);
        }

        private void BtnINSERTROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNINSERTROW);
        }

        private void BtnCOPYROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNCOPYROW);
        }

        private void BtnINSERTCOPYROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNINSERTCOPYROW);
        }

        private void BtnADDCOPYROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNADDCOPYROW);
        }

        private void BtnSINGLEVOUCHER_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (formModel.listButtons.Count > 0 || formModel.panels.Count > 0 || formModel.buttonGroups.Count > 0)
            {
                if( MessageUtil.ShowMsgBox("是否清空数据，重置模板数据？","温馨提示", MsgType.YesNo) == DialogResult.Yes)
                {
                    formModel.listButtons.Clear();
                    formModel.panels.Clear();
                    formModel.buttonGroups.Clear();
                }
                else
                {
                    return;
                }
            }
            FormTemplateFactory.getFormModel_SingleVoucher(formModel);
        }

        private void BtnMVVOUCHER_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (formModel.listButtons.Count > 0 || formModel.panels.Count > 0 || formModel.buttonGroups.Count > 0)
            {
                if (MessageUtil.ShowMsgBox("是否清空数据，重置模板数据？", "温馨提示", MsgType.YesNo) == DialogResult.Yes)
                {
                    formModel.listButtons.Clear();
                    formModel.panels.Clear();
                    formModel.buttonGroups.Clear();
                }
                else
                {
                    return;
                }
            }
            FormTemplateFactory.getFormModel_MSVoucher(formModel);
        }

        private void BtnBASEINFO_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (formModel.listButtons.Count > 0 || formModel.panels.Count > 0 || formModel.buttonGroups.Count > 0)
            {
                if (MessageUtil.ShowMsgBox("是否清空数据，重置模板数据？", "温馨提示", MsgType.YesNo) == DialogResult.Yes)
                {
                    formModel.listButtons.Clear();
                    formModel.panels.Clear();
                    formModel.buttonGroups.Clear();
                }
                else
                {
                    return;
                }
            }
            FormTemplateFactory.getFormModel_BasicInfo(formModel);
        }

        private void BtnTREEBASEINFO_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (formModel.listButtons.Count > 0 || formModel.panels.Count > 0 || formModel.buttonGroups.Count > 0)
            {
                if (MessageUtil.ShowMsgBox("是否清空数据，重置模板数据？", "温馨提示", MsgType.YesNo) == DialogResult.Yes)
                {
                    formModel.listButtons.Clear();
                    formModel.panels.Clear();
                    formModel.buttonGroups.Clear();
                }
                else
                {
                    return;
                }
            }

            FormTemplateFactory.getFormModel_TreeBasicInfo(formModel);
        }

        private void BtnUPROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNUPROW);
        }

        private void BtnDOWNROWButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNDOWNROW);
        }

        private void BtnUpRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (selectedGridRowData != null)
            {
                switch (selectedGridRowData.GetType().Name)
                {
                    case "ButtonModel":
                        DevControlUtil.UpRow<ButtonModel>(gridView4);
                        gridControl4.RefreshDataSource();
                        ResetButtonModelIndex(gridControl4);
                        break;
                    case "ListButtonModel":
                        DevControlUtil.UpRow<ListButtonModel>(gridView7);
                        gridControl7.RefreshDataSource();
                        ResetListButtonModelIndex(gridControl7);
                        break;
                }
            }
        }

        private void BtnDownRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(selectedGridRowData!= null)
            {
                switch (selectedGridRowData.GetType().Name)
                {
                    case "ButtonModel":
                        DevControlUtil.DownRow<ButtonModel>(gridView4);
                        gridControl4.RefreshDataSource();
                        ResetButtonModelIndex(gridControl4);
                        break;
                    case "ListButtonModel":
                        DevControlUtil.DownRow<ListButtonModel>(gridView7);
                        gridControl7.RefreshDataSource();
                        ResetListButtonModelIndex(gridControl7);
                        break;
                }
            }
        }

        /// <summary>
        /// 重置按钮索引
        /// </summary>
        /// <param name="gridControl"></param>
        private void ResetButtonModelIndex(GridControl gridControl)
        {
            int iRow = 1;
            List<ButtonModel> buttonModels = gridControl.DataSource as List<ButtonModel>;
            foreach (ButtonModel buttonModel in buttonModels)
            {
                buttonModel.iIndex = iRow++;
            }
        }

        /// <summary>
        /// 重置按钮索引
        /// </summary>
        /// <param name="gridControl"></param>
        private void ResetListButtonModelIndex(GridControl gridControl)
        {
            int iRow = 1;
            List<ListButtonModel> buttonModels = gridControl.DataSource as List<ListButtonModel>;
            foreach (ListButtonModel buttonModel in buttonModels)
            {
                buttonModel.iIndex = iRow++;
            }
        }
        private void BtnSelectAllButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNSELECTALL);
        }

        private void BtnReSelectButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNRESELECT);
        }

        private void BtnCancelSelectButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNCANCELSELECT);
        }

        private void BtnFirstButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNFIRST);
        }

        private void BtnPerviousButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNPREVIOUS);
        }

        private void BtnNextButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNNEXT);
        }

        private void BtnLastButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ButtonType.BTNLAST);
        }

        private void BtnListQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNQUERY);
        }

        private void BtnListPreciseQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNPRECISEQUERY);
        }

        private void BtnListRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNREFRESH);
        }

        private void BtnListNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNNEW);
        }

        private void BtnListEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNEDIT);
        }

        private void BtnListDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNDELETE);
        }

        private void BtnListSubmit_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNSUBMIT);
        }

        private void BtnListUnSubmit_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNUNSUBMIT);
        }

        private void BtnListAudit_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNAUDIT);
        }

        private void BtnListUnAudit_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNUNAUDIT);
        }

        private void BtnListExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNEXIT);
        }

        private void BtnListSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNSELECTALL);
        }

        private void BtnListReSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNRESELECT);
        }

        private void BtnListCancelSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNCANCELSELECT);
        }

        private void BtnU8Publish_ItemClick(object sender, ItemClickEventArgs e)
        {
            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            paramDict.Add("formId", formModel.id);
            LiContexts.LiContext.getHttpEntity("sp_DeployU8").execProcedureNoResult(paramDict);

            if (LiContexts.LiContext.getHttpEntity("sp_DeployU8").bSuccess)
            {
                MessageUtil.Show("发布成功", "系统提示");
            }
            else
            {
                MessageUtil.Show("发布失败:" + LiContexts.LiContext.getHttpEntity("sp_CreateTable").resultContent, "系统提示");
            }
        }

        private void BtnListPushButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNPUSH);
        }

        private void BtnListRefButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNREF);
        }

        private void BtnUpShowButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNUPSHOW);
        }

        private void BtnDownShowButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            addButton(ListButtonType.BTNDOWNSHOW);
        }

        private void BtnAddHeadSourceID_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getHeadSourceIDControl());
        }

        private void BtnAddHeadSourceCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getHeadSourceCodeControl());
        }

        private void BtnAddHeadSourceType_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getHeadSourceTypeControl());
        }

        private void BtnAddBodySourceID_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getBodySourceIDControl());
        }

        private void BtnAddBodySourceCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getBodySourceCodeControl());
        }

        private void BtnAddBodySourceType_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getBodySourceTypeControl());
        }

        private void BtnAddBodySourceQty_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getBodySourceQtyControl());
        }

        private void BtnAddBHeadSourceQty_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getHeadSourceQtyControl());
        }

        private void BtnAddHeadConvertCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getHeadConvertCodeControl());
        }

        private void BtnAddBodyConvertCode_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getBodyConvertCodeControl());
        }

        private void BtnAddQty_ItemClick(object sender, ItemClickEventArgs e)
        {
            addControl(ControlFactory.getQuantityControl());
        }

        private void BtnAddControlEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            ControlModel controlModel = (ControlModel)selectedGridRowData;
            ControlEventModel controlEventModel = ControlEventModel.getInstance(controlModel.id);

            DevControlUtil.addRowInGridView<ControlEventModel>(controlEventModel, gridControl17);

            gridControl17.RefreshDataSource();
        }

        private void BtnRemoveControlEvent_ItemClick(object sender, ItemClickEventArgs e)
        {
            deleteGridRow(selectedGridRowData, gridControl17);
            gridView17.RefreshData();
        }

        private void GridView17_DoubleClick(object sender, EventArgs e)
        {
            ControlEventModel controlEvent = gridView17.GetFocusedRow() as ControlEventModel;
            if (controlEvent == null) return;
            LiControlEventExpressionForm liForm = new LiControlEventExpressionForm(controlEvent, formModel);
            liForm.ShowDialog();

            gridView17.RefreshRow(gridView17.FocusedRowHandle);
        }
    }



}