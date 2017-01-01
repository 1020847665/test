using System.Configuration;

namespace BestWise.Common.File.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationCollection(typeof(ImageElement), AddItemName = SizeItemAttribute, CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class SizeElementCollection : ConfigurationElementCollection
    {
        private const string SizeItemAttribute = "size";

        /// <summary>
        /// 获取第一个Size
        /// </summary>
        /// <returns></returns>
        public SizeElement FirstOrDefault()
        {
            return this[0];
        }

        /// <summary>
        /// 通过索引获取Size
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public SizeElement this[int index]
        {
            get { return (SizeElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);
                base.BaseAdd(index, value);
            }
        }

        /// <summary>
        /// 通过名称获取Size
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public new SizeElement this[string name]
        {
            get { return (SizeElement)base.BaseGet(name); }
            set
            {
                if (BaseGet(name) != null)
                    base.BaseRemove(name);
                base.BaseAdd(value);
            }
        }

        /// <summary>
        /// 创建新节点
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new SizeElement();
        }

        /// <summary>
        /// 得到节点的键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SizeElement)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
        protected override string ElementName
        {
            get { return SizeItemAttribute; }
        }
    }
}