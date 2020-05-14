using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
using LiModel.Interface;

namespace LiModel.LiTable
{
    public class RelationshipType : GridlookUpEditModel
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
                _dataSource.Add(new GridlookUpEditModel() { code = "0", name = "无" });
                _dataSource.Add(new GridlookUpEditModel() { code = "1", name = "一对一" });
                _dataSource.Add(new GridlookUpEditModel() { code = "2", name = "一对多" });
                _dataSource.Add(new GridlookUpEditModel() { code = "3", name = "自已" });
            }
            return _dataSource;
        }
    }
}
