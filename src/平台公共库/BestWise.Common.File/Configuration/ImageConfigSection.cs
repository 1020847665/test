using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;

namespace BestWise.Common.File.Configuration
{
    public class ImageConfigSection : ConfigurationSection
    {
        private const string SectionPath = "imageConfig";
        private const string ImageAttribute = "image";

        /// <summary>
        /// 图片节点配置
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ImageElementCollection Images { get { return (ImageElementCollection)base[""]; } }

        #region Methods
        /// <summary>
        /// Gets an instance of <see cref="ImageConfigSection"/> that represents the current configuration.
        /// </summary>
        /// <returns>An instance of <see cref="ImageConfigSection"/>, or null if no configuration is specified.</returns>
        public static ImageConfigSection GetConfiguration()
        {
            try
            {
                return ConfigurationManager.GetSection(SectionPath) as ImageConfigSection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片所有 Size 的路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <returns>返回图片所有 Size 的路径</returns>
        public static IDictionary<string,string> GetImagePaths(string type,string path)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentNullException("type");
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            IDictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                ImageElement _ie = GetConfiguration().Images[type.Trim().ToLower()];
                if (_ie != null)
                {
                    int pos = path.Contains("/") ? path.LastIndexOf('/') : 0;
                    string _root = path.Substring(0, pos), _fileName = path.Remove(0, pos + 1);
                    if (_ie.IsSaveOriginal) result.Add("Original", path);
                    if(_ie.Sizes != null && _ie.Sizes.Count > 0)
                    {
                        foreach (SizeElement item in _ie.Sizes)
                        {
                            result.Add(item.Name, String.Format(item.Path, _root.TrimEnd('/'), _fileName.Trim('/')));
                        }
                    }
                }
                else
                    throw new Exception("该类型在图片配置信息中不存在！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (result != null && result.Count == 0) result = null;
            return result;
        }

        /// <summary>
        /// 根据图片配置类型和图片基本路径，返回图片默认 Size 的路径
        /// </summary>
        /// <param name="type">图片配置类型</param>
        /// <param name="path">图片基本路径</param>
        /// <returns>返回图片默认 Size 的路径</returns>
        public static string GetImageDefaultPath(string type,string path)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentNullException("type");
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            string result = String.Empty; ;
            try
            {
                ImageElement _ie = GetConfiguration().Images[type.Trim().ToLower()];
                if (_ie != null)
                {
                    int pos = path.Contains("/") ? path.LastIndexOf('/') : 0;
                    string _root = path.Substring(0, pos), _fileName = path.Remove(0, pos + 1);
                    if (_ie.Sizes != null && _ie.Sizes.Count > 0)
                    {
                        foreach (SizeElement item in _ie.Sizes)
                        {
                            if (item.IsDefault)
                            {
                                result = String.Format(item.Path, _root.TrimEnd('/'), _fileName.Trim('/'));
                                break;
                            }
                        }
                    }
                    if (String.IsNullOrWhiteSpace(result) && _ie.IsSaveOriginal)
                        result = path;
                }
                else
                    throw new Exception("该类型在图片配置信息中不存在！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
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
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentNullException("type");
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");
            string result = path;
            if (size == null) size = Size.Empty;
            try
            {
                ImageElement _ie = GetConfiguration().Images[type.Trim().ToLower()];
                if (_ie != null)
                {
                    int pos = path.LastIndexOf('/'), differenc = int.MaxValue, _differenc = 0;
                    string _root = path.Substring(0, pos), _fileName = path.Remove(0, pos + 1);
                    if (_ie.Sizes != null && _ie.Sizes.Count > 0)
                    {
                        foreach (SizeElement item in _ie.Sizes)
                        {
                            _differenc = Math.Abs(item.Width - size.Width) + Math.Abs(item.Height - size.Height);
                            if (differenc > _differenc)
                            {
                                differenc = _differenc;
                                result = String.Format(item.Path, _root.TrimEnd('/'), _fileName.Trim('/'));
                            }
                        }
                    }
                }
                else
                    throw new Exception("该类型在图片配置信息中不存在！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
    }
}
