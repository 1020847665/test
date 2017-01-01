using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model
{
    /// <summary>
    /// 教师图片
    /// </summary>
    public class TeacherPicture
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        [DisplayName("图片ID")]
        public Guid PictureId { get; set; }

        /// <summary>
        /// 教师ID
        /// </summary>
        [DisplayName("教师ID")]
        public Guid TeacherId { get; set; }

        /// <summary>
        /// 图片URL
        /// </summary>
        [DisplayName("图片URL")]
        public string Url { get; set; }

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
