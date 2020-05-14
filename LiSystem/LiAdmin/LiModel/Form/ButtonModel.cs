﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;

namespace LiModel.Form
{
    public class ButtonModel
    {

        public static ButtonModel getInstance(int buttonGroupId, string categoryGuid)
        {
            return new ButtonModel() { id = 0, buttonGroupId = buttonGroupId, caption = "按钮1", name = "button1", iconsize = "Large", categoryGuid = categoryGuid, events = new List<EventModel>() };
        }
        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]  
        public int id { set; get; }

        /// <summary>
        /// ID
        /// </summary>
        [Browsable(false)]  
        public int buttonGroupId { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题")]
        public string caption { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("名称")]
        public string name { set; get; }

        /// <summary>
        /// 图标大小,Default,Large,SmallWithText,SmallWithoutText,All
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("标题"), TypeConverter(typeof(PropertyGridListConvert)), PropertyGridListAttribute(new string[] { "Default", "Large", "SmallWithText", "SmallWithoutText", "All" })]
        [Description("")]
        public string iconsize { set; get; }

        /// <summary>
        /// 类别ID
        /// </summary>
        [Browsable(false)]  
        public string categoryGuid { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("图标"), Editor]
        public string icon { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("状态"), Editor]
        public string voucherStatus { set; get; }

        /// <summary>
        /// 上个单据状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("上个单据状态"), Editor]
        public string previousVoucherStatus { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("清除状态"), Editor]
        [DefaultValue(false)]
        public bool bClearStatus { set; get; }

        /// <summary>
        /// 状态字段
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("状态字段"), Editor]
        public string statusFieldName { set; get; }

        /// <summary>
        /// 表体实体Key
        /// </summary>
        [Browsable(true), Category("基本属性"), DisplayName("表体实体Key"), Editor]
        public string entityKey { set; get; }

        /// <summary>
        /// 事件组
        /// </summary>
        [Browsable(false)]
        public List<EventModel> events;
    }
}