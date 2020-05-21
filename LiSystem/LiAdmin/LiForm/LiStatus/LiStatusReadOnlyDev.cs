using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using LiCommon.Util;
using LiModel.Form;
using LiControl.Util;

namespace LiForm.LiStatus
{
    public class LiStatusReadOnlyDev : LiAStatus,LiIStatusReadOnlyDev
    {
        private StatusModel _statusModel = new StatusModel();
        private Dictionary<string, GridColumn> _gridColumnDict = new Dictionary<string, GridColumn>();
        private Dictionary<string, BarButtonItem> _buttonDict = new Dictionary<string, BarButtonItem>();
        private Dictionary<string, Control> _controlDict = new Dictionary<string, Control>();

        public StatusModel statusModel { set { _statusModel = value; } get { return _statusModel; } }
        public Dictionary<string, GridColumn> gridColumnDict { set { _gridColumnDict = value; } get { return _gridColumnDict; } }
        public Dictionary<string, BarButtonItem> buttonDict { set { _buttonDict = value; } get { return _buttonDict; } }
        public Dictionary<string, Control> controlDict { set { _controlDict = value; } get { return _controlDict; } }

        /// <summary>
        /// 要在显示窗体后，才显示
        /// </summary>
        /// <param name="context"></param>
        public override void Handle(LiStatusContext context)
        {
            foreach (ControlStatusModel controlStatusModel in statusModel.dataControlStatuss)
            {
                if (_gridColumnDict != null && _gridColumnDict.ContainsKey(controlStatusModel.code))
                {
                    GridColumn gridColumn = _gridColumnDict[controlStatusModel.code];
                    ControlModel controlModel = gridColumn.Tag as ControlModel;
                    if (controlModel != null)
                    {
                        if (!controlModel.bReadOnly)
                        {
                            gridColumn.OptionsColumn.ReadOnly = controlStatusModel.bReadOnly;
                        }
                    }
                }

                if (_buttonDict != null && _buttonDict.ContainsKey(controlStatusModel.code))
                {
                    BarButtonItem button = _buttonDict[controlStatusModel.code];
                    button.Enabled = !controlStatusModel.bReadOnly;
                }


                if (_controlDict != null && _controlDict.ContainsKey(controlStatusModel.code))
                {
                    Control control = _controlDict[controlStatusModel.code];
                    ControlModel controlModel = control.Tag as ControlModel;
                    if (controlModel != null)
                    {
                        if (!controlModel.bReadOnly)
                        {
                            DevControlUtil.setContorlReadOnly(controlStatusModel.bReadOnly, control);
                        }
                    }

                }
            }

            setFieldControlValue(statusModel.userFieldName, LiContexts.LiContext.userInfo.userCode, _controlDict);
            setFieldControlValue(statusModel.dateFieldName, DateTime.Now, _controlDict);
            setFieldControlValue(statusModel.statusFieldName, statusModel.code, _controlDict);

            LiStatusReadOnlyDev previousStatus = context.getPreviousStatus() as LiStatusReadOnlyDev;
            if(previousStatus != null)
            {
                clearFieldControlValue(previousStatus.statusModel.userFieldName, _controlDict);
                clearFieldControlValue(previousStatus.statusModel.dateFieldName, _controlDict);
            }
        }

        /// <summary>
        /// 设置控件值
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="controlDict"></param>
        private void setFieldControlValue(string fieldName, object value, Dictionary<string, Control> controlDict)
        {
            if (!string.IsNullOrEmpty(fieldName) && controlDict.ContainsKey(fieldName))
            {
                Control control = controlDict[fieldName];
                DevControlUtil.setContorlData(value, control);
            }
        }
        /// <summary>
        /// 设置控件值
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="controlDict"></param>
        private void clearFieldControlValue(string fieldName, Dictionary<string, Control> controlDict)
        {
            if (!string.IsNullOrEmpty(fieldName) && controlDict.ContainsKey(fieldName))
            {
                Control control = controlDict[fieldName];
                DevControlUtil.setContorlData(null, control);
            }
        }

        public bool isNewStatus()
        {
            return statusModel.bNew;
        }

        public bool isShowStatus()
        {
            return statusModel.bShow;
        }

        /// <summary>
        /// 用户控件
        /// </summary>
        public string userFieldName { set; get; }

        /// <summary>
        /// 日期控件
        /// </summary>
        public string dateFieldName { set; get; }
    }
}
