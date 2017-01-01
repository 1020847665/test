using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model.Enums
{
    /// <summary>
    /// 资料类型
    /// </summary>
    public enum MaterialType
    {
        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        Video = 0,

        /// <summary>
        /// 音频
        /// </summary>
        [Description("音频")]
        Audio = 1,

        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other=2
    }
}
