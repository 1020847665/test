using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace BestWise.Common.File.Configuration
{
    public class FileConfigSection:ConfigurationSection
    {
        private const string SectionPath = "fileConfig";

        /// <summary>
        /// 文件节点配置
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public FileElementCollection Files { get { return (FileElementCollection)base[""]; } }

        #region Methods
        /// <summary>
        /// Gets an instance of <see cref="FileConfigSection"/> that represents the current configuration.
        /// </summary>
        /// <returns>An instance of <see cref="FileConfigSection"/>, or null if no configuration is specified.</returns>
        public static FileConfigSection GetConfiguration()
        {
            try
            {
                return ConfigurationManager.GetSection(SectionPath) as FileConfigSection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
