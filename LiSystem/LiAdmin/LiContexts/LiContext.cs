using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

using DevExpress.XtraBars.Ribbon;
using LiContexts.Model;
using LiHttp.Enum;
using LiHttp.GetEntity;
using LiModel.Users;
using LiModel.Form;
using LiModel.Basic;
using LiLog;
using LiHttp.RequestParam;
using LiContexts.LiEnum;
using LiVoucherConvert;
using LiVoucherConvert.Service;
using LiModel.LiConvert;
using LiVoucherConvert.Service.Impl;
using LiVoucherConvert.Model;
using LiHttp;
using LiCommon.LiExpression.LiDefaultValue;

namespace LiContexts
{
    public class LiContext
    {
        public static DefaultValueContext _defaultValueContext;
        /// <summary>
        /// 默认值表达式
        /// </summary>
        public static DefaultValueContext defaultValueContext { set { _defaultValueContext = value; } get { return _defaultValueContext; } }

        public static Dictionary<string, PageFormModel> pageFormModels = new Dictionary<string, PageFormModel>();


        private static Dictionary<string, DataTable> _liRefDataDataTable = new Dictionary<string, DataTable>();

        /// <summary>
        /// 引用控件的显示数据
        /// </summary>
        public static Dictionary<string, DataTable> liRefDataDataTable { set { _liRefDataDataTable = value; } get { return _liRefDataDataTable; } }


        private static Dictionary<string, DataTable> _liDictDataTable = new Dictionary<string, DataTable>();

        /// <summary>
        /// 字典控件的显示数据
        /// </summary>
        public static Dictionary<string, DataTable> liDictDataTable { set { _liDictDataTable = value; } get { return _liDictDataTable; } }



        private static Dictionary<string, VoucherStatusModel> _VoucherStatusModels = new Dictionary<string, VoucherStatusModel>();

        /// <summary>
        /// 状态数据
        /// </summary>
        public static Dictionary<string, VoucherStatusModel> VoucherStatusModels { set { _VoucherStatusModels = value; } get { return _VoucherStatusModels; } }

        private static Dictionary<string, FormModel> _FormModels = new Dictionary<string, FormModel>();

        /// <summary>
        /// 表单界数据
        /// </summary>
        public static Dictionary<string, FormModel> FormModels { set { _FormModels = value; } get { return _FormModels; } }

        private static Dictionary<string, VoucherCodeModel> _VoucherCodeModels = new Dictionary<string, VoucherCodeModel>();

        /// <summary>
        /// 单据编号规则
        /// </summary>
        public static Dictionary<string, VoucherCodeModel> VoucherCodeModels { set { _VoucherCodeModels = value; } get { return _VoucherCodeModels; } }

        /// <summary>
        /// 默认系统数据库名称
        /// </summary>
        public const string SYSTEMCODE_DEFAULT = "LiSystem";


        public static UserModel _userInfo;

        /// <summary>
        /// 用户信息
        /// </summary>
        public static UserModel userInfo { set { _userInfo = value; LogUtil.UserID = value.userCode; LogUtil.UserName = value.userName; }get{ return _userInfo; } }
        //public static RibbonForm currentForm;

        public static string _HostIP ;
        public static string _HostName ;
        /// <summary>
        /// 主机IP
        /// </summary>
        public static string HostIP { set{ _HostIP = value; LogUtil.HostIP = _HostIP; } get { return _HostIP; } }
        /// <summary>
        /// 主机名称
        /// </summary>
        public static string HostName { set { _HostName = value; LogUtil.HostName = _HostName; } get { return _HostName; } }

        public static DateTime _LoginData;
        /// <summary>
        /// 登陆日期
        /// </summary>
        public static DateTime LoginData { set { _LoginData = value; } get { return _LoginData; } }

        public static SystemInfoModel _SystemInfo;
        /// <summary>
        /// 系统信息
        /// </summary>
        public static SystemInfoModel SystemInfo { set { _SystemInfo = value; _SystemCode = value.systemCode; } get { return _SystemInfo; } }

        public static string _SystemCode;
        /// <summary>
        /// 帐套号
        /// </summary>
        public static string SystemCode { set { _SystemCode = value;  } get { return _SystemCode; } }


        public static string _SeverIP;
        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string SeverIP { set { _SeverIP = value; LiHttp.Server.LiHttpSetting.URL = string.Format("http://{0}:8002", value); } get { return _SeverIP; } }

        public static string SystemLoginType = LoginType.LISYSTEM;

        static LiContext()
        {
            defaultValueContext = new DefaultValueContext();
            //addHttpEntity("NewData", new NewDataEntity());
            //addHttpEntity("VoucherData", new VoucherDataEntity());
            //addHttpEntity("Procedure", new ProcedureEntity());

            //liHttpDelete = new LiHttpInsert
        }


        //获取对应的引用数据
        public static DataTable getRefDataDataTable(string key)
        {
            if (liRefDataDataTable.ContainsKey(key))
            {
                return liRefDataDataTable[key];
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// 获取对应的字典数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DataTable getDictDataTable(string key)
        {
            if (liDictDataTable.ContainsKey(key))
            {
                return liDictDataTable[key];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取对应的状态数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static VoucherStatusModel getVoucherStatusModels(string key)
        {
            if (!VoucherStatusModels.ContainsKey(key))
            {
                List<VoucherStatusModel> voucherStatusList = LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherStatus, SystemCode).getEntityList<VoucherStatusModel>(key,"code");
                if (voucherStatusList != null && voucherStatusList.Count > 0)
                {
                    VoucherStatusModels.Add(key, voucherStatusList[0]);
                }
            }
            return VoucherStatusModels[key];
        }

        /// <summary>
        /// 获取对应的单据界面数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static FormModel getFormModel(string key,string systemCode)
        {
            if (!FormModels.ContainsKey(key))
            {
                QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).getQueryParamModel_ShowAllColumn();

                QueryComplexWhereModel queryComplexWhereModel = QueryComplexWhereModel.AND();
                queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.AND("systemCode", systemCode));
                queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.AND("name", key));

                paramModel.complexWheres = queryComplexWhereModel;

                FormModel formModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).getEntitySingle<FormModel>(paramModel);
                if (formModel != null )
                {
                    FormModels.Add(key, formModel);
                }
            }
            return FormModels[key];
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<FormModel> getFormModelList(string key, string systemCode)
        {

            QueryParamModel paramModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).getQueryParamModel_ShowAllColumn();

            QueryComplexWhereModel queryComplexWhereModel = QueryComplexWhereModel.AND();
            queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.AND("systemCode", systemCode));
            queryComplexWhereModel.wheres.Add(QueryComplexWhereModel.AND("name", key));

            paramModel.complexWheres = queryComplexWhereModel;

            return LiContexts.LiContext.getHttpEntity(LiEntityKey.FormModel).getEntityList<FormModel>(paramModel);
        }
        /// <summary>
        /// 获取对应的单据编号规则
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static VoucherCodeModel getVoucherCodeModels(string key)
        {
            if (!VoucherCodeModels.ContainsKey(key))
            {
                Dictionary<string, object> whereDict = new Dictionary<string, object>();
                whereDict.Add("entityKey", key);
                whereDict.Add("bDefault", 1);
                VoucherCodeModel voucherCodeModel = LiContexts.LiContext.getHttpEntity(LiEntityKey.VoucherCode, SystemCode).getEntitySingle<VoucherCodeModel>( whereDict);
                if (voucherCodeModel != null)
                {
                    VoucherCodeModels.Add(key, voucherCodeModel);
                }
                else
                {
                    return null;
                }
            }

            return VoucherCodeModels[key];
        }

        /// <summary>
        /// 获取对应的单据界面数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, object> getVoucherEmptyDatas(string key )
        {
            Dictionary<string, object> dictData = LiContexts.LiContext.getHttpEntity(key, SystemCode).getEntityNewData();


            return dictData;
        }

        //增加对应的引用数据
        public static void addRefDataDataTable(List<string> entityKeys)
        {
            foreach (string key in liRefDataDataTable.Keys)
            {
                if(entityKeys.Contains(key))
                {
                    entityKeys.Remove(key);
                }
            }
            if (entityKeys.Count <= 0) return;
            Dictionary<string, DataTable> dataTables = LiContexts.LiContext.getHttpEntity("NewData", SYSTEMCODE_DEFAULT).getBasicInfos(entityKeys);

            foreach (KeyValuePair<string, DataTable> kvp in dataTables)
            {
                liRefDataDataTable.Add(kvp.Key, kvp.Value);
            }

        }

        /// <summary>
        /// 增加对应的字典数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void addDictDataTable(List<string> dictKeys)
        {
            foreach (string key in liDictDataTable.Keys)
            {
                if (dictKeys.Contains(key))
                {
                    dictKeys.Remove(key);
                }
            }
            if (dictKeys.Count <= 0) return;

            Dictionary<string, DataTable> dataTables = LiContexts.LiContext.getHttpEntity("NewData", SYSTEMCODE_DEFAULT).getDictInfos(dictKeys);

            foreach (KeyValuePair<string, DataTable> kvp in dataTables)
            {
                liDictDataTable.Add(kvp.Key, kvp.Value);
            }

        }

        /// <summary>
        /// 弃用
        /// </summary>
        /// <param name="key"></param>
        /// <param name="httpEntity"></param>
        public static void addHttpEntity(string key, AHttpEntity httpEntity)
        {
            LiHttpUtil.addHttpEntity(key, httpEntity);
        }

        //public static TEntity getHttpEntity<TEntity>(string entityKey, string systemCode = SYSTEMCODE) where TEntity : class
        //{
        //    string keyStr = string.Concat(entityKey, "_", systemCode);
        //    if (!httpEntitys.ContainsKey(keyStr))
        //    {
        //        httpEntitys.Add(keyStr, new AHttpEntity(entityKey));
        //    }
        //    TEntity entity = httpEntitys[key] as TEntity;
        //    return entity;
        //}

        /// <summary>
        /// 弃用
        /// </summary>
        /// <param name="entityKey"></param>
        /// <param name="systemCode"></param>
        /// <returns></returns>
        public static AHttpEntity getHttpEntity(string entityKey, string systemCode = SYSTEMCODE_DEFAULT)
        {
            return  LiHttpUtil.getHttpEntity(entityKey, systemCode); ;

        }

        /// <summary>
        /// 弃用，获取单据转换
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="convertCode"></param>
        /// <returns></returns>
        public static AVoucherConvert getVoucherConvert(string convertType, string convertCode)
        {
            return LiVoucherConvertUtil.getVoucherConvert(convertType, convertCode);
        }

        /// <summary>
        /// 弃用，下推单据
        /// </summary>
        /// <param name="convertType"></param>
        /// <param name="convertCode"></param>
        /// <returns></returns>
        public static LiReponseModel pushVoucher(string convertType, string convertCode)
        {
            return LiVoucherConvertUtil.pushVoucher(convertType, convertCode);
        }

        //public static AHttpEntity getHttpEntity()
        //{
        //    return getHttpEntity<AHttpEntity>("Common");
        //}

        // 打开子窗体方法
        public static bool ContainPageMdi(PageFormModel pageFormModel)
        {
            return pageFormModels.ContainsKey(pageFormModel.ToString());
        }
        public static bool AddPageMdi(PageFormModel pageFormModel, Form parentForm)
        {
            if(LiContext.SystemLoginType == LoginType.U8)
            {
                pageFormModel.liForm.Show();
                return true;
            }
            else
            {

                if (!pageFormModels.ContainsKey(pageFormModel.ToString()))
                {
                    pageFormModels.Add(pageFormModel.ToString(), pageFormModel);
                    if (pageFormModel.liForm.MdiParent == null)
                        pageFormModel.liForm.MdiParent = parentForm;
                    pageFormModel.liForm.Show();
                    return true;
                }
                else
                {
                    pageFormModel = pageFormModels[pageFormModel.ToString()];
                    pageFormModel.liForm.Activate();
                }

            }


            return false;
            // 设置当前 tab页的 图标,我这里也默认取navBar中的Item中的图标
            //xtraTabbedMdiManager1.Pages[childForm].Image = navItem.SmallImage;
        }
        public static void ClearPageMdi()
        {
            pageFormModels.Clear();
            
        }

        public static void updateUserSkin(string skinName )
        {
            userInfo.skinName = skinName;
            getHttpEntity(LiEntityKey.User).updateEntity(userInfo);
        }
    }



}
