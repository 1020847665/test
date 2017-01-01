using BestWise.Common;
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
    /// 培训班联系人业务逻辑层
    /// </summary>
    public class BLLTrainContact
    {
        private static readonly DALTrainContact _dal = new DALTrainContact();
        private static BLLTrainContact _instantiate = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public static BLLTrainContact Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLTrainContact()); }
        }

        #region 删除培训班联系人
        /// <summary>
        /// 删除培训班联系人
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid contactId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(contactId)) result = ResultMessage.SucceedResult("操作成功！");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 获取联系人信息
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回联系人信息</returns>
        public ResultMessage<List<TrainContact>> GetContactList(Guid contactId)
        {
            ResultMessage<List<TrainContact>> result = ResultMessage<List<TrainContact>>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<List<TrainContact>>.SucceedResult(_dal.GetContactList(contactId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<List<TrainContact>>.FailureResult(ex);
            }
            return result;
        }
        #endregion
    }
}
