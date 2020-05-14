using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
using LiModel.Interface;

namespace LiModel.LiTable
{
    public class EntityOrder : GridlookUpEditModel
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
                _dataSource.Add(new GridlookUpEditModel() { code = "master", name = "主" });
                _dataSource.Add(new GridlookUpEditModel() { code = "slave", name = "次" });
            }
            return _dataSource;
        }
    }
}
