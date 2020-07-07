using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiCommon.LiEnum
{
    public class ControlType
    {
        public const string VoucherCodeEdit = "VoucherCodeEdit";
        public const string TextEdit = "TextEdit";
        public const string CheckEdit = "CheckEdit";
        public const string MemoEdit = "MemoEdit";
        public const string DecimalEdit = "DecimalEdit";
        public const string IntEdit = "IntEdit";
        public const string DateTimeEdit = "DateTimeEdit";
        public const string TimeEdit = "TimeEdit";
        public const string GridLookUpEditComboBox = "GridLookUpEditComboBox";
        public const string GridLookUpEdit = "GridLookUpEdit";
        public const string GridLookUpEditRef = "GridLookUpEditRef";
        public const string GridLookUpEditRefAssist = "GridLookUpEditRefAssist";
        public const string TreeListLookUpEdit = "TreeListLookUpEdit";
        public const string UserEdit = "UserEdit";
        public const string DateEdit = "DateEdit";
        public const string StatusEdit = "StatusEdit";
        public const string CalcEdit = "CalcEdit";
        public const string GridControl = "GridControl";
        public const string PictureEdit = "PictureEdit";

        public static string getDataTypeByControlType(string controlType)
        {
            switch (controlType)
            {
                case ControlType.CalcEdit:
                    return "double";
                case ControlType.DecimalEdit:
                    return "double";
                case ControlType.IntEdit:
                    return "int";
                case ControlType.TextEdit:
                case ControlType.MemoEdit:
                case ControlType.GridLookUpEditComboBox:
                case ControlType.GridLookUpEditRef:
                    return "string";
                default:
                    return "string";
            }
        }
        public static List<string> getControlTyps()
        {
            List<string> controlTypes = new List<string>();
            controlTypes.Add(ControlType.CheckEdit);
            controlTypes.Add(ControlType.DateEdit);
            controlTypes.Add(ControlType.DateTimeEdit);
            controlTypes.Add(ControlType.DecimalEdit);
            controlTypes.Add(ControlType.GridLookUpEditComboBox);
            controlTypes.Add(ControlType.GridLookUpEditRef);
            controlTypes.Add(ControlType.GridLookUpEditRefAssist);
            controlTypes.Add(ControlType.IntEdit);
            controlTypes.Add(ControlType.MemoEdit);
            controlTypes.Add(ControlType.StatusEdit);
            controlTypes.Add(ControlType.TextEdit);
            controlTypes.Add(ControlType.TimeEdit);
            controlTypes.Add(ControlType.TreeListLookUpEdit);
            controlTypes.Add(ControlType.UserEdit);
            controlTypes.Add(ControlType.VoucherCodeEdit);
            controlTypes.Add(ControlType.VoucherCodeEdit);
            controlTypes.Add(ControlType.CalcEdit);

            return controlTypes;
        }
    }
}
