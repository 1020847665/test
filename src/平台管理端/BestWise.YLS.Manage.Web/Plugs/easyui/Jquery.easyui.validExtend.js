/** 
* 扩展easyui的validator插件rules，支持更多类型验证 
*/
$.extend($.fn.validatebox.defaults.rules, {
    minLength: { // 判断最小长度  
        validator: function (value, param) {
            return value.length >= param[0];
        },
        message: '最少输入 {0} 个字符'
    },
    length: { // 长度  
        validator: function (value, param) {
            var len = $.trim(value).length;
            return len >= param[0] && len <= param[1];
        },
        message: "输入内容长度必须介于{0}和{1}之间"
    },
    phone: {// 验证电话号码  
        validator: function (value) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '电话格式不正确,请使用下面格式:020-88888888'
    },
    url: {
        validator: function (value) {
            return /^(http|https):\/\/\w+\.+[0-9a-zA-Z]+\.\w+?$/i.test(value);
        },
        message: '网址格式不正确，请使用下面的格式:http://www.baidu.com'
    },
    eamil: {// 验证邮箱 
        validator: function (value) {
            return /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i.test(value);
        },
        message: '邮箱格式不正确'
    },
    compare: {
        validator: function (value, param) {
            return $('' + param + '').val() == value;
        },
        message: '两次输入的密码不一致!'
    },
    password: {// 验证密码  
        validator: function (value) {
            return /^[0-9a-zA-Z]{6,22}$/i.test(value);
        },
        message: '格式不正确,由大小写字母与数字组成6~22位密码'
    },
    mobile: {// 验证手机号码  
        validator: function (value) {
            return /^(13|15|18)\d{9}$/i.test(value);
        },
        message: '手机号码格式不正确'
    },
    phoneAndMobile: {// 电话号码或手机号码  
        validator: function (value) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value) || /^(13|15|18)\d{9}$/i.test(value);
        },
        message: '电话号码或手机号码格式不正确'
    },
    intorline: {// 输入数字以及横线  
        validator: function (value) {
            return /^[\d\-\+ ()]*$/i.test(value);
        },
        message: '联系电话格式不正确'
    },
    idcard: {// 验证身份证  
        validator: function (value) {
            return /^\d{15}(\d{2}[A-Za-z0-9])?$/i.test(value) || /^\d{18}(\d{2}[A-Za-z0-9])?$/i.test(value);
        },
        message: '身份证号码格式不正确'
    },
    intOrFloat: {// 验证整数或小数  
        validator: function (value) {
            return /^\d+(\.\d+)?$/i.test(value);
        },
        message: '请输入数字，并确保格式正确'
    },
    currency: {// 验证货币  
        validator: function (value) {
            return /^\d+(\.\d+)?$/i.test(value);
        },
        message: '货币格式不正确'
    },
    qq: {// 验证QQ,从10000开始  
        validator: function (value) {
            return /^[1-9]\d{4,9}$/i.test(value);
        },
        message: 'QQ号码格式不正确'
    },
    integer: {// 验证整数  
        validator: function (value) {
            return /^[+]?[1-9]+\d*$/i.test(value);
        },
        message: '请输入整数'
    },
    nochinese: {// 验证非中文
        validator: function (value) {
            return /^[^\u0391-\uFFE5]+$/i.test(value);
        },
        message: '请输入英文'
    },
    chinese: {// 验证中文  
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/i.test(value);
        },
        message: '请输入中文'
    },
    chineseAndLength: {// 验证中文及长度  
        validator: function (value) {
            var len = $.trim(value).length;
            if (len >= param[0] && len <= param[1]) {
                return /^[\u0391-\uFFE5]+$/i.test(value);
            }
            message: '请输入中文'
        },
    },
    english: {// 验证英语  
        validator: function (value) {
            return /^[A-Za-z]+$/i.test(value);
        },
        message: '请输入英文'
    },
    englishAndLength: {// 验证英语及长度  
        validator: function (value, param) {
            var len = $.trim(value).length;
            if (len >= param[0] && len <= param[1]) {
                return /^[A-Za-z]+$/i.test(value);
            }
        },
        message: '请输入英文'
    },
    englishUpperCase: {// 验证英语大写  
        validator: function (value) {
            return /^[A-Z]+$/.test(value);
        },
        message: '请输入大写英文'
    },
    unnormal: {// 验证是否包含空格和非法字符  
        validator: function (value) {
            return /.+/i.test(value);
        },
        message: '输入值不能为空和包含其他非法字符'
    },
    username: {// 验证用户名  
        validator: function (value) {
            return /^[a-zA-Z][a-zA-Z0-9_]{5,15}$/i.test(value);
        },
        message: '用户名不合法（字母开头，允许6-16字节，允许字母数字下划线）'
    },
    faxno: {// 验证传真  
        validator: function (value) {
            return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$/i.test(value);
        },
        message: '传真号码不正确'
    },
    zip: {// 验证邮政编码  
        validator: function (value) {
            return /^[1-9]\d{5}$/i.test(value);
        },
        message: '邮政编码格式不正确'
    },
    ip: {// 验证IP地址  
        validator: function (value) {
            return /d+.d+.d+.d+/i.test(value);
        },
        message: 'IP地址格式不正确'
    },
    name: {// 验证姓名，可以是中文或英文  
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/i.test(value) | /^\w+[\w\s]+\w+$/i.test(value);
        },
        message: '请输入姓名'
    },
    date: {// 验证日期 
        validator: function (value) {
            //格式yyyy-MM-dd或yyyy-M-d
            return /^(?:(?!0000)[0-9]{4}([-]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-]?)0?2\2(?:29))$/i.test(value);
        },
        message: '清输入合适的日期格式'
    },
    md: { // 验证结束日期大于开始日期
        validator: function (value, param) {
            startTime2 = $(param[0]).datetimebox('getValue');
            var d1 = $.fn.datebox.defaults.parser(startTime2);
            var d2 = $.fn.datebox.defaults.parser(value);
            varify = d2 > d1;
            return varify;
        },
        message: '结束日期要大于开始日期！'
    },
    engOrChinese: {// 可以是中文或英文  
        validator: function (value) {
            return /^[\u0391-\uFFE5]+$/i.test(value) | /^\w+[\w\s]+\w+$/i.test(value);
        },
        message: '请输入中文'
    },
    engOrChineseAndLength: {// 可以是中文或英文  
        validator: function (value) {
            var len = $.trim(value).length;
            if (len >= param[0] && len <= param[1]) {
                return /^[\u0391-\uFFE5]+$/i.test(value) | /^\w+[\w\s]+\w+$/i.test(value);
            }
        },
        message: '请输入中文或英文'
    },
    discount: {// 折扣
        validator: function (value) {
            return /^([0-9](\.[0-9]{1,2})?|10)$/i.test(value);
        },
        message: '请输入10以内的2位小数'
    }
});
