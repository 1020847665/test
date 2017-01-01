using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.Common.File
{
    public class Cache
    {
        private static Cache myInstance;
        public static IFileServer Observer;

        private Cache()
        {
        }

        public static void Attach(IFileServer observer)
        {
            Observer = observer;
        }
        public static Cache GetInstance()
        {
            if (myInstance == null)
            {
                myInstance = new Cache();
            }
            return myInstance;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        public bool MakeDirectory(string strDirName)
        {
            return Observer.MakeDirectory(strDirName);
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="SavePath"></param>
        /// <returns></returns>
        public bool UploadFile(byte[] Context, string SavePath)
        {
            return Observer.UploadFile(Context, SavePath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public bool DeleteFile(string FilePath)
        {
            return Observer.DeleteFile(FilePath);
        }

        /// <summary>
        /// Copy文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="ObjectPath"></param>
        /// <param name="ObjectFilename"></param>
        /// <returns></returns>
        public bool CopyFile(string FilePath, string ObjectPath, string ObjectFilename)
        {
            return Observer.CopyFile(FilePath, ObjectPath, ObjectFilename);
        }

        /// <summary>
        /// 得到Remoting服务文件信息
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        public FileInfo GetFileInfo(string FilePath)
        {
            return Observer.GetFileInfo(FilePath);
        }

        /// <summary>
        /// 判断Remoting服务文件是否存在
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>是否存在</returns>
        public bool IsExistsByFile(string FilePath)
        {
            return Observer.IsExistsByFile(FilePath);
        }

        /// <summary>
        /// 得到Remoting服务文件的大小
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>文件的大小</returns>
        public long ContentLength(string FilePath)
        {
            return Observer.ContentLength(FilePath);
        }

        /// <summary>
        /// 得到Remoting服务图片文件的尺寸
        /// </summary>
        /// <param name="FilePath">Remoting图片文件的路径</param>
        /// <returns>图片文件的尺寸</returns>
        public Size GetPictureSize(string FilePath)
        {
            return Observer.GetPictureSize(FilePath);
        }

        /// <summary>
        /// 返回截取Remoting服务图片文件的尺寸后的数据字节
        /// </summary>
        /// <param name="FilePath">Remoting图片文件的路径</param>
        /// <param name="Size">截取后图片文件的尺寸</param>
        /// <returns>截取后图片文件数据字节</returns>
        public byte[] CutPicture(string FilePath, Size Size)
        {
            return Observer.CutPicture(FilePath, Size);
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
            return Observer.CutPicture(path, bytes, Size);
        }

        /// <summary>
        /// 修改Remoting服务文件名
        /// </summary>
        /// <param name="sourceFilePath">Remoting原文件的路径</param>
        /// <param name="destFilePath">Remoting新文件的路径</param>
        /// <returns></returns>
        public bool UpdateFileName(string sourceFilePath, string destFilePath)
        {
            return Observer.UpdateFileName(sourceFilePath, destFilePath);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹信息</param>
        /// <returns>子文件信息列表</returns>
        public FileInfo[] GetFiles(DirectoryInfo FolderInfo)
        {
            return Observer.GetFiles(FolderInfo);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹的路径</param>
        /// <returns>子文件信息列表</returns>
        public FileInfo[] GetFiles(string FolderPath)
        {
            return Observer.GetFiles(FolderPath);
        }

        /// <summary>
        /// 得到Remoting服务文件夹信息
        /// </summary>
        /// <param name="FilePath">Remoting文件夹的路径</param>
        /// <returns>文件夹信息</returns>
        public DirectoryInfo GetDirectoryInfo(string FolderPath)
        {
            return Observer.GetDirectoryInfo(FolderPath);
        }

        /// <summary>
        /// 修改Remoting服务文件夹名
        /// </summary>
        /// <param name="sourceFolderPath">Remoting原文件夹的路径</param>
        /// <param name="destFolderPath">Remoting新文件夹的路径</param>
        /// <returns></returns>
        public bool UpdateDirectoryName(string sourceFolderPath, string destFolderPath)
        {
            return Observer.UpdateDirectoryName(sourceFolderPath, destFolderPath);
        }

        /// <summary>
        /// 删除Remoting服务文件夹
        /// </summary>
        /// <param name="FolderPath">Remoting文件夹的路径</param>
        /// <returns></returns>
        public bool DeleteDirectory(string FolderPath)
        {
            return Observer.DeleteDirectory(FolderPath);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹信息</param>
        /// <returns>子文件夹信息列表</returns>
        public DirectoryInfo[] GetDirectories(DirectoryInfo FolderInfo)
        {
            return Observer.GetDirectories(FolderInfo);
        }

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹的路径</param>
        /// <returns>子文件夹信息列表</returns>
        public DirectoryInfo[] GetDirectories(string FolderPath)
        {
            return Observer.GetDirectories(FolderPath);
        }

        /// <summary>
        /// 得到Remoting服务文件二进制数据
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        public byte[] GetFileBytes(string FilePath)
        {
            return Observer.GetFileBytes(FilePath);
        }
    }
}