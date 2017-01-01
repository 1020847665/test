using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BestWise.User.Model;
using BestWise.Common;
using BestWise.Common.Mvc;
using YSF.CT.Encrypt;
using BestWise.YLS.Manage.FrameWork;
using BestWise.YLS.Model;
using BestWise.YLS.Manage.Web.Common.Filter;
using BestWise.YLS.Model.Enums;

namespace BestWise.YLS.Manage.Web.Controllers.Sys
{
    public class SysUsersController : BasicController
    {
        #region 系统用户页
        /// <summary>
        /// 系统用户页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取系统用户列表
        /// <summary>
        /// 获取系统用户列表
        /// </summary>
        /// <returns>返回用户信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetPagedList()
        {
            BaseFilter baseFilter = GetPageFiler(GetQueryCondition());
            ResultMessage<PagedList<Users>> result = await BLLUsers.Instantiate.GetPageList(baseFilter);
            return SerializePageList<Users>(result);
        }
        #endregion

        #region 新增、编辑用户
        /// <summary>
        /// 新增、编辑用户
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns></returns>
        public async Task<ActionResult> EditUser(Users model)
        {
            if (model.UserId != Guid.Empty)
            {
                ResultMessage<Users> result = await BLLUsers.Instantiate.GetModel(model.UserId);
                if (result.IsSucceed() && result.Data != null) model = result.Data;
            }
            ViewBag.Gender = new SelectList(SelectListExtensions.GetList<Gender>(), "Value", "Text");//性别
            return PartialView("_PartialCreateOrModify", model);
        }
        #endregion

        #region 保存用户信息
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns>返回保存结果</returns>
        public async Task<ActionResult> SaveUser(Users model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                if (model.UserId != Guid.Empty) result = await BLLUsers.Instantiate.UpdateUser(model);
                else result = await BLLUsers.Instantiate.Add(model);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region  删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回操作结果</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid userId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLUsers.Instantiate.DeleteUserById(userId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 构建列表查询条件
        /// <summary>
        /// 构建列表查询条件
        /// </summary>
        /// <returns>返回列表查询条件</returns>
        private Dictionary<string, SearchInfo> GetQueryCondition()
        {
            Dictionary<string, SearchInfo> queryDictionary = new Dictionary<string, SearchInfo>();
            queryDictionary.Add("searchKey", new SearchInfo() { FieldName = "UserName", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            queryDictionary.Add("searchKey_1", new SearchInfo() { FieldName = "NickName", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            queryDictionary.Add("searchKey_2", new SearchInfo() { FieldName = "Email", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            queryDictionary.Add("searchKey_3", new SearchInfo() { FieldName = "MobileNumber", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            queryDictionary.Add("searchKey_4", new SearchInfo() { FieldName = "PhoneNumber", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            return queryDictionary;
        }
        #endregion

        #region 重置密码页
        /// <summary>
        /// 重置密码页
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回重置密码结果</returns>
        public async Task<ActionResult> EditPwd(Users model)
        {
            if (model.UserId != Guid.Empty)
            {
                ResultMessage<Users> result = await BLLUsers.Instantiate.GetModel(model.UserId);
                if (result.IsSucceed() && result.Data != null) return View(result.Data);
            }
            return Content(string.Empty);
        }
        #endregion

        #region 重置密码修改
        /// <summary>
        /// 重置密码修改
        /// </summary>
        /// <returns>返回重置用户密码结果</returns>
        [HttpPost]
        public async Task<JsonResult> ChangePwd(Users model)
        {
            var result = await BLLUsers.Instantiate.GetModel(model.UserId);
            if (result.IsSucceed() && result.Data != null) return Json(await BLLUsers.Instantiate.UpdatePassword(model.Password, model.UserId));
            return Json(ResultMessage.FailureResult("用户不存在！"));
        }
        #endregion

        #region 顶部修改自身密码页
        /// <summary>
        /// 顶部修改自身密码页
        /// </summary>
        /// <returns>返回当前用户信息</returns>
        public ActionResult AlterPwd()
        {
            Users model = new Users();
            model.UserName = CurrentUser.UserName;
            model.NickName = CurrentUser.NickName;
            model.UserId = CurrentUser.UserId;
            return View(model);
        }
        #endregion

        #region 保存密码修改
        /// <summary>
        /// 保存密码修改
        /// </summary>
        /// <returns>返回密码修改结果</returns>
        [HttpPost]
        public async Task<JsonResult> SaveAlterPwd(Users model)
        {
            var result = await BLLUsers.Instantiate.GetModel(model.UserId);
            if (result.IsSucceed() && result.Data != null)
            {
                if (model.Password != model.ConfirmPassword)
                    return Json(ResultMessage.FailureResult("两次新密码输入不一致！"));
                if (YS.Encrypt(model.OldPassword) != result.Data.Password)
                    return Json(ResultMessage.FailureResult("输入的原密码有误！"));
                if (YS.Encrypt(model.Password) == result.Data.Password)
                    return Json(ResultMessage.FailureResult("新密码不能和旧密码相同！"));
                var alterResult = await BLLUsers.Instantiate.UpdatePassword(model.Password, model.UserId);
                //密码修改成功
                if (alterResult.IsSucceed())
                {
                    //注销会话
                    await BLLUsers.Instantiate.SignOut();
                    //清除cookie
                    HttpCookie cookie = new HttpCookie("BestWise-SysUser");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
                return Json(alterResult);
            }
            return Json(ResultMessage.FailureResult("用户不存在！"));
        }
        #endregion

        #region 禁用/启用
        /// <summary>
        /// 禁用/启用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Forbidden(Users model)
        {
            if (model.UserId != CurrentUser.UserId)
            {
                var result = await BLLUsers.Instantiate.GetModel(model.UserId);
                if (result.IsSucceed() && result.Data != null)
                {
                    model.IsDisabled = !result.Data.IsDisabled;
                    return Json(await BLLUsers.Instantiate.UpdateUserState(model));
                }
                return Json(ResultMessage.FailureResult("用户不存在！"));
            }
            return Json(ResultMessage.FailureResult("无法对当前登录用户进行此操作！"));
        }
        #endregion

    }
}