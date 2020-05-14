using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using LiCommon.LiException;

namespace LiCommon.Util
{
    /// <summary>
    /// HTTPWeb操作帮助类
    /// </summary>
    public class HttpUtil
    {
        
        const string sUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        const string sContentType = "application/json;charset=UTF-8";
        const string sResponseEncoding = "utf-8";

        private HttpUtil() { }

        private static HttpUtil _instance;

        /// <summary>
        /// 单一实例
        /// </summary>
        public static HttpUtil Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HttpUtil();
                }
                return _instance;
            }
        }


        /*                          .Ctor                           */

        /// <summary>
        /// 发起请求
        /// </summary>
        /// <param name="httpRequest">HttpWebRequest对象</param>
        /// <returns>返回本次请求的结果</returns>
        public string SendHttpWebRequest(HttpWebRequest httpRequest)
        {
            Stream responseStream;
            string rs = "";
            try
            {
                responseStream = httpRequest.GetResponse().GetResponseStream();
                using (StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding(sResponseEncoding)))
                {
                    rs = responseReader.ReadToEnd();
                }
                responseStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return rs;
        }

        /// <summary>
        /// 发起请求(get)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <returns>返回本次请求的结果</returns>
        public string get(string url)
        {
            string rs = "";
            try
            {
                // 1.创建httpWebRequest对象
                WebRequest webRequest = WebRequest.Create(url);
                HttpWebRequest httpRequest = webRequest as HttpWebRequest;

                // 2.填充httpWebRequest的基本信息
                httpRequest.UserAgent = sUserAgent;
                httpRequest.ContentType = sContentType;
                httpRequest.Method = "GET";
                //httpRequest.CookieContainer = cc; //把接收到的包一起发送

                Stream responseStream = httpRequest.GetResponse().GetResponseStream();
                using (StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding(sResponseEncoding)))
                {
                    rs = responseReader.ReadToEnd();
                }
                responseStream.Close();
            }
            catch (Exception ex)
            {
                throw (new GetException(ex.Message, ex));
            }
            return rs;
        }

        /// <summary>
        /// 发起请求(post)
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">参数</param>
        /// <returns>返回本次请求的结果</returns>
        public string post(string url, string param)
        {
            string rs = "";
            try
            {
                // 1.创建httpWebRequest对象
                WebRequest webRequest = WebRequest.Create(url);
                HttpWebRequest httpRequest = webRequest as HttpWebRequest;

                // 2.填充httpWebRequest的基本信息
                httpRequest.UserAgent = sUserAgent;
                httpRequest.ContentType = sContentType;
                httpRequest.Method = "POST";
                //httpRequest.CookieContainer = cc; //把接收到的包一起发送

                // 3.Post参数
                Encoding encoding = Encoding.GetEncoding("utf-8");
                byte[] bytesToPost = encoding.GetBytes(param);

                // 4.填充post内容
                httpRequest.ContentLength = bytesToPost.Length;
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(bytesToPost, 0, bytesToPost.Length);
                requestStream.Close();


                Stream responseStream = httpRequest.GetResponse().GetResponseStream();
                using (StreamReader responseReader = new StreamReader(responseStream, Encoding.GetEncoding(sResponseEncoding)))
                {
                    rs = responseReader.ReadToEnd();
                }
                responseStream.Close();
            }
            catch (Exception ex)
            {
                throw (new PostException(ex.Message, ex));
            }
            return rs;
        }

        public string HttpUploadFile(string url, string data)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "text/html;charset=UTF-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", "ABC"));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            Encoding encoding = Encoding.GetEncoding("utf-8");
            byte[] bArr = encoding.GetBytes(data);

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            postStream.Write(bArr, 0, bArr.Length);
            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
        }

        /// <summary>
        /// 表单数据项
        /// </summary>
        public class FormItemModel
        {
            /// <summary>
            /// 表单键，request["key"]
            /// </summary>
            public string Key { set; get; }
            /// <summary>
            /// 表单值,上传文件时忽略，request["key"].value
            /// </summary>
            public string Value { set; get; }
            /// <summary>
            /// 是否是文件
            /// </summary>
            public bool IsFile
            {
                get
                {
                    if (FileContent == null || FileContent.Length == 0)
                        return false;

                    if (FileContent != null && FileContent.Length > 0 && string.IsNullOrWhiteSpace(FileName))
                        throw new Exception("上传文件时 FileName 属性值不能为空");
                    return true;
                }
            }
            /// <summary>
            /// 上传的文件名
            /// </summary>
            public string FileName { set; get; }
            /// <summary>
            /// 上传的文件内容
            /// </summary>
            public Stream FileContent { set; get; }
        }
    }
}
