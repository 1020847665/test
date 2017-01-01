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
    /// 资料附件
    /// </summary>
    public class MaterialAttachment
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        [DisplayName("附件ID")]
        public Guid AttachmentId { get; set; }

        /// <summary>
        /// 资料ID
        /// </summary>
        [DisplayName("资料ID")]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        [DisplayName("附件名称")]
        public string Name{ get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        [DisplayName("附件地址")]
        public string AttachmentUrl { get; set; }

        /// <summary>
        /// 资料类型
        /// </summary>
        [DisplayName("资料类型")]
        public MaterialType Type { get; set; }

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
                if (!string.IsNullOrWhiteSpace(this.TypeText)) name = this.TypeText.ToEnum<MaterialType>().GetText();
                return name;
            }
        }
        #endregion
    }
}
