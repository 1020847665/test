using BestWise.EnterpriseLibrary.Data;
using BestWise.YLS.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.FrameWork.DAL
{
    /// <summary>
    /// 教师图片数据访问层
    /// </summary>
    public class DALTeacherPicture
    {
        #region 添加教师图片
        /// <summary>
        /// 添加教师图片
        /// </summary>
        /// <param name="dataXML">xml数据</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回添加结果</returns>
        internal bool AddTeacherPicture(string dataXML, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetStoredProcCommand("AddTeacherPicture"))
            {
                db.AddInParameter(dbCommand, "@DataXML", DbType.Xml, dataXML);
                result = transaction != null
                ? db.ExecuteNonQuery(dbCommand, transaction.Transaction) > 0
                : db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 删除教师图片
        /// <summary>
        /// 删除教师图片
        /// </summary>
        /// <param name="pictureId">图片ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid pictureId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE TeacherPicture SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE PictureId = @PictureId  ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@AttachmentId", DbType.Guid, pictureId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 获取教师图片
        /// <summary>
        /// 获取教师图片
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回教师图片信息</returns>
        public List<TeacherPicture> GetPictureList(Guid teacherId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT PictureId,TeacherId,Url,Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM TeacherPicture ");
            strSql.Append("WHERE TeacherId=@TeacherId ORDER BY Cdt DESC ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TeacherId", DbType.Guid, teacherId);
                return db.ExecuteDataSet(dbCommand).ToList<TeacherPicture>();
            }
        }
        #endregion
      
    }
}
