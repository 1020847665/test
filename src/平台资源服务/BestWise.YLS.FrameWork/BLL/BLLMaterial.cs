using BestWise.Common;
using BestWise.YLS.FrameWork.DAL;
using BestWise.YLS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BestWise.User.FrameWork.BLL;
using BestWise.Common.Mvc;
using BestWise.YLS.Model.Enums;
using System.Text.RegularExpressions;

namespace BestWise.YLS.FrameWork.BLL
{
    /// <summary>
    /// 资料信息业务逻辑处理层
    /// </summary>
    public class BLLMaterial : BaseBLL
    {
        private static readonly DALMaterial _dal = new DALMaterial();
        private static BLLMaterial _instantiate = null;

        /// <summary>
        /// 实例化对象
        /// </summary>
        public static BLLMaterial Instantiate
        {
            get { return _instantiate ?? (_instantiate = new BLLMaterial()); }
        }

        #region 新增资料信息
        /// <summary>
        /// 新增资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回新增结果</returns>
        public ResultMessage Add(Material model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.MaterialId = Guid.NewGuid();
                    model.Mby = CurrentUser.UserId;
                    model.Cby = CurrentUser.UserId;
                    if (model.AttachmentList != null && model.AttachmentList.Count > 0)
                    {
                        foreach (MaterialAttachment item in model.AttachmentList)
                        {
                            item.MaterialId = model.MaterialId;
                            item.Cby = CurrentUser.UserId;
                            item.Mby = CurrentUser.UserId;
                            item.Type = GetMaterialType(item.AttachmentUrl);
                        }
                    }
                    if (_dal.AddMaterial(model, XMLHelper.SerializeXml(model.AttachmentList))) result = ResultMessage.SucceedResult("操作成功！");
                }
                else result = ResultMessage.UnauthorizedResult();
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 更新资料信息
        /// <summary>
        /// 更新资料信息
        /// </summary>
        /// <param name="model">资料信息</param>
        /// <returns>返回修改结果</returns>
        public ResultMessage Update(Material model)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (CurrentUser != null)
                {
                    model.Mby = CurrentUser.UserId;
                    if (model.AttachmentList != null && model.AttachmentList.Count > 0)
                    {
                        foreach (MaterialAttachment item in model.AttachmentList)
                        {
                            item.MaterialId = model.MaterialId;
                            item.Cby = CurrentUser.UserId;
                            item.Mby = CurrentUser.UserId;
                            item.Type = GetMaterialType(item.AttachmentUrl);
                        }
                    }
                    if (_dal.UpdateMaterial(model, XMLHelper.SerializeXml(model.AttachmentList))) result = ResultMessage.SucceedResult("操作成功！");
                }
                else result = ResultMessage.FailureResult("非法操作");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 根据资料ID获取资料信息
        /// <summary>
        /// 根据资料ID获取资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>实体对象</returns>
        public ResultMessage<Material> GetModel(Guid materialId)
        {
            ResultMessage<Material> result = ResultMessage<Material>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<Material>.SucceedResult(_dal.GetModel(materialId));
            }
            catch (Exception ex)
            {
                result = ResultMessage<Material>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 分页获取资料信息
        /// <summary>
        /// 分页获取资料信息
        /// </summary>
        /// <param name="baseFilter">筛选条件</param>
        /// <returns>资料列表集合</returns>
        public ResultMessage<PagedList<Material>> GetPageList(BaseFilter baseFilter)
        {
            ResultMessage<PagedList<Material>> result = ResultMessage<PagedList<Material>>.FailureResult("获取失败！");
            try
            {
                result = ResultMessage<PagedList<Material>>.SucceedResult(_dal.GetPageList(baseFilter));
            }
            catch (Exception ex)
            {
                result = ResultMessage<PagedList<Material>>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 删除资料信息
        /// <summary>
        /// 删除资料信息
        /// </summary>
        /// <param name="materialId">资料ID</param>
        /// <returns>返回删除结果</returns>
        public ResultMessage Delete(Guid materialId)
        {
            ResultMessage result = ResultMessage.FailureResult("操作失败！");
            try
            {
                if (_dal.Delete(materialId)) result = ResultMessage.SucceedResult("操作成功！");
            }
            catch (Exception ex)
            {
                result = ResultMessage.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 获取资料附件
        /// <summary>
        /// 获取资料附件
        /// </summary>
        /// <param name="materialId">附件ID</param>
        /// <returns>返回资料附件</returns>
        public ResultMessage<List<MaterialAttachment>> GetAttachmentList(Guid materialId)
        {
            ResultMessage<List<MaterialAttachment>> result = ResultMessage<List<MaterialAttachment>>.FailureResult("获取失败！");
            try
            {
                List<MaterialAttachment> list = _dal.GetAttachmentList(materialId);
                if (list != null && list.Count > 0)
                {
                    foreach (MaterialAttachment item in list)
                    {
                        if (!IsUrl(item.AttachmentUrl)) item.AttachmentUrl = string.Concat(ConfigHelper.GetConfigString("AttachmentUrl"), item.AttachmentUrl);
                    }
                }
                result = ResultMessage<List<MaterialAttachment>>.SucceedResult(list);
            }
            catch (Exception ex)
            {
                result = ResultMessage<List<MaterialAttachment>>.FailureResult(ex);
            }
            return result;
        }
        #endregion

        #region 根据文件路径获取文件类型
        /// <summary>
        /// 根据文件路径获取文件类型
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>返回文件类型</returns>
        public MaterialType GetMaterialType(string path)
        {
            MaterialType materialType = MaterialType.Other;
            List<string> video = new List<string>() { ".mpg", ".mpeg", ".mp4", ".avi", ".rm", ".rmvb", ".mov", ".wmv", ".asf", ".dat", ".divx", ".vob", ".asx", ".wvx", ".mpe", ".mpa" };//常见视频文件
            List<string> audio = new List<string>() { ".mp3", ".mp1", ".mp2", ".wma", ".mid", ".mmf" };//常见音频频文件
            if (!string.IsNullOrWhiteSpace(path) && !IsUrl(path))
            {
                string extensionName = DirFile.GetExtension(path);
                if (video.Contains(extensionName)) materialType = MaterialType.Video;
                else if (audio.Contains(extensionName)) materialType = MaterialType.Audio;
            }
            return materialType;
        }
        #endregion

        #region 判断字符串是否为url
        /// <summary>
        /// 判断字符串是否为url
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回判断结果</returns>
        public static bool IsUrl(string str)
        {
            bool result = false;
            try
            {
                string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
                result = Regex.IsMatch(str, Url);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }
        #endregion
    }
}
