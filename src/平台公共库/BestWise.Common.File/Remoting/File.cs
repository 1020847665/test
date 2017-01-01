using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BestWise.Common.File.Remoting
{
    public class File : IFile
    {
        private static BestWise.Common.File.Remoting.File _instance;

        /// <summary>
        /// 获取一个实例
        /// </summary>
        /// <returns></returns>
        public static BestWise.Common.File.Remoting.File Instance()
        {
            if (_instance == null) _instance = new BestWise.Common.File.Remoting.File();
            return _instance;
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>是否存在</returns>
        public bool Exists(string path)
        {
            return Request.IsExistsByFile(path);
        }

        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>删除是否成功</returns>
        public bool Delete(string path)
        {
            return Request.DeleteFile(path);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <param name="bytes">文件二进制数据</param>
        /// <returns>保存文件是否成功！</returns>
        public bool Save(string path, byte[] bytes)
        {
            try
            {
                return Request.UploadFile(bytes, path);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>文件的二进制数据</returns>
        public byte[] Read(string path)
        {
            return Request.GetFileBytes(path);
        }

        /// <summary>
        /// 读取文件信息
        /// </summary>
        /// <param name="path">文件的Cdn路径</param>
        /// <returns>文件信息</returns>
        public Hashtable GetFileInfo(string path)
        {
            Hashtable result = new Hashtable();
            FileInfo file = Request.GetFileInfo(path);
            foreach (PropertyInfo p in typeof(FileInfo).GetPropertiesCache())
            {
                if (!result.ContainsKey(p.Name))
                    result.Add(p.Name, p.GetValue(file,null));
                else
                    result[p.Name] = p.GetValue(file, null);
            }
            return result;
        }
    }
}