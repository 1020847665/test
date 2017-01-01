using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestWise.YLS.Manage.Web.Common.Filter
{
    /// <summary>
    /// 用于获取安全Sql条件工具类
    /// </summary>
    public class Tool
    {
        /// <summary>
        /// 获取安全字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RequestString(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            if (null == InText) return string.Empty;
            if (!string.IsNullOrWhiteSpace(InText))
            {
                InText = InText.Replace("--", ""); //替换sql注释
                string word = "and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join"; //sql敏感词
                foreach (string i in word.Split('|'))
                {
                    if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                    {
                        //如有危险字符，则返回空字符串
                        return string.Empty;
                    }
                }
            }
            return InText.ToString().Trim();
        }

        /// <summary>
        /// Guid
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Guid RequestGuid(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            Guid t = Guid.Empty;
            if (InText == null) return t;
            Guid.TryParse(InText.ToString(), out t);
            return t;
        }


        /// <summary>
        /// Int
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int RequestInt(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            int t = 0;
            if (InText == null) return t;
            int.TryParse(InText.ToString(), out t);
            return t;
        }

        /// <summary>
        /// Long
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long RequestLong(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            long t = 0;
            if (InText == null) return t;
            long.TryParse(InText.ToString(), out t);
            return t;
        }

        /// <summary>
        /// Decimal
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal RequestDecimal(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            decimal t = 0;
            if (InText == null) return t;
            decimal.TryParse(InText.ToString(), out t);
            return t;
        }


        #region 日期
        /// <summary>
        /// DateTime
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RequestDateString(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            if (InText == null) return string.Empty;
            DateTime t = DateTime.MinValue;
            DateTime.TryParse(InText.ToString(), out t);
            if (t.Year <= 1900) return string.Empty;
            return t.ToString();
        }


        /// <summary>
        /// DateTime
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RequestEndDateString(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            if (InText == null) return string.Empty;
            DateTime t = DateTime.MinValue;
            DateTime.TryParse(InText.ToString(), out t);
            if (t.Year <= 1900) return string.Empty;
            return t.ToString().Replace("0:00:00", "23:59:59");
        }

        /// <summary>
        /// DateTime
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime? RequestDateTime(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            if (InText == null) return null;
            DateTime t = DateTime.MinValue;
            DateTime.TryParse(InText.ToString(), out t);
            if (t.Year <= 1900) return null;
            return t;
        }

        /// <summary>
        /// DateTime
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime? RequestEndTime(string key)
        {
            var InText = HttpContext.Current.Request.Params[key];
            if (InText == null) return null;
            DateTime t = DateTime.MinValue;
            DateTime.TryParse(InText.ToString(), out t);
            if (t.Year <= 1900) return null;
            return DateTime.Parse(t.ToString().Replace("0:00:00", "23:59:59"));
        }
        #endregion
    }
}