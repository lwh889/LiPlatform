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

namespace LiManage
{
    public partial class LiManageForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public LiManageForm1()
        {
            InitializeComponent();
            initevent();//初始化事件  
            MainRibbonForm = this;  
        }

        private void initevent()
        {
            navBarItem1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItem1_LinkClicked);
            navBarItem2.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(navBarItem2_LinkClicked);
        }
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            nbiOpenLink(new RibbonForm1(), "新开的窗口1");

        }
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            nbiOpenLink(new RibbonForm2(), "新开的窗口2");

        }
        public static LiManageForm1 MainRibbonForm { get; set; }
        public static void nbiOpenLink(Form frm, string caption)
        {
            try
            {
                MainRibbonForm.Cursor = Cursors.WaitCursor;
                frm.Text = caption;
                frm.MdiParent = MainRibbonForm;
                var f = frm as RibbonForm; if (f != null)
                {
                    f.Ribbon.AllowMinimizeRibbon = true;
                    f.Ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
                    f.Ribbon.Minimized = true;
                    //f.Ribbon.ApplicationIcon = Program.icoWinform.ToBitmap();  
                }
                frm.Show();
                MainRibbonForm.Cursor = Cursors.Default;
            }
            catch (Exception e)
            {

            }
        }  
    }
}