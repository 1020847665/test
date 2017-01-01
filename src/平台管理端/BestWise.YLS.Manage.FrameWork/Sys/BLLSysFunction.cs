using BestWise.Common;
using BestWise.Common.Mvc;
using BestWise.User.Model;
using BestWise.YLS.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.YLS.Manage.FrameWork
{
    /// <summary>
    /// 系统功能业务逻辑层
    /// </summary>
    public class BLLSysFunction
    {
        private static BLLSysFunction _instantion = null;

        public static BLLSysFunction Instantion
        {
            get { return _instantion ?? (_instantion = new BLLSysFunction()); }
        }

        #region 新增实体对象
        /// <summary>
        /// 新增实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns></returns>
        public async Task<ResultMessage> Add(Sys_Function model)
        {
            return await SysFunctionClient.Add(model);
        }
        #endregion

        #region 更新实体对象
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="model">选择项实体</param>
        /// <returns></returns>
        public async Task<ResultMessage> Update(Sys_Function model)
        {
            return await SysFunctionClient.Update(model);
        }
        #endregion

        #region 获取实体对象
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public async Task<ResultMessage<Sys_Function>> GetModel(int Id)
        {
            return await SysFunctionClient.GetModel(Id);
        }
        #endregion

        #region 分页获取功能信息
        /// <summary>
        /// 分页获取功能信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>列表集合</returns>
        public async Task<ResultMessage<PagedList<Sys_Function>>> GetPageList(BaseFilter baseFilter)
        {
            return await SysFunctionClient.GetPageList(baseFilter);
        }
        #endregion

        #region 获取功能列表信息
        /// <summary>
        /// 获取功能列表信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>列表集合</returns>
        public async Task<ResultMessage<List<SysFunction>>> GetFunctionList(BaseFilter baseFilter)
        {
            return await SysFunctionClient.GetFunctionList(baseFilter);
        }
        #endregion

        #region 删除功能点信息
        /// <summary>
        /// 删除功能点信息
        /// </summary>
        /// <param name="id">功能点ID</param>
        /// <returns>返回删除结果</returns>
        public async Task<ResultMessage> Delete(int id)
        {
            return await SysFunctionClient.Delete(id);
        }
        #endregion

    }
}
