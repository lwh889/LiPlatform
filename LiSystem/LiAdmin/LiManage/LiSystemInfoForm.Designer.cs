namespace LiManage
{
    partial class LiSystemInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiSystemInfoForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.btnSelect = new DevExpress.XtraBars.BarButtonItem();
            this.btnStatus = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEdit_systemCode = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_systemDataBaseName = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_systemName = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_systemTitle = new DevExpress.XtraEditors.TextEdit();
            this.pictureEdit_CompanyLogo = new DevExpress.XtraEditors.PictureEdit();
            this.textEdit_CompanyName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemDataBaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemTitle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit_CompanyLogo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_CompanyName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnSave,
            this.btnExit,
            this.btnSelect,
            this.btnStatus});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 5;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(887, 225);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnSave
            // 
            this.btnSave.Caption = "保存";
            this.btnSave.Id = 1;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.LargeImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.Tag = "LiSystemInfoForm.btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSave_ItemClick);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出";
            this.btnExit.Id = 2;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.LargeImage")));
            this.btnExit.Name = "btnExit";
            this.btnExit.Tag = "LiSystemInfoForm.btnExit";
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnExit_ItemClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Caption = "选择公司Logo";
            this.btnSelect.Id = 3;
            this.btnSelect.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.ImageOptions.Image")));
            this.btnSelect.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSelect.ImageOptions.LargeImage")));
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Tag = "LiSystemInfoForm.btnSelect";
            this.btnSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSelect_ItemClick);
            // 
            // btnStatus
            // 
            this.btnStatus.Caption = "状态";
            this.btnStatus.Id = 4;
            this.btnStatus.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnStatus.ImageOptions.LargeImage")));
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Tag = "LiSystemInfoForm.btnStatus";
            this.btnStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnStatus_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "功能区";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSelect);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnStatus);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExit);
            this.ribbonPageGroup1.MergeOrder = 1;
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 599);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(887, 48);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.textEdit_systemCode);
            this.layoutControl1.Controls.Add(this.textEdit_systemDataBaseName);
            this.layoutControl1.Controls.Add(this.textEdit_systemName);
            this.layoutControl1.Controls.Add(this.textEdit_systemTitle);
            this.layoutControl1.Controls.Add(this.pictureEdit_CompanyLogo);
            this.layoutControl1.Controls.Add(this.textEdit_CompanyName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 225);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(887, 374);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEdit_systemCode
            // 
            this.textEdit_systemCode.Location = new System.Drawing.Point(113, 18);
            this.textEdit_systemCode.MenuManager = this.ribbon;
            this.textEdit_systemCode.Name = "textEdit_systemCode";
            this.textEdit_systemCode.Size = new System.Drawing.Size(756, 28);
            this.textEdit_systemCode.StyleController = this.layoutControl1;
            this.textEdit_systemCode.TabIndex = 10;
            this.textEdit_systemCode.Tag = "LiSystemInfoForm.systemCode";
            // 
            // textEdit_systemDataBaseName
            // 
            this.textEdit_systemDataBaseName.Location = new System.Drawing.Point(113, 52);
            this.textEdit_systemDataBaseName.MenuManager = this.ribbon;
            this.textEdit_systemDataBaseName.Name = "textEdit_systemDataBaseName";
            this.textEdit_systemDataBaseName.Size = new System.Drawing.Size(756, 28);
            this.textEdit_systemDataBaseName.StyleController = this.layoutControl1;
            this.textEdit_systemDataBaseName.TabIndex = 9;
            this.textEdit_systemDataBaseName.Tag = "LiSystemInfoForm.systemDataBaseName";
            // 
            // textEdit_systemName
            // 
            this.textEdit_systemName.Location = new System.Drawing.Point(113, 86);
            this.textEdit_systemName.MenuManager = this.ribbon;
            this.textEdit_systemName.Name = "textEdit_systemName";
            this.textEdit_systemName.Size = new System.Drawing.Size(756, 28);
            this.textEdit_systemName.StyleController = this.layoutControl1;
            this.textEdit_systemName.TabIndex = 8;
            this.textEdit_systemName.Tag = "LiSystemInfoForm.systemName";
            // 
            // textEdit_systemTitle
            // 
            this.textEdit_systemTitle.Location = new System.Drawing.Point(113, 120);
            this.textEdit_systemTitle.MenuManager = this.ribbon;
            this.textEdit_systemTitle.Name = "textEdit_systemTitle";
            this.textEdit_systemTitle.Size = new System.Drawing.Size(756, 28);
            this.textEdit_systemTitle.StyleController = this.layoutControl1;
            this.textEdit_systemTitle.TabIndex = 7;
            this.textEdit_systemTitle.Tag = "LiSystemInfoForm.systemTitle";
            // 
            // pictureEdit_CompanyLogo
            // 
            this.pictureEdit_CompanyLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureEdit_CompanyLogo.Location = new System.Drawing.Point(113, 188);
            this.pictureEdit_CompanyLogo.MenuManager = this.ribbon;
            this.pictureEdit_CompanyLogo.Name = "pictureEdit_CompanyLogo";
            this.pictureEdit_CompanyLogo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit_CompanyLogo.Size = new System.Drawing.Size(756, 94);
            this.pictureEdit_CompanyLogo.StyleController = this.layoutControl1;
            this.pictureEdit_CompanyLogo.TabIndex = 6;
            this.pictureEdit_CompanyLogo.Tag = "LiSystemInfoForm.companyLogo";
            // 
            // textEdit_CompanyName
            // 
            this.textEdit_CompanyName.Location = new System.Drawing.Point(113, 154);
            this.textEdit_CompanyName.MenuManager = this.ribbon;
            this.textEdit_CompanyName.Name = "textEdit_CompanyName";
            this.textEdit_CompanyName.Size = new System.Drawing.Size(756, 28);
            this.textEdit_CompanyName.StyleController = this.layoutControl1;
            this.textEdit_CompanyName.TabIndex = 4;
            this.textEdit_CompanyName.Tag = "LiSystemInfoForm.companyName";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(887, 374);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 270);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(857, 74);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.pictureEdit_CompanyLogo;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 170);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(857, 100);
            this.layoutControlItem3.Text = "公司Logo";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEdit_systemTitle;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 102);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(857, 34);
            this.layoutControlItem2.Text = "系统标题";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEdit_CompanyName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 136);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(857, 34);
            this.layoutControlItem1.Text = "公司名称";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEdit_systemName;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(857, 34);
            this.layoutControlItem4.Text = "系统名称";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.textEdit_systemDataBaseName;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(857, 34);
            this.layoutControlItem5.Text = "系统数据库";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(90, 22);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.textEdit_systemCode;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(857, 34);
            this.layoutControlItem6.Text = "系统代码";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(90, 22);
            // 
            // LiSystemInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 647);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "LiSystemInfoForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "公司信息设置";
            this.Load += new System.EventHandler(this.LiSystemInfoForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemDataBaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_systemTitle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit_CompanyLogo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_CompanyName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit textEdit_CompanyName;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit_CompanyLogo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraBars.BarButtonItem btnSelect;
        private DevExpress.XtraBars.BarButtonItem btnStatus;
        private DevExpress.XtraEditors.TextEdit textEdit_systemTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit textEdit_systemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit textEdit_systemDataBaseName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit textEdit_systemCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}