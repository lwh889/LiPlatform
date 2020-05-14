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
    public partial class LiVersionFlowListForm : DevExpress.XtraBars.Ribbon.RibbonForm
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

        public LiVersionFlowListForm()
        {
            InitializeComponent();

            Init();
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
            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).getEntityList<LiVersionFlowModel>();
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
            LiVersionFlowModel liFlowModel = gridView1.GetFocusedRow() as LiVersionFlowModel;

            LiVersionFlowForm ribbonForm = new LiVersionFlowForm(liFlowModel);

            ribbonForm.setEditStatus();
            ribbonForm.ShowDialog();
        }

        private void btnOpen_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnForbid_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}