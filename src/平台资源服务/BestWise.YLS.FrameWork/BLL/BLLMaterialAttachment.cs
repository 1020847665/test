using BestWise.Common;
using BestWise.YLS.FrameWork.DAL;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.FrameWork.BLL
{
    /// <summary>
    /// 资料附件业务逻辑层
    /// </summary>
    public class BLLMaterialAttachment
    {
        private static readonly DALMaterialAttachment _dal = new DALMaterialAttachment();
        private static BLLMaterialAttachment _instantiate = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public static BLLMaterialAttachment Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLMaterialAttachment()); }
        }

        #region 删除资料附件信息
        /// <summary>
        /// 删除资料附件信息
        /// </summary>
        /// <param name="attachmentId">资料附件ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid attachmentId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(attachmentId)) result = ResultMessage.SucceedResult("操作成功！");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 根据附件ID获取附件信息
        /// <summary>
        /// 根据附件ID获取附件信息
        /// </summary>
        /// <param name="attachmentId">附件ID</param>
        /// <returns>实体对象</returns>
        public ResultMessage<MaterialAttachment> GetModel(Guid attachmentId)
        {
            ResultMessage<MaterialAttachment> result = ResultMessage<MaterialAttachment>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<MaterialAttachment>.SucceedResult(_dal.GetModel(attachmentId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<MaterialAttachment>.FailureResult(ex);
            }
            return result;
        }
        #endregion
    }
}
