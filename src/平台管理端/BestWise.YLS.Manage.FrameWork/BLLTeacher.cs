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
    /// 教师信息业务逻辑层
    /// </summary>
    public class BLLTeacher
    {
        private static BLLTeacher _instantiate = null;
        /// <summary>
        /// 实例对象
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
        public async Task<ResultMessage> Add(Teacher model)
        {
            return await TeacherClient.Add(model);
        }
        #endregion

        #region 删除教师信息
        /// <summary>
        /// 删除教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> Delete(Guid teacherId)
        {
            return await TeacherClient.Delete(teacherId);
        }
        #endregion

        #region 更新教师信息
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回修改结果</returns>
        public async Task<ResultMessage> Update(Teacher model)
        {
            return await TeacherClient.Update(model);
        }
        #endregion

        #region 分页获取教师信息
        /// <summary>
        /// 分页获取教师信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>教师列表集合</returns>
        public async Task<ResultMessage<PagedList<Teacher>>> GetPageList(BaseFilter baseFilter)
        {
            return await TeacherClient.GetPageList(baseFilter);
        }
        #endregion

        #region 根据教师ID获取教师信息
        /// <summary>
        /// 根据教师ID获取教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>实体对象</returns>
        public async Task<ResultMessage<Teacher>> GetModel(Guid teacherId)
        {
            return await TeacherClient.GetModel(teacherId);
        }
        #endregion

        #region 获取教师图片
        /// <summary>
        /// 获取教师图片
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回教师图片</returns>
        public async Task<ResultMessage<List<TeacherPicture>>> GetPictureList(Guid teacherId)
        {
            return await TeacherClient.GetPictureList(teacherId);
        }
        #endregion

        #region 删除教师图片
        /// <summary>
        /// 删除教师图片
        /// </summary>
        /// <param name="pictureId">图片ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> DeletePicture(Guid pictureId)
        {
            return await TeacherClient.DeletePicture(pictureId);
        }
        #endregion
    }
}
