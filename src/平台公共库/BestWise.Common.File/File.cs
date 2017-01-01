using BestWise.Common.File.Configuration;
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;

namespace BestWise.Common.File
{
    public class File
    {
        private static BestWise.Common.File.Providers.TaskProviderCollection _fileProviders = null;

        private static File _Instantiate = null;

        public static File Instantiate
        {
            get { return _Instantiate ?? (_Instantiate = new File()); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public File()
        {
            _fileProviders = BestWise.Common.File.Providers.ProviderManager.GetProviders();
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>是否存在</returns>
        public bool Exists(string path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            bool result = false;
            if (_fileProviders != null && _fileProviders.Count > 0)
            {
                foreach (BestWise.Common.File.Providers.BaseProvider provider in _fileProviders)
                {
                    result = provider.IsExistsByFile(path);
                    if (!result) break;
                }
            }
            return result;
        }

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>删除是否成功</returns>
        public bool Delete(string path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            bool result = false;
            if (_fileProviders != null && _fileProviders.Count > 0)
            {
                foreach (BestWise.Common.File.Providers.BaseProvider provider in _fileProviders)
                {
                    result = provider.DeleteFile(path);
                }
            }
            return result;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <param name="bytes">文件二进制数据</param>
        /// <returns>保存文件是否成功！</returns>
        public bool Save(string path, byte[] bytes)
        {
            bool result = false;
            try
            {
                if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
                if (bytes == null || bytes.Length == 0) throw new ArgumentNullException("bytes", "File.Save bytes 不能为 null;path:" + path);
                if (_fileProviders != null && _fileProviders.Count > 0)
                {
                    foreach (BestWise.Common.File.Providers.BaseProvider provider in _fileProviders)
                    {
                        result = provider.UploadFile(bytes, path);
                    }
                }
                else
                {
                    //System.IO.FileInfo _info = new System.IO.FileInfo(string.Concat(HttpContext.Current.Server.MapPath("~/"), path));
                    System.IO.FileInfo _info = new System.IO.FileInfo(path.GetMapPath());
                    if (!_info.Directory.Exists) _info.Directory.Create();
                    System.IO.File.WriteAllBytes(_info.FullName, bytes);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            if (!result)
            {
                Delete(path);
            }
            return result;
        }

        #region 导入Excel信息
        /// <summary>
        /// 导入Excel信息
        /// </summary>
        /// <returns></returns>
        public ResultMessage<DataTable> ImportExcel(string excelType)
        {
            ResultMessage<DataTable> result = ResultMessage<DataTable>.FailureResult();
            try
            {
                string fileSavePath = String.Empty;//文件保存路径
                DataTable dt = null;
                if (!string.IsNullOrWhiteSpace(excelType))
                {
                    HttpRequest request = System.Web.HttpContext.Current.Request;
                    byte[] bytes = null;
                    if (request.Files != null && request.Files.Count > 0)
                    {
                        foreach (string key in request.Files.AllKeys)
                        {
                            HttpPostedFile httpPostedFile = request.Files[key];
                            using (MemoryStream ms = new MemoryStream())
                            {
                                httpPostedFile.InputStream.CopyTo(ms);
                                bytes = ms.ToArray();
                            }
                            if (bytes != null && bytes.Length > 0)
                            {
                                fileSavePath = SaveFile(excelType, bytes);
                                break;
                            }
                            else result = ResultMessage<DataTable>.FailureResult("请选择上传文件！");
                        }
                    }
                    else if (request.InputStream != null && request.InputStream.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            request.InputStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }
                        if (bytes != null && bytes.Length > 0)
                            fileSavePath = SaveFile(excelType, bytes);
                        else result = ResultMessage<DataTable>.FailureResult("请选择上传文件！");
                    }
                }
                else result = ResultMessage<DataTable>.FailureResult("文件用途不明确！");
                if (!string.IsNullOrWhiteSpace(fileSavePath))
                {
                    string savePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, fileSavePath);
                    dt = AsposeCellHelper.GetExcelData(savePath);
                    result = ResultMessage<DataTable>.SucceedResult(dt);
                    System.IO.File.Delete(savePath);
                }
            }
            catch (Exception ex)
            {
                result = ResultMessage<DataTable>.FailureResult(ex);
            }
            return result;
        }
        #endregion


        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="bytes">文件二进制数据</param>
        /// <param name="fileType">文件类型</param>
        /// <returns>保存文件是否成功！</returns>
        public string ConfigureSave(string name, byte[] bytes, string fileType)
        {
            bool result = false;
            string path = string.Empty;
            try
            {
                if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("文件节点名称不能为空");
                if (bytes == null) throw new ArgumentNullException("文件不能为空");
                FileElement fileElement = FileConfigSection.GetConfiguration().Files[name.Trim().ToLower()];
                if (fileElement != null)
                {
                    if (fileElement.ContentLength > 0 && bytes.Length > fileElement.ContentLength) throw new Exception("上传文件大于限制字节数，上传失败！");
                    string fileName = string.Concat(Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture), fileType).ToLower(),
                      root = String.Format(fileElement.RootPath, DateTime.Now.ToString("yyyyMMdd"));
                    path = string.Concat(root.TrimEnd('/'), "/", fileName);
                    if (_fileProviders != null && _fileProviders.Count > 0)
                    {
                        foreach (BestWise.Common.File.Providers.BaseProvider provider in _fileProviders)
                        {
                            result = provider.UploadFile(bytes, path);
                        }
                        if (!result) path = string.Empty;
                    }
                    else
                    {
                        System.IO.FileInfo _info = new System.IO.FileInfo(string.Concat(HttpContext.Current.Server.MapPath("~/"), path));
                        if (!_info.Directory.Exists) _info.Directory.Create();
                        System.IO.File.WriteAllBytes(_info.FullName, bytes);
                    }
                }
                else throw new Exception("该类型在文件配置信息中不存在！");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return path;
        }


        #region 保存文件
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="bytes">文件二进制数据</param>
        /// <returns>保存文件是否成功！</returns>
        public string SaveFile(string name, byte[] bytes)
        {
            string path = string.Empty;
            try
            {
                if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("文件节点名称不能为空");
                if (bytes == null) throw new ArgumentNullException("文件不能为空");
                FileElement fileElement = FileConfigSection.GetConfiguration().Files[name.Trim().ToLower()];
                if (fileElement != null)
                {
                    if (fileElement.ContentLength > 0 && bytes.Length > fileElement.ContentLength) throw new Exception("上传文件大于限制字节数，上传失败！");
                    string fileName = string.Concat(Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture), ".", fileElement.Type).ToLower(),
                      root = String.Format(fileElement.RootPath, DateTime.Now.ToString("yyyyMMdd"));
                    path = string.Concat(root.TrimEnd('/'), "/", fileName);
                    System.IO.FileInfo info = new System.IO.FileInfo(string.Concat(HttpContext.Current.Server.MapPath("~/"), path));
                    if (!info.Directory.Exists) info.Directory.Create();
                    System.IO.File.WriteAllBytes(info.FullName, bytes);
                }
                else throw new Exception("该类型在文件配置信息中不存在！");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return path;
        }
        #endregion





        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>文件的二进制数据</returns>
        public byte[] Read(string path)
        {
            byte[] result = null;
            try
            {
                if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
                if (_fileProviders != null && _fileProviders.Count > 0)
                {
                    foreach (BestWise.Common.File.Providers.BaseProvider provider in _fileProviders)
                    {
                        result = provider.GetFileBytes(path);
                        if (result != null) break;
                    }
                }
                else
                {
                    string _path = HttpContext.Current.Server.MapPath(path);
                    if (System.IO.File.Exists(_path)) result = System.IO.File.ReadAllBytes(_path);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }
    }
}