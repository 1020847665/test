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
    /// 教师图片业务处理层
    /// </summary>
    public class BLLTeacherPicture
    {
        private static readonly DALTeacherPicture _dal = new DALTeacherPicture();
        private static BLLTeacherPicture _instantiate = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public static BLLTeacherPicture Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLTeacherPicture()); }
        }

        #region 删除教师图片
        /// <summary>
        /// 删除教师图片
        /// </summary>
        /// <param name="pictureId">图片ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid pictureId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(pictureId)) result = ResultMessage.SucceedResult("操作成功！");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 根据教师ID获取教师图片信息
        /// <summary>
        /// 根据教师ID获取教师图片信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回教师图片</returns>
        public ResultMessage<List<TeacherPicture>> GetPictureList(Guid teacherId)
        {
            ResultMessage<List<TeacherPicture>> result = ResultMessage<List<TeacherPicture>>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<List<TeacherPicture>>.SucceedResult(_dal.GetPictureList(teacherId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<List<TeacherPicture>>.FailureResult(ex);
            }
            return result;
        }
        #endregion
    }
}
