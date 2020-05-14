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
    /// 接口类，和服务器交互
    /// </summary>
    public interface ILiHttp
    {
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="liHttpSetting"></param>
        /// <param name="retultContent"></param>
        /// <returns></returns>
        bool httpGet(LiHttpSetting liHttpSetting, out string retultContent);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="liHttpSetting"></param>
        /// <param name="paramModel"></param>
        /// <param name="retultContent"></param>
        /// <returns></returns>
        bool httpPost(LiHttpSetting liHttpSetting, IParamModel paramModel, out string retultContent);

    }
}
