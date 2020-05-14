using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Localization;
using DevExpress.LookAndFeel;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using LiForm.Dev.Util;

using LiForm.Event;
using LiForm.Event.Mediator;
using LiForm.Event.EventForm;

using LiModel.Form;
using LiCommon.Util;
using LiForm.LiStatus;
using LiHttp.Enum;
using LiContexts;
using LiHttp.GetEntity;
using LiModel.LiEnum;
using LiModel.Util;
using LiControl.Util;
using LiFlow.Model;
using LiFlow.Util;
using LiFlow.Enums;

namespace LiForm.Dev
{
    public partial class LiForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        /// <summary>
        /// 普通控件缓存
        /// </summary>
        private Dictionary<string, Control> _liControlDict = new Dictionary<string, Control>();

        /// <summary>
        /// 引用控件缓存
        /// </summary>
        private Dictionary<string, Control> _liControlRefDict = new Dictionary<string, Control>();

        /// <summary>
        /// 引用辅助控件缓存
        /// </summary>
        private Dictionary<string, List<Control>> _liControlRefAssistDict = new Dictionary<string, List<Control>>();


        /// <summary>
        /// 表格普通控件缓存
        /// </summary>
        private Dictionary<string, RepositoryItem> _liRepositoryItemDict = new Dictionary<string, RepositoryItem>();

        /// <summary>
        /// 表格引用控件缓存
        /// </summary>
        private Dictionary<string, RepositoryItem> _liRepositoryItemRefDict = new Dictionary<string, RepositoryItem>();

        /// <summary>
        /// 表格引用辅助控件
        /// </summary>
        private Dictionary<string, List<RepositoryItem>> _liRepositoryItemRefAssistDict = new Dictionary<string, List<RepositoryItem>>();

        /// <summary>
        /// 表格列
        /// </summary>
        private Dictionary<string, GridColumn> _liGridColumnDict = new Dictionary<string, GridColumn>();

        /// <summary>
        /// 表格引用控件列
        /// </summary>
        private Dictionary<string, GridColumn> _liGridColumnRefDict = new Dictionary<string, GridColumn>();

        /// <summary>
        /// 树形基础档案控件列
        /// </summary>
        private Dictionary<string, TreeListColumn> _liTreeListColumnDict = new Dictionary<string, TreeListColumn>();

        /// <summary>
        /// 表格引用辅助控件列
        /// </summary>
        private Dictionary<string, List<GridColumn>> _liGridColumnRefAssistDict = new Dictionary<string, List<GridColumn>>();

        /// <summary>
        /// 按钮
        /// </summary>
        private Dictionary<string, BarButtonItem> _liButtonDict = new Dictionary<string, BarButtonItem>();

        /// <summary>
        /// 表格控件
        /// </summary>
        private Dictionary<string, GridControl> _liGridControlDict = new Dictionary<string, GridControl>();

        /// <summary>
        /// 树形基础档案控件
        /// </summary>
        private Dictionary<string, TreeList> _liTreeListDict = new Dictionary<string, TreeList>();

        /// <summary>
        /// 表格控件视图
        /// </summary>
        private Dictionary<string, GridView> _liGridViewDict = new Dictionary<string, GridView>();

        /// <summary>
        /// 按钮事件中介
        /// </summary>
        private Dictionary<string, LiAEventMediator> _liEventMediatorDict = new Dictionary<string, LiAEventMediator>();

        /// <summary>
        /// 树形表格数据源
        /// </summary>
        private DataTable _formTreeData;

        /// <summary>
        /// 浮动窗口容器
        /// </summary>
        private Dictionary<string, DockPanel> _liDockPanelDict = new Dictionary<string, DockPanel>();

        /// <summary>
        /// 状态，用于只读控制
        /// </summary>
        private VoucherStatusModel _voucherStatusModel ;

        /// <summary>
        /// 浮动窗口保存事件
        /// </summary>
        public EventHandler dockPanelEditSaveEventHandler;
        /// <summary>
        /// 浮动窗口关闭事件
        /// </summary>
        public EventHandler dockPanelEditCloseEventHandler;

        public Dictionary<string, LiAEventMediator> liEventMediatorDict { set { _liEventMediatorDict = value; } get { return _liEventMediatorDict; } }
        public Dictionary<string, GridColumn> liGridColumnDict { set { _liGridColumnDict = value; } get { return _liGridColumnDict; } }
        public Dictionary<string, GridColumn> liGridColumnRefDict { set { _liGridColumnRefDict = value; } get { return _liGridColumnRefDict; } }
        public Dictionary<string, List<GridColumn>> liGridColumnRefAssistDict { set { _liGridColumnRefAssistDict = value; } get { return _liGridColumnRefAssistDict; } }

        public Dictionary<string, BarButtonItem> liButtonDict { set { _liButtonDict = value; } get { return _liButtonDict; } }
        public Dictionary<string, Control> liControlDict { set { _liControlDict = value; } get { return _liControlDict; } }

        public Dictionary<string, Control> liControlRefDict { set { _liControlRefDict = value; } get { return _liControlRefDict; } }
        public Dictionary<string, List<Control>> liControlRefAssistDict { set { _liControlRefAssistDict = value; } get { return _liControlRefAssistDict; } }

        public Dictionary<string, RepositoryItem> liRepositoryItemDict { set { _liRepositoryItemDict = value; } get { return _liRepositoryItemDict; } }
        public Dictionary<string, RepositoryItem> liRepositoryItemRefDict { set { _liRepositoryItemRefDict = value; } get { return _liRepositoryItemRefDict; } }
        public Dictionary<string, List<RepositoryItem>> liRepositoryItemRefAssistDict { set { _liRepositoryItemRefAssistDict = value; } get { return _liRepositoryItemRefAssistDict; } }

        public Dictionary<string, TreeListColumn> liTreeListColumnDict { set { _liTreeListColumnDict = value; } get { return _liTreeListColumnDict; } }

        public Dictionary<string, DockPanel> liDockPanelDict { set { _liDockPanelDict = value; } get { return _liDockPanelDict; } }

        public VoucherStatusModel voucherStatusModel 
        { 
            set { 
                _voucherStatusModel = value;

                foreach (StatusModel statusModel in _voucherStatusModel.dataStatuss)
                {
                    LiStatusReadOnlyDev liStatusDev = new LiStatusReadOnlyDev();
                    liStatusDev.buttonDict = _liButtonDict;
                    liStatusDev.controlDict = _liControlDict;
                    liStatusDev.gridColumnDict = _liGridColumnDict;
                    liStatusDev.statusModel = statusModel;

                    liStatusContext.addStatus(statusModel.code, liStatusDev);
                }
            } 
            get { return _voucherStatusModel; } 
        }

        public Dictionary<string, GridControl> liGridControlDict { set { _liGridControlDict = value; } get { return _liGridControlDict; } }
        public Dictionary<string, GridView> liGridViewDict { set { _liGridViewDict = value; } get { return _liGridViewDict; } }

        public Dictionary<string, TreeList> liTreeListDict { set { _liTreeListDict = value; } get { return _liTreeListDict; } }
        /// <summary>
        /// 状态上下文
        /// </summary>
        public LiStatusContext liStatusContext = new LiStatusContext();


        /// <summary>
        /// 单号或者唯一ID
        /// </summary>
        public string formId = System.Guid.NewGuid().ToString();

        /// <summary>
        /// 单据编码
        /// </summary>
        public string formCode = "";

        public object voucherId { get { return formDataDict[formModel.keyFieldName]; } }

        public object voucherCode { get { return formDataDict[formModel.codeFieldName]; } }

        public string mainTableName { set; get; }

        public FormModel formModel { set; get; }
        public Dictionary<string, object> formDataDict { set; get; }

        public DataTable formTreeData { get { return _formTreeData; } }

        /// <summary>
        /// 单据编码规则
        /// </summary>
        public VoucherCodeModel voucherCodeModel;

        public ComponentResourceManager resources = new ComponentResourceManager(typeof(LiForm));

        public LiForm(FormModel formModel)
        {
            InitializeComponent();

            this.formModel = formModel;
            this.formCode = formModel.name;

            Init();


            this.Controls.Clear();

            FormUtil.setRibbon(this.ribbon);

            List<ButtonGroupModel> buttonGroups = formModel.buttonGroups;
            FormUtil.loadRibbonButton(buttonGroups, this, resources);

            List<PanelModel> panelModels = formModel.panels.OrderByDescending(m => m.type).ToList();
            foreach (PanelModel panelModel in panelModels)
            {
                DevExpress.XtraLayout.LayoutControl layoutControlGrid = null;
                switch (panelModel.type)
                {
                    case "Basic":
                        layoutControlGrid = FormUtil.getBasicLayout(panelModel, this);
                        break;
                    case "Grid":
                        layoutControlGrid = FormUtil.getGridLayout(panelModel, this );
                        break;
                }
                if (layoutControlGrid != null)
                    this.Controls.Add(layoutControlGrid);
            }

            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);


        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {
            ///获取基础档案Key，然后加载
            List<string> entityKeys = new List<string>();
            List<string> dictKeys = new List<string>();
            foreach (PanelModel panelModel in formModel.panels)
            {
                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {
                        if (!entityKeys.Contains(controlModel.basicInfoKey))
                        {
                            switch (controlModel.controltype)
                            {
                                case "UserEdit":
                                case "GridLookUpEditRef":
                                case "TreeListLookUpEdit":
                                    entityKeys.Add(controlModel.basicInfoKey);
                                    break;
                                case "GridLookUpEditComboBox":
                                    dictKeys.Add(controlModel.dictInfoType);
                                    break;
                            }
                        }
                    }
                }
            }

            ///加载基础档案
            LiContexts.LiContext.addRefDataDataTable(entityKeys);
            LiContexts.LiContext.addDictDataTable(dictKeys);

            //获取单号编码规则
            voucherCodeModel = LiContexts.LiContext.getVoucherCodeModels(formCode);
        }

        public void InitControl()
        {
            //浮动窗口
            liDockPanelDict.Add("dockPanel_Edit", dockPanel_Edit);

            //浮动窗口事件
            dockPanelEditSaveEventHandler = new EventHandler(this.btnSaveDockPanelEdit_Click);
            dockPanelEditCloseEventHandler = new EventHandler(this.btnCloseDockPanelEdit_Click);
            
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void getEntitys(object key)
        {
            //LiContexts.LiContext.getHttpEntity(formCode, LiContext.SystemCode).entityKey = formCode;

            formDataDict = LiContexts.LiContext.getHttpEntity(formCode, LiContext.SystemCode).getEntityDictionarySingle(key, formModel.codeFieldName);
            //formDataDict =  LiContexts.LiContext.getHttpEntity().getEntityDictionarySingle(key, formModel.codeFieldName);

        }

        public void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            ButtonModel buttonModel = (ButtonModel)e.Item.Tag;
            setVoucherStatus(buttonModel.voucherStatus);

            LiAEventMediator liEventMediator = liEventMediatorDict[e.Item.Name];

            LiAEvent liEventItemClick = liEventMediator.getLiEvent(e.Item.Name);
            liEventItemClick.focusEntityKey = buttonModel.entityKey;
            liEventItemClick.sendEvent();

        }

        /// <summary>
        /// 设置单据状态
        /// </summary>
        /// <param name="voucherStatus"></param>
        public void setVoucherStatus(string voucherStatus)
        {
            if (!string.IsNullOrEmpty(voucherStatus))
            {
                liStatusContext.setStatus(liStatusContext.getStatus(voucherStatus));
                liStatusContext.Handle();
            }
        }

        /// <summary>
        /// 设置新增状态,要在显示窗体后，才显示
        /// </summary>
        /// <param name="voucherStatus"></param>
        public void setVoucherNewStatus()
        {
            Dictionary<string, LiAStatus> statusDict = liStatusContext.getStatusDict();
            foreach(KeyValuePair<string,LiAStatus> kvp in statusDict){
                LiIStatusReadOnlyDev status = kvp.Value as LiIStatusReadOnlyDev;
                if (status.isNewStatus())
                {
                    liStatusContext.setStatus(kvp.Value);
                    liStatusContext.Handle();

                    break;
                }
            }
        }


        /// <summary>
        /// 设置浏览状态,要在显示窗体后，才显示
        /// </summary>
        /// <param name="voucherStatus"></param>
        public void setVoucherShowStatus()
        {
            Dictionary<string, LiAStatus> statusDict = liStatusContext.getStatusDict();
            foreach (KeyValuePair<string, LiAStatus> kvp in statusDict)
            {
                LiIStatusReadOnlyDev status = kvp.Value as LiIStatusReadOnlyDev;
                if (status.isShowStatus())
                {
                    liStatusContext.setStatus(kvp.Value);
                    liStatusContext.Handle();
                    break;
                }
            }
        }


        /// <summary>
        /// 设置浏览状态,要在显示窗体后，才显示
        /// </summary>
        /// <param name="voucherStatus"></param>
        public void setVoucherStatus()
        {
            LiIStatusReadOnlyDev newStatus = null;
            LiAStatus showStatus = null;
            Dictionary<string, LiAStatus> statusDict = liStatusContext.getStatusDict();
            foreach (KeyValuePair<string, LiAStatus> kvp in statusDict)
            {
                LiIStatusReadOnlyDev status = kvp.Value as LiIStatusReadOnlyDev;
                if (status.isNewStatus() )
                {
                    newStatus = status;
                }
                if (status.isShowStatus())
                {
                    showStatus = kvp.Value;
                }
            }

            if (newStatus != null)
            {
                liStatusContext.setStatus(showStatus);
                liStatusContext.Handle();
            }
            else
            {
                liStatusContext.setStatus(statusDict[getVoucherStatusValue()]);
                liStatusContext.Handle();
            }

        }

        private void LiForm_Load(object sender, EventArgs e)
        {
            loadData();

            if (getVoucherType() == VoucherType.TreeBasicInfo)
            {
                loadTreeData();
            }
        }

        public void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in liControlDict)
            {
                if (formDataDict.ContainsKey(kvp.Key))
                {
                    formDataDict[kvp.Key] = DevControlUtil.getControlData(kvp.Value);
                }
                //else
                //{
                //    formDataDict.Add(kvp.Key, DevControlUtil.getControlData(kvp.Value));
                //}
            }

            foreach (KeyValuePair<string, GridControl> kvp in liGridControlDict)
            {
                if (formDataDict.ContainsKey(kvp.Key))
                {
                    formDataDict[kvp.Key] = DataUtil.TableToDictionary((DataTable)kvp.Value.DataSource);
                }
                else
                {
                    formDataDict.Add(kvp.Key, DataUtil.TableToDictionary((DataTable)kvp.Value.DataSource));
                }
            }
            removeAttrData();
        }

        /// <summary>
        /// 修复数据，增加表体引用辅助属性字段
        /// </summary>
        private void repairAttrData()
        {
            foreach (PanelModel panelModel in formModel.panels)
            {
                if (string.IsNullOrEmpty(panelModel.entityColumnName)) continue;

                List<Dictionary<string, object>> lists = formDataDict[panelModel.entityColumnName] as List<Dictionary<string, object>>;

                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {
                        foreach (Dictionary<string, object> dict in lists)
                        {
                            if (!dict.ContainsKey(controlModel.name))
                            {
                                switch (controlModel.controltype)
                                {
                                    case "IntEdit":
                                        dict.Add(controlModel.name, DataUtil.GetDefaultValue(Type.GetType("System.Int32")));
                                        break;
                                    case "CheckEdit":
                                        dict.Add(controlModel.name, DataUtil.GetDefaultValue(Type.GetType("System.Boolean")));
                                        break;
                                    case "DecimalEdit":
                                    case "CalcEdit":
                                        dict.Add(controlModel.name, DataUtil.GetDefaultValue(Type.GetType("System.Double")));
                                        break;
                                    case "DateTimeEdit":
                                    case "TimeEdit":
                                    case "DateEdit":
                                        dict.Add(controlModel.name, DataUtil.GetDefaultValue(Type.GetType("System.DateTime")));
                                        break;
                                    case "VoucherCodeEdit":
                                    case "TextEdit":
                                    case "MemoEdit":
                                    case "GridLookUpEditComboBox":
                                    case "UserEdit":
                                    case "GridLookUpEditRef":
                                    case "GridLookUpEditRefAssist":
                                    case "TreeListLookUpEdit":
                                        dict.Add(controlModel.name, DataUtil.GetDefaultValue(Type.GetType("System.String")));
                                        break;
                                    default:
                                        dict.Add(controlModel.name, DataUtil.GetDefaultValue(Type.GetType("System.String")));
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 修复数据，移除表体引用辅助属性字段
        /// </summary>
        private void removeAttrData()
        {
            foreach (PanelModel panelModel in formModel.panels)
            {
                if (string.IsNullOrEmpty(panelModel.entityColumnName)) continue;

                List<Dictionary<string, object>> lists = formDataDict[panelModel.entityColumnName] as List<Dictionary<string, object>>;

                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel controlModel in controlGroupModel.controls)
                    {
                        if (controlModel.controltype == "GridLookUpEditRefAssist")
                        {
                            foreach (Dictionary<string, object> dict in lists)
                            {
                                if (dict.ContainsKey(controlModel.name))
                                {
                                    dict.Remove(controlModel.name);
                                }
                            }
                        }
                    }
                }
            }

        }

        public void loadTreeData()
        {
            //LiContexts.LiContext.getHttpEntity<VoucherDataEntity>("VoucherData").entityKey = formCode;

            _formTreeData = LiContexts.LiContext.getHttpEntity(formCode, LiContext.SystemCode).getDataTable();

            liTreeListDict[formCode].DataSource = formTreeData;
            liTreeListDict[formCode].Refresh();

            setTreeListLookUpEdit("ParentID", formTreeData);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void loadData()
        {
            try
            {
                repairAttrData();

                foreach (KeyValuePair<string, object> kvp in formDataDict)
                {
                    if (_liControlDict.ContainsKey(kvp.Key))
                    {
                        Control control = _liControlDict[kvp.Key];
                        DevControlUtil.setContorlData(kvp.Value, control);
                    }

                    if (_liGridControlDict.ContainsKey(kvp.Key))
                    {
                        GridControl gridControl = _liGridControlDict[kvp.Key];

                        List<Dictionary<string, object>> lists = kvp.Value as List<Dictionary<string, object>>;
                        DataTable dt = DataUtil.DictionaryToTable(lists);
                        //if (getVoucherStatus().isNewStatus())
                        //{
                        //    dt.Rows.Clear();
                        //}
                        gridControl.DataSource = dt;
                        //gridControl.RefreshDataSource();
                        //gridControl.Refresh();
                    }

                }

                //引用赋值
                foreach (KeyValuePair<string, Control> kvp in _liControlRefDict)
                {
                    Control control = kvp.Value;
                    ControlModel controlModel = (ControlModel)control.Tag;

                    DevControlUtil.bringRefAssistValue(liControlRefAssistDict, LiContexts.LiContext.liRefDataDataTable, controlModel, control);
                }

                //表体引用赋值
                foreach (KeyValuePair<string, GridView> kvp in _liGridViewDict)
                {
                    GridView gridView = kvp.Value;
                    GridControl gridControl = _liGridControlDict[kvp.Key];
                    DataTable dt = (DataTable)gridControl.DataSource;

                    foreach (KeyValuePair<string, GridColumn> kvpColumn in _liGridColumnRefDict)
                    {
                        //如果包含才循环，不浪费资源
                        if (gridView.Columns.Contains(kvpColumn.Value))
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                DevControlUtil.bringGridRefAssistValue((ControlModel)kvpColumn.Value.Tag, dr, liGridColumnRefDict, liGridColumnRefAssistDict, LiContexts.LiContext.liRefDataDataTable, kvpColumn.Value);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageUtil.Show("加载数据错误：" + ex.Message, "系统提示");
            }
        }


        /// <summary>
        /// 初始化皮肤
        /// </summary>
        private void InitSkin()
        {
            //DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(ribbonGalleryBarItem1, true);
        }

        /// <summary>
        /// 引用控件值变更，带出属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void gridLookUpEdit_Properties_EditValueChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            ControlModel controlModel = (ControlModel)control.Tag;

            DevControlUtil.bringRefAssistValue(liControlRefAssistDict, LiContexts.LiContext.liRefDataDataTable, controlModel, control);
        }

        /// <summary>
        /// 表体值变更事件，如引用控件带出属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gridView = (GridView)sender;

            GridColumn gridColumn = e.Column;
            DataRow dr = gridView.GetDataRow(e.RowHandle);

            DevControlUtil.bringGridRefAssistValue((ControlModel)gridColumn.Tag, dr, liGridColumnRefDict, liGridColumnRefAssistDict, LiContexts.LiContext.liRefDataDataTable, gridColumn);
        }

        /// <summary>
        /// 增加新行
        /// </summary>
        /// <param name="entityKey"></param>
        public void addNewRow(string entityKey)
        {
            GridView gridView = liGridViewDict[entityKey];
            gridView.AddNewRow();
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="rowHandle"></param>
        public void deleteRow(string entityKey, int rowHandle)
        {
            GridView gridView = liGridViewDict[entityKey];
            gridView.DeleteRow(rowHandle);
        }

        /// <summary>
        /// 插入新行
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="rowHandle"></param>
        public void insertNewRow(string entityKey, int rowHandle)
        {
            GridControl gridControl = liGridControlDict[entityKey];
            GridView gridView = liGridViewDict[entityKey];

            DataTable dt = gridControl.DataSource as DataTable; 
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr , rowHandle);
        }

        /// <summary>
        /// 获取表体的数据源
        /// </summary>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public DataTable getEntityData(string entityKey)
        {
            GridControl gridControl = liGridControlDict[entityKey];
            return gridControl.DataSource as DataTable;
        }

        /// <summary>
        /// 获取表体焦点行
        /// </summary>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public int getFocusRowHandle(string entityKey)
        {
            GridView gridView = liGridViewDict[entityKey];
            return gridView.FocusedRowHandle;
        }

        /// <summary>
        /// 保存单据
        /// </summary>
        public void saveVoucher()
        {
            this.getData();

            if (Convert.ToInt32(this.voucherId) > 0)
            {
                //LiContexts.LiContext.getHttpEntity().entityKey = this.formCode;
                LiContexts.LiContext.getHttpEntity(this.formCode, LiContext.SystemCode).updateEntity(this.formDataDict);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(this.formCode, LiContext.SystemCode).tipStr, "温馨提示");

            }
            else
            {
                if (voucherCodeModel != null)
                    formDataDict[formModel.codeFieldName] = getVoucherNo();

                //LiContexts.LiContext.getHttpEntity().entityKey = this.formCode;
                LiContexts.LiContext.getHttpEntity(this.formCode, LiContext.SystemCode).newEntity(this.formDataDict);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(this.formCode, LiContext.SystemCode).tipStr, "温馨提示");

            }

            this.getEntitys(this.voucherCode);
            this.loadData();
        }

        /// <summary>
        /// 获取空数据包
        /// </summary>
        public void getNewDate()
        {
            formDataDict = LiContexts.LiContext.getHttpEntity(formCode, LiContext.SystemCode).getEntityNewData();

        }

        /// <summary>
        /// 用于设置单据用户和日期信息
        /// </summary>
        /// <param name="userField"></param>
        /// <param name="dataField"></param>
        /// <param name="formDataDict"></param>
        public void setVoucherUserInfo(string userField, string dataField,Dictionary<string, object> formDataDict)
        {
            formDataDict[userField] = LiContexts.LiContext.userInfo.userCode;
            formDataDict[dataField] = DateTime.Now;
        }

        /// <summary>
        /// 获取单据号
        /// </summary>
        /// <returns></returns>
        public string getVoucherNo()
        {
            string VoucherNo = string.Empty;

            if (voucherCodeModel != null)
            {
                DateTime fieldDateValue = Convert.ToDateTime(formDataDict[voucherCodeModel.fieldDateName]);

                Dictionary<string, object> paramDict = new Dictionary<string, object>();
                paramDict.Add("entityKey", formCode);
                paramDict.Add("fieldTextValue", !string.IsNullOrWhiteSpace(voucherCodeModel.fieldTextName) ? formDataDict[voucherCodeModel.fieldTextName] : "");
                paramDict.Add("fieldDateValue", fieldDateValue.ToString(voucherCodeModel.dateTimeFormat));
                paramDict.Add("dateValue", fieldDateValue.ToString("yyyy-MM-dd HH:mm:ss"));
                VoucherNo = Convert.ToString(LiContexts.LiContext.getHttpEntity("sp_getVoucherCode").execProcedureSingleValue_Object( "VoucherNo", paramDict));
            }

            return VoucherNo;
        }

        /// <summary>
        /// 获取单据当前状态
        /// </summary>
        /// <returns></returns>
        public LiStatusReadOnlyDev getVoucherStatus()
        {
            return liStatusContext.getCurrentStatus() as LiStatusReadOnlyDev;
        }

        /// <summary>
        /// 获取单据当前状态
        /// </summary>
        /// <returns></returns>
        public string getVoucherStatusValue()
        {
            return Convert.ToString(formDataDict[formModel.statusFieldName]);
        }


        /// <summary>
        /// 显示浮动窗体
        /// </summary>
        /// <param name="bShow"></param>
        public void setShowDockPanel(string dockPanelKey, bool bShow)
        {
            if (!liDockPanelDict.ContainsKey(dockPanelKey)) return;
            if (bShow)
            {
                liDockPanelDict[dockPanelKey].Show();
            }
            else
            {
                liDockPanelDict[dockPanelKey].Close();
            }
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <returns></returns>
        public string getVoucherType()
        {
            return formModel.formType;
        }

        /// <summary>
        /// 浮动窗口保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveDockPanelEdit_Click(object sender, EventArgs e)
        {
            saveVoucher();
            //saveDict();

            loadTreeData();
            loadData();

            setTreeListLookUpEdit("ParentID", formTreeData);
        }

        /// <summary>
        /// 浮动窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseDockPanelEdit_Click(object sender, EventArgs e)
        {
            setShowDockPanel("dockPanel_Edit", false);
        }

        public void setTreeListLookUpEdit(string key, DataTable dt)
        {
            TreeListLookUpEdit treeListLookUpEdit = liControlDict[key] as TreeListLookUpEdit;
            treeListLookUpEdit.Properties.DataSource = dt;
            treeListLookUpEdit.Refresh();
        }

        /// <summary>
        /// 获取树形控件焦点行
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DataRow getDataRowInTreeList(string key)
        {
            TreeList treeList = liTreeListDict[key];
            return treeList.GetFocusedDataRow();
        }

    }

}