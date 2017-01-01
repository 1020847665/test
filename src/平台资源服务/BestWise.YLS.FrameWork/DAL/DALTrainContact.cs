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
    /// 培训班联系人数据访问层
    /// </summary>
    public class DALTrainContact
    {
        #region 添加培训班联系人
        /// <summary>
        /// 添加培训班联系人
        /// </summary>
        /// <param name="dataXML">xml数据</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回添加结果</returns>
        internal bool AddTrainContact(string dataXML, BWDatabaseWithTransaction transaction = null)
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

        #region 删除培训班联系人
        /// <summary>
        /// 删除培训班联系人
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid contactId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE TrainContact SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE ContactId = @ContactId  ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@ContactId", DbType.Guid, contactId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
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
        public List<TrainContact> GetContactList(Guid contactId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ContactId,TrainId,Name,ContactNumber,Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM TrainContact ");
            strSql.Append("WHERE ContactId=@ContactId ORDER BY Cdt DESC ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@ContactId", DbType.Guid, contactId);
                return db.ExecuteDataSet(dbCommand).ToList<TrainContact>();
            }
        }
        #endregion
    }
}
