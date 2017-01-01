using System.Configuration.Provider;
using System.Drawing;
using System.IO;

namespace BestWise.Common.File.Providers
{
    public abstract class BaseProvider : ProviderBase
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns></returns>
        public abstract void MakeDirectory(string path);

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="context">上传的二进制数据</param>
        /// <param name="path">文件路径</param>
        public abstract bool UploadFile(byte[] context, string path);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">要删除的文件路径</param>
        /// <returns></returns>
        public abstract bool DeleteFile(string path);

        /// <summary>
        /// Copy文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="objectPath">目标目录</param>
        /// <param name="objectFilename">复制后的文件名，如果为空则为原文件名</param>
        /// <returns></returns>
        public virtual bool CopyFile(string path, string objectPath, string objectFilename)
        {
            return false;
        }

        /// <summary>
        /// 判断服务文件是否存在
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns>是否存在</returns>
        public abstract bool IsExistsByFile(string path);

        ///// <summary>
        ///// 得到服务图片文件的尺寸
        ///// </summary>
        ///// <param name="path">图片文件的路径</param>
        ///// <returns>图片文件的尺寸</returns>
        //public abstract Size GetPictureSize(string path);

        ///// <summary>
        ///// 返回截取服务图片文件的尺寸后的数据字节
        ///// </summary>
        ///// <param name="path">图片文件的路径</param>
        ///// <param name="Size">截取后图片文件的尺寸</param>
        ///// <returns>截取后图片文件数据字节</returns>
        //public abstract byte[] CutPicture(string path, Size Size);

        ///// <summary>
        ///// 上传图片到远程文件服务器
        ///// </summary>
        ///// <param name="path">保存图片的路径</param>
        ///// <param name="bytes">上传图片的数据字节数组</param>
        ///// <param name="Size">保存图片的Size</param>
        ///// <returns>是否上传成功！</returns>
        //public virtual bool CutPicture(string path, byte[] bytes, Size Size);

        ///// <summary>
        ///// 修改服务文件名
        ///// </summary>
        ///// <param name="sourceFilePath">原文件的路径</param>
        ///// <param name="destFilePath">Remoting新文件的路径</param>
        ///// <returns></returns>
        //public abstract bool UpdateFileName(string sourceFilePath, string destFilePath);

        ///// <summary>
        ///// 得到服务文件夹子文件信息列表
        ///// </summary>
        ///// <param name="FolderInfo">文件夹信息</param>
        ///// <returns>子文件信息列表</returns>
        //public abstract FileInfo[] GetFiles(DirectoryInfo FolderInfo);


        ///// <summary>
        ///// 得到服务文件夹子文件信息列表
        ///// </summary>
        ///// <param name="path">文件夹的路径</param>
        ///// <returns>子文件信息列表</returns>
        //public abstract FileInfo[] GetFiles(string path);

        ///// <summary>
        ///// 得到服务文件夹信息
        ///// </summary>
        ///// <param name="path">文件夹的路径</param>
        ///// <returns>文件夹信息</returns>
        //public abstract DirectoryInfo GetDirectoryInfo(string path);

        ///// <summary>
        ///// 修改服务文件夹名
        ///// </summary>
        ///// <param name="sourceFolderPath">原文件夹的路径</param>
        ///// <param name="destFolderPath">新文件夹的路径</param>
        ///// <returns></returns>
        //public abstract bool UpdateDirectoryName(string sourceFolderPath, string destFolderPath);

        ///// <summary>
        ///// 删除服务文件夹
        ///// </summary>
        ///// <param name="path">文件夹的路径</param>
        ///// <returns></returns>
        //public abstract bool DeleteDirectory(string path);

        ///// <summary>
        ///// 得到服务文件夹子文件夹信息列表
        ///// </summary>
        ///// <param name="FolderInfo">文件夹信息</param>
        ///// <returns>子文件夹信息列表</returns>
        //public abstract DirectoryInfo[] GetDirectories(DirectoryInfo FolderInfo);

        ///// <summary>
        ///// 得到服务文件夹子文件夹信息列表
        ///// </summary>
        ///// <param name="path">文件夹的路径</param>
        ///// <returns>子文件夹信息列表</returns>
        //public abstract DirectoryInfo[] GetDirectories(string path);

        /// <summary>
        /// 得到服务文件二进制数据
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns>文件信息</returns>
        public abstract byte[] GetFileBytes(string path);
    }
}
