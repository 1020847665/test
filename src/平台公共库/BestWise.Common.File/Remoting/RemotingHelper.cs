using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.Common.File.Remoting
{
    public class RemotingHelper : System.MarshalByRefObject,IFileServer
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="strDirName">目录路径</param>
        /// <returns></returns>
        public bool MakeDirectory(string strDirName)
        {
            return Cache.GetInstance().MakeDirectory(strDirName);
        }

        /// <summary>
        /// 上传文件到FTP服务器
        /// </summary>
        /// <param name="context">上传的二进制数据</param>
        /// <param name="savePath">文件路径</param>
        public bool UploadFile(byte[] context, string savePath)
        {
            return Cache.GetInstance().UploadFile(context, savePath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">要删除的文件路径</param>
        /// <returns></returns>
        public bool DeleteFile(string filePath)
        {
            return Cache.GetInstance().DeleteFile(filePath);
        }

        /// <summary>
        /// Copy文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="objectPath">目标目录</param>
        /// <param name="objectFilename">复制后的文件名，如果为空则为原文件名</param>
        /// <returns></returns>
        public bool CopyFile(string filePath, string objectPath, string objectFilename)
        {
            return Cache.GetInstance().CopyFile(filePath, objectPath, objectFilename);
        }

        /// <summary>
        /// 得到Remoting服务文件信息
        /// </summary>
        /// <param name="filePath">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        public FileInfo GetFileInfo(string filePath)
        {
            return Cache.GetInstance().GetFileInfo(filePath);
        }

        /// <summary>
        /// 判断Remoting服务文件是否存在
        /// </summary>
        /// <param name="filePath">Remoting文件的路径</param>
        /// <returns>是否存在</returns>
        public bool IsExistsByFile(string filePath)
        {
            return Cache.GetInstance().IsExistsByFile(filePath);
        }

        /// <summary>
        /// 得到Remoting服务文件的大小
        /// </summary>
        /// <param name="filePath">Remoting文件的路径</param>
        /// <returns>文件的大小</returns>
        public long ContentLength(string filePath)
        {
            return Cache.GetInstance().ContentLength(filePath);
        }

        /// <summary>
        /// 得到Remoting服务图片文件的尺寸
        /// </summary>
        /// <param name="filePath">Remoting图片文件的路径</param>
        /// <returns>图片文件的尺寸</returns>
        public Size GetPictureSize(string filePath)
        {
            return Cache.GetInstance().GetPictureSize(filePath);
        }

        /// <summary>
        /// 返回截取Remoting服务图片文件的尺寸后的数据字节
        /// </summary>
        /// <param name="filePath">Remoting图片文件的路径</param>
        /// <param name="size">截取后图片文件的尺寸</param>
        /// <returns>截取后图片文件数据字节</returns>
        public byte[] CutPicture(string filePath, Size size)
        {
            return Cache.GetInstance().CutPicture(filePath, size);
        }

        /// <summary>
        /// 上传图片到远程文件服务器
        /// </summary>
        /// <param name="path">保存图片的路径</param>
        /// <param name="bytes">上传图片的数据字节数组</param>
        /// <param name="Size">保存图片的Size</param>
        /// <returns>是否上传成功！</returns>
        public bool CutPicture(string path, byte[] bytes, Size Size)
        {
            return Cache.GetInstance().CutPicture(path, bytes, Size);
        }

        /// <summary>
        /// 修改Remoting服务文件名
        /// </summary>
        /// <param name="sourceFilePath">Remoting原文件的路径</param>
        /// <param name="destFilePath">Remoting新文件的路径</param>
        /// <returns></returns>
        public bool UpdateFileName(string sourceFilePath, string destFilePath)
        {
            return Cache.GetInstance().UpdateFileName(sourceFilePath, destFilePath);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="folderInfo">Remoting文件夹信息</param>
        /// <returns>子文件信息列表</returns>
        public FileInfo[] GetFiles(DirectoryInfo folderInfo)
        {
            return Cache.GetInstance().GetFiles(folderInfo);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="folderPath">Remoting文件夹的路径</param>
        /// <returns>子文件信息列表</returns>
        public FileInfo[] GetFiles(string folderPath)
        {
            return Cache.GetInstance().GetFiles(folderPath);
        }

        /// <summary>
        /// 得到Remoting服务文件夹信息
        /// </summary>
        /// <param name="folderPath">Remoting文件夹的路径</param>
        /// <returns>文件夹信息</returns>
        public DirectoryInfo GetDirectoryInfo(string folderPath)
        {
            return Cache.GetInstance().GetDirectoryInfo(folderPath);
        }

        /// <summary>
        /// 修改Remoting服务文件夹名
        /// </summary>
        /// <param name="sourceFolderPath">Remoting原文件夹的路径</param>
        /// <param name="destFolderPath">Remoting新文件夹的路径</param>
        /// <returns></returns>
        public bool UpdateDirectoryName(string sourceFolderPath, string destFolderPath)
        {
            return Cache.GetInstance().UpdateDirectoryName(sourceFolderPath, destFolderPath);
        }

        /// <summary>
        /// 删除Remoting服务文件夹
        /// </summary>
        /// <param name="folderPath">Remoting文件夹的路径</param>
        /// <returns></returns>
        public bool DeleteDirectory(string folderPath)
        {
            return Cache.GetInstance().DeleteDirectory(folderPath);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="folderInfo">Remoting文件夹信息</param>
        /// <returns>子文件夹信息列表</returns>
        public DirectoryInfo[] GetDirectories(DirectoryInfo folderInfo)
        {
            return Cache.GetInstance().GetDirectories(folderInfo);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="folderPath">Remoting文件夹的路径</param>
        /// <returns>子文件夹信息列表</returns>
        public DirectoryInfo[] GetDirectories(string folderPath)
        {
            return Cache.GetInstance().GetDirectories(folderPath);
        }

        /// <summary>
        /// 得到Remoting服务文件二进制数据
        /// </summary>
        /// <param name="filePath">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        public byte[] GetFileBytes(string filePath)
        {
            return Cache.GetInstance().GetFileBytes(filePath);
        }
    }
}