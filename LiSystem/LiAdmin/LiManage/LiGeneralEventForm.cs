using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using LiHttp.Enum;
using LiContexts;
using LiModel.Form;
using LiControl.Util;

namespace LiManage
{
    public partial class LiGeneralEventForm : DevExpress.XtraEditors.XtraForm
    {
        private List<GeneralEventModel> _DataSources = new List<GeneralEventModel>();
        public List<GeneralEventModel> DataSources
        {
            set { _DataSources = value; }
            get { return _DataSources; }
        }

        private List<GeneralEventModel> _SelectRows = new List<GeneralEventModel>();
        public List<GeneralEventModel> SelectRows
        {
            set { _SelectRows = value; }
            get { return _SelectRows; }
        }

        public LiGeneralEventForm()
        {
            InitializeComponent();
        }

        private void loadData()
        {
            DataSources = getEvents();
            gridControl1.DataSource = DataSources;
            gridView1.BestFitColumns();
            gridControl1.RefreshDataSource();
        }

        private List<GeneralEventModel> getEvents()
        {
            //string resultContent;
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "liGeneralEvent");

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            //{
            //    return JsonUtil.GetEntityToList<GeneralEventModel>(resultContent);
            //}

            return LiContexts.LiContext.getHttpEntity(LiEntityKey.GeneralEvent).getEntityList<GeneralEventModel>();

        }

        private void LiGeneralEvent_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnReturnData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            _SelectRows.Clear();
            if (gridControl1.DataSource != null)
            {
                _SelectRows = DevControlUtil.getSelectDatas<GeneralEventModel>(gridView1);

                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                MessageBox.Show("请选择数据！", "温馨提示");
            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}