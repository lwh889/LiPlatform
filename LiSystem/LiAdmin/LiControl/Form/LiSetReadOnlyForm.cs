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
using DevExpress.XtraLayout;

using LiModel.Form;
using LiCommon.Util;
using LiHttp.Enum;
using LiContexts;
using LiModel.LiEnum;
using LiModel.Util;
using LiControl.Util;

namespace LiControl.Form
{
    public partial class LiSetReadOnlyForm : DevExpress.XtraEditors.XtraForm
    {
        /// <summary>
        /// 单据状态
        /// </summary>
        private VoucherStatusModel voucherStatusModel;

        /// <summary>
        /// 单据控件状态，用于更新
        /// </summary>
        private List<ControlStatusModel> newControlStatusModels;

        /// <summary>
        /// 单据表头控件
        /// </summary>
        Dictionary<string, Control> controlDict;

        /// <summary>
        /// 单据表头控件标题
        /// </summary>
        Dictionary<string, LayoutControlItem> layoutControlItemDict;

        /// <summary>
        /// 用户控件
        /// </summary>
        List<GridlookUpEditModel> userControls = new List<GridlookUpEditModel>();

        /// <summary>
        /// 时间控件
        /// </summary>
        List<GridlookUpEditModel> dateControls = new List<GridlookUpEditModel>();

        /// <summary>
        /// 状态控件
        /// </summary>
        List<GridlookUpEditModel> statusControls = new List<GridlookUpEditModel>();

        #region 引用数据源
        private GridlookUpEditModel gridlookUpEditModel = new GridlookUpEditModel();
        #endregion

        /// <summary>
        /// 单据标识
        /// </summary>
        string formID = "LiSetReadOnlyForm";

        public LiSetReadOnlyForm(List<ControlStatusModel> newControlStatusModels, List<GridlookUpEditModel> userControls, List<GridlookUpEditModel> dateControls, List<GridlookUpEditModel> statusControls, VoucherStatusModel voucherStatusModel)
        {
            InitializeComponent();
            this.voucherStatusModel = voucherStatusModel;
            this.newControlStatusModels = newControlStatusModels;
            this.userControls = userControls;
            this.dateControls = dateControls;
            this.statusControls = statusControls;

            Init();

        }

        private void Init()
        {
            InitData();
            InitControl();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            controlDict = new Dictionary<string, Control>();
            layoutControlItemDict = new Dictionary<string, LayoutControlItem>();
            DevFormUtil.getControlInForm(formID + ".", layoutControlItemDict, controlDict, this);
        }

        private void InitControl()
        {

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(), gridlookUpEditModel.getDictModelDesc(), repositoryItemGridLookUpEdit_userFieldName, this, userControls);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(), gridlookUpEditModel.getDictModelDesc(), repositoryItemGridLookUpEdit_dateFieldName, this, dateControls);

            GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowMode.NAME, gridlookUpEditModel.getValueMember(), gridlookUpEditModel.getDisplayMember(), gridlookUpEditModel.getSearchColumns(), gridlookUpEditModel.getDisplayColumns(), gridlookUpEditModel.getDictModelDesc(), repositoryItemGridLookUpEdit_statusFieldName, this, statusControls);

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void loadData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                DevControlUtil.setContorlData(ModelUtil.getModelValue<VoucherStatusModel>(kvp.Key, voucherStatusModel), kvp.Value);
            }

            gridControl1.DataSource = voucherStatusModel.dataStatuss;
            gridControl1.RefreshDataSource();

            if (voucherStatusModel.dataStatuss != null && voucherStatusModel.dataStatuss.Count > 0)
            {
                gridControl2.DataSource = voucherStatusModel.dataStatuss[0].dataControlStatuss;
                gridControl2.RefreshDataSource();
            }
            else
            {
                gridControl2.DataSource = null;
                gridControl2.RefreshDataSource();
            }
        }

        /// <summary>
        /// 获取Form数据到模型
        /// </summary>
        public void getData()
        {
            foreach (KeyValuePair<string, Control> kvp in controlDict)
            {
                ModelUtil.setModelValue<VoucherStatusModel>(kvp.Key, DevControlUtil.getControlData(kvp.Value), voucherStatusModel);
            }
            voucherStatusModel.dataStatuss = (List<StatusModel>)gridControl1.DataSource;
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<VoucherStatusModel> getEntitys(string key)
        {
            //string resultContent;
            //QueryParamModel paramModel = LiHttpQuery.getQueryParamModel_ShowAllColumn("queryBy", "liVoucherStatus");
            //paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single("code",key));

            //if (LiContext.liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            //{
            //    return JsonUtil.GetEntityToList<VoucherStatusModel>(resultContent);
            //}

            //return null;

            return LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, LiContext.SystemCode).getEntityList<VoucherStatusModel>(key, "code");
        }

        private void btnAddStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            List<ControlStatusModel> controlStatusModels = new List<ControlStatusModel>();

            foreach (ControlStatusModel model in newControlStatusModels)
            {
                controlStatusModels.Add(model.Clone());
            }

            StatusModel statusModel = new StatusModel();
            statusModel.code = "StatusName";
            statusModel.name = "状态名称";
            statusModel.fid = voucherStatusModel.id;
            statusModel.dataControlStatuss = controlStatusModels;

            voucherStatusModel.dataStatuss.Add(statusModel);

            gridControl1.DataSource = voucherStatusModel.dataStatuss;
            gridControl1.RefreshDataSource();
            gridView1.BestFitColumns();
        }

        private void btnRefreshStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (StatusModel statusModel in voucherStatusModel.dataStatuss)
            {
                List<ControlStatusModel> addControlStatusModels = new List<ControlStatusModel>();
                List<ControlStatusModel> deleteControlStatusModels = new List<ControlStatusModel>();
                foreach (ControlStatusModel controlStatusModel in statusModel.dataControlStatuss)
                {
                   var entity =  newControlStatusModels.Where(m => m.code == controlStatusModel.code).FirstOrDefault();
                   if (entity == null)
                   {
                       deleteControlStatusModels.Add(controlStatusModel);
                   }
                }
                foreach (ControlStatusModel controlStatusModel in newControlStatusModels)
                {
                    var entity = statusModel.dataControlStatuss.Where(m => m.code == controlStatusModel.code).FirstOrDefault();
                    if (entity == null)
                    {
                        addControlStatusModels.Add(controlStatusModel);
                    }
                }
                DataUtil.deleteInList<ControlStatusModel>(deleteControlStatusModels, statusModel.dataControlStatuss);

                statusModel.dataControlStatuss.AddRange(addControlStatusModels);
            }

            loadData();
        }

        private void btnDeleteStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StatusModel deleteValue = gridView1.GetFocusedRow() as StatusModel;
            if (deleteValue != null)
            {
                DevControlUtil.deleteRowInGridView<StatusModel>((StatusModel)deleteValue, gridControl1);
            }

            gridControl1.RefreshDataSource();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            getData();
            string resultContent;
            bool bSuccess = false;

            if (voucherStatusModel.id > 0)
            {
                LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, LiContext.SystemCode).updateEntity(voucherStatusModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, LiContext.SystemCode).tipStr, "温馨提示");
                //if (LiContext.liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdate(), LiHttpUpdate.getUpdateParamModel("liVoucherStatus", voucherStatusModel), out resultContent))
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
                LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, LiContext.SystemCode).newEntity(voucherStatusModel);
                MessageUtil.Show(LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, LiContext.SystemCode).tipStr, "温馨提示");
                //if (LiContext.liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsert(), LiHttpInsert.getInsertParamModel("liVoucherStatus", voucherStatusModel), out resultContent))
                //{
                //    MessageUtil.Show("保存成功！", "温馨提示");
                //    bSuccess = true;

                //}
                //else
                //{
                //    MessageUtil.Show("保存失败！" + resultContent, "温馨提示");
                //}
            }
            if (bSuccess)
            {
                List<VoucherStatusModel> voucherStatusModels = getEntitys(voucherStatusModel.code);

                voucherStatusModel = voucherStatusModels[0];
                loadData();
            }

        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            StatusModel statusModel = gridView1.GetFocusedRow() as StatusModel;
            if (statusModel != null)
            {
                gridControl2.DataSource = statusModel.dataControlStatuss;
                gridControl2.RefreshDataSource();
                gridView2.BestFitColumns();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {

        }

        private void LiSetReadOnlyForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        
    }
}