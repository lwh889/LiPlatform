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
using DevExpress.XtraEditors;

using LiForm.Dev.Util;

using LiForm.Event;
using LiForm.Event.Mediator;

using LiModel.Form;
using LiForm.LiStatus;
using LiContexts.Model;
using LiHttp.Enum;
using LiContexts;

using LiModel.LiEnum;
using LiControl.Util;

namespace LiForm.Dev
{
    public partial class LiListForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        /// <summary>
        /// 每页显示行数  
        /// </summary>
        private int pageSize = 1000;     //每页显示行数 
        /// <summary>
        /// 总记录数  
        /// </summary>
        private int pageSum = 1;         //总记录数  
        /// <summary>
        /// 页数＝总记录数/每页显示行数  
        /// </summary>
        private int pageCount = 1;    //页数＝总记录数/每页显示行数  
        /// <summary>
        /// 当前页号
        /// </summary>
        private int pageCurrent = 1;   //当前页号  
        /// <summary>
        /// 查询条件
        /// </summary>
        public string queryWhereStr = string.Empty;

        /// <summary>
        /// 表格列
        /// </summary>
        private Dictionary<string, GridColumn> _liGridColumnDict = new Dictionary<string, GridColumn>();


        /// <summary>
        /// 字段列表
        /// </summary>
        //private Dictionary<string, string> _liFieldName = new Dictionary<string, string>();

        /// <summary>
        /// 按钮
        /// </summary>
        private Dictionary<string, BarButtonItem> _liButtonDict = new Dictionary<string, BarButtonItem>();

        /// <summary>
        /// 按钮事件中介
        /// </summary>
        private Dictionary<string, LiAEventMediator> _liEventMediatorDict = new Dictionary<string, LiAEventMediator>();

        /// <summary>
        /// 事件
        /// </summary>
        public Dictionary<string, LiAEventMediator> liEventMediatorDict { set { _liEventMediatorDict = value; } get { return _liEventMediatorDict; } }

        /// <summary>
        /// 列表列
        /// </summary>
        public Dictionary<string, GridColumn> liGridColumnDict { set { _liGridColumnDict = value; } get { return _liGridColumnDict; } }
        //public Dictionary<string, string> liFieldName { set { _liFieldName = value; } get { return _liFieldName; } }

        /// <summary>
        /// 列表按钮
        /// </summary>
        public Dictionary<string, BarButtonItem> liButtonDict { set { _liButtonDict = value; } get { return _liButtonDict; } }

        /// <summary>
        /// 状态
        /// </summary>
        public LiStatusContext liStatusContext = new LiStatusContext();

        private List<QuerySchemeModel> _querySchemeModels;
        /// <summary>
        /// 当前所有查询方案
        /// </summary>
        public List<QuerySchemeModel> querySchemeModels { set{_querySchemeModels = value;} get { return _querySchemeModels; } }

        /// <summary>
        /// 查询方案按钮
        /// </summary>
        private Dictionary<string, SimpleButton> querySchemeBtns = new Dictionary<string, SimpleButton>();

        /// <summary>
        /// 快速查询控件
        /// </summary>
        private Dictionary<string, Control> liQuickQueryControlDict = new Dictionary<string, Control>();

        public ComponentResourceManager resources = new ComponentResourceManager(typeof(LiListForm));

        private QuerySchemeModel _currentQuerySchemeModel;
        //当前的查询方案
        public QuerySchemeModel currentQuerySchemeModel
        {
            set {
                _currentQuerySchemeModel = value;
                if (_currentQuerySchemeModel != null)
                {
                    FormUtil.loadQuickQuery(_currentQuerySchemeModel.fields, liQuickQueryControlDict, layoutControlGroup2, layoutControl2);
                    groupControl1.Refresh();
                }

                foreach (KeyValuePair<string, SimpleButton> kvp in querySchemeBtns)
                {
                    if (!kvp.Key.Equals(_currentQuerySchemeModel.querySchemeName))
                    {
                        kvp.Value.Appearance.BorderColor = Color.Transparent;
                    }
                    else
                    {
                        kvp.Value.Appearance.BorderColor = Color.Red;
                    }

                }

            }
            get { return _currentQuerySchemeModel; } }
        #region 引用数据源
        private FieldModel fieldModel = new FieldModel();
        #endregion

        /// <summary>
        /// 表单界面信息
        /// </summary>
        public FormModel formModel { set; get; }

        /// <summary>
        /// 主表名
        /// </summary>
        public string mainTableName { set; get; }

        /// <summary>
        /// 主键
        /// </summary>
        public string primaryFieldName { set; get; }

        /// <summary>
        /// 实体名
        /// </summary>
        public string entityKey { set; get; }

        private string _childTableName;
        public string childTableName {
            get {
                foreach(EntityModel entityModel in currentQuerySchemeModel.entitys)
                {
                    if(entityModel.sEntityType == PanelType.GRID && entityModel.iShow)
                    {
                        return entityModel.sTableName;
                    }
                }
                return _childTableName;
            }
        }

        public LiListForm(FormModel formModel)
        {
            InitializeComponent();

            this.formModel = formModel;

            FormUtil.setRibbon(this.ribbon);

            FormUtil.loadListRibbonButton(formModel.listButtons, this, resources);

            Init();
        }

        public void Init()
        {
            InitData();
            InitView();
        }

        public void InitData()
        {
            entityKey = formModel.name;

            InitGridColumn();

            InitMainTableName();

            InitQueryScheme();
        }

        public void InitQueryScheme()
        {
            QuerySchemeModel defaultQuerySchemeModel = new QuerySchemeModel();
            defaultQuerySchemeModel.userCode = LiContexts.LiContext.userInfo.userCode;
            defaultQuerySchemeModel.querySchemeName = "默认方案";
            defaultQuerySchemeModel.entitys = EntityModel.getDataSource(entityKey); ;
            defaultQuerySchemeModel.fields = FieldModel.getDataSource(entityKey);
            defaultQuerySchemeModel.querys = new List<QueryModel>();

            querySchemeModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).getEntityList<QuerySchemeModel>(LiContexts.LiContext.userInfo.userCode,"userCode");
            querySchemeModels.Insert(0, defaultQuerySchemeModel);

            currentQuerySchemeModel = defaultQuerySchemeModel;
        }

        public void InitMainTableName()
        {
            foreach (PanelModel panelModel in formModel.panels)
            {
                if (panelModel.type == "Basic")
                {
                    primaryFieldName = string.Format("Li{0}_{1}", panelModel.tableName, formModel.keyFieldName);
                    mainTableName = panelModel.tableName;
                    return;
                }
            }
        }

        public void InitGridColumn()
        {
            FieldModel.InitDataSource(formModel);
            EntityModel.InitDataSource(formModel);

            GridColumn gridColumnSel = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnSel.Caption = "选择";
            gridColumnSel.Name = "LiSel_GridColumn";
            gridColumnSel.FieldName = "sel";
            gridColumnSel.Visible = true;
            gridColumnSel.VisibleIndex = 0;
            gridColumnSel.Width = 80;
            gridColumnSel.OptionsColumn.AllowEdit = true;

            liGridColumnDict.Add("sel", gridColumnSel);

            foreach (PanelModel panelModel in formModel.panels)
            {
                foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
                {
                    foreach (ControlModel control in controlGroupModel.controls)
                    {
                        FieldModel fieldModelTemp = new FieldModel();

                        GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                        gridColumn.Caption = control.text;
                        gridColumn.Name = string.Format("Li{0}{1}_GridColumn", panelModel.tableName, control.name);
                        gridColumn.FieldName = string.Format("Li{0}_{1}", panelModel.tableName, control.name);
                        gridColumn.Visible = control.bVisibleInList;
                        gridColumn.VisibleIndex = control.bVisibleInList ? control.colIndex : -1;
                        gridColumn.Width = control.width;
                        gridColumn.OptionsColumn.AllowEdit = false;

                        gridColumn.Tag = control;

                        liGridColumnDict.Add(control.name, gridColumn);

                    }
                }
            }
        }

        public void InitView()
        {
            resetGridControl();
            FormUtil.loadQueryScheme(querySchemeModels, querySchemeBtns, new System.EventHandler(this.btnQueryScheme_Click), layoutControlGroup1);
            FormUtil.loadQuickQuery(querySchemeModels[0].fields, liQuickQueryControlDict, layoutControlGroup2, layoutControl2);
        }

        public void resetGridControl()
        {
            gridView1.Columns.Clear();

            List<GridColumn> showGridColumnList = new List<GridColumn>();
            List<FieldModel> fieldModels = new List<FieldModel>();
            foreach (EntityModel entityModel in currentQuerySchemeModel.entitys)
            {
                if (entityModel.iShow)
                {
                    fieldModels.AddRange(currentQuerySchemeModel.fields.Where(m => m.sEntityCode == entityModel.sEntityCode).ToArray());
                }
            }

            int iRow = 0;
            GridColumn gc = liGridColumnDict["sel"];
            gc.VisibleIndex = iRow++;
            showGridColumnList.Add(gc);
            foreach (FieldModel fieldModel in fieldModels)
            {
                gc = liGridColumnDict[fieldModel.fieldName];
                if (!fieldModel.bColumnDisplay) continue;

                gc.VisibleIndex = iRow++;
                showGridColumnList.Add(gc);
            }
            gridView1.Columns.AddRange(showGridColumnList.ToArray());
            gridControl1.Refresh();

        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        //public List<FormModel> getEntitys(object key)
        //{
        //    string resultContent;
        //    QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "");
        //    paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single(formModel.codeFieldName, key));

        //    if (LiContexts.LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
        //    {
        //        return JsonUtil.GetEntityToList<FormModel>(resultContent);
        //    }

        //    return null;
        //}

        public void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListButtonModel listButtonModel = (ListButtonModel)e.Item.Tag;
            LiAEventMediator liEventMediator = liEventMediatorDict[e.Item.Name];

            LiAEvent liEventItemClick = liEventMediator.getLiEvent(e.Item.Name);

            liEventItemClick.sendEvent();


        }

        private void LiForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        public void loadData()
        {

        }

        private void InitSkin()
        {
            //UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(ribbonGalleryBarItem1, true);
            //this.ribbonControl.Toolbar.ItemLinks.Add(ribbonGalleryBarItem1);
        }

        public void FillGridListCtrlQuery(int curPage = 1)   //更新控件
        {
            //  GridControl1.DataSource = WebService.Pager(。。。。。//显示分页结果
            pagerControl1.RefreshPager(pageSum, curPage);//更新分页控件显示。
        }

        public int setFirstPage()
        {
            this.pageCurrent = 1;
            return this.pageCurrent;
        }

        private void pagerControl1_myPagerEvents(int curPage, int pageSize)
        {
            this.pageCurrent = curPage;
            this.pageSize = pageSize;

            Query();
            FillGridListCtrlQuery(curPage);
        }

        public void setQueryWhere(string queryWhereStr)
        {
            this.queryWhereStr = queryWhereStr;
        }

        public void getQuickQueryWhere()
        {
            queryWhereStr = DevFormUtil.getWhereStr(liQuickQueryControlDict, true);
        }

        public void Query()
        {

            resetGridControl();

            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            paramDict.Add("childTableName", childTableName);
            paramDict.Add("systemCode", LiContext.SystemCode);
            paramDict.Add("entityKey", entityKey);
            paramDict.Add("whereSql", "");

            this.pageSum = LiContexts.LiContext.getHttpEntity("sp_QueryList_Count").execProcedureSingleValue_Int32( "iCount", paramDict);

            paramDict.Clear();
            paramDict.Add("childTableName", childTableName);
            paramDict.Add("entityKey", entityKey);
            paramDict.Add("systemCode", LiContext.SystemCode);
            paramDict.Add("whereSql", queryWhereStr);
            paramDict.Add("orderBySql", "");
            paramDict.Add("rangeSql", string.Format(" and iPageRow > {0} and iPageRow <= {1}", (pageCurrent - 1) * pageSize, pageSize * pageCurrent));

            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity("sp_QueryList").execProcedure_DataTable(paramDict);

            gridView1.BestFitColumns();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                Dictionary<string, object> dict = LiContexts.LiContext.getHttpEntity(entityKey, LiContext.SystemCode).getEntityDictionarySingle(dr[primaryFieldName], formModel.keyFieldName);

                LiForm liForm = FormUtil.getVoucher(entityKey,dict) as LiForm;
                liForm.Text = formModel.text;
                if (LiContext.AddPageMdi(PageFormModel.getInstance(Convert.ToInt32(dict[formModel.keyFieldName]),  liForm, formModel.name,""), this.ParentForm))
                {
                    liForm.setVoucherStatus();
                }
            }
        }

        /// <summary>
        /// 单击快速查询上的按钮方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryScheme_Click(object sender, EventArgs e)
        {
            SimpleButton btn = (SimpleButton)sender;

            currentQuerySchemeModel = querySchemeModels.Where(m => m.querySchemeName == btn.Text).FirstOrDefault();
            
        }

        /// <summary>
        /// 获取列表的数据源
        /// </summary>
        /// <returns></returns>
        public DataTable getListDataSource()
        {
            return gridControl1.DataSource as DataTable;
        }

        /// <summary>
        /// 获取焦点行
        /// </summary>
        /// <returns></returns>
        public DataRow getListFocusedDataRow()
        {
            return gridView1.GetFocusedDataRow();
        }

        /// <summary>
        /// 获取单据编码的字段名称
        /// </summary>
        /// <returns></returns>
        public string getVoucherKeyFieldName()
        {
            return formModel.keyFieldName;
        }

        /// <summary>
        /// 获取表单的Key编码
        /// </summary>
        /// <returns></returns>
        public string getEntityKey()
        {
            return entityKey;
        }

        /// <summary>
        /// 获取窗口容器的窗口
        /// </summary>
        /// <returns></returns>
        public Form getParentForm()
        {
            return this.ParentForm;
        }

        /// <summary>
        /// 获取单据界面信息
        /// </summary>
        /// <returns></returns>
        public FormModel getFormModel()
        {
            return formModel;
        }

        /// <summary>
        /// 获取主键字段名
        /// </summary>
        /// <returns></returns>
        public string getPrimaryFieldName()
        {
            return primaryFieldName;
        }

        public List<DataRow> getSelectedDataRows()
        {
            List<DataRow> SelectedDataRow = new List<DataRow>();
            DataTable dt = gridControl1.DataSource as DataTable;
            if (dt != null)
            {
                DataRow[] drs = dt.Select("sel = True");
                SelectedDataRow.AddRange(drs);
            }

            return SelectedDataRow;
        }

    }

}