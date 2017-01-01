using System;
using System.Linq;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace BestWise.Common.File.Remoting
{
    /// <summary>
    /// 文件夹的操作类
    /// </summary>
    public class Directory : IDirectory
    {
        private static BestWise.Common.File.Remoting.Directory _instance;

        /// <summary>
        /// 获取一个实例
        /// </summary>
        /// <returns></returns>
        public static BestWise.Common.File.Remoting.Directory Instance()
        {
            if (_instance == null) _instance = new BestWise.Common.File.Remoting.Directory();
            return _instance;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>创建是否成功</returns>
        public bool Create(string path)
        {
            bool result = false;
            try
            {
                Request.MakeDirectory(path);
                result=true;
            }
            catch(Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>删除是否成功</returns>
        public bool Delete(string path)
        {
            return Request.DeleteDirectory(path);
        }

        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>目录信息数组列表</returns>
        public ArrayList GetInfo(string path)
        {
            if (String.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(path);
            ArrayList result = new ArrayList();
            try
            {
                result.AddRange(Request.GetDirectories(path));
                result.AddRange(Request.GetFiles(path).Select<FileInfo, FolderItem>(m => new FolderItem(m.FullName, FileType.File)).ToArray());
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        /// <summary>
        /// 获取目录的占用信息(未实现)
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>目录的占用信息(失败返回 null)</returns>
        public int? GetFolderUsage(string path)
        {
            return null;
            //return Request.Directory.GetFolderUsage(path);
        }

        /// <summary>
        /// 获取空间占用信息(未实现)
        /// </summary>
        /// <returns>空间占用信息(失败返回 null)</returns>
        public int? GetFolderUsage()
        {
            return null;
            //return Request.Directory.GetFolderUsage(path);
        }
    }
    public class FolderItem
    {
        /// <summary>
        /// 文件/目录名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 类型 N (文件)、F (目录)
        /// </summary>
        public FileType FileType { get; set; }

        public FolderItem(string fileName, FileType fileType)
        {
            this.FileName = fileName;
            this.FileType = fileType;
        }
    }
}