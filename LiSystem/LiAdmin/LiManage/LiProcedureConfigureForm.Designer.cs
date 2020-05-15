namespace LiManage
{
    partial class LiProcedureConfigureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiProcedureConfigureForm));
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNew = new DevExpress.XtraBars.BarButtonItem();
            this.btnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarButtonItem();
            this.btnStatus = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.textEdit_procedureName = new DevExpress.XtraEditors.TextEdit();
            this.textEdit_entityKey = new DevExpress.XtraEditors.TextEdit();
            this.gridLookUpEdit_dataBaseName = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit_paramType = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barDockControl7 = new DevExpress.XtraBars.BarDockControl();
            this.barManager3 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar5 = new DevExpress.XtraBars.Bar();
            this.btnAddRowWhere = new DevExpress.XtraBars.BarButtonItem();
            this.btnInsertRowWhere = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteRow = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl5 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl6 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl8 = new DevExpress.XtraBars.BarDockControl();
            this.gridLookUpEdit_systemCode = new DevExpress.XtraEditors.GridLookUpEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_procedureName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_entityKey.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_dataBaseName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit_paramType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_systemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnNew,
            this.btnEdit,
            this.btnSave,
            this.btnExit,
            this.btnStatus});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbon.MaxItemId = 6;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1334, 225);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnNew
            // 
            this.btnNew.Caption = "新增";
            this.btnNew.Id = 1;
            this.btnNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.Image")));
            this.btnNew.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnNew.ImageOptions.LargeImage")));
            this.btnNew.Name = "btnNew";
            this.btnNew.Tag = "LiProcedureConfigureForm.btnNew";
            this.btnNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNew_ItemClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Caption = "编辑";
            this.btnEdit.Id = 2;
            this.btnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.Image")));
            this.btnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEdit.ImageOptions.LargeImage")));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Tag = "LiProcedureConfigureForm.btnEdit";
            this.btnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEdit_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "保存";
            this.btnSave.Id = 3;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.LargeImage")));
            this.btnSave.Name = "btnSave";
            this.btnSave.Tag = "LiProcedureConfigureForm.btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出";
            this.btnExit.Id = 4;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.LargeImage")));
            this.btnExit.Name = "btnExit";
            this.btnExit.Tag = "LiProcedureConfigureForm.btnExit";
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExit_ItemClick);
            // 
            // btnStatus
            // 
            this.btnStatus.Caption = "状态";
            this.btnStatus.Id = 5;
            this.btnStatus.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnStatus.ImageOptions.Image")));
            this.btnStatus.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnStatus.ImageOptions.LargeImage")));
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Tag = "LiProcedureConfigureForm.btnStatus";
            this.btnStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStatus_ItemClick);
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
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNew);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnEdit);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnStatus);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnExit);
            this.ribbonPageGroup1.MergeOrder = 1;
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 774);
            this.ribbonStatusBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1334, 48);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridLookUpEdit_systemCode);
            this.layoutControl1.Controls.Add(this.textEdit_procedureName);
            this.layoutControl1.Controls.Add(this.textEdit_entityKey);
            this.layoutControl1.Controls.Add(this.gridLookUpEdit_dataBaseName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 225);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1334, 88);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // textEdit_procedureName
            // 
            this.textEdit_procedureName.Location = new System.Drawing.Point(729, 18);
            this.textEdit_procedureName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textEdit_procedureName.MenuManager = this.ribbon;
            this.textEdit_procedureName.Name = "textEdit_procedureName";
            this.textEdit_procedureName.Size = new System.Drawing.Size(183, 28);
            this.textEdit_procedureName.StyleController = this.layoutControl1;
            this.textEdit_procedureName.TabIndex = 6;
            this.textEdit_procedureName.Tag = "LiProcedureConfigureForm.procedureName";
            // 
            // textEdit_entityKey
            // 
            this.textEdit_entityKey.Location = new System.Drawing.Point(429, 18);
            this.textEdit_entityKey.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textEdit_entityKey.MenuManager = this.ribbon;
            this.textEdit_entityKey.Name = "textEdit_entityKey";
            this.textEdit_entityKey.Size = new System.Drawing.Size(183, 28);
            this.textEdit_entityKey.StyleController = this.layoutControl1;
            this.textEdit_entityKey.TabIndex = 5;
            this.textEdit_entityKey.Tag = "LiProcedureConfigureForm.entityKey";
            // 
            // gridLookUpEdit_dataBaseName
            // 
            this.gridLookUpEdit_dataBaseName.Location = new System.Drawing.Point(129, 18);
            this.gridLookUpEdit_dataBaseName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridLookUpEdit_dataBaseName.MenuManager = this.ribbon;
            this.gridLookUpEdit_dataBaseName.Name = "gridLookUpEdit_dataBaseName";
            this.gridLookUpEdit_dataBaseName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEdit_dataBaseName.Properties.View = this.gridLookUpEdit1View;
            this.gridLookUpEdit_dataBaseName.Size = new System.Drawing.Size(183, 28);
            this.gridLookUpEdit_dataBaseName.StyleController = this.layoutControl1;
            this.gridLookUpEdit_dataBaseName.TabIndex = 4;
            this.gridLookUpEdit_dataBaseName.Tag = "LiProcedureConfigureForm.dataBaseName";
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1310, 98);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 58);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(1280, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gridLookUpEdit_dataBaseName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(300, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(129, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(300, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Tag = "";
            this.layoutControlItem1.Text = "数据库名称";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(108, 22);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.textEdit_entityKey;
            this.layoutControlItem2.Location = new System.Drawing.Point(300, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(300, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(129, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(300, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Tag = "";
            this.layoutControlItem2.Text = "存储过程标识";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(108, 22);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.textEdit_procedureName;
            this.layoutControlItem3.Location = new System.Drawing.Point(600, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(300, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(129, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(680, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Tag = "";
            this.layoutControlItem3.Text = "存储过程名称";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(108, 22);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.gridControl1);
            this.panelControl1.Controls.Add(this.barDockControl7);
            this.panelControl1.Controls.Add(this.barDockControl8);
            this.panelControl1.Controls.Add(this.barDockControl6);
            this.panelControl1.Controls.Add(this.barDockControl5);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 313);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1334, 461);
            this.panelControl1.TabIndex = 3;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl1.Location = new System.Drawing.Point(3, 39);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemGridLookUpEdit_paramType});
            this.gridControl1.Size = new System.Drawing.Size(1328, 419);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.Tag = "LiProcedureConfigureForm.datas";
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.Tag = "LiProcedureConfigureForm.datas";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "参数名称";
            this.gridColumn1.FieldName = "paramName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Tag = "LiProcedureConfigureForm.paramName";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "参数类型";
            this.gridColumn2.ColumnEdit = this.repositoryItemGridLookUpEdit_paramType;
            this.gridColumn2.FieldName = "paramType";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Tag = "LiProcedureConfigureForm.paramType";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // repositoryItemGridLookUpEdit_paramType
            // 
            this.repositoryItemGridLookUpEdit_paramType.AutoHeight = false;
            this.repositoryItemGridLookUpEdit_paramType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit_paramType.Name = "repositoryItemGridLookUpEdit_paramType";
            this.repositoryItemGridLookUpEdit_paramType.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "参数长度";
            this.gridColumn3.FieldName = "paramLength";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Tag = "LiProcedureConfigureForm.paramLength";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // barDockControl7
            // 
            this.barDockControl7.CausesValidation = false;
            this.barDockControl7.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl7.Location = new System.Drawing.Point(3, 39);
            this.barDockControl7.Manager = this.barManager3;
            this.barDockControl7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControl7.Size = new System.Drawing.Size(0, 419);
            // 
            // barManager3
            // 
            this.barManager3.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar5});
            this.barManager3.DockControls.Add(this.barDockControl5);
            this.barManager3.DockControls.Add(this.barDockControl6);
            this.barManager3.DockControls.Add(this.barDockControl7);
            this.barManager3.DockControls.Add(this.barDockControl8);
            this.barManager3.Form = this.panelControl1;
            this.barManager3.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnAddRowWhere,
            this.btnInsertRowWhere,
            this.btnDeleteRow});
            this.barManager3.MainMenu = this.bar5;
            this.barManager3.MaxItemId = 3;
            // 
            // bar5
            // 
            this.bar5.BarName = "Main menu";
            this.bar5.DockCol = 0;
            this.bar5.DockRow = 0;
            this.bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar5.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAddRowWhere),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnInsertRowWhere),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDeleteRow)});
            this.bar5.OptionsBar.MultiLine = true;
            this.bar5.OptionsBar.UseWholeRow = true;
            this.bar5.Text = "Main menu";
            // 
            // btnAddRowWhere
            // 
            this.btnAddRowWhere.Caption = "增行";
            this.btnAddRowWhere.Id = 0;
            this.btnAddRowWhere.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRowWhere.ImageOptions.Image")));
            this.btnAddRowWhere.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAddRowWhere.ImageOptions.LargeImage")));
            this.btnAddRowWhere.Name = "btnAddRowWhere";
            this.btnAddRowWhere.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnAddRowWhere.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAddRowWhere_ItemClick);
            // 
            // btnInsertRowWhere
            // 
            this.btnInsertRowWhere.Caption = "插行";
            this.btnInsertRowWhere.Id = 1;
            this.btnInsertRowWhere.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertRowWhere.ImageOptions.Image")));
            this.btnInsertRowWhere.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnInsertRowWhere.ImageOptions.LargeImage")));
            this.btnInsertRowWhere.Name = "btnInsertRowWhere";
            this.btnInsertRowWhere.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnInsertRowWhere.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInsertRowWhere_ItemClick);
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Caption = "删行";
            this.btnDeleteRow.Id = 2;
            this.btnDeleteRow.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteRow.ImageOptions.Image")));
            this.btnDeleteRow.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDeleteRow.ImageOptions.LargeImage")));
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnDeleteRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteRow_ItemClick);
            // 
            // barDockControl5
            // 
            this.barDockControl5.CausesValidation = false;
            this.barDockControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl5.Location = new System.Drawing.Point(3, 3);
            this.barDockControl5.Manager = this.barManager3;
            this.barDockControl5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControl5.Size = new System.Drawing.Size(1328, 36);
            // 
            // barDockControl6
            // 
            this.barDockControl6.CausesValidation = false;
            this.barDockControl6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl6.Location = new System.Drawing.Point(3, 458);
            this.barDockControl6.Manager = this.barManager3;
            this.barDockControl6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControl6.Size = new System.Drawing.Size(1328, 0);
            // 
            // barDockControl8
            // 
            this.barDockControl8.CausesValidation = false;
            this.barDockControl8.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl8.Location = new System.Drawing.Point(1331, 39);
            this.barDockControl8.Manager = this.barManager3;
            this.barDockControl8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControl8.Size = new System.Drawing.Size(0, 419);
            // 
            // gridLookUpEdit_systemCode
            // 
            this.gridLookUpEdit_systemCode.Location = new System.Drawing.Point(129, 42);
            this.gridLookUpEdit_systemCode.MenuManager = this.ribbon;
            this.gridLookUpEdit_systemCode.Name = "gridLookUpEdit_systemCode";
            this.gridLookUpEdit_systemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gridLookUpEdit_systemCode.Properties.View = this.gridView2;
            this.gridLookUpEdit_systemCode.Size = new System.Drawing.Size(183, 28);
            this.gridLookUpEdit_systemCode.StyleController = this.layoutControl1;
            this.gridLookUpEdit_systemCode.TabIndex = 7;
            this.gridLookUpEdit_systemCode.Tag = "LiProcedureConfigureForm.systemCode";
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.gridLookUpEdit_systemCode;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(300, 34);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(167, 34);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1280, 34);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "系统代码";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(108, 22);
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // LiProcedureConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 822);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LiProcedureConfigureForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "配置存储过程信息";
            this.Load += new System.EventHandler(this.LiProcedureConfigureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_procedureName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit_entityKey.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_dataBaseName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit_paramType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit_systemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem btnNew;
        private DevExpress.XtraBars.BarButtonItem btnEdit;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnExit;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraBars.BarDockControl barDockControl7;
        private DevExpress.XtraBars.BarManager barManager3;
        private DevExpress.XtraBars.Bar bar5;
        private DevExpress.XtraBars.BarButtonItem btnAddRowWhere;
        private DevExpress.XtraBars.BarButtonItem btnInsertRowWhere;
        private DevExpress.XtraBars.BarButtonItem btnDeleteRow;
        private DevExpress.XtraBars.BarDockControl barDockControl5;
        private DevExpress.XtraBars.BarDockControl barDockControl6;
        private DevExpress.XtraBars.BarDockControl barDockControl8;
        private DevExpress.XtraBars.BarButtonItem btnStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit_paramType;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.TextEdit textEdit_procedureName;
        private DevExpress.XtraEditors.TextEdit textEdit_entityKey;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit_dataBaseName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.GridLookUpEdit gridLookUpEdit_systemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}