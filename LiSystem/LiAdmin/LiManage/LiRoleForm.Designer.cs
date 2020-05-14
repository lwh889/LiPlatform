namespace LiManage
{
    partial class LiRoleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiRoleForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnAddRole = new DevExpress.XtraBars.BarButtonItem();
            this.btnEditRole = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteRole = new DevExpress.XtraBars.BarButtonItem();
            this.btnSetAuth = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.btnSetStatus = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnAddRole,
            this.btnEditRole,
            this.btnDeleteRole,
            this.btnSetAuth,
            this.btnExit,
            this.btnSetStatus});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 8;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(741, 147);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnAddRole
            // 
            this.btnAddRole.Caption = "添加角色";
            this.btnAddRole.Id = 1;
            this.btnAddRole.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRole.ImageOptions.Image")));
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Tag = "liRoles.btnAddRole";
            this.btnAddRole.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddRole_ItemClick);
            // 
            // btnEditRole
            // 
            this.btnEditRole.Caption = "编辑角色";
            this.btnEditRole.Id = 2;
            this.btnEditRole.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEditRole.ImageOptions.Image")));
            this.btnEditRole.Name = "btnEditRole";
            this.btnEditRole.Tag = "liRoles.btnEditRole";
            this.btnEditRole.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEditRole_ItemClick);
            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.Caption = "删除角色";
            this.btnDeleteRole.Id = 3;
            this.btnDeleteRole.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteRole.ImageOptions.Image")));
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Tag = "liRoles.btnDeleteRole";
            this.btnDeleteRole.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteRole_ItemClick);
            // 
            // btnSetAuth
            // 
            this.btnSetAuth.Caption = "设置权限";
            this.btnSetAuth.Id = 4;
            this.btnSetAuth.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSetAuth.ImageOptions.Image")));
            this.btnSetAuth.Name = "btnSetAuth";
            this.btnSetAuth.Tag = "liRoles.btnSetAuth";
            this.btnSetAuth.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetAuth_ItemClick);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出";
            this.btnExit.Id = 6;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.LargeImage")));
            this.btnExit.Name = "btnExit";
            this.btnExit.Tag = "liRoles.btnExit";
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExit_ItemClick);
            // 
            // btnSetStatus
            // 
            this.btnSetStatus.Caption = "设置状态";
            this.btnSetStatus.Id = 7;
            this.btnSetStatus.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSetStatus.ImageOptions.Image")));
            this.btnSetStatus.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSetStatus.ImageOptions.LargeImage")));
            this.btnSetStatus.Name = "btnSetStatus";
            this.btnSetStatus.Tag = "liRoles.btnSetStatus";
            this.btnSetStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSetStatus_ItemClick);
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
            this.ribbonPageGroup1.ItemLinks.Add(this.btnAddRole);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnEditRole);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDeleteRole);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSetAuth);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSetStatus);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExit);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 460);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(741, 31);
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
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel1.ID = new System.Guid("a9ad1492-7f0f-47eb-83bb-00089078c34f");
            this.dockPanel1.Location = new System.Drawing.Point(541, 147);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(200, 313);
            this.dockPanel1.Text = "dockPanel1";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(191, 286);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 147);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.ribbon;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridControl1.Size = new System.Drawing.Size(541, 313);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.Click += new System.EventHandler(this.gridView1_Click);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "角色编码";
            this.gridColumn1.FieldName = "roleCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Tag = "liRoles.roleCode";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "角色名称";
            this.gridColumn2.FieldName = "roleName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Tag = "liRoles.roleName";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            this.repositoryItemTextEdit1.PasswordChar = '*';
            // 
            // LiRoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 491);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "LiRoleForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "LiRoleForm";
            this.Load += new System.EventHandler(this.LiRoleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnAddRole;
        private DevExpress.XtraBars.BarButtonItem btnEditRole;
        private DevExpress.XtraBars.BarButtonItem btnDeleteRole;
        private DevExpress.XtraBars.BarButtonItem btnSetAuth;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem btnSetStatus;
    }
}