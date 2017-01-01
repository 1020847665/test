using BestWise.Common.File.UpYun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.Common.File.Providers
{
    public class UpYunProvider : BaseProvider
    {
        private Worker _worker = null;
        protected Worker Worker
        {
            get
            {
                if (_worker == null)
                    _worker = new Worker(this.SpaceName, this.UserName, this.Password, this.ApiDomain);
                return _worker;
            }
        }

        /// <summary>
        ///  空间名称
        /// </summary>
        protected string SpaceName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        protected string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        protected string Password { get; set; }

        /// <summary>
        /// API接口域名
        /// </summary>
        protected string ApiDomain { get; set; }



        #region 判断文件是否存在
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>是否存在</returns>
        public override bool IsExistsByFile(string path)
        {
            bool result = false;
            using (HttpResponseMessage response = this.Worker.Request(HttpMethod.Head, path))
            {
                result = response.IsSuccessStatusCode;
            }
            return result;
        }
        #endregion

        #region 删除指定的文件
        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>删除是否成功</returns>
        public override bool DeleteFile(string path)
        {
            bool result = false;
            using (HttpResponseMessage response = this.Worker.Request(HttpMethod.Delete, path))
            {
                result = response.IsSuccessStatusCode;
            }
            return result;
        }
        #endregion

        #region 上传文件到服务器
        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="context">上传的二进制数据</param>
        /// <param name="path">文件路径</param>
        public override bool UploadFile(byte[] context, string path)
        {
            bool result = false;
            this.Worker.IsAutoCreateDirectory = true;
            using (HttpResponseMessage response = this.Worker.Request(HttpMethod.Put, path, context))
            {
                result = response.IsSuccessStatusCode;
            }
            return result;
        }
        #endregion

        #region 得到服务文件信息
        /// <summary>
        /// 得到服务文件信息
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns>文件信息</returns>
        public Hashtable GetFileInfo(string path)
        {
            Hashtable result = new Hashtable();
            using (HttpResponseMessage response = this.Worker.Request(HttpMethod.Head, path))
            {
                try
                {
                    result = new Hashtable();
                    if (response.IsSuccessStatusCode)
                    {
                        if (this.Worker.Infos != null)
                        {
                            foreach (string key in this.Worker.Infos.Keys)
                            {
                                if (this.Worker.Infos[key] != null && (!String.IsNullOrWhiteSpace(this.Worker.Infos[key].ToString())))
                                {
                                    result.Add(key.Substring(key.LastIndexOf('-') + 1), this.Worker.Infos[key]);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex);
                }
            }
            return result;
        }
        #endregion

        #region 得到服务文件二进制数据
        /// <summary>
        /// 得到服务文件二进制数据
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns>文件信息</returns>
        public override byte[] GetFileBytes(string path)
        {
            byte[] result = null;

            using (HttpResponseMessage response = this.Worker.Request(HttpMethod.Get, path))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (BinaryReader br = new BinaryReader(response.Content.ReadAsStreamAsync().Result))
                    {
                        result = br.ReadBytes(1024 * 1024 * 100); /// 又拍云存储最大文件限制 100Mb，对于普通用户可以改写该值，以减少内存消耗
                    }
                }
            }
            return result;
        }
        #endregion

        #region 创建目录
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public override void MakeDirectory(string path)
        {
            Hashtable headers = new Hashtable();
            headers.Add("folder", "create");
            this.Worker.IsAutoCreateDirectory = true;
            this.Worker.Request(HttpMethod.Post, path, headers);
        }
        #endregion

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config != null && config.Count > 0)
            {
                this.SpaceName = config["spaceName"];
                this.UserName = config["userName"];
                this.Password = config["password"];
                this.ApiDomain = config["apiDomain"];
            }
            base.Initialize(name, config);
        }
    }
}
