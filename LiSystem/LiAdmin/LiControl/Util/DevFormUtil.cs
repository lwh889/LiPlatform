using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid.Views.Grid;
using LiModel.Form;
using LiModel.LiEnum;

namespace LiControl.Util
{
    public class DevFormUtil
    {

        public static string getWhereStr(Dictionary<string, Control> liControlDict, bool IsProcedure)
        {

            string sWhere = string.Empty;

            foreach (KeyValuePair<string, Control> kvp in liControlDict)
            {
                object value = DevControlUtil.getControlData(kvp.Value);
                if (value == null || string.IsNullOrWhiteSpace(Convert.ToString(value)))
                {
                    continue;
                }

                FieldModel fieldModel = kvp.Value.Tag as FieldModel;

                if (kvp.Value.Name.Substring(kvp.Value.Name.Length - 2) == "_B")
                {
                    sWhere += string.Format(" and {0} >= {1} ", fieldModel.columnFieldName, getWhereValue(value, kvp.Value, IsProcedure));
                }
                else if (kvp.Value.Name.Substring(kvp.Value.Name.Length - 2) == "_E")
                {
                    sWhere += string.Format(" and {0} <= {1} ", fieldModel.columnFieldName, getWhereValue(value, kvp.Value, IsProcedure));
                }
                else
                {
                    sWhere += string.Format(" and {0} {1} {2} ", fieldModel.columnFieldName, JudgmentSymbol.getJudeSymbol(fieldModel.sJudgeSymbol), getWhereValue(value, kvp.Value, IsProcedure));
                }
            }

            return sWhere;
        }


        public static string getWhereValue(object value, Control control, bool IsProcedure)
        {
            FieldModel field = control.Tag as FieldModel;
            string whereValue = string.Empty;

            switch (control.GetType().FullName)
            {
                case "DevExpress.XtraEditors.CheckEdit":
                    whereValue = Convert.ToBoolean(value) == true ? "1" : "0";
                    break;
                case "DevExpress.XtraEditors.CalcEdit":
                    whereValue = Convert.ToString(value);
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    whereValue = string.Format("{1}{0}{1}", Convert.ToDateTime(value).ToString("yyyy-MM-dd"), IsProcedure == true ? "''" : "'");
                    break;
                case "DevExpress.XtraEditors.GridLookUpEdit":
                    whereValue = string.Format("{1}{0}{1}", Convert.ToString(value), IsProcedure == true ? "''" : "'");
                    break;
                case "DevExpress.XtraEditors.TextEdit":

                    switch (field.sJudgeSymbol)
                    {
                        case JudgmentSymbol.Like:
                            whereValue = string.Format("{1}%{0}%{1}", value, IsProcedure == true ? "''" : "'");
                            break;
                        case JudgmentSymbol.LikeLeft:
                            whereValue = string.Format("{1}%{0}{1}", value, IsProcedure == true ? "''" : "'");
                            break;
                        case JudgmentSymbol.LikeRight:
                            whereValue = string.Format("{1}{0}%{1}", value, IsProcedure == true ? "''" : "'");
                            break;
                        default:
                            whereValue = string.Format("{1}{0}{1}", value, IsProcedure == true ? "''" : "'");
                            break;
                    }

                    break;
            }

            return whereValue;
        }


        public static string getPreciseWhereStr(List<QueryModel> queryList, bool IsProcedure = false)
        {
            StringBuilder sSqlWhere = new StringBuilder();

            if (queryList.Count > 0) sSqlWhere.Append(" and ");
            foreach (QueryModel model in queryList)
            {
                sSqlWhere.Append(string.Format(" {0} {1} {2} {3} {4} {5}", model.sBracketsBefore, model.sFieldName, JudgmentSymbol.getJudeSymbol(model.sJudgmentSymbol), getPreciseWhereValue(model, IsProcedure), model.sJoin, model.sBracketsAfter));
            }

            return sSqlWhere.ToString();
        }


        public static string getPreciseWhereValue(QueryModel model, bool IsProcedure = false)
        {
            string value = string.Empty;

            switch (model.sFieldType)
            {
                case "CheckEdit":
                    value = string.Format("{1}{0}{1}", Convert.ToBoolean(model.oQueryValue), IsProcedure == true ? "''" : "'");
                    break;
                case "DateTimeEdit":
                case "TimeEdit":
                case "DateEdit":
                    value = string.Format("{1}{0}{1}", Convert.ToDateTime(model.oQueryValue).ToString("yyyy-MM-dd"), IsProcedure == true ? "''" : "'");
                    break;
                case "IntEdit":
                case "DecimalEdit":
                case "CalcEdit":
                    value = Convert.ToString(model.oQueryValue);
                    break;
                default:
                    switch (model.sJudgmentSymbol)
                    {
                        case JudgmentSymbol.Like:
                            value = string.Format("{1}%{0}%{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                        case JudgmentSymbol.LikeLeft:
                            value = string.Format("{1}%{0}{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                        case JudgmentSymbol.LikeRight:
                            value = string.Format("{1}{0}%{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                        default:
                            value = string.Format("{1}{0}{1}", Convert.ToString(model.oQueryValue), IsProcedure == true ? "''" : "'");
                            break;
                    }

                    break;
            }


            return value;
        }


        public static void getBarButtonItemInForm(string key, Dictionary<string, BarButtonItem> barButtonItemDict, Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control.GetType().Name == "RibbonControl")
                {
                    RibbonControl ribbonControl = control as RibbonControl;
                    foreach (BarButtonItem barButtonItem in ribbonControl.Items)
                    {
                        string tag = barButtonItem.Tag == null ? string.Empty : (string)barButtonItem.Tag;
                        if (tag.Contains(key))
                        {
                            barButtonItemDict.Add(tag.Replace(key, ""), barButtonItem);
                        }
                    }
                }
                else
                {
                    getBarButtonItemInForm(key, barButtonItemDict, control);

                }
            }

        }


        public static void getControlInForm(string key, Dictionary<string, LayoutControlItem> layoutControlItemDict, Dictionary<string, Control> controlDict, Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control.GetType().Name == "LayoutControl")
                {
                    LayoutControl layoutControl = control as LayoutControl;
                    foreach (LayoutControlItem layoutControlItem in layoutControl.Root.Items)
                    {
                        if (layoutControlItem.Control != null)
                        {
                            string tag = layoutControlItem.Control.Tag == null ? string.Empty : (string)layoutControlItem.Control.Tag;
                            if (tag.Contains(key))
                            {
                                layoutControlItemDict.Add(tag.Replace(key, ""), layoutControlItem);
                                controlDict.Add(tag.Replace(key, ""), layoutControlItem.Control);
                            }
                        }
                    }

                }
                else
                {
                    getControlInForm(key, layoutControlItemDict, controlDict, control);
                }
            }
        }


        public static void getGridColumnInForm(string key, Dictionary<string, GridColumn> gridColumnDict, Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control.GetType().Name == "LayoutControl")
                {
                    getGridColumnInForm(key, gridColumnDict, control);
                }
                else if (control.GetType().Name == "GridControl")
                {
                    GridControl gridControl = (GridControl)control;
                    foreach (GridView gridView in gridControl.ViewCollection)
                    {

                        foreach (GridColumn gc in gridView.Columns)
                        {
                            string tag = gc.Tag == null ? string.Empty : (string)gc.Tag;
                            if (tag.Contains(key))
                            {
                                gridColumnDict.Add(tag.Replace(key, ""), gc);
                            }
                        }
                    }

                }
                else
                {
                    getGridColumnInForm(key, gridColumnDict, control);

                }

            }
        }

        public static void getGridControlInForm(string key, Dictionary<string, GridControl> gridControlDict, Dictionary<string, GridView> gridViewDict, Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                if (control.GetType().Name == "LayoutControl")
                {
                    getGridControlInForm(key, gridControlDict, gridViewDict, control);
                }
                else if (control.GetType().Name == "GridControl")
                {
                    GridControl gridControl = (GridControl)control;
                    string gridControlTag = gridControl.Tag == null ? string.Empty : (string)gridControl.Tag;
                    if (gridControlTag.Contains(key))
                    {
                        gridControlDict.Add(gridControlTag.Replace(key, ""), gridControl);
                    }

                    foreach (GridView gridView in gridControl.ViewCollection)
                    {
                        string gridViewTag = gridView.Tag == null ? string.Empty : (string)gridView.Tag;
                        if (gridViewTag.Contains(key))
                        {
                            gridViewDict.Add(gridViewTag.Replace(key, ""), gridView);
                        }
                    }

                }
                else
                {
                    getGridControlInForm(key, gridControlDict, gridViewDict, control);

                }

            }
        }
    }
}
