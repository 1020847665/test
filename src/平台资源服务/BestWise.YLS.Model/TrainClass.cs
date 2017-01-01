using BestWise.YLS.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestWise.Common;

namespace BestWise.YLS.Model
{
    /// <summary>
    /// 培训班信息
    /// </summary>
    public class TrainClass
    {
        /// <summary> 
        /// 培训ID 
        /// </summary> 
        [DisplayName("培训ID")]
        public Guid TrainId { get; set; }

        /// <summary> 
        /// 培训班名称 
        /// </summary> 
        [DisplayName("培训班名称")]
        public string Name { get; set; }

        /// <summary> 
        /// 介绍 
        /// </summary> 
        [DisplayName("介绍")]
        public string Introduce { get; set; }

        /// <summary> 
        /// 内容 
        /// </summary> 
        [DisplayName("内容")]
        public string Body { get; set; }

        /// <summary> 
        /// 培训要求 
        /// </summary> 
        [DisplayName("培训要求")]
        public string TrainNeeds { get; set; }

        /// <summary> 
        /// LogoUrl 
        /// </summary> 
        [DisplayName("LogoUrl")]
        public string LogoUrl { get; set; }

        /// <summary> 
        /// 培训对象 
        /// </summary> 
        [DisplayName("培训对象")]
        public string Target { get; set; }

        /// <summary> 
        /// 培训班人数 
        /// </summary> 
        [DisplayName("培训班人数")]
        public int Number { get; set; }

        /// <summary> 
        /// 培训地址 
        /// </summary> 
        [DisplayName("培训地址")]
        public string Address { get; set; }

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
        /// 截止时间 
        /// </summary> 
        [DisplayName("截止时间")]
        public DateTime DeadLineTime { get; set; }

        /// <summary> 
        /// 学号前缀 
        /// </summary> 
        [DisplayName("学号前缀")]
        public string NumberPrefix { get; set; }

        /// <summary> 
        /// 培训班状态 
        /// </summary> 
        [DisplayName("培训班状态")]
        public int State { get; set; }

        /// <summary> 
        /// 培训班类型 
        /// </summary> 
        [DisplayName("培训班类型")]
        public int Type { get; set; }

        /// <summary> 
        /// 培训老师 
        /// </summary> 
        [DisplayName("培训老师")]
        public string Teachers { get; set; }

        /// <summary> 
        /// 是否可以被预约 
        /// </summary> 
        [DisplayName("是否可以被预约")]
        public bool IsOrder { get; set; }

        /// <summary> 
        /// 二维码地址 
        /// </summary> 
        [DisplayName("二维码地址")]
        public string CodeUrl { get; set; }

        /// <summary> 
        /// 报名链接 
        /// </summary> 
        [DisplayName("报名链接")]
        public string EnterURL { get; set; }

        /// <summary> 
        /// 实际报名人数 
        /// </summary> 
        [DisplayName("实际报名人数")]
        public int ActualEnterNumber { get; set; }

        /// <summary> 
        /// 实际报到人数 
        /// </summary> 
        [DisplayName("实际报到人数")]
        public int ActualReportNumber { get; set; }

        /// <summary> 
        /// 是否停止报名 
        /// </summary> 
        [DisplayName("是否停止报名")]
        public bool IsStop { get; set; }

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
        /// 创建时间 
        /// </summary> 
        [DisplayName("创建时间")]
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
        /// 培训班联系人
        /// </summary>
        [DisplayName("培训班联系人")]
        public List<TrainContact> Contacts { get; set; }

        /// <summary>
        /// 类别文本
        /// </summary>
        [DisplayName("类别文本")]
        public string TypeText
        {
            get
            {
                return ((TrainClassType)this.Type).GetText();
            }
        }

        /// <summary>
        /// 状态文本
        /// </summary>
        [DisplayName("状态文本")]
        public string StateText
        {
            get
            {
                return ((TrainClassState)this.State).GetText();
            }
        }
        #endregion

    }
}
