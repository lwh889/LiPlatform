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
        /// <summary>
        /// 获取U8单据数据
        /// </summary>
        /// <param name="liU8ApiGetData"></param>
        /// <returns></returns>
        public static LiU8COReponseModel getU8VouchList(LiU8ApiGetDataModel liU8ApiGetData)
        {
            U8Login.clsLogin u8Login = getLogin(liU8ApiGetData);
            LiGetVouchData liGetVouchData = new LiGetVouchData();
            liGetVouchData.Init(u8Login);
            LiU8COReponseModel liU8COReponse = LiU8COReponseModel.getInstance();
            try
            {
                DataTable dt = liGetVouchData.getU8VouchList(liU8ApiGetData);
                liU8COReponse.bSuccess = true;
                liU8COReponse.vouchDatas = DataTableUtil.getDictionaryToListByDataTable(dt);
                liU8COReponse.vouchDataRowCount = liGetVouchData.getU8VouchListCount(liU8ApiGetData);
            }
            catch(Exception ex)
            {
                liU8COReponse.bSuccess = false;
                liU8COReponse.resultContent = ex.Message;
            }

            return liU8COReponse;
        }

        /// <summary>
        /// 获取U8单据数据总行数
        /// </summary>
        /// <param name="liU8ApiGetData"></param>
        /// <returns></returns>
        public static LiU8COReponseModel getU8VouchListCount(LiU8ApiGetDataModel liU8ApiGetData)
        {
            U8Login.clsLogin u8Login = getLogin(liU8ApiGetData);
            LiGetVouchData liGetVouchData = new LiGetVouchData();
            liGetVouchData.Init(u8Login);
            LiU8COReponseModel liU8COReponse = LiU8COReponseModel.getInstance();
            try
            {
                int iCount = liGetVouchData.getU8VouchListCount(liU8ApiGetData);
                liU8COReponse.bSuccess = true;
                liU8COReponse.vouchDataRowCount  = iCount;
            }
            catch(Exception ex)
            {
                liU8COReponse.bSuccess = false;
                liU8COReponse.resultContent = ex.Message;
            }
            return liU8COReponse;
        }

        /// <summary>
        /// 获取单据CO
        /// </summary>
        /// <param name="liU8ApiInfo"></param>
        /// <returns></returns>
        public static ILiU8CO getLiU8CO(object liU8ApiInfo)
        {
            string sSubId = string.Empty;
            string sVouchType = string.Empty;
            string sBillType = string.Empty;
            string sBusType = string.Empty;
            bool bPositive = true;

            U8Login.clsLogin u8Login = null;
            ILiU8CO liU8CO = null;
            try
            {

                switch (liU8ApiInfo.GetType().Name)
                {
                    case "LiU8ApiIdModel":
                        LiU8ApiIdModel liU8ApiId = liU8ApiInfo as LiU8ApiIdModel;
                        u8Login = getLogin(liU8ApiId);
                        sSubId = liU8ApiId.sSubId;
                        sVouchType = liU8ApiId.sVouchType;
                        sBillType = liU8ApiId.sBillType;
                        sBusType = liU8ApiId.sBusType;
                        bPositive = liU8ApiId.bPositive;
                        break;
                    case "LiU8ApiDataModel":
                        LiU8ApiDataModel liU8ApiData = liU8ApiInfo as LiU8ApiDataModel;
                        u8Login = getLogin(liU8ApiData);
                        sSubId = liU8ApiData.sSubId;
                        sVouchType = liU8ApiData.sVouchType;
                        sBillType = liU8ApiData.sBillType;
                        sBusType = liU8ApiData.sBusType;
                        bPositive = liU8ApiData.bPositive;
                        break;
                }
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

                liU8CO.SetApiContext("liU8ApiInfo", liU8ApiInfo);
                if (sSubId == U8Sub.PU)
                {
                    liU8CO.SetApiContext("bPositive", bPositive);
                    liU8CO.SetApiContext("sBillType", sBillType);
                    liU8CO.SetApiContext("sBusType", sBusType);
                    liU8CO.SetApiContext("VoucherState", 2);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return liU8CO;
        }

        /// <summary>
        /// 删除U8单据ID
        /// </summary>
        /// <param name="liU8ApiId"></param>
        /// <returns></returns>
        public static List<LiU8COReponseModel> OperationVouch(LiU8ApiIdModel liU8ApiId)
        {
            List<LiU8COReponseModel> u8COReponseModels = new List<LiU8COReponseModel>();

            LiU8COReponseModel liU8COReponse = null;

            try
            {
                ILiU8CO liU8CO = getLiU8CO(liU8ApiId);

                foreach (object vouchId in liU8ApiId.vouchIds)
                {
                    try
                    {
                        liU8CO.SetVouchID(Convert.ToString(vouchId));
                        switch (liU8ApiId.sOperationType)
                        {
                            case OperationType.AUDIT:
                                liU8COReponse = liU8CO.Audit();
                                break;
                            case OperationType.DELETE:
                                liU8COReponse = liU8CO.Delete();
                                break;
                            case OperationType.UNAUDIT:
                                liU8COReponse = liU8CO.UnAudit();
                                if (liU8COReponse.bSuccess && liU8ApiId.bDelete)
                                {
                                    liU8CO.SetVouchID(Convert.ToString(liU8COReponse.vouchID));
                                    LiU8COReponseModel liU8COReponseAudit = liU8CO.Delete();
                                    liU8COReponse.bDeleteSuccess = liU8COReponseAudit.bSuccess;
                                    liU8COReponse.resultContent = string.Format("{0}，{1}", liU8COReponse.resultContent, liU8COReponseAudit.resultContent);
                                }
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        liU8COReponse = LiU8COReponseModel.getInstance();
                        liU8COReponse.bSuccess = false;
                        liU8COReponse.bAuditSuccess = false;
                        liU8COReponse.bDeleteSuccess = false;
                        liU8COReponse.resultContent = ex.Message;
                    }
                    u8COReponseModels.Add(liU8COReponse);
                }
            }
            catch (Exception ex)
            {
                liU8COReponse = LiU8COReponseModel.getInstance();
                liU8COReponse.bSuccess = false;
                liU8COReponse.bAuditSuccess = false;
                liU8COReponse.bDeleteSuccess = false;
                liU8COReponse.resultContent = ex.Message;
                u8COReponseModels.Add(liU8COReponse);
            }
            
            return u8COReponseModels;
        }

        /// <summary>
        /// 新增U8单据
        /// </summary>
        /// <param name="liU8ApiData"></param>
        /// <returns></returns>
        public static List<LiU8COReponseModel> AddU8Vouch(LiU8ApiDataModel liU8ApiData)
        {
            List<LiU8COReponseModel> u8COReponseModels = new List<LiU8COReponseModel>();

            LiU8COReponseModel liU8COReponse = null;

            try
            {
                ILiU8CO liU8CO = getLiU8CO(liU8ApiData);


                foreach (Dictionary<string, object> vouchData in liU8ApiData.vouchDatas)
                {
                    List<Dictionary<string, object>> bodyDatas = JsonUtil.GetDictionaryToList(Convert.ToString(vouchData["datas"]));

                    liU8CO.InitDom(bodyDatas.Count);
                    liU8CO.SetVouchData(vouchData);

                    liU8COReponse = liU8CO.Insert();
                    if (liU8COReponse.bSuccess && liU8ApiData.bAudit)
                    {
                        liU8CO.SetVouchID(Convert.ToString(liU8COReponse.vouchID));
                        LiU8COReponseModel liU8COReponseAudit = liU8CO.Audit();
                        liU8COReponse.bAuditSuccess = liU8COReponseAudit.bSuccess;
                        liU8COReponse.resultContent = string.Format("{0}，{1}", liU8COReponse.resultContent, liU8COReponseAudit.resultContent);
                    }

                    u8COReponseModels.Add(liU8COReponse);
                }
            }
            catch (Exception ex)
            {
                liU8COReponse = LiU8COReponseModel.getInstance();
                liU8COReponse.bSuccess = false;
                liU8COReponse.bAuditSuccess = false;
                liU8COReponse.bDeleteSuccess = false;
                liU8COReponse.resultContent = ex.Message;
                u8COReponseModels.Add(liU8COReponse);
            }
            return u8COReponseModels;
        }

        /// <summary>
        /// 获取U8Login组件
        /// </summary>
        /// <param name="liU8ApiData"></param>
        /// <returns></returns>
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
                throw new Exception("登陆失败，原因：" + u8Login.ShareString);
            }

            return u8Login;
        }

        /// <summary>
        /// 获取U8Login组件
        /// </summary>
        /// <param name="liU8ApiData"></param>
        /// <returns></returns>
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
                throw new Exception("登陆失败，原因：" + u8Login.ShareString);
            }

            return u8Login;
        }

        /// <summary>
        /// 获取U8Login组件
        /// </summary>
        /// <param name="liU8ApiGetData"></param>
        /// <returns></returns>
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
                throw new Exception("登陆失败，原因：" + u8Login.ShareString);
            }

            return u8Login;
        }
    }
}
