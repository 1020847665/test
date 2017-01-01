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
    /// 学员预约业务逻辑处理层
    /// </summary>
    public class BLLStudentBook
    {
        private static BLLStudentBook _instantiate = null;

        /// <summary>
        /// 实例对象
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
        public async Task<ResultMessage> Add(StudentBook model)
        {
            return await StudentBookClient.Add(model);
        }
        #endregion

        #region 删除预约信息
        /// <summary>
        /// 删除预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> Delete(Guid bookId)
        {
            return await StudentBookClient.Delete(bookId);
        }
        #endregion

        #region 更新预约信息
        /// <summary>
        /// 更新预约信息
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回修改结果</returns>
        public async Task<ResultMessage> Update(StudentBook model)
        {
            return await StudentBookClient.Update(model);
        }
        #endregion

        #region 分页获取预约信息
        /// <summary>
        /// 分页获取预约信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>预约列表集合</returns>
        public async Task<ResultMessage<PagedList<StudentBook>>> GetPageList(BaseFilter baseFilter)
        {
            return await StudentBookClient.GetPageList(baseFilter);
        }
        #endregion

        #region 根据预约ID获取预约信息
        /// <summary>
        /// 根据预约ID获取预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>实体对象</returns>
        public async Task<ResultMessage<StudentBook>> GetModel(Guid bookId)
        {
            return await StudentBookClient.GetModel(bookId);
        }
        #endregion

        #region 预约状态处理
        /// <summary>
        /// 预约状态处理
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回处理结果</returns>
        public async Task<ResultMessage> DealState(StudentBook model)
        {
            return await StudentBookClient.DealState(model);
        }
        #endregion
    }
}
