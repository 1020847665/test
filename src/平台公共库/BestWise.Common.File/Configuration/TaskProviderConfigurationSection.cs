using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.Common.File.Configuration
{
    public class TaskProviderConfigurationSection : ConfigurationSection
    {
        private readonly ConfigurationProperty _providers;
        private ConfigurationPropertyCollection _properties;
        private const string ProvidersPropertyName = "providers";

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaskProviderConfigurationSection()
        {
            _providers = new ConfigurationProperty(ProvidersPropertyName, typeof(ProviderSettingsCollection), null);
            _properties = new ConfigurationPropertyCollection();
            _properties.Add(_providers);
        }

        /// <summary>
        /// 推送数据的所有的 Provider 集合
        /// </summary>
        [ConfigurationProperty(ProvidersPropertyName)]
        [StringValidator(MinLength = 1)]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base[_providers]; }
        }

        /// <summary>
        ///  推送数据的Provider的属性集合
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get { return _properties; }
        }
    }
}
