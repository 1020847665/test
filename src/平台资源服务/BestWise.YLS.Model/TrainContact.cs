using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Model
{
    /// <summary>
    /// 培训班联系人
    /// </summary>
    public class TrainContact
    {
        /// <summary> 
        /// 联系人ID 
        /// </summary> 
        [DisplayName("联系人ID")]
        public Guid ContactId { get; set; }

        /// <summary>
        /// 培训班ID
        /// </summary>
        [DisplayName("培训班ID")]
        public Guid TrainId{ get; set; }

        /// <summary> 
        /// 联系人 
        /// </summary> 
        [DisplayName("联系人")]
        public string Name { get; set; }

        /// <summary> 
        /// 联系电话 
        /// </summary> 
        [DisplayName("联系电话")]
        public string ContactNumber { get; set; }

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

        #endregion

    }
}
