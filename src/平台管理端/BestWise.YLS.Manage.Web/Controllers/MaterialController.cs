using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.YLS.Manage.FrameWork;
using BestWise.YLS.Manage.Web;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BWC.Galaxy.UMP.Manage.Web.Controllers
{
    public class MaterialController : BasicController
    {
        #region 资料页
        /// <summary>
        /// 资料页
        /// </summary>
        /// <returns>返回资料视图页</returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 资料附件页
        /// <summary>
        /// 资料附件页
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>返回资料附件视图页</returns>
        public ActionResult Attachment(Guid materialId)
        {
            ViewBag.MaterialId = materialId;
            return View();
        }
        #endregion

        #region 获取资料列表
        /// <summary>
        /// 获取资料列表
        /// </summary>
        /// <returns>返回资料信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetPagedList()
        {
            BaseFilter baseFilter = GetPageFiler(GetQueryCondition());
            ResultMessage<PagedList<Material>> result = await BLLMaterial.Instantiate.GetPageList(baseFilter);
            return SerializePageList<Material>(result);
        }
        #endregion

        #region 获取资料附件列表
        /// <summary>
        /// 获取资料附件列表
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>返回资料附件信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetAttachmentList(Guid materialId)
        {
            ResultMessage<List<MaterialAttachment>> result = await BLLMaterial.Instantiate.GetAttachmentList(materialId);
            return SerializeList<MaterialAttachment>(result);
        }
        #endregion

        #region 构建列表查询条件
        /// <summary>
        /// 构建列表查询条件
        /// </summary>
        /// <returns>返回列表查询条件</returns>
        private Dictionary<string, SearchInfo> GetQueryCondition()
        {
            Dictionary<string, SearchInfo> queryDictionary = new Dictionary<string, SearchInfo>();
            queryDictionary.Add("searchKey", new SearchInfo() { FieldName = "Name", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            return queryDictionary;
        }
        #endregion

        #region 资料编辑页
        /// <summary>
        /// 资料编辑页
        /// </summary>
        /// <returns>返回资料编辑页</returns>
        public async Task<ActionResult> Edit(Material model)
        {
            if (model.MaterialId != Guid.Empty)
            {
                ResultMessage<Material> result = await BLLMaterial.Instantiate.GetModel(model.MaterialId);
                if (result.IsSucceed() && result.Data != null) model = result.Data;
            }
            return PartialView("_PartialEdit", model);
        }
        #endregion

        #region 保存资料信息
        /// <summary>
        /// 保存资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回保存结果</returns>
        [ValidateInput(false)]
        public async Task<ActionResult> SaveMaterial(Material model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                if (model.MaterialId != Guid.Empty) result = await BLLMaterial.Instantiate.Update(model);
                else result = await BLLMaterial.Instantiate.Add(model);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除资料
        /// <summary>
        /// 删除资料
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid materialId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLMaterial.Instantiate.Delete(materialId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除资料附件
        /// <summary>
        /// 删除资料附件
        /// </summary>
        /// <param name="attachmentId">资料附件ID</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteAttachment(Guid attachmentId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLMaterial.Instantiate.DeleteAttachment(attachmentId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 资料附件预览页
        /// <summary>
        /// 资料附件预览页
        /// </summary>
        /// <param name="attachmentId">附件Id</param>
        /// <returns>返回资料附件预览视图页</returns>
        public async Task<ActionResult> Preview(Guid attachmentId)
        {
            ResultMessage<MaterialAttachment> result = await BLLMaterial.Instantiate.GetAttachmentModel(attachmentId);
            if (result != null && result.IsSucceed() && result.Data != null)
            {
                MaterialAttachment model = result.Data;
                ViewBag.AttachmentUrl = string.Concat(ConfigHelper.GetConfigString("AttachmentUrl"), SwfHelper.GetSwfFilePath(string.Concat(@"/", model.AttachmentUrl)));
            }
            return View();
        }
        #endregion

        #region 资料多媒体附件预览页
        /// <summary>
        /// 资料多媒体附件预览页
        /// </summary>
        /// <param name="attachmentId">附件Id</param>
        /// <returns>返回资料多媒体附件预览视图页</returns>
        public async Task<ActionResult> MediaPreview(Guid attachmentId)
        {
            ResultMessage<MaterialAttachment> result = await BLLMaterial.Instantiate.GetAttachmentModel(attachmentId);
            if (result != null && result.IsSucceed() && result.Data != null)
            {
                MaterialAttachment model = result.Data;
                ViewBag.AttachmentUrl = string.Concat(ConfigHelper.GetConfigString("AttachmentUrl"), model.AttachmentUrl);
            }
            return View("_PartialMediaPreview");
        }
        #endregion

    }
}