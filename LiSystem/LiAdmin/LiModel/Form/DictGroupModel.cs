using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.LiAttribute;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using LiModel.Converter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace LiModel.Form
{
    /// <summary>
    /// 字典模型
    /// </summary>
    public class DictGroupModel : AGridlookUpEditModelBase
    {
        public override TEntity getInstance<TEntity>()
        {
            return new TEntity();
        }

        public override string getValueMember()
        {
            return "ID";
        }

        public override string getDisplayMember()
        {
            return "Name";
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("Name"))
            {
                SearchColumns.Add("Name");
            }
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("Code"))
            {
                DisplayColumns.Add("Code");
            }
            if (!DisplayColumns.Contains("Name"))
            {
                DisplayColumns.Add("Name");
            }
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public override Dictionary<string, string> getDictModelDesc()
        {
            if (!DictModelDesc.ContainsKey("Code"))
            {
                DictModelDesc.Add("Code", "字典档案编码");
            }
            if (!DictModelDesc.ContainsKey("Name"))
            {
                DictModelDesc.Add("Name", "字典档案名称");
            }
            return DictModelDesc;
        }

        /// <summary>
        /// 字典组节点ID
        /// </summary>
        [No]
        public int ID { get; set; }	//数据ID，主键

        /// <summary>
        /// 字典组节点编码
        /// </summary>
        [Edit]
        [Display(Name = "编码", Order = 2)]
        public string Code { get; set; }	//数据名称

        /// <summary>
        /// 字典组节点名称
        /// </summary>
        [Edit]
        [Display(Name = "名称", Order = 3)]
        public string Name { get; set; }	//数据名称

        /// <summary>
        /// 字典组父节点
        /// </summary>
        [Edit]
        [Display(Name = "所属字典组", Order = 1)]
        [ControlTypeAttribute("TreeListLookUpEdit")]
        public int ParentID { get; set; }	//父标签ID，父标签的数据ID

        /// <summary>
        /// 字典组是否是系统
        /// </summary>
        [No]
        [DefaultValue(true)]
        public bool isGroup { get; set; }

        /// <summary>
        /// 字典组是否是系统
        /// </summary>
        [No]
        public bool isSystem { get; set; }

        /// <summary>
        /// 字典组ID
        /// </summary>
        [No]
        public int GroupId { get; set; }	//分组ID，当前位于树形菜单第几级的意思

        /// <summary>
        /// 字典组图标索引
        /// </summary>
        [No]
        public int imageIndex { get; set; }

        /// <summary>
        /// 字典组顺序
        /// </summary>
        [No]
        public int iOrder { get; set; }	//

        private DateTime _dModifyDate;

        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonConverter(typeof(LiDateTimeConverter))]
        public DateTime dModifyDate { get { _dModifyDate = DateTime.Now; return _dModifyDate; } set { _dModifyDate = value; } }

    }
}
