
using System.Web;
using System.Web.Mvc;

namespace BestWise.YLS.Manage.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new UnHandleExceptionAttribute());
            filters.Add(new ModelValidFilterAttribute());
            filters.Add(new PermissionAuthorizeAttribute());
        }
    }
}
