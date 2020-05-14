using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiModel.Form;

namespace LiModel.Interface
{
    public interface IGridlookUpEditModel
    {
        List<string> getSearchColumns();

        List<string> getDisplayColumns();

        Dictionary<string, string> getDictModelDesc();

        string getValueMember();

        string getDisplayMember();

    }
}
