using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Form
{
    public class GridlookUpEditShowModeModel : GridlookUpEditModel
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
                _dataSource.Add(new GridlookUpEditModel() { code = "VALUE", name = "值" });
                _dataSource.Add(new GridlookUpEditModel() { code = "NAME", name = "名称" });
                _dataSource.Add(new GridlookUpEditModel() { code = "NAME_VALUE", name = "名称_值" });
                _dataSource.Add(new GridlookUpEditModel() { code = "VALUE_NAME", name = "值_名称" });
            }
            return _dataSource;
        }

    }
}