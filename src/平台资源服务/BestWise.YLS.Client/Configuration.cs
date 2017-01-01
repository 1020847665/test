using BestWise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Client
{
    /// <summary>
    /// 资源服务配置
    /// </summary>
    public class Configuration
    {
        public static readonly string SourceServerAddress = ConfigHelper.GetConfigString("ServiceCenter");
    }
}
