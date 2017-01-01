using System.Drawing;

namespace BestWise.Common.File
{
    public interface IImage
    {

        /// <summary>
        /// 得到Remoting服务图片文件的尺寸
        /// </summary>
        /// <param name="path">Remoting图片文件的路径</param>
        /// <returns>图片文件的尺寸</returns>
        Size GetSize(string path);

        /// <summary>
        /// 返回截取Remoting服务图片文件的尺寸后的数据字节
        /// </summary>
        /// <param name="path">Remoting图片文件的路径</param>
        /// <param name="szie">截取后图片文件的尺寸</param>
        /// <returns>截取后图片文件数据字节</returns>
        byte[] GetBytes(string path, Size szie);

        /// <summary>
        /// 上传图片到远程文件服务器
        /// </summary>
        /// <param name="path">保存图片的路径</param>
        /// <param name="bytes">上传图片的数据字节数组</param>
        /// <param name="szie">保存图片的Size</param>
        /// <returns>是否上传成功！</returns>
        bool Save(string path, byte[] bytes, Size szie);
    }
}