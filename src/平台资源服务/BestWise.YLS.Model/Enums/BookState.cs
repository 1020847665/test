using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model.Enums
{
    /// <summary>
    /// 预约状态
    /// </summary>
    public enum BookState
    {
        /// <summary>
        /// 接受
        /// </summary>
        [Description("接受")]
        Receive = 1,

        /// <summary>
        /// 已拒绝
        /// </summary>
        [Description("拒绝")]
        Refuse = 2
    }
}
