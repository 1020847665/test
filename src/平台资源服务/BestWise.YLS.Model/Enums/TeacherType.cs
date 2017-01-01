using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model.Enums
{
    /// <summary>
    /// 教师类型
    /// </summary>
    public enum TeacherType
    {
        /// <summary>
        /// 团校
        /// </summary>
        [Description("团校")]
        YouthLeague = 0,

        /// <summary>
        /// 特聘
        /// </summary>
        [Description("特聘")]
        Resident = 1,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 2
    }
}
