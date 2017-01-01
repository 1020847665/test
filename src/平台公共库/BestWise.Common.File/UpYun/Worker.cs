using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace BestWise.Common.File.UpYun
{
    public class Worker
    {
        #region 属性
        private string _bucketname = string.Empty;//空间名称
        private string _username = string.Empty;//操作员名称
        private string _password = string.Empty;// 密码
        private bool _upAuth = false;
        private string _api_domain = string.Empty;// 切换 API 接口的域名(默认 v0.api.upyun.com 自动识别, v1.api.upyun.com 电信, v2.api.upyun.com 联通, v3.api.upyun.com 移动)
        private string _dl = "/";
        private Hashtable _tmp_infos = new Hashtable();
        private string _file_secret;
        private string _content_md5;
        private bool _auto_mkdir = false;

        /// <summary>
        /// 设置待上传文件的 Content-MD5 值（如又拍云服务端收到的文件MD5值与用户设置的不一致，将回报 406 Not Acceptable 错误）
        /// </summary>
        public string ContentMd5 { set { _content_md5 = value; } }

        /// <summary>
        /// 设置待上传文件的 访问密钥（注意：仅支持图片空！，设置密钥后，无法根据原文件URL直接访问，需带 URL 后面加上 （缩略图间隔标志符+密钥） 进行访问）
        /// 如缩略图间隔标志符为 ! ，密钥为 bac，上传文件路径为 /folder/test.jpg ，那么该图片的对外访问地址为： http://空间域名/folder/test.jpg!bac
        /// </summary>
        public string FileSecret { set { _file_secret = value; } }

        public bool IsAutoCreateDirectory { set { _auto_mkdir = value; } }

        /// <summary>
        /// 
        /// </summary>
        public Hashtable Infos { get { return this._tmp_infos; } }


        public string version() { return "1.0.1"; }

        #endregion

        public Worker(string SpaceName, string UserName, string Password, string ApiDomain)
        {
            this._bucketname = SpaceName;
            this._username = UserName;
            this._password = Password;
            this._api_domain = string.IsNullOrWhiteSpace(ApiDomain) ? "v0.api.upyun.com" : ApiDomain;
        }

    
        #region
        /// <summary>
        /// 得到一个 HttpWebResponse 实例
        /// </summary>
        /// <param name="method">请求的动作</param>
        /// <param name="Url">相对路径</param>
        /// <returns>HttpWebResponse 实例</returns>
        public HttpWebResponse GetResponse(HttpMethod method, string Url)
        {
            return GetResponse(method, Url, new Hashtable());
        }

        /// <summary>
        /// 得到一个 HttpWebResponse 实例
        /// </summary>
        /// <param name="method">请求的动作</param>
        /// <param name="Url">相对路径</param>
        /// <param name="headers">Http 请求头</param>
        /// <returns>HttpWebResponse 实例</returns>
        public HttpWebResponse GetResponse(HttpMethod method, string Url, Hashtable headers)
        {
            return GetResponse(method, Url, (byte[])null, headers);
        }

        /// <summary>
        /// 得到一个 HttpWebResponse 实例
        /// </summary>
        /// <param name="method">请求的动作</param>
        /// <param name="Url">相对路径</param>
        /// <param name="postData">二进制数据</param>
        /// <returns>HttpWebResponse 实例</returns>
        public HttpWebResponse GetResponse(HttpMethod method, string Url, byte[] postData)
        {
            return GetResponse(method, Url, postData, new Hashtable());
        }

        /// <summary>
        /// 得到一个 HttpWebResponse 实例
        /// </summary>
        /// <param name="method">请求的动作</param>
        /// <param name="Url">相对路径</param>
        /// <param name="postData">二进制数据</param>
        /// <param name="headers">Http 请求头</param>
        /// <returns>HttpWebResponse 实例</returns>
        public HttpWebResponse GetResponse(HttpMethod method, string Url, byte[] postData, Hashtable headers)
        {
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create("http://" + _api_domain + this._dl + this._bucketname + ("/" + Url.TrimStart('/')));
            request.Method = method.Method.ToUpper();
            //System.IO.File.AppendAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\xxx.log", new string[] { request.RequestUri.AbsoluteUri, method.Method.ToUpper() });
            if (this._auto_mkdir)
            {
                headers.Add("mkdir", "true");
                this._auto_mkdir = false;
            }

            if (postData != null)
            {
                request.ContentLength = postData.Length;
                request.KeepAlive = true;
                if (!String.IsNullOrWhiteSpace(this._content_md5))
                {
                    request.Headers.Add("Content-MD5", this._content_md5);
                    this._content_md5 = null;
                }
                if (this._file_secret != null)
                {
                    request.Headers.Add("Content-Secret", this._file_secret);
                    this._file_secret = null;
                }
            }

            if (this._upAuth)
            {
                upyunAuth(headers, method, Url, request);
            }
            else
            {
                request.Headers.Add("Authorization", "Basic " +
                    Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(this._username + ":" + this._password)));
            }
            foreach (DictionaryEntry var in headers)
            {
                request.Headers.Add(var.Key.ToString(), var.Value.ToString());
            }

            if (postData != null)
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(postData, 0, postData.Length);
                    dataStream.Close();
                }
            }
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                this._tmp_infos = new Hashtable();
                foreach (var hl in response.Headers)
                {
                    string name = (string)hl;
                    if (name.Length > 7 && name.Substring(0, 7) == "x-upyun")
                    {
                        this._tmp_infos.Add(name, response.Headers[name]);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return response;
        }
        #endregion

        #region HttpClient 同步请求
        public HttpResponseMessage Request(HttpMethod method, string Url)
        {
            return Request(method, Url, (byte[])null, new Hashtable());
        }

        public HttpResponseMessage Request(HttpMethod method, string Url, Hashtable headers)
        {
            return Request(method, Url, (byte[])null, headers);
        }

        public HttpResponseMessage Request(HttpMethod method, string Url, byte[] bytes)
        {
            return Request(method, Url, bytes, new Hashtable());
        }
        public HttpResponseMessage Request(HttpMethod method, string Url, byte[] bytes, Hashtable headers)
        {
            HttpResponseMessage response;
            Uri uri = new Uri("http://" + _api_domain + this._dl + this._bucketname + ("/" + (Url.TrimStart('/'))));
            string requestUri = String.Empty;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri.GetComponents(UriComponents.Scheme | UriComponents.HostAndPort, UriFormat.SafeUnescaped));
                if (uri.AbsolutePath.TrimStart('/').Length > 0) requestUri += (uri.AbsolutePath.TrimStart('/'));

                if (this._auto_mkdir)
                {
                    headers.Add("mkdir", "true");
                    this._auto_mkdir = false;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(this._username + ":" + this._password)));
                if (bytes != null)
                {
                    //client.DefaultRequestHeaders.Add("Content-Length", bytes.LongLength.ToString());
                    if (!String.IsNullOrWhiteSpace(this._content_md5))
                    {
                        client.DefaultRequestHeaders.Add("Content-MD5", this._content_md5);
                        this._content_md5 = null;
                    }
                    if (this._file_secret != null)
                    {
                        client.DefaultRequestHeaders.Add("Content-Secret", this._file_secret);
                        this._file_secret = null;
                    }
                }

                foreach (DictionaryEntry var in headers)
                {
                    client.DefaultRequestHeaders.Add(var.Key.ToString(), var.Value.ToString());
                }

                using (HttpRequestMessage request = new HttpRequestMessage(method, requestUri))
                {
                    request.Method = method;

                    if (bytes != null)
                    {
                        //MediaTypeFormatter formatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            using (StreamContent content = new StreamContent(ms))
                            {
                                request.Content = content;
                                response = client.SendAsync(request, CancellationToken.None).Result;
                                if (response.IsSuccessStatusCode)
                                {
                                    this._tmp_infos = new Hashtable();
                                    foreach (var hl in response.Headers)
                                    {
                                        string name = (string)hl.Key;
                                        if (name.Length > 7 && name.Substring(0, 7) == "x-upyun")
                                        {
                                            this._tmp_infos.Add(name, hl.Value.FirstOrDefault(m => !String.IsNullOrWhiteSpace(m)));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        response = client.SendAsync(request, CancellationToken.None).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            this._tmp_infos = new Hashtable();
                            foreach (var hl in response.Headers)
                            {
                                string name = (string)hl.Key;
                                if (name.Length > 7 && name.Substring(0, 7) == "x-upyun")
                                {
                                    this._tmp_infos.Add(name, response.Headers.GetValues(name));
                                }
                            }
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region HttpClient 异步请求
        public Task<HttpResponseMessage> RequestAsync(HttpMethod method, string Url)
        {
            return RequestAsync(method, Url, (byte[])null, new Hashtable());
        }

        public Task<HttpResponseMessage> RequestAsync(HttpMethod method, string Url, Hashtable headers)
        {
            return RequestAsync(method, Url, (byte[])null, headers);
        }

        public Task<HttpResponseMessage> RequestAsync(HttpMethod method, string Url, byte[] bytes)
        {
            return RequestAsync(method, Url, bytes, new Hashtable());
        }

        public Task<HttpResponseMessage> RequestAsync(HttpMethod method, string Url, byte[] bytes, Hashtable headers)
        {
            return _RequestAsync(method, Url, bytes, headers);
        }

        private async Task<HttpResponseMessage> _RequestAsync(HttpMethod method, string Url, byte[] bytes, Hashtable headers)
        {
            HttpResponseMessage response;
            Uri uri = new Uri("http://" + _api_domain + this._dl + this._bucketname + ("/" + (Url.TrimStart('/'))));
            string requestUri = String.Empty;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri.GetComponents(UriComponents.Scheme | UriComponents.HostAndPort, UriFormat.SafeUnescaped));
                if (uri.AbsolutePath.TrimStart('/').Length > 0) requestUri += (uri.AbsolutePath.TrimStart('/'));

                if (this._auto_mkdir)
                {
                    headers.Add("mkdir", "true");
                    this._auto_mkdir = false;
                }

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(this._username + ":" + this._password)));
                if (bytes != null)
                {
                    //client.DefaultRequestHeaders.Add("Content-Length", bytes.LongLength.ToString());
                    if (!String.IsNullOrWhiteSpace(this._content_md5))
                    {
                        client.DefaultRequestHeaders.Add("Content-MD5", this._content_md5);
                        this._content_md5 = null;
                    }
                    if (this._file_secret != null)
                    {
                        client.DefaultRequestHeaders.Add("Content-Secret", this._file_secret);
                        this._file_secret = null;
                    }
                }

                foreach (DictionaryEntry var in headers)
                {
                    client.DefaultRequestHeaders.Add(var.Key.ToString(), var.Value.ToString());
                }

                using (HttpRequestMessage request = new HttpRequestMessage(method, requestUri))
                {
                    request.Method = method;

                    if (bytes != null)
                    {
                        //MediaTypeFormatter formatter = new System.Net.Http.Formatting.JsonMediaTypeFormatter();
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            using (StreamContent content = new StreamContent(ms))
                            {
                                request.Content = content;
                                response = await client.SendAsync(request, CancellationToken.None);
                                if (response.IsSuccessStatusCode)
                                {
                                    this._tmp_infos = new Hashtable();
                                    foreach (var hl in response.Headers)
                                    {
                                        string name = (string)hl.Key;
                                        if (name.Length > 7 && name.Substring(0, 7) == "x-upyun")
                                        {
                                            this._tmp_infos.Add(name, hl.Value.FirstOrDefault(m => !String.IsNullOrWhiteSpace(m)));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        response = client.SendAsync(request, CancellationToken.None).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            this._tmp_infos = new Hashtable();
                            foreach (var hl in response.Headers)
                            {
                                string name = (string)hl.Key;
                                if (name.Length > 7 && name.Substring(0, 7) == "x-upyun")
                                {
                                    this._tmp_infos.Add(name, response.Headers.GetValues(name));
                                }
                            }
                        }
                    }
                }
            }
            return response;
        }
        #endregion

        #region 私有方法
        private void upyunAuth(Hashtable headers, HttpMethod method, string uri, HttpWebRequest request)
        {
            DateTime dt = DateTime.UtcNow;
            string date = dt.ToString("ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));
            request.Date = dt;
            //headers.Add("Date", date);
            string auth;
            if (request.ContentLength != -1)
                auth = md5(method.Method.ToUpper() + '&' + uri + '&' + date + '&' + request.ContentLength + '&' + md5(this._password));
            else
                auth = md5(method.Method.ToUpper() + '&' + uri + '&' + date + '&' + 0 + '&' + md5(this._password));
            headers.Add("Authorization", "UpYun " + this._username + ':' + auth);
        }

        private AuthenticationHeaderValue upyunAuth(HttpMethod method, string uri, HttpRequestMessage request)
        {
            DateTime dt = DateTime.UtcNow;
            string date = dt.ToString("ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.CreateSpecificCulture("en-US"));
            request.Headers.Date = dt;
            //headers.Add("Date", date);
            string auth;
            if (request.Content.Headers.ContentLength != -1)
                auth = md5(method.Method.ToUpper() + '&' + uri + '&' + date + '&' + request.Content.Headers.ContentLength + '&' + md5(this._password));
            else
                auth = md5(method.Method.ToUpper() + '&' + uri + '&' + date + '&' + 0 + '&' + md5(this._password));
            return new AuthenticationHeaderValue("UpYun", this._username + ":" + auth);
        }

        private string md5(string str)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str));
            string resule = BitConverter.ToString(s);
            resule = resule.Replace("-", "");
            return resule.ToLower();
        }
        #endregion
    }
}
