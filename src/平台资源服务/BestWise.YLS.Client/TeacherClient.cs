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
    /// 教师信息Client
    /// </summary>
    public class TeacherClient
    {
        #region 新增教师信息
        /// <summary>
        /// 新增教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回新增结果</returns>
        public static async Task<ResultMessage> Add(Teacher model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Teacher/Add"), HttpMethod.Post, model);
        }
        #endregion

        #region 分页获取教师信息
        /// <summary>
        /// 分页获取教师信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>教师列表集合</returns>
        public static async Task<ResultMessage<PagedList<Teacher>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<Teacher>>(string.Concat(Configuration.SourceServerAddress, "api/Teacher/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 根据教师ID获取教师信息
        /// <summary>
        /// 根据教师ID获取教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>实体对象</returns>
        public static async Task<ResultMessage<Teacher>> GetModel(Guid teacherId)
        {
            return await HttpClientHelper.RequestAsync<Teacher>(string.Concat(Configuration.SourceServerAddress, "api/Teacher/GetModel"), HttpMethod.Get, new { teacherId = teacherId });
        }
        #endregion

        #region 更新教师信息
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回修改结果</returns>
        public static async Task<ResultMessage> Update(Teacher model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Teacher/Update"), HttpMethod.Post, model);
        }
        #endregion

        #region 删除教师信息
        /// <summary>
        /// 删除教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> Delete(Guid teacherId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Teacher/Delete"), HttpMethod.Post, new { teacherId = teacherId });
        }
        #endregion

        #region 获取教师图片
        /// <summary>
        /// 获取教师图片
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回教师图片</returns>
        public static async Task<ResultMessage<List<TeacherPicture>>> GetPictureList(Guid teacherId)
        {
            return await HttpClientHelper.RequestAsync<List<TeacherPicture>>(string.Concat(Configuration.SourceServerAddress, "api/Teacher/GetPictureList"), HttpMethod.Get, new { teacherId = teacherId });
        }
        #endregion

        #region 删除教师图片
        /// <summary>
        /// 删除教师图片
        /// </summary>
        /// <param name="pictureId">图片ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> DeletePicture(Guid pictureId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/Teacher/DeletePicture"), HttpMethod.Post, new { pictureId = pictureId });
        }
        #endregion
    }
}
