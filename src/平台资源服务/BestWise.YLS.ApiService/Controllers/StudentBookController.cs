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
    /// 学员预约相关接口
    /// </summary>
    [RoutePrefix("api/StudentBook")]
    [BWAuthorize]
    public class StudentBookController : ApiController
    {
        #region 新增预约信息
        /// <summary>
        /// 新增预约信息
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回新增结果</returns>
        [HttpPost]
        [Route("Add")]
        public ResultMessage Add(StudentBook model)
        {
            return BLLStudentBook.Instantiate.Add(model);
        }
        #endregion

        #region 分页获取预约信息
        /// <summary>
        /// 分页获取预约信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>预约列表集合</returns>
        [HttpPost]
        [Route("GetPageList")]
        public ResultMessage<PagedList<StudentBook>> GetPageList(BaseFilter baseFilter)
        {
            return BLLStudentBook.Instantiate.GetPageList(baseFilter);
        }
        #endregion

        #region 更新预约信息
        /// <summary>
        /// 更新预约信息
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("Update")]
        public ResultMessage Update(StudentBook model)
        {
            return BLLStudentBook.Instantiate.Update(model);
        }
        #endregion

        #region 根据预约ID获取预约信息
        /// <summary>
        /// 根据预约ID获取预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>实体对象</returns>
        [HttpGet]
        [Route("GetModel")]
        public ResultMessage<StudentBook> GetModel(Guid bookId)
        {
            return BLLStudentBook.Instantiate.GetModel(bookId);
        }
        #endregion

        #region 删除预约信息
        /// <summary>
        /// 删除预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("Delete")]
        public ResultMessage Delete(Guid bookId)
        {
            return BLLStudentBook.Instantiate.Delete(bookId);
        }
        #endregion

        #region 预约状态处理
        /// <summary>
        /// 预约状态处理
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回处理结果</returns>
        [HttpPost]
        [Route("DealState")]
        public ResultMessage DealState(StudentBook model)
        {
            return BLLStudentBook.Instantiate.DealState(model);
        }
        #endregion
    }
}
