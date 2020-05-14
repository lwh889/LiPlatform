using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.LiAttribute;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using LiModel.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LiModel.Form
{
    /// <summary>
    /// 字典
    /// </summary>
    public class DictModel
    {
        public int id { set; get; }

        /// <summary>
        /// 编码
        /// </summary>
        [Edit]
        [Display(Name = "编码", Order = 2)]
        public string dictCode { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Edit]
        [Display(Name = "名称", Order = 3)]
        public string dictName { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        [Edit]
        [Display(Name = "排序", Order = 4)]
        public int dictOrder { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Edit]
        [Display(Name = "备注", Order = 5)]
        [ControlTypeAttribute("MemoEdit")]
        public string dictMemo { set; get; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Edit]
        [ControlTypeAttribute("TreeListLookUpEdit")]
        [Display(Name = "所属字典组", Order = 1)]
        public int dictParentID { set; get; }

        private DateTime _dModifyDate;

        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime dModifyDate { get { _dModifyDate = DateTime.Now; return _dModifyDate; } set { _dModifyDate = value; } }
    }
}
