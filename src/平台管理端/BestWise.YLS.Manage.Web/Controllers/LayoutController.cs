using BestWise.Common;
using BestWise.YLS.Manage.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BestWise.User.Model;
using BestWise.YLS.Manage.FrameWork;

namespace BestWise.YLS.Manage.Web.Controllers
{
    public class LayoutController : BasicController
    {
        public async Task<ActionResult> Main()
        {
            ResultMessage<List<UserMenu>> result = await BLLUsers.Instantiate.GetUserMenu();
            ViewBag.menuList = result.Data;
            return View();
        }

        /// <summary>
        /// 左侧菜单栏
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Left()
        {
            ResultMessage<List<UserMenu>> result = await BLLUsers.Instantiate.GetUserMenu();
            ViewBag.leftList = result.Data;
            return View();
        }
    }
}