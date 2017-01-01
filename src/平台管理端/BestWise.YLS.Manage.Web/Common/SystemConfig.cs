using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BestWise.YLS.Manage.Web
{
    /// <summary>
    /// 系统参数配置
    /// xml位置：~/bin/SysConfig.xml
    /// </summary>
    [Serializable]
    public class SystemConfig
    {
        /// <summary>
        /// 设置默认配置
        /// </summary>
        public SystemConfig()
        {
            this.LoginTitle = "欢迎使用 后台管理程序";
            this.MainTitle = "console – 后台管理程序";
            this.LoginTopWords = "欢迎登陆 console-后台管理程序";
            this.Company = "微团校管理平台";
            this.ICP = "微团校管理平台 ";
            this.FileDirectory = "/File";
            this.IndexUrl = "/Layout/Main";
            this.DefaultUrl = "/Home/Default";
        }


        #region 系统参数

        /// <summary>
        /// 登录页标题
        /// </summary>
        [Description("登录页标题")]
        [ConfigGroup(BelongsGroup.系统参数, true, InputType.文本)]
        public string LoginTitle { get; set; }

        /// <summary>
        /// 主页标题
        /// </summary>
        [Description("主页标题")]
        [ConfigGroup(BelongsGroup.系统参数, true, InputType.文本)]
        public string MainTitle { get; set; }

        /// <summary>
        /// 登录页顶部提示语
        /// </summary>
        [Description("登录页顶部提示语")]
        [ConfigGroup(BelongsGroup.系统参数, true, InputType.文本)]
        public string LoginTopWords { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [Description("公司名称")]
        [ConfigGroup(BelongsGroup.系统参数, true, InputType.文本)]
        public string Company { get; set; }

        /// <summary>
        /// 备案号
        /// </summary>
        [Description("备案号")]
        [ConfigGroup(BelongsGroup.系统参数, true, InputType.文本)]
        public string ICP { get; set; }

        /// <summary>
        /// 上传文件根目录
        /// </summary>
        [Description("上传文件根目录")]
        [ConfigGroup(BelongsGroup.系统参数, true, InputType.文本)]
        public string FileDirectory { get; set; }

        #endregion

        #region 链接参数
        /// <summary>
        /// 系统首页
        /// </summary>
        [Description("系统首页")]
        [ConfigGroup(BelongsGroup.链接地址, true, InputType.文本)]
        public string IndexUrl { get; set; }

        /// <summary>
        /// 默认主页
        /// </summary>
        [Description("默认主页")]
        [ConfigGroup(BelongsGroup.链接地址, true, InputType.文本)]
        public string DefaultUrl { get; set; }

        #endregion

    }
}