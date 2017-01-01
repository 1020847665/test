using System.Collections;

namespace BestWise.Common.File
{
    /// <summary>
    /// 目录的操作接口
    /// </summary>
    public interface IDirectory
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>创建是否成功</returns>
        bool Create(string path);

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>删除是否成功</returns>
        bool Delete(string path);

        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>目录信息数组列表</returns>
        ArrayList GetInfo(string path);

        /// <summary>
        /// 获取目录的占用信息
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <returns>目录的占用信息(失败返回 null)</returns>
        int? GetFolderUsage(string path);

        /// <summary>
        /// 获取空间占用信息
        /// </summary>
        /// <returns>空间占用信息(失败返回 null)</returns>
        int? GetFolderUsage();
    }
}
