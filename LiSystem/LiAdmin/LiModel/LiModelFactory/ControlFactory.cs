using LiModel.Form;
using LiModel.LiAttribute;
<<<<<<< HEAD
using LiModel.LiEnum;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiModelFactory
{
    public class ControlFactory
    {
        public const int WIDTH = 400;
        public static List<ControlModel> getInstancesByModel(object entity)
        {
            List<ControlModel> controls = new List<ControlModel>();

            var t = entity.GetType();

            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                if (property.IsDefined(typeof(EditAttribute), true))
                {
                    ControlModel customerControlModel = new ControlModel();
                    customerControlModel.name = property.Name;

                    controls.Add(customerControlModel);

                    var attributes = property.GetCustomAttributes(true);
                    foreach (var attribute in attributes)
                    {
                        if (typeof(ControlTypeAttribute).FullName.Equals(attribute.GetType().FullName))
                        {
                            customerControlModel.controltype = Convert.ToString(attribute.GetType().GetProperty("Value").GetValue(attribute));
                        }

                        if (typeof(DisplayAttribute).FullName.Equals(attribute.GetType().FullName))
                        {
                            customerControlModel.text = Convert.ToString(attribute.GetType().GetProperty("Name").GetValue(attribute));
                            customerControlModel.row = Convert.ToInt16(attribute.GetType().GetProperty("Order").GetValue(attribute));
                        }
                    }

                    if (string.IsNullOrEmpty(customerControlModel.controltype))
                    {

                        switch (property.GetType().FullName)
                        {
                            case "System.Boolean":
                                customerControlModel.controltype = "CheckEdit";
                                break;
                            case "System.String":
                                customerControlModel.controltype = "TextEdit";
                                break;
                            case "System.DateTime":
                                customerControlModel.controltype = "CalcEdit";
                                break;
                            case "System.Int16":
                            case "System.Int32":
                            case "System.Int64":
                            case "System.Double":
                            case "System.Float":
                                customerControlModel.controltype = "CalcEdit";
                                break;
                            default:
                                customerControlModel.controltype = "TextEdit";
                                break;
                        }
                    }
                }

            }

            return controls;
        }

<<<<<<< HEAD
        public static ControlModel getCodeControl(int controlGroupId)
        {
            ControlModel controlModel = ControlModel.getInstance(controlGroupId);
            controlModel.name = "cCode";
            controlModel.text = "编码";
            controlModel.width = WIDTH;
            controlModel.length = 30;
            controlModel.controltype = ControlType.TextEdit;
            return controlModel;
        }
        public static ControlModel getNameControl(int controlGroupId)
        {
            ControlModel controlModel = ControlModel.getInstance(controlGroupId);
            controlModel.name = "cName";
            controlModel.text = "名称";
            controlModel.width = WIDTH;
            controlModel.length = 30;
            controlModel.controltype = ControlType.TextEdit;
            return controlModel;
        }

=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
        public static ControlModel getVoucherCode()
        {
            return new ControlModel() { id = 0, name = "billCode", text = "单据编号", width = WIDTH, length = 20, height = 24, col = 1, row = 1, controltype = "VoucherCodeEdit", bVisibleInList = true, bVisible = true, bIsNull = true };
        }
<<<<<<< HEAD
        public static ControlModel getRowNoControl()
        {
            return new ControlModel() { id = 0, name = "iRow", text = "行号", width = 80, length = 9, height = 24, col = 1, row = 1, controltype = "IntEdit", bVisibleInList = true, bVisible = true, bIsNull = true, bReadOnly = true};
        }
        public static ControlModel getQuantityControl()
        {
            return new ControlModel() { id = 0, name = "iQuantity", text = "数量", width = WIDTH, length = 9, height = 24, col = 2, row = 1, controltype = "DecimalEdit", scale = 6, bVisibleInList = true, bVisible = true, bIsNull = true};
        }
        public static ControlModel getPriceControl()
        {
            return new ControlModel() { id = 0, name = "iPrice", text = "单价", width = WIDTH, length = 9, height = 24, col = 3, row = 1, controltype = "DecimalEdit", scale = 6, bVisibleInList = true, bVisible = true, bIsNull = true };
        }
        public static ControlModel getAmountControl()
        {
            return new ControlModel() { id = 0, name = "iAmount", text = "金额", width = WIDTH, length = 9, height = 24, col = 4, row = 1, controltype = "DecimalEdit", scale = 2, bVisibleInList = true, bVisible = true, bIsNull = true };
        }
        public static ControlModel getMemoHeadControl()
        {
            return new ControlModel() { id = 0, name = "cMemo", text = "备注", width = WIDTH, length = 9, height = 24, col = 5, row = 1, controltype = "MemoEdit", bVisibleInList = true, bVisible = true, bIsNull = true };
        }
        public static ControlModel getMemoBodyControl()
        {
            ControlModel controlModel = getMemoHeadControl();
            controlModel.name = "bMemo";
            return controlModel;
        }
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9

        public static ControlModel getVoucherDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dDate";
            controlModel.text = "单据日期";

            return controlModel;
        }

        /// <summary>
        /// 状态控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getStatusControl()
        {
            return new ControlModel() { id = 0, width = WIDTH, length = 20, height = 24, col = 1, row = 1, controltype = "StatusEdit", bVisibleInList = true, bVisible = true, dictInfoType = "3", gridlookUpEditShowModelJson = "{\"valueMember\":\"dictCode\",\"displayMember\":\"dictName\",\"searchColumns\":[\"dictName\"],\"displayColumns\":[\"dictName\"]}", bIsNull = true };
        }
        /// <summary>
        /// 用户控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControl()
        {
            return new ControlModel() { id = 0, width = WIDTH, length = 20, height = 24, col = 1, row = 1, controltype = "UserEdit", bVisibleInList = true, bVisible = true, basicInfoKey = "liUsers", basicInfoTableKey = "userCode", basicInfoShowFieldName = "userName", basicInfoShowMode = "NAME",gridlookUpEditShowModelJson= "{\"showMode\":\"NAME\",\"valueMember\":\"userCode\",\"displayMember\":\"userName\",\"searchColumns\":[\"userCode\",\"userName\"],\"displayColumns\":[\"userCode\",\"userName\"],\"dictModelDesc\":{\"userCode\":\"用户编码\",\"userName\":\"用户名称\"}}", bIsNull = true };
        }
        /// <summary>
        /// 日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControl()
        {
            return new ControlModel() { id = 0, width = WIDTH, length = 20, height = 24, col = 1, row = 1, controltype = "DateEdit", bVisibleInList = true, bVisible = true, bIsNull = true };
        }

        /// <summary>
        /// 制单人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getVoucherStatusControl()
        {
            ControlModel controlModel = getStatusControl();
            controlModel.name = "billStatus";
            controlModel.text = "单据状态";
            controlModel.bReadOnly = true;
            return controlModel;
        }
        /// <summary>
        /// 制单人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlMaker()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cMaker";
            controlModel.text = "制单人";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 1;
            controlModel.col = 1;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }
        /// <summary>
        /// 制单日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlMakerDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dMakeDate";
            controlModel.text = "制单日期";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 1;
            controlModel.col = 2;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }

        /// <summary>
        /// 修改人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlModifer()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cModifer";
            controlModel.text = "修改人";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 1;
            controlModel.col = 3;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }
        /// <summary>
        /// 修改日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlModifyDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dModifiedDate";
            controlModel.text = "修改日期";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 1;
            controlModel.col = 4;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }

        /// <summary>
        /// 提交人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlSubmit()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cSumbit";
            controlModel.text = "提交人";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 2;
            controlModel.col = 1;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }
        /// <summary>
        /// 提交日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlSubmitDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dSumbitDate";
            controlModel.text = "提交日期";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 2;
            controlModel.col = 2;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }

        /// <summary>
        /// 审核人控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getUserControlAuditor()
        {
            ControlModel controlModel = getUserControl();
            controlModel.name = "cAuditor";
            controlModel.text = "审核人";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 2;
            controlModel.col = 3;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }
        /// <summary>
        /// 审核日期控件
        /// </summary>
        /// <param name="controlGroupId"></param>
        /// <returns></returns>
        public static ControlModel getDateControlAuditDate()
        {
            ControlModel controlModel = getDateControl();
            controlModel.name = "dAuditDate";
            controlModel.text = "审核日期";
            controlModel.bReadOnly = true;
<<<<<<< HEAD
            controlModel.row = 2;
            controlModel.col = 4;
=======
>>>>>>> 5884da41c4c556e5dddecd5764f1e6d9153448c9
            return controlModel;
        }
    }
}
