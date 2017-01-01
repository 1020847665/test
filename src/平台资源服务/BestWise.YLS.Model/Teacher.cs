using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BestWise.YLS.Model.Enums;
using BestWise.Common;

namespace BestWise.YLS.Model
{
    /// <summary>
    /// 教师信息
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// 教师ID
        /// </summary>
        [DisplayName("教师ID")]
        public Guid TeacherId { get; set; }

        /// <summary>
        /// 教师名称
        /// </summary>
        [DisplayName("教师名称")]
        public string Name { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [DisplayName("介绍")]
        public string Introduce { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        public string Organization { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        public string Position { get; set; }

        /// <summary>
        /// 主讲课程
        /// </summary>
        [DisplayName("主讲课程")]
        public string Course { get; set; }

        /// <summary>
        /// 教师类型
        /// </summary>
        [DisplayName("教师类型")]
        public TeacherType Type { get; set; }

        /// <summary>
        /// 是否可以预约
        /// </summary>
        [DisplayName("是否可以预约")]
        public bool IsOrder{ get; set; }

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

        #region 扩展属性
        /// <summary>
        /// 教师图片
        /// </summary>
        [DisplayName("教师图片")]
        public List<TeacherPicture> Pictures { get; set; }

        /// <summary>
        /// 类型文本
        /// </summary>
        [DisplayName("类型文本")]
        public string TypeText { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [DisplayName("类型名称")]
        public string TypeName
        {
            get
            {
                string name = string.Empty;
                if (!string.IsNullOrWhiteSpace(this.TypeText)) name = this.TypeText.ToEnum<TeacherType>().GetText();
                return name;
            }
        }
        #endregion
    }
}
