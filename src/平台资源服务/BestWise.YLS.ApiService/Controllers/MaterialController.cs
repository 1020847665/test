using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.YLS.FrameWork.BLL;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BestWise.YLS.ApiService.Controllers
{
    /// <summary>
    /// 资料相关接口
    /// </summary>
    [RoutePrefix("api/Material")]
    [BWAuthorize]
    public class MaterialController : ApiController
    {
        #region 新增资料信息
        /// <summary>
        /// 新增资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回新增结果</returns>
        [HttpPost]
        [Route("Add")]
        public ResultMessage Add(Material model)
        {
            return BLLMaterial.Instantiate.Add(model);
        }
        #endregion

        #region 分页获取资料信息
        /// <summary>
        /// 分页获取资料信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>资料列表集合</returns>
        [HttpPost]
        [Route("GetPageList")]
        public ResultMessage<PagedList<Material>> GetPageList(BaseFilter baseFilter)
        {
            return BLLMaterial.Instantiate.GetPageList(baseFilter);
        }
        #endregion

        #region 更新资料信息
        /// <summary>
        /// 更新资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("Update")]
        public ResultMessage Update(Material model)
        {
            return BLLMaterial.Instantiate.Update(model);
        }
        #endregion

        #region 根据资料ID获取资料信息
        /// <summary>
        /// 根据资料ID获取资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>实体对象</returns>
        [HttpGet]
        [Route("GetModel")]
        public ResultMessage<Material> GetModel(Guid materialId)
        {
            return BLLMaterial.Instantiate.GetModel(materialId);
        }
        #endregion

        #region 删除资料信息
        /// <summary>
        /// 删除资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("Delete")]
        public ResultMessage Delete(Guid materialId)
        {
            return BLLMaterial.Instantiate.Delete(materialId);
        }
        #endregion

        #region 获取资料附件
        /// <summary>
        /// 获取资料附件
        /// </summary>
        /// <param name="materialId">附件ID</param>
        /// <returns>返回资料附件</returns>
        [HttpGet]
        [Route("GetAttachmentList")]
        public ResultMessage<List<MaterialAttachment>> GetAttachmentList(Guid materialId)
        {
            return BLLMaterial.Instantiate.GetAttachmentList(materialId);
        }
        #endregion

        #region 删除资料附件信息
        /// <summary>
        /// 删除资料附件信息
        /// </summary>
        /// <param name="attachmentId">资料附件ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("DeleteAttachment")]
        public ResultMessage DeleteAttachment(Guid attachmentId)
        {
            return BLLMaterialAttachment.Instantiate.Delete(attachmentId);
        }
        #endregion

        #region 根据附件ID获取附件信息
        /// <summary>
        /// 根据附件ID获取附件信息
        /// </summary>
        /// <param name="attachmentId">附件ID</param>
        /// <returns>实体对象</returns>
        [HttpGet]
        [Route("GetAttachmentModel")]
        public ResultMessage<MaterialAttachment> GetAttachmentModel(Guid attachmentId)
        {
            return BLLMaterialAttachment.Instantiate.GetModel(attachmentId);
        }
        #endregion
    }
}
