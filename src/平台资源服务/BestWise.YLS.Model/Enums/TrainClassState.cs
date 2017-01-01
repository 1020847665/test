using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model.Enums
{
    /// <summary>
    /// 培训班状态
    /// </summary>
    public enum TrainClassState
    {
        /// <summary>
        /// 报名中
        /// </summary>
        [Description("报名中")]
        Entering = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        [Description("进行中")]
        Ongoing = 1,

        /// <summary>
        /// 已报到 
        /// </summary>
        [Description("已报到")]
        Reported = 2,

        /// <summary>
        /// 已结束
        /// </summary>
        [Description("已结束")]
        Ended = 3,

        /// <summary>
        /// 已结业
        /// </summary>
        [Description("已结业")]
        Graduated = 4,


    }
}
