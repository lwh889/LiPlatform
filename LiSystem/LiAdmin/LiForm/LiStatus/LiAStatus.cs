using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiForm.LiStatus
{
    public abstract class LiAStatus
    {
        public string statusName {set;get;}

        public abstract void Handle(LiStatusContext context);

    }
}
