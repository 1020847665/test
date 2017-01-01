using BestWise.Common.File.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BestWise.Common.File.Providers
{
    public class ProviderManager
    {
        private static bool m_isInitialized = false;
        private static TaskProviderCollection _providers = null;
        public static TaskProviderCollection GetProviders()
        {
            try
            {
                TaskProviderConfigurationSection config = null;
                if ((!m_isInitialized) || _providers == null || _providers.Count == 0)
                {

                    // 找到配置文件中“MessageProvider”节点
                    config = (TaskProviderConfigurationSection)ConfigurationManager.GetSection("FileProvider");

                    if (config == null)
                        throw new ConfigurationErrorsException("在配置文件中没找到“FileProvider”节点");

                    _providers = new TaskProviderCollection();

                    // 使用System.Web.Configuration.ProvidersHelper类调用每个Provider的Initialize()方法
                    ProvidersHelper.InstantiateProviders(config.Providers, _providers, typeof(BaseProvider));
                    m_isInitialized = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _providers;
        }
    }
}
