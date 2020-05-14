using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
using LiModel.Interface;

namespace LiModel.LiTable
{
    public class ClassName : GridlookUpEditModel
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
                _dataSource.Add(new GridlookUpEditModel() { code = "JsonModel", name = "Json类" });
                _dataSource.Add(new GridlookUpEditModel() { code = "EntityModel", name = "实体类" });
            }
            return _dataSource;
        }
    }
}
