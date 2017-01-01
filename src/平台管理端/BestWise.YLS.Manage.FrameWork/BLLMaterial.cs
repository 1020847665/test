using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.YLS.Client;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Manage.FrameWork
{
    /// <summary>
    /// 资料业务逻辑层
    /// </summary>
    public class BLLMaterial
    {
        private static BLLMaterial _instantiate = null;
        /// <summary>
        /// 实例对象
        /// </summary>
        public static BLLMaterial Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLMaterial()); }
        }

        #region 新增资料信息
        /// <summary>
        /// 新增资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回新增结果</returns>
        public async Task<ResultMessage> Add(Material model)
        {
            return await MaterialClient.Add(model);
        }
        #endregion

        #region 删除资料信息
        /// <summary>
        /// 删除资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> Delete(Guid materialId)
        {
            return await MaterialClient.Delete(materialId);
        }
        #endregion

        #region 更新资料信息
        /// <summary>
        /// 更新资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回修改结果</returns>
        public async Task<ResultMessage> Update(Material model)
        {
            return await MaterialClient.Update(model);
        }
        #endregion

        #region 分页获取资料信息
        /// <summary>
        /// 分页获取资料信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>返回资料列表信息</returns>
        public async Task<ResultMessage<PagedList<Material>>> GetPageList(BaseFilter baseFilter)
        {
            return await MaterialClient.GetPageList(baseFilter);
        }
        #endregion

        #region 根据资料ID获取资料信息
        /// <summary>
        /// 根据资料ID获取资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>实体对象</returns>
        public async Task<ResultMessage<Material>> GetModel(Guid materialId)
        {
            return await MaterialClient.GetModel(materialId);
        }
        #endregion

        #region 获取资料附件
        /// <summary>
        /// 获取资料附件
        /// </summary>
        /// <param name="materialId">附件ID</param>
        /// <returns>返回资料附件</returns>
        public async Task<ResultMessage<List<MaterialAttachment>>> GetAttachmentList(Guid materialId)
        {
            return await MaterialClient.GetAttachmentList(materialId);
        }
        #endregion

        #region 删除资料附件信息
        /// <summary>
        /// 删除资料附件信息
        /// </summary>
        /// <param name="attachmentId">资料ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> DeleteAttachment(Guid attachmentId)
        {
            return await MaterialClient.DeleteAttachment(attachmentId);
        }
        #endregion

        #region 根据附件ID获取附件信息
        /// <summary>
        /// 根据附件ID获取附件信息
        /// </summary>
        /// <param name="attachmentId">附件ID</param>
        /// <returns>实体对象</returns>
        public async Task<ResultMessage<MaterialAttachment>> GetAttachmentModel(Guid attachmentId)
        {
            return await MaterialClient.GetAttachmentModel(attachmentId);
        }
        #endregion
    }
}
