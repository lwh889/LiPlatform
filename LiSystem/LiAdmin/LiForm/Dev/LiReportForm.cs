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
using LiForm.Event.Mediator;
using DevExpress.XtraGrid.Columns;
using LiForm.Dev.Util;
using DevExpress.XtraEditors;
using LiModel.Form;
using LiModel.LiReport;
using LiControl.Util;
using LiForm.Event;
using LiHttp.RequestParam;
using LiHttp.Enum;
using LiContexts;

namespace LiForm.Dev
{
    public partial class LiReportForm : DevExpress.XtraBars.Ribbon.RibbonForm
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

        /// <summary>
        /// 列表按钮
        /// </summary>
        public Dictionary<string, BarButtonItem> liButtonDict { set { _liButtonDict = value; } get { return _liButtonDict; } }

        private List<QuerySchemeModel> _querySchemeModels;
        /// <summary>
        /// 当前所有查询方案
        /// </summary>
        public List<QuerySchemeModel> querySchemeModels { set { _querySchemeModels = value; } get { return _querySchemeModels; } }

        /// <summary>
        /// 查询方案按钮
        /// </summary>
        private Dictionary<string, SimpleButton> querySchemeBtns = new Dictionary<string, SimpleButton>();

        /// <summary>
        /// 快速查询控件
        /// </summary>
        private Dictionary<string, Control> liQuickQueryControlDict = new Dictionary<string, Control>();

        public ComponentResourceManager resources = new ComponentResourceManager(typeof(LiListForm3));

        private QuerySchemeModel _currentQuerySchemeModel;
        //当前的查询方案
        public QuerySchemeModel currentQuerySchemeModel
        {
            set
            {
                _currentQuerySchemeModel = value;
                if (_currentQuerySchemeModel != null)
                {
                    FormUtil.loadQuickQuery(_currentQuerySchemeModel.fields, liQuickQueryControlDict, layoutControlGroup2, layoutControl2, this);
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
            get { return _currentQuerySchemeModel; }
        }


        /// <summary>
        /// 实体名
        /// </summary>
        public string entityKey { set; get; }
        /// <summary>
        /// 表单界面信息
        /// </summary>
        public LiReportModel reportModel { set; get; }

        public LiReportForm(LiReportModel reportModel)
        {
            InitializeComponent();

            this.reportModel = reportModel;
            this.entityKey = reportModel.reportKey;

            FormUtil.setRibbon(this.ribbon);

            FormUtil.loadReportRibbonButton(reportModel.buttons.OrderBy(m => m.iIndex).ToList(), this, resources);

            Init();
        }

        public void Init()
        {
            InitData();
            InitView();
        }


        public void InitData()
        {
            InitGridColumn();

            InitQueryScheme();
        }

        public void InitGridColumn()
        {
            ReportFormUtil.getGridColumn(reportModel.datas, liGridColumnDict);

        }
        public void InitQueryScheme()
        {
            QuerySchemeModel defaultQuerySchemeModel = new QuerySchemeModel();
            defaultQuerySchemeModel.userCode = LiContexts.LiContext.userInfo.userCode;
            defaultQuerySchemeModel.querySchemeName = "默认方案";
            //defaultQuerySchemeModel.entitys = EntityModel.getDataSource(entityKey);
            //defaultQuerySchemeModel.fields = FieldModel.getDataSource(entityKey);
            defaultQuerySchemeModel.querys = new List<QueryModel>();

            querySchemeModels = loadQuerySchemeModels();
            querySchemeModels.Insert(0, defaultQuerySchemeModel);

            currentQuerySchemeModel = defaultQuerySchemeModel;
        }

        public void InitView()
        {
            resetGridControl();
            FormUtil.loadQueryScheme(querySchemeModels, querySchemeBtns, new System.EventHandler(this.btnQueryScheme_Click), layoutControlGroup1);
            //FormUtil.loadQuickQuery(querySchemeModels[0].fields, liQuickQueryControlDict, layoutControlGroup2, layoutControl2, this);
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
                gc = liGridColumnDict[fieldModel.columnFieldName];
                if (!fieldModel.bColumnDisplay) continue;

                gc.VisibleIndex = iRow++;
                showGridColumnList.Add(gc);
            }
            gridView1.Columns.AddRange(showGridColumnList.ToArray());
            gridControl1.Refresh();

        }


        public List<QuerySchemeModel> loadQuerySchemeModels()
        {
            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "userCode", LiContext.userInfo.userCode, "entityKey", entityKey);
            return LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).getEntityList<QuerySchemeModel>(paramModel);

        }

        public void barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiReportButtonModel liReportButton = (LiReportButtonModel)e.Item.Tag;
            LiAEventMediator liEventMediator = liEventMediatorDict[e.Item.Name];

            LiAEvent liEventItemClick = liEventMediator.getLiEvent(e.Item.Name);

            liEventItemClick.sendEvent();


        }

        private void LiReportForm_Load(object sender, EventArgs e)
        {

        }
        public void loadData()
        {

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

        public void RefreshData()
        {
            Query();
        }
        public void Query()
        {

            resetGridControl();

            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            //paramDict.Add("childTableName", childTableName);
            //paramDict.Add("systemCode", LiContext.SystemCode);
            //paramDict.Add("entityKey", entityKey);
            //paramDict.Add("whereSql", "");

            //this.pageSum = LiContexts.LiContext.getHttpEntity("sp_QueryList_Count").execProcedureSingleValue_Int32("iCount", paramDict);

            //paramDict.Clear();
            //paramDict.Add("childTableName", childTableName);
            //paramDict.Add("entityKey", entityKey);
            //paramDict.Add("systemCode", LiContext.SystemCode);
            //paramDict.Add("whereSql", queryWhereStr);
            //paramDict.Add("orderBySql", "");
            //paramDict.Add("rangeSql", string.Format(" and iPageRow > {0} and iPageRow <= {1}", (pageCurrent - 1) * pageSize, pageSize * pageCurrent));

            //gridControl1.DataSource = LiContexts.LiContext.getHttpEntity("sp_QueryList").execProcedure_DataTable(paramDict);

            gridView1.BestFitColumns();
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
        public LiReportModel getReportModel()
        {
            return reportModel;
        }

    }
}