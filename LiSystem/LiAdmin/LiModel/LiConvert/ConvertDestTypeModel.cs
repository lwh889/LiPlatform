﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;

namespace LiModel.LiConvert
{
    public class ConvertDestTypeModel : GridlookUpEditModel
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
                _dataSource.Add(new GridlookUpEditModel() { code = "System", name = "本系统" });
                _dataSource.Add(new GridlookUpEditModel() { code = "SystemOut", name = "系统外" });
            }
            return _dataSource;
        }
    }
}
