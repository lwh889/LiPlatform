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

using LiModel.Form;
using LiCommon.Util;
using LiForm.LiStatus;
using LiModel.Dev.GridlookUpEdit;
using LiHttp.Server;
using LiHttp.RequestParam;
using LiContexts.Model;
using LiHttp.GetEntity;

using LiModel.LiConvert;
using LiModel.LiEnum;
using LiForm.Dev.Util;
using LiModel.Util;
using LiControl.Util;
using LiContexts;
using LiModel.Basic;
using LiVoucherConvert;

namespace LiForm.Dev
{
    public partial class LiRefForm : DevExpress.XtraBars.Ribbon.RibbonForm
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
        /// 表体表名
        /// </summary>
        string bodyTableName = string.Empty;

        private List<DataRow> _SelectDataRows = new List<DataRow>();

        public List<DataRow> SelectDataRows { set { _SelectDataRows = value; } get { return _SelectDataRows; } }
        /// <summary>
        /// 表格列
        /// </summary>
        private Dictionary<string, GridColumn> _liGridColumnDict = new Dictionary<string, GridColumn>();

        public Dictionary<string, GridColumn> liGridColumnDict { set { _liGridColumnDict = value; } get { return _liGridColumnDict; } }

        public List<QuerySchemeModel> querySchemeModels = new List<QuerySchemeModel>();
        private Dictionary<string, SimpleButton> querySchemeBtns = new Dictionary<string, SimpleButton>();
        private Dictionary<string, Control> liQuickQueryControlDict = new Dictionary<string, Control>();

        private LiConvertHeadModel liConvertHeadModel;
        private List<TableModel> tableModelList;
        private string entityKey;


        public LiRefForm(string bodyTableName, LiConvertHeadModel liConvertHeadModel, List<TableModel> tableModelList)
        {
            InitializeComponent();

            this.liConvertHeadModel = liConvertHeadModel;
            this.bodyTableName = bodyTableName;
            this.tableModelList = tableModelList;
            Init();
        }

        private void Init()
        {
            InitData();
            InitControl();
            InitView();
        }

        private void InitData()
        {
            entityKey = liConvertHeadModel.convertSource;
        }

        private void InitControl()
        {
            InitGridColumn();
        }

        public void InitGridColumn()
        {
            int iRow = 1;

            GridColumn gridColumnSel = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnSel.Caption = "选择";
            gridColumnSel.Name = string.Format("LiSel_GridColumn");
            gridColumnSel.FieldName = string.Format("sel");
            gridColumnSel.Visible = true;
            gridColumnSel.VisibleIndex = iRow++;
            gridColumnSel.Width = 50;
            gridColumnSel.OptionsColumn.AllowEdit = true;

            liGridColumnDict.Add(gridColumnSel.FieldName, gridColumnSel);

            foreach (FieldModel fieldModel in liConvertHeadModel.queryFields)
            {
                GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumn.Caption = fieldModel.columnFieldName;
                gridColumn.Name = string.Format("Li{0}{1}_GridColumn", fieldModel.sEntityCode, fieldModel.fieldName);
                gridColumn.FieldName = string.Format("Li{0}_{1}", fieldModel.sEntityCode, fieldModel.fieldName);
                gridColumn.Visible = fieldModel.bColumnDisplay;
                gridColumn.VisibleIndex = iRow++;
                gridColumn.Width = fieldModel.iColumnWidth;
                gridColumn.OptionsColumn.AllowEdit = false;

                gridColumn.Tag = fieldModel;

                liGridColumnDict.Add(gridColumn.FieldName, gridColumn);
            }
        }
        public void InitView()
        {
            gridView1.Columns.Clear();
            gridView1.Columns.AddRange(liGridColumnDict.Values.ToArray());
            gridControl1.Refresh();

            FormUtil.loadQuickQuery(liConvertHeadModel.queryFields, liQuickQueryControlDict, layoutControlGroup2, layoutControl2, this);
        }

        public void setQueryWhere(string queryWhereStr)
        {
            this.queryWhereStr = queryWhereStr;
        }

        public void getQuickQueryWhere()
        {
            queryWhereStr = DevFormUtil.getWhereStr(liQuickQueryControlDict, true);

            queryWhereStr += LiVoucherConvertUtil.getFiliterSQL(liConvertHeadModel, tableModelList);
        }

        public void Query()
        {

            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            paramDict.Add("childTableName", bodyTableName);
            paramDict.Add("systemCode", LiContext.SystemCode);
            paramDict.Add("entityKey", entityKey);
            paramDict.Add("whereSql", queryWhereStr);

            this.pageSum = LiContexts.LiContext.getHttpEntity("sp_QueryList_Count").execProcedureSingleValue_Int32("iCount", paramDict);

            paramDict.Clear();
            paramDict.Add("childTableName", bodyTableName);
            paramDict.Add("entityKey", entityKey);
            paramDict.Add("systemCode", LiContext.SystemCode);
            paramDict.Add("whereSql", queryWhereStr);
            paramDict.Add("orderBySql", "");
            paramDict.Add("rangeSql", string.Format(" and iPageRow > {0} and iPageRow <= {1}", (pageCurrent - 1) * pageSize, pageSize * pageCurrent));

            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity("sp_QueryList").execProcedure_DataTable(paramDict);

            gridView1.BestFitColumns();
        }

        public void FillGridListCtrlQuery(int curPage = 1)   //更新控件
        {
            //  GridControl1.DataSource = WebService.Pager(。。。。。//显示分页结果
            pagerControl1.RefreshPager(pageSum, curPage);//更新分页控件显示。
        }

        private void pagerControl1_myPagerEvents(int curPage, int pageSize)
        {
            this.pageCurrent = curPage;
            this.pageSize = pageSize;

            Query();
            FillGridListCtrlQuery(curPage);
        }

        private void btnReturn_ItemClick(object sender, ItemClickEventArgs e)
        {
            DataTable dt = gridControl1.DataSource as DataTable;
            DataRow[] drs = dt.Select(" sel = True ");
            SelectDataRows.Clear();
            if (drs != null && drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    SelectDataRows.Add(dr);
                }
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            getQuickQueryWhere();
            this.Query();
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}