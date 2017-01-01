using BestWise.Common;
using BestWise.Common.Mvc;
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
    /// 资料附件数据访问层
    /// </summary>
    public class DALMaterialAttachment
    {
        #region 添加资料附件
        /// <summary>
        /// 添加资料附件
        /// </summary>
        /// <param name="dataXML">xml数据</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回添加结果</returns>
        internal bool AddMaterialAttachment(string dataXML, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetStoredProcCommand("AddMaterialAttachment"))
            {
                db.AddInParameter(dbCommand, "@DataXML", DbType.Xml, dataXML);
                result = transaction != null
                ? db.ExecuteNonQuery(dbCommand, transaction.Transaction) > 0
                : db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 删除资料附件
        /// <summary>
        /// 删除资料附件
        /// </summary>
        /// <param name="attachmentId">附件ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid attachmentId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE MaterialAttachment SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE AttachmentId = @AttachmentId  ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@AttachmentId", DbType.Guid, attachmentId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 根据附件ID获取附件信息
        /// <summary>
        /// 根据附件ID获取附件信息
        /// </summary>
        /// <param name="attachmentId">附件ID</param>
        /// <returns>实体对象</returns>
        internal MaterialAttachment GetModel(Guid attachmentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT AttachmentId,Name,MaterialId,AttachmentUrl,[Type] AS TypeText FROM MaterialAttachment ");
            strSql.Append("WHERE IsDeleted=0 AND AttachmentId = @AttachmentId");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@AttachmentId", DbType.Guid, attachmentId);
                return db.ExecuteReader(dbCommand).FirstOrDefault<MaterialAttachment>();
            }
        }
        #endregion
    }
}
