﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Interface;

namespace LiModel.Form
{
    public class ControlEventTypeModel : GridlookUpEditModel
    {
        public const string EditValueChanged = "EditValueChanged";

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
                _dataSource.Add(new GridlookUpEditModel() { code = "EditValueChanged", name = "值变化后事件" });
            }
            return _dataSource;
        }

    }
}
