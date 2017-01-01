using BestWise.Common;
using BestWise.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestWise.Common.Client;
using System.Net.Http;
using BestWise.Common.Mvc;

namespace BestWise.YLS.Client
{
    /// <summary>
    /// 系统功能Client
    /// </summary>
    public class SysFunctionClient
    {
        #region 新增实体对象
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns></returns>
        public static async Task<ResultMessage> Add(Sys_Function model)
        {
            return await HttpClientHelper.RequestAsync<Sys_Function>(string.Concat(Configuration.SourceServerAddress, "api/UserFunction/AddFunction"), HttpMethod.Post, model);
        }
        #endregion

        #region 更新实体对象
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns></returns>
        public static async Task<ResultMessage> Update(Sys_Function model)
        {
            return await HttpClientHelper.RequestAsync<Sys_Function>(string.Concat(Configuration.SourceServerAddress, "api/UserFunction/UpdateFunction"), HttpMethod.Post, model);
        }
        #endregion

        #region 获取实体对象
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public static async Task<ResultMessage<Sys_Function>> GetModel(int Id)
        {
            return await HttpClientHelper.RequestAsync<Sys_Function>(string.Concat(Configuration.SourceServerAddress, "api/UserFunction/GetModel"), HttpMethod.Get, new { Id = Id });
        }
        #endregion

        #region 分页获取功能信息
        /// <summary>
        /// 分页获取功能信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>列表集合</returns>
        public static async Task<ResultMessage<PagedList<Sys_Function>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<Sys_Function>>(string.Concat(Configuration.SourceServerAddress, "api/UserFunction/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 获取功能列表信息
        /// <summary>
        /// 获取功能列表信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>列表集合</returns>
        public static async Task<ResultMessage<List<SysFunction>>> GetFunctionList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, List<SysFunction>>(string.Concat(Configuration.SourceServerAddress, "api/UserFunction/GetFunctionList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 删除功能点信息
        /// <summary>
        /// 删除功能点信息
        /// </summary>
        /// <param name="id">功能点ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> Delete(int id)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/UserFunction/Delete"), HttpMethod.Post, new { id = id });
        }
        #endregion
    }
}
