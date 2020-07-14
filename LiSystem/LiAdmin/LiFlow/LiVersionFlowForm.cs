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
using LiHttp.Enum;
using LiContexts;
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
using LiFlow.Element.Shapes;
using LiFlow.Element;
using LiFlow.Enums;
using LiFlow.LiForm;
using LiHttp.GetEntity;


namespace LiFlow
{
    public partial class LiVersionFlowForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 状态

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


        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        /// <summary>
        /// 单据控件状态，用于更新
        /// </summary>
        private List<ControlStatusModel> newControlStatusModels = new List<ControlStatusModel>();
        #endregion

        #region 表单控件信息
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

        #endregion

        /// <summary>
        /// 
        /// 数据
        /// </summary>
        LiVersionFlowModel formData;

        TableModel currentTableModel = null;

        private string formID = "LiVersionFlowForm";

        #region 引用数据源
        private TableModel tableModel = new TableModel();
        #endregion

        Dictionary<ShapeBase, LiVersionFlowNodeModel> liFlowNodeMap = new Dictionary<ShapeBase, LiVersionFlowNodeModel>();

        Dictionary<string, ShapeBase> shapeBaseMap = new Dictionary<string, ShapeBase>();

        public LiVersionFlowForm(LiVersionFlowModel formData)
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
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<LiVersionFlowModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
                {
                    DevControlUtil.setContorlData(LiModel.Util.ModelUtil.getModelValue<LiVersionFlowModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (LiVersionFlowNodeModel liFlowNodeModel in formData.nodes)
                {
                    Point p =  new Point() { X = liFlowNodeModel.X, Y = liFlowNodeModel.Y };
                    ShapeBase shapeBase = NewShape(liFlowNodeModel.flowNodeType);
                    shapeBase.Height = liFlowNodeModel.height;
                    shapeBase.Width = liFlowNodeModel.width;
                    shapeBase.Text = liFlowNodeModel.flowNodeName;

                    addFlowNode(shapeBase, liFlowNodeModel);
                    this.graphControl1.AddShape(shapeBase, p);
                }

                foreach (KeyValuePair<ShapeBase, LiVersionFlowNodeModel> kvp in liFlowNodeMap)
                {
                    if (kvp.Value.connectors.Count > 0)
                    {
                        foreach (LiVersionFlowConnectorModel liFlowConnectorModel in kvp.Value.connectors)
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
                LiModel.Util.ModelUtil.setModelValue<LiVersionFlowModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }


            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                LiModel.Util.ModelUtil.setModelValue<LiVersionFlowModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

            foreach (KeyValuePair<ShapeBase, LiVersionFlowNodeModel> kvp in liFlowNodeMap)
            {
                kvp.Value.X = kvp.Key.X;
                kvp.Value.Y = kvp.Key.Y;
                kvp.Value.width = kvp.Key.Width;
                kvp.Value.height = kvp.Key.Height;
            }
        }

        private void addFlowNode(ShapeBase shapeBase, LiVersionFlowNodeModel liFlowNodeModel)
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

        public ShapeBase NewShape(string shapeTypeStr)
        {
            ShapeTypes shapeType = ShapeTypes.Node;
            switch (shapeTypeStr)
            {
                case "StartElement":
                    shapeType = ShapeTypes.Start;
                    break;
                case "EndElement":
                    shapeType = ShapeTypes.End;
                    break;
                case "NodeElement":
                    shapeType = ShapeTypes.Node;
                    break;
                case "ConditionElement":
                    shapeType = ShapeTypes.Condition;
                    break;
            }
            return this.graphControl1.NewShape(shapeType);
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
        }

        private void LiVersionFlowForm_Load(object sender, EventArgs e)
        {
            loadData();
        }


        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="ent"></param>
        private void graphControl1_OnLiDoubleClick(object ent)
        {
            LiVersionFlowNodeSetForm liFlowNodeSetForm = null;
            ShapeBase shape = graphControl1.getFoucsedEntity();
            if (shape == null) return;

            LiVersionFlowNodeModel liFlowNodeModel = liFlowNodeMap[shape];

            switch (ent.GetType().Name)
            {
                case "StartElement":
                case "ConditionElement":
                case "NodeElement":
                case "EndElement":
                    if (currentTableModel == null)
                    {
                        MessageUtil.Show("请选择单据类型！", "温馨提示");
                        return;
                    }
                    liFlowNodeSetForm = new LiVersionFlowNodeSetForm(liFlowNodeModel,currentTableModel);
                    liFlowNodeSetForm.ShowDialog();
                    break;
            }
        }


        private void graphControl1_OnAddNode(object ent)
        {

        }

        private void graphControl1_OnAttactConnector(int shapeBaseFormIndex, int shapeBaseToIndex, ShapeBase shapeBaseForm, ShapeBase shapeBaseTo)
        {
        }

        private void graphControl1_OnRemoveConnection(Connection connection)
        {
        }

        private void graphControl1_OnRemoveConnector(ShapeBase shapeBaseForm, ShapeBase shapeBaseTo)
        {
        }

        private void graphControl1_OnRemoveNode(object ent)
        {
        }

        private void gridLookUpEdit_EntityKey_EditValueChanged(object sender, EventArgs e)
        {
            currentTableModel = tableModel.getDataSource<List<TableModel>>().Where(m => m.entityKey == Convert.ToString(gridLookUpEdit_EntityKey.EditValue)).FirstOrDefault();
            if (currentTableModel != null)
            {
                textEdit_EntityName.Text = currentTableModel.entityName;
            }
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

        private void btnOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            formData.openStatus = OpenStatus.OPEN;


            LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).updateEntity(formData);
            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).bSuccess)
            {
                MessageUtil.Show("已开启", "温馨提示");
            }
            else
            {
                MessageUtil.Show("开启失败：" + LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).resultContent, "温馨提示");
            }
        }

        private void btnForbid_ItemClick(object sender, ItemClickEventArgs e)
        {
            formData.openStatus = OpenStatus.PROHIBIT;


            LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).updateEntity(formData);
            if (LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).bSuccess)
            {
                MessageUtil.Show("已禁止", "温馨提示");
            }
            else
            {
                MessageUtil.Show("禁止失败：" + LiContexts.LiContext.getHttpEntity(LiEntityKey.LiVersionFlow, LiContext.SystemCode).resultContent, "温馨提示");
            }
        }



    }
}