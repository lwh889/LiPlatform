using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
using LiModel.Interface;

namespace LiModel.LiTable
{
    public class ColumnType : GridlookUpEditModel
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
                _dataSource.Add(new GridlookUpEditModel() { code = "collection", name = "collection" });
                _dataSource.Add(new GridlookUpEditModel() { code = "nvarchar", name = "nvarchar" });
                _dataSource.Add(new GridlookUpEditModel() { code = "dateTime", name = "dateTime" });
                _dataSource.Add(new GridlookUpEditModel() { code = "datetime2", name = "datetime2" });
                _dataSource.Add(new GridlookUpEditModel() { code = "int", name = "int" });
                _dataSource.Add(new GridlookUpEditModel() { code = "bit", name = "bit" });
                _dataSource.Add(new GridlookUpEditModel() { code = "decimal", name = "decimal" });
                _dataSource.Add(new GridlookUpEditModel() { code = "float", name = "float" });
                _dataSource.Add(new GridlookUpEditModel() { code = "narchar", name = "narchar" });
                _dataSource.Add(new GridlookUpEditModel() { code = "numeric", name = "numeric" });
                _dataSource.Add(new GridlookUpEditModel() { code = "smallint", name = "smallint" });
                _dataSource.Add(new GridlookUpEditModel() { code = "timestamp", name = "timestamp" });
                _dataSource.Add(new GridlookUpEditModel() { code = "tinyint", name = "tinyint" });
                _dataSource.Add(new GridlookUpEditModel() { code = "uniqueidentifier", name = "uniqueidentifier" });
                _dataSource.Add(new GridlookUpEditModel() { code = "varchar", name = "varchar" });
            }
            return _dataSource;
        }
    }
}
