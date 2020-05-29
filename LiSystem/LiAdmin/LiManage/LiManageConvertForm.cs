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
using LiHttp.RequestParam;
using LiU8API.Model;
using LiU8API;
using LiU8API.LiEnum;

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

            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityOrder", "master", "systemCode", LiContext.SystemCode);
            tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

            paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityType", "Basic", "systemCode", LiContext.SystemCode);
            tableBasicInfoModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

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
            QueryParamModel paramModel;

            paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityKey", entity.convertDest, "systemCode", LiContext.SystemCode);
            List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);
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
                    paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
                    QueryParamModel.getWHereANDByTwoParam(paramModel, "entityKey", entity.convertSource, "systemCode", LiContext.SystemCode);
                    List<TableModel> tableDestModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

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

            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityKey", entity.convertDest, "systemCode", LiContext.SystemCode);
            List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

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

            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityKey", entity.convertSource, "systemCode", LiContext.SystemCode);
            List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

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
                QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
                QueryParamModel.getWHereANDByTwoParam(paramModel, "entityKey", entity.convertDest, "systemCode", LiContext.SystemCode);
                List<TableModel> tableModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

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

        private void BarButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiU8VoucherModel liU8VoucherModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntitySingle<LiU8VoucherModel>(1,"id");

            U8Voucher u8Voucher = new U8Voucher(OperationType.NEW, liU8VoucherModels);
            u8Voucher.initDomHead();
            u8Voucher.initDomBody(1);
            u8Voucher.setDomHeadValue("id", "");
            u8Voucher.setDomHeadValue("ivtid", "95");
            u8Voucher.setDomHeadValue("cbustype", "普通销售");
            u8Voucher.setDomHeadValue("cstcode", "01");
            u8Voucher.setDomHeadValue("ddate", "2015-01-21");
            u8Voucher.setDomHeadValue("ccuscode", "00000002");
            u8Voucher.setDomHeadValue("cdepcode", "0302");
            u8Voucher.setDomHeadValue("cpersoncode", "00023");
            u8Voucher.setDomHeadValue("ccusoaddress", "武昌");
            u8Voucher.setDomHeadValue("cexch_name", "人民币");
            u8Voucher.setDomHeadValue("iexchrate", "1");
            u8Voucher.setDomHeadValue("itaxrate", "17");
            u8Voucher.setDomHeadValue("cmaker", "demo");
            u8Voucher.setDomHeadValue("dpredatebt", "2015-01-21");
            u8Voucher.setDomHeadValue("ccusphone", "023-35835833");
            u8Voucher.setDomHeadValue("cstname", "普通销售");
            u8Voucher.setDomHeadValue("iarmoney", "234100");
            u8Voucher.setDomHeadValue("ccusname", "云飞电子科技集团");
            u8Voucher.setDomHeadValue("ccusaddress", "湖北省武汉市武昌区信息路11号");
            u8Voucher.setDomHeadValue("breturnflag", "0");
            u8Voucher.setDomHeadValue("icuscreline", "1000000000");
            u8Voucher.setDomHeadValue("ccusperson", "阚奇");
            u8Voucher.setDomHeadValue("caddcode", "001");
            u8Voucher.setDomHeadValue("cdeliverunit", "");
            u8Voucher.setDomHeadValue("ccreditcuscode", "0111");
            u8Voucher.setDomHeadValue("ccreditcusname", "云飞电子科技集团");
            u8Voucher.setDomHeadValue("iverifystate", "0");
            u8Voucher.setDomHeadValue("iswfcontrolled", "1");
            u8Voucher.setDomHeadValue("zdsum", ".00");


            u8Voucher.setDomBodyValue(0, "irowno", "1");
            u8Voucher.setDomBodyValue(0, "autoid", "");
            u8Voucher.setDomBodyValue(0, "id", "");
            u8Voucher.setDomBodyValue(0, "cinvcode", "01019002065");
            u8Voucher.setDomBodyValue(0, "bservice", "0");
            u8Voucher.setDomBodyValue(0, "cinvname", "硬盘-1000G");
            u8Voucher.setDomBodyValue(0, "cinvstd", "希捷 1000G/7200RPM/32M/SATA2/企业级");
            u8Voucher.setDomBodyValue(0, "cinvm_unit", "PCS");
            u8Voucher.setDomBodyValue(0, "igrouptype", "0");
            u8Voucher.setDomBodyValue(0, "cgroupcode", "01");
            u8Voucher.setDomBodyValue(0, "iquantity", "3.0000000000");
            u8Voucher.setDomBodyValue(0, "iquotedprice", "100.0000000000");
            u8Voucher.setDomBodyValue(0, "iunitprice", "85.4700000000");
            u8Voucher.setDomBodyValue(0, "imoney", "170.94");
            u8Voucher.setDomBodyValue(0, "itax", "29.06");
            u8Voucher.setDomBodyValue(0, "isum", "200");
            u8Voucher.setDomBodyValue(0, "inatunitprice", "85.4700000000");
            u8Voucher.setDomBodyValue(0, "inatmoney", "170.94");
            u8Voucher.setDomBodyValue(0, "inattax", "29.06");
            u8Voucher.setDomBodyValue(0, "inatsum", "200");
            u8Voucher.setDomBodyValue(0, "inatdiscount", "0");
            u8Voucher.setDomBodyValue(0, "idiscount", "0");
            u8Voucher.setDomBodyValue(0, "fsalecost", ".0000000000");
            u8Voucher.setDomBodyValue(0, "fsaleprice", ".0000000000");
            u8Voucher.setDomBodyValue(0, "dpredate", "2015-02-10");
            u8Voucher.setDomBodyValue(0, "itaxunitprice", "100.0000000000");
            u8Voucher.setDomBodyValue(0, "cconfigstatus", "未选配");
            u8Voucher.setDomBodyValue(0, "dpremodate", "2015-02-09");
            u8Voucher.setDomBodyValue(0, "batomodel", "0");
            u8Voucher.setDomBodyValue(0, "dreleasedate", "2015-02-10T00:00:00");
            u8Voucher.setDomBodyValue(0, "editprop", "A");

            U8APIReponse u8APIReponse = u8Voucher.commit();

        }

        private void BarButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiU8VoucherModel liU8VoucherModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntitySingle<LiU8VoucherModel>(3, "id");

            U8Voucher u8Voucher = new U8Voucher(OperationType.NEW, liU8VoucherModels);
            u8Voucher.initDomHead();
            u8Voucher.initDomBody(1);


            u8Voucher.setDomHeadValue("poid", "");
            u8Voucher.setDomHeadValue("cbustype", "95");
            u8Voucher.setDomHeadValue("dpodate", "95");
            u8Voucher.setDomHeadValue("cpoid", "95");
            u8Voucher.setDomHeadValue("cvenabbname", "95");
            u8Voucher.setDomHeadValue("cexch_name", "95");
            u8Voucher.setDomHeadValue("nflat", "95");
            u8Voucher.setDomHeadValue("cmaker", "95");
            u8Voucher.setDomHeadValue("cvencode", "95");
            u8Voucher.setDomHeadValue("idiscounttaxtype", "95");
            u8Voucher.setDomHeadValue("contractcodet", "95");
            u8Voucher.setDomHeadValue("iflowid", "95");
            u8Voucher.setDomHeadValue("cflowname", "95");
            u8Voucher.setDomHeadValue("ccontactcode", "95");
            u8Voucher.setDomHeadValue("cmobilephone", "95");
            u8Voucher.setDomHeadValue("cappcode", "95");
            u8Voucher.setDomHeadValue("csysbarcode", "95");

            u8Voucher.setDomBodyValue(0, "id", "A");
            u8Voucher.setDomBodyValue(0, "cinvcode", "A");
            u8Voucher.setDomBodyValue(0, "iquantity", "A");
            u8Voucher.setDomBodyValue(0, "darrivedate", "A");
            u8Voucher.setDomBodyValue(0, "ivouchrowno", 1);
            u8Voucher.setDomBodyValue(0, "cbmemo", "A");
            u8Voucher.setDomBodyValue(0, "editprop", "A");
            u8Voucher.setDomBodyValue(0, "editprop", "A");
            u8Voucher.setDomBodyValue(0, "editprop", "A");
            u8Voucher.setDomBodyValue(0, "editprop", "A");

            U8APIReponse u8APIReponse = u8Voucher.commit();
        }
    }
    public struct ConvertDestColumnInfo
    {
        public string convertSourceType;
        public string convertSCollection;
    }
}