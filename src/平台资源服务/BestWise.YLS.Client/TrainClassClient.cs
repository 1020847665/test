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
    /// 培训班Client
    /// </summary>
    public class TrainClassClient
    {
        #region 新增培训班信息
        /// <summary>
        /// 新增培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <returns>返回新增结果</returns>
        public static async Task<ResultMessage> Add(TrainClass model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/TrainClass/Add"), HttpMethod.Post, model);
        }
        #endregion

        #region 分页获取培训班信息
        /// <summary>
        /// 分页获取培训班信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>培训班列表集合</returns>
        public static async Task<ResultMessage<PagedList<TrainClass>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<TrainClass>>(string.Concat(Configuration.SourceServerAddress, "api/TrainClass/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 根据培训ID获取培训信息
        /// <summary>
        /// 根据培训ID获取培训信息
        /// </summary>
        /// <param name="trainId">培训ID</param>
        /// <returns>实体对象</returns>
        public static async Task<ResultMessage<TrainClass>> GetModel(Guid trainId)
        {
            return await HttpClientHelper.RequestAsync<TrainClass>(string.Concat(Configuration.SourceServerAddress, "api/TrainClass/GetModel"), HttpMethod.Get, new { trainId = trainId });
        }
        #endregion

        #region 更新培训班信息
        /// <summary>
        /// 更新培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <returns>返回修改结果</returns>
        public static async Task<ResultMessage> Update(TrainClass model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/TrainClass/Update"), HttpMethod.Post, model);
        }
        #endregion

        #region 删除培训班信息
        /// <summary>
        /// 删除培训班信息
        /// </summary>
        /// <param name="trainId">培训班ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> Delete(Guid trainId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/TrainClass/Delete"), HttpMethod.Post, new { trainId = trainId });
        }
        #endregion

        #region 获取联系人信息
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回联系人信息</returns>
        public static async Task<ResultMessage<List<TrainContact>>> GetContactList(Guid trainId)
        {
            return await HttpClientHelper.RequestAsync<List<TrainContact>>(string.Concat(Configuration.SourceServerAddress, "api/TrainClass/GetContactList"), HttpMethod.Get, new { trainId = trainId });
        }
        #endregion

        #region 删除联系人
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> DeleteContact(Guid contactId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/TrainClass/DeleteContact"), HttpMethod.Post, new { contactId = contactId });
        }
        #endregion
    }
}
