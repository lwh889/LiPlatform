using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;

using LiModel.Form;
using LiForm.Event;
using LiForm.Event.EventForm;
using LiForm.Event.Mediator;
using LiCommon.Util;
using LiModel.Dev.GridlookUpEdit;
using LiModel.LiConvert;
using LiHttp.Enum;
using LiContexts;
using LiModel.LiEnum;
using LiModel.Util;
using LiControl.Util;

using Newtonsoft.Json;

namespace LiForm.Dev.Util
{
    public class FormUtil
    {
        /// <summary>
        /// 下推单据
        /// </summary>
        /// <param name="liConvertHeadModel"></param>
        /// <param name="drs"></param>
        /// <param name="liForm"></param>
        public static void pushVoucher(LiConvertHeadModel liConvertHeadModel,DataRow drHead, List<DataRow> drs, LiForm liForm, bool isPrefix = true)
        {


            //获取转换关系
            List<LiConvertBodyModel> convertHeadList = liConvertHeadModel.datas.Where(m => m.convertDCollection == null).ToList();
            List<LiConvertBodyModel> convertBodyList = liConvertHeadModel.datas.Where(m => m.convertDCollection != null).ToList();

            //获取集合数据源
            var groupList = convertBodyList.GroupBy(m => m.convertDCollection);
            string collectionName = "";
            foreach (var group in groupList)
            {
                collectionName = group.Key;
            }
            List<Dictionary<string, object>> dtDest = liForm.formDataDict[collectionName] as List<Dictionary<string, object>>;

            //转换
            foreach (LiConvertBodyModel convertHead in convertHeadList)
            {
                if (string.IsNullOrEmpty(convertHead.convertSourceField)) continue;
                string fieldName = isPrefix ? string.Format("Li{0}_{1}", convertHead.convertSourceType, convertHead.convertSourceField): convertHead.convertSourceField;
                liForm.formDataDict[convertHead.convertDestField] = drHead[fieldName];
            }

            foreach (DataRow dr in drs)
            {
                Dictionary<string, object> drDest = new Dictionary<string, object>();

                foreach (LiConvertBodyModel convertBody in convertBodyList)
                {
                    if (string.IsNullOrEmpty(convertBody.convertSourceField)) continue;

                    string fieldName = isPrefix ? string.Format("Li{0}_{1}", convertBody.convertSourceType, convertBody.convertSourceField) : convertBody.convertSourceField;
                    if (drDest.ContainsKey(convertBody.convertDestField))
                    {
                        drDest[convertBody.convertDestField] = dr[fieldName];
                    }
                    else
                    {
                        drDest.Add(convertBody.convertDestField, dr[fieldName]);
                    }
                }

                dtDest.Add(drDest);
            }
        }
        /// <summary>
        /// 加载快速查询界面
        /// </summary>
        /// <param name="fieldList"></param>
        /// <param name="liControlDict"></param>
        /// <param name="layoutControlGroup"></param>
        /// <param name="layoutControl"></param>
        public static void loadQuickQuery(List<FieldModel> fieldList,Dictionary<string, Control> liControlDict, LayoutControlGroup layoutControlGroup, LayoutControl layoutControl)
        {

            int i = 0;
            LayoutControlItem layoutControlItemPrevious = null;
            layoutControl.BeginUpdate();
            layoutControl.Controls.Clear();
            layoutControl.Root.Items.Clear();
            layoutControlGroup.Clear();
            
            liControlDict.Clear();

            foreach (FieldModel field in fieldList)
            {
                if (!field.bQuery) continue;

                LayoutControlItem layoutControlItem = layoutControlGroup.AddItem();

                Control control = ControlModelUtil.getControl(field.sColumnControlType);
                control.Name = string.Format("{0}{1}", field.code, field.bRange ? "_B" : "");
                control.Tag = field;
                layoutControlItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                layoutControlItem.MaxSize = new System.Drawing.Size(300, 25);
                layoutControlItem.Control = control;
                layoutControlItem.Text = field.name;
                layoutControlItem.Name = string.Format("layoutControlItem{0}{1}", field.code, field.bRange ? "_B" : "");

                layoutControl.Controls.Add(layoutControlItem.Control);
                liControlDict.Add(layoutControlItem.Name, layoutControlItem.Control);

                //如果不是第一个，都放在前一个后面
                if (i != 0)
                {
                    layoutControlItem.Move(layoutControlItemPrevious, InsertType.Right);
                }

                //保存前一个
                layoutControlItemPrevious = layoutControlItem;

                i++;
                if (i > 3) i = 0;

                if (field.bRange)
                {
                    layoutControlItem = layoutControlGroup.AddItem();

                    control = ControlModelUtil.getControl(field.sColumnControlType);
                    control.Name = string.Format("{0}{1}", field.code, field.bRange ? "_E" : "");
                    layoutControlItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                    layoutControlItem.MaxSize = new System.Drawing.Size(300, 25);
                    layoutControlItem.Control = control;
                    layoutControlItem.Text = "——";
                    layoutControlItem.Name = string.Format("layoutControlItem{0}{1}", field.code, field.bRange ? "_E" : "");

                    layoutControl.Controls.Add(layoutControlItem.Control);
                    liControlDict.Add(layoutControlItem.Name, layoutControlItem.Control);

                    //如果不是第一个，都放在前一个后面
                    if (i != 0)
                    {
                        layoutControlItem.Move(layoutControlItemPrevious, InsertType.Right);
                    }

                    //保存前一个
                    layoutControlItemPrevious = layoutControlItem;

                    i++;
                    if (i > 3) i = 0;
                }


            }
            layoutControl.EndUpdate();
        }

        /// <summary>
        /// 加载默认方案按钮
        /// </summary>
        /// <param name="querySchemeModels"></param>
        /// <param name="querySchemeBtns"></param>
        /// <param name="buttonHandler"></param>
        /// <param name="layoutControlGroup1"></param>
        public static void loadQueryScheme(List<QuerySchemeModel> querySchemeModels, Dictionary<string, SimpleButton> querySchemeBtns, System.EventHandler buttonHandler, LayoutControlGroup layoutControlGroup)
        {

            LayoutControlItem defaultLayoutControlItem = null;
            foreach (QuerySchemeModel querySchemeModel in querySchemeModels)
            {
                LayoutControlItem layoutControlItem = layoutControlGroup.AddItem();
                layoutControlItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                SimpleButton simpleButton = new SimpleButton();
                simpleButton.Text = querySchemeModel.querySchemeName;
                simpleButton.Click += buttonHandler;
                simpleButton.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;

                layoutControlItem.MaxSize = simpleButton.CalcBestSize();
                layoutControlItem.Control = simpleButton;

                if (defaultLayoutControlItem != null)
                {
                    layoutControlItem.Move(defaultLayoutControlItem, InsertType.Right);
                }
                defaultLayoutControlItem = layoutControlItem;

                querySchemeBtns.Add(simpleButton.Text, simpleButton);
            }
        }

        /// <summary>
        /// 获取单据列表Form
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static RibbonForm getVoucherList(string key, string systemCode)
        {
            FormModel formModel = LiContext.getFormModel(key, systemCode);
            //FormModel formModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).getEntitySingle<FormModel>(key,"name");
            //FormModel formModel = FormModelEntity.getEntitySingle_FormModel(key);
            LiListForm liListForm = new LiListForm(formModel);


            return liListForm;

        }


        public static RibbonForm getVoucher(string key)
        {

            //Dictionary<string, object> dictData = LiContexts.LiContext.getHttpEntity<NewDataEntity>("NewData").getEntityNewData(key);
            //Dictionary<string, object> dictData = NewDataEntity.getEntityNewData(key);

            return FormUtil.getVoucher(key, LiContexts.LiContext.getVoucherEmptyDatas(key));


        }

        public static RibbonForm getVoucher(string key, Dictionary<string, object> formDataDict)
        {
            LiForm liForm = new LiForm(LiContexts.LiContext.getFormModel(key, LiContext.SystemCode));
            liForm.formDataDict =  formDataDict;
            liForm.formCode = key;
            liForm.voucherStatusModel = LiContexts.LiContext.getVoucherStatusModels(key);

            return liForm;
        }


        /// <summary>
        /// 设置工具栏
        /// </summary>
        /// <param name="ribbon"></param>
        public static void setRibbon(DevExpress.XtraBars.Ribbon.RibbonControl ribbon)
        {
            ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
            ribbon.ShowFullScreenButton = DevExpress.Utils.DefaultBoolean.True;
            ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
            ribbon.ShowToolbarCustomizeItem = false;
            ribbon.Toolbar.ShowCustomizeItem = false;


            ribbon.Items.Clear();
            ribbon.Items.Add(ribbon.ExpandCollapseItem);
        }

        /// <summary>
        /// 按钮工具栏加载
        /// </summary>
        /// <param name="buttonGroupModel"></param>
        /// <param name="ribbon"></param>
        /// <param name="ribbonPage"></param>
        /// <param name="resources"></param>
        public static void loadRibbonButton(List<ButtonGroupModel> buttonGroups, LiForm liForm, System.ComponentModel.ComponentResourceManager resources)
        {
            DevExpress.XtraBars.Ribbon.RibbonControl ribbon = liForm.ribbon;
            DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage = liForm.ribbonPage1;

            if (buttonGroups == null || buttonGroups.Count <= 0) return;

            ButtonGroupModel buttonGroupModel = buttonGroups[0];
            DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();

            ribbonPageGroup.AllowMinimize = buttonGroupModel.allowMinimize;
            ribbonPageGroup.AllowTextClipping = buttonGroupModel.allowTextClipping;
            ribbonPageGroup.Name = buttonGroupModel.name;
            ribbonPageGroup.Text = buttonGroupModel.text;
            ribbonPageGroup.MergeOrder = 1;

            List<ButtonModel> buttons = buttonGroupModel.buttons;
            //自动流式布局，大图标占整个，小图标，一行三个
            foreach (ButtonModel button in buttons)
            {
                DevExpress.XtraBars.BarButtonItem barButtonItem = new DevExpress.XtraBars.BarButtonItem();
                barButtonItem.Caption = button.caption;
                barButtonItem.CategoryGuid = new System.Guid(button.categoryGuid);
                //barButtonItem.Glyph = ((System.Drawing.Image)(liForm.resources.GetObject("barButtonItem1.Glyph")));
                barButtonItem.Id = button.id;
                barButtonItem.Name = button.name;
                barButtonItem.Tag = button;

                LiAEventMediator liAEventMediator = new LiEventFormMediator();
                liForm.liEventMediatorDict.Add(String.Format("{0}", button.name), liAEventMediator);
                ///为了发送事件
                LiAEvent liEvent = new LiEventItemClick(String.Format("{0}", button.name));
                liEvent.focusEntityKey = button.entityKey;
                liEvent.liForm = liForm;
                liEvent.Tag = button;
                liAEventMediator.register(liEvent);

                foreach (EventModel eventModel in button.events)
                {
                    LiAEvent obj = ReflectionUtil.CreateInstance<LiAEvent>(eventModel.fullName, eventModel.assemblyName);
                    obj.focusEntityKey = button.entityKey;
                    obj.liForm = liForm;
                    obj.Tag = button;
                    obj.id = button.name+eventModel.fullName;
                    liAEventMediator.register(obj);
                }

                barButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(liForm.barButtonItem_ItemClick);

                string[] iconNames = string.IsNullOrEmpty(button.icon) ? new string[0] : button.icon.Split('|');
                switch (button.iconsize)
                {
                    //大图标
                    case "All":
                    case "Default":
                    case "Large":
                        barButtonItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
                        if(iconNames.Length>1)
                            barButtonItem.ImageOptions.LargeImage = ImageUtil.getBitmap(iconNames[0], iconNames[1]);

                        break;
                    //小图标
                    case "SmallWithText":
                    case "SmallWithoutText":
                        barButtonItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
                        if(iconNames.Length>1)
                            barButtonItem.ImageOptions.Image = ImageUtil.getBitmap(iconNames[0], iconNames[1]);
                        break;
                }
                liForm.liButtonDict.Add(barButtonItem.Name, barButtonItem);

                ribbonPageGroup.ItemLinks.Add(barButtonItem);
                ribbon.Items.Add(barButtonItem);

            }

            ribbonPage.Groups.Clear();
            ribbonPage.Groups.Add(ribbonPageGroup);

            ribbon.Pages.Add(ribbonPage);
        }



        /// <summary>
        /// 列表按钮工具栏加载
        /// </summary>
        /// <param name="buttonGroupModel"></param>
        /// <param name="ribbon"></param>
        /// <param name="ribbonPage"></param>
        /// <param name="resources"></param>
        public static void loadListRibbonButton(List<ListButtonModel> buttons, LiListForm liListForm, System.ComponentModel.ComponentResourceManager resources)
        {
            DevExpress.XtraBars.Ribbon.RibbonControl ribbon = liListForm.ribbon;
            DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage = liListForm.ribbonPage1;

            DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();

            ribbonPageGroup.AllowMinimize = true;
            ribbonPageGroup.AllowTextClipping = true;
            ribbonPageGroup.Name = "List";
            ribbonPageGroup.Text = "列表";
            ribbonPageGroup.MergeOrder = 1;

            //自动流式布局，大图标占整个，小图标，一行三个
            foreach (ListButtonModel button in buttons)
            {
                DevExpress.XtraBars.BarButtonItem barButtonItem = new DevExpress.XtraBars.BarButtonItem();
                barButtonItem.Caption = button.caption;
                barButtonItem.CategoryGuid = new System.Guid(button.categoryGuid);
                barButtonItem.Glyph = ((System.Drawing.Image)(liListForm.resources.GetObject("barButtonItem1.Glyph")));
                barButtonItem.Id = button.id;
                barButtonItem.Name = button.name;
                barButtonItem.Tag = button;

                LiAEventMediator liAEventMediator = new LiEventFormMediator();
                liListForm.liEventMediatorDict.Add(String.Format("{0}", button.name), liAEventMediator);
                ///为了发送事件
                LiAEvent liEvent = new LiEventItemClick(String.Format("{0}", button.name));
                liEvent.Tag = button;
                liEvent.liListForm = liListForm;
                liAEventMediator.register(liEvent);

                foreach (EventModel eventModel in button.events)
                {
                    LiAEvent obj = ReflectionUtil.CreateInstance<LiAEvent>(eventModel.fullName, eventModel.assemblyName);
                    obj.Tag = button;
                    obj.liListForm = liListForm;
                    obj.id = button.name + eventModel.fullName;
                    liAEventMediator.register(obj);
                }

                barButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(liListForm.barButtonItem_ItemClick);

                string[] iconNames = string.IsNullOrEmpty(button.icon) ? new string[0] : button.icon.Split('|');
                switch (button.iconsize)
                {
                    //大图标
                    case "All":
                    case "Default":
                    case "Large":
                        barButtonItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
                        if (iconNames.Length > 1)
                            barButtonItem.ImageOptions.LargeImage = ImageUtil.getBitmap(iconNames[0], iconNames[1]);

                        break;
                    //小图标
                    case "SmallWithText":
                    case "SmallWithoutText":
                        barButtonItem.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
                        if (iconNames.Length > 1)
                            barButtonItem.ImageOptions.Image = ImageUtil.getBitmap(iconNames[0], iconNames[1]);
                        break;
                }
                liListForm.liButtonDict.Add(barButtonItem.Name, barButtonItem);

                ribbonPageGroup.ItemLinks.Add(barButtonItem);
                ribbon.Items.Add(barButtonItem);

            }

            ribbonPage.Groups.Clear();
            ribbonPage.Groups.Add(ribbonPageGroup);

            ribbon.Pages.Add(ribbonPage);
        }

        /// <summary>
        /// 设定容器在哪个地方
        /// </summary>
        /// <param name="dockStr"></param>
        /// <param name="dock"></param>
        public static DockStyle setDock(string dockStr)
        {
            switch (dockStr)
            {
                case "top":
                    return System.Windows.Forms.DockStyle.Top;
                case "bottom":
                    return System.Windows.Forms.DockStyle.Bottom;
                case "fill":
                    return System.Windows.Forms.DockStyle.Fill;
                case "left":
                    return System.Windows.Forms.DockStyle.Left;
                case "right":
                    return System.Windows.Forms.DockStyle.Right;
                default:
                    return System.Windows.Forms.DockStyle.Fill;
            }
        }

        /// <summary>
        /// 获取控件
        /// </summary>
        /// <param name="controlModel"></param>
        /// <returns></returns>
        public static Control getControl(ControlModel controlModel, LayoutControl layoutControl, LiForm liForm)
        {
            Control control = ControlModelUtil.getControl(controlModel);
            control.Name = controlModel.name;
            control.Tag = controlModel;

            switch (controlModel.controltype)
            {
                case "StatusEdit":
                case "GridLookUpEditComboBox":
                    GridLookUpEdit gridLookUpEditComboBox = (GridLookUpEdit)control;
                    GridView gridViewComboBox = new GridView();

                    gridViewComboBox.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                    gridViewComboBox.Name = string.Format("gridLookUpEditComboBoxView_{0}", control.Name);
                    gridViewComboBox.OptionsSelection.EnableAppearanceFocusedCell = false;
                    gridViewComboBox.OptionsView.ShowGroupPanel = false;

                    layoutControl.Controls.Add(gridLookUpEditComboBox);
                    gridLookUpEditComboBox.Properties.View = gridViewComboBox;

                    gridLookUpEditComboBox.StyleController = layoutControl;

                    GridlookUpEditShowModel gridlookUpEditShowModelComboBox_ComboBox = JsonUtil.GetEntity<GridlookUpEditShowModel>(controlModel.gridlookUpEditShowModelJson);
                    DataTable liComboBoxData = LiContexts.LiContext.getDictDataTable(controlModel.dictInfoType);
                    GridlookUpEditUtil.InitDefaultComboBoxControl(gridlookUpEditShowModelComboBox_ComboBox.valueMember, gridlookUpEditShowModelComboBox_ComboBox.displayMember, gridlookUpEditShowModelComboBox_ComboBox.searchColumns, gridlookUpEditShowModelComboBox_ComboBox.displayColumns, gridLookUpEditComboBox, liForm, liComboBoxData);

                    break;
                case "UserEdit":
                case "GridLookUpEditRef":
                    GridLookUpEdit gridLookUpEditRef = (GridLookUpEdit)control;
                    
                    GridView gridViewRef = new GridView();
                    gridViewRef.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                    gridViewRef.Name = string.Format("gridLookUpEditRefView_{0}", control.Name);
                    gridViewRef.OptionsSelection.EnableAppearanceFocusedCell = false;
                    gridViewRef.OptionsView.ShowGroupPanel = false;

                    layoutControl.Controls.Add(gridLookUpEditRef);
                    gridLookUpEditRef.Properties.View = gridViewRef;
                    gridLookUpEditRef.Properties.EditValueChanged += new System.EventHandler(liForm.gridLookUpEdit_Properties_EditValueChanged);

                    gridLookUpEditRef.StyleController = layoutControl;

                    GridlookUpEditShowModel gridlookUpEditShowModel = JsonUtil.GetEntity<GridlookUpEditShowModel>(controlModel.gridlookUpEditShowModelJson);
                    DataTable liRefData = LiContexts.LiContext.getRefDataDataTable(controlModel.basicInfoKey);

                    GridlookUpEditUtil.InitDefaultRefControl(GridlookUpEditShowModeUtil.getEnum(gridlookUpEditShowModel.showMode), gridlookUpEditShowModel.valueMember, gridlookUpEditShowModel.displayMember, gridlookUpEditShowModel.searchColumns, gridlookUpEditShowModel.displayColumns, gridlookUpEditShowModel.dictModelDesc, gridLookUpEditRef, liForm, liRefData);
                    break;
                case "TreeListLookUpEdit":
                    TreeListLookUpEdit treeListLookUpEdit = (TreeListLookUpEdit)control;
                    DataTable liTreeRefData = LiContexts.LiContext.getRefDataDataTable(controlModel.basicInfoKey);
                    treeListLookUpEdit.Properties.DataSource = liTreeRefData;
                    break;
                default:
                    break;
            }

            return control;
        }


        
        /// <summary>
        /// 获取表格控件
        /// </summary>
        /// <param name="controlModel"></param>
        /// <returns></returns>
        public static RepositoryItem getRepositoryItemControl(ControlModel controlModel, GridControl gridControl1, LiForm liForm)
        {

            RepositoryItem control = ControlModelUtil.getRepositoryItemControl(controlModel);
            control.Name = controlModel.name;
            control.Tag = controlModel;
            control.AutoHeight = false;

            switch (controlModel.controltype)
            {
                case "StatusEdit":
                case "GridLookUpEditComboBox":
                    RepositoryItemGridLookUpEdit gridLookUpEditComboBox = (RepositoryItemGridLookUpEdit)control;
                    GridView gridViewComboBox = new GridView();



                    gridViewComboBox.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                    gridViewComboBox.Name = string.Format("gridLookUpEditComboBoxView_{0}", control.Name);
                    gridViewComboBox.OptionsSelection.EnableAppearanceFocusedCell = false;
                    gridViewComboBox.OptionsView.ShowGroupPanel = false;

                    gridControl1.RepositoryItems.Add(gridLookUpEditComboBox);
                    gridLookUpEditComboBox.View = gridViewComboBox;

                    
                    GridlookUpEditShowModel gridlookUpEditShowModelComboBox_ComboBox = JsonUtil.GetEntity<GridlookUpEditShowModel>(controlModel.gridlookUpEditShowModelJson);
                    DataTable liComboBoxData = LiContexts.LiContext.getDictDataTable(controlModel.dictInfoType);
                    GridlookUpEditRepositoryItemUtil.InitDefaultComboBoxControl(gridlookUpEditShowModelComboBox_ComboBox.valueMember, gridlookUpEditShowModelComboBox_ComboBox.displayMember, gridlookUpEditShowModelComboBox_ComboBox.searchColumns, gridlookUpEditShowModelComboBox_ComboBox.displayColumns, gridLookUpEditComboBox, liForm, liComboBoxData);

                    break;
                case "UserEdit":
                case "GridLookUpEditRef":
                    RepositoryItemGridLookUpEdit gridLookUpEditRef = (RepositoryItemGridLookUpEdit)control;

                    GridView gridViewRef = new GridView();
                    gridViewRef.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                    gridViewRef.Name = string.Format("gridLookUpEditRefView_{0}", control.Name);
                    gridViewRef.OptionsSelection.EnableAppearanceFocusedCell = false;
                    gridViewRef.OptionsView.ShowGroupPanel = false;

                    gridControl1.RepositoryItems.Add(gridLookUpEditRef);
                    gridLookUpEditRef.View = gridViewRef;

                    
                    GridlookUpEditShowModel gridlookUpEditShowModel = JsonUtil.GetEntity<GridlookUpEditShowModel>(controlModel.gridlookUpEditShowModelJson);
                    DataTable liRefData = LiContexts.LiContext.getRefDataDataTable(controlModel.basicInfoKey);
                    GridlookUpEditRepositoryItemUtil.InitDefaultRefControl(GridlookUpEditShowModeUtil.getEnum(gridlookUpEditShowModel.showMode), gridlookUpEditShowModel.valueMember, gridlookUpEditShowModel.displayMember, gridlookUpEditShowModel.searchColumns, gridlookUpEditShowModel.displayColumns, gridlookUpEditShowModel.dictModelDesc, gridLookUpEditRef, liForm, liRefData);
                    break;
                case "TreeListLookUpEdit":
                    RepositoryItemTreeListLookUpEdit treeListLookUpEdit = (RepositoryItemTreeListLookUpEdit)control;
                    DataTable liTreeRefData = LiContexts.LiContext.getRefDataDataTable(controlModel.basicInfoKey);
                    treeListLookUpEdit.DataSource = liTreeRefData;
                    break;
                default:
                    break;
            }
            return control;
        }
        /// <summary>
        /// 获取每组的控件，等于整个页签上的绘制
        /// </summary>
        /// <param name="controlGroupModel"></param>
        /// <param name="layoutControlGroup"></param>
        public static void setBasicLayoutControlGroup(ControlGroupModel controlGroupModel, LayoutControlGroup layoutControlGroup,LayoutControl layoutControl, LiForm liForm )
        {
            int rowSpacing = 10;
            layoutControlGroup.Name = string.Format("layoutControlGroup_{0}", controlGroupModel.name);


            List<ControlModel> controls = controlGroupModel.controls;
            var groups = controls.OrderBy(m => m.row).ThenBy(m => m.col).GroupBy(m => m.row);
            foreach (var grouprow in groups)
            {
                int i = 0;
                LayoutControlItem layoutControlItemPrevious = null;
                foreach (var groupcol in grouprow)
                {
                    if (!groupcol.bVisible) continue;

                    LayoutControlItem layoutControlItem = layoutControlGroup.AddItem();
                    //是否自动分配宽度
                    if (!controlGroupModel.autoAllocation)
                    {
                        //只 要后面为假，其他分配就不行
                        layoutControlItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
                        layoutControlItem.MaxSize = new System.Drawing.Size(groupcol.width, groupcol.height+ rowSpacing);
                    }

                    layoutControlItem.Control = getControl(groupcol, layoutControl,liForm);
                    layoutControlItem.Text = groupcol.bRequired ? string.Format("{0}(*)", groupcol.text) : groupcol.text;
                    layoutControlItem.AppearanceItemCaption.ForeColor = groupcol.bRequired ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                    layoutControlItem.Name = groupcol.name;
                    layoutControlItem.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);

                    liForm.liControlDict.Add(groupcol.name, layoutControlItem.Control);
                    switch(groupcol.controltype)
                    {
                        case "UserEdit":
                        case "GridLookUpEditRef":
                        case "TreeListLookUpEdit":
                            liForm.liControlRefDict.Add(groupcol.name, layoutControlItem.Control);
                            break;
                        case "GridLookUpEditRefAssist":
                            if (!liForm.liControlRefAssistDict.ContainsKey(groupcol.basicInfoKey))
                            {
                                liForm.liControlRefAssistDict.Add(groupcol.basicInfoKey, new List<Control>());
                            }
                            liForm.liControlRefAssistDict[groupcol.basicInfoKey].Add( layoutControlItem.Control);
                            break;
                    }

                    //如果不是第一个，都放在前一个后面
                    if (i != 0)
                    {
                        layoutControlItem.Move(layoutControlItemPrevious, InsertType.Right);
                    }

                    //保存前一个
                    layoutControlItemPrevious = layoutControlItem;
                    i++;
                }
            }

        }

        /// <summary>
        /// 基本信息的绘制
        /// </summary>
        /// <param name="panelModel"></param>
        /// <returns></returns>
        public static DevExpress.XtraLayout.LayoutControl getBasicLayout(PanelModel panelModel, LiForm liForm )
        {
            //流式布局
            liForm.mainTableName = panelModel.tableName;

            DevExpress.XtraLayout.LayoutControl layoutControl = new DevExpress.XtraLayout.LayoutControl();
            DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();

            layoutControl.BeginUpdate();

            setLayoutControl(panelModel, layoutControl, layoutControlGroup);

            //绘制每个页签
            List<ControlGroupModel> controlGroupModels = panelModel.controlGroups;
            if (controlGroupModels.Count > 1)
            {
                TabbedControlGroup tabbedControlGroup = layoutControl.Root.AddTabbedGroup();
                tabbedControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);

                foreach (ControlGroupModel controlGroupModel in controlGroupModels)
                {
                    LayoutControlGroup childLayoutControlGroup = tabbedControlGroup.AddTabPage(controlGroupModel.text);

                    if (liForm.formModel.formType == VoucherType.Voucher || liForm.formModel.formType == VoucherType.BasicInfo)
                    {
                        setBasicLayoutControlGroup(controlGroupModel, childLayoutControlGroup, layoutControl, liForm);
                    }
                    else if (liForm.formModel.formType == VoucherType.TreeBasicInfo)
                    {
                        setTreeListControlGroup(controlGroupModel, layoutControlGroup, liForm.dockPanelEditSaveEventHandler, liForm.dockPanelEditCloseEventHandler, liForm);
                    }
                }
            }
            else
            {

                if (liForm.formModel.formType == VoucherType.Voucher || liForm.formModel.formType == VoucherType.BasicInfo)
                {
                    setBasicLayoutControlGroup(controlGroupModels[0], layoutControlGroup, layoutControl, liForm);
                }
                else if (liForm.formModel.formType == VoucherType.TreeBasicInfo)
                {
                    setTreeListControlGroup(controlGroupModels[0], layoutControlGroup, liForm.dockPanelEditSaveEventHandler, liForm.dockPanelEditCloseEventHandler, liForm);
                }
            }

            layoutControl.EndUpdate();
            return layoutControl;
        }

        /// <summary>
        /// 设置流式布局
        /// </summary>
        /// <param name="panelModel"></param>
        /// <param name="layoutControl"></param>
        /// <param name="layoutControlGroup"></param>
        public static void setLayoutControl(PanelModel panelModel, LayoutControl layoutControl, LayoutControlGroup layoutControlGroup)
        {
            layoutControl.Controls.Clear();
            layoutControl.Root.Items.Clear();
            layoutControl.Dock = setDock(panelModel.dock);

            layoutControl.Name = string.Format("layoutControl_{0}", panelModel.name);
            layoutControl.Root = layoutControlGroup;
            layoutControl.Size = new System.Drawing.Size(panelModel.width, panelModel.height);
            layoutControl.Text = string.Format("layoutControl_{0}", panelModel.text);


            layoutControlGroup.CustomizationFormText = string.Format("layoutControlGroup_{0}", panelModel.name);
            layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup.GroupBordersVisible = false;
            layoutControlGroup.Name = string.Format("layoutControlGroup_{0}", panelModel.name);
            layoutControlGroup.Text = string.Format("layoutControlGroup_{0}", panelModel.name);
            layoutControlGroup.TextVisible = false;
            layoutControlGroup.AppearanceItemCaption.Options.UseTextOptions = true;
            layoutControlGroup.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            layoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(15, 15, 15, 15);

        }

        /// <summary>
        /// 表格的绘制
        /// </summary>
        /// <param name="panelModel"></param>
        /// <returns></returns>
        public static DevExpress.XtraLayout.LayoutControl getGridLayout(PanelModel panelModel, LiForm liForm )
        {
            //流式布局
            DevExpress.XtraLayout.LayoutControl layoutControl = new DevExpress.XtraLayout.LayoutControl();
            DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();

            layoutControl.BeginUpdate();
            setLayoutControl(panelModel, layoutControl, layoutControlGroup);

            //绘制每个页签
            List<ControlGroupModel> controlGroupModels = panelModel.controlGroups;
            if (controlGroupModels.Count > 1)
            {
                TabbedControlGroup tabbedControlGroup = layoutControl.Root.AddTabbedGroup();
                tabbedControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);

                foreach (ControlGroupModel controlGroupModel in controlGroupModels)
                {
                    LayoutControlGroup childLayoutControlGroup = tabbedControlGroup.AddTabPage(controlGroupModel.text);
                    setGridLayoutControlGroup(controlGroupModel, childLayoutControlGroup, liForm);
                }
            }
            else
            {
                setGridLayoutControlGroup(controlGroupModels[0], layoutControlGroup, liForm);
            }

            //添加子表主键到字典
            foreach(ControlGroupModel controlGroupModel in panelModel.controlGroups)
            {
                if(!liForm.liGridPrimaryKeyDict.ContainsKey(controlGroupModel.name))
                    liForm.liGridPrimaryKeyDict.Add(controlGroupModel.name, panelModel.primaryKeyName);
            }

            layoutControl.EndUpdate();
            return layoutControl;
        }

        
        /// <summary>
        /// 绘制表格
        /// </summary>
        /// <param name="controlGroupModel"></param>
        /// <param name="layoutControlGroup"></param>
        /// <param name="liForm"></param>
        public static void setTreeListControlGroup(ControlGroupModel controlGroupModel, LayoutControlGroup layoutControlGroup,EventHandler saveEventHandler,EventHandler closeEventHandler, LiForm liForm)
        {
            layoutControlGroup.Name = controlGroupModel.name;
            layoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);

            List<ControlModel> controls = controlGroupModel.controls.OrderBy(m => m.col).ToList();

            LayoutControlItem layoutControlItem = layoutControlGroup.AddItem();
            layoutControlItem.TextVisible = false;

            //表格控件
            DevExpress.XtraEditors.PanelControl panelControl = new DevExpress.XtraEditors.PanelControl();


            DevExpress.XtraTreeList.TreeList treeList = new DevExpress.XtraTreeList.TreeList();

            panelControl.Controls.Add(treeList);

            treeList.BeginInit();

            liForm.liTreeListDict.Add(liForm.formCode, treeList);
            panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl.Name = string.Format("panelControl_{0}", controlGroupModel.name);

            treeList.Name = "treeList1";
            treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsView.AutoWidth = false;

            if (controlGroupModel.buttonGroups != null && controlGroupModel.buttonGroups.Count > 0 && controlGroupModel.buttonGroups[0].buttons != null && controlGroupModel.buttonGroups[0].buttons.Count > 0)
            {
                setPanelToolButton(controlGroupModel.buttonGroups[0], panelControl, liForm);
            }

            layoutControlItem.Control = panelControl;
            
            //加载列
            foreach (ControlModel control in controls)
            {
                if (!control.bVisible) continue;

                DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();

                treeListColumn.Caption = control.bRequired ? string.Format("{0}(*)", control.text) : control.text;
                treeListColumn.AppearanceHeader.ForeColor = control.bRequired ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                treeListColumn.AppearanceHeader.Options.UseForeColor = control.bRequired;
                treeListColumn.FieldName = control.name;
                treeListColumn.Name = string.Format("treeListColumn_{0}", control.name);
                treeListColumn.Visible = true;
                treeListColumn.VisibleIndex = control.col;
                treeListColumn.Width = control.width;
                treeListColumn.OptionsColumn.ReadOnly = control.bReadOnly;
                treeListColumn.Tag = control;

                liForm.liTreeListColumnDict.Add(control.name, treeListColumn);
                treeList.Columns.Add(treeListColumn);
            }

            treeList.EndInit();

            treeList.Refresh();


            DevExpress.XtraLayout.LayoutControl layoutControlLeft = FormUtil.setDockPanelByBasicInfo(saveEventHandler, closeEventHandler, liForm.liDockPanelDict["dockPanel_Edit"]);

            DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupLeft = layoutControlLeft.Root;

            //树形控件
            TreeListLookUpEdit listEditParentID = new TreeListLookUpEdit();
            TreeList treeListLookUpEdit1TreeList = new TreeList();
            treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            treeListLookUpEdit1TreeList.OptionsView.ShowIndentAsRowStyle = true;
            treeListLookUpEdit1TreeList.Columns.AddRange(liForm.liTreeListColumnDict.Values.ToArray());

            listEditParentID.EditValue = "父节点";
            listEditParentID.Name = "listEditParentID";
            listEditParentID.Properties.NullText = "";
            listEditParentID.Properties.PopupSizeable = false;
            listEditParentID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            listEditParentID.Properties.TreeList = treeListLookUpEdit1TreeList;
            listEditParentID.StyleController = layoutControlLeft;
            listEditParentID.Properties.ValueMember = "ID";
            listEditParentID.Properties.DisplayMember = "Code";
            listEditParentID.Properties.TreeList.KeyFieldName = "ID";
            listEditParentID.Properties.TreeList.ParentFieldName = "ParentID";



            LayoutControlItem layoutControlItemLeft = layoutControlGroupLeft.AddItem();
            layoutControlItemLeft.Control = listEditParentID;
            layoutControlItemLeft.Text ="父项";
            layoutControlItemLeft.Name = "layoutControlItemParentID";
            liForm.liControlDict.Add("ParentID", layoutControlItemLeft.Control);

            foreach (ControlModel customerControlModel in controls)
            {
                layoutControlItemLeft = layoutControlGroupLeft.AddItem();
                layoutControlItemLeft.Control = ControlModelUtil.getControl(customerControlModel.controltype);
                layoutControlItemLeft.Text = customerControlModel.text;
                layoutControlItemLeft.Name = customerControlModel.name;

                switch (customerControlModel.controltype)
                {
                    case "UserEdit":
                    case "GridLookUpEditRef":
                    case "TreeListLookUpEdit":
                        liForm.liControlRefDict.Add(customerControlModel.name, layoutControlItemLeft.Control);
                        break;
                    case "GridLookUpEditRefAssist":
                        if (!liForm.liControlRefAssistDict.ContainsKey(customerControlModel.basicInfoKey))
                        {
                            liForm.liControlRefAssistDict.Add(customerControlModel.basicInfoKey, new List<Control>());
                        }
                        liForm.liControlRefAssistDict[customerControlModel.basicInfoKey].Add(layoutControlItemLeft.Control);
                        break;
                }


                liForm.liControlDict.Add(customerControlModel.name, layoutControlItemLeft.Control);

            }

            //layoutControlLeft.EndInit();
        }

        /// <summary>
        /// 绘制表格
        /// </summary>
        /// <param name="controlGroupModel"></param>
        /// <param name="layoutControlGroup"></param>
        /// <param name="liForm"></param>
        public static void setGridLayoutControlGroup(ControlGroupModel controlGroupModel, LayoutControlGroup layoutControlGroup, LiForm liForm )
        {
            layoutControlGroup.Name = controlGroupModel.name;
            layoutControlGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);

            List<ControlModel> controls = controlGroupModel.controls.OrderBy(m => m.col).ToList();

            LayoutControlItem layoutControlItem = layoutControlGroup.AddItem();
            layoutControlItem.TextVisible = false;

            //表格控件
            DevExpress.XtraEditors.PanelControl panelControl = new DevExpress.XtraEditors.PanelControl();
            DevExpress.XtraGrid.GridControl gridControl = new DevExpress.XtraGrid.GridControl();
            DevExpress.XtraGrid.Views.Grid.GridView gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            gridControl.BeginInit();
            gridView.BeginInit();

            liForm.liRowFieldDict.Add(controlGroupModel.name, controlGroupModel.rowFieldName);
            liForm.liGridControlDict.Add(controlGroupModel.name, gridControl);
            liForm.liGridViewDict.Add(controlGroupModel.name, gridView);
            // 
            // panelControl1
            // 
            panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            //panelControl1.Location = new System.Drawing.Point(0, 267);
            panelControl.Name = string.Format("panelControl_{0}", controlGroupModel.name);
            //panelControl1.Size = new System.Drawing.Size(834, 250);
            panelControl.TabIndex = 6;

            if (controlGroupModel.buttonGroups != null && controlGroupModel.buttonGroups.Count>0 && controlGroupModel.buttonGroups[0].buttons != null && controlGroupModel.buttonGroups[0].buttons.Count > 0)
            {
                setPanelToolButton(controlGroupModel.buttonGroups[0], panelControl, liForm);
            }
            // 
            // gridControl1
            // 
            gridControl.Cursor = System.Windows.Forms.Cursors.Default;
            gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl.MainView = gridView;
            //gridControl1.MenuManager = this.ribbon;
            gridControl.Name = string.Format("gridControl_{0}", controlGroupModel.name);
            //gridControl1.Size = new System.Drawing.Size(830, 246);
            gridControl.TabIndex = 0;
            // 
            // gridView1
            // 
            gridView.GridControl = gridControl;
            gridView.Name = string.Format("gridView_{0}", controlGroupModel.name);
            gridView.OptionsView.ColumnAutoWidth = false;
            gridView.OptionsView.ShowGroupPanel = false;
            gridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(liForm.gridView_CellValueChanged);
            panelControl.Controls.Clear();
            gridControl.ViewCollection.Clear();

            panelControl.Controls.Add(gridControl);
            gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView});



            layoutControlItem.Control = panelControl;

            //加载列
            foreach (ControlModel control in controls)
            {
                if (!control.bVisible) continue;

                GridColumn gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();

                gridColumn.Caption = control.bRequired ? string.Format("{0}(*)", control.text) : control.text;
                gridColumn.AppearanceHeader.ForeColor = control.bRequired ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                gridColumn.AppearanceHeader.Options.UseForeColor = control.bRequired;
                gridColumn.Name = control.name;
                gridColumn.FieldName = control.name;
                gridColumn.Visible = true;
                gridColumn.VisibleIndex = control.col;
                gridColumn.Width = control.width;
                gridColumn.OptionsColumn.ReadOnly = control.bReadOnly;
                gridColumn.ColumnEdit = getRepositoryItemControl( control,gridControl,liForm );
                gridColumn.Tag = control;

                liForm.liGridColumnDict.Add(control.name, gridColumn);
                liForm.liRepositoryItemDict.Add(control.name, gridColumn.ColumnEdit);
                switch (control.controltype)
                {
                    case "UserEdit":
                    case "GridLookUpEditRef":
                    case "TreeListLookUpEdit":
                        liForm.liGridColumnRefDict.Add(control.name, gridColumn);
                        liForm.liRepositoryItemRefDict.Add(control.name, gridColumn.ColumnEdit);
                        break;
                    case "GridLookUpEditRefAssist":
                        
                        if (!liForm.liGridColumnRefAssistDict.ContainsKey(control.basicInfoKey))
                        {
                            liForm.liGridColumnRefAssistDict.Add(control.basicInfoKey, new List<GridColumn>());
                        }
                        liForm.liGridColumnRefAssistDict[control.basicInfoKey].Add(gridColumn);

                        if (!liForm.liRepositoryItemRefAssistDict.ContainsKey(control.basicInfoKey))
                        {
                            liForm.liRepositoryItemRefAssistDict.Add(control.basicInfoKey, new List<RepositoryItem>());
                        }
                        liForm.liRepositoryItemRefAssistDict[control.basicInfoKey].Add(gridColumn.ColumnEdit);
                        break;
                }

                gridView.Columns.Add(gridColumn);

            }


            gridView.EndInit();
            gridControl.EndInit();

            gridControl.Refresh();
        }

        /// <summary>
        /// 容器工具栏按钮
        /// </summary>
        /// <param name="buttonGroupModel"></param>
        /// <param name="panelControl"></param>
        /// <param name="liForm"></param>
        public static void setPanelToolButton(ButtonGroupModel buttonGroupModel, Control panelControl,LiForm liForm  )
        {
            DevExpress.XtraBars.BarManager barManager;
            DevExpress.XtraBars.Bar bar;

            barManager = new DevExpress.XtraBars.BarManager(liForm.components);
            bar = new DevExpress.XtraBars.Bar();

            barManager.BeginInit();


            barManager.Bars.Add(bar);

            barManager.Form = panelControl;
            barManager.MainMenu = bar;
            barManager.MaxItemId = 1;

            bar.BarName = "Main menu";
            bar.DockCol = 0;
            bar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            bar.OptionsBar.MultiLine = true;
            bar.OptionsBar.UseWholeRow = true;
            bar.Text = "Main menu";
            bar.OptionsBar.DrawBorder = false;
            bar.OptionsBar.DrawDragBorder = false;

            List<ButtonModel> buttons = buttonGroupModel.buttons;
            foreach (ButtonModel button in buttons)
            {
                string[] iconNames = string.IsNullOrEmpty(button.icon) ? new string[0] : button.icon.Split('|');

                DevExpress.XtraBars.BarButtonItem barButtonItem = new DevExpress.XtraBars.BarButtonItem();
                barButtonItem.Caption = button.caption;
                barButtonItem.ImageOptions.Image = ImageUtil.getBitmap16(iconNames[0], iconNames[1]);
                //barButtonItem.ImageOptions.LargeImage = ImageUtil.getBitmap(iconNames[0], iconNames[1]);
                barButtonItem.Id = 0;
                barButtonItem.Tag = button;
                barButtonItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(liForm.barButtonItem_ItemClick);
                barButtonItem.Name = button.name;
                barButtonItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

                LiAEventMediator liAEventMediator = new LiEventFormMediator();
                liForm.liEventMediatorDict.Add(String.Format("{0}", button.name), liAEventMediator);
                ///为了发送事件
                LiAEvent liEvent = new LiEventItemClick(String.Format("{0}", button.name));
                liEvent.focusEntityKey = button.entityKey;
                liEvent.liForm = liForm;
                liAEventMediator.register(liEvent);

                foreach (EventModel eventModel in button.events)
                {
                    LiAEvent obj = ReflectionUtil.CreateInstance<LiAEvent>(eventModel.fullName, eventModel.assemblyName);
                    obj.focusEntityKey = button.entityKey;
                    obj.liForm = liForm;
                    obj.id = button.name + eventModel.fullName;
                    liAEventMediator.register(obj);
                }

                liForm.liButtonDict.Add(button.name, barButtonItem);
                barManager.Items.Add(barButtonItem);
                bar.LinksPersistInfo.Add(new DevExpress.XtraBars.LinkPersistInfo(barButtonItem));
            }


            barManager.EndInit();
        }


        /// <summary>
        /// 初始化基础资料编辑的浮动框
        /// </summary>
        /// <param name="dockPanel"></param>
        /// <returns></returns>
        public static DevExpress.XtraLayout.LayoutControl setDockPanelByBasicInfo(EventHandler saveEventHandler, EventHandler closeEventHandler, Control dockPanel)
        {

            DevExpress.XtraLayout.LayoutControl layoutControl1;
            DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
            DevExpress.XtraEditors.PanelControl panelControl1;
            DevExpress.XtraLayout.LayoutControl layoutControl2;
            DevExpress.XtraEditors.SimpleButton simpleButton2;
            DevExpress.XtraEditors.SimpleButton simpleButton1;
            DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;


            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();

            //DevExpress.XtraBars.Docking.ControlContainer dockPanel_Container = (DevExpress.XtraBars.Docking.ControlContainer)dockPanel.Controls[0];
            // 
            // dockPanel3_Container
            // 
            dockPanel.Controls.Add(layoutControl1);
            dockPanel.Controls.Add(panelControl1);

            // 
            // layoutControl1
            // 
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            //layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = layoutControlGroup1;
            //layoutControl1.Size = new System.Drawing.Size(214, 297);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            //layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "layoutControlGroup1";
            //layoutControlGroup1.Size = new System.Drawing.Size(214, 306);
            layoutControlGroup1.TextVisible = false;
            layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
            layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(15, 15, 15, 15);
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(layoutControl2);
            panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            //panelControl1.Location = new System.Drawing.Point(0, 297);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new System.Drawing.Size(214, 70);
            panelControl1.TabIndex = 0;
            // 
            // layoutControl2
            // 
            layoutControl2.Controls.Add(simpleButton2);
            layoutControl2.Controls.Add(simpleButton1);
            layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            //layoutControl2.Location = new System.Drawing.Point(2, 2);
            layoutControl2.Name = "layoutControl2";
            //layoutControl2.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1301, 245, 450, 400);
            layoutControl2.OptionsView.AutoSizeInLayoutControl = DevExpress.XtraLayout.AutoSizeModes.ResizeToMinSize;
            layoutControl2.Root = layoutControlGroup2;
            //layoutControl2.Size = new System.Drawing.Size(210, 66);
            layoutControl2.TabIndex = 0;
            layoutControl2.Text = "layoutControl2";
            // 
            // simpleButton2
            // 
            //simpleButton2.ImageOptions.Image = global::LiManage.Properties.Resources.save_32x324;
            //simpleButton2.Location = new System.Drawing.Point(12, 14);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new System.Drawing.Size(91, 38);
            simpleButton2.StyleController = layoutControl2;
            simpleButton2.Click += saveEventHandler;
            simpleButton2.TabIndex = 5;
            simpleButton2.Text = "保存";
            // 
            // simpleButton1
            // 
            //simpleButton1.ImageOptions.Image = global::LiManage.Properties.Resources.close_32x322;
            //simpleButton1.Location = new System.Drawing.Point(107, 14);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new System.Drawing.Size(91, 38);
            simpleButton1.StyleController = layoutControl2;
            simpleButton1.Click += closeEventHandler;
            simpleButton1.TabIndex = 4;
            simpleButton1.Text = "关闭";
            // 
            // layoutControlGroup2
            // 
            layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup2.GroupBordersVisible = false;
            layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            layoutControlItem2,
            layoutControlItem1});
            //layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup2.Name = "Root";
            //layoutControlGroup2.Size = new System.Drawing.Size(210, 66);
            layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = simpleButton1;
            layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            //layoutControlItem1.Location = new System.Drawing.Point(95, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            //layoutControlItem1.Size = new System.Drawing.Size(95, 46);
            //layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            layoutControlItem1.TrimClientAreaToControl = false;

            //layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            //layoutControlItem1.MaxSize = new System.Drawing.Size(90, 60);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = simpleButton2;
            layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            //layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            layoutControlItem2.Name = "layoutControlItem2";
            //layoutControlItem2.Size = new System.Drawing.Size(95, 46);
            //layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem2.TextVisible = false;
            layoutControlItem2.TrimClientAreaToControl = false;

            layoutControlItem1.Move(layoutControlItem2, DevExpress.XtraLayout.Utils.InsertType.Right);
            //layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            //layoutControlItem2.MaxSize = new System.Drawing.Size(90, 60);

            return layoutControl1;
        }

    }
}

/// <summary>
/// 加载皮肤按钮
/// </summary>
/// <param name="liForm"></param>
//public static void loadSkinButton(RibbonControl ribbon, RibbonPage ribbonPage, LiForm liForm)
//{

//    DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
//    RibbonGalleryBarItem ribbonGalleryBarItem1 = new DevExpress.XtraBars.RibbonGalleryBarItem();
//    DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();

//    galleryItemGroup1.Caption = "Group1";

//    ribbonPageGroup1.Name = "ribbonPageGroup1";
//    ribbonPageGroup1.Text = "界面皮肤";
//    //这两个很重要，决定是否显示
//    ribbonPageGroup1.ItemLinks.Add(ribbonGalleryBarItem1);
//    ribbon.Items.Add(ribbonGalleryBarItem1);


//    DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(ribbonGalleryBarItem1, true);
//    ribbonGalleryBarItem1.Caption = "ribbonGalleryBarItem1";
//    ribbonGalleryBarItem1.CategoryGuid = new System.Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
//    ribbonGalleryBarItem1.Id = 1;
//    ribbonGalleryBarItem1.Name = "ribbonGalleryBarItem1";
//    ribbonGalleryBarItem1.Gallery.Groups.Add(galleryItemGroup1);
//    ribbonGalleryBarItem1.GalleryItemClick += liForm.RibbonGalleryBarItem1_ItemClick;


//    ribbonPage.Groups.Add(ribbonPageGroup1);
//    //ribbon.Items.
//}