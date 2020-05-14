using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using LiFlow.Model;
using LiFlow.Element;
using LiFlow.Util;
using LiCommon.Util;

namespace LiFlow
{
    public partial class LiShowFlowForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 
        /// 数据
        /// </summary>
        LiVersionFlowModel liVersionFlowModel;

        LiVoucherFlowModel liVoucherFlowModel;

        Dictionary<string, ShapeBase> shapeBaseMap = new Dictionary<string, ShapeBase>();

        Dictionary<ShapeBase, LiVersionFlowNodeModel> liFlowNodeMap = new Dictionary<ShapeBase, LiVersionFlowNodeModel>();

        public LiShowFlowForm(LiVersionFlowModel liVersionFlowModel, LiVoucherFlowModel liVoucherFlowModel)
        {
            InitializeComponent();

            this.liVersionFlowModel = liVersionFlowModel;
            this.liVoucherFlowModel = liVoucherFlowModel;

            Init();
        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {

        }

        public void InitControl()
        {

        }

        public void loadDate()
        {
            try
            {

                foreach (LiVersionFlowNodeModel liFlowNodeModel in liVersionFlowModel.nodes)
                {
                    Point p = new Point() { X = liFlowNodeModel.X, Y = liFlowNodeModel.Y };
                    ShapeBase shapeBase = FlowUtil.NewShape(liFlowNodeModel.flowNodeType, this.graphControl1);
                    shapeBase.Height = liFlowNodeModel.height;
                    shapeBase.Width = liFlowNodeModel.width;
                    shapeBase.Text = liFlowNodeModel.flowNodeName;

                    LiVoucherFlowStepModel liVoucherFlowStepModel = liVoucherFlowModel.datas.Where(m => m.flowVersionNodeId == liFlowNodeModel.id).FirstOrDefault();
                    if (liVoucherFlowStepModel != null)
                    {
                        shapeBase.ShapeColor = Color.Green;
                    }

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
            }
            catch (Exception ex)
            {
                MessageUtil.Show(ex.ToString());
            }

            this.graphControl1.Refresh();
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
        }

        private void LiShowFlowForm_Load(object sender, EventArgs e)
        {
            loadDate();
        }

    }
}