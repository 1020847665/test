using System.Drawing;

namespace BestWise.Common.File.Remoting
{
    public class Image : IImage
    {
        private static BestWise.Common.File.Remoting.Image _instance;

        /// <summary>
        /// 获取一个实例
        /// </summary>
        /// <returns></returns>
        public static BestWise.Common.File.Remoting.Image Instance()
        {
            if (_instance == null) _instance = new BestWise.Common.File.Remoting.Image();
            return _instance;
        }

        /// <summary>
        /// 获取图片的 Size
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <returns>图片的 Size</returns>
        public Size GetSize(string path)
        {
            return Request.GetPictureSize(path);
        }

        /// <summary>
        /// 获取图片的二进制数据
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="szie">图片的 Size</param>
        /// <returns>二进制数据</returns>
        public byte[] GetBytes(string path, Size szie)
        {
            return Request.CutPicture(path, szie);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="path">图片地址</param>
        /// <param name="bytes">图片二进制数据</param>
        /// <param name="szie">图片的 Size</param>
        /// <returns>是否保存成功</returns>
        public bool Save(string path, byte[] bytes, Size szie)
        {
            return Request.CutPicture(path, bytes, szie);
        }
    }
}