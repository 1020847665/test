using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.User.Model;
using BestWise.YLS.Manage.Web.Common;
using Newtonsoft.Json;
using BestWise.YLS.Manage.FrameWork;

namespace BestWise.YLS.Manage.Web.Controllers.Sys
{
    public class SysRoleController : BasicController
    {
        #region 系统角色页
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取角色分页列表
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetPageList()
        {
            BaseFilter basefilter = GetPageFiler(GetQueryCondition());
            ResultMessage<PagedList<Sys_Roles>> result = await BLLSysRole.Instantiatie.GetPageList(basefilter);
            return SerializePageList<Sys_Roles>(result);
        }
        #endregion

        #region 构建列表查询条件
        /// <summary>
        /// 构建列表查询条件
        /// </summary>
        /// <returns>返回列表查询条件</returns>
        public Dictionary<string, SearchInfo> GetQueryCondition()
        {
            Dictionary<string, SearchInfo> dictionary = new Dictionary<string, SearchInfo>();
            dictionary.Add("RoleName", new SearchInfo()
            {
                FieldName = "RoleName",
                SqlOperator = SqlOperator.Like,
            });
            return dictionary;
        }
        #endregion

        #region 获取Combotree下拉列表数据【所属层级】
        /// <summary>
        /// 获取Combotree下拉列表数据【所属层级】
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Combotree()
        {
            ResultMessage<PagedList<Sys_Roles>> result = await BLLSysRole.Instantiatie.GetPageList(new BaseFilter());
            if (result.IsSucceed() && result.Data != null && result.Data.Items.Count > 0)
            {
                List<EasyData> data = new List<EasyData>() {
                    new EasyData(){
                    id="0",
                    parentCode="0",
                    text="/",
                    children= EasyData.Recursion(result.Data.Items.ToList().ConvertAll<EasyData>(
                        o => new EasyData()
                        {
                            id = o.RoleId.ToString(),
                            text = o.RoleName,
                            parentCode = o.ParentId.ToString()
                        }), "0", true)
                    }
                };
                return JsonConvert.SerializeObject(data); ;
            }
            return string.Empty;
        }
        #endregion

        #region 获取系统角色信息
        /// <summary>
        /// 获取系统角色信息
        /// </summary>
        /// <returns>返回系统角色</returns>
        public async Task<ActionResult> ComboBox()
        {
            ResultMessage<PagedList<Sys_Roles>> result = await BLLSysRole.Instantiatie.GetPageList(GetPageFiler());
            if (result.IsSucceed() && result.Data != null && result.Data.Items.Count > 0)
            {
                return Json(result.Data.Items.ToList());
            }
            return Json("");
        }
        #endregion

        #region 角色编辑
        /// <summary>
        /// 角色编辑
        /// </summary>
        /// <param name="role">系统角色</param>
        /// <returns>返回角色编辑页</returns>
        public async Task<ActionResult> Edit(Sys_Roles model)
        {
            if (model.RoleId > 0)
            {
                ResultMessage<Sys_Roles> result = await BLLSysRole.Instantiatie.GetModel(model.RoleId);
                if (result.IsSucceed() && result.Data != null) model = result.Data;
            }
            return PartialView("_PartialCreateOrModify", model);
        }
        #endregion

        #region 角色信息保存
        /// <summary>
        /// 角色信息保存
        /// </summary>
        /// <param name="model">角色信息</param>
        /// <returns>返回保存结果</returns>
        public async Task<ActionResult> Save(Sys_Roles model)
        {
            ResultMessage result = new ResultMessage(); ;
            try
            {
                if (model.RoleId == 0) result = await BLLSysRole.Instantiatie.Add(model);
                else result = await BLLSysRole.Instantiatie.Update(model);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 角色删除
        /// <summary>
        /// 角色删除
        /// </summary>
        /// <returns>返回删除结果</returns>
        public async Task<JsonResult> DeleteSysRoles(int roleId)
        {
            ResultMessage result = ResultMessage.FailureResult("删除角色失败");
            if (roleId > 0) result = await BLLSysRole.Instantiatie.Delete(roleId);
            return Json(result);
        }
        #endregion
    }
}