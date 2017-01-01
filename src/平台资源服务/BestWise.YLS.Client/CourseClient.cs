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
    /// 课程Client
    /// </summary>
    public class CourseClient
    {
        #region 新增课程信息
        /// <summary>
        /// 新增课程信息
        /// </summary>
        /// <param name="model">课程信息</param>
        /// <returns>返回新增结果</returns>
        public static async Task<ResultMessage> Add(Course model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Course/Add"), HttpMethod.Post, model);
        }
        #endregion

        #region 分页获取课程信息
        /// <summary>
        /// 分页获取课程信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>课程列表集合</returns>
        public static async Task<ResultMessage<PagedList<Course>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<Course>>(string.Concat(Configuration.SourceServerAddress, "api/Course/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 根据课程ID获取课程信息
        /// <summary>
        /// 根据课程ID获取课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>实体对象</returns>
        public static async Task<ResultMessage<Course>> GetModel(Guid courseId)
        {
            return await HttpClientHelper.RequestAsync<Course>(string.Concat(Configuration.SourceServerAddress, "api/Course/GetModel"), HttpMethod.Get, new { courseId = courseId });
        }
        #endregion

        #region 更新课程信息
        /// <summary>
        /// 更新课程信息
        /// </summary>
        /// <param name="model">课程信息</param>
        /// <returns>返回修改结果</returns>
        public static async Task<ResultMessage> Update(Course model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Course/Update"), HttpMethod.Post, model);
        }
        #endregion

        #region 删除课程信息
        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> Delete(Guid courseId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Course/Delete"), HttpMethod.Post, new { courseId = courseId });
        }
        #endregion
    }
}
