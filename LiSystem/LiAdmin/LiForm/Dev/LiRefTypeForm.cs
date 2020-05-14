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
using LiModel.LiConvert;
using LiCommon.Util;
using LiModel.LiEnum;
using LiControl.Util;
using LiModel.Basic;

namespace LiForm.Dev
{
    public partial class LiRefTypeForm : DevExpress.XtraEditors.XtraForm
    {
        private LiConvertHeadModel _LiConvertHeadModel;

        /// <summary>
        /// 返回值
        /// </summary>
        public LiConvertHeadModel liConvertHeadModel { get { return _LiConvertHeadModel; } set { _LiConvertHeadModel = value; } }

        private List<LiConvertHeadModel> liConvertHeadModelList;

        public LiRefTypeForm(List<LiConvertHeadModel> liConvertHeadModelList)
        {
            InitializeComponent();

            this.liConvertHeadModelList = liConvertHeadModelList;

            Init();
        }

        private void Init()
        {
            InitData();
            InitControl();
        }

        private void InitData()
        {

        }

        private void InitControl()
        {
            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, LiConvertHeadModel.getValueMember(), LiConvertHeadModel.getDisplayMember(), LiConvertHeadModel.getSearchColumns(), LiConvertHeadModel.getDisplayColumns(), LiConvertHeadModel.getDictModelDesc(), gridLookUpEdit1, this, liConvertHeadModelList);
        }

        private void btnConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            liConvertHeadModel = liConvertHeadModelList.Where(m => m.convertCode == Convert.ToString(gridLookUpEdit1.EditValue)).FirstOrDefault();

            this.DialogResult = DialogResult.Yes;

            this.Close();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}