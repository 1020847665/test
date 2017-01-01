using BestWise.Common;
using BestWise.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestWise.Common.Mvc;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SysWeb = System.Web;
using Microsoft.AspNet.Identity;
using System.Web;
using BestWise.YLS.Client;

namespace BestWise.YLS.Manage.FrameWork
{
    /// <summary>
    /// 系统用户业务逻辑层
    /// </summary>
    public class BLLUsers
    {
        private static BLLUsers _instantiate = null;
        /// <summary>
        /// 实例对象
        /// </summary>
        public static BLLUsers Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLUsers()); }
        }
        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLogin">用户登录对象实例</param>
        /// <returns>返回登录结果</returns>
        public async Task<ResultMessage> SignInAsync(UserLogin userLogin)
        {
            ResultMessage result = ResultMessage.FailureResult("登录失败");
            try
            {
                ResultMessage<BaseUserWithToken> resultUser = await UsersClient.SignInAsync(userLogin);
                if (resultUser != null && resultUser.IsSucceed())
                {
                    SignInAsync(resultUser.Data, true);
                    result = ResultMessage.SucceedResult("登录成功");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }
        #endregion

        #region 增加用户信息
        /// <summary>
        /// 增加用户信息
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>返回增加用户信息结果</returns>
        public async Task<ResultMessage> Add(Users model)
        {
            return await UsersClient.Add(model);
        }
        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns>返回注销结果</returns>
        public async Task<ResultMessage> SignOut()
        {
            return await UsersClient.SignOut();
        }
        #endregion

        #region 获取用户菜单信息
        /// <summary>
        /// 获取用户菜单信息
        /// </summary>
        /// <returns>返回用户菜单信息</returns>
        public async Task<ResultMessage<List<UserMenu>>> GetUserMenu()
        {
            ResultMessage<List<UserMenu>> reslut = new ResultMessage<List<UserMenu>>();
            //if (CacheHelper.Exist("sysUserMenu"))
            //    reslut = ResultMessage<List<UserMenu>>.SucceedResult(CacheHelper.Retrieve<List<UserMenu>>("sysUserMenu"));
            //else
            //{
            reslut = await UsersClient.GetUserMenu();
            //    if (reslut.IsSucceed() && reslut.Data != null && reslut.Data.Count > 0)
            //        CacheHelper.Store("sysUserMenu", reslut.Data, TimeSpan.FromDays(1));
            //}
            return reslut;
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>返回修改用户信息结果</returns>
        public async Task<ResultMessage> UpdateUser(Users model)
        {
            return await UsersClient.UpdateUser(model);
        }
        #endregion

        #region 删除用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回删除用户信息结果</returns>
        public async Task<ResultMessage> DeleteUserById(Guid userId)
        {
            return await UsersClient.DeleteUserById(userId);
        }
        #endregion

        #region 修改用户密码
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>返回修改用户密码结果</returns>
        public async Task<ResultMessage> UpdatePassword(string password, Guid? userId = null)
        {
            return await UsersClient.UpdatePassword(password, userId);
        }
        #endregion

        #region 分页获取用户信息
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>返回用户信息</returns>
        public async Task<ResultMessage<PagedList<Users>>> GetPageList(BaseFilter baseFilter)
        {
            return await UsersClient.GetPageList(baseFilter);
        }
        #endregion

        #region 修改用户状态
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>返回修改结果</returns>
        public async Task<ResultMessage> UpdateUserState(Users model)
        {
            return await UsersClient.UpdateUserState(model);
        }
        #endregion

        #region  获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<ResultMessage<Users>> GetModel(Guid userId)
        {
            return await UsersClient.GetModel(userId);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 异步登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="isPersistent"></param>
        private void SignInAsync(BaseUserWithToken user, bool isPersistent)
        {
            AuthenticationManager.SignOut(ConstantData.AuthenticationType);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, CreateMemberIdentity(user));
        }

        /// <summary>
        /// 创建成员身份
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        private ClaimsIdentity CreateMemberIdentity(BaseUserWithToken user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(ConstantData.AuthenticationType, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString(), ClaimValueTypes.String));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString(), ClaimValueTypes.String));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName ?? String.Empty, ClaimValueTypes.String));
            identity.AddClaim(new Claim(BestWiseClaimTypes.NickName, user.NickName ?? String.Empty, ClaimValueTypes.String));
            identity.AddClaim(new Claim(BestWiseClaimTypes.AuthToken, String.Concat(user.TokenType, " ", user.AccessToken), ClaimValueTypes.String));
            identity.AddClaim(new Claim(BestWiseClaimTypes.IdentityProvider, BestWiseClaimTypes.DefaultIdentityProvider));
            return identity;
        }

        /// <summary>
        /// OWIN 上下文。
        /// </summary>
        private IOwinContext OwinContext { get { return HttpContext.GetOwinContext(); } }
        private IAuthenticationManager AuthenticationManager { get { return OwinContext.Authentication; } }
        /// <summary>
        /// 当前请求上下文。
        /// </summary>
        private SysWeb.HttpContextBase HttpContext { get { return new SysWeb.HttpContextWrapper(SysWeb.HttpContext.Current); } }
        #endregion

    }
}
