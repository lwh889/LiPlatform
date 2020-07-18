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
using LiModel.Basic;
using LiContexts.Model;
using LiCommon.Util;
using LiModel.LiReport;

namespace LiManage
{
    public partial class LiReportDesignListForm : DevExpress.XtraBars.Ribbon.RibbonForm
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

        private LiManageForm2 manageForm;
        public LiReportDesignListForm(LiManageForm2 manageForm)
        {
            InitializeComponent();
            this.manageForm = manageForm;
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
            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiReport).getEntityList<LiReportModel>();
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
            LiReportModel liReportModel = LiReportModel.getInstance();
            LiReportDesignForm ribbonForm = new LiReportDesignForm(liReportModel, manageForm);
            LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(-1, ribbonForm, "LiReportDesignForm"), this.ParentForm);
            ribbonForm.setNewStatus();

        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            openEditForm();
        }

        public void openEditForm()
        {
            LiReportModel liReportModel = gridView1.GetFocusedRow() as LiReportModel;
            LiReportDesignForm ribbonForm = new LiReportDesignForm(liReportModel, manageForm);
            LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(liReportModel.id,  ribbonForm, ribbonForm.Name), this.ParentForm);
            ribbonForm.setEditStatus();
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void LiReportDesignListForm_Load(object sender, EventArgs e)
        {
            Query();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            openEditForm();
        }

        private void BtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiReportModel liReportModel = gridView1.GetFocusedRow() as LiReportModel;
            if (liReportModel != null)
            {
                LiContexts.LiContext.getHttpEntity(LiHttp.Enum.LiEntityKey.LiReport).deleteEntity(liReportModel);
                if (LiContexts.LiContext.getHttpEntity(LiHttp.Enum.LiEntityKey.LiReport).bSuccess)
                {
                    MessageUtil.Show("删除成功！", "温馨提示");
                }
                else
                {
                    MessageUtil.Show("删除失败！" + LiContexts.LiContext.getHttpEntity(LiHttp.Enum.LiEntityKey.LiReport).resultContent, "温馨提示");
                }
            }
        }
    }
}