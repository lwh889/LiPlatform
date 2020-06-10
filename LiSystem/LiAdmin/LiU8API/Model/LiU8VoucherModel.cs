using LiModel.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8API.Model
{
    public class LiU8VoucherModel : AGridlookUpEditModelBase
    {
        public override TEntity getInstance<TEntity>()
        {
            return new TEntity();
        }
        public override string getValueMember()
        {
            return "code";
        }

        public override string getDisplayMember()
        {
            return "name";
        }

        public override List<string> getSearchColumns()
        {

            if (!SearchColumns.Contains("code"))
            {
                SearchColumns.Add("code");
            }
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

        public int id { set; get; }

        public string code { set; get; }

        public string name { set; get; }

        public string voucherType { set; get; }

        public List<LiU8OperationModel> operations;

    }
}
