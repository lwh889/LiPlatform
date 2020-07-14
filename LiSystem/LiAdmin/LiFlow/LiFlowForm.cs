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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList;


using LiModel.Form;
using LiHttp.Server;
using LiHttp.RequestParam;
using LiCommon.Util;
using LiModel.LiEnum;
using LiFlow.Model;
using LiControl.Form;
using LiControl.Util;
using LiModel.Util;


using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;

using LiModel.Basic;
using LiFlow.Element.Flow;
using LiHttp.Enum;
using LiFlow.Element;
using LiFlow.Enums;
using LiFlow.LiForm;
using LiContexts;
using LiFlow.Util;

namespace LiFlow
{
    public partial class LiFlowForm : DevExpress.XtraBars.Ribbon.RibbonForm
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
        /// 表格控件
        /// </summary>
        private Dictionary<string, GridControl> gridControlDict = new Dictionary<string, GridControl>();

        /// <summary>
        /// 表格控件视图
        /// </summary>
        private Dictionary<string, GridView> gridViewDict = new Dictionary<string, GridView>();

        /// <summary>
        /// 表体列内容
        /// </summary>
        Dictionary<string, GridColumn> gridColumnDict = new Dictionary<string, GridColumn>();

        /// <summary>
        /// 
        /// 数据
        /// </summary>
        LiFlowModel formData;

        TableModel currentTableModel = null;

        /// <summary>
        /// 表单的状态
        /// </summary>
        public string formStatusCode { set; get; }

        public void setNewStatus()
        {
            formStatusCode = "NewStatus";
            setFormStatus(formStatusCode);
        }

        public void setEditStatus()
        {
            formStatusCode = "EditStatus";
            setFormStatus(formStatusCode);
        }

        public void setShowStatus()
        {
            formStatusCode = "ShowStatus";
            setFormStatus(formStatusCode);
        }

        private string formID = "LiFlowForm";

        #region 引用数据源
        private TableModel tableModel = new TableModel();
        #endregion

        Dictionary<ShapeBase, LiFlowNodeModel> liFlowNodeMap = new Dictionary<ShapeBase, LiFlowNodeModel>();

        Dictionary<string, ShapeBase> shapeBaseMap = new Dictionary<string, ShapeBase>();

        public LiFlowForm(LiFlowModel formData)
        {
            InitializeComponent();

            this.formData = formData;
            Init();
        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {
            Dictionary<string, object> whereDict = new Dictionary<string, object>();
            whereDict.Add("entityType", "Voucher");
            whereDict.Add("entityOrder", "master");
            tableModel.setDataSource(LiContexts.LiContext.getHttpEntity(LiEntityKey.TableInfo).getEntityList<TableModel>(whereDict));

            //读取Form上的控件
            DevFormUtil.getControlInForm(formID + ".", layoutControlItemDict, controlDict, this);
            DevFormUtil.getGridColumnInForm(formID + ".", gridColumnDict, this);
            DevFormUtil.getBarButtonItemInForm(formID + ".", buttonDict, this);
            DevFormUtil.getGridControlInForm(formID + ".", gridControlDict, gridViewDict, this);

            //获取单据状态
            List<VoucherStatusModel> voucherStatusModels = LiSetReadOnlyForm.getEntitys(this.Name);
            if (voucherStatusModels != null && voucherStatusModels.Count > 0)
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
        }

        public void InitControl()
        {

            GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowMode.VALUE, tableModel.getValueMember(), tableModel.getDisplayMember(), tableModel.getSearchColumns(), tableModel.getDisplayColumns(), tableModel.getDictModelDesc(), gridLookUpEdit_EntityKey, this, tableModel.getDataSource<List<TableModel>>());


            StartElement se = this.flowControlList1.AddShape(ShapeTypes.Start, new Point(10, 10),true) as StartElement;
            EndElement ee = this.flowControlList1.AddShape(ShapeTypes.End, new Point(10, 63), true) as EndElement;
            NodeElement ne = this.flowControlList1.AddShape(ShapeTypes.Node, new Point(10, 116), true) as NodeElement;
            ConditionElement ce = this.flowControlList1.AddShape(ShapeTypes.Condition, new Point(10, 169), true) as ConditionElement;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            try
            {

                foreach (KeyValuePair<string, Control> kvp in controlDict)
                {
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<LiFlowModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
                {
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<LiFlowModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (LiFlowNodeModel liFlowNodeModel in formData.nodes)
                {
                    Point p =  new Point() { X = liFlowNodeModel.X, Y = liFlowNodeModel.Y };
                    ShapeBase shapeBase = FlowUtil.NewShape(liFlowNodeModel.flowNodeType, this.graphControl1);
                    shapeBase.Height = liFlowNodeModel.height;
                    shapeBase.Width = liFlowNodeModel.width;
                    shapeBase.Text = liFlowNodeModel.flowNodeName;

                    addFlowNode(shapeBase, liFlowNodeModel);
                    this.graphControl1.AddShape(shapeBase, p);
                }

                foreach (KeyValuePair<ShapeBase, LiFlowNodeModel> kvp in liFlowNodeMap)
                {
                    if (kvp.Value.connectors.Count > 0)
                    {
                        foreach (LiFlowConnectorModel liFlowConnectorModel in kvp.Value.connectors)
                        {
                            ShapeBase shapeBaseTo = shapeBaseMap[liFlowConnectorModel.flowNodeCodeTo];
                            this.graphControl1.AddConnection(kvp.Key.Connectors[liFlowConnectorModel.flowNodeFormIndex], shapeBaseTo.Connectors[liFlowConnectorModel.flowNodeToIndex]);
                        }
                    }
                }

                this.graphControl1.Refresh();
            }
            catch (Exception ex)
            {
                MessageUtil.Show("加载失败：" + ex.ToString(), "系统提示");
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        public void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<LiFlowModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }


            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<LiFlowModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

            foreach (KeyValuePair<ShapeBase, LiFlowNodeModel> kvp in liFlowNodeMap)
            {
                kvp.Value.X = kvp.Key.X;
                kvp.Value.Y = kvp.Key.Y;
                kvp.Value.width = kvp.Key.Width;
                kvp.Value.height = kvp.Key.Height;
            }
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


        private void graphControl1_OnShowProps(object ent)
        {
            this.propertyGridControl1.SelectedObject = ent;
        }

        private void LiFlowForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void flowControlList1_DoubleClick(object sender, EventArgs e)
        {
            ShapeBase shapeBase = this.flowControlList1.getFoucsedEntity();
            ShapeBase newShapeBase = FlowUtil.NewShape(shapeBase.GetType().Name, this.graphControl1);
            AddShape(newShapeBase, new Point(10, 10));
        }


        public ShapeBase AddShape(ShapeBase shapeBase, Point p)
        {
            return this.graphControl1.AddShape(shapeBase, p);
        }

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="ent"></param>
        private void graphControl1_OnLiDoubleClick(object ent)
        {
            LiFlowNodeSetForm liFlowNodeSetForm = null;
            ShapeBase shape = graphControl1.getFoucsedEntity();
            if (shape == null) return;

            LiFlowNodeModel liFlowNodeModel = liFlowNodeMap[shape];
            updateModelByShape(shape, liFlowNodeModel);

            switch (ent.GetType().Name)
            {
                case "Proxy":
                    LiFlowSetForm liFlowSetForm = new LiFlowSetForm();
                    liFlowSetForm.ShowDialog();
                    break;
                case "StartElement":
                case "ConditionElement":
                case "NodeElement":
                case "EndElement":
                    if (currentTableModel == null)
                    {
                        MessageUtil.Show("请选择单据类型！", "温馨提示");
                        return;
                    }
                    liFlowNodeSetForm = new LiFlowNodeSetForm(liFlowNodeModel,currentTableModel);
                    liFlowNodeSetForm.ShowDialog();
                    break;
            }
            updateShapeBaseByModel(shape, liFlowNodeModel);
        }

        /// <summary>
        /// 更新到形状
        /// </summary>
        /// <param name="shapeBase"></param>
        /// <param name="liFlowNodeModel"></param>
        public void updateShapeBaseByModel(ShapeBase shapeBase, LiFlowNodeModel liFlowNodeModel)
        {
            shapeBase.Height = liFlowNodeModel.height;
            shapeBase.Width = liFlowNodeModel.width;

            if(liFlowNodeModel.X != shapeBase.X)
                shapeBase.X = liFlowNodeModel.X;
            if (liFlowNodeModel.Y != shapeBase.Y)
                shapeBase.Y = liFlowNodeModel.Y;

            shapeBase.Text = liFlowNodeModel.flowNodeName;

        }

        /// <summary>
        /// 形状更新到对象
        /// </summary>
        /// <param name="shapeBase"></param>
        /// <param name="liFlowNodeModel"></param>
        public void updateModelByShape(ShapeBase shapeBase, LiFlowNodeModel liFlowNodeModel)
        {
            liFlowNodeModel.height = shapeBase.Height;
            liFlowNodeModel.width = shapeBase.Width ;
            liFlowNodeModel.X = shapeBase.X;
            liFlowNodeModel.Y = shapeBase.Y;
            liFlowNodeModel.flowNodeName = shapeBase.Text;
        }

        private void graphControl1_OnAddNode(object ent)
        {
            ShapeBase shapeBase = ent as ShapeBase;

            if (!liFlowNodeMap.ContainsKey(shapeBase))
            {
                LiFlowNodeModel liFlowNodeModel = LiFlowNodeModel.getInstance();
                liFlowNodeModel.flowNodeName = shapeBase.Text;
                liFlowNodeModel.flowNodeName = shapeBase.Text;
                liFlowNodeModel.height = shapeBase.Height;
                liFlowNodeModel.width = shapeBase.Width;
                liFlowNodeModel.X = shapeBase.X;
                liFlowNodeModel.Y = shapeBase.Y;

                switch (ent.GetType().Name)
                {
                    case FlowNodeType.STARTELEMENT:
                        liFlowNodeModel.flowNodeCode = "startElementCode1";
                        liFlowNodeModel.flowNodeType = FlowNodeType.STARTELEMENT;
                        break;
                    case FlowNodeType.NODEELEMENT:
                        liFlowNodeModel.flowNodeCode = "NodeElementCode1";
                        liFlowNodeModel.flowNodeType = FlowNodeType.NODEELEMENT;
                        break;
                    case FlowNodeType.CONDITIONELEMENT:
                        liFlowNodeModel.flowNodeCode = "ConditionElementCode1";
                        liFlowNodeModel.flowNodeType = FlowNodeType.CONDITIONELEMENT;
                        break;
                    case FlowNodeType.ENDELEMENT:
                        liFlowNodeModel.flowNodeCode = "EndElementCode1";
                        liFlowNodeModel.flowNodeType = FlowNodeType.ENDELEMENT;
                        break;
                }

                addFlowNode(shapeBase, liFlowNodeModel);
            }

        }

        private void graphControl1_OnAttactConnector(int shapeBaseFormIndex, int shapeBaseToIndex, ShapeBase shapeBaseForm, ShapeBase shapeBaseTo)
        {
            addConnector(shapeBaseFormIndex, shapeBaseToIndex, shapeBaseForm, shapeBaseTo);
        }

        private void graphControl1_OnRemoveConnection(Connection connection)
        {
            ShapeBase shapeBaseTo = null;
            if (connection.To.AttachedTo != null)
            {
                shapeBaseTo = connection.To.AttachedTo.ShapeBase;
            }
            removeConnector(connection.ShapeBaseFrom, shapeBaseTo);
        }

        private void graphControl1_OnRemoveConnector(ShapeBase shapeBaseForm, ShapeBase shapeBaseTo)
        {
            removeConnector(shapeBaseForm, shapeBaseTo);
        }

        private void graphControl1_OnRemoveNode(object ent)
        {
            removeFlowNode((ShapeBase)ent);
        }


        private void addConnector(int shapeBaseFormIndex, int shapeBaseToIndex, ShapeBase shapeBaseForm, ShapeBase shapeBaseTo)
        {
            if (shapeBaseForm == null || shapeBaseTo == null) return;

            LiFlowNodeModel liFlowNodeModelTo = liFlowNodeMap[shapeBaseTo];
            LiFlowNodeModel liFlowNodeModelForm = liFlowNodeMap[shapeBaseForm];
            List<LiFlowConnectorModel> liFlowConnectorModelList = liFlowNodeModelForm.connectors;

            foreach (LiFlowConnectorModel liFlowConnector in liFlowConnectorModelList)
            {
                if (liFlowNodeModelTo.flowNodeCode == liFlowConnector.flowNodeCodeTo)
                {
                    return;
                }

            }

            LiFlowConnectorModel liFlowConnectorModel = LiFlowConnectorModel.getInstance();
            liFlowConnectorModel.flowNodeCodeTo = liFlowNodeModelTo.flowNodeCode;
            liFlowConnectorModel.flowNodeNameTo = liFlowNodeModelTo.flowNodeName;
            liFlowConnectorModel.flowNodeFormIndex = shapeBaseFormIndex;
            liFlowConnectorModel.flowNodeToIndex = shapeBaseToIndex;

            liFlowConnectorModel.flowNodeId = liFlowNodeModelForm.id;

            liFlowConnectorModelList.Add(liFlowConnectorModel);
        }

        private void removeConnector(ShapeBase shapeBaseForm, ShapeBase shapeBaseTo)
        {
            if (shapeBaseForm ==null || shapeBaseTo == null) return;

            LiFlowNodeModel liFlowNodeModelTo = liFlowNodeMap[shapeBaseTo];
            LiFlowNodeModel liFlowNodeModelForm = liFlowNodeMap[shapeBaseForm];
            List<LiFlowConnectorModel> liFlowConnectorModelList = liFlowNodeModelForm.connectors;

            LiFlowConnectorModel liFlowConnectorDelete = null;
            foreach (LiFlowConnectorModel liFlowConnector in liFlowConnectorModelList)
            {
                if (liFlowNodeModelTo.flowNodeCode == liFlowConnector.flowNodeCodeTo)
                {
                    liFlowConnectorDelete = liFlowConnector;
                    break;
                }

            }

            liFlowConnectorModelList.Remove(liFlowConnectorDelete);
        }

        private void addFlowNode(ShapeBase shapeBase, LiFlowNodeModel liFlowNodeModel)
        {
            if (!liFlowNodeMap.ContainsKey(shapeBase))
            {
                liFlowNodeMap.Add(shapeBase, liFlowNodeModel);
            }
            if (!shapeBaseMap.ContainsKey(liFlowNodeModel.flowNodeCode))
            {
                shapeBaseMap.Add(liFlowNodeModel.flowNodeCode, shapeBase);
            }

            if (!formData.nodes.Contains(liFlowNodeModel))
            {
                formData.nodes.Add(liFlowNodeModel);
            }
        }


        private void removeFlowNode(ShapeBase shapeBase)
        {
            if (liFlowNodeMap.ContainsKey(shapeBase))
            {
                formData.nodes.Remove(liFlowNodeMap[shapeBase]);
                shapeBaseMap.Remove(liFlowNodeMap[shapeBase].flowNodeCode);
            }

            liFlowNodeMap.Remove(shapeBase);
        }

        private void btnNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            formData = LiFlowModel.getInstance();

            graphControl1.Clear();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            getData();

            if(formData.id > 0){
                LiContexts.LiContext.getHttpEntity(LiEntityKey.LiFlow, LiContext.SystemCode).saveEntity(true, formData);
            }
            else
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.LiFlow, LiContext.SystemCode).saveEntity(false, formData);
            }

            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.LiFlow, LiContext.SystemCode).tipStr, "温馨提示");

        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
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

        private void gridLookUpEdit_EntityKey_EditValueChanged(object sender, EventArgs e)
        {
            currentTableModel = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == Convert.ToString(gridLookUpEdit_EntityKey.EditValue)).FirstOrDefault();
            if (currentTableModel != null)
            {
                textEdit_EntityName.Text = currentTableModel.entityName;
                FormModel formModel = LiContext.getFormModel(Convert.ToString(gridLookUpEdit_EntityKey.EditValue), LiContext.SystemCode);
                //FormModel formModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).getEntitySingle<FormModel>(Convert.ToString(gridLookUpEdit_EntityKey.EditValue),"name");

                FieldModel.InitDataSource(formModel);
            }

        }

        private void btnRelease_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LiVersionFlowModel liVersionFlowModel = new LiVersionFlowModel();
            liVersionFlowModel.flowVersionNumber = string.Format("{0}_{1}", formData.entityKey, DateTime.Now.ToString("yyyyMMddHHmmss"));
            liVersionFlowModel.flowCode =formData.flowCode;
            liVersionFlowModel.flowName = formData.flowName;
            liVersionFlowModel.entityKey = formData.entityKey;
            liVersionFlowModel.entityName = formData.entityName;
            liVersionFlowModel.bDefault = formData.bDefault;
            liVersionFlowModel.flowVersionDate = DateTime.Now;
            liVersionFlowModel.nodes = new List<LiVersionFlowNodeModel>();

            foreach (LiFlowNodeModel liFlowNodeModel in formData.nodes)
            {
                LiVersionFlowNodeModel liVersionFlowNodeModel = new LiVersionFlowNodeModel();
                liVersionFlowNodeModel.flowNodeCode = liFlowNodeModel.flowNodeCode;
                liVersionFlowNodeModel.flowNodeName = liFlowNodeModel.flowNodeName;
                liVersionFlowNodeModel.flowNodeType = liFlowNodeModel.flowNodeType;
                liVersionFlowNodeModel.flowNodeInformation = liFlowNodeModel.flowNodeInformation;
                liVersionFlowNodeModel.X = liFlowNodeModel.X;
                liVersionFlowNodeModel.Y = liFlowNodeModel.Y;
                liVersionFlowNodeModel.width = liFlowNodeModel.width;
                liVersionFlowNodeModel.height = liFlowNodeModel.height;
                liVersionFlowNodeModel.Users = new List<LiVersionFlowUserModel>();
                liVersionFlowNodeModel.connectors = new List<LiVersionFlowConnectorModel>();

                foreach (LiFlowUserModel liFlowUserModel in liFlowNodeModel.users)
                {
                    LiVersionFlowUserModel liVersionFlowUserModel = new LiVersionFlowUserModel();
                    liVersionFlowUserModel.userCode = liFlowUserModel.userCode;
                    liVersionFlowUserModel.userName = liFlowUserModel.userName;
                    liVersionFlowNodeModel.Users.Add(liVersionFlowUserModel);
                }

                foreach (LiFlowConnectorModel liFlowConnectorModel in liFlowNodeModel.connectors)
                {
                    LiVersionFlowConnectorModel liVersionFlowConnectorModel = new LiVersionFlowConnectorModel();
                    liVersionFlowConnectorModel.flowNodeCodeTo = liFlowConnectorModel.flowNodeCodeTo;
                    liVersionFlowConnectorModel.flowNodeNameTo = liFlowConnectorModel.flowNodeNameTo;
                    liVersionFlowConnectorModel.flowNodeToIndex = liFlowConnectorModel.flowNodeToIndex;
                    liVersionFlowConnectorModel.flowNodeFormIndex= liFlowConnectorModel.flowNodeFormIndex;
                    liVersionFlowConnectorModel.conditions = new List<LiVersionFlowConditionModel>();

                    foreach (LiFlowConditionModel liFlowConditionModel in liFlowConnectorModel.conditions)
                    {
                        LiVersionFlowConditionModel liVersionFlowConditionModel = new LiVersionFlowConditionModel();
                        liVersionFlowConditionModel.sBracketsBefore = liFlowConditionModel.sBracketsBefore;
                        liVersionFlowConditionModel.sFieldName = liFlowConditionModel.sFieldName;
                        liVersionFlowConditionModel.sJudgmentSymbol = liFlowConditionModel.sJudgmentSymbol;
                        liVersionFlowConditionModel.oQueryValue = liFlowConditionModel.oQueryValue;
                        liVersionFlowConditionModel.sJoin = liFlowConditionModel.sJoin;
                        liVersionFlowConditionModel.sBracketsAfter = liFlowConditionModel.sBracketsAfter;
                        liVersionFlowConditionModel.sFieldType = liFlowConditionModel.sFieldType;
                        liVersionFlowConditionModel.sBasicCode = liFlowConditionModel.sBasicCode;
                        liVersionFlowConnectorModel.conditions.Add(liVersionFlowConditionModel);
                    }
                    liVersionFlowNodeModel.connectors.Add(liVersionFlowConnectorModel);
                }

                liVersionFlowModel.nodes.Add(liVersionFlowNodeModel);
            }

            LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).saveEntity(false, liVersionFlowModel);
            MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).tipStr, "温馨提示");
        }
    }
}