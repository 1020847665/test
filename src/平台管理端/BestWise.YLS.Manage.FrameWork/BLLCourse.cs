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
    /// 课程业务逻辑层
    /// </summary>
    public class BLLCourse
    {
        private static BLLCourse _instantiate = null;
        /// <summary>
        /// 实例对象
        /// </summary>
        public static BLLCourse Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLCourse()); }
        }

        #region 新增课程信息
        /// <summary>
        /// 新增课程信息
        /// </summary>
        /// <param name="model">课程信息</param>
        /// <returns>返回新增结果</returns>
        public async Task<ResultMessage> Add(Course model)
        {
            return await CourseClient.Add(model);
        }
        #endregion

        #region 删除课程信息
        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> Delete(Guid courseId)
        {
            return await CourseClient.Delete(courseId);
        }
        #endregion

        #region 更新课程信息
        /// <summary>
        /// 更新课程信息
        /// </summary>
        /// <param name="model">课程信息</param>
        /// <returns>返回修改结果</returns>
        public async Task<ResultMessage> Update(Course model)
        {
            return await CourseClient.Update(model);
        }
        #endregion

        #region 分页获取课程信息
        /// <summary>
        /// 分页获取课程信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>课程列表集合</returns>
        public async Task<ResultMessage<PagedList<Course>>> GetPageList(BaseFilter baseFilter)
        {
            return await CourseClient.GetPageList(baseFilter);
        }
        #endregion

        #region 根据课程ID获取课程信息
        /// <summary>
        /// 根据课程ID获取课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>实体对象</returns>
        public async Task<ResultMessage<Course>> GetModel(Guid courseId)
        {
            return await CourseClient.GetModel(courseId);
        }
        #endregion
    }
}
