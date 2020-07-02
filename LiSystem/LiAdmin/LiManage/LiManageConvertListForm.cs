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
using LiModel.LiConvert;

namespace LiManage
{
    public partial class LiManageConvertListForm : DevExpress.XtraBars.Ribbon.RibbonForm
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

        public LiManageConvertListForm()
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
            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert,LiContext.SystemCode).getEntityList<LiConvertHeadModel>();
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
            LiConvertHeadModel liConvertHeadModel = LiConvertHeadModel.getInstance();
            LiManageConvertForm2 ribbonForm = new LiManageConvertForm2(liConvertHeadModel);
            LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(-1, ribbonForm, "LiManageConvertForm"), this.ParentForm);
            ribbonForm.setNewStatus();

        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            openEditForm();
        }

        public void openEditForm()
        {
            LiConvertHeadModel tableModel = gridView1.GetFocusedRow() as LiConvertHeadModel;
            LiManageConvertForm2 ribbonForm = new LiManageConvertForm2(tableModel);
            LiContexts.LiContext.AddPageMdi(PageFormModel.getInstance(tableModel.id,  ribbonForm, ribbonForm.Name), this.ParentForm);
            ribbonForm.setEditStatus();
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void LiManageConvertListForm_Load(object sender, EventArgs e)
        {
            Query();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            openEditForm();
        }

        private void BtnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiConvertHeadModel liConvertHeadModel = gridView1.GetFocusedRow() as LiConvertHeadModel;
            if (liConvertHeadModel != null)
            {
                LiContexts.LiContext.getHttpEntity(LiHttp.Enum.LiEntityKey.LiConvert, LiContext.SystemCode).deleteEntity(liConvertHeadModel);
                if (LiContexts.LiContext.getHttpEntity(LiHttp.Enum.LiEntityKey.LiConvert, LiContext.SystemCode).bSuccess)
                {
                    MessageUtil.Show("删除成功！", "温馨提示");
                }
                else
                {
                    MessageUtil.Show("删除失败！" + LiContexts.LiContext.getHttpEntity(LiHttp.Enum.LiEntityKey.LiConvert, LiContext.SystemCode).resultContent, "温馨提示");
                }
            }
        }
    }
}