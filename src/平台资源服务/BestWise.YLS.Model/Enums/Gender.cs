using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BestWise.YLS.Model.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 0,

        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        Female = 1
    }
}
