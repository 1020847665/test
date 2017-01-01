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
    /// 学员预约Client
    /// </summary>
    public class StudentBookClient
    {
        #region 新增预约信息
        /// <summary>
        /// 新增预约信息
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回新增结果</returns>
        public static async Task<ResultMessage> Add(StudentBook model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/StudentBook/Add"), HttpMethod.Post, model);
        }
        #endregion

        #region 分页获取预约信息
        /// <summary>
        /// 分页获取预约信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>预约列表集合</returns>
        public static async Task<ResultMessage<PagedList<StudentBook>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<StudentBook>>(string.Concat(Configuration.SourceServerAddress, "api/StudentBook/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 根据预约ID获取预约信息
        /// <summary>
        /// 根据预约ID获取预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>实体对象</returns>
        public static async Task<ResultMessage<StudentBook>> GetModel(Guid bookId)
        {
            return await HttpClientHelper.RequestAsync<StudentBook>(string.Concat(Configuration.SourceServerAddress, "api/StudentBook/GetModel"), HttpMethod.Get, new { bookId = bookId });
        }
        #endregion

        #region 更新预约信息
        /// <summary>
        /// 更新预约信息
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回修改结果</returns>
        public static async Task<ResultMessage> Update(StudentBook model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/StudentBook/Update"), HttpMethod.Post, model);
        }
        #endregion

        #region 删除预约信息
        /// <summary>
        /// 删除预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> Delete(Guid bookId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/StudentBook/Delete"), HttpMethod.Post, new { bookId = bookId });
        }
        #endregion

        #region 预约状态处理
        /// <summary>
        /// 预约状态处理
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回处理结果</returns>
        public static async Task<ResultMessage> DealState(StudentBook model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/StudentBook/DealState"), HttpMethod.Post, model);
        }
        #endregion
    }
}
