using BestWise.Common.File.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace BestWise.Common.File
{
    public class Image
    {
        /// <summary>
        /// 获取图片的 Size
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <returns>图片的 Size</returns>
        public static Size GetSize(string path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            Size result = Size.Empty;
            return result;
        }


        /// <summary>
        /// 图片等比例缩放后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="bytes">图片二进制数据</param>
        /// <param name="size">图片的 Size</param>
        /// <returns>是否保存成功</returns>
        public static bool Zoom(string path, byte[] bytes, Size size)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            if (bytes == null) throw new ArgumentNullException("bytes");
            if (size == null) throw new ArgumentNullException("size");
            try
            {
                return File.Instantiate.Save(path, ImageHelper.Zoom(path, bytes, size));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 图片等比例缩放后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="bytes">图片二进制数据</param>
        /// <param name="size">图片的 Size</param>
        /// <param name="quality">处理后图片的品质</param>
        /// <returns>是否保存成功</returns>
        public static bool Zoom(string path, byte[] bytes, Size size, int quality)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            if (bytes == null) throw new ArgumentNullException("bytes");
            if (size == null) throw new ArgumentNullException("size");
            try
            {
                return File.Instantiate.Save(path, ImageHelper.Zoom(path, bytes, size, quality));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 图片等比例缩放后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="base64">图片 base64 字符串</param>
        /// <param name="size">图片的 Size</param>
        /// <returns>是否保存成功</returns>
        public static bool Zoom(string path, string base64, Size size)
        {
            if (String.IsNullOrWhiteSpace(base64)) throw new ArgumentNullException("base64");
            return Zoom(path, Convert.FromBase64String(base64), size);
        }

        /// <summary>
        /// 图片等比例缩放后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="base64">图片 base64 字符串</param>
        /// <param name="size">图片的 Size</param>
        /// <param name="quality">处理后图片的品质</param>
        /// <returns>是否保存成功</returns>
        public static bool Zoom(string path, string base64, Size size, int quality)
        {
            if (String.IsNullOrWhiteSpace(base64)) throw new ArgumentNullException("base64");
            return Zoom(path, Convert.FromBase64String(base64), size, quality);
        }

        /// <summary>
        /// 最大适应裁剪图片后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="bytes">图片二进制数据</param>
        /// <param name="size">图片的 Size</param>
        /// <returns>是否保存成功</returns>
        public static bool Crop(string path, byte[] bytes, Size size)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            if (bytes == null) throw new ArgumentNullException("bytes");
            if (size == null) throw new ArgumentNullException("size");
            try
            {
                return File.Instantiate.Save(path, ImageHelper.Crop(path, bytes, size));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 最大适应裁剪图片后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="bytes">图片二进制数据</param>
        /// <param name="size">图片的 Size</param>
        /// <param name="quality">处理后图片的品质</param>
        /// <returns>是否保存成功</returns>
        public static bool Crop(string path, byte[] bytes, Size size, int quality)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            if (bytes == null) throw new ArgumentNullException("bytes");
            if (size == null) throw new ArgumentNullException("size");
            try
            {
                return File.Instantiate.Save(path, ImageHelper.Crop(path, bytes, size, quality));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 最大适应裁剪图片后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="base64">图片 base64 字符串</param>
        /// <param name="size">图片的 Size</param>
        /// <returns>是否保存成功</returns>
        public static bool Crop(string path, string base64, Size size)
        {
            if (String.IsNullOrWhiteSpace(base64)) throw new ArgumentNullException("base64");
            return Crop(path, Convert.FromBase64String(base64), size);
        }

        /// <summary>
        /// 最大适应裁剪图片后保存
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="base64">图片 base64 字符串</param>
        /// <param name="size">图片的 Size</param>
        /// <param name="quality">处理后图片的品质</param>
        /// <returns>是否保存成功</returns>
        public static bool Crop(string path, string base64, Size size, int quality)
        {
            if (String.IsNullOrWhiteSpace(base64)) throw new ArgumentNullException("base64");
            return Crop(path, Convert.FromBase64String(base64), size, quality);
        }

        public static string Save(string type, byte[] bytes)
        {
           return Save(type, "image.png", bytes);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="type">图片地址</param>
        /// <param name="fileName">图片文件名（用于获取扩展名，不做保存文件名）</param>
        /// <param name="bytes">图片二进制数据</param>
        /// <returns>保存后主图片的路径，为空时，表示上传不成功！</returns>
        public static string Save(string type, string fileName, byte[] bytes)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentNullException("type");
            if (bytes == null) throw new ArgumentNullException("bytes");
            string result = String.Empty;
            int _quality = -1, _sizeQuality = -1;
            try
            {
                ImageElement _ie = ImageConfigSection.GetConfiguration().Images[type.Trim().ToLower()];
                if (_ie != null)
                {
                    _quality = _ie.Quality;
                    if (_ie.ContentLength > 0 && bytes.Length > _ie.ContentLength) throw new Exception("上传图片大于限制字节数，上传失败！");
                    bool _result = true;
                    string _root = String.Format(_ie.RootPath, DateTime.Now.ToString("yyyyMMdd")), _fileName = (Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture) + Path.GetExtension(fileName)).ToLower(), _path;
                    if (_ie.IsFileNameWithSize)
                    {
                        Size _oSize = ImageHelper.GetSize(bytes);
                        _fileName = _oSize.Width + "_" + _oSize.Height + "_" + _fileName;
                    }
                    List<string> saved = new List<string>();
                    #region 保存原图
                    if (_ie.IsSaveOriginal)
                    {
                        _path = _root.TrimEnd('/') + "/" + _fileName;
                        _result = File.Instantiate.Save(_path, bytes);
                        if (_result) saved.Add(_path);
                    }
                    #endregion
                    Size _size;
                    if (_result && _ie.Sizes != null && _ie.Sizes.Count > 0)
                    {
                        foreach (SizeElement item in _ie.Sizes)
                        {
                            _sizeQuality = item.Quality > 0 ? item.Quality : _quality;
                            _path = String.Format(item.Path, _root.TrimEnd('/'), _fileName);
                            _size = new Size(item.Width, item.Height);
                            _result = item.IsCrop ? Crop(_path, bytes, _size, _sizeQuality) : Zoom(_path, bytes, _size, _sizeQuality);
                            if (_result)
                                saved.Add(_path);
                            else
                                break;
                        }
                    }
                    if ((!_result) && saved != null && saved.Count > 0)
                    {
                        foreach (string p in saved)
                        {
                            File.Instantiate.Delete(p);
                        }
                    }
                    else
                        result = _root.TrimEnd('/') + "/" + _fileName; ;
                }
                else
                {
                    throw new Exception("该类型在图片配置信息中不存在！");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="type">图片地址</param>
        /// <param name="fileName">图片文件名（用于获取扩展名，不做保存文件名）</param>
        /// <param name="base64">图片 base64 字符串</param>
        /// <returns>保存后主图片的路径，为空时，表示上传不成功！</returns>
        public static string Save(string type, string fileName, string base64)
        {
            if (String.IsNullOrWhiteSpace(base64)) throw new ArgumentNullException("base64");
            return Save(type, fileName, Convert.FromBase64String(base64));
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，删除所有 Size 的图片
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <returns>是否删除成功</returns>
        public static bool Delete(string type, string path)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentNullException("type");
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            bool result = false;
            try
            {
                if (!ImageHelper.IsOldImage(path))
                {
                    IDictionary<string, string> paths = GetImagePaths(type, path);
                    if (paths != null && paths.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> item in paths)
                        {
                            if (!String.IsNullOrWhiteSpace(item.Value)) File.Instantiate.Delete(item.Value);
                        }
                    }
                }
                else
                {
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs40/40_"));
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs60/60_"));
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs100/100_"));
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs160/160_"));
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs180/180_"));
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs220/220_"));
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs310/310_"));
                    File.Instantiate.Delete(path.Replace(@"/images/", @"/thumbs410/410_"));
                    File.Instantiate.Delete(path);
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片所有 Size 的路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <returns>返回图片所有 Size 的路径</returns>
        public static IDictionary<string, string> GetImagePaths(string type, string path)
        {
            return ImageConfigSection.GetImagePaths(type, path);
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片指定键的路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <param name="key">图片基本路径键</param>
        /// <returns>返回图片所有 Size 的路径</returns>
        public static string GetImagePath(string type, string path, string key)
        {
            string result = String.Empty;
            IDictionary<string, string> paths = ImageConfigSection.GetImagePaths(type, path);
            if (paths != null && paths.ContainsKey(key))
                result = paths[key];
            return result;
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片所有 Size 的 URL 路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <returns>返回图片所有 Size 的 URL 路径</returns>
        public static IDictionary<string, string> GetImageUrls(string type, string path)
        {
            IDictionary<string, string> result = null;
            IDictionary<string, string> paths = GetImagePaths(type, path);
            if (paths != null && paths.Count > 0)
            {
                result = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> item in result)
                {
                    result.Add(item.Key, ImageHelper.GetImageUrl(item.Value));
                }
            }
            return result;
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片指定键的 URL 路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <param name="key">图片基本路径键</param>
        /// <returns>返回图片所有 Size 的 URL 路径</returns>
        public static string GetImageUrl(string type, string path, string key)
        {
            if (String.IsNullOrWhiteSpace(path)) return ImageHelper.GetImageUrl(path);
            if (ImageHelper.IsOldImage(path)) return ImageHelper.GetImageUrl(path);
            return ImageHelper.GetImageUrl(GetImagePath(type, path, key));
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片默认 Size 的 URL 路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <returns>返回图片默认 Size 的 URL 路径</returns>
        public static string GetImageUrl(string type, string path)
        {
            if (String.IsNullOrWhiteSpace(path)) return ImageHelper.GetImageUrl(path);
            if (ImageHelper.IsOldImage(path)) return ImageHelper.GetImageUrl(path);
            return ImageHelper.GetImageUrl(ImageConfigSection.GetImageDefaultPath(type, path));
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片默认 Size 的路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <returns>返回图片默认 Size 的路径</returns>
        public static string GetImagePath(string type, string path)
        {
            return ImageConfigSection.GetImageDefaultPath(type, path);
        }

        /// <summary>
        /// 获取最合适显示 Size 的图片的路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <param name="size"></param>
        /// <returns>返回最合适显示 Size 的图片的路径</returns>
        public static string GetBestImagePath(string type, string path, Size size)
        {
            return ImageConfigSection.GetBestImagePath(type, path, size);
        }

        /// <summary>
        /// 获取最合适显示 Size 的图片的 URL 路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <param name="size"></param>
        /// <returns>返回最合适显示 Size 的图片的 URL 路径</returns>
        public static string GetBestImageUrl(string type, string path, Size size)
        {
            return ImageHelper.GetImageUrl(GetBestImagePath(type, path, size));
        }
    }
}