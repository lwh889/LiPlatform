﻿namespace LiControl.Dev
{
    partial class PagerControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PagerControl));
            this.lcStatus = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButtonToPage = new DevExpress.XtraEditors.SimpleButton();
            this.textEditToPage = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonExportCurPage = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonExportAllPage = new DevExpress.XtraEditors.SimpleButton();
            this.textEditAllPageCount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEditPageSize = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButtonEnd = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonNext = new DevExpress.XtraEditors.SimpleButton();
            this.textEditCurPage = new DevExpress.XtraEditors.TextEdit();
            this.simpleButtonPre = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonFirst = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditToPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAllPageCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCurPage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lcStatus
            // 
            this.lcStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lcStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcStatus.Location = new System.Drawing.Point(577, 2);
            this.lcStatus.Margin = new System.Windows.Forms.Padding(4);
            this.lcStatus.Name = "lcStatus";
            this.lcStatus.Padding = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.lcStatus.Size = new System.Drawing.Size(227, 43);
            this.lcStatus.TabIndex = 12;
            this.lcStatus.Text = "(共XXX条记录，每页XX条，共XX页)";
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.lcStatus);
            this.panelControl1.Controls.Add(this.simpleButtonToPage);
            this.panelControl1.Controls.Add(this.textEditToPage);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.simpleButtonExportCurPage);
            this.panelControl1.Controls.Add(this.simpleButtonExportAllPage);
            this.panelControl1.Controls.Add(this.textEditAllPageCount);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.comboBoxEditPageSize);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.simpleButtonEnd);
            this.panelControl1.Controls.Add(this.simpleButtonNext);
            this.panelControl1.Controls.Add(this.textEditCurPage);
            this.panelControl1.Controls.Add(this.simpleButtonPre);
            this.panelControl1.Controls.Add(this.simpleButtonFirst);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1048, 47);
            this.panelControl1.TabIndex = 30;
            // 
            // simpleButtonToPage
            // 
            this.simpleButtonToPage.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButtonToPage.Location = new System.Drawing.Point(534, 2);
            this.simpleButtonToPage.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButtonToPage.Name = "simpleButtonToPage";
            this.simpleButtonToPage.Size = new System.Drawing.Size(43, 43);
            this.simpleButtonToPage.TabIndex = 13;
            this.simpleButtonToPage.Text = "跳转";
            // 
            // textEditToPage
            // 
            this.textEditToPage.Dock = System.Windows.Forms.DockStyle.Left;
            this.textEditToPage.EditValue = "1";
            this.textEditToPage.Location = new System.Drawing.Point(494, 2);
            this.textEditToPage.Margin = new System.Windows.Forms.Padding(4);
            this.textEditToPage.Name = "textEditToPage";
            this.textEditToPage.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditToPage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEditToPage.Properties.AutoHeight = false;
            this.textEditToPage.Size = new System.Drawing.Size(40, 43);
            this.textEditToPage.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl2.Location = new System.Drawing.Point(441, 2);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 43);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "当前页：";
            // 
            // simpleButtonExportCurPage
            // 
            this.simpleButtonExportCurPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButtonExportCurPage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonExportCurPage.ImageOptions.Image")));
            this.simpleButtonExportCurPage.Location = new System.Drawing.Point(804, 2);
            this.simpleButtonExportCurPage.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButtonExportCurPage.Name = "simpleButtonExportCurPage";
            this.simpleButtonExportCurPage.Size = new System.Drawing.Size(123, 43);
            this.simpleButtonExportCurPage.TabIndex = 2;
            this.simpleButtonExportCurPage.Text = "导出当前页";
            this.simpleButtonExportCurPage.Visible = false;
            // 
            // simpleButtonExportAllPage
            // 
            this.simpleButtonExportAllPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.simpleButtonExportAllPage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonExportAllPage.ImageOptions.Image")));
            this.simpleButtonExportAllPage.Location = new System.Drawing.Point(927, 2);
            this.simpleButtonExportAllPage.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButtonExportAllPage.Name = "simpleButtonExportAllPage";
            this.simpleButtonExportAllPage.Size = new System.Drawing.Size(119, 43);
            this.simpleButtonExportAllPage.TabIndex = 2;
            this.simpleButtonExportAllPage.Text = "导出全部页";
            this.simpleButtonExportAllPage.Visible = false;
            // 
            // textEditAllPageCount
            // 
            this.textEditAllPageCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.textEditAllPageCount.Location = new System.Drawing.Point(402, 2);
            this.textEditAllPageCount.Margin = new System.Windows.Forms.Padding(4);
            this.textEditAllPageCount.Name = "textEditAllPageCount";
            this.textEditAllPageCount.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.textEditAllPageCount.Properties.Appearance.Options.UseForeColor = true;
            this.textEditAllPageCount.Properties.AutoHeight = false;
            this.textEditAllPageCount.Properties.ReadOnly = true;
            this.textEditAllPageCount.Size = new System.Drawing.Size(39, 43);
            this.textEditAllPageCount.TabIndex = 14;
            // 
            // labelControl4
            // 
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl4.Location = new System.Drawing.Point(346, 2);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(56, 43);
            this.labelControl4.TabIndex = 15;
            this.labelControl4.Text = "总页数：";
            // 
            // comboBoxEditPageSize
            // 
            this.comboBoxEditPageSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.comboBoxEditPageSize.EditValue = "1000";
            this.comboBoxEditPageSize.Location = new System.Drawing.Point(281, 2);
            this.comboBoxEditPageSize.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxEditPageSize.Name = "comboBoxEditPageSize";
            this.comboBoxEditPageSize.Properties.Appearance.Options.UseTextOptions = true;
            this.comboBoxEditPageSize.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.comboBoxEditPageSize.Properties.AutoHeight = false;
            this.comboBoxEditPageSize.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditPageSize.Properties.DisplayFormat.FormatString = "d";
            this.comboBoxEditPageSize.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.comboBoxEditPageSize.Properties.EditFormat.FormatString = "d";
            this.comboBoxEditPageSize.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.comboBoxEditPageSize.Properties.EditValueChangedDelay = 1;
            this.comboBoxEditPageSize.Properties.Items.AddRange(new object[] {
            "100",
            "200",
            "1000",
            "5000",
            "10000"});
            this.comboBoxEditPageSize.Size = new System.Drawing.Size(65, 43);
            this.comboBoxEditPageSize.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(201, 2);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(80, 43);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = " 分页大小：";
            // 
            // simpleButtonEnd
            // 
            this.simpleButtonEnd.Appearance.Options.UseTextOptions = true;
            this.simpleButtonEnd.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleButtonEnd.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButtonEnd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonEnd.ImageOptions.Image")));
            this.simpleButtonEnd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonEnd.Location = new System.Drawing.Point(162, 2);
            this.simpleButtonEnd.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButtonEnd.Name = "simpleButtonEnd";
            this.simpleButtonEnd.Size = new System.Drawing.Size(39, 43);
            this.simpleButtonEnd.TabIndex = 0;
            // 
            // simpleButtonNext
            // 
            this.simpleButtonNext.Appearance.Options.UseTextOptions = true;
            this.simpleButtonNext.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleButtonNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButtonNext.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonNext.ImageOptions.Image")));
            this.simpleButtonNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonNext.Location = new System.Drawing.Point(123, 2);
            this.simpleButtonNext.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButtonNext.Name = "simpleButtonNext";
            this.simpleButtonNext.Size = new System.Drawing.Size(39, 43);
            this.simpleButtonNext.TabIndex = 0;
            // 
            // textEditCurPage
            // 
            this.textEditCurPage.Dock = System.Windows.Forms.DockStyle.Left;
            this.textEditCurPage.EditValue = "1";
            this.textEditCurPage.Location = new System.Drawing.Point(80, 2);
            this.textEditCurPage.Margin = new System.Windows.Forms.Padding(4);
            this.textEditCurPage.Name = "textEditCurPage";
            this.textEditCurPage.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditCurPage.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.textEditCurPage.Properties.AutoHeight = false;
            this.textEditCurPage.Properties.ReadOnly = true;
            this.textEditCurPage.Size = new System.Drawing.Size(43, 43);
            this.textEditCurPage.TabIndex = 4;
            // 
            // simpleButtonPre
            // 
            this.simpleButtonPre.Appearance.Options.UseTextOptions = true;
            this.simpleButtonPre.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleButtonPre.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButtonPre.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonPre.ImageOptions.Image")));
            this.simpleButtonPre.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonPre.Location = new System.Drawing.Point(41, 2);
            this.simpleButtonPre.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButtonPre.Name = "simpleButtonPre";
            this.simpleButtonPre.Size = new System.Drawing.Size(39, 43);
            this.simpleButtonPre.TabIndex = 0;
            // 
            // simpleButtonFirst
            // 
            this.simpleButtonFirst.Appearance.Options.UseTextOptions = true;
            this.simpleButtonFirst.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.simpleButtonFirst.Dock = System.Windows.Forms.DockStyle.Left;
            this.simpleButtonFirst.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButtonFirst.ImageOptions.Image")));
            this.simpleButtonFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButtonFirst.Location = new System.Drawing.Point(2, 2);
            this.simpleButtonFirst.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButtonFirst.Name = "simpleButtonFirst";
            this.simpleButtonFirst.Size = new System.Drawing.Size(39, 43);
            this.simpleButtonFirst.TabIndex = 0;
            // 
            // PagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "PagerControl";
            this.Size = new System.Drawing.Size(1048, 47);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditToPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditAllPageCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditPageSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditCurPage.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lcStatus;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonToPage;
        private DevExpress.XtraEditors.TextEdit textEditToPage;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExportCurPage;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExportAllPage;
        private DevExpress.XtraEditors.TextEdit textEditAllPageCount;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditPageSize;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonEnd;
        private DevExpress.XtraEditors.SimpleButton simpleButtonNext;
        private DevExpress.XtraEditors.TextEdit textEditCurPage;
        private DevExpress.XtraEditors.SimpleButton simpleButtonPre;
        private DevExpress.XtraEditors.SimpleButton simpleButtonFirst;
    }
}
