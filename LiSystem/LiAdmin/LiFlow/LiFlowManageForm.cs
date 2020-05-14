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
using LiHttp.Enum;
using LiContexts;
using LiFlow.Model;
using LiFlow.Util;
using LiFlow.Enums;
using LiCommon.Util;

namespace LiFlow
{
    public partial class LiFlowManageForm : DevExpress.XtraBars.Ribbon.RibbonForm
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

        public LiFlowManageForm()
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
            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVoucherFlow, LiContext.SystemCode).getEntityList<LiVoucherFlowModel>();
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

        private void btnShowFlow_ItemClick(object sender, ItemClickEventArgs e)
        {
            showFlowForm();
        }

        private void btnStopShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            stopFlow();
        }

        private void stopFlow()
        {
            string resultContent = string.Empty;

            LiVoucherFlowModel liVoucherFlowModel = gridView1.GetFocusedRow() as LiVoucherFlowModel;
            
            LiVoucherFlowModel liVoucherFlowTemp = FlowUtil.getCurrentFlow(liVoucherFlowModel.entityKey, liVoucherFlowModel.voucherId);

            FlowUtil.revokeFlow(FlowStatus.STOP, RevokeType.Stop, liVoucherFlowModel.entityKey, liVoucherFlowModel.voucherId, liVoucherFlowModel.datas.LastOrDefault(), out resultContent);

            MessageUtil.Show(resultContent, "温馨提示");
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            showFlowForm();
        }


        private void showFlowForm()
        {
            LiVoucherFlowModel liVoucherFlowModel = gridView1.GetFocusedRow() as LiVoucherFlowModel;

            LiVersionFlowModel liVersionFlowModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).getEntitySingle<LiVersionFlowModel>(liVoucherFlowModel.flowVersionId, "id");
            LiShowFlowForm ribbonForm = new LiShowFlowForm(liVersionFlowModel, liVoucherFlowModel);

            ribbonForm.ShowDialog();
        }
    }
}