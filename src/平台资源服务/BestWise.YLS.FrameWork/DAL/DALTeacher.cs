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
    /// 教师数据访问层
    /// </summary>
    public class DALTeacher
    {
        #region 根据教师ID获取教师信息
        /// <summary>
        /// 根据教师ID获取教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>实体对象</returns>
        internal Teacher GetModel(Guid teacherId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TeacherId,Name,Introduce,Organization,MobileNumber,Email,Position,");
            strSql.Append("Course,[Type] AS TypeText,IsOrder,Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM Teacher ");
            strSql.Append("WHERE TeacherId = @TeacherId");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TeacherId", DbType.Guid, teacherId);
                return db.ExecuteReader(dbCommand).FirstOrDefault<Teacher>();
            }
        }
        #endregion

        #region 更新教师信息
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回更新结果</returns>
        internal bool Update(Teacher model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Teacher SET ");
            strSql.Append("Name = @Name,");
            strSql.Append("Introduce = @Introduce,");
            strSql.Append("Organization = @Organization,");
            strSql.Append("MobileNumber = @MobileNumber,");
            strSql.Append("Email = @Email,");
            strSql.Append("Position = @Position,");
            strSql.Append("Course = @Course,");
            strSql.Append("Type = @Type,");
            strSql.Append("IsOrder = @IsOrder,");
            strSql.Append("Notes = @Notes,");
            strSql.Append("Mdt = @Mdt,");
            strSql.Append("Mby = @Mby ");
            strSql.Append("WHERE TeacherId = @TeacherId  ");
            var db = transaction != null ? transaction.Database : DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TeacherId", DbType.Guid, model.TeacherId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String, model.Introduce);
                db.AddInParameter(dbCommand, "@Organization", DbType.String, model.Organization);
                db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, model.MobileNumber);
                db.AddInParameter(dbCommand, "@Email", DbType.String, model.Email);
                db.AddInParameter(dbCommand, "@Position", DbType.String, model.Position);
                db.AddInParameter(dbCommand, "@Course", DbType.String, model.Course);
                db.AddInParameter(dbCommand, "@Type", DbType.String, model.Type);
                db.AddInParameter(dbCommand, "@IsOrder", DbType.Boolean, model.IsOrder);
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

        #region 新增教师信息
        /// <summary>
        /// 新增教师信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回新增结果</returns>
        internal bool Add(Teacher model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Teacher(");
            strSql.Append("TeacherId,");
            strSql.Append("Name,");
            strSql.Append("Introduce,");
            strSql.Append("Organization,");
            strSql.Append("MobileNumber,");
            strSql.Append("Email,");
            strSql.Append("Position,");
            strSql.Append("Course,");
            strSql.Append("Type,");
            strSql.Append("IsOrder,");
            strSql.Append("Notes,");
            strSql.Append("IsDeleted,");
            strSql.Append("Cdt,");
            strSql.Append("Cby,");
            strSql.Append("Mdt,");
            strSql.Append("Mby");
            strSql.Append(") VALUES (");
            strSql.Append("@TeacherId,");
            strSql.Append("@Name,");
            strSql.Append("@Introduce,");
            strSql.Append("@Organization,");
            strSql.Append("@MobileNumber,");
            strSql.Append("@Email,");
            strSql.Append("@Position,");
            strSql.Append("@Course,");
            strSql.Append("@Type,");
            strSql.Append("@IsOrder,");
            strSql.Append("@Notes,");
            strSql.Append("@IsDeleted,");
            strSql.Append("@Cdt,");
            strSql.Append("@Cby,");
            strSql.Append("@Mdt,");
            strSql.Append("@Mby)");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TeacherId", DbType.Guid, model.TeacherId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String, model.Introduce);
                db.AddInParameter(dbCommand, "@Organization", DbType.String, model.Organization);
                db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, model.MobileNumber);
                db.AddInParameter(dbCommand, "@Email", DbType.String, model.Email);
                db.AddInParameter(dbCommand, "@Position", DbType.String, model.Position);
                db.AddInParameter(dbCommand, "@Course", DbType.String, model.Course);
                db.AddInParameter(dbCommand, "@Type", DbType.String, model.Type);
                db.AddInParameter(dbCommand, "@IsOrder", DbType.Boolean, model.IsOrder);
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

        #region 分页获取教师信息
        /// <summary>
        /// 分页获取教师信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>教师列表集合</returns>
        internal PagedList<Teacher> GetPageList(BaseFilter baseFilter)
        {
            SearchCondition searchCondition = new SearchCondition(baseFilter.Condition, baseFilter.Sort);
            PagedList<Teacher> pagedList = null;
            string sort = !string.IsNullOrWhiteSpace(searchCondition.GetSort()) ? searchCondition.GetSort() : "Cdt DESC ";
            string filter = string.Concat("WHERE IsDeleted=0 ", searchCondition.GetFilter());
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT COUNT(1) FROM Teacher ");
            strsql.Append(filter);
            strsql.Append("SELECT TeacherId,Name,Introduce,Organization,MobileNumber,Email,Position,");
            strsql.Append("Course,[Type] AS TypeText,IsOrder,Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM Teacher ");
            strsql.Append(filter);
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
                     ? new PagedList<Teacher>(ds.Tables[1].ToList<Teacher>(),
                         int.Parse(ds.Tables[0].Rows[0][0].ToString()), baseFilter.PageSize, baseFilter.PageIndex)
                     : null;
            }
            return pagedList;
        }
        #endregion

        #region 删除教师信息
        /// <summary>
        /// 删除教师信息
        /// </summary>
        /// <param name="teacherId">教师ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid teacherId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Teacher SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE TeacherId = @TeacherId  ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TeacherId", DbType.Guid, teacherId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 添加教师信息
        /// <summary>
        /// 添加教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <param name="pictures">教师图片信息</param>
        /// <returns>返回结果</returns>
        internal bool AddTeacher(Teacher model, string pictures)
        {
            bool result = true;
            using (var transaction = new BWDatabaseWithTransaction())
            {
                try
                {
                    result = this.Add(model, transaction);
                    if (result && !string.IsNullOrWhiteSpace(pictures))
                    {
                        DALTeacherPicture dal = new DALTeacherPicture();
                        result = dal.AddTeacherPicture(pictures, transaction);
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

        #region 修改教师信息
        /// <summary>
        /// 修改教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <param name="pictures">教师图片信息</param>
        /// <returns>返回结果</returns>
        internal bool UpdateTeacher(Teacher model, string pictures)
        {
            bool result = true;
            using (var transaction = new BWDatabaseWithTransaction())
            {
                try
                {
                    result = this.Update(model, transaction);
                    if (result && !string.IsNullOrWhiteSpace(pictures))
                    {
                        DALTeacherPicture dal = new DALTeacherPicture();
                        result = dal.AddTeacherPicture(pictures, transaction);
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
    }
}
