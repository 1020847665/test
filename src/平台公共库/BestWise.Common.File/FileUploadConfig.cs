using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.Common.File
{
    /// <summary>
    /// 文件上传配置
    /// </summary>
    public static class FileUploadConfig
    {
        /// <summary>
        /// 判断是否应用 UpYun
        /// </summary>
        /// <returns>是否应用 UpYun</returns>
        public static bool IsUpYun()
        {
            return (!String.IsNullOrWhiteSpace(ConfigHelper.GetConfigString("upyun_bucketname"))) && (!String.IsNullOrWhiteSpace(ConfigHelper.GetConfigString("upyun_username"))) && (!String.IsNullOrWhiteSpace(ConfigHelper.GetConfigString("upyun_password")));
        }

        /// <summary>
        /// 判断是否应用 Remoting
        /// </summary>
        /// <returns>是否应用 Remoting</returns>
        public static bool IsRemoting()
        {
            return (!String.IsNullOrWhiteSpace(ConfigHelper.GetConfigString("RemotingIp"))) && (!String.IsNullOrWhiteSpace(ConfigHelper.GetConfigString("RemotingRoot")));
        }
    }
}
