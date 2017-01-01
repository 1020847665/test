using BestWise.Common;
using BestWise.Common.DEncrypt;
using BestWise.User.Model;
using BestWise.YLS.Manage.FrameWork;
using BestWise.YLS.Manage.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BestWise.YLS.Manage.Web.Controllers
{
    public class HomeController : BasicController
    {
        public ActionResult Default()
        {
            return View();
        }

        #region 登录页面
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> Login()
        {

            await BLLUsers.Instantiate.SignOut();
            var cookie = Request.Cookies["BestWise-SysUser"];
            if (cookie != null)
            {
                UserLogin model = new UserLogin();
                var key = ConfigHelper.GetConfigString("cookieEncryptKey");
                model.UserName = cookie["Account"].GetString();
                model.Password = cookie["Password"].GetString();
                if (!string.IsNullOrWhiteSpace(model.UserName))
                    model.UserName = DEncrypt.Decrypt(model.UserName, key);
                if (!string.IsNullOrWhiteSpace(model.Password))
                    model.Password = DEncrypt.Decrypt(model.Password, key);
                ResultMessage result = await BLLUsers.Instantiate.SignInAsync(model);
                if (result.IsSucceed())
                    return RedirectToAction("Main", "LayOut");
            }
            return View();
        }
        #endregion

        #region 登录检查
        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLogin model)
        {
            if (!string.Equals(model.Captcha, Session["ValidateCode"].GetString(), StringComparison.OrdinalIgnoreCase))
                return Json(ResultMessage.FailureResult("输入的验证码有误！"));
            ResultMessage result = await BLLUsers.Instantiate.SignInAsync(model);
            if (result.IsSucceed())
            {
                HttpCookie cookie = new HttpCookie("BestWise-SysUser");
                if (model.IsRemember)
                {
                    var key = ConfigHelper.GetConfigString("cookieEncryptKey");
                    cookie.Values.Add("Account", DEncrypt.Encrypt(model.UserName.Trim(), key));
                    cookie.Values.Add("Password", DEncrypt.Encrypt(model.Password.Trim(), key));
                    cookie.Expires = DateTime.Now.AddDays(7);
                }
                else cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return Json(result);
        }
        #endregion

        #region 注销登录
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginOut()
        {
            ResultMessage result = ResultMessage.SucceedResult();
            try
            {
                await BLLUsers.Instantiate.SignOut();
                HttpCookie cookie = new HttpCookie("BestWise-SysUser");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex.Message);
            }
            return Json(result);
        }
        #endregion
    }
}