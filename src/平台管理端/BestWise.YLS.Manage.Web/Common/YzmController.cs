using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BestWise.YLS.Manage.Web.Common
{

    /// <summary>
    /// 验证码生成
    /// </summary>
    [AllowAnonymous]
    public class YzmController : Controller
    {
        private char code;
        /// <summary>  
        /// 验证码类型(0-字母数字混合,1-数字,2-字母)  
        /// </summary>  
        private string validateCodeType = "0";
        /// <summary>  
        /// 验证码字符个数  
        /// </summary>  
        private int validateCodeCount = 4;
        /// <summary>  
        /// 验证码的字符集，去掉了一些容易混淆的字符  
        /// </summary>  
        private char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };

        public void Create()
        {
            //取消缓存  
            Response.BufferOutput = true;
            Response.Cache.SetExpires(DateTime.Now.AddMilliseconds(-1));
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.AppendHeader("Pragma", "No-Cache");
            //获取设置参数  
            if (!string.IsNullOrEmpty(Request.QueryString["validateCodeType"]))
                validateCodeType = Request.QueryString["validateCodeType"];
            if (!string.IsNullOrEmpty(Request.QueryString["validateCodeCount"]))
                int.TryParse(Request.QueryString["validateCodeCount"], out validateCodeCount);

            string checkCode = String.Empty;
            //生成随机生成器  
            Random random = new Random();
            for (int i = 0; i < validateCodeCount; i++)
            {
                code = character[random.Next(character.Length)];
                // 要求全为数字或字母  
                if (validateCodeType == "1")
                {
                    if ((int)code < 48 || (int)code > 57)
                    {
                        i--;
                        continue;
                    }
                }
                else if (validateCodeType == "2")
                {
                    if ((int)code < 65 || (int)code > 90)
                    {
                        i--;
                        continue;
                    }
                }
                checkCode += code;
            }
            Response.Cookies.Add(new System.Web.HttpCookie("ValidateCode", checkCode));
            this.Session["ValidateCode"] = checkCode;
            if (checkCode == null || checkCode.Trim() == String.Empty) return;
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 15.0 + 40)), 23);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            try
            {
                //清空图片背景色  
                g.Clear(System.Drawing.Color.White);
                //画图片的背景噪音线  
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Silver), x1, y1, x2, y2);
                }
                System.Drawing.Font font = new System.Drawing.Font("Arial", 14, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.Color.Blue, System.Drawing.Color.DarkRed, 1.2f, true);
                int cySpace = 16;
                for (int i = 0; i < validateCodeCount; i++)
                {
                    g.DrawString(checkCode.Substring(i, 1), font, brush, (i + 1) * cySpace, 1);
                }
                //画图片的前景噪音点  
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, System.Drawing.Color.FromArgb(random.Next()));
                }
                //画图片的边框线  
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}