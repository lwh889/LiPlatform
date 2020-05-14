using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiModel.Form;
using LiHttp.Server;
using LiHttp.RequestParam;
using LiCommon.Util;
using LiModel.Dev.GridlookUpEdit;
using LiHttp.Enum;

using Newtonsoft.Json;
using System.Data;

using LiCommon.LiException;

namespace LiHttp.GetEntity
{
    public abstract class AHttpEntity
    {
        public LiHttpQuery liHttpQuery;

        public LiHttpInsert liHttpInsert;

        public LiHttpUpdate liHttpUpdate;

        public LiHttpDelete liHttpDelete;

        public LiHttpProcedure liHttpProcedure;

        public LiHttpGetNewData liHttpGetNewData;

        /// <summary>
        /// 系统代码
        /// </summary>
        public string systemCode = string.Empty;
        /// <summary>
        /// 实体类型
        /// </summary>
        public string entityKey = string.Empty;
        /// <summary>
        /// 是否成功执行
        /// </summary>
        public bool bSuccess = false;
        /// <summary>
        /// 服务器返回内容
        /// </summary>
        public string resultContent = string.Empty;
        /// <summary>
        /// 内容提示
        /// </summary>
        public string tipStr = string.Empty;

        public AHttpEntity(string entityKey, string systemCode)
        {
            this.entityKey = entityKey;
            this.systemCode = systemCode;

            liHttpQuery = new LiHttpQuery(entityKey, systemCode);
            liHttpInsert = new LiHttpInsert(entityKey, systemCode);
            liHttpUpdate = new LiHttpUpdate(entityKey, systemCode);
            liHttpDelete = new LiHttpDelete(entityKey, systemCode);
            liHttpProcedure = new LiHttpProcedure(entityKey, systemCode);

            liHttpGetNewData = new LiHttpGetNewData(entityKey, systemCode);
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, DataTable> getRefControlDatas(  string basicInfoKey = "BasicInfoKey")
        {
            Dictionary<string, DataTable> refData = new Dictionary<string, DataTable>();
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQueryControlData("refControl"), paramModel, out resultContent))
            {
                Newtonsoft.Json.Linq.JArray jadataref = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultContent);
                foreach (Newtonsoft.Json.Linq.JToken jt in jadataref)
                {
                    refData.Add(Convert.ToString(jt[basicInfoKey]), DataUtil.DictionaryToTable(JsonUtil.GetDictionaryToList(Convert.ToString(jt["data"]))));
                }

            }
            else
            {
                throw (new LiHttpException(resultContent));
            }

            return refData;
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, TEntity> getRefControls<TEntity>( string basicInfoKey = "BasicInfoKey")
        {
            Dictionary<string, TEntity> refControl = new Dictionary<string, TEntity>();
            
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQueryControlInfo("refControl"), paramModel, out resultContent))
            {
                Newtonsoft.Json.Linq.JArray jacontrolref = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultContent);
                foreach (Newtonsoft.Json.Linq.JToken jt in jacontrolref)
                {
                    refControl.Add(Convert.ToString(jt[basicInfoKey]), JsonUtil.GetEntity<TEntity>(Convert.ToString(jt["data"])));
                }
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }

            return refControl;
        }

        public void execProcedure( Dictionary<string, object> paramDict)
        {
            ProcedureParamModel paramModel = liHttpProcedure.getProcedureParamModel(paramDict);

            bSuccess = liHttpProcedure.httpPost(LiHttpSetting_DrmAdmin.getHttpProcedure("procedure"), paramModel, out resultContent);
            if (!bSuccess)
            {
                throw (new LiHttpException(resultContent));
            }
        }
        public DataTable execProcedure_DataTable( Dictionary<string, object> paramDict)
        {
            execProcedure(paramDict);
            return DataUtil.DictionaryToTable(JsonUtil.GetDictionaryToList(resultContent));
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<TEntity> getEntityList<TEntity>()
        {
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            {
                return JsonUtil.GetEntityToList<TEntity>(resultContent);
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<TEntity> getEntityList<TEntity>(object keyValue, string keyFieldName)
        {
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");
            paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single(keyFieldName, keyValue));

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            {
                return JsonUtil.GetEntityToList<TEntity>(resultContent);
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<TEntity> getEntityList<TEntity>(Dictionary<string, object> whereDict)
        {
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");
            foreach (KeyValuePair<string, object> kvp in whereDict)
            {
                paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single(kvp.Key, kvp.Value));
            }

            return getEntityList<TEntity>(paramModel);
        }

        public QueryParamModel getQueryParamModel_ShowAllColumn()
        {
            return liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<TEntity> getEntityList<TEntity>(QueryParamModel paramModel)
        {
            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            {
                return JsonUtil.GetEntityToList<TEntity>(resultContent);
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<TEntity> getEntityList<TEntity>(Dictionary<string,Dictionary<string, object>> whereDict)
        {
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");
            foreach (KeyValuePair<string, Dictionary<string, object>> kvp in whereDict)
            {
                switch (kvp.Key)
                {
                    case LogicalOperator.AND:
                        foreach (KeyValuePair<string, object> kvpChild in kvp.Value)
                        {
                            paramModel.wheres.Add(LiHttpQuery.getQueryWhereModelByAnd_Mulitiple(kvp.Value));
                        }
                        break;
                    case LogicalOperator.OR:
                        foreach (KeyValuePair<string, object> kvpChild in kvp.Value)
                        {
                            paramModel.wheres.Add(LiHttpQuery.getQueryWhereModelByOr_Mulitiple(kvp.Value));
                        }
                        break;
                }
            }

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            {
                return JsonUtil.GetEntityToList<TEntity>(resultContent);
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }

        /// <summary>
        /// 获取单一实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="keyValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public TEntity getEntitySingle<TEntity>(object keyValue, string keyFieldName)
        {
            List<TEntity> list = getEntityList<TEntity>(keyValue, keyFieldName);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// 获取单一实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="keyValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public TEntity getEntitySingle<TEntity>(Dictionary<string, object> whereDict)
        {
            List<TEntity> list = getEntityList<TEntity>(whereDict);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// 获取单一实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="keyValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public TEntity getEntitySingle<TEntity>(QueryParamModel paramModel)
        {
            List<TEntity> list = getEntityList<TEntity>(paramModel);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return default(TEntity);
            }
        }

        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public DataTable getDataTable()
        {
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            {
                List<Dictionary<string, object>> list = JsonUtil.GetDictionaryToList(resultContent);
                return DataUtil.DictionaryToTable(list);
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }


        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> getEntityDictionaryInList()
        {
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            {
                return JsonUtil.GetDictionaryToList(resultContent);
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }

        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> getEntityDictionaryInList(object keyValue, string keyFieldName)
        {
            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");
            paramModel.wheres.Add(LiHttpQuery.getQueryWhereModel_Single(keyFieldName, keyValue));

            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("query"), paramModel, out resultContent))
            {
                return JsonUtil.GetDictionaryToList(resultContent);
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }

        /// <summary>
        /// 获取单一字典
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyFieldName"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public Dictionary<string, object> getEntityDictionarySingle(object keyValue, string keyFieldName)
        {
            List<Dictionary<string, object>> lists = getEntityDictionaryInList(keyValue, keyFieldName);
            if (lists != null && lists.Count > 0)
            {
                return lists[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取新增数据
        /// </summary>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public Dictionary<string, object> getEntityNewData()
        {
            GetNewDataParamModel paramModel = liHttpGetNewData.getGetNewDataParamModel();
            //paramModel.entityKey = entityKey;
            //paramModel.systemCode = systemCode;
            if (liHttpGetNewData.httpPost(LiHttpSetting_DrmAdmin.getHttpGetNewData("getNewData"), paramModel, out resultContent))
            {
                return DataUtil.getEmptyDictionary(JsonUtil.GetDictionary(resultContent));
            }
            else
            {
                throw (new LiHttpException(resultContent));
            }
        }

        public void newEntity(Dictionary<string, object> datas)
        {
            saveEntity(false, datas);
        }

        public void updateEntity(Dictionary<string, object> datas)
        {
            saveEntity(true, datas);
        }

        public bool saveEntity(bool isUpdate,  Dictionary<string, object> datas)
        {
            tipStr = "";
            if (isUpdate)
            {

                if (liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdate(), liHttpUpdate.getUpdateParamModel( datas), out resultContent))
                {
                    tipStr = "修改成功！";
                    bSuccess = true;
                }

                else
                {
                    tipStr = "修改失败！" + resultContent;
                }
            }
            else
            {
                if (liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsert(), liHttpInsert.getInsertParamModel(datas), out resultContent))
                {
                    tipStr = "保存成功！";
                    bSuccess = true;

                }
                else
                {
                    tipStr = "保存失败！" + resultContent;
                }
            }

            return bSuccess;
        }

        public bool deleteEntity( object datas)
        {

            if (liHttpDelete.httpPost(LiHttpSetting_DrmAdmin.getHttpDelete(), liHttpDelete.getDeleteParamModel( datas), out resultContent))
            {
                tipStr = "删除成功！";
                bSuccess = true;
            }
            else
            {
                tipStr = "删除失败！";
                bSuccess = false;
            }

            return bSuccess;
        }

        public void newEntity( object datas)
        {
            saveEntity(false, datas);
        }

        public void updateEntity(object datas)
        {
            saveEntity(true, datas);
        }

        public void saveEntity(bool isUpdate, object datas)
        {
            bSuccess = false;

            if (isUpdate)
            {

                if (liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdate(), liHttpUpdate.getUpdateParamModel( datas), out resultContent))
                {
                    tipStr = "修改成功！";
                    bSuccess = true;
                }

                else
                {
                    tipStr = "修改失败！" + resultContent;
                }
            }
            else
            {
                if (liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsert(), liHttpInsert.getInsertParamModel( datas), out resultContent))
                {
                    tipStr = "保存成功！";
                    bSuccess = true;

                }
                else
                {
                    tipStr = "保存失败！" + resultContent;
                }
            }

        }


        public void batchNewEntity<TEntity>(List<TEntity> datas)
        {
            batchSaveEntity<TEntity>(false, datas);
        }

        public void batchUpdateEntity<TEntity>(List<TEntity> datas)
        {
            batchSaveEntity<TEntity>(true, datas);
        }

        public void batchSaveEntity<TEntity>(bool isUpdate, List<TEntity> datas)
        {
            if (isUpdate)
            {

                if (liHttpUpdate.httpPost(LiHttpSetting_DrmAdmin.getHttpUpdateBatch(), liHttpUpdate.getUpdateBatchParamModel<TEntity>( datas), out resultContent))
                {
                    tipStr = "修改成功！";
                    bSuccess = true;
                }

                else
                {
                    tipStr = "修改失败！" + resultContent;
                }
            }
            else
            {
                if (liHttpInsert.httpPost(LiHttpSetting_DrmAdmin.getHttpInsertBatch(), liHttpInsert.getInsertBatchParamModel<TEntity>(datas), out resultContent))
                {
                    tipStr = "保存成功！";
                    bSuccess = true;

                }
                else
                {
                    tipStr = "保存失败！" + resultContent;
                }
            }

        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, DataTable> getBasicInfos(List<string> entityKeys)
        {
            Dictionary<string, DataTable> refData = new Dictionary<string, DataTable>();

            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");//无效的数据
            paramModel.entityKeys = entityKeys;
            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("getBasicInfos"), paramModel, out resultContent))
            {
                Newtonsoft.Json.Linq.JArray jadataref = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultContent);
                foreach (Newtonsoft.Json.Linq.JToken jt in jadataref)
                {
                    refData.Add(Convert.ToString(jt["BasicInfoKey"]), DataUtil.DictionaryToTable(JsonUtil.GetDictionaryToList(Convert.ToString(jt["data"]))));
                }

            }
            else
            {
                throw (new LiHttpException(resultContent));
            }

            return refData;
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Dictionary<string, DataTable> getDictInfos(List<string> entityKeys)
        {
            Dictionary<string, DataTable> refData = new Dictionary<string, DataTable>();

            QueryParamModel paramModel = liHttpQuery.getQueryParamModel_ShowAllColumn("queryBy");
            paramModel.entityKeys = entityKeys;
            if (liHttpQuery.httpPost(LiHttpSetting_DrmAdmin.getHttpQuery("getDictInfos"), paramModel, out resultContent))
            {
                Newtonsoft.Json.Linq.JArray jadataref = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(resultContent);
                foreach (Newtonsoft.Json.Linq.JToken jt in jadataref)
                {
                    refData.Add(Convert.ToString(jt["DictInfoKey"]), DataUtil.DictionaryToTable(JsonUtil.GetDictionaryToList(Convert.ToString(jt["data"]))));
                }

            }
            else
            {
                throw (new LiHttpException(resultContent));
            }

            return refData;
        }


        public object execProcedureSingleValue_Object( string fieldName, Dictionary<string, object> paramDict)
        {
            execProcedure( paramDict);
            if (bSuccess)
            {
                List<Dictionary<string, object>> dictCounts = JsonUtil.GetDictionaryToList(resultContent);
                if (dictCounts.Count > 0)
                {
                    return dictCounts[0][fieldName];
                }

            }
            return null;
        }
        public Int32 execProcedureSingleValue_Int32( string fieldName, Dictionary<string, object> paramDict)
        {
            object value = execProcedureSingleValue_Object( fieldName, paramDict);

            if (value != null)
            {
                return Convert.ToInt32(value);
            }
            return default(Int32);
        }
    }
}
