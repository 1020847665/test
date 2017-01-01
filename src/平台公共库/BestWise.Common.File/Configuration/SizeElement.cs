using System;
using System.Configuration;

namespace BestWise.Common.File.Configuration
{
    public class SizeElement : ConfigurationElement
    {
        private const string NameAttribute = "name";
        private const string WidthAttribute = "width";
        private const string HeightAttribute = "height";
        private const string PathAttribute = "path";
        private const string IsCropAttribute = "crop";
        private const string IsDefaultAttribute = "default";
        private const string qualityAttribute = "quality";

        /// <summary>
        /// 图片大小别名
        /// </summary>
        [ConfigurationProperty(NameAttribute, IsKey = true, IsRequired = true)]
        public string Name { get { return base[NameAttribute] as string; } set { base[NameAttribute] = value; } }

        /// <summary>
        /// 图片最大宽度（不设置或为0时，表示宽度不做限制，单位为:像素(px)）
        /// </summary>
        [ConfigurationProperty(WidthAttribute, IsRequired = true)]
        public int Width { get { return base[WidthAttribute] != null ? Convert.ToInt32(base[WidthAttribute]) : 0; } set { base[WidthAttribute] = value; } }

        /// <summary>
        /// 图片最大高度（不设置或为0时，表示高度不做限制，单位为:像素(px)）
        /// </summary>
        [ConfigurationProperty(HeightAttribute, IsRequired = true)]
        public int Height { get { return base[HeightAttribute] != null ? Convert.ToInt32(base[HeightAttribute]) : 0; } set { base[HeightAttribute] = value; } }

        /// <summary>
        /// 是否最大范围裁剪图片（true：最大范围裁剪图片；false：等比例缩放图片）
        /// </summary>
        [ConfigurationProperty(IsCropAttribute, DefaultValue = false)]
        public bool IsCrop { get { return base[IsCropAttribute] != null ? Convert.ToBoolean(base[IsCropAttribute]) : false; } set { base[IsCropAttribute] = value; } }

        /// <summary>
        /// 是否默认 Size
        /// </summary>
        [ConfigurationProperty(IsDefaultAttribute, DefaultValue = false, IsDefaultCollection = true)]
        public bool IsDefault { get { return base[IsDefaultAttribute] != null ? Convert.ToBoolean(base[IsDefaultAttribute]) : false; } set { base[IsDefaultAttribute] = value; } }

        /// <summary>
        /// 图片生成的品质 （默认为-1，表示使用系统统一的品质压缩值）
        /// </summary>
        [ConfigurationProperty(qualityAttribute, DefaultValue = -1)]
        public int Quality { get { return base[qualityAttribute] != null ? Convert.ToInt32(base[qualityAttribute]) : -1; } set { base[qualityAttribute] = value; } }

        /// <summary>
        /// 该 Size 的图片存放相对根路径的路径,{0}表示图片存放的根路径，{1}表示图片文件名
        /// </summary>
        [ConfigurationProperty(PathAttribute, IsRequired = true)]
        public string Path { get { return base[PathAttribute] as string; } set { base[PathAttribute] = value; } }
    }
}