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
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Localization;

using LiCommon.Util;
using LiFlow.Util;

namespace LiAdmin
{
    public partial class RibbonForm4 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public RibbonForm4()
        {
            InitializeComponent();

            InitSkinGallery();
        }

        private void RibbonForm4_Load(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;

            pictureBox2.Parent = pictureBox1;
            //FlowUtil.replaceMessagePlaceholder("单据编号【{TestHead.billCode}】,请审核！【{TestHead.billCode123}】", null);
            //DataTable dt = new DataTable();
            //dt.Columns.Add("ID", Type.GetType("System.Int32"));
            //dt.Columns.Add("ParentID", Type.GetType("System.Int32"));
            //dt.Columns.Add("Name", Type.GetType("System.String"));

            //DataRow dr = dt.NewRow();
            //dr["ID"] = 1;
            //dr["ParentID"] = DBNull.Value;
            //dr["Name"] = "一";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //dr["ID"] = 2;
            //dr["ParentID"] = DBNull.Value;
            //dr["Name"] = "二";
            //dt.Rows.Add(dr); dr = dt.NewRow();
            //dr["ID"] = 3;
            //dr["ParentID"] = 1;
            //dr["Name"] = "三";
            //dt.Rows.Add(dr);

            //treeList1.DataSource = dt;
            //treeListLookUpEdit1.Properties.DataSource = dt;
            //treeListLookUpEdit1.EditValue = 2;
        }

        void InitSkinGallery()
        {
            DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(ribbonGalleryBarItem1, true);
            this.ribbon.Toolbar.ItemLinks.Clear();
            this.ribbon.Toolbar.ItemLinks.Add(ribbonGalleryBarItem1);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void TextEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }

}