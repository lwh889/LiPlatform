﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using LiCommon.Util;
using LiModel.Form;
using LiModel.LiEnum;
using LiHttp.Enum;
using LiContexts;
using LiForm.Dev.Util;
using LiModel.Util;
using LiControl.Util;
using LiCommon.LiEnum;

namespace LiForm.Dev
{
    public partial class LiQueryForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 列表窗口
        /// </summary>
        public LiListForm liListForm;

        /// <summary>
        /// 当前查询方案
        /// </summary>
        public QuerySchemeModel currentQuerySchemeModel;

        /// <summary>
        /// 所有查询方案
        /// </summary>
        private List<QuerySchemeModel> _querySchemeModels;

        /// <summary>
        /// 所有查询方案
        /// </summary>
        public List<QuerySchemeModel> querySchemeModels { set { _querySchemeModels = value; }get { return _querySchemeModels; } }

        private List<QueryModel> _returnQuerys;

        /// <summary>
        /// 返回当前查询条件
        /// </summary>
        public List<QueryModel> returnQuerys { set { _returnQuerys = value; } get { return _returnQuerys; } }

        /// <summary>
        /// 查询方案按钮
        /// </summary>
        private Dictionary<string, SimpleButton> querySchemeBtns = new Dictionary<string, SimpleButton>();

        #region 引用数据源
        private FieldModel fieldModel = new FieldModel();
        private BracketsAfterModel bracketsAfterModel = new BracketsAfterModel();
        private BracketsBeforeModel bracketsBeforeModel = new BracketsBeforeModel();
        private JudgmentSymbolModel judgmentSymbolModel = new JudgmentSymbolModel();
        private ControlTypeModel controlTypeModel = new ControlTypeModel();
        private JoinModel joinModel = new JoinModel();
        #endregion

        private string entityKey;

        public LiQueryForm(LiListForm liListForm)
        {
            InitializeComponent();

            this.entityKey = liListForm.entityKey;
            this.querySchemeModels = liListForm.querySchemeModels;
            this.liListForm = liListForm;

            Init();
        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {

            List<QueryModel> sList = new List<QueryModel>();
            gridControl1.DataSource = sList;

        }

        public void InitControl()
        {
            gridControl2.DataSource = EntityModel.getDataSource(entityKey);

            gridControl3.DataSource = FieldModel.getDataSource(entityKey);

            InitGridlookUpEdit();
            InitQueryScheme();
        }

        /// <summary>
        /// 初始化控件数据源
        /// </summary>
        public void InitGridlookUpEdit()
        {

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(bracketsAfterModel.getValueMember(), bracketsAfterModel.getDisplayMember(), bracketsAfterModel.getSearchColumns(), bracketsAfterModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sBracketsAfter, this, bracketsAfterModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(bracketsBeforeModel.getValueMember(), bracketsBeforeModel.getDisplayMember(), bracketsBeforeModel.getSearchColumns(), bracketsBeforeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sBracketsBefore, this, bracketsBeforeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(judgmentSymbolModel.getValueMember(), judgmentSymbolModel.getDisplayMember(), judgmentSymbolModel.getSearchColumns(), judgmentSymbolModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sJudgmentSymbol, this, judgmentSymbolModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(judgmentSymbolModel.getValueMember(), judgmentSymbolModel.getDisplayMember(), judgmentSymbolModel.getSearchColumns(), judgmentSymbolModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sJudgeSymbol, this, judgmentSymbolModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(controlTypeModel.getValueMember(), controlTypeModel.getDisplayMember(), controlTypeModel.getSearchColumns(), controlTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_controlType, this, controlTypeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(joinModel.getValueMember(), joinModel.getDisplayMember(), joinModel.getSearchColumns(), joinModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sJoin, this, joinModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(fieldModel.getValueMember(), fieldModel.getDisplayMember(), fieldModel.getSearchColumns(), fieldModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sFieldName, this, FieldModel.getDataSource(entityKey));

        }

        /// <summary>
        /// 初始化查询方案
        /// </summary>
        public void InitQueryScheme()
        {
            FormUtil.loadQueryScheme(querySchemeModels, querySchemeBtns, new System.EventHandler(this.btnQueryScheme_Click), layoutControlGroup1);

            currentQuerySchemeModel = querySchemeModels[0];
        }

        /// <summary>
        /// 增加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRow_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<QueryModel> sList = gridControl1.DataSource as List<QueryModel>;
            QueryModel model = new QueryModel();
            model.id = sList.Count > 0 ? ((int)sList.Max(m => m.id)) + 1 : 0;
            //model.sFieldName = sValueMember;
            //model.sFieldType = dictFeildType[model.sFieldName];

            model.sJudgmentSymbol = JudgmentSymbol.Like;

            sList.Add(model);
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();

        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertRow_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            List<QueryModel> sList = gridControl1.DataSource as List<QueryModel>;

            QueryModel model = new QueryModel();
            model.id = sList.Count > 0 ? ((int)sList.Max(m => m.id)) + 1 : 0;
            //model.sFieldName = sValueMember;
            model.sJudgmentSymbol = JudgmentSymbol.Like;

            sList.Insert(gridView1.GetSelectedRows()[0], model);

            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteRow_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<QueryModel> sList = gridControl1.DataSource as List<QueryModel>;
            QueryModel model = gridView1.GetFocusedRow() as QueryModel;
            DataUtil.deleteInList(model, sList);

            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        /// <summary>
        /// 另保存行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LiInputDialog inputDialog = new LiInputDialog();
            if (inputDialog.ShowDialog() == DialogResult.Yes)
            {

                QuerySchemeModel querySchemeModel = new QuerySchemeModel();
                querySchemeModel.entityKey = entityKey;
                querySchemeModel.userCode = LiContexts.LiContext.userInfo.userCode;
                querySchemeModel.querySchemeName = inputDialog.getValue();
                querySchemeModel.entitys = new List<EntityModel>();
                querySchemeModel.fields = new List<FieldModel>();
                querySchemeModel.querys = new List<QueryModel>();

                List<QueryModel> querys = gridControl1.DataSource as List<QueryModel>;
                querySchemeModel.querys = ModelUtil.copyEntitys<QueryModel>(querys);

                List<EntityModel> entitys = gridControl2.DataSource as List<EntityModel>;
                querySchemeModel.entitys = ModelUtil.copyEntitys<EntityModel>(entitys);

                List<FieldModel> fields = gridControl3.DataSource as List<FieldModel>;
                querySchemeModel.fields = ModelUtil.copyEntitys<FieldModel>(fields);

                LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).newEntity(querySchemeModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).tipStr, "温馨提示");

                querySchemeModels = liListForm.loadQuerySchemeModels();
                FormUtil.loadQueryScheme(querySchemeModels, querySchemeBtns, new System.EventHandler(this.btnQueryScheme_Click), layoutControlGroup1);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存查询方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentQuerySchemeModel.querySchemeName.Equals("默认方案"))
            {
                LiInputDialog inputDialog = new LiInputDialog();
                if (inputDialog.ShowDialog() == DialogResult.Yes)
                {
                    QuerySchemeModel querySchemeModel = new QuerySchemeModel();
                    querySchemeModel.entityKey = entityKey;
                    querySchemeModel.userCode = LiContexts.LiContext.userInfo.userCode;
                    querySchemeModel.querySchemeName = inputDialog.getValue();
                    querySchemeModel.entitys = gridControl2.DataSource as List<EntityModel>;
                    querySchemeModel.fields = gridControl3.DataSource as List<FieldModel>;
                    querySchemeModel.querys = gridControl1.DataSource as List<QueryModel>;

                    LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).newEntity(querySchemeModel);
                    MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).tipStr, "温馨提示");

                    querySchemeModels = liListForm.loadQuerySchemeModels();
                    FormUtil.loadQueryScheme(querySchemeModels, querySchemeBtns, new System.EventHandler(this.btnQueryScheme_Click), layoutControlGroup1);

                }

            }
            else
            {
                currentQuerySchemeModel.entitys = gridControl2.DataSource as List<EntityModel>;
                currentQuerySchemeModel.fields = gridControl3.DataSource as List<FieldModel>;
                currentQuerySchemeModel.querys = gridControl1.DataSource as List<QueryModel>;
                LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).updateEntity(currentQuerySchemeModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.QueryScheme, LiContext.SystemCode).tipStr, "温馨提示");
            }
        }

        /// <summary>
        /// 查询返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentQuerySchemeModel.querys = gridControl1.DataSource as List<QueryModel>;
            returnQuerys = currentQuerySchemeModel.querys;
            this.DialogResult = DialogResult.Yes;
            return;
        }

        /// <summary>
        /// 查询方案按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryScheme_Click(object sender, EventArgs e)
        {
            SimpleButton btn = (SimpleButton)sender;

            QuerySchemeModel querySchemeModel = querySchemeModels.Where(m => m.querySchemeName == btn.Text).FirstOrDefault();
            if (querySchemeModel != null)
            {
                gridControl1.DataSource = querySchemeModel.querys;
                gridControl2.DataSource = querySchemeModel.entitys;
                gridControl3.DataSource = querySchemeModel.fields;
                currentQuerySchemeModel = querySchemeModel;
            }

            //显示按钮红色
            foreach (KeyValuePair<string, SimpleButton> kvp in querySchemeBtns)
            {
                if (!kvp.Key.Equals(btn.Text))
                {
                    kvp.Value.Appearance.BorderColor = Color.Transparent;
                }
                else
                {
                    kvp.Value.Appearance.BorderColor = Color.Red;
                }

            }

        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            DevControlUtil.customDrawRowIndicator(e);
        }

        /// <summary>
        /// 动态控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {

            if (e.RowHandle < 0) return;

            List<QueryModel> sList = gridControl1.DataSource as List<QueryModel>;
            QueryModel model = sList[e.RowHandle];
            FieldModel fieldModel = FieldModel.getDataSource(entityKey).Where(m => m.columnFieldName == model.sFieldName).FirstOrDefault();
            switch (e.Column.FieldName)
            {
                case "oQueryValue":
                    switch (model.sFieldType)
                    {
                        case "CheckEdit":
                            e.RepositoryItem = repositoryItemCheckEdit_Bool;
                            break;
                        case "DateTimeEdit":
                        case "TimeEdit":
                        case "DateEdit":
                            e.RepositoryItem = repositoryItemDateEdit_Date;
                            break;
                        case "IntEdit":
                        case "DecimalEdit":
                        case "CalcEdit":
                            e.RepositoryItem = repositoryItemCalcEdit_Decimal;
                            break;
                        case ControlType.StatusEdit:
                        case ControlType.GridLookUpEditComboBox:
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(model.sFieldType);
                            FormUtil.setGirdControlDataSource(fieldModel.sColumnControlType, fieldModel.dictInfoType, fieldModel.gridlookUpEditShowModelJson, gridControl1, e.RepositoryItem, this);
                            break;
                        case ControlType.UserEdit:
                        case ControlType.GridLookUpEditRef:
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(model.sFieldType);
                            FormUtil.setGirdControlDataSource(fieldModel.sColumnControlType, fieldModel.basicInfoKey, fieldModel.gridlookUpEditShowModelJson,gridControl1, e.RepositoryItem, this);
                            break;
                        default:
                            e.RepositoryItem = repositoryItemTextEdit_String;
                            break;
                    }
                    break;
            }

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            List<FieldModel> fieldList = repositoryItemGridLookUpEdit_sFieldName.DataSource as List<FieldModel>;
            QueryModel queryModel = gridView1.GetFocusedRow() as QueryModel;
            switch (e.Column.FieldName)
            {
                case "sFieldName":
                    FieldModel fieldModel = fieldList.Where(m => m.code == Convert.ToString(e.Value)).FirstOrDefault();
                    queryModel.sFieldType = fieldModel.sColumnControlType;
                    queryModel.sFieldName = fieldModel.columnFieldName;
                    break;
            }

        }
    }
}