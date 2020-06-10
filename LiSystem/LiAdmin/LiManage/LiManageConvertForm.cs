using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using LiCommon.Util;
using LiContexts;
using LiControl.Util;
using LiForm.Dev.Util;
using LiHttp.Enum;
using LiHttp.RequestParam;
using LiModel.Basic;
using LiModel.Form;
using LiModel.LiConvert;
using LiModel.LiEnum;
using LiU8API;
using LiU8API.LiEnum;
using LiU8API.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LiManage
{
    public partial class LiManageConvertForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        List<TableModel> tableAllModelList;
        List<TableModel> tableModelList;
        List<TableModel> tableBasicInfoModelList;
        List<LiU8VoucherModel> u8VoucherModels;

        //Dictionary<ColumnModel, ConvertDestColumnInfo> columnSourceTableTypeDict = new Dictionary<ColumnModel, ConvertDestColumnInfo>();

        #region 引用数据源
        private LiU8FieldModel liU8FieldModel = new LiU8FieldModel();
        private ConvertDestTypeModel convertDestTypeModel = new ConvertDestTypeModel();
        private ConvertSourceTypeModel convertSourceTypeModel = new ConvertSourceTypeModel();
        private ConvertRelation convertRelation = new ConvertRelation();
        private TableModel tableModel = new TableModel();
        private LiU8VoucherModel liU8VoucherModel = new LiU8VoucherModel();
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

            tableAllModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>();

            paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getQueryParamModel_ShowAllColumn();
            QueryParamModel.getWHereANDByTwoParam(paramModel, "entityType", "Basic", "systemCode", LiContext.SystemCode);
            tableBasicInfoModelList = LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(paramModel);

            u8VoucherModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntityList<LiU8VoucherModel>();

        }

        private void InitControl()
        {

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(convertDestTypeModel.getValueMember(), convertDestTypeModel.getDisplayMember(), convertDestTypeModel.getSearchColumns(), convertDestTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_convertDestType, this, convertDestTypeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(convertSourceTypeModel.getValueMember(), convertSourceTypeModel.getDisplayMember(), convertSourceTypeModel.getSearchColumns(), convertSourceTypeModel.getDisplayColumns(), repositoryItemGridLookUpEdit_convertSourceType, this, convertSourceTypeModel.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(convertRelation.getValueMember(), convertRelation.getDisplayMember(), convertRelation.getSearchColumns(), convertRelation.getDisplayColumns(), repositoryItemGridLookUpEdit_convertRelation, this, convertRelation.getDataSource());

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), repositoryItemGridLookUpEdit_refBasicInfoType, this, tableBasicInfoModelList);

            //GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_convertSourceField, this, columnSourceModelList);

            //GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_convertRelationField, this, columnRelationModelList);

            //GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), repositoryItemGridLookUpEdit_refBasicInfoField, this, columnBasicInfoModelList);

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

            switch (e.Column.FieldName)
            {
                case "convertDest":
                    switch (entity.convertDestType)
                    {
                        case ConvertDestTypeModel.System:
                            getLiConvertBodysByTableModel(entity.convertDest, entity.datas);
                            break;
                        case ConvertDestTypeModel.U8:
                            getLiConvertBodysByLiU8Field(entity.convertDest, entity.datas);
                            break;
                    }
                    gridControl2.DataSource = entity.datas;
                    gridControl2.Refresh();
                    break;
                case "convertSource":
                    getFieldModelByTableModel(entity.convertSource, entity.queryFields);
                    gridControl8.DataSource = entity.queryFields;
                    gridControl8.Refresh();


                    break;
            }
        }

        /// <summary>
        /// 根据TableModel获取查询条件
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="fieldModels"></param>
        private void getFieldModelByTableModel(string entityKey, List<FieldModel> fieldModels)
        {
            List<TableModel> tableDestModelList = tableAllModelList.Where(m => m.entityKey == entityKey).ToList();

            fieldModels.Clear();
            foreach (TableModel tableModel in tableDestModelList)
            {

                foreach (ColumnModel columnModel in tableModel.datas)
                {

                    FieldModel fieldModel = new FieldModel();
                    fieldModel.code = string.Format("{0}_{1}", tableModel.tableName, columnModel.columnName);
                    fieldModel.name = columnModel.columnAbbName;
                    fieldModel.sEntityCode = tableModel.tableName;
                    fieldModel.sEntityName = tableModel.tableAbbName;
                    fieldModel.fieldName = columnModel.columnName;
                    fieldModel.columnFieldName = columnModel.columnAbbName;
                    fieldModel.iColumnWidth = 100;
                    fieldModel.bColumnDisplay = true;
                    fieldModel.bQuery = false;
                    fieldModel.bRange = false;
                    fieldModel.sColumnControlType =ControlType.TextEdit;
                    fieldModel.sRefTypeCode = "";
                    fieldModel.sJudgeSymbol = JudgmentSymbol.Like;

                    fieldModels.Add(fieldModel);
                }

            }
        }

        /// <summary>
        /// 根据TableModel获取转换的表体
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="liConvertBodyModels"></param>
        private void getLiConvertBodysByTableModel(string entityKey, List<LiConvertBodyModel> liConvertBodyModels)
        {
            liConvertBodyModels.Clear();

            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == entityKey).ToList();

            foreach(TableModel tableModel in tableModelList)
            {
                foreach (ColumnModel columnModel in tableModel.datas)
                {
                    LiConvertBodyModel bodyModel = new LiConvertBodyModel();
                    bodyModel.convertDestType = tableModel.tableName;
                    bodyModel.convertDestField = columnModel.columnName;
                    bodyModel.convertDCollection = tableModel.entityColumnName;
                    liConvertBodyModels.Add(bodyModel);
                }
            }
        }

        private List<ColumnModel> getColumnModelsByTableModel(string entityKey)
        {
            List<ColumnModel> columnModels = new List<ColumnModel>();

            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == entityKey).ToList();

            foreach (TableModel tableModel in tableModelList)
            {
                columnModels.AddRange(tableModel.datas.ToArray());
            }
            return columnModels;
        }

        /// <summary>
        /// 根据LiU8Field获取转换的表体
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="liConvertBodyModels"></param>
        private void getLiConvertBodysByLiU8Field(string entityKey, List<LiConvertBodyModel> liConvertBodyModels)
        {
            liConvertBodyModels.Clear();
            List<LiU8FieldModel>  liU8Fields= getLiU8FieldsByLiU8Voucher(entityKey);
            foreach (LiU8FieldModel liU8Field in liU8Fields)
            {
                LiConvertBodyModel bodyModel = new LiConvertBodyModel();
                bodyModel.convertDestType = liU8Field.fieldEntityType;
                bodyModel.convertDestField = liU8Field.fieldName;
                bodyModel.convertDCollection = getBodyFieldNameByLiU8Voucher(entityKey);
                liConvertBodyModels.Add(bodyModel);
            }
        }

        /// <summary>
        /// 获取U8API同步的字段集合
        /// </summary>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        private List<LiU8FieldModel> getLiU8FieldsByLiU8Voucher(string entityKey)
        {
            LiU8VoucherModel liU8VoucherModel = u8VoucherModels.Where(m => m.code == entityKey).FirstOrDefault();
            if (liU8VoucherModel != null && liU8VoucherModel.operations != null && liU8VoucherModel.operations.Count > 0)
            {
                LiU8OperationModel liU8Operation = liU8VoucherModel.operations.Where(m => m.operationCode == "New").FirstOrDefault();
                if (liU8Operation != null)
                {

                    return liU8Operation.fields;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取表体字段名
        /// </summary>
        /// <returns></returns>
        private string getBodyFieldNameByLiU8Voucher(string entityKey)
        {
            LiU8VoucherModel liU8VoucherModel = u8VoucherModels.Where(m => m.code == entityKey).FirstOrDefault();
            LiU8OperationModel liU8Operation = liU8VoucherModel.operations.Where(m => m.operationCode == "New").FirstOrDefault();
            if (liU8Operation != null)
            {
                LiU8ParamModel liU8Param = liU8Operation.paramModels.Where(m => m.paramBoType == "1").FirstOrDefault();
                if (liU8Param != null) return liU8Param.paramName;

            }

            return string.Empty;
        }


        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            LiConvertHeadModel entityHead = gridView1.GetFocusedRow() as LiConvertHeadModel;

            LiConvertBodyModel entityBody = gridView2.GetFocusedRow() as LiConvertBodyModel;
            if (entityBody == null || entityHead == null) return;

            switch (e.Column.FieldName)
            {
                case "convertSourceField":
                    switch (entityHead.convertSourceType)
                    {
                        case ConvertSourceTypeModel.System:

                            List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == entityHead.convertSource).ToList();
                            foreach(TableModel tableModel in tableModelList)
                            {
                                ColumnModel columnModel = tableModel.datas.Where(m => m.columnName == Convert.ToString(e.Value)).FirstOrDefault();
                                if(columnModel != null)
                                {
                                    entityBody.convertSourceType = tableModel.tableName;
                                }
                            }

                            break;
                        case ConvertSourceTypeModel.U8:
                            List<LiU8FieldModel> liU8Fields = getLiU8FieldsByLiU8Voucher(entityHead.convertSource);
                            LiU8FieldModel liU8FieldModel = liU8Fields.Where(m=>m.fieldName == Convert.ToString(e.Value)).FirstOrDefault();
                            
                            if(liU8FieldModel != null)
                            {
                                entityBody.convertSourceType = liU8FieldModel.fieldEntityType;
                            }
                            break;
                    }
                    break;
            }
            //gridView2.RefreshData();
            //gridControl2.Refresh();
        }

        private void BarButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            LiU8VoucherModel liU8VoucherModels = LiContexts.LiContext.getHttpEntity(LiEntityKey.LiU8Voucher).getEntitySingle<LiU8VoucherModel>(1, "id");

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


            u8Voucher.setDomHeadValue("ivtid", "8173");
            u8Voucher.setDomHeadValue("poid", "");
            u8Voucher.setDomHeadValue("cbustype", "普通采购");
            u8Voucher.setDomHeadValue("cpoid", "0000000003");
            u8Voucher.setDomHeadValue("dpodate", "2020-06-01");
            u8Voucher.setDomHeadValue("cvencode", "99001");
            //u8Voucher.setDomHeadValue("cdepcode", "0401");
            //u8Voucher.setDomHeadValue("cpersoncode", "00043");
            u8Voucher.setDomHeadValue("nflat", "1");
            u8Voucher.setDomHeadValue("itaxrate", "17");
            u8Voucher.setDomHeadValue("icost", "0");
            u8Voucher.setDomHeadValue("ibargain", "0");
            u8Voucher.setDomHeadValue("cstate", "1");
            u8Voucher.setDomHeadValue("idiscounttaxtype", "0");
            u8Voucher.setDomHeadValue("cdefine1", "顾潇");
            u8Voucher.setDomHeadValue("cvenabbname", "广州曜能量饮料有限公司");
            //u8Voucher.setDomHeadValue("cdepname", "采购部");
            //u8Voucher.setDomHeadValue("cpersonname", "顾潇");
            u8Voucher.setDomHeadValue("cexch_name", "人民币");
            u8Voucher.setDomHeadValue("cmaker", "demo9");
            u8Voucher.setDomHeadValue("ireturncount", "0");
            u8Voucher.setDomHeadValue("iverifystateex", "2");
            u8Voucher.setDomHeadValue("iswfcontrolled", "0");
            u8Voucher.setDomHeadValue("cverifier", "demo");
            u8Voucher.setDomHeadValue("cvenname", "广州曜能量饮料有限公司");
            u8Voucher.setDomHeadValue("cvenregcode", "cvenregcode");
            //u8Voucher.setDomHeadValue("cvoucherstate", "审核");
            u8Voucher.setDomHeadValue("cvenbank", "招商银行广州分行越秀支行");
            u8Voucher.setDomHeadValue("cmaketime", "2020-06-01");
            u8Voucher.setDomHeadValue("caudittime", "2020-06-01");
            u8Voucher.setDomHeadValue("cauditdate", "2020-06-01");
            //u8Voucher.setDomHeadValue("bstorageorder", "True");
            //u8Voucher.setDomHeadValue("iprintcount", "0");
            //u8Voucher.setDomHeadValue("csysbarcode", "||pupo|0000000037");
            //u8Voucher.setDomHeadValue("editprop", "A");

            //u8Voucher.setDomHeadValue("poid", "");
            //u8Voucher.setDomHeadValue("cbustype", "普通采购");
            //u8Voucher.setDomHeadValue("dpodate", "2015-01-05");
            //u8Voucher.setDomHeadValue("cpoid", "0000000037");
            ////u8Voucher.setDomHeadValue("cvenabbname", "95");
            //u8Voucher.setDomHeadValue("cexch_name", "人民币");
            //u8Voucher.setDomHeadValue("nflat", "1");
            ////u8Voucher.setDomHeadValue("cmaker", "95");
            //u8Voucher.setDomHeadValue("cvencode", "01005");
            //u8Voucher.setDomHeadValue("cdepcode", "0401");
            //u8Voucher.setDomHeadValue("cpersoncode", "00043");
            //u8Voucher.setDomHeadValue("itaxrate", "17");
            //u8Voucher.setDomHeadValue("nflat", "1");
            //u8Voucher.setDomHeadValue("idiscounttaxtype", "0");
            ////u8Voucher.setDomHeadValue("contractcodet", "95");
            ////u8Voucher.setDomHeadValue("iflowid", "95");
            ////u8Voucher.setDomHeadValue("cflowname", "95");
            ////u8Voucher.setDomHeadValue("ccontactcode", "95");
            ////u8Voucher.setDomHeadValue("cmobilephone", "95");
            ////u8Voucher.setDomHeadValue("cappcode", "95");
            ////u8Voucher.setDomHeadValue("csysbarcode", "95");

            u8Voucher.setDomBodyValue(0, "id", "");
            u8Voucher.setDomBodyValue(0, "poid", "");
            u8Voucher.setDomBodyValue(0, "cinvcode", "010101");
            u8Voucher.setDomBodyValue(0, "cinvname", "250ml曜能量");
            u8Voucher.setDomBodyValue(0, "iquantity", "165.0000000000");
            u8Voucher.setDomBodyValue(0, "inum", ".0000000000");
            u8Voucher.setDomBodyValue(0, "iunitprice", "42.7400000000");
            u8Voucher.setDomBodyValue(0, "imoney", "7051.28");
            u8Voucher.setDomBodyValue(0, "itax", "1198.72");
            u8Voucher.setDomBodyValue(0, "isum", "8250");
            u8Voucher.setDomBodyValue(0, "inatunitprice", "42.7400000000");
            u8Voucher.setDomBodyValue(0, "inatmoney", "7051.28");
            u8Voucher.setDomBodyValue(0, "inattax", "1198.72");
            u8Voucher.setDomBodyValue(0, "inatsum", "8250");
            u8Voucher.setDomBodyValue(0, "darrivedate", "2020-06-01");
            u8Voucher.setDomBodyValue(0, "cinvm_unit", "箱");
            u8Voucher.setDomBodyValue(0, "igrouptype", "0");
            //u8Voucher.setDomBodyValue(0, "cgroupcode", "01");
            u8Voucher.setDomBodyValue(0, "iinvexchrate", ".00000000");
            //u8Voucher.setDomBodyValue(0, "ccomunitcode", "0108");
            u8Voucher.setDomBodyValue(0, "ipertaxrate", "17.000000");
            u8Voucher.setDomBodyValue(0, "itaxprice", "50.000000");
            u8Voucher.setDomBodyValue(0, "btaxcost", "True");
            //u8Voucher.setDomBodyValue(0, "ireceivedqty", ".00000000");
            u8Voucher.setDomBodyValue(0, "bgsp", "0");
            u8Voucher.setDomBodyValue(0, "sotype", "0");
            u8Voucher.setDomBodyValue(0, "cbcloser", "");
            u8Voucher.setDomBodyValue(0, "iordertype", "0");
            u8Voucher.setDomBodyValue(0, "ivouchrowno", "1");
            u8Voucher.setDomBodyValue(0, "csrpolicy", "PE");
            u8Voucher.setDomBodyValue(0, "irequiretrackstyle", "0");
            //u8Voucher.setDomBodyValue(0, "cinvccode", "16");
            //u8Voucher.setDomBodyValue(0, "binvtype", "0");
            //u8Voucher.setDomBodyValue(0, "bservice", "0");
            //u8Voucher.setDomBodyValue(0, "cbsysbarcode", "||pupo|0000000037|1");
            u8Voucher.setDomBodyValue(0, "cplanmethod", "R");
            u8Voucher.setDomBodyValue(0, "bgift", "0");
            //u8Voucher.setDomBodyValue(0, "cfactorycode", "001");
            //u8Voucher.setDomBodyValue(0, "cfactoryname", "工厂一");
            u8Voucher.setDomBodyValue(0, "editprop", "A");

            //u8Voucher.setDomBodyValue(0, "id", "");
            //u8Voucher.setDomBodyValue(0, "cinvcode", "SJK001");
            //u8Voucher.setDomBodyValue(0, "iquantity", "108.0000000000");
            //u8Voucher.setDomBodyValue(0, "darrivedate", "2015-01-06");
            //u8Voucher.setDomBodyValue(0, "ivouchrowno", 1);
            ////u8Voucher.setDomBodyValue(0, "cbmemo", "A");
            //u8Voucher.setDomBodyValue(0, "editprop", "A");

            U8APIReponse u8APIReponse = u8Voucher.commit();
        }


        private void GridView2_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

            LiConvertHeadModel entityHead = gridView1.GetFocusedRow() as LiConvertHeadModel;

            LiConvertBodyModel entityBody = gridView2.GetFocusedRow() as LiConvertBodyModel;

            switch (e.Column.FieldName)
            {
                case "convertDestField":
                    switch (entityHead.convertDestType)
                    {
                        case ConvertDestTypeModel.System:
                            List<ColumnModel> columnModels = getColumnModelsByTableModel(entityHead.convertDest);
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, columnModels);
                            break;
                        case ConvertDestTypeModel.U8:

                            List<LiU8FieldModel> liU8Fields = getLiU8FieldsByLiU8Voucher(entityHead.convertDest);
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, liU8FieldModel.getValueMember(), liU8FieldModel.getDisplayMember(), liU8FieldModel.getSearchColumns(), liU8FieldModel.getDisplayColumns(), liU8FieldModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, liU8Fields);
                            break;
                    }
                    break;
                case "convertSourceField":
                    switch (entityHead.convertSourceType)
                    {
                        case ConvertDestTypeModel.System:
                            List<ColumnModel> columnModels = getColumnModelsByTableModel(entityHead.convertSource);
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, columnModels);
                            break;
                        case ConvertDestTypeModel.U8:

                            List<LiU8FieldModel> liU8Fields = getLiU8FieldsByLiU8Voucher(entityHead.convertSource);
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, liU8FieldModel.getValueMember(), liU8FieldModel.getDisplayMember(), liU8FieldModel.getSearchColumns(), liU8FieldModel.getDisplayColumns(), liU8FieldModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, liU8Fields);
                            break;
                    }
                    break;
            }
        }

        private void GridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

            LiConvertHeadModel entity = gridView1.GetFocusedRow() as LiConvertHeadModel;

            switch (e.Column.FieldName)
            {
                case "convertRelationField":
                    List<ColumnModel> columnRelationModelList = new List<ColumnModel>();

                    if (entity.convertRelation == "2")
                    {
                        List<TableModel> tableModelList = tableAllModelList.Where(m => m.entityKey == entity.convertDest).ToList();
                        foreach (TableModel tableModel in tableModelList)
                        {
                            columnRelationModelList.AddRange(tableModel.datas.Where(m => m.columnType == "decimal" || m.columnType == "float" || m.columnType == "int" || m.columnType == "numeric" || m.columnType == "smallint" || m.columnType == "tinyint").ToArray());
                        }

                        e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                        GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, columnRelationModelList);
                    }

                    break;
                case "convertDest":
                    switch (entity.convertDestType)
                    {
                        case ConvertDestTypeModel.System:
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, tableModelList);
                            break;
                        case ConvertDestTypeModel.U8:
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, liU8VoucherModel.getValueMember(), liU8VoucherModel.getDisplayMember(), liU8VoucherModel.getSearchColumns(), liU8VoucherModel.getDisplayColumns(), liU8VoucherModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, u8VoucherModels);
                            break;
                    }

                    break;
                case "convertSource":
                    switch (entity.convertSourceType)
                    {
                        case ConvertSourceTypeModel.System:
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, tableModelList);
                            break;
                        case ConvertSourceTypeModel.U8:
                            e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, liU8VoucherModel.getValueMember(), liU8VoucherModel.getDisplayMember(), liU8VoucherModel.getSearchColumns(), liU8VoucherModel.getDisplayColumns(), liU8VoucherModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, u8VoucherModels);
                            break;
                    }

                    break;
            }
        }

        private void GridView2_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.RowHandle < 0) return;

            LiConvertBodyModel entity = gridView2.GetFocusedRow() as LiConvertBodyModel;

            switch (e.Column.FieldName)
            {
                case "refBasicInfoValueField":
                case "refBasicInfoField":
                    if (!string.IsNullOrEmpty(entity.refBasicInfoType))
                    {
                        List<ColumnModel> columnModels = getColumnModelsByTableModel(entity.refBasicInfoType);
                        e.RepositoryItem = ControlModelUtil.getRepositoryItemControl(ControlType.GridLookUpEditRef);

                        GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, columnModel.getValueMember(), columnModel.getDisplayMember(), columnModel.getSearchColumns(), columnModel.getDisplayColumns(), columnModel.getDictModelDesc(), e.RepositoryItem as RepositoryItemGridLookUpEdit, this, columnModels);
                    }
                    break;
            }
        }
    }
    public struct ConvertDestColumnInfo
    {
        public string convertSourceType;
        public string convertSCollection;
    }
}