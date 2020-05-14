namespace LiManage
{
    partial class LiManageForm2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiManageForm2));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddMenu = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddVoucher = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddU8BasicInfo = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddBasicInfo = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditMenu = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteMenu = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteVoucher = new DevExpress.XtraBars.BarButtonGroup();
            this.btnDeleteU8BasicInfo = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteBasicInfo = new DevExpress.XtraBars.BarButtonItem();
            this.btnAddMenuGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteMenuGroup = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditMenuGroup = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.popupMenuTree = new DevExpress.XtraBars.PopupMenu();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTree)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ribbon.ApplicationIcon")));
            this.ribbon.DrawGroupCaptions = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnExit,
            this.btnAddMenu,
            this.btnAddVoucher,
            this.btnAddU8BasicInfo,
            this.btnAddBasicInfo,
            this.btnEditMenu,
            this.btnDeleteMenu,
            this.btnDeleteVoucher,
            this.btnDeleteU8BasicInfo,
            this.btnDeleteBasicInfo,
            this.btnAddMenuGroup,
            this.btnDeleteMenuGroup,
            this.btnEditMenuGroup});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbon.MaxItemId = 20;
            this.ribbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbon.Size = new System.Drawing.Size(1544, 195);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            this.ribbon.Merge += new DevExpress.XtraBars.Ribbon.RibbonMergeEventHandler(this.ribbon_Merge);
            this.ribbon.UnMerge += new DevExpress.XtraBars.Ribbon.RibbonMergeEventHandler(this.ribbon_UnMerge);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出系统 ";
            this.btnExit.Id = 7;
            this.btnExit.ImageOptions.Image = global::LiManage.Properties.Resources.cancel_16x16;
            this.btnExit.ImageOptions.LargeImage = global::LiManage.Properties.Resources.cancel_32x32;
            this.btnExit.Name = "btnExit";
            // 
            // btnAddMenu
            // 
            this.btnAddMenu.Caption = "新增菜单";
            this.btnAddMenu.Id = 8;
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddMenu_ItemClick);
            // 
            // btnAddVoucher
            // 
            this.btnAddVoucher.Caption = "新增单据";
            this.btnAddVoucher.Id = 9;
            this.btnAddVoucher.Name = "btnAddVoucher";
            this.btnAddVoucher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddVoucher_ItemClick);
            // 
            // btnAddU8BasicInfo
            // 
            this.btnAddU8BasicInfo.Caption = "新增U8基础档案";
            this.btnAddU8BasicInfo.Id = 10;
            this.btnAddU8BasicInfo.Name = "btnAddU8BasicInfo";
            this.btnAddU8BasicInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddU8BasicInfo_ItemClick);
            // 
            // btnAddBasicInfo
            // 
            this.btnAddBasicInfo.Caption = "新增基础档案";
            this.btnAddBasicInfo.Id = 11;
            this.btnAddBasicInfo.Name = "btnAddBasicInfo";
            this.btnAddBasicInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddBasicInfo_ItemClick);
            // 
            // btnEditMenu
            // 
            this.btnEditMenu.Caption = "编辑菜单";
            this.btnEditMenu.Id = 12;
            this.btnEditMenu.Name = "btnEditMenu";
            this.btnEditMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditMenu_ItemClick);
            // 
            // btnDeleteMenu
            // 
            this.btnDeleteMenu.Caption = "删除菜单";
            this.btnDeleteMenu.Id = 13;
            this.btnDeleteMenu.Name = "btnDeleteMenu";
            this.btnDeleteMenu.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteMenu_ItemClick);
            // 
            // btnDeleteVoucher
            // 
            this.btnDeleteVoucher.Caption = "删除单据";
            this.btnDeleteVoucher.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.btnDeleteVoucher.Id = 14;
            this.btnDeleteVoucher.Name = "btnDeleteVoucher";
            this.btnDeleteVoucher.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteVoucher_ItemClick);
            // 
            // btnDeleteU8BasicInfo
            // 
            this.btnDeleteU8BasicInfo.Caption = "删除U8基础档案";
            this.btnDeleteU8BasicInfo.Id = 15;
            this.btnDeleteU8BasicInfo.Name = "btnDeleteU8BasicInfo";
            this.btnDeleteU8BasicInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteU8BasicInfo_ItemClick);
            // 
            // btnDeleteBasicInfo
            // 
            this.btnDeleteBasicInfo.Caption = "删除基础档案";
            this.btnDeleteBasicInfo.Id = 16;
            this.btnDeleteBasicInfo.Name = "btnDeleteBasicInfo";
            this.btnDeleteBasicInfo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteBasicInfo_ItemClick);
            // 
            // btnAddMenuGroup
            // 
            this.btnAddMenuGroup.Caption = "新增菜单组";
            this.btnAddMenuGroup.Id = 17;
            this.btnAddMenuGroup.Name = "btnAddMenuGroup";
            this.btnAddMenuGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddMenuGroup_ItemClick);
            // 
            // btnDeleteMenuGroup
            // 
            this.btnDeleteMenuGroup.Caption = "删除菜单组";
            this.btnDeleteMenuGroup.Id = 18;
            this.btnDeleteMenuGroup.Name = "btnDeleteMenuGroup";
            this.btnDeleteMenuGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteMenuGroup_ItemClick);
            // 
            // btnEditMenuGroup
            // 
            this.btnEditMenuGroup.Caption = "编辑菜单组";
            this.btnEditMenuGroup.Id = 19;
            this.btnEditMenuGroup.Name = "btnEditMenuGroup";
            this.btnEditMenuGroup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditMenuGroup_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.MergeOrder = 999;
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "功能区";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExit);
            this.ribbonPageGroup1.MergeOrder = 999;
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "系统操作";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 929);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1544, 48);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabbedMdiManager1_MouseDown);
            this.xtraTabbedMdiManager1.PageRemoved += new DevExpress.XtraTabbedMdi.MdiTabPageEventHandler(this.xtraTabbedMdiManager1_PageRemoved);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("333e02f4-bd23-4dac-b2b3-4aef4cd27ead");
            this.dockPanel1.Location = new System.Drawing.Point(0, 195);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(281, 200);
            this.dockPanel1.Size = new System.Drawing.Size(281, 734);
            this.dockPanel1.Text = "菜单";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.treeList1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(6, 35);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(266, 693);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // treeList1
            // 
            this.treeList1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.ShowColumns = false;
            this.treeList1.SelectImageList = this.imageList1;
            this.treeList1.Size = new System.Drawing.Size(266, 693);
            this.treeList1.TabIndex = 0;
            this.treeList1.GetSelectImage += new DevExpress.XtraTreeList.GetSelectImageEventHandler(this.treeList1_GetSelectImage);
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.FocusedNodeChanged);
            this.treeList1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseUp);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "名称";
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.MinWidth = 34;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "buju.png");
            this.imageList1.Images.SetKeyName(1, "canzhaoyinyong.png");
            this.imageList1.Images.SetKeyName(2, "chakan-2.png");
            this.imageList1.Images.SetKeyName(3, "chexiaotijiao.png");
            this.imageList1.Images.SetKeyName(4, "diyiye.png");
            this.imageList1.Images.SetKeyName(5, "diyiye-2.png");
            this.imageList1.Images.SetKeyName(6, "down.png");
            this.imageList1.Images.SetKeyName(7, "fanhuishuju.png");
            this.imageList1.Images.SetKeyName(8, "geshi.png");
            this.imageList1.Images.SetKeyName(9, "gouhao.png");
            this.imageList1.Images.SetKeyName(10, "houyiye.png");
            this.imageList1.Images.SetKeyName(11, "qianyiye.png");
            this.imageList1.Images.SetKeyName(12, "qitachukudan.png");
            this.imageList1.Images.SetKeyName(13, "qitarukudan.png");
            this.imageList1.Images.SetKeyName(14, "streamlist.png");
            this.imageList1.Images.SetKeyName(15, "U8+logo_CS.ico");
            this.imageList1.Images.SetKeyName(16, "up.png");
            this.imageList1.Images.SetKeyName(17, "zuihouyiye.png");
            // 
            // popupMenuTree
            // 
            this.popupMenuTree.ItemLinks.Add(this.btnAddMenu);
            this.popupMenuTree.ItemLinks.Add(this.btnAddMenuGroup);
            this.popupMenuTree.ItemLinks.Add(this.btnEditMenu);
            this.popupMenuTree.ItemLinks.Add(this.btnEditMenuGroup);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteMenu);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteMenuGroup);
            this.popupMenuTree.ItemLinks.Add(this.btnAddVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnAddU8BasicInfo);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnAddBasicInfo);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteU8BasicInfo);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteBasicInfo);
            this.popupMenuTree.ItemLinks.Add(this.btnDeleteVoucher);
            this.popupMenuTree.Name = "popupMenuTree";
            this.popupMenuTree.Ribbon = this.ribbon;
            // 
            // LiManageForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1544, 977);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LiManageForm2";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "系统设计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabbedMdiManager1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuTree)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnAddMenu;
        private DevExpress.XtraBars.BarButtonItem btnAddVoucher;
        private DevExpress.XtraBars.BarButtonItem btnAddU8BasicInfo;
        private DevExpress.XtraBars.BarButtonItem btnAddBasicInfo;
        private DevExpress.XtraBars.BarButtonItem btnEditMenu;
        private DevExpress.XtraBars.BarButtonItem btnDeleteMenu;
        private DevExpress.XtraBars.BarButtonGroup btnDeleteVoucher;
        private DevExpress.XtraBars.BarButtonItem btnDeleteU8BasicInfo;
        private DevExpress.XtraBars.BarButtonItem btnDeleteBasicInfo;
        private DevExpress.XtraBars.PopupMenu popupMenuTree;
        private DevExpress.XtraBars.BarButtonItem btnAddMenuGroup;
        private DevExpress.XtraBars.BarButtonItem btnDeleteMenuGroup;
        private DevExpress.XtraBars.BarButtonItem btnEditMenuGroup;
    }
}