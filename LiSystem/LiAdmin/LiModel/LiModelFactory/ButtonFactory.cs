using LiModel.Form;
using LiModel.LiEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiModelFactory
{
    public class ButtonFactory
    {
        public static ButtonModel getFormButtonModel(string buttonType, int buttonGroupId, string categoryGuid, string entityKey = "")
        {
            ButtonModel buttonModel = ButtonModel.getInstance(buttonGroupId, categoryGuid);
            EventModel eventModel = EventModel.getInstance(0, 0, 0, buttonModel.id, 0);
            eventModel.assemblyName = "LiForm";

            switch (buttonType)
            {
                case ButtonType.BTNNEW:
                    buttonModel.caption = ButtonType.BTNNEW;
                    buttonModel.name = "btnNew";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_add_32x32.png";
                    buttonModel.voucherStatus = "NewStatus";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "billStatus";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventNew";
                    break;
                case ButtonType.BTNEDIT:
                    buttonModel.caption = ButtonType.BTNEDIT;
                    buttonModel.name = "btnEdit";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_Modify_32x32.png";
                    buttonModel.voucherStatus = "EditStatus";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "billStatus";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventEdit";
                    break;
                case ButtonType.BTNSAVE:
                    buttonModel.caption = ButtonType.BTNSAVE;
                    buttonModel.name = "btnSave";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_save_32x32.png";
                    buttonModel.voucherStatus = "ShowStatus";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "billStatus";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventSave";
                    break;
                case ButtonType.BTNDELETE:
                    buttonModel.caption = ButtonType.BTNDELETE;
                    buttonModel.name = "btnDelete";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_delete_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventDelete";
                    break;
                case ButtonType.BTNSUBMIT:
                    buttonModel.caption = ButtonType.BTNSUBMIT;
                    buttonModel.name = "btnSubmit";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_Submit_32x32.png";
                    buttonModel.voucherStatus = "SubmitStatus";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "billStatus";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventSubmit";
                    break;
                case ButtonType.BTNUNSUBMIT:
                    buttonModel.caption = ButtonType.BTNUNSUBMIT;
                    buttonModel.name = "btnUnSubmit";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_UnSubmit_32x32.png";
                    buttonModel.voucherStatus = "EditStatus";
                    buttonModel.bClearStatus = true;
                    buttonModel.previousVoucherStatus = "SubmitStatus";
                    buttonModel.statusFieldName = "billStatus";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventUnSubmit";
                    break;
                case ButtonType.BTNAUDIT:
                    buttonModel.caption = ButtonType.BTNAUDIT;
                    buttonModel.name = "btnAudit";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_approve_32x32.png";
                    buttonModel.voucherStatus = "AuditStatus";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "billStatus";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventAudit";
                    break;
                case ButtonType.BTNUNAUDIT:
                    buttonModel.caption = ButtonType.BTNUNAUDIT;
                    buttonModel.name = "btnUnAudit";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_Unapprove_32x32.png";
                    buttonModel.voucherStatus = "SubmitStatus";
                    buttonModel.bClearStatus = true;
                    buttonModel.previousVoucherStatus = "AuditStatus";
                    buttonModel.statusFieldName = "billStatus";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventUnAudit";
                    break;
                case ButtonType.BTNREF:
                    buttonModel.caption = ButtonType.BTNREF;
                    buttonModel.name = "btnRef";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_Refer_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventRef";
                    break;
                case ButtonType.BTNPUSH:
                    buttonModel.caption = ButtonType.BTNPUSH;
                    buttonModel.name = "btnPush";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_push creating_16x16.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventPush";
                    break;
                case ButtonType.BTNEXIT:
                    buttonModel.caption = ButtonType.BTNEXIT;
                    buttonModel.name = "btnExit";
                    buttonModel.iconsize = "Large";
                    buttonModel.icon = "U8|U8_Exit_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventExit";
                    break;
                case ButtonType.BTNADDROW:
                    buttonModel.caption = ButtonType.BTNADDROW;
                    buttonModel.name = "btnAddRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_Add a row_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventNewRowData";
                    break;
                case ButtonType.BTNDELETEROW:
                    buttonModel.caption = ButtonType.BTNDELETEROW;
                    buttonModel.name = "btnDeleteRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_Delete a row_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventDeleteRowData";
                    break;
                case ButtonType.BTNINSERTROW:
                    buttonModel.caption = ButtonType.BTNINSERTROW;
                    buttonModel.name = "btnInsertRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_insert row_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventInsertRowData";
                    break;
                case ButtonType.BTNCOPYROW:
                    buttonModel.caption = ButtonType.BTNCOPYROW;
                    buttonModel.name = "btnCopyRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_copy a row_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventCopyRowData";
                    break;
                case ButtonType.BTNINSERTCOPYROW:
                    buttonModel.caption = ButtonType.BTNINSERTCOPYROW;
                    buttonModel.name = "btnInsertCopyRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_insert row_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventInsertCopyRowData";
                    break;
                case ButtonType.BTNADDCOPYROW:
                    buttonModel.caption = ButtonType.BTNADDCOPYROW;
                    buttonModel.name = "btnAddCopyRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_Add a row_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventAddCopyRowData";
                    break;
                case ButtonType.BTNUPROW:
                    buttonModel.caption = ButtonType.BTNUPROW;
                    buttonModel.name = "btnUpRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_up_16x16.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventUpRowData";
                    break;
                case ButtonType.BTNDOWNROW:
                    buttonModel.caption = ButtonType.BTNDOWNROW;
                    buttonModel.name = "btnDownRow";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_down_16x16.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventDownRowData";
                    break;
                case ButtonType.BTNPREVIOUS:
                    buttonModel.caption = ButtonType.BTNPREVIOUS;
                    buttonModel.name = "btnPrevious";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_previous page_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventPrevious";
                    break;
                case ButtonType.BTNFIRST:
                    buttonModel.caption = ButtonType.BTNFIRST;
                    buttonModel.name = "btnFirst";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_first page_16x16.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventFirst";
                    break;
                case ButtonType.BTNLAST:
                    buttonModel.caption = ButtonType.BTNLAST;
                    buttonModel.name = "btnLast";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_Last page_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventLast";
                    break;
                case ButtonType.BTNNEXT:
                    buttonModel.caption = ButtonType.BTNNEXT;
                    buttonModel.name = "btnNext";
                    buttonModel.iconsize = "Large";
                    buttonModel.entityKey = entityKey;
                    buttonModel.icon = "U8|U8_Next page_32x32.png";
                    buttonModel.voucherStatus = "";
                    buttonModel.bClearStatus = false;
                    buttonModel.previousVoucherStatus = "";
                    buttonModel.statusFieldName = "";
                    eventModel.fullName = "LiForm.Event.EventForm.LiEventNext";
                    break;
            }

            buttonModel.events.Add(eventModel);
            return buttonModel;
        }

        public static ListButtonModel getListButtonModel(string buttonType, int formId, string categoryGuid, string entityKey = "")
        {
            ListButtonModel listButtonModel = ListButtonModel.getInstance(formId, categoryGuid);
            EventModel eventModel = EventModel.getInstance(0, 0, 0, 0, listButtonModel.id);
            eventModel.assemblyName = "LiForm";
            switch (buttonType)
            {
                case ListButtonType.BTNQUERY:
                    listButtonModel.caption = ListButtonType.BTNQUERY;
                    listButtonModel.name = "btnQuery";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_query_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListQuery";
                    break;
                case ListButtonType.BTNPRECISEQUERY:
                    listButtonModel.caption = ListButtonType.BTNPRECISEQUERY;
                    listButtonModel.name = "btnPreciseQuery";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Query Budgetinfor_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListPreciseQuery";
                    break;
                case ListButtonType.BTNREFRESH:
                    listButtonModel.caption = ListButtonType.BTNREFRESH;
                    listButtonModel.name = "btnRefresh";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_refresh_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListRefresh";
                    break;
                case ListButtonType.BTNNEW:
                    listButtonModel.caption = ListButtonType.BTNNEW;
                    listButtonModel.name = "btnNew";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_add_32x32.png";
                    listButtonModel.voucherStatus = "NewStatus";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListNew";
                    break;
                case ListButtonType.BTNEDIT:
                    listButtonModel.caption = ListButtonType.BTNEDIT;
                    listButtonModel.name = "btnEdit";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Modify_32x32.png";
                    listButtonModel.voucherStatus = "EditStatus";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListEdit";
                    break;
                case ListButtonType.BTNDELETE:
                    listButtonModel.caption = ListButtonType.BTNDELETE;
                    listButtonModel.name = "btnDelete";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_delete_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListDelete";
                    break;
                case ListButtonType.BTNSUBMIT:
                    listButtonModel.caption = ListButtonType.BTNSUBMIT;
                    listButtonModel.name = "btnSubmit";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Submit_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListSubmit";
                    break;
                case ListButtonType.BTNUNSUBMIT:
                    listButtonModel.caption = ListButtonType.BTNUNSUBMIT;
                    listButtonModel.name = "btnUnSubmit";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_UnSubmit_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListUnSubmit";
                    break;
                case ListButtonType.BTNAUDIT:
                    listButtonModel.caption = ListButtonType.BTNAUDIT;
                    listButtonModel.name = "btnAudit";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_approve_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListAudit";
                    break;
                case ListButtonType.BTNUNAUDIT:
                    listButtonModel.caption = ListButtonType.BTNUNAUDIT;
                    listButtonModel.name = "btnUnAudit";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Unapprove_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListUnAudit";
                    break;
                case ListButtonType.BTNREF:
                    listButtonModel.caption = ListButtonType.BTNREF;
                    listButtonModel.name = "btnRef";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Refer_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListRef";
                    break;
                case ListButtonType.BTNPUSH:
                    listButtonModel.caption = ListButtonType.BTNPUSH;
                    listButtonModel.name = "btnPush";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_push creating_16x16.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListPush";
                    break;
                case ListButtonType.BTNEXIT:
                    listButtonModel.caption = ListButtonType.BTNEXIT;
                    listButtonModel.name = "btnExit";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Exit_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListExit";
                    break;
                case ListButtonType.BTNSELECTALL:
                    listButtonModel.caption = ListButtonType.BTNSELECTALL;
                    listButtonModel.name = "btnSelectAll";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Select all_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListSelectAll";
                    break;
                case ListButtonType.BTNRESELECT:
                    listButtonModel.caption = ListButtonType.BTNRESELECT;
                    listButtonModel.name = "btnReSelect";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_reselect_16x16.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListReSelect";
                    break;
                case ListButtonType.BTNCANCELSELECT:
                    listButtonModel.caption = ListButtonType.BTNCANCELSELECT;
                    listButtonModel.name = "btnCancelSelect";
                    listButtonModel.iconsize = "Large";
                    listButtonModel.icon = "U8|U8_Select none_32x32.png";
                    listButtonModel.voucherStatus = "";
                    eventModel.fullName = "LiForm.Event.EventListForm.LiEventListCancelSelect";
                    break;
            }

            listButtonModel.events.Add(eventModel);
            return listButtonModel;

        }

        public static string getCategoryGuidByButtonGroupModel(ButtonGroupModel buttonGroupModel)
        {
            return buttonGroupModel.buttons.Count <= 0 ? Guid.NewGuid().ToString() : buttonGroupModel.buttons[0].categoryGuid;
        }
        public static string getCategoryGuidByFormModel(FormModel formModel)
        {
            return formModel.listButtons.Count <= 0 ? Guid.NewGuid().ToString() : formModel.listButtons[0].categoryGuid;
        }
    }
}
