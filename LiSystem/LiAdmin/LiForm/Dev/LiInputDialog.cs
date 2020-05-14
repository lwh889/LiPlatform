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

namespace LiForm.Dev
{
    public partial class LiInputDialog : DevExpress.XtraEditors.XtraForm
    {
        private string valueStr;

        public LiInputDialog()
        {
            InitializeComponent();
        }

        public string getValue()
        {
            return valueStr;
        }

        private void btnConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            valueStr = textEdit1.Text;

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}