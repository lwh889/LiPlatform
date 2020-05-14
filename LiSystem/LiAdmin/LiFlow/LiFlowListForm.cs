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

using LiFlow.Model;
using LiHttp.Enum;
using LiContexts;

namespace LiFlow
{
    public partial class LiFlowListForm : DevExpress.XtraBars.Ribbon.RibbonForm
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

        public LiFlowListForm()
        {
            InitializeComponent();
        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {

        }

        public void InitControl()
        {

        }

        public void Query()
        {
            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiFlow, LiContext.SystemCode).getEntityList<LiFlowModel>();
            gridView1.BestFitColumns();
        }

        private void btnQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            Query();
        }

        private void btnRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Query();
        }

        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiFlowModel liFlowModel = LiFlowModel.getInstance();
            LiFlowForm ribbonForm = new LiFlowForm(liFlowModel);
            ribbonForm.setNewStatus();
            ribbonForm.ShowDialog();
            //LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(-1, "LiFlow", ribbonForm), this.ParentForm);
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            showLiFlowForm();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            showLiFlowForm();
        }

        private void showLiFlowForm()
        {
            LiFlowModel liFlowModel = gridView1.GetFocusedRow() as LiFlowModel;

            LiFlowForm ribbonForm = new LiFlowForm(liFlowModel);

            ribbonForm.setEditStatus();
            ribbonForm.ShowDialog();
        }

        private void btnRelease_ItemClick(object sender, ItemClickEventArgs e)
        {
        }
    }
}