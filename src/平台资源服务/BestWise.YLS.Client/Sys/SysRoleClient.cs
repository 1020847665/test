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
    /// 系统角色Client
    /// </summary>
    public class SysRoleClient
    {
        #region 新增实体对象
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns>返回新增结果</returns>
        public static async Task<ResultMessage> Add(Sys_Roles model)
        {
            return await HttpClientHelper.RequestAsync<Sys_Roles>(string.Concat(Configuration.SourceServerAddress, "api/UserRole/AddRole"), HttpMethod.Post, model);
        }
        #endregion

        #region 更新实体对象
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns>返回修改结果</returns>
        public static async Task<ResultMessage> Update(Sys_Roles model)
        {
            return await HttpClientHelper.RequestAsync<Sys_Roles>(string.Concat(Configuration.SourceServerAddress, "api/UserRole/UpdateRole"), HttpMethod.Post, model);
        }
        #endregion

        #region 获取实体对象
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public static async Task<ResultMessage<Sys_Roles>> GetModel(int roleId)
        {
            return await HttpClientHelper.RequestAsync<Sys_Roles>(string.Concat(Configuration.SourceServerAddress, "api/UserRole/GetModel"), HttpMethod.Get, new { roleId = roleId });
        }
        #endregion

        #region 分页获取角色信息
        /// <summary>
        /// 分页获取角色信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>列表集合</returns>
        public static async Task<ResultMessage<PagedList<Sys_Roles>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<Sys_Roles>>(string.Concat(Configuration.SourceServerAddress, "api/UserRole/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region 删除角色信息
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回删除结果</returns>
        public static async Task<ResultMessage> Delete(int roleId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/UserRole/DeleteRoleById"), HttpMethod.Post, new { roleId= roleId});
        }
        #endregion

        #region 添加角色功能 
        /// <summary>
        /// 添加角色功能
        /// </summary>
        /// <param name="list">角色功能</param>
        /// <returns>返回添加结果</returns>
        public static async Task<ResultMessage> AddRoleFunction(List<RoleFunction> list)
        {
            return await HttpClientHelper.RequestAsync<List<RoleFunction>>(string.Concat(Configuration.SourceServerAddress, "api/UserRole/AddRoleFunction"), HttpMethod.Post, list);
        }
        #endregion
    }
}
