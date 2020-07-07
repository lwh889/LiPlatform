using LiU8CO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO.Service
{
    public interface ILiU8CO
    {
        void InitCO();
        void InitDom(int iRow);
        void setDefaultValue();
        LiU8COReponseModel Insert();

        LiU8COReponseModel Audit();

        LiU8COReponseModel UnAudit();

        LiU8COReponseModel Delete();
    }
}
