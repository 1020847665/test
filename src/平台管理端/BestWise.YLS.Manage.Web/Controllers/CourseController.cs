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
    public class CourseController : BasicController
    {
        #region 课程页
        /// <summary>
        /// 课程页
        /// </summary>
        /// <returns>返回课程视图页</returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取课程信息列表
        /// <summary>
        /// 获取课程信息列表
        /// </summary>
        /// <returns>返回课程信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetPagedList()
        {
            BaseFilter baseFilter = GetPageFiler(GetQueryCondition());
            ResultMessage<PagedList<Course>> result = await BLLCourse.Instantiate.GetPageList(baseFilter);
            return SerializePageList<Course>(result);
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

        #region 课程编辑页
        /// <summary>
        /// 课程编辑页
        /// </summary>
        /// <returns>返回课程编辑页</returns>
        public async Task<ActionResult> Edit(Course model)
        {
            if (model.CourseId != Guid.Empty)
            {
                ResultMessage<Course> result = await BLLCourse.Instantiate.GetModel(model.CourseId);
                if (result.IsSucceed() && result.Data != null) model = result.Data;
            }
            ViewBag.CourseId = model.CourseId;
            return PartialView("_PartialEdit", model);
        }
        #endregion

        #region 保存课程信息
        /// <summary>
        /// 保存课程信息
        /// </summary>
        /// <param name="model">保存课程信息</param>
        /// <returns>返回保存结果</returns>
        public async Task<ActionResult> Save(Course model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                if (model.CourseId != Guid.Empty) result = await BLLCourse.Instantiate.Update(model);
                else result = await BLLCourse.Instantiate.Add(model);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除课程信息
        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid courseId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLCourse.Instantiate.Delete(courseId);
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