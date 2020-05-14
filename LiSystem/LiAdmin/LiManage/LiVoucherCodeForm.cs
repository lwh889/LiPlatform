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

using LiModel.Form;
using LiCommon.Util;
using LiHttp.Enum;
using LiContexts;
using LiControl.Util;
using LiModel.Util;


namespace LiManage
{
    public partial class LiVoucherCodeForm : DevExpress.XtraEditors.XtraForm
    {
        FormModel formModel;

        List<GridlookUpEditModel> fieldTextDatasoucre = new List<GridlookUpEditModel>();

        List<GridlookUpEditModel> fieldDateDatasoucre = new List<GridlookUpEditModel>();

        #region 引用数据源
        private GridlookUpEditModel gridlookUpEditModel = new GridlookUpEditModel();
        private FlowNoRangeModel flowNoRangeModel = new FlowNoRangeModel();
        private DateTimeFormatModel dateTimeFormatModel = new DateTimeFormatModel();
        #endregion

        public LiVoucherCodeForm(FormModel formModel)
        {
            InitializeComponent();

            this.formModel = formModel;

            Init();
        }

        public void Init()
        {
            InitData();

            InitControl();
        }

        public void InitData()
        {
            PanelModel panelModel = formModel.panels.Where(m=>m.type == "Basic").FirstOrDefault();

            foreach (ControlGroupModel controlGroupModel in panelModel.controlGroups)
            {
                foreach (ControlModel controlModel in controlGroupModel.controls)
                {
                    switch (controlModel.controltype)
                    {
                        case "TextEdit":
                            fieldTextDatasoucre.Add(new GridlookUpEditModel() { code = controlModel.name, name = controlModel.text });
                            break;

                        case "DateTimeEdit":
                        case "DateEdit":
                            fieldDateDatasoucre.Add(new GridlookUpEditModel() { code = controlModel.name, name = controlModel.text });
                            break;
                    }

                }
            }
        }

        public void InitControl()
        {
            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(),  repositoryItemGridLookUpEdit_FieldText, this, fieldTextDatasoucre);

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(), repositoryItemGridLookUpEdit_FieldDate, this, fieldDateDatasoucre);

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(flowNoRangeModel.getValueMember(), flowNoRangeModel.getDisplayMember(), flowNoRangeModel.getSearchColumns(), flowNoRangeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_flowNoRange, this, flowNoRangeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(dateTimeFormatModel.getValueMember(), dateTimeFormatModel.getDisplayMember(), dateTimeFormatModel.getSearchColumns(), dateTimeFormatModel.getDisplayColumns(), repositoryItemGridLookUpEdit_dateTimeFormat, this, dateTimeFormatModel.getDataSource());

        }

        private void loadData()
        {
            gridControl1.DataSource = LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherCode, LiContext.SystemCode).getEntityList<VoucherCodeModel>(formModel.name, "entityKey");
            btnAddRow_ItemClick(null, null);
        }

        private void getDate()
        {

        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<VoucherCodeModel> voucherCodeList = gridControl1.DataSource as List<VoucherCodeModel>;
            LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherCode, LiContext.SystemCode).batchSaveEntity<VoucherCodeModel>(true, voucherCodeList);
            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherCode, LiContext.SystemCode).tipStr, "系统提示");
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnDeleteRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<VoucherCodeModel> voucherCodeList = gridControl1.DataSource as List<VoucherCodeModel>;
            VoucherCodeModel voucherCodeModel = gridView1.GetFocusedRow() as VoucherCodeModel;
            DataUtil.deleteInList(voucherCodeModel, voucherCodeList);

            gridControl1.DataSource = voucherCodeList;
            gridControl1.RefreshDataSource();
        }

        private void btnAddRow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<VoucherCodeModel> voucherCodeList = gridControl1.DataSource as List<VoucherCodeModel>;
            voucherCodeList.Add(new VoucherCodeModel() { entityKey = formModel.name, flowNoRange = "DAY", iZero=1,iStep = 1,dateTimeFormat = "yyyyMMdd",bDefault = voucherCodeList.Count<= 0 });
            gridControl1.DataSource = voucherCodeList;
            gridControl1.RefreshDataSource();
        }

        private void LiVoucherCodeForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}