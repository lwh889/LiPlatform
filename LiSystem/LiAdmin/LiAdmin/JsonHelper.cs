using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiAdmin
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 对象转换为Json,
        /// </summary>
        /// <param name="en">对象</param>
        /// <returns>Json</returns>
        public static string GetJson(object en)
        {
            try
            {
                string rs = JsonConvert.SerializeObject(en);
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
                string rs = JsonConvert.SerializeObject(en); //JsonConvert.SerializeObject(en, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }); // 忽略null值  //两个参数的 4.0中没有方法
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
                Newtonsoft.Json.Linq.JArray ja = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(json);
                foreach (Newtonsoft.Json.Linq.JToken jt in ja)
                {
                    T en = JsonConvert.DeserializeObject<T>(jt.ToString());
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
                T en = JsonConvert.DeserializeObject<T>(json);
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
