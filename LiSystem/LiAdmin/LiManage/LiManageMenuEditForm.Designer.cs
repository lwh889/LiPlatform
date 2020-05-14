namespace LiManage
{
    partial class LiManageMenuEditForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiManageMenuEditForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnModify = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.btnStatus = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditCode = new DevExpress.XtraEditors.TextEdit();
            this.textEditID = new DevExpress.XtraEditors.TextEdit();
            this.textEditName = new DevExpress.XtraEditors.TextEdit();
            this.listEditParentID = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridLookUpEdit_systemCode = new DevExpress.XtraEditors.GridLookUpEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listEditParentID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_systemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnSave,
            this.btnNew,
            this.btnModify,
            this.btnExit,
            this.btnStatus});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(631, 225);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnSave
            // 
            this.btnSave.Caption = "保存";
            this.btnSave.Id = 1;
            this.btnSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.LargeImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.Tag = "LiManageMenuEditForm.btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnNew
            // 
            this.btnNew.Caption = "新增";
            this.btnNew.Id = 2;
            this.btnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.Image")));
            this.btnNew.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.LargeImage")));
            this.btnNew.Name = "btnNew";
            this.btnNew.Tag = "LiManageMenuEditForm.btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnModify
            // 
            this.btnModify.Caption = "修改";
            this.btnModify.Id = 3;
            this.btnModify.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.ImageOptions.Image")));
            this.btnModify.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnModify.ImageOptions.LargeImage")));
            this.btnModify.Name = "btnModify";
            this.btnModify.Tag = "LiManageMenuEditForm.btnModify";
            this.btnModify.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnModify_ItemClick);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出";
            this.btnExit.Id = 4;
            this.btnExit.ImageOptions.Image = global::LiManage.Properties.Resources.cancel_16x161;
            this.btnExit.ImageOptions.LargeImage = global::LiManage.Properties.Resources.cancel_32x321;
            this.btnExit.Name = "btnExit";
            this.btnExit.Tag = "LiManageMenuEditForm.btnExit";
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExit_ItemClick);
            // 
            // btnStatus
            // 
            this.btnStatus.Caption = "状态设置";
            this.btnStatus.Id = 5;
            this.btnStatus.ImageOptions.Image = global::LiManage.Properties.Resources.status_16x16;
            this.btnStatus.ImageOptions.LargeImage = global::LiManage.Properties.Resources.status_32x32;
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Tag = "LiManageMenuEditForm.btnStatus";
            this.btnStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStatus_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.MergeOrder = 0;
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "功能区";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnModify);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnStatus);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExit);
            this.ribbonPageGroup1.MergeOrder = 1;
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "基本操作";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 658);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(631, 48);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridLookUpEdit_systemCode);
            this.layoutControl1.Controls.Add(this.textEditCode);
            this.layoutControl1.Controls.Add(this.textEditID);
            this.layoutControl1.Controls.Add(this.textEditName);
            this.layoutControl1.Controls.Add(this.listEditParentID);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 225);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1017, 125, 675, 600);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(631, 433);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEditCode
            // 
            this.textEditCode.Location = new System.Drawing.Point(93, 100);
            this.textEditCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textEditCode.MenuManager = this.ribbon;
            this.textEditCode.Name = "textEditCode";
            this.textEditCode.Size = new System.Drawing.Size(219, 28);
            this.textEditCode.StyleController = this.layoutControl1;
            this.textEditCode.TabIndex = 8;
            this.textEditCode.Tag = "LiManageMenuEditForm.Code";
            // 
            // textEditID
            // 
            this.textEditID.EditValue = "";
            this.textEditID.Location = new System.Drawing.Point(93, 18);
            this.textEditID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textEditID.MenuManager = this.ribbon;
            this.textEditID.Name = "textEditID";
            this.textEditID.Properties.ReadOnly = true;
            this.textEditID.Size = new System.Drawing.Size(219, 28);
            this.textEditID.StyleController = this.layoutControl1;
            this.textEditID.TabIndex = 7;
            this.textEditID.Tag = "LiManageMenuEditForm.ID";
            // 
            // textEditName
            // 
            this.textEditName.EditValue = "";
            this.textEditName.Location = new System.Drawing.Point(93, 124);
            this.textEditName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textEditName.MenuManager = this.ribbon;
            this.textEditName.Name = "textEditName";
            this.textEditName.Size = new System.Drawing.Size(219, 28);
            this.textEditName.StyleController = this.layoutControl1;
            this.textEditName.TabIndex = 5;
            this.textEditName.Tag = "LiManageMenuEditForm.Name";
            // 
            // listEditParentID
            // 
            this.listEditParentID.EditValue = "父节点";
            this.listEditParentID.Location = new System.Drawing.Point(93, 42);
            this.listEditParentID.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listEditParentID.MenuManager = this.ribbon;
            this.listEditParentID.Name = "listEditParentID";
            this.listEditParentID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.listEditParentID.Properties.NullText = "";
            this.listEditParentID.Properties.PopupSizeable = false;
            this.listEditParentID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.listEditParentID.Properties.TreeList = this.treeListLookUpEdit1TreeList;
            this.listEditParentID.Size = new System.Drawing.Size(219, 28);
            this.listEditParentID.StyleController = this.layoutControl1;
            this.listEditParentID.TabIndex = 4;
            this.listEditParentID.Tag = "LiManageMenuEditForm.ParentID";
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.treeListLookUpEdit1TreeList.Location = new System.Drawing.Point(0, 0);
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            this.treeListLookUpEdit1TreeList.Size = new System.Drawing.Size(400, 200);
            this.treeListLookUpEdit1TreeList.TabIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(631, 433);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.listEditParentID;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(300, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "父节点";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 22);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 130);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(601, 273);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEditName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 106);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(300, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "菜单名称";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditID;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(300, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "父ID";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 22);
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEditCode;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 82);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(300, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(105, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(601, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "菜单编码";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 22);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Add_16x16.png");
            this.imageList1.Images.SetKeyName(1, "Add_32x32.png");
            this.imageList1.Images.SetKeyName(2, "AddFile_16x16.png");
            this.imageList1.Images.SetKeyName(3, "AddFile_32x32.png");
            this.imageList1.Images.SetKeyName(4, "AddItem_16x16.png");
            this.imageList1.Images.SetKeyName(5, "AddItem_32x32.png");
            this.imageList1.Images.SetKeyName(6, "Apply_16x16.png");
            // 
            // gridLookUpEdit_systemCode
            // 
            this.gridLookUpEdit_systemCode.EditValue = "LiManageMenuEditForm.ParentID";
            this.gridLookUpEdit_systemCode.Location = new System.Drawing.Point(93, 66);
            this.gridLookUpEdit_systemCode.MenuManager = this.ribbon;
            this.gridLookUpEdit_systemCode.Name = "gridLookUpEdit_systemCode";
            this.gridLookUpEdit_systemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEdit_systemCode.Properties.View = this.gridLookUpEdit1View;
            this.gridLookUpEdit_systemCode.Size = new System.Drawing.Size(219, 28);
            this.gridLookUpEdit_systemCode.StyleController = this.layoutControl1;
            this.gridLookUpEdit_systemCode.TabIndex = 9;
            this.gridLookUpEdit_systemCode.Tag = "LiManageMenuEditForm.systemCode";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gridLookUpEdit_systemCode;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(300, 34);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(131, 34);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(601, 34);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "系统代码";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(72, 22);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // LiManageMenuEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 706);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LiManageMenuEditForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "菜单编辑";
            this.Load += new System.EventHandler(this.LiManageMenuEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listEditParentID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_systemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnModify;
        private DevExpress.XtraEditors.TextEdit textEditName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TreeListLookUpEdit listEditParentID;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraEditors.TextEdit textEditID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        public DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnStatus;
        private DevExpress.XtraEditors.TextEdit textEditCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit_systemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}