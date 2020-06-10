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
        public const string Collection = "collection";
        public const string Nvarchar = "nvarchar";
        public const string Datetime = "datetime";
        public const string Datetime2 = "datetime2";
        public const string Int = "int";
        public const string Bit = "bit";
        public const string Decimal = "decimal";
        public const string Float = "float";
        public const string Numeric = "numeric";
        public const string Smallint = "smallint";
        public const string Timestamp = "timestamp";
        public const string Tinyint = "tinyint";
        public const string Uniqueidentifier = "uniqueidentifier";
        public const string Varchar = "varchar";
        public const string DateTime = "dateTime";
        public const string Money = "money";
        public const string Refassist = "refassist";
        public const string Dict = "dict";
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
                _dataSource.Add(new GridlookUpEditModel() { code = "datetime", name = "datetime" });
                _dataSource.Add(new GridlookUpEditModel() { code = "datetime2", name = "datetime2" });
                _dataSource.Add(new GridlookUpEditModel() { code = "int", name = "int" });
                _dataSource.Add(new GridlookUpEditModel() { code = "bit", name = "bit" });
                _dataSource.Add(new GridlookUpEditModel() { code = "decimal", name = "decimal" });
                _dataSource.Add(new GridlookUpEditModel() { code = "float", name = "float" });
                _dataSource.Add(new GridlookUpEditModel() { code = "numeric", name = "numeric" });
                _dataSource.Add(new GridlookUpEditModel() { code = "smallint", name = "smallint" });
                _dataSource.Add(new GridlookUpEditModel() { code = "timestamp", name = "timestamp" });
                _dataSource.Add(new GridlookUpEditModel() { code = "tinyint", name = "tinyint" });
                _dataSource.Add(new GridlookUpEditModel() { code = "uniqueidentifier", name = "uniqueidentifier" });
                _dataSource.Add(new GridlookUpEditModel() { code = "varchar", name = "varchar" });
                _dataSource.Add(new GridlookUpEditModel() { code = "dateTime", name = "dateTime" }); 
                _dataSource.Add(new GridlookUpEditModel() { code = "money", name = "money" });
                _dataSource.Add(new GridlookUpEditModel() { code = "refassist", name = "refassist" });
                _dataSource.Add(new GridlookUpEditModel() { code = "dict", name = "dict" });
            }
            return _dataSource;
        }
    }
}
