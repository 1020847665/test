using System.Configuration;

namespace BestWise.Common.File.Configuration
{
  
    [ConfigurationCollection(typeof(ImageElement), AddItemName = ImageItemAttribute, CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class ImageElementCollection : ConfigurationElementCollection
    {
        private const string ImageItemAttribute = "image";

        /// <summary>
        /// 获取第一个图片节点
        /// </summary>
        /// <returns></returns>
        public ImageElement FirstOrDefault()
        {
            return this[0];
        }

        /// <summary>
        /// 通过索引创建节点
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public ImageElement this[int index]
        {
            get { return (ImageElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);
                base.BaseAdd(index, value);
            }
        }

        /// <summary>
        /// 通过类型获取节点
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public new ImageElement this[string type]
        {
            get { return (ImageElement)base.BaseGet(type.ToLower()); }
            set
            {
                if (BaseGet(type) != null)
                    base.BaseRemove(type.ToLower());
                base.BaseAdd(value);
            }
        }

        /// <summary>
        /// 创建新节点
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ImageElement();
        }

        /// <summary>
        /// 得到节点的键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ImageElement)element).Type;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
        protected override string ElementName
        {
            get { return ImageItemAttribute; }
        }
    }
}