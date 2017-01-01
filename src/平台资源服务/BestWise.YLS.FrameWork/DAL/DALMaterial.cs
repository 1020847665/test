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
    /// 资料信息数据访问层
    /// </summary>
    public class DALMaterial
    {
        #region 根据资料ID获取资料信息
        /// <summary>
        /// 根据资料ID获取资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>实体对象</returns>
        internal Material GetModel(Guid materialId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MaterialId,Name,Introduce,IsDeleted,Cdt,Cby,Mdt,Mby,Notes FROM Material ");
            strSql.Append("WHERE MaterialId = @MaterialId");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@MaterialId", DbType.Guid, materialId);
                return db.ExecuteReader(dbCommand).FirstOrDefault<Material>();
            }
        }
        #endregion

        #region 更新资料信息
        /// <summary>
        /// 更新资料信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回更新结果</returns>
        internal bool Update(Material model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Material SET ");
            strSql.Append("Name = @Name,");
            strSql.Append("Introduce = @Introduce,");
            strSql.Append("Notes = @Notes,");
            strSql.Append("Mdt = @Mdt,");
            strSql.Append("Mby = @Mby ");
            strSql.Append("WHERE MaterialId = @MaterialId  ");
            var db = transaction != null ? transaction.Database : DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@MaterialId", DbType.Guid, model.MaterialId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String, model.Introduce);
                db.AddInParameter(dbCommand, "@Notes", DbType.String, model.Notes);
                db.AddInParameter(dbCommand, "@Mdt", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@Mby", DbType.Guid, model.Mby);
                result = transaction != null
                 ? db.ExecuteNonQuery(dbCommand, transaction.Transaction) > 0
                 : db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 新增资料信息
        /// <summary>
        /// 新增资料信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回新增结果</returns>
        internal bool Add(Material model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Material(");
            strSql.Append("MaterialId,");
            strSql.Append("Name,");
            strSql.Append("Introduce,");
            strSql.Append("Notes,");
            strSql.Append("IsDeleted,");
            strSql.Append("Cdt,");
            strSql.Append("Cby,");
            strSql.Append("Mdt,");
            strSql.Append("Mby");
            strSql.Append(") VALUES (");
            strSql.Append("@MaterialId,");
            strSql.Append("@Name,");
            strSql.Append("@Introduce,");
            strSql.Append("@Notes,");
            strSql.Append("@IsDeleted,");
            strSql.Append("@Cdt,");
            strSql.Append("@Cby,");
            strSql.Append("@Mdt,");
            strSql.Append("@Mby)");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@MaterialId", DbType.Guid, model.MaterialId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String,model.Introduce );
                db.AddInParameter(dbCommand, "@Notes", DbType.String, model.Notes);
                db.AddInParameter(dbCommand, "@IsDeleted", DbType.Boolean, false);
                db.AddInParameter(dbCommand, "@Cdt", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@Cby", DbType.Guid, model.Cby);
                db.AddInParameter(dbCommand, "@Mdt", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@Mby", DbType.Guid, model.Mby);
                result = transaction != null
               ? db.ExecuteNonQuery(dbCommand, transaction.Transaction) > 0
               : db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 分页获取资料信息
        /// <summary>
        /// 分页获取资料信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>资料列表集合</returns>
        internal PagedList<Material> GetPageList(BaseFilter baseFilter)
        {
            SearchCondition searchCondition = new SearchCondition(baseFilter.Condition, baseFilter.Sort);
            PagedList<Material> pagedList = null;
            string sort = !string.IsNullOrWhiteSpace(searchCondition.GetSort()) ? searchCondition.GetSort() : "Cdt DESC ";
            string filter = string.Concat("WHERE IsDeleted=0 ", searchCondition.GetFilter());
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT COUNT(1) FROM Material ");
            strsql.Append(filter);

            strsql.Append("SELECT a.MaterialId,a.Name,a.Cdt,b.AttachmentCount FROM( ");
            strsql.Append("SELECT MaterialId,Name,Introduce,Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM Material ");
            strsql.Append(filter);
            strsql.Append(") AS a LEFT JOIN (SELECT ISNULL(COUNT(AttachmentId),0) AS AttachmentCount,MaterialId FROM MaterialAttachment WHERE IsDeleted=0 GROUP BY MaterialId) AS b ");
            strsql.Append("ON b.MaterialId = a.MaterialId ");
            strsql.AppendFormat("ORDER BY {0} ", sort);
            if (baseFilter.IsPage) strsql.Append("OFFSET @PageIndex ROWS FETCH NEXT @PageSize ROWS ONLY ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strsql.ToString()))
            {
                if (baseFilter.IsPage)
                {
                    db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, (baseFilter.PageIndex - 1) * baseFilter.PageSize);
                    db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, baseFilter.PageSize);
                }
                var ds = db.ExecuteDataSet(dbCommand);
                pagedList = ds != null && ds.Tables[0] != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0
                     ? new PagedList<Material>(ds.Tables[1].ToList<Material>(),
                         int.Parse(ds.Tables[0].Rows[0][0].ToString()), baseFilter.PageSize, baseFilter.PageIndex)
                     : null;
            }
            return pagedList;
        }
        #endregion

        #region 删除资料信息
        /// <summary>
        /// 删除资料信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid courseId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Material SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE MaterialId = @MaterialId  ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@MaterialId", DbType.Guid, courseId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 添加资料信息
        /// <summary>
        /// 添加资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <param name="attachment">资料附件信息</param>
        /// <returns>返回结果</returns>
        internal bool AddMaterial(Material model, string attachment)
        {
            bool result = true;
            using (var transaction = new BWDatabaseWithTransaction())
            {
                try
                {
                    result = this.Add(model, transaction);
                    if (result && !string.IsNullOrWhiteSpace(attachment))
                    {
                        DALMaterialAttachment dal = new DALMaterialAttachment();
                        result = dal.AddMaterialAttachment(attachment, transaction);
                    }
                    if (!result) transaction.Rollback();
                    else transaction.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    Logger.LogException(ex);
                    transaction.Rollback();
                }
            }
            return result;
        }
        #endregion

        #region 修改资料信息
        /// <summary>
        /// 修改资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <param name="attachment">资料附件信息</param>
        /// <returns>返回结果</returns>
        internal bool UpdateMaterial(Material model, string attachment)
        {
            bool result = true;
            using (var transaction = new BWDatabaseWithTransaction())
            {
                try
                {
                    result = this.Update(model, transaction);
                    if (result && !string.IsNullOrWhiteSpace(attachment))
                    {
                        DALMaterialAttachment dal = new DALMaterialAttachment();
                        result = dal.AddMaterialAttachment(attachment, transaction);
                    }
                    if (!result) transaction.Rollback();
                    else transaction.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    Logger.LogException(ex);
                    transaction.Rollback();
                }
            }
            return result;
        }
        #endregion

        #region 获取资料附件
        /// <summary>
        /// 获取资料附件
        /// </summary>
        /// <param name="materialId">附件ID</param>
        /// <returns>返回资料附件</returns>
        public List<MaterialAttachment> GetAttachmentList(Guid materialId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT AttachmentId,MaterialId,AttachmentUrl,[Type] AS TypeText,Name,Cdt FROM MaterialAttachment ");
            strSql.Append("WHERE MaterialId=@MaterialId AND IsDeleted=0 ORDER BY Cdt DESC");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@MaterialId", DbType.Guid, materialId);
                return db.ExecuteDataSet(dbCommand).ToList<MaterialAttachment>();
            }
        }
        #endregion
    }
}
