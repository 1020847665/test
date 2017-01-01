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
    /// 学员预约数据访问层
    /// </summary>
    public class DALStudentBook
    {
        #region 根据预约ID获取预约信息
        /// <summary>
        /// 根据预约ID获取预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>实体对象</returns>
        internal StudentBook GetModel(Guid bookId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT BookId,BookName,UserName,Sex,Organization,Position,MobileNumber,Email,StartTime,EndTime,DealState,");
            strSql.Append("CASE DealState WHEN 0 THEN '未处理' WHEN 1 THEN '已处理' WHEN 2 THEN '已拒绝' END AS DealStateText,");
            strSql.Append("TrainOrganization,TrainAddress,TrainNumber,TrainNeeds,[Type],Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM StudentBook ");
            strSql.Append("WHERE BookId = @BookId");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@BookId", DbType.Guid, bookId);
                return db.ExecuteReader(dbCommand).FirstOrDefault<StudentBook>();
            }
        }
        #endregion

        #region 更新预约信息
        /// <summary>
        /// 更新预约信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回更新结果</returns>
        internal bool Update(StudentBook model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE StudentBook SET ");
            strSql.Append("BookName = @BookName,");
            strSql.Append("UserName = @UserName,");
            strSql.Append("Sex = @Sex,");
            strSql.Append("Organization = @Organization,");
            strSql.Append("Position = @Position,");
            strSql.Append("MobileNumber = @MobileNumber,");
            strSql.Append("Email = @Email,");
            strSql.Append("StartTime = @StartTime,");
            strSql.Append("EndTime = @EndTime,");
            strSql.Append("TrainOrganization = @TrainOrganization,");
            strSql.Append("TrainAddress = @TrainAddress,");
            strSql.Append("TrainNumber = @TrainNumber,");
            strSql.Append("TrainNeeds = @TrainNeeds,");
            strSql.Append("Type = @Type,");
            strSql.Append("Notes = @Notes,");
            strSql.Append("Mdt = @Mdt,");
            strSql.Append("Mby = @Mby ");
            strSql.Append("WHERE BookId = @BookId  ");
            var db = transaction != null ? transaction.Database : DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@BookId", DbType.Guid, model.BookId);
                db.AddInParameter(dbCommand, "@BookName", DbType.String, model.BookName);
                db.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                db.AddInParameter(dbCommand, "@Sex", DbType.Int32, model.Sex);
                db.AddInParameter(dbCommand, "@Organization", DbType.String, model.Organization);
                db.AddInParameter(dbCommand, "@Position", DbType.String, model.Position);
                db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, model.MobileNumber);
                db.AddInParameter(dbCommand, "@Email", DbType.String, model.Email);
                db.AddInParameter(dbCommand, "@StartTime", DbType.DateTime, model.StartTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, model.EndTime);
                db.AddInParameter(dbCommand, "@TrainOrganization", DbType.String, model.TrainOrganization);
                db.AddInParameter(dbCommand, "@TrainAddress", DbType.String, model.TrainAddress);
                db.AddInParameter(dbCommand, "@TrainNumber", DbType.String, model.TrainNumber);
                db.AddInParameter(dbCommand, "@TrainNeeds", DbType.String, model.TrainNeeds);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, model.Type);
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

        #region 新增预约信息
        /// <summary>
        /// 新增预约信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns>返回新增结果</returns>
        internal bool Add(StudentBook model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO StudentBook(");
            strSql.Append("BookId,");
            strSql.Append("BookName,");
            strSql.Append("UserName,");
            strSql.Append("Sex,");
            strSql.Append("Organization,");
            strSql.Append("Position,");
            strSql.Append("MobileNumber,");
            strSql.Append("Email,");
            strSql.Append("StartTime,");
            strSql.Append("EndTime,");
            strSql.Append("DealState,");
            strSql.Append("TrainOrganization,");
            strSql.Append("TrainAddress,");
            strSql.Append("TrainNumber,");
            strSql.Append("TrainNeeds,");
            strSql.Append("Type");
            strSql.Append("Notes,");
            strSql.Append("IsDeleted,");
            strSql.Append("Cdt,");
            strSql.Append("Cby,");
            strSql.Append("Mdt,");
            strSql.Append("Mby");
            strSql.Append(") VALUES (");
            strSql.Append("@BookId,");
            strSql.Append("@BookName,");
            strSql.Append("@UserName,");
            strSql.Append("@Sex,");
            strSql.Append("@Organization,");
            strSql.Append("@Position,");
            strSql.Append("@MobileNumber,");
            strSql.Append("@Email,");
            strSql.Append("@StartTime,");
            strSql.Append("@EndTime,");
            strSql.Append("@DealState,");
            strSql.Append("@TrainOrganization,");
            strSql.Append("@TrainAddress,");
            strSql.Append("@TrainNumber,");
            strSql.Append("@TrainNeeds,");
            strSql.Append("@Type");
            strSql.Append("@Notes,");
            strSql.Append("@IsDeleted,");
            strSql.Append("@Cdt,");
            strSql.Append("@Cby,");
            strSql.Append("@Mdt,");
            strSql.Append("@Mby)");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@BookId", DbType.Guid, Guid.NewGuid());
                db.AddInParameter(dbCommand, "@BookName", DbType.String, model.BookName);
                db.AddInParameter(dbCommand, "@UserName", DbType.String, model.UserName);
                db.AddInParameter(dbCommand, "@Sex", DbType.Int32, model.Sex);
                db.AddInParameter(dbCommand, "@Organization", DbType.String, model.Organization);
                db.AddInParameter(dbCommand, "@Position", DbType.String, model.Position);
                db.AddInParameter(dbCommand, "@MobileNumber", DbType.String, model.MobileNumber);
                db.AddInParameter(dbCommand, "@Email", DbType.String, model.Email);
                db.AddInParameter(dbCommand, "@StartTime", DbType.DateTime, model.StartTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, model.EndTime);
                db.AddInParameter(dbCommand, "@DealState", DbType.Int32, model.DealState);
                db.AddInParameter(dbCommand, "@TrainOrganization", DbType.String, model.TrainOrganization);
                db.AddInParameter(dbCommand, "@TrainAddress", DbType.String, model.TrainAddress);
                db.AddInParameter(dbCommand, "@TrainNumber", DbType.String, model.TrainNumber);
                db.AddInParameter(dbCommand, "@TrainNeeds", DbType.String, model.TrainNeeds);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, model.Type);
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

        #region 分页获取预约信息
        /// <summary>
        /// 分页获取预约信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>预约列表集合</returns>
        internal PagedList<StudentBook> GetPageList(BaseFilter baseFilter)
        {
            SearchCondition searchCondition = new SearchCondition(baseFilter.Condition, baseFilter.Sort);
            PagedList<StudentBook> pagedList = null;
            string sort = !string.IsNullOrWhiteSpace(searchCondition.GetSort()) ? searchCondition.GetSort() : "Cdt DESC ";
            string filter = string.Concat("WHERE IsDeleted=0 ", searchCondition.GetFilter());
            StringBuilder strsql = new StringBuilder();
            strsql.Append("SELECT COUNT(1) FROM StudentBook ");
            strsql.Append(filter);
            strsql.Append("SELECT BookId,BookName,UserName,Sex,Organization,Position,MobileNumber,Email,StartTime,EndTime,DealState,");
            strsql.Append("CASE DealState WHEN 0 THEN '未处理' WHEN 1 THEN '已处理' WHEN 2 THEN '已拒绝' END AS DealStateText,");
            strsql.Append("TrainOrganization,TrainAddress,TrainNumber,TrainNeeds,[Type],Notes,IsDeleted,Cdt,Cby,Mdt,Mby FROM StudentBook ");
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
                     ? new PagedList<StudentBook>(ds.Tables[1].ToList<StudentBook>(),
                         int.Parse(ds.Tables[0].Rows[0][0].ToString()), baseFilter.PageSize, baseFilter.PageIndex)
                     : null;
            }
            return pagedList;
        }
        #endregion

        #region 删除预约信息
        /// <summary>
        /// 删除预约信息
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid bookId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE StudentBook SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE BookId = @BookId ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@BookId", DbType.Guid, bookId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 预约状态处理
        /// <summary>
        /// 预约状态处理
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回处理结果</returns>
        public bool DealState(StudentBook model)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE StudentBook SET ");
            strSql.Append("DealState = @DealState, ");
            strSql.Append("Notes = @Notes ");
            strSql.Append("WHERE BookId = @BookId ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@BookId", DbType.Guid, model.BookId);
                db.AddInParameter(dbCommand, "@DealState", DbType.Int32, model.DealState);
                db.AddInParameter(dbCommand, "@Notes", DbType.String, model.Notes);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion
    }
}
