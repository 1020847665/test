using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BestWise.YLS.Manage.Web.Startup))]
namespace BestWise.YLS.Manage.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
