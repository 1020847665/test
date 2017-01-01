using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;
using System.Text;
namespace BestWise.YLS.Manage.Web
{

    public class SystemConfigHelper
    {
        /// <summary>
        /// 配置文件保存路径
        /// </summary>
        public static string ConfigFilePath { get { return HttpContext.Current.Server.MapPath("~/bin/SysConfig.xml"); } }

        #region 得到配置分组特性
        /// <summary>
        /// 得到配置分组特性
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ConfigGroupAttribute GetConfigGroupAttribute(PropertyInfo p)
        {
            var attrs = p.GetCustomAttributes(typeof(ConfigGroupAttribute), true);
            if (attrs.Length == 0) return null;
            return attrs[0] as ConfigGroupAttribute;
        }
        #endregion

        #region 获取描述
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static string GetDescriptionAttribute(PropertyInfo p)
        {
            var attrs = p.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs.Length == 0) return p.Name;
            var attr = attrs[0] as DescriptionAttribute;
            if (attr == null) return p.Name;
            return attr.Description;
        }
        #endregion

        #region 返回枚举项的描述信息
        /// <summary>
        /// 返回枚举项的描述信息
        /// </summary>
        /// <param name="value">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetDescription<T>(T value)
        {
            var enums = value.ToString().Split(',');
            var result = string.Empty;
            var type = typeof(T);
            foreach (var item in enums)
            {
                if (string.IsNullOrEmpty(item)) continue;
                var val = Enum.Parse(type, item);
                result += GetEnumDescription(type, Convert.ToInt32(val)) + ",";
            }
            result = result.TrimEnd(',');
            return result;
        }
        #endregion

        #region 返回枚举项的描述信息
        /// <summary>
        /// 返回枚举项的描述信息
        /// </summary>
        /// <param name="type">枚举</param>
        /// <param name="orderByInt">要获取描述信息的枚举项。</param>
        /// <returns>枚举想的描述信息。</returns>
        public static string GetEnumDescription(Type type, int orderByInt)
        {
            Type enumType = type;
            string name = Enum.GetName(enumType, orderByInt);
            if (name != null)
            {
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fieldInfo,
                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return orderByInt.ToString();
        }
        #endregion

        /// <summary>
        /// 获取系统配置
        /// </summary>
        /// <returns></returns>
        public static SystemConfig GetSysConfig()
        {
            SystemConfig config = new SystemConfig();
            if (File.Exists(ConfigFilePath))
            {
                FileStream file = new FileStream(ConfigFilePath, FileMode.Open, FileAccess.Read);
                XmlSerializer xmlSearializer = new XmlSerializer(typeof(SystemConfig));
                config = (SystemConfig)xmlSearializer.Deserialize(file);
                file.Close();
            }
            return config;
        }

        /// <summary>
        /// 重置默认配置
        /// </summary>
        public static void ResetConfig()
        {
            SystemConfig config = new SystemConfig();
            StreamWriter sw = new StreamWriter(SystemConfigHelper.ConfigFilePath, false);
            XmlSerializer serializer = new XmlSerializer(typeof(SystemConfig));
            serializer.Serialize(sw, config);
            sw.Close();
        }

        /// <summary>
        /// 输出HTML
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string GetPropertyHtml(PropertyInfo p)
        {
            SystemConfig config = new SystemConfig();
            if (File.Exists(ConfigFilePath))
            {
                FileStream file = new FileStream(ConfigFilePath, FileMode.Open, FileAccess.Read);
                XmlSerializer xmlSearializer = new XmlSerializer(typeof(SystemConfig));
                config = (SystemConfig)xmlSearializer.Deserialize(file);
                file.Close();
            }
            var html = new StringBuilder();
            var result = " <li><label style='width:110px'>{0}</label>{1}</li>";
            var attribute = GetDescriptionAttribute(p);
            var configGroupAttribute = GetConfigGroupAttribute(p);
            var objVal = p.GetValue(config, null);
            var val = objVal == null ? string.Empty : objVal.ToString();
            val = val.Replace("\"", "&quot;");
            if (configGroupAttribute != null && configGroupAttribute.CanEdit)
            {
                if (configGroupAttribute.InputType == InputType.文本)
                    html.AppendFormat("<input style='width: {0}px' type='text' name='{1}' class='dfinput' value='{2}'>{3}", configGroupAttribute.InputWidth, p.Name, val, configGroupAttribute.Suffix);
                if (configGroupAttribute.InputType == InputType.单选)
                {
                    var boolVal = val == true.ToString();
                    const string selectedHtml = " checked='checked' ";
                    html.AppendFormat("<cite><input type='radio' name='{0}' value='True'   {1} />是&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", p.Name, (boolVal ? selectedHtml : string.Empty));
                    html.AppendFormat("<input type='radio' name='{0}' value='False'  {1} />否</cite>", p.Name, (!boolVal ? selectedHtml : string.Empty));
                }
            }
            else
            {
                html.Append(val);
            }
            return string.Format(result, attribute, html);
        }
    }

    #region 系统参数隶属组
    /// <summary>
    /// 系统参数隶属组
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ConfigGroupAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group">隶属分组</param>
        /// <param name="canEdit">是否可以编辑</param>
        /// <param name="dt">数据类型</param>
        /// <param name="suffix">后缀</param>
        /// <param name="width">控件宽度</param>
        public ConfigGroupAttribute(BelongsGroup group, bool canEdit, InputType dt, string suffix = "", int width = 500)
        {
            this.Group = group;
            this.CanEdit = canEdit;
            this.InputType = dt;
            this.Suffix = suffix;
            this.InputWidth = width;
        }

        /// <summary>
        /// 分组名称
        /// </summary>
        public BelongsGroup Group { get; set; }

        /// <summary>
        /// 文本框宽度
        /// </summary>
        [Description("文本框宽度")]
        public int InputWidth { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        [Description("后缀")]
        public string Suffix { get; set; }

        /// <summary>
        /// 是否可以编辑
        /// </summary>
        [Description("是否可以编辑")]
        public bool CanEdit { get; set; }

        /// <summary>
        /// 数据呈现方式
        /// </summary>
        [Description("数据呈现方式")]
        public InputType InputType { get; set; }
    }
    #endregion

    #region 隶属配置组
    /// <summary>
    /// 隶属配置组
    /// </summary>
    public enum BelongsGroup
    {
        /// <summary>
        /// 设备系统
        /// </summary>
        [Description("系统参数")]
        系统参数 = 0,

        /// <summary>
        /// 分页设置
        /// </summary>
        [Description("分页参数")]
        分页设置 = 1,

        /// <summary>
        /// 业务参数
        /// </summary>
        [Description("业务参数")]
        业务参数 = 2,

        /// <summary>
        /// 链接地址
        /// </summary>
        [Description("链接地址")]
        链接地址 = 3
    }
    #endregion

    #region 数据类型
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum InputType
    {
        /// <summary>
        /// 文本
        /// </summary>
        文本 = 1,

        /// <summary>
        /// 单选
        /// </summary>
        单选 = 2
    }
    #endregion

    public class ConfigGroupClass
    {
        public List<PropertyInfo> List { get; set; }

        public ConfigGroupClass()
        {
            List = new List<PropertyInfo>();
        }
        public BelongsGroup GroupName { get; set; }
        public string BelongsGroupDescription { get; set; }
    }
}