using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestWise.Common;
using BestWise.User.Model;
using BestWise.YLS.Client;

namespace BestWise.YLS.Manage.FrameWork
{
    /// <summary>
    /// 角色功能业务处理层
    /// </summary>
    public class BLLRoleFunction
    {
        private static BLLRoleFunction _instantion = null;
        public static BLLRoleFunction Instantion
        {
            get { return _instantion ?? (_instantion = new BLLRoleFunction()); }
        }

        #region 获取角色功能列表
        /// <summary>
        /// 获取角色功能列表
        /// </summary>
        /// <returns></returns>
        public async Task<ResultMessage<List<Sys_RoleFunction>>> GetList()
        {
            ResultMessage<List<Sys_RoleFunction>> result = new ResultMessage<List<Sys_RoleFunction>>();
            //缓存获取列表
            //if (CacheHelper.Exist("RoleFunction"))
            //    result = ResultMessage<List<Sys_RoleFunction>>.SucceedResult(CacheHelper.Retrieve<List<Sys_RoleFunction>>("RoleFunction"));
            //else
            //{
                //缓存不存在则进行查询
            result = await UsersClient.GetFunctionList();
            //    if (result.IsSucceed() && result.Data != null)
            //        CacheHelper.Store("RoleFunction", result.Data, TimeSpan.FromDays(1));  //加入缓存一天
            //}
            return result;
        }
        #endregion

        #region 根据角色获取功能【缓存获取】
        /// <summary>
        /// 根据角色获取功能【缓存获取】
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public async Task<ResultMessage<List<Sys_RoleFunction>>> GetList(int roleId)
        {
            ResultMessage<List<Sys_RoleFunction>> result = await GetList();
            if (result.IsSucceed() && result.Data != null)
                result.Data = result.Data.Where(o => o.RoleId == roleId).ToList();
            return result;
        }
        #endregion

    }

}

