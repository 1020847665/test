using BestWise.Common;
using BestWise.User.FrameWork.BLL;
using BestWise.YLS.FrameWork.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestWise.Common.Mvc;
using BestWise.YLS.Model;

namespace BestWise.YLS.FrameWork.BLL
{
    /// <summary>
    /// 学员预约处理业务逻辑层
    /// </summary>
    public class BLLStudentBook : BaseBLL
    {
        private static readonly DALStudentBook _dal = new DALStudentBook();
        private static BLLStudentBook _instantiate = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public static BLLStudentBook Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLStudentBook()); }
        }

        #region 新增预约信息
        /// <summary>
        /// 新增预约信息
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回新增结果</returns>
        public ResultMessage Add(StudentBook model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.Mby = CurrentUser.UserId;
                    model.Cby = CurrentUser.UserId;
                    if (_dal.Add(model)) result = ResultMessage.SucceedResult("操作成功！");
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

        #region 更新预约信息
        /// <summary>
        /// 更新预约信息
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回修改结果</returns>
        public ResultMessage Update(StudentBook model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.Mby = CurrentUser.UserId;
                    if (_dal.Update(model)) result = ResultMessage.SucceedResult("操作成功！");
                }
                else result = ResultMessage.UnauthorizedResult("非法操作");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 根据预约ID获取预约信息
        /// <summary>
        /// 根据预约ID获取预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>实体对象</returns>
        public ResultMessage<StudentBook> GetModel(Guid bookId)
        {
            ResultMessage<StudentBook> result = ResultMessage<StudentBook>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<StudentBook>.SucceedResult(_dal.GetModel(bookId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<StudentBook>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 分页获取预约信息
        /// <summary>
        /// 分页获取预约信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>预约列表集合</returns>
        public ResultMessage<PagedList<StudentBook>> GetPageList(BaseFilter baseFilter)
        {
            ResultMessage<PagedList<StudentBook>> result = ResultMessage<PagedList<StudentBook>>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<PagedList<StudentBook>>.SucceedResult(_dal.GetPageList(baseFilter));
            }
            catch (Exception ex)
            {
                result = ResultMessage<PagedList<StudentBook>>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 删除预约信息
        /// <summary>
        /// 删除预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid bookId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(bookId)) result = ResultMessage.SucceedResult("操作成功！");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 预约状态处理
        /// <summary>
        /// 预约状态处理
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回处理结果</returns>
        public ResultMessage DealState(StudentBook model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.DealState(model)) result = ResultMessage.SucceedResult("操作成功！");
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
