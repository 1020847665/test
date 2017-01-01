using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BestWise.YLS.Model
{
    /// <summary>
    ///  课程信息
    /// </summary>
    public class Course
    {
        /// <summary>
        /// 课程ID
        /// </summary>
        [DisplayName("课程ID")]
        public Guid CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        [DisplayName("课程名称")]
        public string Name { get; set; }

        /// <summary>
        /// 课程介绍
        /// </summary>
        [DisplayName("课程介绍")]
        public string Introduce { get; set; }

        /// <summary>
        /// 课程内容
        /// </summary>
        [DisplayName("课程内容")]
        public string Body { get; set; }

        /// <summary>
        /// 课程Logo
        /// </summary>
        [DisplayName("课程Logo")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// 是否可预约
        /// </summary>
        [DisplayName("是否可预约")]
        public bool IsOrder { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Notes { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime Cdt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public Guid Cby { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTime Mdt { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DisplayName("修改人")]
        public Guid Mby { get; set; }
    }
}
