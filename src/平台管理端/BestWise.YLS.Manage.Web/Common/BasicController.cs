using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestWise.User.Model;
using System.Security.Claims;
using BestWise.Common;
using Microsoft.AspNet.Identity;
using BestWise.YLS.Manage.Web.Common.Filter;
using BestWise.Common.Mvc;
using System.Reflection;
using System.Threading.Tasks;

namespace BestWise.YLS.Manage.Web
{
    [SuppressMessage("BestWise", "CA1506:AvoidExcessiveClassCoupling", Justification = "由公共图面区域的类复杂性")]
    public abstract class BasicController : BaseController
    {
        private Users user;
        private bool? _IsAuthenticated;

        public BasicController()
            : base()
        {
            SetCurrentUser();
            ViewBag.CurrentUser = user;
            ViewBag.SysConfig = SysConfig;
            ViewBag.IsAuthenticated = _IsAuthenticated.Value;
        }

        #region 条件过滤器
        /// <summary>
        /// 条件过滤器
        /// </summary>
        /// <param name="dictionary">过滤器字典</param>
        /// <param name="isPage">是否分页</param>
        /// <returns>返回查询条件</returns>
        public BaseFilter GetPageFiler(Dictionary<string, SearchInfo> dictionary = null, bool isPage = true)
        {
            List<SearchInfo> searchList = null;
            SearchInfo searchInfo = null;
            BaseFilter baseFilter = new BaseFilter();
            if (isPage)
            {
                baseFilter.PageIndex = PageIndex;
                baseFilter.PageSize = PageSize;
                baseFilter.IsPage = isPage;
            }
            if (dictionary != null && dictionary.Count > 0)
            {
                searchList = new List<SearchInfo>();
                foreach (KeyValuePair<string, SearchInfo> item in dictionary)
                {
                    string searchKey = string.Empty;
                    var arr = item.Key.Split('_');
                    if (arr.Length > 1) searchKey = arr[0].ToString();
                    else searchKey = item.Key;
                    string searchValue = item.Value.FieldValue != null && !string.IsNullOrWhiteSpace(item.Value.FieldValue.ToString()) ? item.Value.FieldValue.ToString() : string.Empty;
                    if (string.IsNullOrWhiteSpace(searchValue)) searchValue = Tool.RequestString(searchKey);
                    if (string.IsNullOrWhiteSpace(searchValue)) continue;
                    searchInfo = new SearchInfo();
                    searchInfo.FieldName = item.Value.FieldName;
                    searchInfo.FieldValue = searchValue;
                    searchInfo.GroupName = item.Value.GroupName;
                    searchInfo.SqlOperator = item.Value.SqlOperator;
                    searchList.Add(searchInfo);
                }
            }
            if (searchList != null && searchList.Count > 0) baseFilter.Condition = searchList;
            return baseFilter;
        }
        #endregion

        #region 是否验证了用户
        /// <summary>
        /// 是否验证了用户
        /// </summary>
        protected bool IsAuthenticated
        {
            get
            {
                return GetIsAuthenticated();
            }
        }
        #endregion

        #region 获取当前登录用户对象
        /// <summary>
        /// 获取当前登录用户对象
        /// </summary>
        protected Users CurrentUser
        {
            get
            {
                SetCurrentUser();
                return user;
            }
        }
        #endregion

        #region 获取系统参数配置对象
        /// <summary>
        /// 获取系统参数配置对象
        /// </summary>
        protected SystemConfig SysConfig
        {
            get
            {
                SystemConfig config = new SystemConfig();
                //缓存获取系统配置，编辑、重置时清空缓存
                if (CacheHelper.Exist("SystemConfig")) config = CacheHelper.Retrieve<SystemConfig>("SystemConfig");
                else
                {
                    //读取配置文件
                    config = SystemConfigHelper.GetSysConfig();
                    CacheHelper.Store("SystemConfig", config, TimeSpan.FromDays(1));  //加入缓存一天
                }
                return config;
            }
        }
        #endregion

        #region 当前页码
        /// <summary>
        /// 当前页码
        /// </summary>
        protected int PageIndex
        {
            get
            {
                int index = ConvertUtils.ConvertToInt32(Request.Params["page"]);
                index = index == 0 ? 1 : index;
                return index;
            }
        }
        #endregion

        #region 分页大小
        /// <summary>
        /// 分页大小
        /// </summary>
        protected int PageSize
        {
            get
            {
                int size = ConvertUtils.ConvertToInt32(Request.Params["rows"]);
                size = size == 0 ? 100 : size;
                return size;
            }
        }
        #endregion

        #region 当前排序
        /// <summary>
        /// 当前排序
        /// 前台EasyUI-Datagrid传值实例：
        /// queryParams: { key:'',                                                       
        /// 查询附加参数
        /// replaceSortField:'RoleName-Name' },   //将排序字段RoleName替换成Name
        /// </summary>
        protected string OrderBy
        {
            get
            {
                var sortFieldArray = Tool.RequestString("sort").Split(',');
                var orderTypeArray = Tool.RequestString("order").Split(',');
                if (sortFieldArray.Length > 0)
                {
                    var sortFieldName = string.Empty;
                    Dictionary<string, string> replaceSortFieldList = new Dictionary<string, string>();
                    var orderByList = new List<string>(sortFieldArray.Length);
                    var replaceSortField = Tool.RequestString("replaceSortField"); //获取替换的排序字段
                    //组装要替换的字符串键值对
                    if (!string.IsNullOrWhiteSpace(replaceSortField))
                    {
                        foreach (var item in replaceSortField.Split(','))
                        {
                            if (!string.IsNullOrWhiteSpace(item.Split('-')[0]) && !replaceSortFieldList.ContainsKey(item.Split('-')[0]))
                                replaceSortFieldList.Add(item.Split('-')[0], item.Split('-')[1]);
                        }
                    }
                    //组装排序
                    for (int i = 0; i < sortFieldArray.Length; i++)
                    {
                        sortFieldName = sortFieldArray[i];
                        sortFieldName = replaceSortFieldList.ContainsKey(sortFieldName) //执行排序字段替换
                            ? replaceSortFieldList[sortFieldName]
                            : sortFieldName;
                        orderByList.Add(string.Format("{0} {1}", sortFieldName, orderTypeArray[i]));
                    }
                    if (orderByList.Count > 0) return String.Join(",", orderByList);
                }
                return string.Empty;
            }
        }
        #endregion

        #region 序列化PageList【分页】
        /// <summary>
        /// 序列化PageList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="extendAttribute"></param>
        /// <returns></returns>
        protected ActionResult SerializePageList<T>(ResultMessage<PagedList<T>> result, object extendAttribute = null)
        {
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            long totalCount = 0;
            object items = null;
            if (result.IsSucceed() && result.Data != null)
            {
                totalCount = result.Data.TotalItemCount;
                items = result.Data.Items;
            }
            resultData.Add("total", totalCount);
            resultData.Add("pageSize", PageSize);
            resultData.Add("pageIndex", PageIndex);
            if (items != null) resultData.Add("rows", items);
            else resultData.Add("rows", "");
            //扩展属性
            if (extendAttribute != null)
            {
                Type type = extendAttribute.GetType();
                foreach (PropertyInfo property in type.GetProperties())
                {
                    try
                    {
                        resultData.Add(property.Name, property.GetValue(extendAttribute, null));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex);
                    }
                }
            }
            return Json(resultData);
        }
        #endregion

        #region 序列化PageList【分页】
        /// <summary>
        /// 序列化PageList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="extendAttribute"></param>
        /// <returns></returns>
        protected ActionResult SerializePageList<T>(PagedList<T> result, object extendAttribute = null)
        {
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            long totalCount = 0;
            object items = null;
            if (result != null && result.Items != null && result.Items.Count > 0)
            {
                totalCount = result.TotalItemCount;
                items = result.Items;
            }
            resultData.Add("total", totalCount);
            if (items != null) resultData.Add("rows", items);
            else resultData.Add("rows", "");
            //扩展属性
            if (extendAttribute != null)
            {
                Type type = extendAttribute.GetType();
                foreach (PropertyInfo property in type.GetProperties())
                {
                    try
                    {
                        resultData.Add(property.Name, property.GetValue(extendAttribute, null));
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return Json(resultData);
        }
        #endregion

        #region 序列化PageList【分页】
        /// <summary>
        /// 序列化PageList
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult SerializePageList<T>(PagedList<T> result)
        {
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            long totalCount = 0;
            object items = null;
            if (result != null && result.Items != null && result.Items.Count > 0)
            {
                totalCount = result.TotalItemCount;
                items = result.Items;
            }
            resultData.Add("total", totalCount);
            if (items != null) resultData.Add("rows", items);
            else resultData.Add("rows", "");
            return Json(resultData);
        }
        #endregion

        #region 序列化PageList【分页】
        /// <summary>
        /// 序列化PageList
        /// </summary>
        /// <returns></returns>
        protected ActionResult SerializeEmpty()
        {
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            resultData.Add("total", 0);
            resultData.Add("rows", "");
            return Json(resultData);
        }
        #endregion

        #region 序列化List
        /// <summary>
        /// 序列化List
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult SerializeList<T>(ResultMessage<List<T>> result)
        {
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            long totalCount = 0;
            object items = null;
            if (result.IsSucceed() && result.Data != null)
            {
                totalCount = result.Data.Count;
                items = result.Data;
            }
            resultData.Add("total", totalCount);
            if (items != null) resultData.Add("rows", items);
            else resultData.Add("rows", "");
            return Json(resultData);
        }
        #endregion

        #region 序列化List
        /// <summary>
        /// 序列化List
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult SerializeList<T>(List<T> result)
        {
            Dictionary<string, object> resultData = new Dictionary<string, object>();
            long totalCount = 0;
            object items = null;
            if (result != null)
            {
                totalCount = result.Count;
                items = result;
            }
            resultData.Add("total", totalCount);
            if (items != null) resultData.Add("rows", items);
            else resultData.Add("rows", "");
            return Json(resultData);
        }
        #endregion

        //↓↓↓↓私有方法↓↓↓↓

        #region 是否已通过验证
        /// <summary>
        /// 是否已通过验证
        /// </summary>
        /// <returns></returns>
        private bool GetIsAuthenticated()
        {
            if (_IsAuthenticated == null)
            {
                HttpContextBase _httpContext = HttpContext;
                if (_httpContext == null && System.Web.HttpContext.Current != null)
                    _httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
                _IsAuthenticated = _httpContext != null && _httpContext.GetOwinContext() != null && _httpContext.GetOwinContext().Authentication.User != null && _httpContext.GetOwinContext().Authentication.User.Identity != null && _httpContext.GetOwinContext().Authentication.User.Identity.IsAuthenticated;
            }
            return _IsAuthenticated.Value;
        }
        #endregion

        #region 设置当前用户信息
        /// <summary>
        /// 设置当前用户信息
        /// </summary>
        private void SetCurrentUser()
        {
            if (GetIsAuthenticated() && (user == null || user.UserId == Guid.Empty))
            {
                HttpContextBase _httpContext = HttpContext;
                if (_httpContext == null && System.Web.HttpContext.Current != null)
                    _httpContext = new HttpContextWrapper(System.Web.HttpContext.Current);
                ClaimsIdentity identity = (ClaimsIdentity)_httpContext.GetOwinContext().Authentication.User.Identity;
                if (identity != null)
                {
                    user = new Users
                    {
                        UserId = identity.FindFirstValue(ClaimTypes.NameIdentifier).GetGuid(),
                        UserName = identity.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                        NickName = identity.FindFirstValue(BestWiseClaimTypes.NickName),
                        RoleId = identity.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType).GetInt()
                    };
                    user.Password = string.Empty;
                    //user.RoleId = user.RoleId == 0 ? 0 : user.RoleId;
                };
            }
        }
        #endregion
    }
}