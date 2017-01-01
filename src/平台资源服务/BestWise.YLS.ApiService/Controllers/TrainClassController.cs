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
    /// 培训班相关接口
    /// </summary>
    [RoutePrefix("api/TrainClass")]
    [BWAuthorize]
    public class TrainClassController : ApiController
    {
        #region 新增培训班信息
        /// <summary>
        /// 新增培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <returns>返回新增结果</returns>
        [HttpPost]
        [Route("Add")]
        public ResultMessage Add(TrainClass model)
        {
            return BLLTrainClass.Instantiate.Add(model);
        }
        #endregion

        #region 分页获取培训班信息
        /// <summary>
        /// 分页获取培训班信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>培训班列表集合</returns>
        [HttpPost]
        [Route("GetPageList")]
        public ResultMessage<PagedList<TrainClass>> GetPageList(BaseFilter baseFilter)
        {
            return BLLTrainClass.Instantiate.GetPageList(baseFilter);
        }
        #endregion

        #region 更新培训班信息
        /// <summary>
        /// 更新培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("Update")]
        public ResultMessage Update(TrainClass model)
        {
            return BLLTrainClass.Instantiate.Update(model);
        }
        #endregion

        #region 根据培训ID获取培训信息
        /// <summary>
        /// 根据培训ID获取培训信息
        /// </summary>
        /// <param name="trainId">培训ID</param>
        /// <returns>实体对象</returns>
        [HttpGet]
        [Route("GetModel")]
        public ResultMessage<TrainClass> GetModel(Guid trainId)
        {
            return BLLTrainClass.Instantiate.GetModel(trainId);
        }
        #endregion

        #region 删除培训班信息
        /// <summary>
        /// 删除培训班信息
        /// </summary>
        /// <param name="trainId">培训班ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("Delete")]
        public ResultMessage Delete(Guid trainId)
        {
            return BLLTrainClass.Instantiate.Delete(trainId);
        }
        #endregion

        #region 获取联系人信息
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回联系人信息</returns>
        [HttpGet]
        [Route("GetContactList")]
        public ResultMessage<List<TrainContact>> GetContactList(Guid trainId)
        {
            return BLLTrainContact.Instantiate.GetContactList(trainId);
        }
        #endregion

        #region 删除联系人
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("DeleteContact")]
        public ResultMessage DeleteContact(Guid contactId)
        {
            return BLLTrainContact.Instantiate.Delete(contactId);
        }
        #endregion
    }
}
