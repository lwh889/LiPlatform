using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Form
{
    /// <summary>
    /// 流水号范围
    /// </summary>
    public class FlowNoRangeModel : GridlookUpEditModel
    {
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
                _dataSource.Add(new GridlookUpEditModel() { code = "DAY", name = "日" });
                _dataSource.Add(new GridlookUpEditModel() { code = "MONTH", name = "月" });
                _dataSource.Add(new GridlookUpEditModel() { code = "YEAR", name = "年" });
            }
            return _dataSource;
        }
    }
}
