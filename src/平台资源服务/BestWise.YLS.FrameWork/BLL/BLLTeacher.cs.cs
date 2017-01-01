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
    /// 教师信息业务逻辑层
    /// </summary>
    public class BLLTeacher : BaseBLL
    {
        private static readonly DALTeacher _dal = new DALTeacher();
        private static BLLTeacher _instantiate = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public static BLLTeacher Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLTeacher()); }
        }

        #region 新增教师信息
        /// <summary>
        /// 新增教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回新增结果</returns>
        public ResultMessage Add(Teacher model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.TeacherId = Guid.NewGuid();
                    model.Mby = CurrentUser.UserId;
                    model.Cby = CurrentUser.UserId;
                    if (model.Pictures != null && model.Pictures.Count > 0)
                    {
                        foreach (TeacherPicture item in model.Pictures)
                        {
                            item.TeacherId = model.TeacherId;
                            item.Cby = CurrentUser.UserId;
                            item.Mby = CurrentUser.UserId;
                            item.Cdt = DateTime.Now;
                            item.Mdt = DateTime.Now;
                        }
                    }
                    if (_dal.AddTeacher(model, XMLHelper.SerializeXml(model.Pictures))) result = ResultMessage.SucceedResult("操作成功！");
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

        #region 更新教师信息
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回修改结果</returns>
        public ResultMessage Update(Teacher model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.Mby = CurrentUser.UserId;
                    if (model.Pictures != null && model.Pictures.Count > 0)
                    {
                        foreach (TeacherPicture item in model.Pictures)
                        {
                            item.TeacherId = model.TeacherId;
                            item.Mby = CurrentUser.UserId;
                            item.Cby = CurrentUser.UserId;
                            item.Mdt = DateTime.Now;
                        }
                    }
                    if (_dal.UpdateTeacher(model, XMLHelper.SerializeXml(model.Pictures))) result = ResultMessage.SucceedResult("操作成功！");
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

        #region 根据教师ID获取教师信息
        /// <summary>
        /// 根据教师ID获取教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>实体对象</returns>
        public ResultMessage<Teacher> GetModel(Guid teacherId)
        {
            ResultMessage<Teacher> result = ResultMessage<Teacher>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<Teacher>.SucceedResult(_dal.GetModel(teacherId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<Teacher>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 分页获取教师信息
        /// <summary>
        /// 分页获取教师信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>教师列表集合</returns>
        public ResultMessage<PagedList<Teacher>> GetPageList(BaseFilter baseFilter)
        {
            ResultMessage<PagedList<Teacher>> result = ResultMessage<PagedList<Teacher>>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<PagedList<Teacher>>.SucceedResult(_dal.GetPageList(baseFilter));
            }
            catch (Exception ex)
            {
                result = ResultMessage<PagedList<Teacher>>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 删除教师信息
        /// <summary>
        /// 删除教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid teacherId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(teacherId)) result = ResultMessage.SucceedResult("操作成功！");
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
