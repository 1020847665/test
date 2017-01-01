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
    /// 教师相关接口
    /// </summary>
    [RoutePrefix("api/Teacher")]
    [BWAuthorize]
    public class TeacherController : ApiController
    {
        #region 新增教师信息
        /// <summary>
        /// 新增教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回新增结果</returns>
        [HttpPost]
        [Route("Add")]
        public ResultMessage Add(Teacher model)
        {
            return BLLTeacher.Instantiate.Add(model);
        }
        #endregion

        #region 分页获取教师信息
        /// <summary>
        /// 分页获取教师信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>教师列表集合</returns>
        [HttpPost]
        [Route("GetPageList")]
        public ResultMessage<PagedList<Teacher>> GetPageList(BaseFilter baseFilter)
        {
            return BLLTeacher.Instantiate.GetPageList(baseFilter);
        }
        #endregion

        #region 更新教师信息
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("Update")]
        public ResultMessage Update(Teacher model)
        {
            return BLLTeacher.Instantiate.Update(model);
        }
        #endregion

        #region 根据教师ID获取教师信息
        /// <summary>
        /// 根据教师ID获取教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>实体对象</returns>
        [HttpGet]
        [Route("GetModel")]
        public ResultMessage<Teacher> GetModel(Guid teacherId)
        {
            return BLLTeacher.Instantiate.GetModel(teacherId);
        }
        #endregion

        #region 删除教师信息
        /// <summary>
        /// 删除教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("Delete")]
        public ResultMessage Delete(Guid teacherId)
        {
            return BLLTeacher.Instantiate.Delete(teacherId);
        }
        #endregion

        #region 获取教师图片
        /// <summary>
        /// 获取教师图片
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回教师图片</returns>
        [HttpGet]
        [Route("GetPictureList")]
        public ResultMessage<List<TeacherPicture>> GetPictureList(Guid teacherId)
        {
            return BLLTeacherPicture.Instantiate.GetPictureList(teacherId);
        }
        #endregion

        #region 删除教师图片
        /// <summary>
        /// 删除教师图片
        /// </summary>
        /// <param name="pictureId">图片ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("DeletePicture")]
        public ResultMessage DeletePicture(Guid pictureId)
        {
            return BLLTeacherPicture.Instantiate.Delete(pictureId);
        }
        #endregion
    }
}
