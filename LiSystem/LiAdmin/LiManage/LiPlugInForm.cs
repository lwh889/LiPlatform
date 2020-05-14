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

using System.IO;
using System.Reflection;

using LiHttp.Server;
using LiHttp.RequestParam;
using LiCommon.Util;
using LiModel.Form;
using LiControl.Util;
using LiModel.Util;


namespace LiManage
{
    public partial class LiPlugInForm : DevExpress.XtraEditors.XtraForm
    {
        public string filePath;

        private List<DataRow> _SelectRows = new List<DataRow>();
        public List<DataRow> SelectRows
        {
            set { _SelectRows = value; }
            get { return _SelectRows; }
        }

        public LiPlugInForm(string filePath)
        {
            InitializeComponent();

            this.filePath = filePath;
        }

        private void LiPlugInForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("eventFullName"));
            dt.Columns.Add(new DataColumn("eventAssemblyName"));

            Assembly assembly = Assembly.Load(File.ReadAllBytes(filePath)); // 加载程序集（EXE 或 DLL）
            Type[] types = assembly.GetTypes();
            foreach (Type t in types)
            {
                DataRow dr = dt.NewRow();
                dr["eventFullName"] = t.FullName;
                dr["eventAssemblyName"] = assembly.GetName().Name;

                dt.Rows.Add(dr);
            }

            gridControl1.DataSource = dt;
            gridView1.BestFitColumns();
            gridControl1.RefreshDataSource();
        }

        private void btnReturnData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            _SelectRows.Clear();
            if (gridControl1.DataSource != null)
            {
                _SelectRows = DevControlUtil.getSelectDataRows(gridView1,gridControl1);

                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                MessageBox.Show("请选择数据！", "温馨提示");
            }
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

    }
}