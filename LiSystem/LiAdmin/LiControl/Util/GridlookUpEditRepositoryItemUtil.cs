using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;

using LiModel.LiEnum;

namespace LiControl.Util
{
    /// <summary>
    /// 表格引用控件工具类
    /// </summary>
    public class GridlookUpEditRepositoryItemUtil
    {
        public static Dictionary<RepositoryItemGridLookUpEdit, CustomDisplayTextEventHandler> customDisplayTextEventHandlerDict = new Dictionary<RepositoryItemGridLookUpEdit, CustomDisplayTextEventHandler>();
        public static Dictionary<RepositoryItemGridLookUpEdit, EventHandler> eventHandlerDict = new Dictionary<RepositoryItemGridLookUpEdit, EventHandler>();
        public static Dictionary<RepositoryItemGridLookUpEdit, ChangingEventHandler> changingEventHandlerDict = new Dictionary<RepositoryItemGridLookUpEdit, ChangingEventHandler>();

        /// <summary>
        /// 下拉框显示
        /// </summary>
        /// <typeparam name="TEntity">档案的实体</typeparam>
        /// <param name="sValueMember">值字段名</param>
        /// <param name="sDisplayMember">显示字段名</param>
        /// <param name="searchColumns">搜索列</param>
        /// <param name="displayColumns">下拉显示列</param>
        /// <param name="gridlookUpEdit">控件</param>
        /// <param name="view">窗口</param>
        /// <param name="dataSource">数据源</param>
        public static void InitDefaultComboBoxControl(string sValueMember, string sDisplayMember, List<string> searchColumns, List<string> displayColumns, RepositoryItemGridLookUpEdit gridlookUpEdit, Control view, object dataSource)
        {
            gridlookUpEdit.View.OptionsView.ShowColumnHeaders = false;
            gridlookUpEdit.View.OptionsView.ShowIndicator = false;
            gridlookUpEdit.View.OptionsView.ShowHorzLines = false;

            gridlookUpEdit.ValueMember = sValueMember;
            gridlookUpEdit.DisplayMember = sDisplayMember;
            gridlookUpEdit.DataSource = dataSource;
            gridlookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            gridlookUpEdit.PopulateViewColumns();

            gridlookUpEdit.TextEditStyle = TextEditStyles.Standard;
            gridlookUpEdit.NullText = "";
            //设置修改时会立即展开下拉框
            gridlookUpEdit.ImmediatePopup = true;
            gridlookUpEdit.ShowDropDown = ShowDropDown.DoubleClick;

            gridlookUpEdit.View.Columns.Clear();
            int iColumnIndex = 0;
            foreach (string displayName in displayColumns)
            {
                GridColumn col = gridlookUpEdit.View.Columns.AddField(displayName);
                col.VisibleIndex = iColumnIndex++;
                col.Caption = displayName;
            }

            //绑定下拉框事件
            if (eventHandlerDict.ContainsKey(gridlookUpEdit))
            {
                gridlookUpEdit.Popup -= eventHandlerDict[gridlookUpEdit];
                eventHandlerDict.Remove(gridlookUpEdit);
            }
            Action<object, EventArgs> gridLookUpEdit1_Popup = (sender, e) =>
            {
                FilterLookup(sender, searchColumns);
            };
            eventHandlerDict.Add(gridlookUpEdit, new EventHandler(gridLookUpEdit1_Popup));
            gridlookUpEdit.Popup += eventHandlerDict[gridlookUpEdit];


            //绑定值改变事件
            if (changingEventHandlerDict.ContainsKey(gridlookUpEdit))
            {
                gridlookUpEdit.EditValueChanging -= changingEventHandlerDict[gridlookUpEdit];
                changingEventHandlerDict.Remove(gridlookUpEdit);
            }
            Action<object, ChangingEventArgs> gridLEdit1_EditValueChanging = (sender, e) =>
            {
                if (!view.Visible) view.Show();
                view.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
                {
                    FilterLookup(sender, searchColumns);
                }));
            };
            changingEventHandlerDict.Add(gridlookUpEdit, new ChangingEventHandler(gridLEdit1_EditValueChanging));
            gridlookUpEdit.EditValueChanging += changingEventHandlerDict[gridlookUpEdit];

        }


        /// <summary>
        /// 参照档案类型的配置
        /// </summary>
        /// <typeparam name="TEntity">档案的实体</typeparam>
        /// <param name="sValueMember">值字段名</param>
        /// <param name="sDisplayMember">显示字段名</param>
        /// <param name="searchColumns">搜索列</param>
        /// <param name="displayColumns">下拉显示列</param>
        /// <param name="dictModelDesc">显示名称字典</param>
        /// <param name="gridlookUpEdit">控件</param>
        /// <param name="view">窗口</param>
        /// <param name="dataSource">数据源</param>
        public static void InitDefaultRefControl(string sValueMember, string sDisplayMember, List<string> searchColumns, List<string> displayColumns, Dictionary<string, string> dictModelDesc, RepositoryItemGridLookUpEdit gridlookUpEdit, Control view, object dataSource)
        {
            InitDefaultRefControl(GridlookUpEditShowMode.VALUE_NAME, sValueMember, sDisplayMember, searchColumns, displayColumns, dictModelDesc, gridlookUpEdit, view, dataSource);
        }

        /// <summary>
        /// 参照档案类型的配置
        /// </summary>
        /// <typeparam name="TEntity">档案的实体</typeparam>
        /// <param name="showMode">显示模式</param>
        /// <param name="sValueMember">值字段名</param>
        /// <param name="sDisplayMember">显示字段名</param>
        /// <param name="searchColumns">搜索列</param>
        /// <param name="displayColumns">下拉显示列</param>
        /// <param name="dictModelDesc">显示名称字典</param>
        /// <param name="gridlookUpEdit">控件</param>
        /// <param name="view">窗口</param>
        /// <param name="dataSource">数据源</param>
        public static void InitDefaultRefControl(GridlookUpEditShowMode showMode, string sValueMember, string sDisplayMember, List<string> searchColumns, List<string> displayColumns, Dictionary<string, string> dictModelDesc, RepositoryItemGridLookUpEdit gridlookUpEdit, Control view, object dataSource)
        {

            gridlookUpEdit.ValueMember = sValueMember;
            gridlookUpEdit.DisplayMember = sDisplayMember;
            gridlookUpEdit.DataSource = dataSource;
            gridlookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup;
            gridlookUpEdit.PopulateViewColumns();

            //gridlookUpEdit.Columns[1].Caption = "姓名";
            //lookUpEdit_Driver.PopupWidth = 500;
            //设置可以编辑
            gridlookUpEdit.TextEditStyle = TextEditStyles.Standard;
            gridlookUpEdit.NullText = "";
            //设置修改时会立即展开下拉框
            gridlookUpEdit.ImmediatePopup = true;
            gridlookUpEdit.ShowDropDown = ShowDropDown.DoubleClick;

            gridlookUpEdit.View.Columns.Clear();
            int iColumnIndex = 0;
            foreach (string displayName in displayColumns)
            {
                GridColumn col = gridlookUpEdit.View.Columns.AddField(displayName);
                col.VisibleIndex = iColumnIndex++;
                col.Caption = dictModelDesc[displayName];
            }

            //自定义显示值
            if (customDisplayTextEventHandlerDict.ContainsKey(gridlookUpEdit))
            {
                gridlookUpEdit.CustomDisplayText -= customDisplayTextEventHandlerDict[gridlookUpEdit];
                customDisplayTextEventHandlerDict.Remove(gridlookUpEdit);
            }
            Action<object, CustomDisplayTextEventArgs> gridLookUpEdit1_CustomDisplayText = (sender, e) =>
            {
                switch (showMode)
                {
                    case GridlookUpEditShowMode.NAME:
                        e.DisplayText = string.Format("{0}", e.DisplayText);
                        break;
                    case GridlookUpEditShowMode.VALUE:
                        e.DisplayText = string.Format("{0}", e.Value);
                        break;
                    case GridlookUpEditShowMode.VALUE_NAME:
                        e.DisplayText = string.Format("{0} - {1}", e.Value, e.DisplayText);
                        break;
                    case GridlookUpEditShowMode.NAME_VALUE:
                        e.DisplayText = string.Format("{0} - {1}", e.DisplayText, e.Value);
                        break;
                }
            };
            customDisplayTextEventHandlerDict.Add(gridlookUpEdit, new CustomDisplayTextEventHandler(gridLookUpEdit1_CustomDisplayText));
            gridlookUpEdit.CustomDisplayText += customDisplayTextEventHandlerDict[gridlookUpEdit];

            //绑定下拉框事件
            if (eventHandlerDict.ContainsKey(gridlookUpEdit))
            {
                gridlookUpEdit.Popup -= eventHandlerDict[gridlookUpEdit];
                eventHandlerDict.Remove(gridlookUpEdit);
            }
            Action<object, EventArgs> gridLookUpEdit1_Popup = (sender, e) =>
            {
                FilterLookup(sender, searchColumns);
            };
            eventHandlerDict.Add(gridlookUpEdit, new EventHandler(gridLookUpEdit1_Popup));
            gridlookUpEdit.Popup += eventHandlerDict[gridlookUpEdit];

            //绑定值改变事件
            if (changingEventHandlerDict.ContainsKey(gridlookUpEdit))
            {
                gridlookUpEdit.EditValueChanging -= changingEventHandlerDict[gridlookUpEdit];
                changingEventHandlerDict.Remove(gridlookUpEdit);
            }
            Action<object, ChangingEventArgs> gridLEdit1_EditValueChanging = (sender, e) =>
            {

                if (!view.Visible) view.Show();
                view.BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate
                {
                    FilterLookup(sender, searchColumns);
                }));
            };
            changingEventHandlerDict.Add(gridlookUpEdit, new ChangingEventHandler(gridLEdit1_EditValueChanging));
            gridlookUpEdit.EditValueChanging += changingEventHandlerDict[gridlookUpEdit];
        }

        /// <summary>
        /// 模糊查询的关键方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="arrFilterField"></param>
        private static void FilterLookup(object sender, List<string> filterFieldList)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            GridView gridView = edit.Properties.View as GridView;
            FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);
            CriteriaOperator[] arrCriteriaOperator = new CriteriaOperator[filterFieldList.Count];
            int iRow = 0;
            foreach (string filterfield in filterFieldList)
            {
                //if(string.IsNullOrWhiteSpace(edit.AutoSearchText))   continue;
                arrCriteriaOperator[iRow] = new BinaryOperator(filterfield, "%" + edit.AutoSearchText + "%", BinaryOperatorType.Like);
                iRow++;
            }

            string filterCondition = new GroupOperator(GroupOperatorType.Or, arrCriteriaOperator).ToString();
            fi.SetValue(gridView, filterCondition);
            MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }

    }
}
