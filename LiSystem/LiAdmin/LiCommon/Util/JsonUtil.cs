using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LiCommon.Util
{
    /// <summary>
    /// Json帮助类
    /// </summary>
   public  class JsonUtil
    {
       public static IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
       public static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
       static JsonUtil()
       {
           jsonSerializerSettings.Converters.Add(timeConverter);
       }
        /// <summary>
        /// Json转换为Dict,
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns>Json</returns>
       public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;

        }

       
        /// <summary>
        /// Json转换为Dict,
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns>Json</returns>
       public static Dictionary<string, object> GetDictionary(string jsonStr)
       {
           if (string.IsNullOrEmpty(jsonStr))
               return new Dictionary<string, object>();

           Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
           Dictionary<string, object> jsonDictTemp = new Dictionary<string, object>();
           foreach (KeyValuePair<string, object> kvp in jsonDict)
           {
               if (kvp.Value == null)
               {
                   jsonDictTemp.Add(kvp.Key, kvp.Value);
                   continue;
               }
               if (kvp.Value.GetType().Name == "JArray")
               {
                   List<Dictionary<string, object>> childDict = new List<Dictionary<string, object>>();
                    List<object> childArray = new List<object>();
                    Newtonsoft.Json.Linq.JArray ja = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(kvp.Value.ToString());
                    foreach (Newtonsoft.Json.Linq.JToken jt in ja)
                    {
                        if (jt.HasValues)
                        {
                            childDict.Add(GetDictionary(jt.ToString()));
                            if (!jsonDictTemp.ContainsKey(kvp.Key))
                            {
                                jsonDictTemp.Add(kvp.Key, childDict);
                            }
                        }
                        else {
                            childArray.Add(jt.ToString());
                            if (!jsonDictTemp.ContainsKey(kvp.Key))
                            {
                                jsonDictTemp.Add(kvp.Key, childArray);
                            }
                        }
                    }

               }else if (kvp.Value.GetType().Name == "JObject")
               {
                   jsonDictTemp.Add(kvp.Key, GetDictionary(kvp.Value.ToString()));

               }
           }


           foreach (KeyValuePair<string, object> kvp in jsonDictTemp)
           {
               jsonDict[kvp.Key] = kvp.Value;
           }
           return jsonDict;

       }

       public static List<Dictionary<string, object>> GetDictionaryToList(string jsonStr)
       {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            Newtonsoft.Json.Linq.JArray ja = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(jsonStr);
            foreach (Newtonsoft.Json.Linq.JToken jt in ja)
            {
                list.Add(GetDictionary(jt.ToString()));
            }

            return list;
       }

        /// <summary>
        /// 对象转换为Json,
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns>Json</returns>
        public static string GetJson(object en)
        {
            try
            {
                string rs = JsonConvert.SerializeObject(en, jsonSerializerSettings);
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 对象转换为Json并忽略null值
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns>Json</returns>
        public static string GetJsonOfIgnoreNull(object en)
        {
            try
            {
                string rs = JsonConvert.SerializeObject(en, jsonSerializerSettings);
                //string rs = JsonConvert.SerializeObject(en); //JsonConvert.SerializeObject(en, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }); // 忽略null值  //两个参数的 4.0中没有方法
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// json转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json</param>
        /// <returns>实体类</returns>
        public static List<T> GetEntityToList<T>(string json)
        {
            List<T> list = new List<T>();
            try
            {
                Newtonsoft.Json.Linq.JArray ja = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(json, jsonSerializerSettings);
                foreach (Newtonsoft.Json.Linq.JToken jt in ja)
                {
                    T en = JsonConvert.DeserializeObject<T>(jt.ToString(), jsonSerializerSettings);
                    list.Add(en);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }

        /// <summary>
        /// json转换为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json</param>
        /// <returns>实体类</returns>
        public static T GetEntity<T>(string json)
        {
            try
            {
                T en = JsonConvert.DeserializeObject<T>(json, jsonSerializerSettings);
                return en;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// json转换为匿名对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json</param>
        /// <returns>实体类</returns>
        public static T GetEntity<T>(string json, T type)
        {
            try
            {
                T en = JsonConvert.DeserializeAnonymousType<T>(json, type);
                return en;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public static Dictionary<string, object> GetDictionaryByModel(object entity){
           return JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(entity, jsonSerializerSettings));
       }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="cKeys">例：string|abc&int|90&string|bvc</param>
        /// <returns></returns>
        public static string GetValue(string json, string cKeys)
        {
            try
            {
                Newtonsoft.Json.Linq.JObject ja = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(json);
                string[] keyArray = cKeys.Split('&');
                Newtonsoft.Json.Linq.JObject jt = ja;
                int i = 0;
                foreach (string keys in keyArray)
                {
                    ++i;
                    string[] keyArray2 = keys.Split('|');
                    string key = keyArray2[0].ToLower();
                    string value = keyArray2[1];
                    switch (key)
                    {
                        case "int":
                            if (i == keyArray.Length)
                                return jt[int.Parse(value)].ToString();
                            else
                                jt = (Newtonsoft.Json.Linq.JObject)jt[int.Parse(value)];
                            break;
                        default:
                            if (i == keyArray.Length)
                                return jt[value].ToString();
                            else
                                jt = (Newtonsoft.Json.Linq.JObject)jt[value];

                            break;
                    }
                }
                return jt.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
