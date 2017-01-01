using System;
using System.Configuration;

namespace BestWise.Common.File.Configuration
{
    public class ImageElement : ConfigurationElement
    {
        private const string TypeAttribute = "type";
        private const string RootPathAttribute = "rootPath";
        private const string MaxContentLength = "maxContentLength";
        private const string SizesAttribute = "sizes";
        private const string IsSaveOriginalAttribute = "original";
        private const string IsFileNameWithSizeAttribute = "fileNameWithSize";
        private const string qualityAttribute = "quality";


        /// <summary>
        /// 图片类型
        /// </summary>
        [ConfigurationProperty(TypeAttribute, IsKey = true, IsRequired = true)]
        public string Type { get { return String.IsNullOrWhiteSpace(base[TypeAttribute] as string) ? String.Empty : base[TypeAttribute].ToString().ToLower().Trim(); } set { base[TypeAttribute] = String.IsNullOrWhiteSpace(value) ?String.Empty:value.ToLower().Trim(); } }

        /// <summary>
        /// 是否保存原图片文件
        /// </summary>
        [ConfigurationProperty(IsSaveOriginalAttribute)]
        public bool IsSaveOriginal { get { return base[IsSaveOriginalAttribute] != null ? Convert.ToBoolean(base[IsSaveOriginalAttribute]) : false; } set { base[IsSaveOriginalAttribute] = value; } }

        /// <summary>
        /// 保存文件名是否包含有图片 SIze 的信息
        /// </summary>
        [ConfigurationProperty(IsFileNameWithSizeAttribute)]
        public bool IsFileNameWithSize { get { return base[IsFileNameWithSizeAttribute] != null ? Convert.ToBoolean(base[IsFileNameWithSizeAttribute]) : false; } set { base[IsFileNameWithSizeAttribute] = value; } }

        /// <summary>
        /// 图片存放的根路径
        /// </summary>
        [ConfigurationProperty(RootPathAttribute)]
        public string RootPath { get { return base[RootPathAttribute] as string; } set { base[RootPathAttribute] = value; } }

        /// <summary>
        /// 图片上传大小 （默认为500KB，单位为: bytes，为0时，表示不做限制）
        /// </summary>
        [ConfigurationProperty(MaxContentLength, DefaultValue = 512000)]
        public int ContentLength { get { return base[MaxContentLength] != null ? Convert.ToInt32(base[MaxContentLength]) : 512000; } set { base[MaxContentLength] = value; } }

        /// <summary>
        /// 图片生成的品质 （默认为-1，表示使用系统统一的品质压缩值）
        /// </summary>
        [ConfigurationProperty(qualityAttribute, DefaultValue = -1)]
        public int Quality { get { return base[qualityAttribute] != null ? Convert.ToInt32(base[qualityAttribute]) : -1; } set { base[qualityAttribute] = value; } }

        /// <summary>
        /// 图片的 Size 列表
        /// </summary>
        [ConfigurationProperty(SizesAttribute, IsRequired = true)]
        public SizeElementCollection Sizes { get { return base[SizesAttribute] as SizeElementCollection; } }
    }
}