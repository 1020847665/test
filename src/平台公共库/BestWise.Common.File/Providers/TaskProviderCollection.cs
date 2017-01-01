using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.Common.File.Providers
{
    public class TaskProviderCollection : ProviderCollection
    {
        /// <summary>
        /// 向集合中添加提供程序。
        /// </summary>
        /// <param name="provider">要添加的提供程序。</param>
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider 参数不能为 null");
            if (!(provider is BaseProvider))
                throw new ArgumentException("provider 参数类型必须是上传文件 BaseProvider.");
            base.Add(provider);
        }
    }
}
