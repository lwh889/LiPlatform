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
        void Init(string voucherClassify, string voucherType, string sSubId, string sAccID, string sYear, string sUserID, string sPassword, string sDate);
        void Init(string voucherClassify, string voucherType, U8Login.clsLogin u8Login);
        void InitCO();
        void InitDom(int iRow);
        void SetApiContext(string paramName, object paramValue );
        void SetDefaultValue();
        void SetVouchData(Dictionary<string, object> datas, string bodyEntityName = "datas");
        void SetVouchID(string vouchID);
        LiU8COReponseModel Insert(bool bAudit);

        LiU8COReponseModel Audit();

        LiU8COReponseModel UnAudit(bool bDelete);

        LiU8COReponseModel Delete();
    }
}
