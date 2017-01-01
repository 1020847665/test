using BestWise.Common;
using BestWise.YLS.FrameWork.DAL;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestWise.User.FrameWork.BLL;
using BestWise.Common.Mvc;
using BestWise.YLS.Model.Enums;

namespace BestWise.YLS.FrameWork.BLL
{
    /// <summary>
    /// 课程信息业务逻辑层
    /// </summary>
    public class BLLCourse : BaseBLL
    {
        private static readonly DALCourse _dal = new DALCourse();
        private static BLLCourse _instantiate = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public static BLLCourse Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLCourse()); }
        }

        #region 新增课程信息
        /// <summary>
        /// 新增课程信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回新增结果</returns>
        public ResultMessage Add(Course model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.CourseId = Guid.NewGuid();
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

        #region 更新课程信息
        /// <summary>
        /// 更新课程信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回修改结果</returns>
        public ResultMessage Update(Course model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.Mby = CurrentUser.UserId;
                    if (_dal.Update(model)) result = ResultMessage.SucceedResult("操作成功！");
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

        #region 根据课程ID获取课程信息
        /// <summary>
        /// 根据课程ID获取课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>实体对象</returns>
        public ResultMessage<Course> GetModel(Guid courseId)
        {
            ResultMessage<Course> result = ResultMessage<Course>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<Course>.SucceedResult(_dal.GetModel(courseId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<Course>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 分页获取课程信息
        /// <summary>
        /// 分页获取课程信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>课程列表集合</returns>
        public ResultMessage<PagedList<Course>> GetPageList(BaseFilter baseFilter)
        {
            ResultMessage<PagedList<Course>> result = ResultMessage<PagedList<Course>>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<PagedList<Course>>.SucceedResult(_dal.GetPageList(baseFilter));
            }
            catch (Exception ex)
            {
                result = ResultMessage<PagedList<Course>>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 删除课程信息
        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid courseId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(courseId)) result = ResultMessage.SucceedResult("操作成功！");
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
