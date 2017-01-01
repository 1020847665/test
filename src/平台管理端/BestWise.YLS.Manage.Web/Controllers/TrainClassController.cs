using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.YLS.Manage.FrameWork;
using BestWise.YLS.Manage.Web;
using BestWise.YLS.Manage.Web.Common.Filter;
using BestWise.YLS.Model;
using BestWise.YLS.Model.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BWC.Galaxy.UMP.Manage.Web.Controllers
{
    public class TrainClassController : BasicController
    {
        #region 培训班信息页
        /// <summary>
        /// 培训班信息页
        /// </summary>
        /// <returns>返回培训班视图页</returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        public ActionResult Test()
        {
            return View();
        }

        #region 获取培训班信息列表
        /// <summary>
        /// 获取培训班信息列表
        /// </summary>
        /// <returns>返回培训班信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetPagedList()
        {
            BaseFilter baseFilter = GetPageFiler(GetQueryCondition());
            ResultMessage<PagedList<TrainClass>> result = await BLLTrainClass.Instantiate.GetPageList(baseFilter);
            return SerializePageList<TrainClass>(result);
        }
        #endregion

        #region 获取培训班联系人列表
        /// <summary>
        /// 获取培训班联系人列表
        /// </summary>
        /// <param name="trainId">培训班ID</param>
        /// <returns>返回培训班联系人信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetContactList(Guid trainId)
        {
            ResultMessage<List<TrainContact>> result = await BLLTrainClass.Instantiate.GetContactList(trainId);
            return SerializeList<TrainContact>(result);
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
            queryDictionary.Add("searchKey", new SearchInfo() { FieldName = "Name", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            return queryDictionary;
        }
        #endregion

        #region 培训班编辑页
        /// <summary>
        /// 培训班编辑页
        /// </summary>
        /// <returns>返回培训编辑页</returns>
        public async Task<ActionResult> Edit(TrainClass model)
        {
            if (model.TrainId != Guid.Empty)
            {
                ResultMessage<TrainClass> result = await BLLTrainClass.Instantiate.GetModel(model.TrainId);
                if (result.IsSucceed() && result.Data != null) model = result.Data;
            }
            ViewBag.Type = new SelectList(SelectListExtensions.GetList<TrainClassType>(), "Value", "Text", (int)TrainClassType.Train);//培训班类型
            ViewBag.State = new SelectList(SelectListExtensions.GetList<TrainClassState>(), "Value", "Text");//培训班状态
            ResultMessage<PagedList<Teacher>> teacherResult = await BLLTeacher.Instantiate.GetPageList(new BaseFilter() { IsPage = false });
            IList<Teacher> teacherList = null;
            if (teacherResult.IsSucceed() && teacherResult.Data != null) teacherList = teacherResult.Data.Items;
            ViewBag.Teacher = teacherList;
            ViewBag.TrainId = model.TrainId;
            return PartialView("_PartialEdit", model);
        }
        #endregion

        #region 保存培训班信息
        /// <summary>
        /// 保存培训班信息
        /// </summary>
        /// <param name="model">培训班信息</param>
        /// <returns>返回保存结果</returns>
        [ValidateInput(false)]
        public async Task<ActionResult> Save(TrainClass model, List<TrainContact> list)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                model.Contacts = list;
                if (model.TrainId != Guid.Empty) result = await BLLTrainClass.Instantiate.Update(model);
                else result = await BLLTrainClass.Instantiate.Add(model);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除培训班信息
        /// <summary>
        /// 删除培训班信息
        /// </summary>
        /// <param name="trainId">培训班ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        public async Task<ActionResult> Delete(Guid trainId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLTrainClass.Instantiate.Delete(trainId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 删除培训班联系人
        /// <summary>
        /// 删除培训班联系人
        /// </summary>
        /// <param name="contactId">联系人ID</param>
        /// <returns>返回删除结果</returns>
        [HttpPost]
        public async Task<ActionResult> DeleteContact(Guid contactId)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLTrainClass.Instantiate.DeleteContact(contactId);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion
    }
}