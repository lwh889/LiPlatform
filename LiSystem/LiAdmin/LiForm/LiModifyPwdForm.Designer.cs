namespace LiForm
{
    partial class LiModifyPwdForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiModifyPwdForm));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.btnSave = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnExit = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnAddStatus = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnDeleteStatus = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnRefreshStatus = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnAddRow = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteRow = new DevExpress.XtraBars.BarButtonItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txt_UserCode = new DevExpress.XtraEditors.TextEdit();
            this.txt_PwdConfirm = new DevExpress.XtraEditors.TextEdit();
            this.txt_PwdNew = new DevExpress.XtraEditors.TextEdit();
            this.txt_PwdOri = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PwdConfirm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PwdNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PwdOri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnSave,
            this.btnExit,
            this.btnAddStatus,
            this.btnDeleteStatus,
            this.btnRefreshStatus,
            this.btnAddRow,
            this.btnDeleteRow});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 9;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSave, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnExit)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // btnSave
            // 
            this.btnSave.Caption = "确认";
            this.btnSave.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.btnSave.Id = 0;
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnExit
            // 
            this.btnExit.Caption = "退出";
            this.btnExit.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right;
            this.btnExit.Id = 1;
            this.btnExit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.ImageOptions.Image")));
            this.btnExit.Name = "btnExit";
            this.btnExit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btnExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExit_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControlTop.Size = new System.Drawing.Size(337, 36);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 268);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControlBottom.Size = new System.Drawing.Size(337, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 36);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 232);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(337, 36);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 232);
            // 
            // btnAddStatus
            // 
            this.btnAddStatus.Caption = "新增状态";
            this.btnAddStatus.Id = 2;
            this.btnAddStatus.Name = "btnAddStatus";
            // 
            // btnDeleteStatus
            // 
            this.btnDeleteStatus.Caption = "删除状态";
            this.btnDeleteStatus.Id = 3;
            this.btnDeleteStatus.Name = "btnDeleteStatus";
            // 
            // btnRefreshStatus
            // 
            this.btnRefreshStatus.Caption = "刷新状态";
            this.btnRefreshStatus.Id = 4;
            this.btnRefreshStatus.Name = "btnRefreshStatus";
            // 
            // btnAddRow
            // 
            this.btnAddRow.Id = 7;
            this.btnAddRow.Name = "btnAddRow";
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Id = 8;
            this.btnDeleteRow.Name = "btnDeleteRow";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txt_UserCode);
            this.layoutControl1.Controls.Add(this.txt_PwdConfirm);
            this.layoutControl1.Controls.Add(this.txt_PwdNew);
            this.layoutControl1.Controls.Add(this.txt_PwdOri);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 36);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(337, 232);
            this.layoutControl1.TabIndex = 23;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txt_UserCode
            // 
            this.txt_UserCode.Location = new System.Drawing.Point(95, 18);
            this.txt_UserCode.MenuManager = this.barManager1;
            this.txt_UserCode.Name = "txt_UserCode";
            this.txt_UserCode.Properties.ReadOnly = true;
            this.txt_UserCode.Size = new System.Drawing.Size(224, 28);
            this.txt_UserCode.StyleController = this.layoutControl1;
            this.txt_UserCode.TabIndex = 7;
            // 
            // txt_PwdConfirm
            // 
            this.txt_PwdConfirm.Location = new System.Drawing.Point(95, 120);
            this.txt_PwdConfirm.MenuManager = this.barManager1;
            this.txt_PwdConfirm.Name = "txt_PwdConfirm";
            this.txt_PwdConfirm.Properties.PasswordChar = '*';
            this.txt_PwdConfirm.Size = new System.Drawing.Size(224, 28);
            this.txt_PwdConfirm.StyleController = this.layoutControl1;
            this.txt_PwdConfirm.TabIndex = 6;
            // 
            // txt_PwdNew
            // 
            this.txt_PwdNew.Location = new System.Drawing.Point(95, 86);
            this.txt_PwdNew.MenuManager = this.barManager1;
            this.txt_PwdNew.Name = "txt_PwdNew";
            this.txt_PwdNew.Properties.PasswordChar = '*';
            this.txt_PwdNew.Size = new System.Drawing.Size(224, 28);
            this.txt_PwdNew.StyleController = this.layoutControl1;
            this.txt_PwdNew.TabIndex = 5;
            // 
            // txt_PwdOri
            // 
            this.txt_PwdOri.Location = new System.Drawing.Point(95, 52);
            this.txt_PwdOri.MenuManager = this.barManager1;
            this.txt_PwdOri.Name = "txt_PwdOri";
            this.txt_PwdOri.Properties.PasswordChar = '*';
            this.txt_PwdOri.Size = new System.Drawing.Size(224, 28);
            this.txt_PwdOri.StyleController = this.layoutControl1;
            this.txt_PwdOri.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup1.Size = new System.Drawing.Size(337, 232);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txt_PwdOri;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(307, 34);
            this.layoutControlItem1.Text = "旧密码";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(72, 22);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 136);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(307, 66);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txt_PwdNew;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(307, 34);
            this.layoutControlItem2.Text = "新密码";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txt_PwdConfirm;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 102);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(307, 34);
            this.layoutControlItem3.Text = "确认密码";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 22);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txt_UserCode;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(307, 34);
            this.layoutControlItem4.Text = "用户编码";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(72, 22);
            // 
            // LiModifyPwdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 268);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "LiModifyPwdForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.LiVoucherCodeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txt_UserCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PwdConfirm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PwdNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_PwdOri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarLargeButtonItem btnSave;
        private DevExpress.XtraBars.BarLargeButtonItem btnExit;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem btnAddStatus;
        private DevExpress.XtraBars.BarLargeButtonItem btnDeleteStatus;
        private DevExpress.XtraBars.BarLargeButtonItem btnRefreshStatus;
        private DevExpress.XtraBars.BarButtonItem btnAddRow;
        private DevExpress.XtraBars.BarButtonItem btnDeleteRow;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txt_PwdConfirm;
        private DevExpress.XtraEditors.TextEdit txt_PwdNew;
        private DevExpress.XtraEditors.TextEdit txt_PwdOri;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit txt_UserCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}