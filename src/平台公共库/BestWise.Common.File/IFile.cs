using System.Collections;

namespace BestWise.Common.File
{
    /// <summary>
    /// 文件的操作接口
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>是否存在</returns>
        bool Exists(string path);

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>删除是否成功</returns>
        bool Delete(string path);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <param name="bytes">文件二进制数据</param>
        /// <returns>保存文件是否成功！</returns>
        bool Save(string path, byte[] bytes);

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>文件的二进制数据</returns>
        byte[] Read(string path);

        /// <summary>
        /// 读取文件信息
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>文件信息</returns>
        Hashtable GetFileInfo(string path);
    }
}
