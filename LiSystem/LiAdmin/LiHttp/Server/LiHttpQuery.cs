using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.RequestParam;
using LiCommon.Util;

using Newtonsoft.Json;

namespace LiHttp.Server
{
    /// <summary>
    /// 查询
    /// </summary>
    public class LiHttpQuery : ALiHttp, ILiHttp
    {
        public LiHttpQuery(string entityKey, string systemCode) : base(systemCode, entityKey)
        {
        }
        /// <summary>
        /// 获取获取查询参数的参数
        /// </summary>
        /// <param name="option">操作类型</param>
        /// <param name="entityKey">查询实体Key</param>
        /// <param name="showAllColumn">是否显示所有行</param>
        /// <param name="showColumns">显示列名集合</param>
        /// <returns></returns>
        public QueryParamModel getQueryParamModel(string option, bool showAllColumn, List<string> showColumns)
        {
            QueryParamModel queryParamModel = new QueryParamModel();
            queryParamModel.type = "query";
            queryParamModel.showAllColumn = showAllColumn;
            queryParamModel.option = option;
            queryParamModel.systemCode = systemCode;
            queryParamModel.entityKey = entityKey;
            queryParamModel.columns = showColumns;
            queryParamModel.wheres = new List<QueryWhereModel>();

            return queryParamModel;
        }

        /// <summary>
        /// 获取查询所有数据的参数
        /// </summary>
        /// <param name="option"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public QueryParamModel getQueryParamModelAll(string option)
        {
            return getQueryParamModel(option, true, new List<string>());
        }

        /// <summary>
        /// 获取没有条件查询的参数
        /// </summary>
        /// <param name="option"></param>
        /// <param name="entityKey"></param>
        /// <param name="showAllColumn"></param>
        /// <param name="showColumns"></param>
        /// <returns></returns>
        public QueryParamModel getQueryParamModelNoWhere(string option,  bool showAllColumn, List<string> showColumns)
        {
            return getQueryParamModel(option, showAllColumn, showColumns);
        }

        /// <summary>
        /// 获取没有条件查询的参数
        /// </summary>
        /// <param name="option"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public QueryParamModel getQueryParamModelNoWhere(string option)
        {
            return getQueryParamModel(option, true, new List<string>());
        }

        /// <summary>
        /// 获取查询显示所有列，没有条件的参数
        /// </summary>
        /// <param name="option"></param>
        /// <param name="entityKey"></param>
        /// <returns></returns>
        public QueryParamModel getQueryParamModel_ShowAllColumn(string option)
        {
            return getQueryParamModel(option, true, new List<string>());
        }

        /// <summary>
        /// 获取查询单一条件的参数
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static QueryWhereModel getQueryWhereModel_Single(string columnName, object value)
        {
            QueryWhereModel queryWhereModel = getQueryWhereModelByAnd();
            QueryWhereValueModel queryWhereValueModel = new QueryWhereValueModel();
            queryWhereValueModel.columnName = columnName;
            queryWhereValueModel.value = value;
            queryWhereModel.values.Add(queryWhereValueModel);
            return queryWhereModel;
        }

        /// <summary>
        /// 获取查询AND多条件的参数
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public static QueryWhereModel getQueryWhereModelByAnd_Mulitiple(Dictionary<string, object> wheres)
        {
            QueryWhereModel queryWhereModel = getQueryWhereModelByAnd();
            foreach (KeyValuePair<string, object> kvp in wheres)
            {
                QueryWhereValueModel queryWhereValueModel = new QueryWhereValueModel();
                queryWhereValueModel.columnName = kvp.Key;
                queryWhereValueModel.value = kvp.Value;
                queryWhereModel.values.Add(queryWhereValueModel);
            }
            return queryWhereModel;
        }

        /// <summary>
        /// 获取查询AND空条件的参数
        /// </summary>
        /// <returns></returns>
        public static QueryWhereModel getQueryWhereModelByAnd()
        {
            QueryWhereModel queryWhereModel = new QueryWhereModel();
            queryWhereModel.logicalOperators = "AND";
            queryWhereModel.values = new List<QueryWhereValueModel>();

            return queryWhereModel;
        }

        /// <summary>
        /// 获取查询OR多条件的参数
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public static QueryWhereModel getQueryWhereModelByOr_Mulitiple(Dictionary<string, object> wheres)
        {
            QueryWhereModel queryWhereModel = getQueryWhereModelByOr();
            foreach (KeyValuePair<string, object> kvp in wheres)
            {
                QueryWhereValueModel queryWhereValueModel = new QueryWhereValueModel();
                queryWhereValueModel.columnName = kvp.Key;
                queryWhereValueModel.value = kvp.Value;
                queryWhereModel.values.Add(queryWhereValueModel);
            }
            return queryWhereModel;
        }

        /// <summary>
        /// 获取查询OR多条件的参数,同个字段名，不能值
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public static QueryWhereModel getQueryWhereModelByOr_Single(string fieldName, object value)
        {
            QueryWhereModel queryWhereModel = getQueryWhereModelByOr();

            QueryWhereValueModel queryWhereValueModel = new QueryWhereValueModel();
            queryWhereValueModel.columnName = fieldName;
            queryWhereValueModel.value = value;
            queryWhereModel.values.Add(queryWhereValueModel);

            return queryWhereModel;
        }

        /// <summary>
        /// 获取查询OR多条件的参数,同个字段名，不能值
        /// </summary>
        /// <param name="wheres"></param>
        /// <returns></returns>
        public static QueryWhereModel getQueryWhereModelByOr_Mulitiple(string fieldName, params object[] values)
        {
            QueryWhereModel queryWhereModel = getQueryWhereModelByOr();
            foreach (object value in values)
            {
                QueryWhereValueModel queryWhereValueModel = new QueryWhereValueModel();
                queryWhereValueModel.columnName = fieldName;
                queryWhereValueModel.value = value;
                queryWhereModel.values.Add(queryWhereValueModel);
            }
            return queryWhereModel;
        }

        /// <summary>
        /// 获取查询OR空条件的参数
        /// </summary>
        /// <returns></returns>
        public static QueryWhereModel getQueryWhereModelByOr()
        {
            QueryWhereModel queryWhereModel = new QueryWhereModel();
            queryWhereModel.logicalOperators = "OR";
            queryWhereModel.values = new List<QueryWhereValueModel>();

            return queryWhereModel;
        }

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="liHttpSetting"></param>
        /// <param name="retultContent"></param>
        /// <returns></returns>
        public override bool httpGet(LiHttpSetting liHttpSetting, out string retultContent)
        {
            return base.httpGet(liHttpSetting, out retultContent);
        }

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="liHttpSetting"></param>
        /// <param name="paramModel"></param>
        /// <param name="retultContent"></param>
        /// <returns></returns>
        public override bool httpPost(LiHttpSetting liHttpSetting, IParamModel paramModel, out string retultContent)
        {
            return base.httpPost(liHttpSetting, paramModel, out retultContent);
        }
    }
}
