using BestWise.Common;
using BestWise.Common.Client;
using BestWise.Common.Mvc;
using BestWise.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Client
{
    /// <summary>
    /// 用户Client
    /// </summary>
    public class UsersClient
    {
        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLogin">用户登录对象实例</param>
        /// <returns>返回登录结果</returns>
        public static async Task<ResultMessage<BaseUserWithToken>> SignInAsync(UserLogin userLogin)
        {
            return await HttpClientHelper.RequestAsync<UserLogin, BaseUserWithToken>(string.Concat(Configuration.SourceServerAddress, "api/User/SignIn"), HttpMethod.Post, userLogin);
        }
        #endregion

        #region 增加用户信息
        /// <summary>
        /// 增加用户信息
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>返回增加用户信息结果</returns>
        public static async Task<ResultMessage> Add(Users model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/User/AddUser"), HttpMethod.Post, model);
        }
        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns>返回注销结果</returns>
        public static async Task<ResultMessage> SignOut()
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/User/SignOut"), HttpMethod.Post, (object)null);
        }
        #endregion

        #region 获取用户菜单信息
        /// <summary>
        /// 获取用户菜单信息
        /// </summary>
        /// <returns>返回用户菜单信息</returns>
        public static async Task<ResultMessage<List<UserMenu>>> GetUserMenu()
        {
            return await HttpClientHelper.RequestAsync<List<UserMenu>>(string.Concat(Configuration.SourceServerAddress, "api/User/GetUserMenu"), HttpMethod.Post, (object)null);
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>返回修改用户信息结果</returns>
        public static async Task<ResultMessage> UpdateUser(Users model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/User/UpdateUser"), HttpMethod.Post, model);
        }
        #endregion

        #region 删除用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回删除用户信息结果</returns>
        public static async Task<ResultMessage> DeleteUserById(Guid userId)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/User/DeleteUserById"), HttpMethod.Post, new { userId = userId });
        }
        #endregion

        #region 修改用户密码
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>返回修改用户密码结果</returns>
        public static async Task<ResultMessage> UpdatePassword(string password, Guid? userId = null)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/User/UpdatePassword"), HttpMethod.Post, new { password = password, userId = userId });
        }
        #endregion

        #region 分页获取用户信息
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>返回用户信息</returns>
        public static async Task<ResultMessage<PagedList<Users>>> GetPageList(BaseFilter baseFilter)
        {
            return await HttpClientHelper.RequestAsync<BaseFilter, PagedList<Users>>(string.Concat(Configuration.SourceServerAddress, "api/User/GetPageList"), HttpMethod.Post, baseFilter);
        }
        #endregion

        #region  获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>返回用户信息</returns>

        public static async Task<ResultMessage<Users>> GetModel(Guid userId)
        {
            return await HttpClientHelper.RequestAsync<Users>(string.Concat(Configuration.SourceServerAddress, "api/User/GetModel"), HttpMethod.Get, new { userId = userId });
        }
        #endregion

        #region 获取角色功能列表
        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <returns>返回角色功能列表</returns>
        public static async Task<ResultMessage<List<Sys_RoleFunction>>> GetFunctionList()
        {
            return await HttpClientHelper.RequestAsync<List<Sys_RoleFunction>>(string.Concat(Configuration.SourceServerAddress, "api/UserRole/GetFunctionList"), HttpMethod.Get, (object)null);
        }
        #endregion

        #region 修改用户状态
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>返回修改结果</returns>
        public static async Task<ResultMessage> UpdateUserState(Users model)
        {
            return await HttpClientHelper.RequestAsync(string.Concat(Configuration.SourceServerAddress, "api/User/UpdateUserState"), HttpMethod.Post, model);
        }
        #endregion
    }
}
