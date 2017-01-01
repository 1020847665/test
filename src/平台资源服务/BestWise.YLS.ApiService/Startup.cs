using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BestWise.YLS.ApiService.Startup))]

namespace BestWise.YLS.ApiService
{
    public partial class Startup:BestWise.Common.WebApi.Startup
    {
        public void Configuration(IAppBuilder app)
        {
           base.ConfigureAuth(app);
        }
    }
}
