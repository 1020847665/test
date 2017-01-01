using System.Drawing;
using System.IO;

namespace BestWise.Common.File
{
    public interface IFileServer
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="strDirName"></param>
        bool MakeDirectory(string strDirName);

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="SavePath"></param>
        /// <returns></returns>
        bool UploadFile(byte[] Context, string SavePath);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        bool DeleteFile(string FilePath);

        /// <summary>
        /// Copy文件
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="ObjectPath"></param>
        /// <param name="ObjectFilename"></param>
        /// <returns></returns>
        bool CopyFile(string FilePath, string ObjectPath, string ObjectFilename);

        /// <summary>
        /// 得到Remoting服务文件信息
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        FileInfo GetFileInfo(string FilePath);

        /// <summary>
        /// 判断Remoting服务文件是否存在
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>是否存在</returns>
        bool IsExistsByFile(string FilePath);

        /// <summary>
        /// 得到Remoting服务文件的大小
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>文件的大小</returns>
        long ContentLength(string FilePath);

        /// <summary>
        /// 得到Remoting服务图片文件的尺寸
        /// </summary>
        /// <param name="FilePath">Remoting图片文件的路径</param>
        /// <returns>图片文件的尺寸</returns>
        Size GetPictureSize(string FilePath);

        /// <summary>
        /// 返回截取Remoting服务图片文件的尺寸后的数据字节
        /// </summary>
        /// <param name="FilePath">Remoting图片文件的路径</param>
        /// <param name="Size">截取后图片文件的尺寸</param>
        /// <returns>截取后图片文件数据字节</returns>
        byte[] CutPicture(string FilePath, Size Size);

        /// <summary>
        /// 上传图片到远程文件服务器
        /// </summary>
        /// <param name="path">保存图片的路径</param>
        /// <param name="bytes">上传图片的数据字节数组</param>
        /// <param name="Size">保存图片的Size</param>
        /// <returns>是否上传成功！</returns>
        bool CutPicture(string path, byte[] bytes, Size Size);

        /// <summary>
        /// 修改Remoting服务文件名
        /// </summary>
        /// <param name="sourceFilePath">Remoting原文件的路径</param>
        /// <param name="destFilePath">Remoting新文件的路径</param>
        /// <returns></returns>
        bool UpdateFileName(string sourceFilePath, string destFilePath);

        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹信息</param>
        /// <returns>子文件信息列表</returns>
        FileInfo[] GetFiles(DirectoryInfo FolderInfo);

        /// <summary>
        /// 得到Remoting服务文件夹子文件信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹的路径</param>
        /// <returns>子文件信息列表</returns>
        FileInfo[] GetFiles(string FolderPath);

        /// <summary>
        /// 得到Remoting服务文件夹信息
        /// </summary>
        /// <param name="FilePath">Remoting文件夹的路径</param>
        /// <returns>文件夹信息</returns>
        DirectoryInfo GetDirectoryInfo(string FolderPath);

        /// <summary>
        /// 修改Remoting服务文件夹名
        /// </summary>
        /// <param name="sourceFolderPath">Remoting原文件夹的路径</param>
        /// <param name="destFolderPath">Remoting新文件夹的路径</param>
        /// <returns></returns>
        bool UpdateDirectoryName(string sourceFolderPath, string destFolderPath);

        /// <summary>
        /// 删除Remoting服务文件夹
        /// </summary>
        /// <param name="FolderPath">Remoting文件夹的路径</param>
        /// <returns></returns>
        bool DeleteDirectory(string FolderPath);

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹信息</param>
        /// <returns>子文件夹信息列表</returns>
        DirectoryInfo[] GetDirectories(DirectoryInfo FolderInfo);

        /// <summary>
        /// 得到Remoting服务文件夹子文件夹信息列表
        /// </summary>
        /// <param name="FilePath">Remoting文件夹的路径</param>
        /// <returns>子文件夹信息列表</returns>
        DirectoryInfo[] GetDirectories(string FolderPath);

        /// <summary>
        /// 得到Remoting服务文件二进制数据
        /// </summary>
        /// <param name="FilePath">Remoting文件的路径</param>
        /// <returns>文件信息</returns>
        byte[] GetFileBytes(string FilePath);
    }
}