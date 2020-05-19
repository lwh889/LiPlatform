using LiModel.Form;
using LiModel.LiEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.LiModelFactory
{
    public class FormTemplateFactory
    {
        public static FormModel getFormModel_SingleVoucher(FormModel formModel)
        {

            //列表按钮
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNQUERY, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNPRECISEQUERY, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNREFRESH, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNNEW, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNEDIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNDELETE, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNSUBMIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNUNSUBMIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNAUDIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNUNAUDIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNEXIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));

            ButtonGroupModel buttonGroupModel = ButtonGroupModel.getInstance(formModel.id, 0, 0);
            formModel.buttonGroups.Add(buttonGroupModel);
            //表单按钮
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNNEW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNEDIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNSAVE, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNDELETE, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNSUBMIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNUNSUBMIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNAUDIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNUNAUDIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNREF, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNPUSH, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNEXIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));

            //修改dock,panel1,text,tableName,parentTableName,parentPrimaryKeyName,entityColumnName,childEntityColumnNames,可选primaryKeyName,foreigntKeyName,keyType
            PanelModel panelModel = PanelModel.getInstance(formModel.id);
            panelModel.dock = DockType.FILL;
            panelModel.type = PanelType.BASIC;
            panelModel.tableName = "";
            formModel.panels.Add(panelModel);

            //基本信息
            ControlGroupModel controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
            controlGroupModel.name = "basicGroup";
            controlGroupModel.text = "基本信息";
            panelModel.controlGroups.Add(controlGroupModel);

            controlGroupModel.controls.Add(ControlFactory.getVoucherCode());
            controlGroupModel.controls.Add(ControlFactory.getVoucherDate());
            controlGroupModel.controls.Add(ControlFactory.getVoucherStatusControl());

            //其他信息
            controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
            controlGroupModel.name = "otherGroup";
            controlGroupModel.text = "其他信息";
            panelModel.controlGroups.Add(controlGroupModel);

            controlGroupModel.controls.Add(ControlFactory.getUserControlMaker());
            controlGroupModel.controls.Add(ControlFactory.getDateControlMakerDate());
            controlGroupModel.controls.Add(ControlFactory.getUserControlModifer());
            controlGroupModel.controls.Add(ControlFactory.getDateControlModifyDate());
            controlGroupModel.controls.Add(ControlFactory.getUserControlSubmit());
            controlGroupModel.controls.Add(ControlFactory.getDateControlSubmitDate());
            controlGroupModel.controls.Add(ControlFactory.getUserControlAuditor());
            controlGroupModel.controls.Add(ControlFactory.getDateControlAuditDate());

            return formModel;
        }
    }
}
