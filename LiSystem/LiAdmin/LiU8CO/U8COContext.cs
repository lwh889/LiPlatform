using LiCommon.Util;
using LiU8CO.LiEnum;
using LiU8CO.Model;
using LiU8CO.Service;
using LiU8CO.Service.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiU8CO
{
    public class U8COContext
    {
        public static List<Dictionary<string, object>> getU8VouchList(LiU8ApiGetDataModel liU8ApiGetData)
        {
            U8Login.clsLogin u8Login = getLogin(liU8ApiGetData);
            LiGetVouchData liGetVouchData = new LiGetVouchData();
            liGetVouchData.Init(u8Login);
            DataTable dt = liGetVouchData.getU8VouchList(liU8ApiGetData);
            return null;
        }
        public static List<Dictionary<string, object>> getU8VouchListCount(LiU8ApiGetDataModel liU8ApiGetData)
        {
            U8Login.clsLogin u8Login = getLogin(liU8ApiGetData);
            LiGetVouchData liGetVouchData = new LiGetVouchData();
            liGetVouchData.Init(u8Login);
            int iCount = liGetVouchData.getU8VouchListCount(liU8ApiGetData);
            return null;
        }
        public static ILiU8CO getLiU8CO(object liU8ApiInfo)
        {
            string sSubId = string.Empty;
            string sVouchType = string.Empty;
            U8Login.clsLogin u8Login = null;
            switch (liU8ApiInfo.GetType().Name)
            {
                case "LiU8ApiIdModel":
                    u8Login = getLogin(liU8ApiInfo as LiU8ApiIdModel);
                    break;
                case "LiU8ApiDataModel":
                    u8Login = getLogin(liU8ApiInfo as LiU8ApiDataModel);
                    break;
            }
            ILiU8CO liU8CO = null;
            switch (sSubId)
            {
                case U8Sub.PU:
                    liU8CO = new LiU8PUCO();
                    break;
                case U8Sub.SA:
                    liU8CO = new LiU8SACO();
                    break;
                case U8Sub.ST:
                    liU8CO = new LiU8STCO();
                    break;
            }
            liU8CO.Init(sSubId, sVouchType, u8Login);
            liU8CO.InitCO();

            return liU8CO;
        }
        public static List<LiU8COReponseModel> DeleteVouch(LiU8ApiIdModel liU8ApiId)
        {
            ILiU8CO liU8CO = getLiU8CO(liU8ApiId);

            List<LiU8COReponseModel> u8COReponseModels = new List<LiU8COReponseModel>();
            foreach (object vouchId in liU8ApiId.vouchIds)
            {
                liU8CO.SetVouchID(Convert.ToString(vouchId));
                LiU8COReponseModel liU8COReponse = liU8CO.Delete();
            }
            return u8COReponseModels;
        }

        public static List<LiU8COReponseModel> AuditVouch(LiU8ApiIdModel liU8ApiId)
        {
            ILiU8CO liU8CO = getLiU8CO(liU8ApiId);

            List<LiU8COReponseModel> u8COReponseModels = new List<LiU8COReponseModel>();
            foreach (object vouchId in liU8ApiId.vouchIds)
            {
                liU8CO.SetVouchID(Convert.ToString(vouchId));
                LiU8COReponseModel liU8COReponse = liU8CO.Audit();
            }
            return u8COReponseModels;
        }

        public static List<LiU8COReponseModel> UnAuditVouch(LiU8ApiIdModel liU8ApiId)
        {
            ILiU8CO liU8CO = getLiU8CO(liU8ApiId.sSubId);

            List<LiU8COReponseModel> u8COReponseModels = new List<LiU8COReponseModel>();
            foreach (object vouchId in liU8ApiId.vouchIds)
            {
                liU8CO.SetVouchID(Convert.ToString(vouchId));
                LiU8COReponseModel liU8COReponse = liU8CO.UnAudit(liU8ApiId.bDelete);
            }
            return u8COReponseModels;
        }

        public static List<LiU8COReponseModel> AddU8Vouch(LiU8ApiDataModel liU8ApiData)
        {
            ILiU8CO liU8CO = getLiU8CO(liU8ApiData.sSubId);

            List<LiU8COReponseModel> u8COReponseModels = new List<LiU8COReponseModel>();
            foreach (Dictionary<string, object> vouchData in liU8ApiData.vouchDatas)
            {
                List<Dictionary<string, object>> bodyDatas = vouchData["datas"] as List<Dictionary<string, object>>;

                liU8CO.InitDom(bodyDatas.Count);
                liU8CO.SetVouchData(vouchData);
                LiU8COReponseModel liU8COReponse = liU8CO.Insert(liU8ApiData.bAudit);

                u8COReponseModels.Add(liU8COReponse);
            }
            return u8COReponseModels;
        }

        public static U8Login.clsLogin getLogin(LiU8ApiIdModel liU8ApiData)
        {

            U8Login.clsLogin u8Login = new U8Login.clsLogin();
            string sSubId = ModelUtil.getValue<LiU8ApiIdModel, string>("sSubId", liU8ApiData);
            string sAccID = ModelUtil.getValue<LiU8ApiIdModel, string>("sAccID", liU8ApiData);
            string sYear = ModelUtil.getValue<LiU8ApiIdModel, string>("sYear", liU8ApiData);
            string sUserID = ModelUtil.getValue<LiU8ApiIdModel, string>("sUserID", liU8ApiData);
            string sPassword = ModelUtil.getValue<LiU8ApiIdModel, string>("sPassword", liU8ApiData);
            string sDate = ModelUtil.getValue<LiU8ApiIdModel, string>("sDate", liU8ApiData);

            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return null;
            }

            return u8Login;
        }

        public static U8Login.clsLogin getLogin(LiU8ApiDataModel liU8ApiData)
        {

            U8Login.clsLogin u8Login = new U8Login.clsLogin();
            string sSubId = ModelUtil.getValue<LiU8ApiDataModel,string>("sSubId", liU8ApiData);
            string sAccID = ModelUtil.getValue<LiU8ApiDataModel, string>("sAccID", liU8ApiData);
            string sYear = ModelUtil.getValue<LiU8ApiDataModel, string>("sYear", liU8ApiData);
            string sUserID = ModelUtil.getValue<LiU8ApiDataModel, string>("sUserID", liU8ApiData);
            string sPassword = ModelUtil.getValue<LiU8ApiDataModel, string>("sPassword", liU8ApiData);
            string sDate = ModelUtil.getValue<LiU8ApiDataModel, string>("sDate", liU8ApiData);

            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return null;
            }

            return u8Login;
        }
        public static U8Login.clsLogin getLogin(LiU8ApiGetDataModel liU8ApiGetData)
        {

            U8Login.clsLogin u8Login = new U8Login.clsLogin();
            string sSubId = ModelUtil.getValue<LiU8ApiGetDataModel, string>("sSubId", liU8ApiGetData);
            string sAccID = ModelUtil.getValue<LiU8ApiGetDataModel, string>("sAccID", liU8ApiGetData);
            string sYear = ModelUtil.getValue<LiU8ApiGetDataModel, string>("sYear", liU8ApiGetData);
            string sUserID = ModelUtil.getValue<LiU8ApiGetDataModel, string>("sUserID", liU8ApiGetData);
            string sPassword = ModelUtil.getValue<LiU8ApiGetDataModel, string>("sPassword", liU8ApiGetData);
            string sDate = ModelUtil.getValue<LiU8ApiGetDataModel, string>("sDate", liU8ApiGetData);

            if (!u8Login.Login(ref sSubId, ref sAccID, ref sYear, ref sUserID, ref sPassword, ref sDate))
            {
                Console.WriteLine("登陆失败，原因：" + u8Login.ShareString);
                return null;
            }

            return u8Login;
        }
    }
}
