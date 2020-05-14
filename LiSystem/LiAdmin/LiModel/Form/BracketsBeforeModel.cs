using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Interface;

namespace LiModel.Form
{
    public class BracketsBeforeModel : GridlookUpEditModel
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
                _dataSource.Add(new BracketsBeforeModel() { code = "0", name = "(" });
                    _dataSource.Add(new BracketsBeforeModel() { code = "1", name = "((" });
                    _dataSource.Add(new BracketsBeforeModel() { code = "2", name = "(((" });
                }
                return _dataSource;
            
        }

    }
}
