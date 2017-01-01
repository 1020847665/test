using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model.Enums
{

    /// <summary>
    /// 预约类型
    /// </summary>
    public enum BookType
    {
        /// <summary>
        /// 课程
        /// </summary>
        [Description("课程")]
        Course = 1,

        /// <summary>
        /// 教师
        /// </summary>
        [Description("教师")]
        Teacher = 2,

        /// <summary>
        /// 培训班
        /// </summary>
        [Description("培训班")]
        Train = 3
    }
}
