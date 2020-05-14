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
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using LiCommon.Util;
using LiModel.Form;
using LiModel.Basic;
using LiModel.LiConvert;
using LiModel.LiEnum;
using LiHttp.Enum;
using LiContexts;
using LiControl.Util;
using LiModel.Util;


namespace LiManage
{
    public partial class LiManageConvertForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        List<TableModel> tableModelList;
        List<TableModel> tableBasicInfoModelList;

        List<ColumnModel> columnDestModelList;
        List<ColumnModel> columnSourceModelList;
        List<ColumnModel> columnRelationModelList;
        List<ColumnModel> columnBasicInfoModelList;

        Dictionary<ColumnModel, ConvertDestColumnInfo> columnSourceTableTypeDict = new Dictionary<ColumnModel, ConvertDestColumnInfo>();

        #region 引用数据源
        private ConvertDestTypeModel convertDestTypeModel = new ConvertDestTypeModel();
        private ConvertSourceTypeModel convertSourceTypeModel = new ConvertSourceTypeModel();
        private ConvertRelation convertRelation = new ConvertRelation();
        private TableModel tableModel = new TableModel();
        private ColumnModel columnModel = new ColumnModel();
        private JudgmentSymbolModel judgmentSymbolModel = new JudgmentSymbolModel();
        private ControlTypeModel controlTypeModel = new ControlTypeModel();
        #endregion

        public LiManageConvertForm()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            InitData();
            InitControl();
        }

        private void InitData()
        {
            List<LiConvertHeadModel> list = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).getEntityList<LiConvertHeadModel>();
            gridControl1.DataSource = list;

            tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>("master", "entityOrder");
            tableBasicInfoModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>("Basic", "entityType");

            columnDestModelList = new List<ColumnModel>();
            columnSourceModelList = new List<ColumnModel>();
            columnRelationModelList = new List<ColumnModel>();
            columnBasicInfoModelList = new List<ColumnModel>();
        }

        private void InitControl()
        {

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(convertDestTypeModel.getValueMember(), convertDestTypeModel.getDisplayMember(), convertDestTypeModel.getSearchColumns(), convertDestTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_convertDestType, this, convertDestTypeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(convertSourceTypeModel.getValueMember(), convertSourceTypeModel.getDisplayMember(), convertSourceTypeModel.getSearchColumns(), convertSourceTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_convertSourceType, this, convertSourceTypeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(convertRelation.getValueMember(), convertRelation.getDisplayMember(), convertRelation.getSearchColumns(), convertRelation.getDisplayColumns(), repositoryItemGridLookUpEdit_convertRelation, this, convertRelation.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_convertDest, this, tableModelList);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_convertSource, this, tableModelList);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_refBasicInfoType, this, tableBasicInfoModelList);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_convertDestField, this, columnDestModelList);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_convertSourceField, this, columnSourceModelList);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_convertRelationField, this, columnRelationModelList);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_refBasicInfoField, this, columnBasicInfoModelList);




            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(judgmentSymbolModel.getValueMember(), judgmentSymbolModel.getDisplayMember(), judgmentSymbolModel.getSearchColumns(), judgmentSymbolModel.getDisplayColumns(), repositoryItemGridLookUpEdit_sJudgeSymbol, this, judgmentSymbolModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(controlTypeModel.getValueMember(), controlTypeModel.getDisplayMember(), controlTypeModel.getSearchColumns(), controlTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_controlType, this, controlTypeModel.getDataSource());


        }

        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<LiConvertHeadModel> sList = gridControl1.DataSource as List<LiConvertHeadModel>;
            LiConvertHeadModel model = LiConvertHeadModel.getInstance();

            sList.Add(model);
            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiConvertHeadModel liConvertHeadModel = gridView1.GetFocusedRow() as LiConvertHeadModel;

            gridControl2.DataSource = liConvertHeadModel.datas;

            gridControl2.RefreshDataSource();
            gridView2.RefreshData();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<LiConvertHeadModel> sList = gridControl1.DataSource as List<LiConvertHeadModel>;
            LiConvertHeadModel model = gridView1.GetFocusedRow() as LiConvertHeadModel;
            DataUtil.deleteInList(model, sList);

            gridControl1.RefreshDataSource();
            gridView1.RefreshData();
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<LiConvertHeadModel> sList = gridControl1.DataSource as List<LiConvertHeadModel>;

            LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).batchSaveEntity(true, sList);

            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.LiConvert, LiContext.SystemCode).tipStr, "系统提示");
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            LiConvertHeadModel liConvertHeadModel = gridView1.GetFocusedRow() as LiConvertHeadModel;

            getColumnDestModelList();
            getColumnSourceModelList();

            gridControl2.DataSource = liConvertHeadModel.datas;

            gridControl2.RefreshDataSource();
            gridView2.RefreshData();


            gridControl8.DataSource = liConvertHeadModel.queryFields;

            gridControl8.RefreshDataSource();
            gridView8.RefreshData();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            LiConvertHeadModel entity = gridView1.GetFocusedRow() as LiConvertHeadModel;
            if (entity == null) return;

            List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(entity.convertDest, "entityKey");
            switch (e.Column.FieldName)
            {
                case "convertDest":
                    entity.datas.Clear();
                
                    foreach(TableModel tableModel in tableModelList){

                        foreach (ColumnModel columnModel in tableModel.datas)
                        {
                            LiConvertBodyModel bodyModel = new LiConvertBodyModel();
                            bodyModel.convertDestType = tableModel.tableName;
                            bodyModel.convertDestField = columnModel.columnName;
                            bodyModel.convertDCollection = tableModel.entityColumnName;
                            entity.datas.Add(bodyModel);

                        }
                    }

                    gridControl2.DataSource = entity.datas;
                    gridControl2.Refresh();
                    

                    getColumnDestModelList();

                    break;
                case "convertSource":
                    List<TableModel> tableDestModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(entity.convertSource, "entityKey");

                    entity.queryFields.Clear();
                    foreach (TableModel tableModel in tableDestModelList)
                    {

                        foreach (ColumnModel columnModel in tableModel.datas)
                        {

                            FieldModel fieldModel = new FieldModel();
                            fieldModel.code = string.Format("{0}_{1}", tableModel.tableName, columnModel.columnName);
                            fieldModel.name = columnModel.columnAbbName;
                            fieldModel.sEntityCode = tableModel.tableName;
                            fieldModel.sEntityName = tableModel.entityName;
                            fieldModel.fieldName = columnModel.columnName;
                            fieldModel.columnFieldName = columnModel.columnAbbName;
                            fieldModel.iColumnWidth = 100;
                            fieldModel.bColumnDisplay = true;
                            fieldModel.bQuery = false;
                            fieldModel.bRange = false;
                            fieldModel.sColumnControlType = "TextEdit";
                            fieldModel.sRefTypeCode = "";
                            fieldModel.sJudgeSymbol = JudgmentSymbol.Like;

                            entity.queryFields.Add(fieldModel);
                        }

                    }

                    gridControl8.DataSource = entity.queryFields;
                    gridControl8.Refresh();


                    getColumnSourceModelList();
                    break;
            }
        }


        private void getColumnDestModelList()
        {
            LiConvertHeadModel entity = gridView1.GetFocusedRow() as LiConvertHeadModel;
            if (entity == null) return;

            List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(entity.convertDest, "entityKey");

            columnDestModelList.Clear();
            foreach (TableModel tableModel in tableModelList)
            {
                columnDestModelList.AddRange(tableModel.datas);

            }
        }

        private void getColumnSourceModelList()
        {
            LiConvertHeadModel entity = gridView1.GetFocusedRow() as LiConvertHeadModel;
            if (entity == null) return;

            List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(entity.convertSource, "entityKey");

            columnSourceTableTypeDict.Clear();
            columnSourceModelList.Clear();
            foreach (TableModel tableModel in tableModelList)
            {
                columnSourceModelList.AddRange(tableModel.datas);
                foreach(ColumnModel columnModel in tableModel.datas)
                {
                    ConvertDestColumnInfo info = new ConvertDestColumnInfo();
                    info.convertSourceType = tableModel.tableName;
                    info.convertSCollection = tableModel.entityColumnName;

                    columnSourceTableTypeDict.Add(columnModel, info);
                }
            }
        }

        private void repositoryItemGridLookUpEdit_convertRelationField_BeforePopup(object sender, EventArgs e)
        {
            LiConvertHeadModel entity = gridView1.GetFocusedRow() as LiConvertHeadModel;
            columnRelationModelList.Clear();

            if (entity.convertRelation == "2")
            {
                List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(entity.convertDest, "entityKey");

                foreach (TableModel tableModel in tableModelList)
                {
                    columnRelationModelList.AddRange(tableModel.datas.Where(m => m.columnType == "decimal" || m.columnType == "float" || m.columnType == "int" || m.columnType == "numeric" || m.columnType == "smallint" || m.columnType == "tinyint").ToArray());
                }
            }

        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            LiConvertBodyModel liConvertBodyModel = gridView2.GetFocusedRow() as LiConvertBodyModel;

            switch (e.Column.FieldName)
            {
                case "convertSourceField":
                    ColumnModel columnModel = columnSourceModelList.Where(m => m.columnName == Convert.ToString(e.Value)).FirstOrDefault();
                    liConvertBodyModel.convertSourceType = columnSourceTableTypeDict[columnModel].convertSourceType;
                    liConvertBodyModel.convertSCollection = columnSourceTableTypeDict[columnModel].convertSCollection;
                    gridView2.RefreshRow(e.RowHandle);
                    break;
            }
        }

        private void repositoryItemGridLookUpEdit_refBasicInfoField_BeforePopup(object sender, EventArgs e)
        {
            LiConvertBodyModel entity = gridView2.GetFocusedRow() as LiConvertBodyModel;

            TableModel tableModel = tableBasicInfoModelList.Where(m => m.entityKey == entity.refBasicInfoType).FirstOrDefault();
            if (tableModel != null)
            {
                columnBasicInfoModelList = tableModel.datas;
                repositoryItemGridLookUpEdit_refBasicInfoField.DataSource = columnBasicInfoModelList;
            }
        }

    }
    public struct ConvertDestColumnInfo
    {
        public string convertSourceType;
        public string convertSCollection;
    }
}