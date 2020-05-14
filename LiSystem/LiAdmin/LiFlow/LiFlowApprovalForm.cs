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

using LiFlow.Enums;
using LiFlow.Util;
using LiFlow.Model;
using LiModel.Form;
using LiCommon.Util;

namespace LiFlow
{
    public partial class LiFlowApprovalForm : DevExpress.XtraEditors.XtraForm
    {
        private string _approvalType;

        /// <summary>
        /// 审批类型
        /// </summary>
        public string approvalType { set { _approvalType = value; } get { return _approvalType; } }

        private string _resultContent;

        /// <summary>
        /// 审批结果信息
        /// </summary>
        public string resultContent { set { _resultContent = value; } get { return _resultContent; } }

        private bool _bSuccess;

        /// <summary>
        /// 审批是否成功
        /// </summary>
        public bool bSuccess { set { _bSuccess = value; } get { return _bSuccess; } }

        /// <summary>
        /// 单据Key
        /// </summary>
        string entityKey;

        /// <summary>
        /// 单据ID
        /// </summary>
        string voucherId;

        /// <summary>
        /// 单据编码
        /// </summary>
        string voucherCode;

        /// <summary>
        /// 单据主表名
        /// </summary>
        string mainTableName;

        /// <summary>
        /// 单据数据
        /// </summary>
        Dictionary<string, object> voucherDataDict;

        /// <summary>
        /// 单据信息
        /// </summary>
        FormModel formModel;

        /// <summary>
        /// 当前审批节点
        /// </summary>
        LiVersionFlowNodeModel currentFlowNode = null;

        /// <summary>
        /// 最新的已审批节点
        /// </summary>
        LiVoucherFlowStepModel liVoucherFlowStepModel = null;

        /// <summary>
        /// 已运行的单据流程
        /// </summary>
        LiVoucherFlowModel liVoucherFlowTemp = null;

        /// <summary>
        /// 版本流程
        /// </summary>
        LiVersionFlowModel liVersionFlowModel = null;

        public LiFlowApprovalForm(string entityKey, string voucherId, string voucherCode, string mainTableName, Dictionary<string, object> voucherDataDict, FormModel formModel)
        {
            InitializeComponent();

            this.entityKey = entityKey;
            this.voucherId = voucherId;
            this.voucherCode = voucherCode;
            this.mainTableName = mainTableName;
            this.voucherDataDict = voucherDataDict;
            this.formModel = formModel;

            Init();
        }

        public void Init()
        {
            InitData();
            InitControl();
        }

        public void InitData()
        {
            getCurrentFlowNode();
        }

        private void getCurrentFlowNode()
        {
            currentFlowNode = FlowUtil.getNextStepFlow(entityKey, voucherId, out liVoucherFlowStepModel, out liVoucherFlowTemp, out liVersionFlowModel);
            if (currentFlowNode == null)
            {
                MessageUtil.Show("找不对应的流程！", "温馨提示");
            }
        }

        public void InitControl()
        {
            if (currentFlowNode != null)
            {
                this.Text = string.Format("当前流程节点【{0}】", currentFlowNode.flowNodeName); 
            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            string content = string.IsNullOrWhiteSpace(this.memoEdit1.Text) ? "同意！" : this.memoEdit1.Text;

            getCurrentFlowNode();

            bSuccess  = FlowUtil.execFlow(content, entityKey, voucherId, voucherCode, mainTableName, formModel, currentFlowNode, liVoucherFlowStepModel, liVoucherFlowTemp,liVersionFlowModel,voucherDataDict, out _resultContent);

            approvalType = ApprovalType.Agree;

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.memoEdit1.Text))
            {
                MessageUtil.Show("请填写审批意见！", "温馨提示");
                return;
            }


            bSuccess = FlowUtil.revokeFlow(this.memoEdit1.Text, RevokeType.Disagree, entityKey, voucherId, liVoucherFlowStepModel, out _resultContent);

            approvalType = ApprovalType.Disagree;

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}