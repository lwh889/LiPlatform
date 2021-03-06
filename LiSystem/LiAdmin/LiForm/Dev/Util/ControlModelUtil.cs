﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

using LiModel.Form;
using LiModel.LiEnum;
using LiCommon.LiEnum;
using LiContexts;

namespace LiForm.Dev.Util
{
    public class ControlModelUtil
    {

        /// <summary>
        /// 获取控件
        /// </summary>
        /// <param name="controlModel"></param>
        /// <returns></returns>
        public static Control getControl(ControlModel controlModel)
        {
            Control control = null;
            switch (controlModel.controltype)
            {
                case ControlType.VoucherCodeEdit:
                    TextEdit voucherCodeEdit = new TextEdit();
                    voucherCodeEdit.Properties.ReadOnly = true;
                    voucherCodeEdit.Properties.NullValuePromptShowForEmptyValue = true;
                    voucherCodeEdit.Properties.NullText = "保存自动生成...";
                    voucherCodeEdit.Text = "保存自动生成...";
                    control = voucherCodeEdit;
                    break;
                case ControlType.TextEdit:
                    TextEdit textEdit = new TextEdit();
                    textEdit.Properties.ReadOnly = controlModel.bReadOnly;
                    control = textEdit;
                    break;
                case ControlType.CheckEdit:
                    CheckEdit checkEdit = new CheckEdit();
                    checkEdit.Properties.ReadOnly = controlModel.bReadOnly;
                    control = checkEdit;
                    break;
                case ControlType.MemoEdit:
                    MemoEdit memoEdit = new MemoEdit();
                    memoEdit.Properties.ReadOnly = controlModel.bReadOnly;
                    control = memoEdit;
                    break;
                case ControlType.IntEdit:
                case ControlType.DecimalEdit:
                case ControlType.CalcEdit:
                    CalcEdit calcEdit = new CalcEdit();
                    calcEdit.Properties.ReadOnly = controlModel.bReadOnly;
                    control = calcEdit;
                    break;
                case ControlType.DateTimeEdit:
                case ControlType.TimeEdit:
                case ControlType.DateEdit:
                    DateEdit dateEdit = new DateEdit();
                    dateEdit.Properties.ReadOnly = controlModel.bReadOnly;
                    control = dateEdit;
                    break;
                case ControlType.StatusEdit:
                case ControlType.GridLookUpEditComboBox:
                    GridLookUpEdit gridLookUpEditComboBox = new GridLookUpEdit();
                    gridLookUpEditComboBox.Properties.ReadOnly = controlModel.bReadOnly;
                    control = gridLookUpEditComboBox;
                    break;
                case ControlType.UserEdit:
                case ControlType.GridLookUpEditRef:
                    GridLookUpEdit gridLookUpEditRef = new GridLookUpEdit();
                    gridLookUpEditRef.Properties.ReadOnly = controlModel.bReadOnly;
                    control = gridLookUpEditRef;
                    break;
                case ControlType.GridLookUpEditRefAssist:
                    TextEdit textRefAssistEdit = new TextEdit();
                    textRefAssistEdit.Properties.ReadOnly = true;
                    control = textRefAssistEdit;
                    break;
                case ControlType.TreeListLookUpEdit:
                    TreeListLookUpEdit treeListLookUpEdit = new TreeListLookUpEdit();
                    treeListLookUpEdit.Properties.BeginInit();
                    treeListLookUpEdit.Properties.NullText = "";
                    treeListLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                    new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
                    treeListLookUpEdit.Properties.ValueMember = "ID";
                    treeListLookUpEdit.Properties.DisplayMember = "Code";

                    TreeList treeListLookUpEdit1TreeList = new TreeList();
                    treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
                    treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;

                    treeListLookUpEdit1TreeList.Columns.Add(new TreeListColumn() { Caption = "名称", FieldName = "Name", Name = "treeListLookUpEdit1TreeList1", Visible = true, VisibleIndex = 0 });


                    treeListLookUpEdit.Properties.TreeList = treeListLookUpEdit1TreeList;

                    //treeListLookUpEdit.Properties.TreeList.KeyFieldName = "ID";
                    //treeListLookUpEdit.Properties.TreeList.ParentFieldName = "ParentID";

                    treeListLookUpEdit.Properties.EndInit();
                    control = treeListLookUpEdit;
                    break;
            }
            return control;
        }



        /// <summary>
        /// 获取控件
        /// </summary>
        /// <param name="controlModel"></param>
        /// <returns></returns>
        public static RepositoryItem getRepositoryItemControl(ControlModel controlModel)
        {
            RepositoryItem control = null;
            switch (controlModel.controltype)
            {
                case ControlType.TextEdit:
                    RepositoryItemTextEdit textEdit = new RepositoryItemTextEdit();
                    textEdit.ReadOnly = controlModel.bReadOnly;
                    control = textEdit;
                    break;
                case ControlType.CheckEdit:
                    RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit();
                    checkEdit.ReadOnly = controlModel.bReadOnly;
                    control = checkEdit;
                    break;
                case ControlType.MemoEdit:
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    memoEdit.ReadOnly = controlModel.bReadOnly;
                    control = memoEdit;
                    break;
                case ControlType.DecimalEdit:
                case ControlType.IntEdit:
                case ControlType.CalcEdit:
                    RepositoryItemCalcEdit calcEdit = new RepositoryItemCalcEdit();
                    calcEdit.ReadOnly = controlModel.bReadOnly;
                    control = calcEdit;
                    break;
                case ControlType.DateTimeEdit:
                case ControlType.TimeEdit:
                case ControlType.DateEdit:
                    RepositoryItemDateEdit dateEdit = new RepositoryItemDateEdit();
                    dateEdit.ReadOnly = controlModel.bReadOnly;
                    control = dateEdit;
                    break;
                case ControlType.StatusEdit:
                case ControlType.GridLookUpEditComboBox:
                    RepositoryItemGridLookUpEdit gridLookUpEditComboBox = new RepositoryItemGridLookUpEdit();
                    gridLookUpEditComboBox.ReadOnly = controlModel.bReadOnly;
                    control = gridLookUpEditComboBox;
                    break;
                case ControlType.UserEdit:
                case ControlType.GridLookUpEditRef:
                    RepositoryItemGridLookUpEdit gridLookUpEditRef = new RepositoryItemGridLookUpEdit();
                    gridLookUpEditRef.ReadOnly = controlModel.bReadOnly;
                    control = gridLookUpEditRef;
                    break;
                case ControlType.GridLookUpEditRefAssist:
                    RepositoryItemTextEdit textRefAssistEdit = new RepositoryItemTextEdit();
                    textRefAssistEdit.ReadOnly = controlModel.bReadOnly;
                    control = textRefAssistEdit;
                    break;
                case ControlType.TreeListLookUpEdit:
                    RepositoryItemTreeListLookUpEdit treeListLookUpEdit = new RepositoryItemTreeListLookUpEdit();
                    treeListLookUpEdit.ReadOnly = controlModel.bReadOnly;
                    treeListLookUpEdit.NullText = "";
                    treeListLookUpEdit.ValueMember = "ID";
                    treeListLookUpEdit.DisplayMember = controlModel.basicInfoShowFieldName;

                    treeListLookUpEdit.TreeList.KeyFieldName = "ID";
                    treeListLookUpEdit.TreeList.ParentFieldName = "ParentID";

                    TreeList treeListLookUpEdit1TreeList = new TreeList();
                    treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
                    treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
                    treeListLookUpEdit1TreeList.TabIndex = 0;

                    treeListLookUpEdit1TreeList.Columns.Add(new TreeListColumn() { Caption = "名称", FieldName = "Name", Name = "treeListLookUpEdit1TreeList1", Visible = true, VisibleIndex = 0 });


                    treeListLookUpEdit.TreeList = treeListLookUpEdit1TreeList;

                    control = treeListLookUpEdit;
                    break;
            }
            return control;
        }

        #region 列表控件


        /// <summary>
        /// 获取控件
        /// </summary>
        /// <param name="controlModel"></param>
        /// <returns></returns>
        public static Control getControl(string controlType)
        {
            Control control = null;
            switch (controlType)
            {
                case ControlType.VoucherCodeEdit:
                    TextEdit voucherCodeEdit = new TextEdit();
                    voucherCodeEdit.Properties.ReadOnly = true;
                    voucherCodeEdit.Properties.NullValuePromptShowForEmptyValue = true;
                    voucherCodeEdit.Properties.NullText = "保存自动生成...";
                    voucherCodeEdit.Text = "保存自动生成...";
                    control = voucherCodeEdit;
                    break;
                case ControlType.TextEdit:
                    TextEdit textEdit = new TextEdit();
                    control = textEdit;
                    break;
                case ControlType.CheckEdit:
                    CheckEdit checkEdit = new CheckEdit();
                    control = checkEdit;
                    break;
                case ControlType.MemoEdit:
                    MemoEdit memoEdit = new MemoEdit();
                    control = memoEdit;
                    break;
                case ControlType.IntEdit:
                case ControlType.DecimalEdit:
                case ControlType.CalcEdit:
                    CalcEdit calcEdit = new CalcEdit();
                    control = calcEdit;
                    break;
                case ControlType.DateTimeEdit:
                case ControlType.TimeEdit:
                case ControlType.DateEdit:
                    DateEdit dateEdit = new DateEdit();
                    control = dateEdit;
                    break;
                case ControlType.StatusEdit:
                case ControlType.GridLookUpEditComboBox:
                    GridLookUpEdit gridLookUpEditComboBox = new GridLookUpEdit();
                    control = gridLookUpEditComboBox;
                    break;
                case ControlType.UserEdit:
                case ControlType.GridLookUpEditRef:
                    GridLookUpEdit gridLookUpEditRef = new GridLookUpEdit();
                    control = gridLookUpEditRef;
                    break;
                case ControlType.GridLookUpEditRefAssist:
                    TextEdit textRefAssistEdit = new TextEdit();
                    control = textRefAssistEdit;
                    break;
                case ControlType.TreeListLookUpEdit:
                    TreeListLookUpEdit treeListLookUpEdit = new TreeListLookUpEdit();

                    treeListLookUpEdit.Properties.ValueMember = "ID";
                    treeListLookUpEdit.Properties.DisplayMember = "Name";

                    treeListLookUpEdit.Properties.TreeList.KeyFieldName = "ID";
                    treeListLookUpEdit.Properties.TreeList.ParentFieldName = "ParentID";

                    TreeList treeListLookUpEdit1TreeList = new TreeList();
                    treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
                    treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
                    treeListLookUpEdit1TreeList.TabIndex = 0;

                    treeListLookUpEdit1TreeList.Columns.Add(new TreeListColumn() { Caption = "名称", FieldName = "Name", Name = "treeListLookUpEdit1TreeList1", Visible = true, VisibleIndex = 0 });


                    treeListLookUpEdit.Properties.TreeList = treeListLookUpEdit1TreeList;

                    control = treeListLookUpEdit;
                    break;
            }
            return control;
        }



        /// <summary>
        /// 获取控件
        /// </summary>
        /// <param name="controlModel"></param>
        /// <returns></returns>
        public static RepositoryItem getRepositoryItemControl(string controlType)
        {
            RepositoryItem control = null;
            switch (controlType)
            {
                case ControlType.TextEdit:
                    RepositoryItemTextEdit textEdit = new RepositoryItemTextEdit();
                    control = textEdit;
                    break;
                case ControlType.CheckEdit:
                    RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit();
                    control = checkEdit;
                    break;
                case ControlType.MemoEdit:
                    RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                    control = memoEdit;
                    break;
                case ControlType.DecimalEdit:
                case ControlType.IntEdit:
                case ControlType.CalcEdit:
                    RepositoryItemCalcEdit calcEdit = new RepositoryItemCalcEdit();
                    control = calcEdit;
                    break;
                case ControlType.DateTimeEdit:
                case ControlType.TimeEdit:
                case ControlType.DateEdit:
                    RepositoryItemDateEdit dateEdit = new RepositoryItemDateEdit();
                    control = dateEdit;
                    break;
                case ControlType.StatusEdit:
                case ControlType.GridLookUpEditComboBox:
                    RepositoryItemGridLookUpEdit gridLookUpEditComboBox = new RepositoryItemGridLookUpEdit();
                    control = gridLookUpEditComboBox;
                    break;
                case ControlType.UserEdit:
                case ControlType.GridLookUpEditRef:
                    RepositoryItemGridLookUpEdit gridLookUpEditRef = new RepositoryItemGridLookUpEdit();
                    control = gridLookUpEditRef;
                    break;
                case ControlType.GridLookUpEditRefAssist:
                    RepositoryItemTextEdit textRefAssistEdit = new RepositoryItemTextEdit();
                    control = textRefAssistEdit;
                    break;
            }
            return control;
        }

        #endregion
    }
}
