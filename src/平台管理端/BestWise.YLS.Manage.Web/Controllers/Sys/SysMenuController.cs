using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BestWise.User.Model;
using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.YLS.Manage.Web.Common;
using Newtonsoft.Json;
using BestWise.YLS.Manage.FrameWork;
using BestWise.YLS.Model;

namespace BestWise.YLS.Manage.Web.Controllers
{
    public class SysMenuController : BasicController
    {
        #region 系统菜单
        /// <summary>
        /// 系统菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion 

        #region 新增、编辑页
        /// <summary>
        /// 新增、编辑页
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> CreateOrModify(int id)
        {
            Sys_Function model = new Sys_Function();
            if (id > 0)
            {
                ResultMessage<Sys_Function> result = await BLLSysFunction.Instantion.GetModel(id);
                if (result.IsSucceed() && result.Data != null) model = result.Data;
            }
            return PartialView("_PartialCreateOrModify", model);
        }
        #endregion

        #region  菜单树形列表
        /// <summary>
        /// 菜单树形列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetTreeList(string state)
        {
            Dictionary<string, List<SysFunctionExt>> list = new Dictionary<string, List<SysFunctionExt>>();
            ResultMessage<PagedList<Sys_Function>> result = await BLLSysFunction.Instantion.GetPageList(new BaseFilter() { IsPage = false });
            List<SysFunctionExt> listMenu = new List<SysFunctionExt>();
            SysFunctionExt menuModel = null;
            if (result.IsSucceed() && result.Data != null && result.Data.Items.Count > 0)
            {
                result.Data.Items.ToList().ForEach(o =>
                {
                    menuModel = new SysFunctionExt();
                    menuModel.Id = o.Id;
                    menuModel.ParentId = o.ParentId;
                    menuModel.Name = o.Name;
                    menuModel.Identify = o.Identify;
                    menuModel.IsSelected = o.IsSelected;
                    menuModel.Type = o.Type;
                    menuModel.Sort = o.Sort;
                    menuModel.Notes = o.Notes;
                    menuModel.IsDeleted = o.IsDeleted;
                    menuModel.Cdt = o.Cdt;
                    menuModel.Cby = o.Cby;
                    menuModel.Mdt = o.Mdt;
                    menuModel.Mby = o.Mby;
                    if (o.ParentId != 0)
                        menuModel._parentId = o.ParentId.ToString();
                    else
                    {
                        menuModel.state = string.IsNullOrWhiteSpace(state) ? "closed" : state;
                    }
                    listMenu.Add(menuModel);

                });
                list.Add("rows", listMenu);
                return Json(list);
            }
            return Json(list);
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
            ResultMessage<PagedList<Sys_Function>> result = await BLLSysFunction.Instantion.GetPageList(new BaseFilter() { IsPage = false });
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
                            id = o.Id.ToString(),
                            text = o.Name,
                            parentCode = o.ParentId.ToString()
                        }), "0", true)
                    }
                };
                return JsonConvert.SerializeObject(data); ;
            }
            return string.Empty;
        }
        #endregion

        #region 保存 新增、编辑详情
        /// <summary>
        /// 保存 新增、编辑详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Save(Sys_Function model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                if (model.Id == 0)
                {
                    model.Type = 1;
                    result = await BLLSysFunction.Instantion.Add(model);
                }
                else
                {
                    ResultMessage<Sys_Function> selectOld = await BLLSysFunction.Instantion.GetModel(model.Id);
                    if (selectOld.IsSucceed() == false || selectOld.Data == null)
                        throw new Exception("无效的编辑操作！");
                    model.Type = selectOld.Data.Type;
                    result = await BLLSysFunction.Instantion.Update(model);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除菜单
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            ResultMessage result = new ResultMessage();
            if (id != 0) result = await BLLSysFunction.Instantion.Delete(id);
            return Json(result);
        }
        #endregion
    }
}