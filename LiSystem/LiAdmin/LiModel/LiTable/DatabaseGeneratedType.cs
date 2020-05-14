using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
using LiModel.Interface;

namespace LiModel.LiTable
{
    public class DatabaseGeneratedType : GridlookUpEditModel
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
                _dataSource.Add(new GridlookUpEditModel() { code = "0", name = "不处理" });
                _dataSource.Add(new GridlookUpEditModel() { code = "1", name = "自增长" });
                _dataSource.Add(new GridlookUpEditModel() { code = "2", name = "计算所得" });
                _dataSource.Add(new GridlookUpEditModel() { code = "3", name = "NEWID" });
            }
            return _dataSource;
        }
    }
}
