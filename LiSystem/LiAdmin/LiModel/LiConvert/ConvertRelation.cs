using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
namespace LiModel.LiConvert
{
    public class ConvertRelation : GridlookUpEditModel
    {
        /// <summary>
        /// 无限参照
        /// </summary>
        public const string INFINITE = "0";

        /// <summary>
        /// 下推一次
        /// </summary>
        public const string ONE = "1";

        /// <summary>
        /// 累计下推
        /// </summary>
        public const string PUSHCUMULATIVE = "2";

        public override List<string> getDisplayColumns()
        {
            if (!DisplayColumns.Contains("name"))
            {
                DisplayColumns.Add("name");
            }
            return DisplayColumns;
        }

        public List<GridlookUpEditModel> getDataSource()
        {
            List<GridlookUpEditModel> _dataSource = getDataSource<List<GridlookUpEditModel>>();

            if (_dataSource.Count <= 0)
            {
                _dataSource.Add(new GridlookUpEditModel() { code = "0", name = "无限参照" });
                _dataSource.Add(new GridlookUpEditModel() { code = "1", name = "下推一次" });
                _dataSource.Add(new GridlookUpEditModel() { code = "2", name = "累计下推" });
            }
            return _dataSource;
        }
    }
}
