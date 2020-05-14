using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Interface;

namespace LiModel.Form
{
    public class GridlookUpEditModel : AGridlookUpEditModelBase
    {
        public GridlookUpEditModel()
        {
            setDataSource(new List<GridlookUpEditModel>());
        }

        public override TEntity getInstance<TEntity>() 
        {
            return new TEntity();
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("name"))
            {
                SearchColumns.Add("name");
            }
            return SearchColumns;
        }

        /// <summary>
        /// 获取显示列
        /// </summary>
        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("code"))
            {
                DisplayColumns.Add("code");
            }
            if (!DisplayColumns.Contains("name"))
            {
                DisplayColumns.Add("name");
            }
            return DisplayColumns;
        }

        /// <summary>
        /// 获取列名映射
        /// </summary>
        public override Dictionary<string, string> getDictModelDesc()
        {
            if (!DictModelDesc.ContainsKey("code"))
            {
                DictModelDesc.Add("code", "编码");
            }
            if (!DictModelDesc.ContainsKey("name"))
            {
                DictModelDesc.Add("name", "名称");
            }
            return DictModelDesc;
        }

        public override string getValueMember()
        {
            return "code";
        }

        public override string getDisplayMember()
        {
            return "name";
        }

        /// <summary>
        /// 编码
        /// </summary>
        public string code { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { set; get; }
    }
}
