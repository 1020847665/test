using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

namespace BestWise.Common.File.Remoting
{
    public static class Request
    {
        private static string remotingIp;
        private static string port;
        private static IFileServer file;
        private static string root;
        private static readonly Regex rgxRoot = new Regex("^([A-Za-z]\\:)?[\\\\]{2}", RegexOptions.IgnoreCase);
        static Request()
        {
            remotingIp = ConfigurationManager.AppSettings["RemotingIp"];
            port = ConfigurationManager.AppSettings["RemotingPort"];
            root = ConfigurationManager.AppSettings["RemotingRoot"];            
            GetObject();
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public static void MakeDirectory(string path)
        {
            path = GetServerPath(path);
            GetObject().MakeDirectory(path);
        }

        /// <summary>
        /// 上传文件到Remoting服务器
        /// </summary>
        /// <param name="Context">上传的二进制数据</param>
        /// <param name="path">文件路径</param>
        public static bool UploadFile(byte[] Context, string path)
        {
            path = GetServerPath(path);
            return GetObject().UploadFile(Context, path);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">要删除的文件路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
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
        public static bool CopyFile(string path, string ObjectPath, string ObjectFilename)
        {
            path = GetServerPath(path);
            ObjectPath = GetServerPath(ObjectPath);
            bool result = GetObject().CopyFile(path, ObjectPath, ObjectFilename);
            return result;
        }

        /// <summary>
        /// 得到Remoting服务文件信息
        /// </summary>
        /// <param name="path">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        public static FileInfo GetFileInfo(string path)
        {
            path = GetServerPath(path);
            FileInfo result = GetObject().GetFileInfo(path);
            return result;
        }

        /// <summary>
        /// 判断Remoting服务文件是否存在
        /// </summary>
        /// <param name="path">Remoting文件的路径</param>
        /// <returns>是否存在</returns>
        public static bool IsExistsByFile(string path)
        {
            path = GetServerPath(path);
            return GetObject().IsExistsByFile(path);
        }

        /// <summary>
        /// 得到Remoting服务文件的大小
        /// </summary>
        /// <param name="path">Remoting文件的路径</param>
        /// <returns>文件的大小</returns>
        public static long ContentLength(string path)
        {
            path = GetServerPath(path);
            return GetObject().ContentLength(path);
        }

        /// <summary>
        /// 得到Remoting服务图片文件的尺寸
        /// </summary>
        /// <param name="path">Remoting图片文件的路径</param>
        /// <returns>图片文件的尺寸</returns>
        public static Size GetPictureSize(string path)
        {
            path = GetServerPath(path);
            return GetObject().GetPictureSize(path);
        }

        /// <summary>
        /// 返回截取Remoting服务图片文件的尺寸后的数据字节
        /// </summary>
        /// <param name="path">Remoting图片文件的路径</param>
        /// <param name="Size">截取后图片文件的尺寸</param>
        /// <returns>截取后图片文件数据字节</returns>
        public static byte[] CutPicture(string path, Size Size)
        {
            path = GetServerPath(path);
            return GetObject().CutPicture(path, Size);
        }

        /// <summary>
        /// 上传图片到远程文件服务器
        /// </summary>
        /// <param name="path">保存图片的路径</param>
        /// <param name="bytes">上传图片的数据字节数组</param>
        /// <param name="Size">保存图片的Size</param>
        /// <returns>是否上传成功！</returns>
        public static bool CutPicture(string path, byte[] bytes, Size Size)
        {
            path = GetServerPath(path);
            return GetObject().CutPicture(path, bytes, Size);
        }

        /// <summary>
        /// 修改Remoting服务文件名
        /// </summary>
        /// <param name="sourceFilePath">Remoting原文件的路径</param>
        /// <param name="destFilePath">Remoting新文件的路径</param>
        /// <returns></returns>
        public static bool UpdateFileName(string sourceFilePath, string destFilePath)
        {
            sourceFilePath = GetServerPath(sourceFilePath);
            destFilePath = GetServerPath(destFilePath);
            bool result = GetObject().UpdateFileName(sourceFilePath, destFilePath);
            return result;
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="FolderInfo">Remoting文件夹信息</param>
        /// <returns>子文件信息列表</returns>
        public static FileInfo[] GetFiles(DirectoryInfo FolderInfo)
        {
            FileInfo[] result = GetObject().GetFiles(FolderInfo);
            return result;
        }


        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="path">Remoting文件夹的路径</param>
        /// <returns>子文件信息列表</returns>
        public static FileInfo[] GetFiles(string path)
        {
            path = GetServerPath(path);
            FileInfo[] result = GetObject().GetFiles(path);
            return result;
        }

        /// <summary>
        /// 得到Remoting服务文件夹信息
        /// </summary>
        /// <param name="path">Remoting文件夹的路径</param>
        /// <returns>文件夹信息</returns>
        public static DirectoryInfo GetDirectoryInfo(string path)
        {
            path = GetServerPath(path);
            DirectoryInfo result = GetObject().GetDirectoryInfo(path);
            return result;
        }

        /// <summary>
        /// 修改Remoting服务文件夹名
        /// </summary>
        /// <param name="sourceFolderPath">Remoting原文件夹的路径</param>
        /// <param name="destFolderPath">Remoting新文件夹的路径</param>
        /// <returns></returns>
        public static bool UpdateDirectoryName(string sourceFolderPath, string destFolderPath)
        {
            sourceFolderPath = GetServerPath(sourceFolderPath);
            destFolderPath = GetServerPath(destFolderPath);
            bool result = GetObject().UpdateDirectoryName(sourceFolderPath, destFolderPath);
            return result;
        }

        /// <summary>
        /// 删除Remoting服务文件夹
        /// </summary>
        /// <param name="path">Remoting文件夹的路径</param>
        /// <returns></returns>
        public static bool DeleteDirectory(string path)
        {
            path = GetServerPath(path);
            bool result = GetObject().DeleteDirectory(path);
            return result;
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="FolderInfo">Remoting文件夹信息</param>
        /// <returns>子文件夹信息列表</returns>
        public static DirectoryInfo[] GetDirectories(DirectoryInfo FolderInfo)
        {
            DirectoryInfo[] result = GetObject().GetDirectories(FolderInfo);
            return result;
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="path">Remoting文件夹的路径</param>
        /// <returns>子文件夹信息列表</returns>
        public static DirectoryInfo[] GetDirectories(string path)
        {
            path = GetServerPath(path);
            DirectoryInfo[] result = GetObject().GetDirectories(path);
            return result;
        }

        /// <summary>
        /// 得到Remoting服务文件二进制数据
        /// </summary>
        /// <param name="path">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        public static byte[] GetFileBytes(string path)
        {
            path = GetServerPath(path);
            byte[] result = GetObject().GetFileBytes(path);
            return result;
        }

        private static IFileServer GetObject()
        {
            if (String.IsNullOrWhiteSpace(remotingIp)) remotingIp = "172.18.253.0";//"127.0.0.1";
            if (String.IsNullOrWhiteSpace(port)) port = "9080";
            if (file == null)
                file = (IFileServer)Activator.GetObject(typeof(RemotingHelper), "tcp://" + remotingIp + ":" + port + "/File");
            return file;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string GetServerPath(string path)
        {
            path=path.Replace("/", "\\");
            if ((!rgxRoot.IsMatch(path)) && (!String.IsNullOrWhiteSpace(root))) path = root.TrimEnd('\\') + "\\" + (path.TrimStart('\\'));
            return path;
        }
    }
}