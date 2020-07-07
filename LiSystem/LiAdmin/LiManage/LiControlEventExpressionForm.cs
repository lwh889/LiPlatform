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
using LiModel.Form;
using LiControl.Util;
using DevExpress.XtraBars;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using LiModel.Util;
using LiModel.Basic;
using LiCommon.Util;

namespace LiManage
{
    public partial class LiControlEventExpressionForm : DevExpress.XtraEditors.XtraForm
    {
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

        #region 引用数据源
        private ControlEventTypeModel controlEventTypeModel = new ControlEventTypeModel();
        #endregion

        private string formID = "LiControlEventExpressionForm";

        private DataTable controlDt;
        private ControlEventModel formData;
        private FormModel formModel;
        public LiControlEventExpressionForm( ControlEventModel controlEvent, FormModel formModel)
        {
            InitializeComponent();

            this.formData = controlEvent;
            this.formModel = formModel;

            Init();
        }

        private void Init()
        {
            InitData();
            InitControl();
        }

        private void InitData()
        {
            //读取Form上的控件
            DevFormUtil.getControlInForm(formID + ".", layoutControlItemDict, controlDict, this);
            DevFormUtil.getGridColumnInForm(formID + ".", gridColumnDict, this);
            DevFormUtil.getBarButtonItemInForm(formID + ".", buttonDict, this);
            DevFormUtil.getGridControlInForm(formID + ".", gridControlDict, gridViewDict, this);

            controlDt = new DataTable();
            controlDt.Columns.Add(new DataColumn("entityType"));
            controlDt.Columns.Add(new DataColumn("fieldCode"));
            controlDt.Columns.Add(new DataColumn("fieldName"));

            foreach(PanelModel panel in formModel.panels)
            {
                foreach(ControlGroupModel controlGroup in panel.controlGroups)
                {
                    foreach(ControlModel control in controlGroup.controls)
                    {
                        DataRow dr = controlDt.NewRow();
                        dr["entityType"] = panel.tableName;
                        dr["fieldCode"] = control.name;
                        dr["fieldName"] = control.text;
                        controlDt.Rows.Add(dr);
                    }
                }
            }
        }
        private void InitControl()
        {
            GridlookUpEditUtil.InitDefaultComboBoxControl(controlEventTypeModel.getValueMember(), controlEventTypeModel.getDisplayMember(), controlEventTypeModel.getSearchColumns(), controlEventTypeModel.getDisplayColumns(), gridLookUpEdit_eventType, this, controlEventTypeModel.getDataSource());

            gridControl1.DataSource = controlDt;
        }

        private void loadData()
        {
            try
            {
                foreach (KeyValuePair<string, Control> kvp in controlDict)
                {
                    DevControlUtil.setContorlData(ModelUtil.getModelValue<ControlEventModel>(kvp.Key, formData), kvp.Value);
                }

                foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
                {
                    DevControlUtil.setContorlData(ModelUtil.getModelValue<ControlEventModel>(kvp.Key, formData), kvp.Value);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.Show("加载失败：" + ex.ToString(), "系统提示");
            }

        }

        private void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                ModelUtil.setModelValue<ControlEventModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }

            foreach (KeyValuePair<string, GridControl> kvp in gridControlDict)
            {
                ModelUtil.setModelValue<ControlEventModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), formData);
            }
        }

        private void BtnReturnData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            getData();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void BtnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void GridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr == null) return;

            string eventExpressionStr =Convert.ToString(memoEdit_eventExpression.EditValue);
            string newStr = string.Format("[{0}:{1}]",dr["entityType"],dr["fieldCode"]) ;
            int SelectionStart = memoEdit_eventExpression.SelectionStart;
            eventExpressionStr = eventExpressionStr.Insert(SelectionStart, newStr);

            memoEdit_eventExpression.EditValue = eventExpressionStr;
            memoEdit_eventExpression.SelectionStart = SelectionStart + newStr.Length;
            memoEdit_eventExpression.Focus();


        }

        private void LiControlEventExpressionForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}