using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.YLS.Manage.FrameWork;
using BestWise.YLS.Manage.Web;
using BestWise.YLS.Model;
using BestWise.YLS.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BWC.Galaxy.UMP.Manage.Web.Controllers
{
    public class StudentBookController : BasicController
    {
        #region 学员预约信息页
        /// <summary>
        /// 学员预约信息页
        /// </summary>
        /// <returns>返回学员预约视图页</returns>
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 课程预约信息页
        /// <summary>
        /// 课程预约信息页
        /// </summary>
        /// <returns>返回课程预约视图页</returns>
        public ActionResult CourseBook()
        {
            return View();
        }
        #endregion

        #region 教师预约信息页
        /// <summary>
        /// 教师预约信息页
        /// </summary>
        /// <returns>返回教师预约视图页</returns>
        public ActionResult TeacherBook()
        {
            return View();
        }
        #endregion

        #region 培训班预约信息页
        /// <summary>
        /// 培训班预约信息页
        /// </summary>
        /// <returns>返回培训班预约视图页</returns>
        public ActionResult TrainBook()
        {
            return View();
        }
        #endregion

        #region 获取学员预约信息列表
        /// <summary>
        /// 获取学员预约信息列表
        /// </summary>
        /// <param name="bookType">预约类型</param>
        /// <returns>返回学员预约信息</returns>
        [HttpPost]
        public async Task<ActionResult> GetPagedList(int bookType)
        {
            BaseFilter baseFilter = GetPageFiler(GetQueryCondition(bookType));
            ResultMessage<PagedList<StudentBook>> result = await BLLStudentBook.Instantiate.GetPageList(baseFilter);
            return SerializePageList<StudentBook>(result);
        }
        #endregion

        #region 构建列表查询条件
        /// <summary>
        /// 构建列表查询条件
        /// </summary>
        /// <param name="bookType">预约类型</param>
        /// <returns>返回列表查询条件</returns>
        private Dictionary<string, SearchInfo> GetQueryCondition(int bookType)
        {
            Dictionary<string, SearchInfo> queryDictionary = new Dictionary<string, SearchInfo>();
            queryDictionary.Add("searchKey", new SearchInfo() { FieldName = "BookName", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            queryDictionary.Add("searchKey_1", new SearchInfo() { FieldName = "UserName", GroupName = "searckGroup", SqlOperator = SqlOperator.Like });
            if (bookType != 0) queryDictionary.Add("searchKey_2", new SearchInfo() { FieldName = "Type", GroupName = "searckType", SqlOperator = SqlOperator.Equal, FieldValue = bookType });
            return queryDictionary;
        }
        #endregion

        #region 预约详情页
        /// <summary>
        /// 预约详情页
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>返回预约详情信息</returns>
        public async Task<ActionResult> Detail(Guid bookId)
        {
            StudentBook model = new StudentBook();
            ResultMessage<StudentBook> result = await BLLStudentBook.Instantiate.GetModel(bookId);
            if (result.IsSucceed() && result.Data != null) model = result.Data;
            return PartialView("_PartialDetail", model);
        }
        #endregion

        #region 预约处理页
        /// <summary>
        /// 预约处理页
        /// </summary>
        /// <param name="bookId">预约ID</param>
        /// <returns>返回预约处理信息</returns>
        public async Task<ActionResult> BookDeal(Guid bookId)
        {
            StudentBook model = new StudentBook();
            ResultMessage<StudentBook> result = await BLLStudentBook.Instantiate.GetModel(bookId);
            int dealState = (int)BookState.Receive;
            if (result.IsSucceed() && result.Data != null)
            {
                model = result.Data;
                dealState = model.DealState != 0 ? model.DealState : dealState;
            }
            ViewBag.DealState = new SelectList(SelectListExtensions.GetList<BookState>(), "Value", "Text", dealState);//预约处理状态
            return PartialView("_PartialBookDeal", model);
        }
        #endregion


        #region 保存教师信息
        /// <summary>
        /// 保存教师信息
        /// </summary>
        /// <param name="model">教师信息</param>
        /// <returns>返回保存结果</returns>
        public async Task<ActionResult> Save(StudentBook model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLStudentBook.Instantiate.Update(model);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region 预约状态处理
        /// <summary>
        /// 预约状态处理
        /// </summary>
        /// <param name="model">预约信息</param>
        /// <returns>返回处理结果</returns>
        [HttpPost]
        public async Task<ActionResult> DealState(StudentBook model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                result = await BLLStudentBook.Instantiate.DealState(model);
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