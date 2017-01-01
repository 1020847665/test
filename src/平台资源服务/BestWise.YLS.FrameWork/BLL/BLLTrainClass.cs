using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.User.FrameWork.BLL;
using BestWise.YLS.FrameWork.DAL;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.FrameWork.BLL
{
    /// <summary>
    /// 培训班业务逻辑层
    /// </summary>
    public class BLLTrainClass : BaseBLL
    {
        private static readonly DALTrainClass _dal = new DALTrainClass();
        private static BLLTrainClass _instantiate = null;

        /// <summary>
        /// 实例化对象
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
        public ResultMessage Add(TrainClass model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.TrainId = Guid.NewGuid();
                    model.Mby = CurrentUser.UserId;
                    model.Cby = CurrentUser.UserId;
                    if (model.Contacts != null && model.Contacts.Count > 0)
                    {
                        foreach (TrainContact item in model.Contacts)
                        {
                            item.TrainId = model.TrainId;
                            item.Cby = CurrentUser.UserId;
                            item.Mby = CurrentUser.UserId;
                            item.Cdt = DateTime.Now;
                            item.Mdt = DateTime.Now;
                        }
                    }
                    if (_dal.AddTrainClass(model, XMLHelper.SerializeXml(model.Contacts))) result = ResultMessage.SucceedResult("操作成功！");
                }
                else result = ResultMessage.UnauthorizedResult();
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 更新培训班信息
        /// <summary>
        /// 更新培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <returns>返回修改结果</returns>
        public ResultMessage Update(TrainClass model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.Mby = CurrentUser.UserId;
                    model.Cby = CurrentUser.UserId;
                    if (model.Contacts != null && model.Contacts.Count > 0)
                    {
                        foreach (TrainContact item in model.Contacts)
                        {
                            item.TrainId = model.TrainId;
                            item.Mby = CurrentUser.UserId;
                            item.Mdt = DateTime.Now;
                        }
                    }
                    if (_dal.UpdateTrainClass(model, XMLHelper.SerializeXml(model.Contacts))) result = ResultMessage.SucceedResult("操作成功！");
                }
                else result = ResultMessage.FailureResult("非法操作");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 根据培训ID获取培训信息
        /// <summary>
        /// 根据培训ID获取培训信息
        /// </summary>
        /// <param name="trainId">培训ID</param>
        /// <returns>实体对象</returns>
        public ResultMessage<TrainClass> GetModel(Guid trainId)
        {
            ResultMessage<TrainClass> result = ResultMessage<TrainClass>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<TrainClass>.SucceedResult(_dal.GetModel(trainId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<TrainClass>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 分页获取培训班信息
        /// <summary>
        /// 分页获取培训班信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>培训班列表集合</returns>
        public ResultMessage<PagedList<TrainClass>> GetPageList(BaseFilter baseFilter)
        {
            ResultMessage<PagedList<TrainClass>> result = ResultMessage<PagedList<TrainClass>>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<PagedList<TrainClass>>.SucceedResult(_dal.GetPageList(baseFilter));
            }
            catch (Exception ex)
            {
                result = ResultMessage<PagedList<TrainClass>>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 删除培训班信息
        /// <summary>
        /// 删除培训班信息
        /// </summary>
        /// <param name="trainId">培训班ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid trainId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(trainId)) result = ResultMessage.SucceedResult("操作成功！");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion
    }
}
