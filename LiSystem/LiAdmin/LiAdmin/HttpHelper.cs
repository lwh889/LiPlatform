using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace LiAdmin
{
    /// <summary>
    /// HTTPWeb操作帮助类
    /// </summary>
    public class HttpHelper
    {
        const string sUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        const string sContentType = "application/x-www-form-urlencoded";
        const string sResponseEncoding = "utf-8";

        private HttpHelper() { }

        private static HttpHelper _instance;

        /// <summary>
        /// 单一实例
        /// </summary>
        public static HttpHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HttpHelper();
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
                httpRequest.Method = "get";
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
                Console.WriteLine(ex);
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
                httpRequest.Method = "post";
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
                Console.WriteLine(ex);
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

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formItems">Post表单内容</param>
        /// <param name="cookieContainer"></param>
        /// <param name="timeOut">默认20秒</param>
        /// <param name="encoding">响应内容的编码类型（默认utf-8）</param>
        /// <returns></returns>
        //public string PostForm(string url, List<Li.Model.FormItemModel> formItems, CookieContainer cookieContainer = null, string refererUrl = null, Encoding encoding = null, int timeOut = 20000)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    #region 初始化请求对象
        //    request.Method = "POST";
        //    request.Timeout = timeOut;
        //    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        //    request.KeepAlive = true;
        //    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
        //    if (!string.IsNullOrEmpty(refererUrl))
        //        request.Referer = refererUrl;
        //    if (cookieContainer != null)
        //        request.CookieContainer = cookieContainer;
        //    #endregion

        //    string boundary = "----" + DateTime.Now.Ticks.ToString("x");//分隔符
        //    request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
        //    //请求流
        //    var postStream = new MemoryStream();
        //    #region 处理Form表单请求内容
        //    //是否用Form上传文件
        //    var formUploadFile = formItems != null && formItems.Count > 0;
        //    if (formUploadFile)
        //    {
        //        //文件数据模板
        //        string fileFormdataTemplate =
        //            "\r\n--" + boundary +
        //            "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
        //            "\r\nContent-Type: application/octet-stream" +
        //            "\r\n\r\n";
        //        //文本数据模板
        //        string dataFormdataTemplate =
        //            "\r\n--" + boundary +
        //            "\r\nContent-Disposition: form-data; name=\"{0}\"" +
        //            "\r\n\r\n{1}";
        //        foreach (var item in formItems)
        //        {
        //            string formdata = null;
        //            if (item.IsFile)
        //            {
        //                //上传文件
        //                formdata = string.Format(
        //                    fileFormdataTemplate,
        //                    item.Key, //表单键
        //                    item.FileName);
        //            }
        //            else
        //            {
        //                //上传文本
        //                formdata = string.Format(
        //                    dataFormdataTemplate,
        //                    item.Key,
        //                    item.Value);
        //            }

        //            //统一处理
        //            byte[] formdataBytes = null;
        //            //第一行不需要换行
        //            if (postStream.Length == 0)
        //                formdataBytes = Encoding.UTF8.GetBytes(formdata.Substring(2, formdata.Length - 2));
        //            else
        //                formdataBytes = Encoding.UTF8.GetBytes(formdata);
        //            postStream.Write(formdataBytes, 0, formdataBytes.Length);

        //            //写入文件内容
        //            if (item.FileContent != null && item.FileContent.Length > 0)
        //            {
        //                using (var stream = item.FileContent)
        //                {
        //                    byte[] buffer = new byte[1024];
        //                    int bytesRead = 0;
        //                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
        //                    {
        //                        postStream.Write(buffer, 0, bytesRead);
        //                    }
        //                }
        //            }
        //        }
        //        //结尾
        //        var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
        //        postStream.Write(footer, 0, footer.Length);

        //    }
        //    else
        //    {
        //        request.ContentType = "application/x-www-form-urlencoded";
        //    }
        //    #endregion

        //    request.ContentLength = postStream.Length;

        //    #region 输入二进制流
        //    if (postStream != null)
        //    {
        //        postStream.Position = 0;
        //        //直接写入流
        //        Stream requestStream = request.GetRequestStream();

        //        byte[] buffer = new byte[1024];
        //        int bytesRead = 0;
        //        while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
        //        {
        //            requestStream.Write(buffer, 0, bytesRead);
        //        }

        //        ////debug
        //        //postStream.Seek(0, SeekOrigin.Begin);
        //        //StreamReader sr = new StreamReader(postStream);
        //        //var postStr = sr.ReadToEnd();
        //        postStream.Close();//关闭文件访问
        //    }
        //    #endregion

        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    if (cookieContainer != null)
        //    {
        //        response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
        //    }

        //    using (Stream responseStream = response.GetResponseStream())
        //    {
        //        using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
        //        {
        //            string retString = myStreamReader.ReadToEnd();
        //            return retString;
        //        }
        //    }
        //}
    }
}
