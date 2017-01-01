using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BestWise.Common;
using Aspose.Cells;
using Aspose.Slides;
using Aspose.Words;
using SaveFormat = Aspose.Words.SaveFormat;

namespace BestWise.YLS.Manage.FrameWork
{
    /// <summary>
    /// 将文件转换成swf文件的辅助类
    /// </summary>
    public class SwfHelper
    {
        private static readonly string pdfDic = "pdf/";
        private static readonly string swfDic = "swf/";

        /// <summary>
        /// 根据源文件获取其swf文件路径
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <returns></returns>
        public static string GetSwfFilePath(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath))
            {
                return string.Empty;
            }
            string pdf = sourcePath.Substring(0, sourcePath.LastIndexOf("/") + 1) + pdfDic;
            string swf = sourcePath.Substring(0, sourcePath.LastIndexOf("/") + 1) + swfDic;

            DirFile.CreateDir(pdf);
            DirFile.CreateDir(swf);
            string serverPath = HttpContext.Current.Server.MapPath(sourcePath);
            string pdfFileName = Path.GetFileNameWithoutExtension(serverPath) + ".pdf";
            string swfFileName = Path.GetFileNameWithoutExtension(serverPath) + ".swf";

            string pdfSavePath = pdf + pdfFileName;//pdf文件相对路径
            string swfSavePath = swf + swfFileName;//swf文件相对路径
            //有的话直接返回swf文件
            if (File.Exists(HttpContext.Current.Server.MapPath(swfSavePath)))
            {
                return swfSavePath;
            }
            string pdfFilePath = HttpContext.Current.Server.MapPath(pdfSavePath);//pdf绝对路径
            string swfFilePath = HttpContext.Current.Server.MapPath(swfSavePath);//swf绝对路径
            string ext = Path.GetExtension(serverPath).ToLower();
            switch (ext)
            {
                case ".pdf":
                    SwfHelper.PDF2SWF(sourcePath, swfSavePath);
                    break;
                case ".txt":
                    SwfHelper.TxtToPDF(serverPath, pdfFilePath);
                    SwfHelper.PDF2SWF(pdfSavePath, swfSavePath);
                    break;
                case ".doc":
                case ".docx":
                    SwfHelper.DOCToPDF(serverPath, pdfFilePath);
                    SwfHelper.PDF2SWF(pdfSavePath, swfSavePath);
                    break;
                case ".ppt":
                case ".pptx":
                    SwfHelper.PptToPDF(serverPath, pdfFilePath);
                    SwfHelper.PDF2SWF(pdfSavePath, swfSavePath);
                    break;
                case ".xls":
                case ".xlsx":
                    SwfHelper.ExcelToPDF(serverPath, pdfFilePath);
                    SwfHelper.PDF2SWF(pdfSavePath, swfSavePath);
                    break;
            }
            return swfSavePath;
        }

        /// <summary>
        /// 转换所有的页，图片质量80%
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        private static bool PDF2SWF(string pdfPath, string swfPath)
        {
            string path = HttpContext.Current.Server.MapPath(pdfPath);
            if (File.Exists(path))
            {
                return PDF2SWF(pdfPath, swfPath, 1, GetPageCount(path), 100);
            }
            return false;
        }


        /// <summary>
        /// 用Aspose.Words进行Word转Pdf
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns></returns>
        private static bool DOCToPDF(string sourcePath, string targetPath)
        {
            try
            {
                Document doc = new Document(sourcePath);
                //保存为PDF文件，此处的SaveFormat支持很多种格式，如图片，epub,rtf 等等
                doc.Save(targetPath, SaveFormat.Pdf);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 用Aspose.Words进行Word转Pdf
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns></returns>
        private static bool TxtToPDF(string sourcePath, string targetPath)
        {
            try
            {
                var encoding = BOMStream.GetEncoding(sourcePath);
                StreamReader sr = new StreamReader(sourcePath, encoding);
                var content = sr.ReadToEnd();
                sr.Close();
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(content));
                Document doc = new Document(ms);
                ms.Close();
                //保存为PDF文件，此处的SaveFormat支持很多种格式，如图片，epub,rtf 等等
                doc.Save(targetPath, SaveFormat.Pdf);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 用Aspose.Words进行Word转Pdf
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns></returns>
        private static bool ExcelToPDF(string sourcePath, string targetPath)
        {
            try
            {
                Workbook excel = new Workbook(sourcePath);
                //保存为PDF文件，此处的SaveFormat支持很多种格式，如图片，epub,rtf 等等
                excel.Save(targetPath, Aspose.Cells.SaveFormat.Pdf);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 用Aspose.Words进行Word转Pdf
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns></returns>
        private static bool PptToPDF(string sourcePath, string targetPath)
        {
            try
            {
                Presentation ppt = new Presentation(sourcePath);
                //保存为PDF文件，此处的SaveFormat支持很多种格式，如图片，epub,rtf 等等
                ppt.Save(targetPath, Aspose.Slides.Export.SaveFormat.Pdf);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///// <summary>
        ///// Word转换成pdf   //原来用office组件进行转换 备份
        ///// </summary>
        ///// <param name="sourcePath">源文件路径</param>
        ///// <param name="targetPath">目标文件路径</param>
        ///// <returns>true=转换成功</returns>
        //private static bool DOCToPDF_bak(string sourcePath, string targetPath)
        //{
        //    Word.Application appWord = null;
        //    Word.Document wordDocument = null;
        //    Object saveChanges = Word.WdSaveOptions.wdSaveChanges;
        //    Object originalFormat = Type.Missing;
        //    Object routeDocument = Type.Missing;
        //    try
        //    {
        //        if (appWord == null)
        //        {
        //            appWord = new Word.Application();
        //        }
        //        wordDocument = appWord.Documents.Open(sourcePath);
        //        wordDocument.ExportAsFixedFormat(targetPath, Word.WdExportFormat.wdExportFormatPDF);
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        if (wordDocument != null)
        //        {
        //            wordDocument.Close(ref saveChanges, ref originalFormat, ref routeDocument);
        //        }
        //        if (appWord != null)
        //        {
        //            appWord.Quit(Type.Missing, Type.Missing, Type.Missing);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 把Excel文件转换成pdf文件
        ///// </summary>
        ///// <param name="sourcePath">需要转换的文件路径和文件名称</param>
        ///// <param name="targetPath">转换完成后的文件的路径和文件名名称</param>
        ///// <returns></returns>
        //private static bool ExcelToPdf(string sourcePath, string targetPath)
        //{
        //    bool result = false;
        //    Excel.XlFixedFormatType xlTypePDF = Excel.XlFixedFormatType.xlTypePDF;//转换成pdf
        //    object missing = Type.Missing;
        //    Excel.ApplicationClass applicationClass = null;
        //    Excel.Workbook workbook = null;
        //    try
        //    {
        //        applicationClass = new Excel.ApplicationClass();
        //        workbook = applicationClass.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
        //        if (workbook != null)
        //        {
        //            workbook.ExportAsFixedFormat(xlTypePDF, targetPath, Excel.XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
        //        }
        //        result = true;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }
        //    finally
        //    {
        //        if (workbook != null)
        //        {
        //            workbook.Close(true, missing, missing);
        //            workbook = null;
        //        }
        //        if (applicationClass != null)
        //        {
        //            applicationClass.Quit();
        //            applicationClass = null;
        //        }
        //    }
        //    return result;
        //}

        /////<summary>        
        ///// 把PowerPoint文件转换成PDF格式文件       
        /////</summary>        
        /////<param name="sourcePath">源文件路径</param>     
        /////<param name="targetPath">目标文件路径</param> 
        /////<returns>true=转换成功</returns> 
        //private static bool PPTToPDF(string sourcePath, string targetPath)
        //{
        //    bool result;
        //    PowerPoint.PpSaveAsFileType targetFileType = PowerPoint.PpSaveAsFileType.ppSaveAsPDF;
        //    object missing = Type.Missing;
        //    PowerPoint.ApplicationClass application = null;
        //    PowerPoint.Presentation persentation = null;
        //    try
        //    {
        //        application = new PowerPoint.ApplicationClass();
        //        persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
        //        persentation.SaveAs(targetPath, targetFileType, Microsoft.Office.Core.MsoTriState.msoTrue);
        //        result = true;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }
        //    finally
        //    {
        //        if (persentation != null)
        //        {
        //            persentation.Close();
        //            persentation = null;
        //        }
        //        if (application != null)
        //        {
        //            application.Quit();
        //            application = null;
        //        }
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();
        //    }
        //    return result;
        //}

        /// <summary>
        /// PDF格式转为SWF
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        /// <param name="swfPath">生成后的SWF文件地址</param>
        /// <param name="beginpage">转换开始页</param>
        /// <param name="endpage">转换结束页</param>
        private static bool PDF2SWF(string pdfPath, string swfPath, int beginpage, int endpage, int photoQuality)
        {
            string exe = HttpContext.Current.Server.MapPath("~/Plugs/flexpaper/pdf2swf.exe");
            pdfPath = HttpContext.Current.Server.MapPath(pdfPath);
            swfPath = HttpContext.Current.Server.MapPath(swfPath);

            if (!System.IO.File.Exists(exe) || !System.IO.File.Exists(pdfPath) || System.IO.File.Exists(swfPath))
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(" \"" + pdfPath + "\"");
            sb.Append(" -o \"" + swfPath + "\"");
            sb.Append(" -s flashversion=9");
            if (endpage > GetPageCount(pdfPath)) endpage = GetPageCount(pdfPath);
            sb.Append(" -p " + "\"" + beginpage + "" + "-" + endpage + "\"");
            sb.Append(" -j " + photoQuality);
            string Command = sb.ToString();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = exe;
            p.StartInfo.Arguments = Command;
            p.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~/Bin/");
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.BeginErrorReadLine();
            p.WaitForExit();
            p.Close();
            p.Dispose();
            return true;
        }

        /// <summary>
        /// 返回页数
        /// </summary>
        /// <param name="pdfPath">PDF文件地址</param>
        private static int GetPageCount(string pdfPath)
        {
            byte[] buffer = System.IO.File.ReadAllBytes(pdfPath);
            int length = buffer.Length;
            if (buffer == null)
                return -1;
            if (buffer.Length <= 0)
                return -1;
            string pdfText = Encoding.Default.GetString(buffer);
            System.Text.RegularExpressions.Regex rx1 = new System.Text.RegularExpressions.Regex(@"/Type\s*/Page[^s]");
            System.Text.RegularExpressions.MatchCollection matches = rx1.Matches(pdfText);
            return matches.Count;
        }

        #region 用于取得一个文本文件的编码方式(Encoding)
        /// <summary>
        /// 用于取得一个文本文件的编码方式(Encoding)。
        /// 通过读取“编码字节序标识（Encoding Bit Order Mark，简写为BOM）”。
        /// </summary>
        /// 
        public class BOMStream
        {
            public BOMStream()
            {
            }

            /// <summary>
            /// 取得一个文本文件的编码方式。如果无法在文件头部找到有效的前导符，Encoding.Default将被返回。
            /// </summary>
            /// <param name="fileName">文件名</param>
            /// 
            public static Encoding GetEncoding(string fileName)
            {
                return GetEncoding(fileName, Encoding.Default);
            }

            /// <summary>
            /// 取得一个文本文件流的编码方式。
            /// </summary>
            /// <param name="stream">文本文件流</param>
            /// <returns></returns>
            /// 
            public static Encoding GetEncoding(FileStream stream)
            {
                return GetEncoding(stream, Encoding.Default);
            }

            /// <summary>
            /// 取得一个文本文件的编码方式。
            /// </summary>
            /// <param name="fileName">文件名。</param>
            /// <param name="defaultEncoding">默认编码方式。当该方法无法从文件的头部取得有效的前导符时，将返回该编码方式。</param>
            /// <returns></returns>
            /// 
            public static Encoding GetEncoding(string fileName, Encoding defaultEncoding)
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                Encoding targetEncoding = GetEncoding(fs, defaultEncoding);
                fs.Close();
                return targetEncoding;
            }

            /// <summary>
            /// 取得一个文本文件流的编码方式。

            /// </summary>
            /// <param name="stream">文本文件流。</param>

            /// <param name="defaultEncoding">默认编码方式。当该方法无法从文件的头部取得有效的前导符时，将返回该编码方式。</param>
            /// <returns></returns>
            /// 
            public static Encoding GetEncoding(FileStream stream, Encoding defaultEncoding)
            {
                Encoding targetEncoding = defaultEncoding;
                if (stream != null && stream.Length >= 2)
                {
                    //保存文件流的前4个字节
                    byte byte1 = 0;
                    byte byte2 = 0;
                    byte byte3 = 0;
                    byte byte4 = 0;

                    //保存当前Seek位置
                    long origPos = stream.Seek(0, SeekOrigin.Begin);
                    stream.Seek(0, SeekOrigin.Begin);
                    int nByte = stream.ReadByte();
                    byte1 = Convert.ToByte(nByte);
                    byte2 = Convert.ToByte(stream.ReadByte());

                    if (stream.Length >= 3)
                    {
                        byte3 = Convert.ToByte(stream.ReadByte());
                    }

                    if (stream.Length >= 4)
                    {
                        byte4 = Convert.ToByte(stream.ReadByte());
                    }
                    //根据文件流的前4个字节判断Encoding
                    //Unicode {0xFF, 0xFE};
                    //BE-Unicode {0xFE, 0xFF};
                    //UTF8 = {0xEF, 0xBB, 0xBF};

                    if (byte1 == 0xFE && byte2 == 0xFF) //UnicodeBe
                    {
                        targetEncoding = Encoding.BigEndianUnicode;
                    }

                    if (byte1 == 0xFF && byte2 == 0xFE && byte3 != 0xFF) //Unicode
                    {
                        targetEncoding = Encoding.Unicode;
                    }

                    if (byte1 == 0xEF && byte2 == 0xBB && byte3 == 0xBF) //UTF8
                    {
                        targetEncoding = Encoding.UTF8;
                    }

                    //恢复Seek位置
                    stream.Seek(origPos, SeekOrigin.Begin);
                }
                return targetEncoding;
            }
        }
        #endregion
    }
}
