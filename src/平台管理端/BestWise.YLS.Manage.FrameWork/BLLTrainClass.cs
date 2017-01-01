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
    /// 培训班信息业务逻辑层
    /// </summary>
    public class BLLTrainClass
    {
        private static BLLTrainClass _instantiate = null;

        /// <summary>
        /// 实例对象
        /// </summary>
        public static BLLTrainClass Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLTrainClass()); }
        }

        #region 新增培训班信息
        /// <summary>
        /// 新增培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <returns>返回新增结果</returns>
        public async Task<ResultMessage> Add(TrainClass model)
        {
            return await TrainClassClient.Add(model);
        }
        #endregion

        #region 删除培训班信息
        /// <summary>
        /// 删除培训班信息
        /// </summary>
        /// <param name="trainId">培训班ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> Delete(Guid trainId)
        {
            return await TrainClassClient.Delete(trainId);
        }
        #endregion

        #region 更新教师信息
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回修改结果</returns>
        public async Task<ResultMessage> Update(TrainClass model)
        {
            return await TrainClassClient.Update(model);
        }
        #endregion

        #region 分页获取培训班信息
        /// <summary>
        /// 分页获取培训班信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>培训班列表集合</returns>
        public async Task<ResultMessage<PagedList<TrainClass>>> GetPageList(BaseFilter baseFilter)
        {
            return await TrainClassClient.GetPageList(baseFilter);
        }
        #endregion

        #region 根据培训ID获取培训信息
        /// <summary>
        /// 根据培训ID获取培训信息
        /// </summary>
        /// <param name="trainId">培训ID</param>
        /// <returns>实体对象</returns>
        public async Task<ResultMessage<TrainClass>> GetModel(Guid trainId)
        {
            return await TrainClassClient.GetModel(trainId);
        }
        #endregion

        #region 获取联系人信息
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回联系人信息</returns>
        public async Task<ResultMessage<List<TrainContact>>> GetContactList(Guid trainId)
        {
            return await TrainClassClient.GetContactList(trainId);
        }
        #endregion

        #region 删除联系人
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> DeleteContact(Guid contactId)
        {
            return await TrainClassClient.DeleteContact(contactId);
        }
        #endregion
    }
}
