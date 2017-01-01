using BestWise.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using BestWise.User.Model;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.AspNet.Identity;


namespace BestWise.YLS.Manage.Web
{

    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Unsealed so that subclassed types can set properties in the default constructor or override our behavior.")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class PermissionAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 请求授权时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null) throw new ArgumentNullException("filterContext");
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
            if (skipAuthorization)
                return;
            if (!AuthorizeCore(filterContext))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult json = new JsonResult();
                    json.Data = ResultMessage.UnauthorizedResult();
                    json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    filterContext.Result = json;
                }
                else filterContext.Result = new PartialViewResult { ViewName = "NoAuthorize" };
            }
        }

        /// <summary>
        /// 自定义授权检查（返回False则授权失败）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected virtual bool AuthorizeCore(AuthorizationContext filterContext)
        {
            HttpContextBase httpContext = filterContext.HttpContext;
            if (httpContext == null) throw new ArgumentNullException("httpContext");
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;
            if (String.IsNullOrWhiteSpace(user.Identity.Name))
                return false;
            Guid userId = GetUserId(httpContext);
            if (userId == Guid.Empty)
                return false;
            return true;
        }



        private Guid GetUserId(HttpContextBase httpContext)
        {
            Guid userId = Guid.Empty;
            IOwinContext OwinContext = httpContext.GetOwinContext();
            IAuthenticationManager AuthenticationManager = OwinContext != null ? httpContext.GetOwinContext().Authentication : null;
            ClaimsPrincipal Principal = AuthenticationManager != null ? AuthenticationManager.User : null;
            IIdentity identity = Principal != null ? Principal.Identity : null;
            userId = identity.GetUserId().GetGuid();
            return userId;
        }
    }
}