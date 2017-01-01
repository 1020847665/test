using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BestWise.YLS.Model
{
    /// <summary>
    /// 资料信息
    /// </summary>
    public class Material
    {
        /// <summary>
        /// 资料ID
        /// </summary>
        [DisplayName("资料ID")]
        public Guid MaterialId{ get; set; }

        /// <summary>
        /// 资料名称
        /// </summary>
        [DisplayName("资料名称")]
        public string Name { get; set; }

        /// <summary>
        /// 资料介绍
        /// </summary>
        [DisplayName("资料介绍")]
        public string Introduce{ get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Notes{ get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set;}

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
        /// 资料附件
        /// </summary>
        [DisplayName("资料附件")]
        public string Attachment{ get; set; }

        /// <summary>
        /// 附件总数
        /// </summary>
        [DisplayName("附件总数")]
        public int AttachmentCount{ get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        [DisplayName("附件列表")]
        public List<MaterialAttachment> AttachmentList{ get; set; }

        #endregion
    }
}
