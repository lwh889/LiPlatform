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
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

using LiModel.Form;
using LiModel.Dev.GridlookUpEdit;
using LiHttp.Server;
using LiCommon.Util;
using LiModel.Basic;
using LiHttp.Enum;
using LiContexts;
using LiControl.Util;
using LiModel.Util;
using LiControl.Form;

using Newtonsoft.Json;
using LiModel.LiEnum;

namespace LiManage
{
    public partial class LiManageBasicInfoInOutDesin : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        /// <summary>
        /// 单据控件状态，用于更新
        /// </summary>
        private List<ControlStatusModel> newControlStatusModels = new List<ControlStatusModel>();

        /// <summary>
        /// 单据上的按钮
        /// </summary>
        Dictionary<string, BarButtonItem> buttonDict = new Dictionary<string, DevExpress.XtraBars.BarButtonItem>();

        /// <summary>
        /// 单据上的表头控件
        /// </summary>
        Dictionary<string, Control> controlDict = new Dictionary<string, Control>();
        /// <summary>
        /// 单据上的表头控件标题
        /// </summary>
        Dictionary<string, LayoutControlItem> layoutControlItemDict = new Dictionary<string, LayoutControlItem>();

        /// <summary>
        /// 表体列内容
        /// </summary>
        Dictionary<string, GridColumn> gridColumnDict = new Dictionary<string, GridColumn>();
        /// <summary>
        /// 状态，用于只读控制
        /// </summary>
        private Dictionary<string, Dictionary<string, bool>> _statusDict = new Dictionary<string, Dictionary<string, bool>>();

        /// <summary>
        /// 菜单模型
        /// </summary>
        private TableModel tableModel;

        /// <summary>
        /// 表单的状态
        /// </summary>
        public string formStatusCode { set; get; }

        #region 引用数据源
        private SystemInfoModel systemInfoModel = new SystemInfoModel();
        #endregion

        public void setNewStatus()
        {
            formStatusCode = "NewStatus";
        }

        public void setEditStatus()
        {
            formStatusCode = "EditStatus";
        }

        public void setShowStatus()
        {
            formStatusCode = "ShowStatus";
        }

        private string formID = "LiManageBasicInfoInOutDesin";

        public Dictionary<string, GridlookUpEditShowModel> refControls;
        public Dictionary<string, DataTable> refDatas;

        public LiManageBasicInfoInOutDesin(TableModel tableModel)
        {
            InitializeComponent();

            Init();

            this.tableModel = tableModel;
        }

        public void Init()
        {
            InitData();
            InitGridlookUpEdit();
        }

        public void InitData()
        {
            systemInfoModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.SystemInfo).getEntityList<SystemInfoModel>());
            //读取Form上的控件
            DevFormUtil.getControlInForm(formID + ".", layoutControlItemDict, controlDict, this);
            DevFormUtil.getGridColumnInForm(formID + ".", gridColumnDict, this);
            DevFormUtil.getBarButtonItemInForm(formID + ".", buttonDict, this);


            //获取单据状态
            List<VoucherStatusModel> voucherStatusModels = LiSetReadOnlyForm.getEntitys(this.Name);
            if (voucherStatusModels.Count > 0)
            {
                voucherStatusModel = voucherStatusModels[0];
            }
            else
            {
                voucherStatusModel = new VoucherStatusModel();
                voucherStatusModel.code = this.Name;
                voucherStatusModel.name = this.Text;
                voucherStatusModel.dataStatuss = new List<StatusModel>();
            }
            setNewStatus();

            refControls = getEntitys_refControl();
            refDatas = getEntitys_refData();
        }

        public void InitGridlookUpEdit()
        {

            GridlookUpEditShowModel gridlookUpEditShowModelComboBox_ComboBox = refControls["sysDatabases"];
            DataTable liComboBoxData = refDatas["sysDatabases"];
            GridlookUpEditUtil.InitDefaultComboBoxControl("name", "name", gridlookUpEditShowModelComboBox_ComboBox.searchColumns, gridlookUpEditShowModelComboBox_ComboBox.displayColumns, gridLookUpEdit_DataBaseName, this, liComboBoxData);

            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.VALUE, systemInfoModel.getValueMember(), systemInfoModel.getDisplayMember(), systemInfoModel.getSearchColumns(), systemInfoModel.getDisplayColumns(), systemInfoModel.getDictModelDesc(), gridLookUpEdit_systemCode, this, systemInfoModel.getDataSource<List<SystemInfoModel>>());
        }

        public static Dictionary<string, DataTable> getEntitys_refData()
        {
            //string resultContent;
            //Dictionary<string, DataTable> refData = new Dictionary<string, DataTable>();
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "sysDatabases");

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQueryControlData("refControl"), paramModel, out resultContent))
            //{
            //    Newtonsoft.Json.Linq.JArray jadataref = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultContent);
            //    foreach (Newtonsoft.Json.Linq.JToken jt in jadataref)
            //    {
            //        refData.Add(Convert.ToString(jt["BasicInfoKey"]), DataUtil.DictionaryToTable(JsonUtil.GetDictionaryToList(Convert.ToString(jt["data"]))));
            //    }

            //}

            //return refData;
            return LiContexts.LiContext.getHttpEntity(LiEntityKey.SysDatabases, LiContext.SYSTEMCODE_DEFAULT).getRefControlDatas();
        }

        public static Dictionary<string, GridlookUpEditShowModel> getEntitys_refControl()
        {
            //string resultContent;
            //Dictionary<string, GridlookUpEditShowModel> refControl = new Dictionary<string, GridlookUpEditShowModel>();
            
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "sysDatabases");

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQueryControlInfo("refControl"), paramModel, out resultContent))
            //{
            //    Newtonsoft.Json.Linq.JArray jacontrolref = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultContent);
            //    foreach (Newtonsoft.Json.Linq.JToken jt in jacontrolref)
            //    {
            //        refControl.Add(Convert.ToString(jt["BasicInfoKey"]), JsonUtil.GetEntity<GridlookUpEditShowModel>(Convert.ToString(jt["data"])));
            //    }
            //}

            //return refControl;

            return LiContexts.LiContext.getHttpEntity(LiEntityKey.SysDatabases, LiContext.SYSTEMCODE_DEFAULT).getRefControls<GridlookUpEditShowModel>();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<TableModel>(kvp.Key, tableModel), kvp.Value);
            }

            gridControl1.DataSource = tableModel.datas;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<TableModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), tableModel);
            }

            tableModel.datas = gridControl1.DataSource as List<ColumnModel>;
        }

        /// <summary>
        /// /设置单据状态
        /// </summary>
        /// <param name="statusCode"></param>
        private void setFormStatus(string statusCode)
        {
            StatusModel statusModel = voucherStatusModel.dataStatuss.Where(m => m.code == statusCode).FirstOrDefault();
            if (statusModel != null)
            {
                foreach (ControlStatusModel controlStatusModel in statusModel.dataControlStatuss)
                {
                    if (buttonDict.ContainsKey(controlStatusModel.code))
                    {
                        BarButtonItem button = buttonDict[controlStatusModel.code];
                        button.Visibility = DevControlUtil.getBarItemVisibility(controlStatusModel.bVisibe);
                        button.Enabled = !controlStatusModel.bReadOnly;
                    }
                    if (controlDict.ContainsKey(controlStatusModel.code))
                    {
                        Control control = controlDict[controlStatusModel.code];
                        control.Visible = controlStatusModel.bVisibe;
                        control.Enabled = !controlStatusModel.bReadOnly;
                    }
                    if (gridColumnDict.ContainsKey(controlStatusModel.code))
                    {
                        GridColumn gridColumn = gridColumnDict[controlStatusModel.code];
                        gridColumn.Visible = controlStatusModel.bVisibe;
                        gridColumn.OptionsColumn.AllowEdit = !controlStatusModel.bReadOnly;
                    }
                }
            }
        }


        /// <summary>
        /// 获取服务端实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<TableModel> getEntitys(object key)
        {
            //string resultContent;
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "liTableInfo");
            //paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single("entityKey", key));

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            //{
            //    return JsonUtil.GetEntityToList<TableModel>(resultContent);
            //}

            //return null;
            return LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(key, "entityKey");
        }

        private void btnGetTableInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEdit_tableName.Text))
            {
                MessageUtil.Show("表名不能为空！", "温馨提示");
                return;
            }

            if (gridLookUpEdit_DataBaseName.EditValue == null)
            {
                MessageUtil.Show("数据库名不能为空！", "温馨提示");
                return;
            }

            string resultContent;
             
            Dictionary<string, object> paramDict = new Dictionary<string, object>();
            paramDict.Add("dataBaseName", gridLookUpEdit_DataBaseName.EditValue);
            paramDict.Add("tableName", textEdit_tableName.Text);
            //ProcedureParamModel paramModel = LiHttpProcedure.getProcedureParamModel("GetTableInfo", paramDict);

            DataTable dt = LiContexts.LiContext.getHttpEntity("GetTableInfo").execProcedure_DataTable(paramDict);
            if (LiContexts.LiContext.getHttpEntity("GetTableInfo").bSuccess)
            {
                //DataTable dt = DataUtil.DictionaryToTable(JsonUtil.GetDictionaryToList(resultContent));
                
                List<ColumnModel> listColumns;
                if (gridControl1.DataSource == null)
                {
                    gridControl1.DataSource = new List<ColumnModel>();
                }
                listColumns = (List<ColumnModel>)gridControl1.DataSource;
                listColumns.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    ColumnModel column = new ColumnModel();
                    column.columnName = Convert.ToString(dr["columnName"]);
                    column.columnType = Convert.ToString(dr["columnType"]);
                    column.length = Convert.ToInt32(dr["columnLength"]);
                    column.primaryKey = Convert.ToBoolean(dr["bPrimaryKey"]);
                    column.columnOrder = Convert.ToInt32(dr["columnOrder"]);
                    column.columnScale = Convert.ToInt32(dr["columnScale"]);
                    column.columnIsNull = Convert.ToBoolean(dr["bIsNull"]);
                    column.bDisplayColumn = false;
                    column.bSearchColumns = false;

                    listColumns.Add(column);
                }
                gridControl1.DataSource = listColumns;
                gridControl1.RefreshDataSource();

                textEdit_keyName.Text = getPrimaryKey(listColumns);
            }

        }

        public string getPrimaryKey(List<ColumnModel> listColumns)
        {
            ColumnModel columnModel = listColumns.Where(m => m.primaryKey == true).FirstOrDefault();
            if (columnModel != null)
            {
                return columnModel.columnName;
            }
            else
            {
                return string.Empty;
            }
        }

        private void btnGetOutInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
                        
            string filename = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Excel文件|*.xls;*.xlsx;*.csv|所有文件|*.*";
            openFileDialog1.Title = "请选择Excel文件";
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = false;
            openFileDialog1.DefaultExt = "所有文件|*.*";
            openFileDialog1.DefaultExt = "*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                if (filename == null || filename == "")
                {
                    MessageBox.Show("请选择导入文件！", "用友提示");
                    return;
                }
                DataTable dt = ExcelUtil.ExcelToDataTable(filename, "Sheet1", true);
                if (dt != null)
                {
                    List<ColumnModel> listColumns = (List<ColumnModel>)gridControl1.DataSource;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ColumnModel columnModel = listColumns.Where(m => m.columnName == Convert.ToString(dr["ColumnName"]).Trim()).FirstOrDefault();

                        if (columnModel != null )
                        {
                            columnModel.columnAbbName = Convert.ToString(dr["Description"]);
                        }
                    }
                    gridControl1.RefreshDataSource();
                }
            }
        }

        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            TableModel tableModel = TableModel.getInstanceByBasic();
            gridControl1.DataSource = tableModel.datas;

            //QueryParamModel queryParamModel = new QueryParamModel();
            //queryParamModel.type = "query";
            //queryParamModel.showAllColumn = true;
            //queryParamModel.option = "queryBy";
            //queryParamModel.entityKey = "form1";

            //string refdatajson = HttpUtil.Instance.post("http://192.168.0.110:8002/ormadmin/LiGetNewData/getNewModel", JsonUtil.GetJson(queryParamModel));
            //Newtonsoft.Json.Linq.JArray jaref = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(JsonUtil.GetValue(refdatajson, "string|data"));
            //foreach (Newtonsoft.Json.Linq.JToken jt in jaref)
            //{
            //    List<Dictionary<string, object>> dict = JsonUtil.GetDictionaryInList(jt["datas"].ToString());
            //    DataTable dt = DataTableUtil.getEmptyDataTable(dict[0]);

            //    dt.Rows.Clear();
            //    gridControl1.DataSource = dt;

            //}

            
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            //GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, "name", "name", ImageUtil.ImageListModel.getSearchColumns(), ImageUtil.ImageListModel.getDisplayColumns(), ImageUtil.ImageListModel.getDictModelDesc(), gridLookUpEdit_DataBaseName, this, ImageUtil.images);
            this.Close();
        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {

            bool bSuccess = false;

            string resultContent = string.Empty;

            getData();

            if (tableModel.id > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).updateEntity(tableModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).tipStr, "温馨提示");
                //if (LiContext.liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdate(), LiHttpUpdate.getUpdateParamModel("liTableInfo", tableModel), out resultContent))
                //{
                //    MessageUtil.Show("修改成功！", "温馨提示");
                //    bSuccess = true;
                //}
                //else
                //{
                //    MessageUtil.Show("修改失败！" + resultContent, "温馨提示");
                //}
            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).newEntity(tableModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).tipStr, "温馨提示");
                //if (LiContext.liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsert(), LiHttpInsert.getInsertParamModel("liTableInfo", tableModel), out resultContent))
                //{
                //    MessageUtil.Show("保存成功！", "温馨提示");
                //    bSuccess = true;

                //}
                //else
                //{
                //    MessageUtil.Show("保存失败！" + resultContent, "温馨提示");
                //}
            }


            getEntitys(textEdit_entityKey.Text);
            loadData();

            formStatusCode = "ShowStatus";
            setFormStatus(formStatusCode);
        }

        private void LiManageBasicInfoInOutDesin_Load(object sender, EventArgs e)
        {

            loadData();
            setFormStatus(formStatusCode);
        }

        private void btnModify_ItemClick(object sender, ItemClickEventArgs e)
        {

            loadData();
            setEditStatus();
            setFormStatus(formStatusCode);
        }

        private void btnStatus_ItemClick(object sender, ItemClickEventArgs e)
        {

            //设置新的控件状态
            foreach (KeyValuePair<string, GridColumn> kvp in gridColumnDict)
            {
                if (newControlStatusModels.Where(m => m.code == kvp.Key).FirstOrDefault() == null)
                {
                    newControlStatusModels.Add(new ControlStatusModel() { code = kvp.Key, name = kvp.Value.Caption, groupName = "表体", bReadOnly = false, bVisibe = true });
                }
            }
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                if (newControlStatusModels.Where(m => m.code == kvp.Key).FirstOrDefault() == null)
                {
                    LayoutControlItem layoutControlItem = layoutControlItemDict[kvp.Key];
                    newControlStatusModels.Add(new ControlStatusModel() { code = kvp.Key, name = layoutControlItem.Text, groupName = "表头", bReadOnly = false, bVisibe = true });
                }
            }
            foreach (KeyValuePair<string, BarButtonItem> kvp in buttonDict)
            {
                if (newControlStatusModels.Where(m => m.code == kvp.Key).FirstOrDefault() == null)
                {
                    newControlStatusModels.Add(new ControlStatusModel() { code = kvp.Key, name = kvp.Value.Caption, groupName = "按钮", bReadOnly = false, bVisibe = true });
                }
            }

            LiSetReadOnlyForm form = new LiSetReadOnlyForm(newControlStatusModels, null, null, null, voucherStatusModel);
            form.Show();
        }
    }
}