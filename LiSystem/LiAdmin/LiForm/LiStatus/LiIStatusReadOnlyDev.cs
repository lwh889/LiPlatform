using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.LiStatus
{
    public interface LiIStatusReadOnlyDev : LiIStatus
    {
        bool isNewStatus();

        bool isShowStatus();
    }
}
