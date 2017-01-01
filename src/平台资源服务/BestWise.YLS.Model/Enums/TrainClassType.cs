using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BestWise.YLS.Model.Enums
{
    /// <summary>
    /// 培训班类型
    /// </summary>
    public enum TrainClassType
    {
        /// <summary>
        /// 调训班
        /// </summary>
        [Description("调训班")]
        Tame = 1,

        /// <summary>
        /// 培训班
        /// </summary>
        [Description("培训班")]
        Train = 2
    }
}
