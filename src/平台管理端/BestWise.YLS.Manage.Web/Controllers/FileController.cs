using BestWise.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BWC.Galaxy.UMP.Manage.Web.Controllers
{
    public class FileController : Controller
    {
        #region 图片上传
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns>返回上传结果</returns>
        [HttpPost]
        [Route("UploadImage/{type}")]
        public ActionResult UploadImage()
        {
            ResultMessage<string> result = ResultMessage<string>.FailureResult("图片上传失败！");
            try
            {
                HttpRequest request = System.Web.HttpContext.Current.Request;
                string imageSavePath = String.Empty;//图片保存路径
                var routeData = RouteData;
                if (routeData != null && routeData.Values != null && routeData.Values.Count > 0)
                {
                    string imageType = routeData.Values["type"].ToString();
                    byte[] bytes = null;
                    if (request.Files != null && request.Files.Count > 0)
                    {
                        foreach (string key in request.Files.AllKeys)
                        {
                            HttpPostedFile httpPostedFile = request.Files[key];
                            using (MemoryStream ms = new MemoryStream())
                            {
                                httpPostedFile.InputStream.CopyTo(ms);
                                bytes = ms.ToArray();
                            }
                            if (bytes != null && bytes.Length > 0)
                            {
                                imageSavePath = BestWise.Common.File.Image.Save(imageType, httpPostedFile.FileName, bytes);
                                break;
                            }
                            else result = ResultMessage<string>.FailureResult("请选择上传图片！");
                        }
                    }
                    else if (request.InputStream != null && request.InputStream.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            request.InputStream.CopyTo(ms);
                            bytes = ms.ToArray();
                        }
                        if (bytes != null && bytes.Length > 0)
                            imageSavePath = BestWise.Common.File.Image.Save(imageType, bytes);
                        else result = ResultMessage<string>.FailureResult("请选择上传图片！");
                    }
                    if (!string.IsNullOrWhiteSpace(imageSavePath))
                        result = ResultMessage<string>.SucceedResult("图片上传成功！", imageSavePath);
                }
                else result = ResultMessage<string>.FailureResult("图片类型不明确！");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return Json(result);
        }
        #endregion

        #region 附件上传
        /// <summary>
        /// 附件上传
        /// </summary>
        /// <returns>返回上传结果</returns>
        [HttpPost]
        [Route("UploadAttachment/{name}")]
        public ActionResult UploadAttachment()
        {
            ResultMessage<string> result = ResultMessage<string>.FailureResult("附件上传失败！");
            try
            {
                HttpRequest request = System.Web.HttpContext.Current.Request;
                string fileSavePath = String.Empty;//文件保存路径
                var routeData = RouteData;
                if (routeData != null && routeData.Values != null && routeData.Values.Count > 0)
                {
                    byte[] bytes = null;
                    string fileName = routeData.Values["name"].ToString();
                    if (request.Files != null && request.Files.Count > 0)
                    {
                        foreach (string key in request.Files.AllKeys)
                        {
                            HttpPostedFile httpPostedFile = request.Files[key];
                            using (MemoryStream ms = new MemoryStream())
                            {
                                httpPostedFile.InputStream.CopyTo(ms);
                                bytes = ms.ToArray();
                            }
                            if (bytes != null && bytes.Length > 0)
                            {
                                string fileType = DirFile.GetExtension(httpPostedFile.FileName);
                                fileSavePath = BestWise.Common.File.File.Instantiate.ConfigureSave(fileName, bytes, fileType);
                                break;
                            }
                            else result = ResultMessage<string>.FailureResult("请选择上传文件！");
                        }
                    }
                }
                else result = ResultMessage<string>.FailureResult("文件用途不明确！");
                if (!string.IsNullOrWhiteSpace(fileSavePath))
                    result = ResultMessage<string>.SucceedResult("文件上传成功！", fileSavePath);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return Json(result);
        }
        #endregion


    }
}