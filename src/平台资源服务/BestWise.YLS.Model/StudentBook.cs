using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using BestWise.YLS.Model.Enums;
using BestWise.Common;

namespace BestWise.YLS.Model
{
    /// <summary>
    /// 学员预约
    /// </summary>
    public class StudentBook
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        [DisplayName("预约ID")]
        public Guid BookId { get; set; }

        /// <summary>
        /// 预约名称
        /// </summary>
        [DisplayName("预约名称")]
        public string BookName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DisplayName("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [DisplayName("单位")]
        public string Organization { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        public string Position { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 邮件
        /// </summary>
        [DisplayName("邮件")]
        public string Email { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DisplayName("开始时间")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DisplayName("结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        [DisplayName("处理状态")]
        public int DealState { get; set; }

        /// <summary>
        /// 培训单位
        /// </summary>
        [DisplayName("培训单位")]
        public string TrainOrganization { get; set; }

        /// <summary>
        /// 培训地址
        /// </summary>
        [DisplayName("培训地址")]
        public string TrainAddress { get; set; }

        /// <summary>
        /// 培训人数
        /// </summary>
        [DisplayName("培训人数")]
        public int TrainNumber { get; set; }

        /// <summary>
        /// 培训需求
        /// </summary>
        [DisplayName("培训需求")]
        public string TrainNeeds { get; set; }

        /// <summary>
        /// 预约类型(0-课程,1-教师,2-培训班)
        /// </summary>
        [DisplayName("培训类型")]
        public int Type { get; set; }

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
        /// 性别文本
        /// </summary>
        [DisplayName("性别文本")]
        public string SexText
        {
            get
            {
                return ((Gender)this.Sex).GetText();
            }
        }

        /// <summary>
        /// 状态处理文本
        /// </summary>
        [DisplayName("状态处理文本")]
        public string DealStateText{ get; set; }
        #endregion
    }
}
