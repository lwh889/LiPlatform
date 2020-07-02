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
using LiModel.LiConvert;
using LiCommon.Util;
using LiModel.LiEnum;
using LiControl.Util;
using LiModel.Basic;
using LiForm.Dev.Util;
using DevExpress.XtraGrid.Columns;

namespace LiForm.Dev
{
    public partial class LiRefQuantityForm : DevExpress.XtraEditors.XtraForm
    {
        private List<DataRow> _SelectDataRows = new List<DataRow>();

        public List<DataRow> SelectDataRows { set { _SelectDataRows = value; } get { return _SelectDataRows; } }
        /// <summary>
        /// 表格列
        /// </summary>
        private Dictionary<string, GridColumn> _liGridColumnDict = new Dictionary<string, GridColumn>();

        /// <summary>
        /// 列表列
        /// </summary>
        public Dictionary<string, GridColumn> liGridColumnDict { set { _liGridColumnDict = value; } get { return _liGridColumnDict; } }


        private LiConvertHeadModel _LiConvertHeadModel;

        /// <summary>
        /// 返回值
        /// </summary>
        public LiConvertHeadModel liConvertHeadModel { get { return _LiConvertHeadModel; } set { _LiConvertHeadModel = value; } }

        private List<DataRow> _convertDatas;

        /// <summary>
        /// 转换数据
        /// </summary>
        public List<DataRow> convertDatas { get { return _convertDatas; } set { _convertDatas = value; } }

        private List<TableModel> _tableModelList;
        /// <summary>
        /// 转换数据
        /// </summary>
        public List<TableModel> tableModelList { get { return _tableModelList; } set { _tableModelList = value; } }


        public LiRefQuantityForm(LiConvertHeadModel liConvertHeadModel, List<TableModel> tableModelList, List<DataRow> convertDatas)
        {
            InitializeComponent();

            this.liConvertHeadModel = liConvertHeadModel;
            this.tableModelList = tableModelList;
            this.convertDatas = convertDatas;

            Init();
        }

        private void Init()
        {
            InitData();
            InitControl();
            InitView();
        }

        private void InitData()
        {

        }

        private void InitControl()
        {
            InitGridColumn();
        }

        public void InitGridColumn()
        {
            ListFormUtil.getGridColumn(tableModelList, liGridColumnDict, ShowGridColumnMode.ShowBody, false);
        }
        public void InitView()
        {
            resetGridControl();
        }
        public void resetGridControl()
        {
            gridView1.Columns.Clear();

            foreach(GridColumn gc in liGridColumnDict.Values)
            {
                gc.VisibleIndex += 2;
            }

            GridColumn gridColumn = liGridColumnDict[ListFormUtil.getFieldNameFormat(liConvertHeadModel.convertPushTableName, liConvertHeadModel.convertPushField)];
            gridColumn.VisibleIndex = 0;

            GridColumn gridColumnPushQty = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumnPushQty.Caption = "下推数量";
            gridColumnPushQty.Name = "LiPushQty_GridColumn";
            gridColumnPushQty.FieldName = "pushQty";
            gridColumnPushQty.Visible = true;
            gridColumnPushQty.VisibleIndex = 1;
            gridColumnPushQty.Width = 100;
            gridColumnPushQty.OptionsColumn.AllowEdit = true;
            gridView1.Columns.Add(gridColumnPushQty);

            gridView1.Columns.AddRange(liGridColumnDict.Values.ToArray());
            gridControl1.Refresh();
        }
        private void btnConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StringBuilder messageStr = new StringBuilder();

            DataTable dt = gridControl1.DataSource as DataTable;
            if(dt!= null)
            {
                int iRow = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    string qtyFieldName = ListFormUtil.getFieldNameFormat(liConvertHeadModel.convertPushTableName, liConvertHeadModel.convertPushField);
                    decimal qty = dr[qtyFieldName] == DBNull.Value ? 0 : Convert.ToDecimal(dr[qtyFieldName]); 

                    string cumulativeQtyFieldName = ListFormUtil.getFieldNameFormat(liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertCumulativeField);
                    decimal cumulativeQty = dr[cumulativeQtyFieldName]==DBNull.Value ? 0 : Convert.ToDecimal(dr[cumulativeQtyFieldName]);

                    decimal pushQty = Convert.ToDecimal(dr["pushQty"]);

                    if(cumulativeQty+pushQty > qty)
                    {
                        messageStr.Append(string.Format("第【{0}】行已超出原有数量！", iRow));
                    }
                    else
                    {
                        dr[qtyFieldName] = dr["pushQty"];
                    }
                    ++iRow;
                }

                if (messageStr.Length > 0)
                {
                    MessageUtil.Show(messageStr.ToString(), "温馨提示");
                    this.DialogResult = DialogResult.No;
                }
                else
                {
                    SelectDataRows.Clear();
                    DataRow[] drs = dt.Select(" pushQty > 0 ");
                    if (drs != null && drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                        {
                            SelectDataRows.Add(dr);
                        }
                    }

                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }

            }
            else
            {
                MessageUtil.Show("数据为空！", "温馨提示");
                this.DialogResult = DialogResult.No;
                this.Close();

            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void LiRefQuantityForm_Load(object sender, EventArgs e)
        {
            DataTable dt = convertDatas[0].Table.Clone();
            dt.Columns.Add(new DataColumn("pushQty", Type.GetType("System.Decimal")));
            foreach(DataRow dr in convertDatas)
            {
                DataRow copyDr = dt.NewRow();
                copyDr.ItemArray = dr.ItemArray;
                dt.Rows.Add(copyDr);
            }

            //foreach(DataRow dr in dt.Rows)
            //{
            //    //修改数量
            //    if (liConvertHeadModel != null && liConvertHeadModel.convertRelation == ConvertRelation.PUSHCUMULATIVE)
            //    {
            //        LiConvertBodyModel liConvertBody = liConvertHeadModel.datas.Where(m => m.bCumulativeRelationQty == true).FirstOrDefault();
            //        if (liConvertBody != null)
            //        {
            //            string qtyFieldName = SQLUtil.getFieldNameFormat(liConvertBody.convertSourceType, liConvertBody.convertSourceField);
            //            decimal qty = dr[qtyFieldName] == DBNull.Value ? 0 : Convert.ToDecimal(dr[qtyFieldName]);

            //            string cumulativeQtyFieldName = SQLUtil.getFieldNameFormat(liConvertHeadModel.convertCumulativeTableName, liConvertHeadModel.convertCumulativeField);
            //            decimal cumulativeQty = dr[cumulativeQtyFieldName] == DBNull.Value ? 0 : Convert.ToDecimal(dr[cumulativeQtyFieldName]);

            //            dr[qtyFieldName] = qty - cumulativeQty < 0 ? 0 : qty - cumulativeQty;

            //        }
            //    }
            //}
            this.gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
        }
    }
}