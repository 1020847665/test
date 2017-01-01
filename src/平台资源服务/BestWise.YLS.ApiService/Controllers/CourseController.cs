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
    /// 课程相关接口
    /// </summary>
    [RoutePrefix("api/Course")]
    [BWAuthorize]
    public class CourseController : ApiController
    {
        #region 新增课程信息
        /// <summary>
        /// 新增课程信息
        /// </summary>
        /// <param name="model">课程信息</param>
        /// <returns>返回新增结果</returns>
        [HttpPost]
        [Route("Add")]
        public ResultMessage Add(Course model)
        {
            return BLLCourse.Instantiate.Add(model);
        }
        #endregion

        #region 分页获取课程信息
        /// <summary>
        /// 分页获取课程信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>课程列表集合</returns>
        [HttpPost]
        [Route("GetPageList")]
        public ResultMessage<PagedList<Course>> GetPageList(BaseFilter baseFilter)
        {
            return BLLCourse.Instantiate.GetPageList(baseFilter);
        }
        #endregion

        #region 更新课程信息
        /// <summary>
        /// 更新课程信息
        /// </summary>
        /// <param name="model">课程信息</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("Update")]
        public ResultMessage Update(Course model)
        {
            return BLLCourse.Instantiate.Update(model);
        }
        #endregion

        #region 根据课程ID获取课程信息
        /// <summary>
        /// 根据课程ID获取课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>实体对象</returns>
        [HttpGet]
        [Route("GetModel")]
        public ResultMessage<Course> GetModel(Guid courseId)
        {
            return BLLCourse.Instantiate.GetModel(courseId);
        }
        #endregion

        #region 删除课程信息
        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("Delete")]
        public ResultMessage Delete(Guid courseId)
        {
            return BLLCourse.Instantiate.Delete(courseId);
        }
        #endregion
    }
}
