using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestWise.YLS.Manage.Web.Common
{
    /// <summary>
    /// 定义EasyUI树的相关数据，方便控制器生成Json数据进行传递
    /// </summary>
    public class EasyData
    {
        #region 属性

        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 节点编码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string parentCode { get; set; }

        /// <summary>
        /// 自定义属性
        /// </summary>
        public object attributes { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        public string iconCls { get; set; }

        /// <summary>
        /// 子节点集合
        /// </summary>
        public List<EasyData> children { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long total { get; set; }

        /// <summary>
        /// 记录行
        /// </summary>
        public Object rows { get; set; }

        /// <summary>
        /// 复选框
        /// </summary>
        public bool ck { get; set; }

        #endregion

        #region 默认构造函数
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EasyData()
        {
            this.children = new List<EasyData>();
            this.state = "open";
        }
        #endregion

        #region 重载构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public EasyData(string id, string text) : this()
        {
            this.id = id;
            this.text = text;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EasyData(string id, string text, string iconCls) : this(id, text)
        {
            this.iconCls = iconCls;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="iconCls"></param>
        /// <param name="code"></param>
        /// <param name="parentCode"></param>
        public EasyData(string id, string text, string iconCls, string code, string parentCode) : this(id, text, iconCls)
        {
            this.code = code;
            this.parentCode = parentCode;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="iconCls"></param>
        /// <param name="code"></param>
        /// <param name="parentCode"></param>
        public EasyData(string id, string text, string iconCls, string code, string parentCode, string href)
            : this(id, text, iconCls, code, parentCode)
        {
            this.href = href;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EasyData(string id, string text, string iconCls, List<EasyData> children)
            : this(id, text, iconCls)
        {
            this.children = children;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EasyData(string id, string text, string iconCls, List<EasyData> children, string state)
            : this(id, text, iconCls, children)
        {
            this.state = state;
        }
        #endregion

        #region 递归生成树(适用于Tree、comboTree)
        /// <summary>
        ///  递归生成树(适用于Tree、comboTree)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public static List<EasyData> Recursion(List<EasyData> list, string parentId, bool IsExpandAll)
        {
            List<EasyData> chilrenlist = (from e in list where e.parentCode == parentId select e).ToList();
            List<EasyData> temp = new List<EasyData>();
            foreach (EasyData item in chilrenlist)
            {
                temp = Recursion(list, item.id, IsExpandAll);
                if (!IsExpandAll)
                {
                    if (string.Equals(item.parentCode, Guid.Empty.ToString()) && temp.Count > 0)
                        item.state = "closed";
                }
                item.children = temp;
            }
            return chilrenlist;
        }
        #endregion
    }
}