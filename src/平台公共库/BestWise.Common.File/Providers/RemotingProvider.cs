using BestWise.Common.File.Remoting;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BestWise.Common.File.Providers
{
    public class RemotingProvider : BaseProvider
    {
        private IFileServer file;
        private static readonly Regex rgxRoot = new Regex("^([A-Za-z]\\:)?[\\\\]{2}", RegexOptions.IgnoreCase);

        /// <summary>
        /// RemotingIP
        /// </summary>
        protected string RemotingIp { get; set; }

        /// <summary>
        /// RemotingPort 
        /// </summary>
        protected int RemotingPort { get; set; }

        /// <summary>
        /// RemotingRoot 
        /// </summary>
        protected string RemotingRoot { get; set; }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public override void MakeDirectory(string path)
        {
            path = GetServerPath(path);
            GetObject().MakeDirectory(path);
        }

        /// <summary>
        /// 上传文件到Remoting服务器
        /// </summary>
        /// <param name="Context">上传的二进制数据</param>
        /// <param name="path">文件路径</param>
        public override bool UploadFile(byte[] Context, string path)
        {
            path = GetServerPath(path);
            return GetObject().UploadFile(Context, path);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">要删除的文件路径</param>
        /// <returns></returns>
        public override bool DeleteFile(string path)
        {
            path = GetServerPath(path);
            return GetObject().DeleteFile(path);
        }

        /// <summary>
        /// Copy文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="ObjectPath">目标目录</param>
        /// <param name="ObjectFilename">复制后的文件名，如果为空则为原文件名</param>
        /// <returns></returns>
        public override bool CopyFile(string path, string ObjectPath, string ObjectFilename)
        {
            path = GetServerPath(path);
            ObjectPath = GetServerPath(ObjectPath);
            bool result = GetObject().CopyFile(path, ObjectPath, ObjectFilename);
            return result;
        }

        ///// <summary>
        ///// 得到Remoting服务文件信息
        ///// </summary>
        ///// <param name="path">Remoting文件的路径</param>
        ///// <returns>文件信息</returns>
        //public override FileInfo GetFileInfo(string path)
        //{
        //    path = GetServerPath(path);
        //    FileInfo result = GetObject().GetFileInfo(path);
        //    return result;
        //}

        /// <summary>
        /// 判断Remoting服务文件是否存在
        /// </summary>
        /// <param name="path">Remoting文件的路径</param>
        /// <returns>是否存在</returns>
        public override bool IsExistsByFile(string path)
        {
            path = GetServerPath(path);
            return GetObject().IsExistsByFile(path);
        }

        ///// <summary>
        ///// 得到Remoting服务文件的大小
        ///// </summary>
        ///// <param name="path">Remoting文件的路径</param>
        ///// <returns>文件的大小</returns>
        //public override long ContentLength(string path)
        //{
        //    path = GetServerPath(path);
        //    return GetObject().ContentLength(path);
        //}

        ///// <summary>
        ///// 得到Remoting服务图片文件的尺寸
        ///// </summary>
        ///// <param name="path">Remoting图片文件的路径</param>
        ///// <returns>图片文件的尺寸</returns>
        //public override Size GetPictureSize(string path)
        //{
        //    path = GetServerPath(path);
        //    return GetObject().GetPictureSize(path);
        //}

        ///// <summary>
        ///// 返回截取Remoting服务图片文件的尺寸后的数据字节
        ///// </summary>
        ///// <param name="path">Remoting图片文件的路径</param>
        ///// <param name="Size">截取后图片文件的尺寸</param>
        ///// <returns>截取后图片文件数据字节</returns>
        //public override byte[] CutPicture(string path, Size Size)
        //{
        //    path = GetServerPath(path);
        //    return GetObject().CutPicture(path, Size);
        //}

        ///// <summary>
        ///// 上传图片到远程文件服务器
        ///// </summary>
        ///// <param name="path">保存图片的路径</param>
        ///// <param name="bytes">上传图片的数据字节数组</param>
        ///// <param name="Size">保存图片的Size</param>
        ///// <returns>是否上传成功！</returns>
        //public override bool CutPicture(string path, byte[] bytes, Size Size)
        //{
        //    path = GetServerPath(path);
        //    return GetObject().CutPicture(path, bytes, Size);
        //}

        ///// <summary>
        ///// 修改Remoting服务文件名
        ///// </summary>
        ///// <param name="sourceFilePath">Remoting原文件的路径</param>
        ///// <param name="destFilePath">Remoting新文件的路径</param>
        ///// <returns></returns>
        //public override bool UpdateFileName(string sourceFilePath, string destFilePath)
        //{
        //    sourceFilePath = GetServerPath(sourceFilePath);
        //    destFilePath = GetServerPath(destFilePath);
        //    bool result = GetObject().UpdateFileName(sourceFilePath, destFilePath);
        //    return result;
        //}

        ///// <summary>
        ///// 得到Remoting服务文件夹子文件信息列表
        ///// </summary>
        ///// <param name="FolderInfo">Remoting文件夹信息</param>
        ///// <returns>子文件信息列表</returns>
        //public override FileInfo[] GetFiles(DirectoryInfo FolderInfo)
        //{
        //    FileInfo[] result = GetObject().GetFiles(FolderInfo);
        //    return result;
        //}


        ///// <summary>
        ///// 得到Remoting服务文件夹子文件信息列表
        ///// </summary>
        ///// <param name="path">Remoting文件夹的路径</param>
        ///// <returns>子文件信息列表</returns>
        //public override FileInfo[] GetFiles(string path)
        //{
        //    path = GetServerPath(path);
        //    FileInfo[] result = GetObject().GetFiles(path);
        //    return result;
        //}

        ///// <summary>
        ///// 得到Remoting服务文件夹信息
        ///// </summary>
        ///// <param name="path">Remoting文件夹的路径</param>
        ///// <returns>文件夹信息</returns>
        //public override DirectoryInfo GetDirectoryInfo(string path)
        //{
        //    path = GetServerPath(path);
        //    DirectoryInfo result = GetObject().GetDirectoryInfo(path);
        //    return result;
        //}

        ///// <summary>
        ///// 修改Remoting服务文件夹名
        ///// </summary>
        ///// <param name="sourceFolderPath">Remoting原文件夹的路径</param>
        ///// <param name="destFolderPath">Remoting新文件夹的路径</param>
        ///// <returns></returns>
        //public override bool UpdateDirectoryName(string sourceFolderPath, string destFolderPath)
        //{
        //    sourceFolderPath = GetServerPath(sourceFolderPath);
        //    destFolderPath = GetServerPath(destFolderPath);
        //    bool result = GetObject().UpdateDirectoryName(sourceFolderPath, destFolderPath);
        //    return result;
        //}

        ///// <summary>
        ///// 删除Remoting服务文件夹
        ///// </summary>
        ///// <param name="path">Remoting文件夹的路径</param>
        ///// <returns></returns>
        //public override bool DeleteDirectory(string path)
        //{
        //    path = GetServerPath(path);
        //    bool result = GetObject().DeleteDirectory(path);
        //    return result;
        //}

        ///// <summary>
        ///// 得到Remoting服务文件夹子文件夹信息列表
        ///// </summary>
        ///// <param name="FolderInfo">Remoting文件夹信息</param>
        ///// <returns>子文件夹信息列表</returns>
        //public override DirectoryInfo[] GetDirectories(DirectoryInfo FolderInfo)
        //{
        //    DirectoryInfo[] result = GetObject().GetDirectories(FolderInfo);
        //    return result;
        //}

        ///// <summary>
        ///// 得到Remoting服务文件夹子文件夹信息列表
        ///// </summary>
        ///// <param name="path">Remoting文件夹的路径</param>
        ///// <returns>子文件夹信息列表</returns>
        //public override DirectoryInfo[] GetDirectories(string path)
        //{
        //    path = GetServerPath(path);
        //    DirectoryInfo[] result = GetObject().GetDirectories(path);
        //    return result;
        //}

        /// <summary>
        /// 得到Remoting服务文件二进制数据
        /// </summary>
        /// <param name="path">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        public override byte[] GetFileBytes(string path)
        {
            path = GetServerPath(path);
            byte[] result = GetObject().GetFileBytes(path);
            return result;
        }

        private IFileServer GetObject()
        {
            if (file == null)
                file = (IFileServer)Activator.GetObject(typeof(RemotingHelper), "tcp://" + this.RemotingIp + ":" + this.RemotingPort + "/File");
            return file;
        }

        /// <summary>
        /// 获取服务器地址
        /// </summary>
        /// <param name="path">地址</param>
        /// <returns></returns>
        private string GetServerPath(string path)
        {
            path = path.Replace("/", "\\");
            if ((!rgxRoot.IsMatch(path)) && (!String.IsNullOrWhiteSpace(this.RemotingRoot))) path = this.RemotingRoot.TrimEnd('\\') + "\\" + (path.TrimStart('\\'));
            return path;
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if(config!=null && config.Count>0)
            {
                this.RemotingIp = config["ip"];
                this.RemotingPort = config["port"].GetInt();
                this.RemotingRoot = config["root"];      
            }
            base.Initialize(name, config);
        }
    }
}
