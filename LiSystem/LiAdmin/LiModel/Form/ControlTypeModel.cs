using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiModel.Form
{
    public class ControlTypeModel : GridlookUpEditModel
    {

        public List<GridlookUpEditModel> getDataSource()
        {
            List<GridlookUpEditModel> _dataSource = getDataSource<List<GridlookUpEditModel>>();
            if (_dataSource.Count <= 0)
            {
                _dataSource.Add(new GridlookUpEditModel() { code = "TextEdit", name = "文本" });
                _dataSource.Add(new GridlookUpEditModel() { code = "CheckEdit", name = "是否" });
                _dataSource.Add(new GridlookUpEditModel() { code = "MemoEdit", name = "多行文本" });
                _dataSource.Add(new GridlookUpEditModel() { code = "IntEdit", name = "整形" });
                _dataSource.Add(new GridlookUpEditModel() { code = "DecimalEdit", name = "小数型" });
                _dataSource.Add(new GridlookUpEditModel() { code = "CalcEdit", name = "计算型" });
                _dataSource.Add(new GridlookUpEditModel() { code = "DateTimeEdit", name = "长日期" });
                _dataSource.Add(new GridlookUpEditModel() { code = "TimeEdit", name = "时间" });
                _dataSource.Add(new GridlookUpEditModel() { code = "DateEdit", name = "日期" });
                _dataSource.Add(new GridlookUpEditModel() { code = "GridLookUpEditComboBox", name = "下拉框" });
                _dataSource.Add(new GridlookUpEditModel() { code = "GridLookUpEditRef", name = "引用类型" });
                _dataSource.Add(new GridlookUpEditModel() { code = "TreeListLookUpEdit", name = "树形控件" });
            }
            return _dataSource;
        }

    }
}
