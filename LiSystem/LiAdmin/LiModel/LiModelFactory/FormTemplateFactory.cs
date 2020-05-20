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
        public static FormModel getFormModel_BasicInfo(FormModel formModel)
        {
            formModel.formType = VoucherType.BasicInfo;
            //列表按钮
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNQUERY, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNPRECISEQUERY, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNREFRESH, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNNEW, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNEDIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNDELETE, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));
            formModel.listButtons.Add(ButtonFactory.getListButtonModel(ListButtonType.BTNEXIT, formModel.id, ButtonFactory.getCategoryGuidByFormModel(formModel)));

            //列表按钮
            ButtonGroupModel buttonGroupModel = ButtonGroupModel.getInstance(formModel.id, 0, 0);
            formModel.buttonGroups.Add(buttonGroupModel);
            //表单按钮
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNNEW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNEDIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNSAVE, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNDELETE, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNEXIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));

            //基本信息
            //修改dock,panel1,text,tableName,parentTableName,parentPrimaryKeyName,entityColumnName,childEntityColumnNames,可选primaryKeyName,foreigntKeyName,keyType
            PanelModel panelModel = PanelModel.getInstance(formModel.id);
            panelModel.name = "basicPanel";
            panelModel.text = "基本信息";
            panelModel.dock = DockType.FILL;
            panelModel.type = PanelType.BASIC;
            panelModel.primaryKeyName = "id";
            panelModel.keyType = KeyType.IDENTITY;
            panelModel.height = 200;
            panelModel.tableName = "";
            formModel.panels.Add(panelModel);

            //基本信息
            ControlGroupModel controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
            controlGroupModel.name = "basicGroup";
            controlGroupModel.text = "基本信息";
            panelModel.controlGroups.Add(controlGroupModel);

            controlGroupModel.controls.Add(ControlFactory.getCodeControl(controlGroupModel.id));
            controlGroupModel.controls.Add(ControlFactory.getNameControl(controlGroupModel.id));

            //其他信息
            controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
            controlGroupModel.name = "otherGroup";
            controlGroupModel.text = "其他信息";
            panelModel.controlGroups.Add(controlGroupModel);

            controlGroupModel.controls.Add(ControlFactory.getUserControlMaker());
            controlGroupModel.controls.Add(ControlFactory.getDateControlMakerDate());
            controlGroupModel.controls.Add(ControlFactory.getUserControlModifer());
            controlGroupModel.controls.Add(ControlFactory.getDateControlModifyDate());

            return formModel;
        }

        public static FormModel getFormModel_TreeBasicInfo(FormModel formModel)
        {
            formModel.formType = VoucherType.TreeBasicInfo;
            //列表按钮
            ButtonGroupModel buttonGroupModel = ButtonGroupModel.getInstance(formModel.id, 0, 0);
            formModel.buttonGroups.Add(buttonGroupModel);
            //表单按钮
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNNEW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNEDIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNSAVE, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNDELETE, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNEXIT, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel)));


            //基本信息
            //修改dock,panel1,text,tableName,parentTableName,parentPrimaryKeyName,entityColumnName,childEntityColumnNames,可选primaryKeyName,foreigntKeyName,keyType
            PanelModel panelModel = PanelModel.getInstance(formModel.id);
            panelModel.name = "basicPanel";
            panelModel.text = "基本信息";
            panelModel.dock = DockType.FILL;
            panelModel.type = PanelType.BASIC;
            panelModel.primaryKeyName = "id";
            panelModel.keyType = KeyType.IDENTITY;
            panelModel.height = 200;
            panelModel.tableName = "";
            formModel.panels.Add(panelModel);

            //基本信息
            ControlGroupModel controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
            controlGroupModel.name = "basicGroup";
            controlGroupModel.text = "基本信息";
            panelModel.controlGroups.Add(controlGroupModel);

            controlGroupModel.controls.Add(ControlFactory.getCodeControl(controlGroupModel.id));
            controlGroupModel.controls.Add(ControlFactory.getNameControl(controlGroupModel.id));


            return formModel;
        }
        /// <summary>
        /// 主从表
        /// </summary>
        /// <param name="formModel"></param>
        /// <returns></returns>
        public static FormModel getFormModel_MSVoucher(FormModel formModel)
        {
            formModel.formType = VoucherType.Voucher;
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

            //基本信息
            //修改dock,panel1,text,tableName,parentTableName,parentPrimaryKeyName,entityColumnName,childEntityColumnNames,可选primaryKeyName,foreigntKeyName,keyType
            PanelModel panelModel = PanelModel.getInstance(formModel.id);
            panelModel.name = "basicPanel";
            panelModel.text = "基本信息";
            panelModel.dock = DockType.TOP;
            panelModel.type = PanelType.BASIC;
            panelModel.primaryKeyName = "id";
            panelModel.keyType = KeyType.IDENTITY;
            panelModel.childEntityColumnNames = "datas";
            panelModel.height = 200;
            panelModel.tableName = "";
            formModel.panels.Add(panelModel);

            ControlGroupModel controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
            controlGroupModel.name = "basicGroup";
            controlGroupModel.text = "基本信息";
            panelModel.controlGroups.Add(controlGroupModel);

            controlGroupModel.controls.Add(ControlFactory.getVoucherCode());
            controlGroupModel.controls.Add(ControlFactory.getVoucherDate());
            controlGroupModel.controls.Add(ControlFactory.getVoucherStatusControl());
            controlGroupModel.controls.Add(ControlFactory.getMemoHeadControl());


            //明细信息
            panelModel = PanelModel.getInstance(formModel.id);
            panelModel.name = "detailPanel";
            panelModel.text = "明细信息";
            panelModel.dock = DockType.FILL;
            panelModel.type = PanelType.GRID;
            panelModel.primaryKeyName = "id";
            panelModel.foreigntKeyName = "fid";
            panelModel.keyType = KeyType.IDENTITY;
            panelModel.parentPrimaryKeyName = "id";
            panelModel.entityColumnName = "datas";
            panelModel.tableName = "";
            formModel.panels.Add(panelModel);

            controlGroupModel = ControlGroupModel.getInstance(panelModel.id);
            controlGroupModel.name = "datas";
            controlGroupModel.text = "明细信息";
            panelModel.controlGroups.Add(controlGroupModel);

            controlGroupModel.controls.Add(ControlFactory.getRowNoControl());
            controlGroupModel.controls.Add(ControlFactory.getQuantityControl());
            controlGroupModel.controls.Add(ControlFactory.getPriceControl());
            controlGroupModel.controls.Add(ControlFactory.getAmountControl());
            controlGroupModel.controls.Add(ControlFactory.getMemoBodyControl());

            buttonGroupModel = ButtonGroupModel.getInstance(0, 0, controlGroupModel.id);
            controlGroupModel.buttonGroups.Add(buttonGroupModel);
            //表单按钮
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNADDROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNINSERTROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNDELETEROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNCOPYROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNADDCOPYROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNINSERTCOPYROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNUPROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));
            buttonGroupModel.buttons.Add(ButtonFactory.getFormButtonModel(ButtonType.BTNDOWNROW, buttonGroupModel.id, ButtonFactory.getCategoryGuidByButtonGroupModel(buttonGroupModel), controlGroupModel.name));

            //其他信息
            panelModel = PanelModel.getInstance(formModel.id);
            panelModel.text = "其他信息";
            panelModel.name = "otherPanel";
            panelModel.dock = DockType.BOTTOM;
            panelModel.type = PanelType.BASIC;
            panelModel.primaryKeyName = "id";
            panelModel.keyType = KeyType.IDENTITY;
            panelModel.childEntityColumnNames = "datas";
            panelModel.height = 120;
            panelModel.tableName = "";
            formModel.panels.Add(panelModel);

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

        /// <summary>
        /// 单表
        /// </summary>
        /// <param name="formModel"></param>
        /// <returns></returns>
        public static FormModel getFormModel_SingleVoucher(FormModel formModel)
        {
            formModel.formType = VoucherType.Voucher;
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
            panelModel.name = "basicPanel";
            panelModel.dock = DockType.FILL;
            panelModel.type = PanelType.BASIC;
            panelModel.primaryKeyName = "id";
            panelModel.keyType = KeyType.IDENTITY;
            panelModel.height = 200;
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
