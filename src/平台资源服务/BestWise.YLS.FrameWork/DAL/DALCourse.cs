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
    /// 课程信息数据访问层
    /// </summary>
    public class DALCourse
    {
        #region 根据课程ID获取课程信息
        /// <summary>
        /// 根据课程ID获取课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>实体对象</returns>
        internal Course GetModel(Guid courseId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT CourseId,Name,Introduce,Body,LogoUrl,IsOrder,Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM Course ");
            strSql.Append("WHERE CourseId = @CourseId");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@CourseId", DbType.Guid, courseId);
                return db.ExecuteReader(dbCommand).FirstOrDefault<Course>();
            }
        }
        #endregion

        #region 更新课程信息
        /// <summary>
        /// 更新课程信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回更新结果</returns>
        internal bool Update(Course model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Course SET ");
            strSql.Append("Name = @Name,");
            strSql.Append("Introduce = @Introduce,");
            strSql.Append("Body = @Body,");
            strSql.Append("LogoUrl = @LogoUrl,");
            strSql.Append("IsOrder = @IsOrder,");
            strSql.Append("Notes = @Notes,");
            strSql.Append("Mdt = @Mdt,");
            strSql.Append("Mby = @Mby ");
            strSql.Append("WHERE CourseId = @CourseId  ");
            var db = transaction != null ? transaction.Database : DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@CourseId", DbType.Guid, model.CourseId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String, model.Introduce);
                db.AddInParameter(dbCommand, "@Body", DbType.String, model.Body);
                db.AddInParameter(dbCommand, "@LogoUrl", DbType.String, model.LogoUrl);
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

        #region 新增课程信息
        /// <summary>
        /// 新增课程信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回新增结果</returns>
        internal bool Add(Course model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO Course(");
            strSql.Append("CourseId,");
            strSql.Append("Name,");
            strSql.Append("Introduce,");
            strSql.Append("Body,");
            strSql.Append("LogoUrl,");
            strSql.Append("IsOrder,");
            strSql.Append("Notes,");
            strSql.Append("IsDeleted,");
            strSql.Append("Cdt,");
            strSql.Append("Cby,");
            strSql.Append("Mdt,");
            strSql.Append("Mby");
            strSql.Append(") VALUES (");
            strSql.Append("@CourseId,");
            strSql.Append("@Name,");
            strSql.Append("@Introduce,");
            strSql.Append("@Body,");
            strSql.Append("@LogoUrl,");
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
                db.AddInParameter(dbCommand, "@CourseId", DbType.Guid, model.CourseId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String, model.Introduce);
                db.AddInParameter(dbCommand, "@Body", DbType.String, model.Body);
                db.AddInParameter(dbCommand, "@LogoUrl", DbType.String, model.LogoUrl);
                db.AddInParameter(dbCommand, "@IsOrder", DbType.String, model.IsOrder);
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

        #region 分页获取课程信息
        /// <summary>
        /// 分页获取课程信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>课程列表集合</returns>
        internal PagedList<Course> GetPageList(BaseFilter baseFilter)
        {
            SearchCondition searchCondition = new SearchCondition(baseFilter.Condition, baseFilter.Sort);
            PagedList<Course> pagedList = null;
            string sort = !string.IsNullOrWhiteSpace(searchCondition.GetSort()) ? searchCondition.GetSort() : "Cdt DESC ";
            string filter = string.Concat("WHERE IsDeleted=0 ", searchCondition.GetFilter());
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT COUNT(1) FROM Teacher ");
            strsql.Append(filter);
            strsql.Append("SELECT CourseId,Name,Introduce,Body,LogoUrl,IsOrder,Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM Course ");
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
                     ? new PagedList<Course>(ds.Tables[1].ToList<Course>(),
                         int.Parse(ds.Tables[0].Rows[0][0].ToString()), baseFilter.PageSize, baseFilter.PageIndex)
                     : null;
            }
            return pagedList;
        }
        #endregion

        #region 删除课程信息
        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="courseId">课程ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid courseId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Course SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE CourseId = @CourseId  ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@CourseId", DbType.Guid, courseId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion
    }
}
