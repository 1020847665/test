using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.User.FrameWork.BLL;
using BestWise.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BestWise.YLS.ApiService.Controllers
{
    /// <summary>
    /// 用户相关接口
    /// </summary>
    [RoutePrefix("api/User")]
    [BWAuthorize]
    public class UserController : BaseApiController
    {
        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLogin">用户登录对象实例</param>
        /// <returns>返回登录结果</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("SignIn")]
        public async Task<ResultMessage<BaseUserWithToken>> SignInAsync(UserLogin userLogin)
        {
            return await BLLBaseUser.Instantiate.SignInAsync(userLogin);
        }
        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns>返回注销结果</returns>
        [AllowAnonymous]
        [Route("SignOut")]
        public ResultMessage SignOut()
        {
            return BLLBaseUser.Instantiate.SignOut();
        }
        #endregion

        #region 获取用户菜单信息
        /// <summary>
        /// 获取用户菜单信息
        /// </summary>
        /// <returns>列表集合</returns>
        [HttpPost]
        [Route("GetUserMenu")]
        public ResultMessage<List<UserMenu>> GetUserMenu()
        {
            return BLLFunction.Instantiate.GetUserMenu();
        }
        #endregion

        #region 添加用户信息
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>返回添加结果</returns>
        [HttpPost]
        [Route("AddUser")]
        public ResultMessage AddUser(Users model)
        {
            return BLLBaseUser.Instantiate.Add(model);
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="model">用户角色</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("UpdateUser")]
        public ResultMessage UpdateUser(Users model)
        {
            return BLLBaseUser.Instantiate.Update(model);
        }
        #endregion

        #region 删除用户信息
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        [Route("DeleteUserById")]
        public ResultMessage DeleteUserById(Guid userId)
        {
            return BLLBaseUser.Instantiate.Delete(userId);
        }
        #endregion

        #region 修改用户密码
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("UpdatePassword")]
        public ResultMessage UpdatePassword(string password, Guid? userId = null)
        {
            return BLLBaseUser.Instantiate.UpdatePassword(password, userId);
        }
        #endregion

        #region 分页获取用户信息
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>列表集合</returns>
        [HttpPost]
        [Route("GetPageList")]
        public ResultMessage<PagedList<Users>> GetPageList(BaseFilter baseFilter)
        {
            return BLLBaseUser.Instantiate.GetPageList(baseFilter);
        }
        #endregion

        #region  获取用户信息
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetModel")]
        public ResultMessage<Users> GetModel(Guid userId)
        {
            return BLLBaseUser.Instantiate.GetModel(userId);
        }
        #endregion

        #region 修改用户状态
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>返回修改结果</returns>
        [HttpPost]
        [Route("UpdateUserState")]
        public ResultMessage UpdateUserState(Users user)
        {
            return BLLBaseUser.Instantiate.UpdateUserState(user);
        }
        #endregion
    }
}
