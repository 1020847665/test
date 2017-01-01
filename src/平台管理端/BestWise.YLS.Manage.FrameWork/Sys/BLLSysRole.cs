using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.User.Model;
using BestWise.YLS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Manage.FrameWork
{
    /// <summary>
    /// 系统角色业务处理层
    /// </summary>
    public class BLLSysRole
    {
        private static BLLSysRole _instantiatie = null;
        public static BLLSysRole Instantiatie
        {
            get { return _instantiatie ?? (_instantiatie = new BLLSysRole()); }
        }

        #region 新增实体对象
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns>返回新增结果</returns>
        public async Task<ResultMessage> Add(Sys_Roles model)
        {
            return await SysRoleClient.Add(model);
        }
        #endregion

        #region 更新实体对象
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns>返回修改结果</returns>
        public async Task<ResultMessage> Update(Sys_Roles model)
        {
            return await SysRoleClient.Update(model);
        }
        #endregion

        #region 获取实体对象
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public async Task<ResultMessage<Sys_Roles>> GetModel(int roleId)
        {
            return await SysRoleClient.GetModel(roleId);
        }
        #endregion

        #region 分页获取角色信息
        /// <summary>
        /// 分页获取角色信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>列表集合</returns>
        public async Task<ResultMessage<PagedList<Sys_Roles>>> GetPageList(BaseFilter baseFilter)
        {
            return await SysRoleClient.GetPageList(baseFilter);
        }
        #endregion

        #region 删除角色信息
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> Delete(int roleId)
        {
            return await SysRoleClient.Delete(roleId);
        }
        #endregion

        #region 添加角色功能 
        /// <summary>
        /// 添加角色功能
        /// </summary>
        /// <param name="list">角色功能</param>
        /// <returns>返回添加结果</returns>
        public async Task<ResultMessage> AddRoleFunction(List<RoleFunction> list)
        {
            return await SysRoleClient.AddRoleFunction(list);
        }
        #endregion
    }
}
