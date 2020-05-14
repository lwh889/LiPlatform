using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiHttp.RequestParam;
using LiCommon.Util;
using Newtonsoft.Json;
using LiCommon.LiException;

namespace LiHttp.Server
{
    /// <summary>
    /// 抽象类，和服务器交互
    /// </summary>
    public abstract class ALiHttp : ILiHttp
    {
        /// <summary>
        /// 默认系统代码
        /// </summary>
        public string systemCode = "LiSystem";

        /// <summary>
        /// 默认系统代码
        /// </summary>
        public string entityKey = string.Empty;

        public ALiHttp()
        {

        }

        public ALiHttp(string systemCode, string entityKey)
        {
            this.systemCode = systemCode;
            this.entityKey = entityKey;
        }

        public virtual bool httpGet(LiHttpSetting liHttpSetting, out string resultContent)
        {
            resultContent = "[]";

            try
            {

                string result = HttpUtil.Instance.get(string.Format("{0}/{1}/{2}/{3}", liHttpSetting.serverUrl, liHttpSetting.serverName, liHttpSetting.serverController, liHttpSetting.serverExecName));

                if (result == "")
                {
                    return false;
                }
                else
                {

                    Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);

                    switch (Convert.ToInt32(jo["code"]))
                    {
                        case 0:
                            resultContent = Convert.ToString(jo["data"]);
                            return true;
                        case 520:
                            resultContent = Convert.ToString(jo["msg"].ToString());
                            return false;
                    }

                }
                return false;
            }
            catch (PostException ex)
            {
                resultContent = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                resultContent = ex.Message;
                return false;
            }
            return false;

        }


        public virtual bool httpPost(LiHttpSetting liHttpSetting, IParamModel paramModel, out string resultContent)
        {
            resultContent = "[]";
            try
            {
                string url = string.Format("{0}/{1}/{2}/{3}", liHttpSetting.serverUrl, liHttpSetting.serverName, liHttpSetting.serverController, liHttpSetting.serverExecName);
                string param = JsonUtil.GetJson(paramModel);
                
                string result = HttpUtil.Instance.post(url, param);

                if (result == "")
                {
                    return false;
                }
                else
                {
                    Newtonsoft.Json.Linq.JObject jo = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(result);

                    switch (Convert.ToInt32(jo["code"].ToString()))
                    {
                        case 0:
                            resultContent = Convert.ToString(jo["data"].ToString());
                            return true;
                        case 520:
                            resultContent = Convert.ToString(jo["msg"].ToString());
                            return false;
                    }

                }
            }
            catch (PostException ex)
            {
                resultContent  = ex.Message;
                return false;
            }
            catch(Exception ex)
            {
                resultContent = ex.Message;
                return false;
            }
            return false;
        }
    }
}
