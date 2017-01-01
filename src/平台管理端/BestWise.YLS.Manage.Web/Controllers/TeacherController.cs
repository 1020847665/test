using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.YLS.Manage.FrameWork;
using BestWise.YLS.Manage.Web;
using BestWise.YLS.Model;
using BestWise.YLS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BWC.Galaxy.UMP.Manage.Web.Controllers
{
    public class TeacherController : BasicController
    {
        #region 教师信息页
        /// <summary>
        /// 教师信息页
        /// </summary>
        /// <returns>返回教师视图页</returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 教师图片
        /// <summary>
        /// 教师图片
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回教师图片视图页</returns>
        public ActionResult Picture(Guid teacherId)
        {
            ViewBag.TeacherId = teacherId;
            return View();
        }
        #endregion

        #region 获取教师信息列表
        /// <summary>
        /// 获取教师信息列表
        /// </summary>
        /// <returns>返回教师信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetPagedList()
        {
            BaseFilter baseFilter = GetPageFiler(GetQueryCondition());
            ResultMessage<PagedList<Teacher>> result = await BLLTeacher.Instantiate.GetPageList(baseFilter);
            return SerializePageList<Teacher>(result);
        }
        #endregion

        #region 获取教师图片列表
        /// <summary>
        /// 获取教师图片列表
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回教师图片信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetPictureList(Guid teacherId)
        {
            ResultMessage<List<TeacherPicture>> result = await BLLTeacher.Instantiate.GetPictureList(teacherId);
            return SerializeList<TeacherPicture>(result);
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

        #region 教师编辑页
        /// <summary>
        /// 教师编辑页
        /// </summary>
        /// <returns>返回教师编辑页</returns>
        public async Task<ActionResult> Edit(Teacher model)
        {
            if (model.TeacherId != Guid.Empty)
            {
                ResultMessage<Teacher> result = await BLLTeacher.Instantiate.GetModel(model.TeacherId);
                if (result.IsSucceed() && result.Data != null) model = result.Data;
            }
            ViewBag.Type = new SelectList(SelectListExtensions.GetList<TeacherType>(), "Value", "Text", (int)TeacherType.YouthLeague);//教师类型
            ViewBag.TeacherId = model.TeacherId;
            return PartialView("_PartialEdit", model);
        }
        #endregion

        #region 保存教师信息
        /// <summary>
        /// 保存教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回保存结果</returns>
        [ValidateInput(false)]
        public async Task<ActionResult> SaveTeacher(Teacher model,List<TeacherPicture> list)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                model.Pictures = list;
                if (model.TeacherId != Guid.Empty) result = await BLLTeacher.Instantiate.Update(model);
                else result = await BLLTeacher.Instantiate.Add(model);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除教师信息
        /// <summary>
        /// 删除教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid teacherId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLTeacher.Instantiate.Delete(teacherId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除教师图片
        /// <summary>
        /// 删除教师图片
        /// </summary>
        /// <param name="pictureId">图片ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteAttachment(Guid pictureId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLTeacher.Instantiate.DeletePicture(pictureId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion
    }
}