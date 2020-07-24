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
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;

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
        /// 排序
        /// </summary>
        public string orderByStr = string.Empty;

        /// <summary>
        /// 表格列
        /// </summary>
        private Dictionary<string, GridColumn> _liGridColumnDict = new Dictionary<string, GridColumn>();


        /// <summary>
        /// 表格列(组)
        /// </summary>
        public Dictionary<string, BandedGridColumn> _liBandedGridColumnDict = new Dictionary<string, BandedGridColumn>();
        /// <summary>
        /// 组列
        /// </summary>
        public Dictionary<string, GridBand> _liGridBandDict = new Dictionary<string, GridBand>();
        /// <summary>
        /// 组列
        /// </summary>
        public Dictionary<string, GridBand> _liGridBandParentDict = new Dictionary<string, GridBand>();
        /// <summary>
        /// 列与组映射
        /// </summary>
        public Dictionary<BandedGridColumn, GridBand> _liBandedGridColumnAndGridBandDict = new Dictionary<BandedGridColumn, GridBand>();

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
        /// 表格视图配置
        /// </summary>
        private Dictionary<string, Dictionary<string, object>> gridViewInfo = new Dictionary<string, Dictionary<string, object>>();


        /// <summary>
        /// 列表列
        /// </summary>
        public Dictionary<string, GridColumn> liGridColumnDict { set { _liGridColumnDict = value; } get { return _liGridColumnDict; } }
        private GridView gridView;

        /// <summary>
        /// 列表列（组）
        /// </summary>
        public Dictionary<string, BandedGridColumn> liBandedGridColumnDict { set { _liBandedGridColumnDict = value; } get { return _liBandedGridColumnDict; } }
        /// <summary>
        /// 组列
        /// </summary>
        public Dictionary<string, GridBand> liGridBandDict { set { _liGridBandDict = value; } get { return _liGridBandDict; } }
        /// <summary>
        /// 父组列
        /// </summary>
        public Dictionary<string, GridBand> liGridBandParentDict { set { _liGridBandParentDict = value; } get { return _liGridBandParentDict; } }
        /// <summary>
        /// 列与组映射 
        /// </summary>
        public Dictionary<BandedGridColumn, GridBand> liBandedGridColumnAndGridBandDict { set { _liBandedGridColumnAndGridBandDict = value; } get { return _liBandedGridColumnAndGridBandDict; } }
        private BandedGridView bandedGridView;

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

                    List<FieldModel> quickFields = _currentQuerySchemeModel.fields.Where(m => m.bQuery == true).ToList();
                    if (quickFields.Count <= 0)
                    {
                        layoutControl2.Visible = false;
                    }
                    else
                    {
                        layoutControl2.Visible = true;
                    }

                }

                foreach (KeyValuePair<string, SimpleButton> kvp in querySchemeBtns)
                {
                    if (!kvp.Key.Equals(_currentQuerySchemeModel.querySchemeName))
                    {
                        kvp.Value.Appearance.BackColor = Color.Transparent;
                    }
                    else
                    {
                        kvp.Value.Appearance.BackColor = Color.Red;
                    }

                }

            }
            get { return _currentQuerySchemeModel; }
        }

        /// <summary>
        /// 报表总数参数
        /// </summary>
        private List<Dictionary<string, object>> paramMapsCount = new List<Dictionary<string, object>>();

        /// <summary>
        /// 报表参数
        /// </summary>
        private List<Dictionary<string, object>> paramMaps = new List<Dictionary<string, object>>();

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

            setViewParam();
            if (reportModel.bColumnGroup)
            {
                bandedGridView = DevControlUtil.getBandedGridView(gridControl1, gridViewInfo);
            }
            else
            {
                gridView = DevControlUtil.getGridView(gridControl1, gridViewInfo);
            }

            FormUtil.setRibbon(this.ribbon);

            FormUtil.loadReportRibbonButton(reportModel.buttons.OrderBy(m => m.iIndex).ToList(), this, resources);

            Init();
        }

        public void Init()
        {
            InitData();
            InitView();
        }

        /// <summary>
        /// 设置报表参数
        /// </summary>
        private void setReportSqlParam()
        {

            //报表总数的参数
            Dictionary<string, object> paramMapCountDict = new Dictionary<string, object>();
            paramMapCountDict.Add("paramName", "whereSql");
            paramMapCountDict.Add("paramType", "nvarchar(max)");
            paramMapsCount.Add(paramMapCountDict);

            Dictionary<string, object> paramMapDict = new Dictionary<string, object>();
            paramMapDict.Add("paramName", "whereSql");
            paramMapDict.Add("paramType", "nvarchar(max)");
            paramMaps.Add(paramMapDict);
            paramMapDict = new Dictionary<string, object>();
            paramMapDict.Add("paramName", "orderBySql");
            paramMapDict.Add("paramType", "nvarchar(max)");
            paramMaps.Add(paramMapDict); paramMapDict = new Dictionary<string, object>();
            paramMapDict.Add("paramName", "rangeSql");
            paramMapDict.Add("paramType", "nvarchar(max)");
            paramMaps.Add(paramMapDict);

        }

        private void setViewParam()
        {
            Dictionary<string, object> optionsView = new Dictionary<string, object>();
            optionsView.Add("ColumnAutoWidth", false);
            optionsView.Add("ShowAutoFilterRow", true);
            optionsView.Add("ShowGroupPanel", false);

            gridViewInfo.Add("OptionsView", optionsView);
        }
        public void InitData()
        {
            setReportSqlParam();

            ReportFormUtil.loadBasicInfo(reportModel);

            InitGridColumn();

            InitQueryScheme();

            getQueryOrderBy();
        }

        public void InitGridColumn()
        {
            FieldModel.InitDataSource(reportModel);

            if (reportModel.bColumnGroup)
            {
                ReportFormUtil.getBandedGridColumn(reportModel.datas, liBandedGridColumnDict, liGridBandDict, liGridBandParentDict, liBandedGridColumnAndGridBandDict, bandedGridView, false);
            }
            else
            {
                ReportFormUtil.getGridColumn(reportModel.datas, liGridColumnDict, false);
            }
            

        }
        public void InitQueryScheme()
        {
            querySchemeModels = FormUtil.loadQuerySchemeModels(entityKey);

        }

        public void InitView()
        {

            reloadQuerySchemes(querySchemeModels);
            currentQuerySchemeModel = querySchemeModels.Count > 0 ? querySchemeModels[0] : null;
            resetGridControl();
        }

        public void reloadQuerySchemes(List<QuerySchemeModel> querySchemeModels)
        {
            this.querySchemeModels = querySchemeModels;
            FormUtil.loadQueryScheme(querySchemeModels, querySchemeBtns, new System.EventHandler(this.btnQueryScheme_Click), layoutControlGroup1, layoutControl1);

            if(querySchemeModels.Count == 1)
            {
                layoutControl1.Visible = false;
            }
            else
            {
                layoutControl1.Visible = true;
            }
        }

        public void resetGridControl()
        {
            if (reportModel.bColumnGroup)
            {
                List<BandedGridColumn> showBandedGridColumnList = new List<BandedGridColumn>();
                //List<GridBand> showGridBandParentList = new List<GridBand>();
                List<GridBand> showGridBandList = new List<GridBand>();

                int iRow = 0;
                if (currentQuerySchemeModel == null) return;
                List<FieldModel> fields = currentQuerySchemeModel.fields.OrderBy(m => m.iColumnIndex).ToList();
                foreach (FieldModel fieldModel in fields)
                {
                    BandedGridColumn gc = liBandedGridColumnDict[fieldModel.fieldName];
                    if (!fieldModel.bColumnDisplay) continue;

                    gc.VisibleIndex = iRow++;
                    showBandedGridColumnList.Add(gc);
                }

                Dictionary<GridBand, List<BandedGridColumn>> gridBandDict = new Dictionary<GridBand, List<BandedGridColumn>>();

                foreach(BandedGridColumn bandedGridColumn in showBandedGridColumnList)
                {
                    GridBand gridBand = liBandedGridColumnAndGridBandDict[bandedGridColumn];
                    if (!gridBandDict.ContainsKey(gridBand))
                    {
                        gridBandDict.Add(gridBand, new List<BandedGridColumn>());
                    }
                    gridBandDict[gridBand].Add(bandedGridColumn);

                    if (!showGridBandList.Contains(gridBand))
                        showGridBandList.Add(gridBand);
                    
                }

                bandedGridView.Bands.Clear();

                foreach (GridBand gridBand in showGridBandList)
                {
                    gridBand.Columns.Clear();
                    List<BandedGridColumn> bandedGridColumns = gridBandDict[gridBand];
                    foreach (BandedGridColumn bandedGrid in bandedGridColumns)
                    {
                        gridBand.Columns.Add(bandedGrid);
                        bandedGrid.OwnerBand = gridBand;
                    }
                }

                bandedGridView.Bands.AddRange(showGridBandList.ToArray());
                bandedGridView.Columns.AddRange(showBandedGridColumnList.ToArray());
            }
            else
            {
                gridView.BeginUpdate();
                gridView.Columns.Clear();
                int iRow = 0;

                if (currentQuerySchemeModel == null) return;
                List<FieldModel> fields = currentQuerySchemeModel.fields.OrderBy(m => m.iColumnIndex).ToList();
                foreach (FieldModel fieldModel in fields)
                {
                    GridColumn gc = liGridColumnDict[fieldModel.fieldName];
                    if (!fieldModel.bColumnDisplay) continue;

                    gc.VisibleIndex = iRow++;
                    gridView.Columns.Add(gc);
                }
                gridView.EndUpdate();
            }

            gridControl1.Refresh();

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

        public int getCurrentPage()
        {
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

        public void getQueryOrderBy()
        {
            if (string.IsNullOrEmpty(orderByStr))
            {
                List<LiReportFieldModel> liReportFields = reportModel.datas.Where(m=>m.bOrderBy == true).OrderBy(m=>m.iOrderByIndex).ToList();
                if(liReportFields != null && liReportFields.Count > 0)
                {
                    List<string> orderBys = new List<string>();
                    foreach (LiReportFieldModel liReportField in liReportFields)
                    {
                        orderBys.Add(string.Format(" {0} {1}", liReportField.columnName, liReportField.orderBy));
                    }

                    orderByStr = string.Format(" {0}",string.Join(",", orderBys));
                }
            }
        }

        public void RefreshData()
        {
            Query();
        }
        public void Query()
        {

            resetGridControl();

            Dictionary<string, object> paramValuesCount = new Dictionary<string, object>();
            paramValuesCount.Add("whereSql", queryWhereStr);

            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            paramDict.Add("dataBaseName", reportModel.dataBaseName);
            paramDict.Add("procedureName", reportModel.reportCountSql);
            paramDict.Add("paramMaps", paramMapsCount);
            paramDict.Add("paramValues", paramValuesCount);

            this.pageSum = LiContexts.LiContext.getHttpEntity(LiEntityKey.sp_Currency).execProcedureByMapSingleValue_Int32("iCount", paramDict);



            Dictionary<string, object> paramValues = new Dictionary<string, object>();
            paramValues.Add("whereSql", queryWhereStr);
            paramValues.Add("orderBySql", orderByStr);
            paramValues.Add("rangeSql", string.Format(" and iPageRow > {0} and iPageRow <= {1}", (pageCurrent - 1) * pageSize, pageSize * pageCurrent));

            paramDict.Clear();
            paramDict.Add("dataBaseName", reportModel.dataBaseName);
            paramDict.Add("procedureName", reportModel.reportSql);
            paramDict.Add("paramMaps", paramMaps);
            paramDict.Add("paramValues", paramValues);

            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.sp_Currency).execProcedureByMap_DataTable(paramDict);

            if (reportModel.bColumnGroup)
            {
                bandedGridView.BestFitColumns();
            }
            else
            {
                gridView.BestFitColumns();
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