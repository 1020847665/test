using System;
using System.Configuration;

namespace BestWise.Common.File.Configuration
{
    /// <summary>
    /// 文件节点
    /// </summary>
    public class FileElement : ConfigurationElement
    {
        private const string NameAttribute = "name";
        private const string TypeAttribute = "type";
        private const string RootPathAttribute = "rootPath";
        private const string MaxContentLength = "maxContentLength";

        /// <summary>
        /// 文件类型
        /// </summary>
        [ConfigurationProperty(TypeAttribute, IsKey = true, IsRequired = true)]
        public string Type { get { return String.IsNullOrWhiteSpace(base[TypeAttribute] as string) ? String.Empty : base[TypeAttribute].ToString().ToLower().Trim(); } set { base[TypeAttribute] = String.IsNullOrWhiteSpace(value) ? String.Empty : value.ToLower().Trim(); } }

        /// <summary>
        /// 文件名称
        /// </summary>
        [ConfigurationProperty(NameAttribute, IsKey = true, IsRequired = true)]
        public string Name { get { return String.IsNullOrWhiteSpace(base[NameAttribute] as string) ? String.Empty : base[NameAttribute].ToString().ToLower().Trim(); } set { base[NameAttribute] = String.IsNullOrWhiteSpace(value) ? String.Empty : value.ToLower().Trim(); } }

        /// <summary>
        /// 文件存放的根路径
        /// </summary>
        [ConfigurationProperty(RootPathAttribute)]
        public string RootPath { get { return base[RootPathAttribute] as string; } set { base[RootPathAttribute] = value; } }

        /// <summary>
        /// 文件上传的大小 （默认为500KB，单位为: bytes，为0时，表示不做限制）
        /// </summary>
        [ConfigurationProperty(MaxContentLength, DefaultValue = 512000)]
        public int ContentLength { get { return base[MaxContentLength] != null ? Convert.ToInt32(base[MaxContentLength]) : 512000; } set { base[MaxContentLength] = value; } }
    }
}
