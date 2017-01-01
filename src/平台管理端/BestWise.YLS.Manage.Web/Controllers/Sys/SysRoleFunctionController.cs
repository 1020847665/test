using BestWise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BestWise.User.Model;

namespace BestWise.YLS.Manage.Web.Controllers.Sys
{
    public class SysRoleFunctionController : BasicController
    {
        #region 系统角色功能页
        public ActionResult Index()
        {
            return View();
        }
        #endregion 

        #region 根据角色ID获取功能
        /// <summary>
        /// 根据角色ID获取功能
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回角色功能信息</returns>
        [HttpPost]
        public async Task<JsonResult> GetFunction(int roleId)
        {
            ResultMessage<List<Sys_RoleFunction>> result = await FrameWork.BLLRoleFunction.Instantion.GetList(roleId);
            return Json(result.Data == null ? (object)"" : result.Data);
        }
        #endregion

        #region 保存角色功能
        /// <summary>
        /// 保存用户功能
        /// </summary>
        /// <param name="list">角色功能列表</param>
        /// <returns>返回添加结构</returns>
        [HttpPost]
        public async Task<ActionResult> SaveFunction(List<RoleFunction> list)
        {
            return Json(await FrameWork.BLLSysRole.Instantiatie.AddRoleFunction(list));
        }
        #endregion
    }
}