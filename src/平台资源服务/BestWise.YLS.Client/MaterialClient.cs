using BestWise.Common;
using BestWise.Common.Client;
using BestWise.Common.Mvc;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Client
{
    /// <summary>
    /// 资料Client
    /// </summary>
    public class MaterialClient
    {
        #region 新增资料信息
        /// <summary>
        /// 新增资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回新增结果</returns>
        public static async Task<ResultMessage> Add(Material model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Material/Add"), HttpMethod.Post, model);
        }
        #endregion

        #region 分页获取资料信息
        /// <summary>
        /// 分页获取资料信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>返回资料列表信息</returns>
        public static async Task<ResultMessage<PagedList<Material>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<Material>>(string.Concat(Configuration.SourceServerAddress, "api/Material/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 根据资料ID获取资料信息
        /// <summary>
        /// 根据资料ID获取资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>实体对象</returns>
        public static async Task<ResultMessage<Material>> GetModel(Guid materialId)
        {
            return await HttpClientHelper.RequestAsync<Material>(string.Concat(Configuration.SourceServerAddress, "api/Material/GetModel"), HttpMethod.Get, new { materialId = materialId });
        }
        #endregion

        #region 更新资料信息
        /// <summary>
        /// 更新资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回修改结果</returns>
        public static async Task<ResultMessage> Update(Material model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Material/Update"), HttpMethod.Post, model);
        }
        #endregion

        #region 删除资料信息
        /// <summary>
        /// 删除资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> Delete(Guid materialId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Material/Delete"), HttpMethod.Post, new { materialId = materialId });
        }
        #endregion

        #region 获取资料附件
        /// <summary>
        /// 获取资料附件
        /// </summary>
        /// <param name="materialId">附件ID</param>
        /// <returns>返回资料附件</returns>
        public static async Task<ResultMessage<List<MaterialAttachment>>> GetAttachmentList(Guid materialId)
        {
            return await HttpClientHelper.RequestAsync<List<MaterialAttachment>>(string.Concat(Configuration.SourceServerAddress, "api/Material/GetAttachmentList"), HttpMethod.Get, new { materialId = materialId });
        }
        #endregion

        #region 删除资料附件信息
        /// <summary>
        /// 删除资料附件信息
        /// </summary>
        /// <param name="attachmentId">资料附件ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> DeleteAttachment(Guid attachmentId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Material/DeleteAttachment"), HttpMethod.Post, new { attachmentId = attachmentId });
        }
        #endregion

        #region 根据附件ID获取附件信息
        /// <summary>
        /// 根据附件ID获取附件信息
        /// </summary>
        /// <param name="attachmentId">附件ID</param>
        /// <returns>实体对象</returns>
        public static async Task<ResultMessage<MaterialAttachment>> GetAttachmentModel(Guid attachmentId)
        {
            return await HttpClientHelper.RequestAsync<MaterialAttachment>(string.Concat(Configuration.SourceServerAddress, "api/Material/GetAttachmentModel"), HttpMethod.Get, new { attachmentId = attachmentId });
        }
        #endregion
    }
}
