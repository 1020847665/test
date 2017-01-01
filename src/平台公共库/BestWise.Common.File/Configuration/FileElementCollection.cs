using System.Configuration;

namespace BestWise.Common.File.Configuration
{
    [ConfigurationCollection(typeof(FileElement), AddItemName = FileItemAttribute, CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class FileElementCollection : ConfigurationElementCollection
    {
        private const string FileItemAttribute = "file";

        /// <summary>
        /// 获取第一个文件节点
        /// </summary>
        /// <returns></returns>
        public FileElement FirstOrDefault()
        {
            return this[0];
        }

        /// <summary>
        /// 通过索引创建节点
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public FileElement this[int index]
        {
            get { return (FileElement)base.BaseGet(index); }
            set
            {
                if (base.BaseGet(index) != null)
                    base.BaseRemoveAt(index);
                base.BaseAdd(index, value);
            }
        }

        /// <summary>
        /// 通过名称获取节点
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public new FileElement this[string name]
        {
            get { return (FileElement)base.BaseGet(name.ToLower()); }
            set
            {
                if (BaseGet(name) != null)
                    base.BaseRemove(name.ToLower());
                base.BaseAdd(value);
            }
        }

        /// <summary>
        /// 创建新节点
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileElement();
        }

        /// <summary>
        /// 得到节点的键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileElement)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }
        protected override string ElementName
        {
            get { return FileItemAttribute; }
        }
    }
}
