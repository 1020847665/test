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
    /// 培训班数据访问层
    /// </summary>
    public class DALTrainClass
    {
        #region 根据培训班ID获取培训班信息
        /// <summary>
        /// 根据培训班ID获取培训班信息
        /// </summary>
        /// <param name="trainId">主键</param>
        /// <returns>实体对象</returns>
        internal TrainClass GetModel(Guid trainId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TrainId,Name,Introduce,Body,TrainNeeds,LogoUrl,[Target],Number,");
            strSql.Append("[Address],StartTime,EndTime,DeadLineTime,NumberPrefix,[State],[Type],Teachers,");
            strSql.Append("IsOrder,CodeUrl,EnterURL,ActualEnterNumber,ActualReportNumber,IsStop,Notes,IsDeleted,Cdt,Cby,Mdt,Mby ");
            strSql.Append("FROM TrainClass");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TrainId", DbType.Guid, trainId);
                return db.ExecuteReader(dbCommand).FirstOrDefault<TrainClass>();
            }
        }
        #endregion

        #region 更新培训班信息
        /// <summary>
        /// 更新培训班信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns></returns>
        internal bool Update(TrainClass model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE TrainClass SET ");
            strSql.Append("Name = @Name,");
            strSql.Append("Introduce = @Introduce,");
            strSql.Append("Body = @Body,");
            strSql.Append("TrainNeeds = @TrainNeeds,");
            strSql.Append("LogoUrl = @LogoUrl,");
            strSql.Append("Target = @Target,");
            strSql.Append("Number = @Number,");
            strSql.Append("Address = @Address,");
            strSql.Append("StartTime = @StartTime,");
            strSql.Append("EndTime = @EndTime,");
            strSql.Append("DeadLineTime = @DeadLineTime,");
            strSql.Append("NumberPrefix = @NumberPrefix,");
            strSql.Append("State = @State,");
            strSql.Append("Type = @Type,");
            strSql.Append("Teachers = @Teachers,");
            strSql.Append("IsOrder = @IsOrder,");
            strSql.Append("CodeUrl = @CodeUrl,");
            strSql.Append("EnterURL = @EnterURL,");
            strSql.Append("ActualEnterNumber = @ActualEnterNumber,");
            strSql.Append("ActualReportNumber = @ActualReportNumber,");
            strSql.Append("IsStop = @IsStop,");
            strSql.Append("Notes = @Notes,");
            strSql.Append("Mdt = @Mdt,");
            strSql.Append("Mby = @Mby ");
            strSql.Append("WHERE TrainId = @TrainId  ");
            var db = transaction != null ? transaction.Database : DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TrainId", DbType.Guid, model.TrainId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String, model.Introduce);
                db.AddInParameter(dbCommand, "@Body", DbType.String, model.Body);
                db.AddInParameter(dbCommand, "@TrainNeeds", DbType.String, model.TrainNeeds);
                db.AddInParameter(dbCommand, "@LogoUrl", DbType.String, model.LogoUrl);
                db.AddInParameter(dbCommand, "@Target", DbType.String, model.Target);
                db.AddInParameter(dbCommand, "@Number", DbType.Int32, model.Number);
                db.AddInParameter(dbCommand, "@Address", DbType.String, model.Address);
                db.AddInParameter(dbCommand, "@StartTime", DbType.DateTime, model.StartTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, model.EndTime);
                db.AddInParameter(dbCommand, "@DeadLineTime", DbType.DateTime, model.DeadLineTime);
                db.AddInParameter(dbCommand, "@NumberPrefix", DbType.String, model.NumberPrefix);
                db.AddInParameter(dbCommand, "@State", DbType.Int32, model.State);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, model.Type);
                db.AddInParameter(dbCommand, "@Teachers", DbType.String, model.Teachers);
                db.AddInParameter(dbCommand, "@IsOrder", DbType.Boolean, model.IsOrder);
                db.AddInParameter(dbCommand, "@CodeUrl", DbType.String, model.CodeUrl);
                db.AddInParameter(dbCommand, "@EnterURL", DbType.String, model.EnterURL);
                db.AddInParameter(dbCommand, "@ActualEnterNumber", DbType.Int32, model.ActualEnterNumber);
                db.AddInParameter(dbCommand, "@ActualReportNumber", DbType.Int32, model.ActualReportNumber);
                db.AddInParameter(dbCommand, "@IsStop", DbType.Boolean, model.IsStop);
                db.AddInParameter(dbCommand, "@Notes", DbType.String, model.Notes);
                db.AddInParameter(dbCommand, "@Mdt", DbType.DateTime, model.Mdt);
                db.AddInParameter(dbCommand, "@Mby", DbType.Guid, model.Mby);
                result = transaction != null
                 ? db.ExecuteNonQuery(dbCommand, transaction.Transaction) > 0
                 : db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 新增培训班信息
        /// <summary>
        /// 新增培训班信息
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">是否启用事务</param>
        /// <returns></returns>
        internal bool Add(TrainClass model, BWDatabaseWithTransaction transaction = null)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO TrainClass(");
            strSql.Append("Name,");
            strSql.Append("Introduce,");
            strSql.Append("Body,");
            strSql.Append("TrainNeeds,");
            strSql.Append("LogoUrl,");
            strSql.Append("Target,");
            strSql.Append("Number,");
            strSql.Append("Address,");
            strSql.Append("StartTime,");
            strSql.Append("EndTime,");
            strSql.Append("DeadLineTime,");
            strSql.Append("NumberPrefix,");
            strSql.Append("State,");
            strSql.Append("Type,");
            strSql.Append("Teachers,");
            strSql.Append("IsOrder,");
            strSql.Append("CodeUrl,");
            strSql.Append("EnterURL,");
            strSql.Append("ActualEnterNumber,");
            strSql.Append("ActualReportNumber,");
            strSql.Append("IsStop,");
            strSql.Append("Notes,");
            strSql.Append("IsDeleted,");
            strSql.Append("Cdt,");
            strSql.Append("Cby,");
            strSql.Append("Mdt,");
            strSql.Append("Mby");
            strSql.Append(") VALUES (");
            strSql.Append("@Name,");
            strSql.Append("@Introduce,");
            strSql.Append("@Body,");
            strSql.Append("@TrainNeeds,");
            strSql.Append("@LogoUrl,");
            strSql.Append("@Target,");
            strSql.Append("@Number,");
            strSql.Append("@Address,");
            strSql.Append("@StartTime,");
            strSql.Append("@EndTime,");
            strSql.Append("@DeadLineTime,");
            strSql.Append("@NumberPrefix,");
            strSql.Append("@State,");
            strSql.Append("@Type,");
            strSql.Append("@Teachers,");
            strSql.Append("@IsOrder,");
            strSql.Append("@CodeUrl,");
            strSql.Append("@EnterURL,");
            strSql.Append("@ActualEnterNumber,");
            strSql.Append("@ActualReportNumber,");
            strSql.Append("@IsStop,");
            strSql.Append("@Notes,");
            strSql.Append("@IsDeleted,");
            strSql.Append("@Cdt,");
            strSql.Append("@Cby,");
            strSql.Append("@Mdt,");
            strSql.Append("@Mby");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TrainId", DbType.Guid, model.TrainId);
                db.AddInParameter(dbCommand, "@Name", DbType.String, model.Name);
                db.AddInParameter(dbCommand, "@Introduce", DbType.String, model.Introduce);
                db.AddInParameter(dbCommand, "@Body", DbType.String, model.Body);
                db.AddInParameter(dbCommand, "@TrainNeeds", DbType.String, model.TrainNeeds);
                db.AddInParameter(dbCommand, "@LogoUrl", DbType.String, model.LogoUrl);
                db.AddInParameter(dbCommand, "@Target", DbType.String, model.Target);
                db.AddInParameter(dbCommand, "@Number", DbType.Int32, model.Number);
                db.AddInParameter(dbCommand, "@Address", DbType.String, model.Address);
                db.AddInParameter(dbCommand, "@StartTime", DbType.DateTime, model.StartTime);
                db.AddInParameter(dbCommand, "@EndTime", DbType.DateTime, model.EndTime);
                db.AddInParameter(dbCommand, "@DeadLineTime", DbType.DateTime, model.DeadLineTime);
                db.AddInParameter(dbCommand, "@NumberPrefix", DbType.String, model.NumberPrefix);
                db.AddInParameter(dbCommand, "@State", DbType.Int32, model.State);
                db.AddInParameter(dbCommand, "@Type", DbType.Int32, model.Type);
                db.AddInParameter(dbCommand, "@Teachers", DbType.String, model.Teachers);
                db.AddInParameter(dbCommand, "@IsOrder", DbType.Boolean, model.IsOrder);
                db.AddInParameter(dbCommand, "@CodeUrl", DbType.String, model.CodeUrl);
                db.AddInParameter(dbCommand, "@EnterURL", DbType.String, model.EnterURL);
                db.AddInParameter(dbCommand, "@ActualEnterNumber", DbType.Int32, model.ActualEnterNumber);
                db.AddInParameter(dbCommand, "@ActualReportNumber", DbType.Int32, model.ActualReportNumber);
                db.AddInParameter(dbCommand, "@IsStop", DbType.Boolean, model.IsStop);
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

        #region 分页获取培训班信息
        /// <summary>
        /// 分页获取培训班信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>培训班列表集合</returns>
        internal PagedList<TrainClass> GetPageList(BaseFilter baseFilter)
        {
            SearchCondition searchCondition = new SearchCondition(baseFilter.Condition, baseFilter.Sort);
            PagedList<TrainClass> pagedList = null;
            string sort = !string.IsNullOrWhiteSpace(searchCondition.GetSort()) ? searchCondition.GetSort() : "Cdt DESC ";
            string filter = string.Concat("WHERE IsDeleted=0 ", searchCondition.GetFilter());
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(1) FROM TrainClass ");
            strSql.Append(filter);
            strSql.Append("SELECT TrainId,Name,Introduce,Body,TrainNeeds,LogoUrl,[Target],Number,");
            strSql.Append("[Address],StartTime,EndTime,DeadLineTime,NumberPrefix,[State],[Type],Teachers,");
            strSql.Append("IsOrder,CodeUrl,EnterURL,ActualEnterNumber,ActualReportNumber,IsStop,Notes,IsDeleted,Cdt,Cby,Mdt,Mby ");
            strSql.Append("FROM TrainClass ");
            strSql.Append(filter);
            strSql.AppendFormat("ORDER BY {0} ", sort);
            if (baseFilter.IsPage) strSql.Append("OFFSET @PageIndex ROWS FETCH NEXT @PageSize ROWS ONLY ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                if (baseFilter.IsPage)
                {
                    db.AddInParameter(dbCommand, "@PageIndex", DbType.Int32, (baseFilter.PageIndex - 1) * baseFilter.PageSize);
                    db.AddInParameter(dbCommand, "@PageSize", DbType.Int32, baseFilter.PageSize);
                }
                var ds = db.ExecuteDataSet(dbCommand);
                pagedList = ds != null && ds.Tables[0] != null && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0
                     ? new PagedList<TrainClass>(ds.Tables[1].ToList<TrainClass>(),
                         int.Parse(ds.Tables[0].Rows[0][0].ToString()), baseFilter.PageSize, baseFilter.PageIndex)
                     : null;
            }
            return pagedList;
        }
        #endregion

        #region 删除培训班信息
        /// <summary>
        /// 删除培训班信息
        /// </summary>
        /// <param name="trainId">培训班ID</param>
        /// <returns>返回删除结果</returns>
        public bool Delete(Guid trainId)
        {
            bool result;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Teacher SET ");
            strSql.Append("IsDeleted = 1 ");
            strSql.Append("WHERE TeacherId = @TeacherId  ");
            var db = DatabaseFactory.CreateDatabase();
            using (var dbCommand = db.GetSqlStringCommand(strSql.ToString()))
            {
                db.AddInParameter(dbCommand, "@TeacherId", DbType.Guid, trainId);
                result = db.ExecuteNonQuery(dbCommand) > 0;
            }
            return result;
        }
        #endregion

        #region 添加培训班信息
        /// <summary>
        /// 添加培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <param name="contacts">联系人信息</param>
        /// <returns>返回结果</returns>
        internal bool AddTrainClass(TrainClass model, string contacts)
        {
            bool result = true;
            using (var transaction = new BWDatabaseWithTransaction())
            {
                try
                {
                    result = this.Add(model, transaction);
                    if (result && !string.IsNullOrWhiteSpace(contacts))
                    {
                        DALTrainContact dal = new DALTrainContact();
                        result = dal.AddTrainContact(contacts, transaction);
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

        #region 修改培训班信息
        /// <summary>
        /// 修改培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <param name="contacts">联系人信息</param>
        /// <returns>返回结果</returns>
        internal bool UpdateTrainClass(TrainClass model, string contacts)
        {
            bool result = true;
            using (var transaction = new BWDatabaseWithTransaction())
            {
                try
                {
                    result = this.Update(model, transaction);
                    if (result && !string.IsNullOrWhiteSpace(contacts))
                    {
                        DALTrainContact dal = new DALTrainContact();
                        result = dal.AddTrainContact(contacts, transaction);
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
