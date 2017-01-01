/// <reference path="jquery.min.js" />

var EmptyGuid = '00000000-0000-0000-0000-000000000000';

function GetOperationName(name, id) {
    return id == EmptyGuid ? "新增 |" : "编辑 |" + name;
}

/*递归获取Guid值*/
function GetGuid() {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    return "UUID-" + (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

/*【Ajax】
type:post/get：请求类型
url：请求地址
dataType：数据类型
data：请求数据
cache：是否缓存
async：是否异步
*/
function $Ajax(options) {
    var index;
    options.type = options.type == undefined ? "post" : options.type;
    options.dataType = options.dataType == undefined ? "json" : options.dataType;
    options.cache = options.cache == undefined ? false : options.cache;
    options.async = options.async == undefined ? true : options.async;
    options.beforeSend = function () { index = layer.load(options.loadMsg == undefined ? "处理中..." : options.loadMsg); };
    options.success = function (result) {
        if (options.typeFlag == undefined) {
            layer.close(index);
            if (result.Status == -1) {
                var returnUrl = encodeURI(window.location.href);
                top.window.location.href = "/Home/Login?returnurl=" + returnUrl + "";
            } else if (result.Status == 1) options.callBack(result); //处理成功进入回调事件
            else if (result.Status == 6) {
                $.messager.confirm('确认信息', result.Message, function (r) {
                    if (r) {
                        options.confirmBack(result);
                    }
                });
            } else {
                if (result.Status == 3) $.messager.alert('错误信息', result.Errors[0].ErrorMessages, 'error');
                else $.messager.alert('错误信息', result.Message, 'error');
                if (options.errorBack != undefined) {
                    options.errorBack(result);
                }
            }
        } else if (options.typeFlag) {
            options.callBack(result, index);
        }
    };
    options.error = function (XMLHttpRequest, textStatus, errorThrown) {
        layer.close(index);
        $.messager.alert('请求出错', errorThrown, 'error');
    };
    $.ajax(options); //发起请求
}

/*【下载文件】
url：请求地址
data：请求数据
*/
function $AjaxDownloadFile(options) {
    var $iframe, iframeDoc, iframeHtml;
    if (($iframe = $('#downloadIframe')).length === 0)
        $iframe = $("<iframe id='downloadIframe' style='display: none' src='about:blank'></iframe>").appendTo("body");
    iframeDoc = $iframe[0].contentWindow || $iframe[0].contentDocument;
    if (iframeDoc.document)
        iframeDoc = iframeDoc.document;
    iframeHtml = "<html><head></head><body><form method='POST' action='" + options.url + "'>";
    Object.keys(options.data).forEach(function (key) {
        iframeHtml += "<input type='hidden' name='" + key + "' value='" + options.data[key] + "'>";
    });
    iframeHtml += "</form></body></html>";
    console.log(iframeHtml);
    iframeDoc.open();
    iframeDoc.write(iframeHtml);
    $(iframeDoc).find('form').submit();
}


/*OpenDialog【显示Dialog窗口】
url：表示请求路径
title：标题
width：宽度
height：高度
fit：是否全屏
class：类名 说明：如果是多层弹出，则必填
resizable： 是否可以被缩放
modal：是否显示遮掩层
toolbar： 窗口顶部工具栏 说明：toolbar:[{text:'编辑',iconCls:'icon-edit',handler:function(){alert('edit')} }]
buttons：窗口底部的按钮 说明：buttons:[{text:'保存',handler:function(){...}}]*/
function OpenDialog(options) {
    var maxHeight = $(window.parent.document).find("#bodyPanel").height() - 60; //最大高度
    options.height = options.height > maxHeight ? maxHeight : options.height;
    var maxWidth = $(window.parent.document).find("#bodyPanel").width() - 40; //最大宽度
    options.width = options.width > maxWidth ? maxWidth : options.width;

    options.iconCls = options.iconCls == undefined ? "icon-application_edit" : options.iconCls;
    options.className = options.className == undefined ? "extenddialog" : options.className;
    options.maximizable = options.maximizable == undefined ? true : options.maximizable;


    $('body').append('<div id="Dialog' + Math.floor(Math.random() * 100 + 1) + '" class="easyui-dialog dialog ' + options.className + '" data-options="modal:true,closed:true"></div>');
    var $current = $('.' + options.className + '');
    //关闭，销毁组件，防止缓存
    options.onClose = function () { $current.dialog('destroy'); }
    //刷新
    options.tools = [{ iconCls: 'icon-refreshn', handler: function () { $current.dialog('refresh'); } }];
    //$("#form-detail").remove();
    $current.dialog(options);
    $current.dialog('open');
}

/*##############弹出Frame窗体#################*/
function OpenFrame(url, options) {
    var maxHeight = 0;
    var maxWidth = 0;
    if (options.parentOpen == undefined || options.parentOpen == false) {
        var maxWidth = $(window.parent.document).find("#bodyPanel").width() - 40;
        var maxHeight = $(window.parent.document).find("#bodyPanel").height() - 60;
    } else {
        var maxWidth = $(document).width() - 40;
        var maxHeight = $(document).height() - 60;
    }
    options.height = options.height > maxHeight ? maxHeight : options.height;
    options.width = options.width > maxWidth ? maxWidth : options.width;
    options.iconCls = options.iconCls == undefined ? "icon-application_double" : options.iconCls;
    options.className = options.className == undefined ? "extenddialogFrame" : options.className;
    options.maximizable = options.maximizable == undefined ? true : options.maximizable;
    var dialogContent = '<div id="Dialog' + Math.floor(Math.random() * 100 + 1) + '" class="easyui-dialog dialog ' + options.className + '" data-options="modal:true,closed:true">';
    dialogContent += "<iframe id='DialogFrame' name='DialogFrame' src=\"" + url + "\" class=\"DialogFrame\" width=\"100%\" height=\"99%\" frameborder=\"0\" scrolling=\"no\"></iframe>";
    dialogContent += '</div>';
    $('body').append(dialogContent);
    var $current = $('.' + options.className + '');
    //关闭窗体时，销毁组件，防止缓存
    options.onClose = options.onClose == undefined ? function () { $current.dialog('destroy'); } : options.onClose;
    $current.dialog(options);
    $current.dialog('open');
}



/*OpenClose【关闭所有Dialog窗口】*/
function CloseDialog() {
    $('.dialog').dialog('close');
}

//格式化日期控件格式 年-月-日
function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

function myparser(s) {
    if (!s) return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}


$.fn.extend({
    /*【搜索表单序列化】*/
    serializeJson: function () {
        var serializeObj = {};
        var array = this.serializeArray();
        if (array.length == 0) array = $(this).find("input,select,textarea").serializeArray();
        $(array).each(function () {
            if (serializeObj[this.name]) {
                if ($.isArray(serializeObj[this.name])) {
                    serializeObj[this.name].push(this.value);
                } else {
                    serializeObj[this.name] = [serializeObj[this.name], this.value];
                }
            } else {
                serializeObj[this.name] = this.value;
            }
        });
        return serializeObj;
    }
});

$(function () {
    //刷新验证码
    $(document).on("click", ".yzmimg", function () {
        $(this).attr('src', '/Yzm/Create?_=' + GetGuid());
    });
});

var CommonJs = {
    //添加行
    AddRow: function () {
        var $firstRow = $(".tablelist-batch tbody tr").eq(0).clone(true);
        var $tabbody = $(".tablelist-batch tbody");
        $tabbody.append($firstRow);
        $(".tablelist-batch tbody tr").last().find("input").val("");
        $tabbody.find("tr:last").find("td:first").text($tabbody.find("tr").length);
    },
    SerializeTab: function () {
        var list = [];
        $(".tablelist-batch tbody tr").each(function (index, domEle) {
            list.push($(domEle).serializeJson());
        });
        console.log(list);
        return list;
    }
};

//Table批量操作
//复制行
$(document).on("click", ".tablelist-batch .add-row", function () {
    var $this = $(this);
    var $cktr = $this.closest("tr").clone(true);
    var $ckbody = $this.closest("tbody");
    $ckbody.append($cktr);
    $ckbody.find("tr:last").find("td:first").text($ckbody.find("tr").length);
});
//删除行
$(document).on("click", ".tablelist-batch .del-row", function () {
    var $this = $(this);
    if ($(".tablelist-batch tbody tr").length > 1) {
        $this.closest("tr").remove();
        var $tr = $(".tablelist-batch tbody tr");
        $.each($tr, function (index, item) {
            $(item).find("td:first").text(index + 1);
        });
    } else $.messager.alert('提示', "无法删除最后一行！", 'warning');
});


//禁用Enter键表单自动提交  
document.onkeydown = function (event) {
    var target, code, tag;
    if (!event) {
        event = window.event; //针对ie浏览器  
        target = event.srcElement;
        code = event.keyCode;
        if (code == 13) {
            tag = target.tagName;
            if (tag == "TEXTAREA") { return true; }
            else { return false; }
        }
    }
    else {
        target = event.target; //针对遵循w3c标准的浏览器，如Firefox  
        code = event.keyCode;
        if (code == 13) {
            tag = target.tagName;
            if (tag == "INPUT") { return false; }
            else { return true; }
        }
    }
};

//按ESC键关闭弹窗
$(window).keydown(function (event) {
    if (event.which == 27) {
        CloseDialog();
    }
});

































































/*【格式化日期】
b：值
f：格式
*/
function FormatDateTime(b) {
    b = b || new Date().getTime();
    var a = new Date(parseInt(b));
    return a.format("yyyy-MM-dd hh:mm:ss");
}
Date.prototype.format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};



/**
 ** 加法函数，用来得到精确的加法结果
 ** 说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。
 ** 调用：accAdd(arg1,arg2)
 ** 返回值：arg1加上arg2的精确结果
 **/
function accAdd(arg1, arg2) {
    var r1, r2, m, c;
    try {
        r1 = arg1.toString().split(".")[1].length;
    }
    catch (e) {
        r1 = 0;
    }
    try {
        r2 = arg2.toString().split(".")[1].length;
    }
    catch (e) {
        r2 = 0;
    }
    c = Math.abs(r1 - r2);
    m = Math.pow(10, Math.max(r1, r2));
    if (c > 0) {
        var cm = Math.pow(10, c);
        if (r1 > r2) {
            arg1 = Number(arg1.toString().replace(".", ""));
            arg2 = Number(arg2.toString().replace(".", "")) * cm;
        } else {
            arg1 = Number(arg1.toString().replace(".", "")) * cm;
            arg2 = Number(arg2.toString().replace(".", ""));
        }
    } else {
        arg1 = Number(arg1.toString().replace(".", ""));
        arg2 = Number(arg2.toString().replace(".", ""));
    }
    return (arg1 + arg2) / m;
}

//给Number类型增加一个add方法，调用起来更加方便。
Number.prototype.add = function (arg) {
    return accAdd(arg, this);
};
/**
 ** 减法函数，用来得到精确的减法结果
 ** 说明：javascript的减法结果会有误差，在两个浮点数相减的时候会比较明显。这个函数返回较为精确的减法结果。
 ** 调用：accSub(arg1,arg2)
 ** 返回值：arg1加上arg2的精确结果
 **/
function accSub(arg1, arg2) {
    var r1, r2, m, n;
    try {
        r1 = arg1.toString().split(".")[1].length;
    }
    catch (e) {
        r1 = 0;
    }
    try {
        r2 = arg2.toString().split(".")[1].length;
    }
    catch (e) {
        r2 = 0;
    }
    m = Math.pow(10, Math.max(r1, r2)); //last modify by deeka //动态控制精度长度
    n = (r1 >= r2) ? r1 : r2;
    return ((arg1 * m - arg2 * m) / m).toFixed(n);
}

// 给Number类型增加一个mul方法，调用起来更加方便。
Number.prototype.sub = function (arg) {
    return accMul(arg, this);
};
/**
 ** 乘法函数，用来得到精确的乘法结果
 ** 说明：javascript的乘法结果会有误差，在两个浮点数相乘的时候会比较明显。这个函数返回较为精确的乘法结果。
 ** 调用：accMul(arg1,arg2)
 ** 返回值：arg1乘以 arg2的精确结果
 **/
function accMul(arg1, arg2) {
    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
    try {
        m += s1.split(".")[1].length;
    }
    catch (e) {
    }
    try {
        m += s2.split(".")[1].length;
    }
    catch (e) {
    }
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m);
}

// 给Number类型增加一个mul方法，调用起来更加方便。
Number.prototype.mul = function (arg) {
    return accMul(arg, this);
};
/** 
 ** 除法函数，用来得到精确的除法结果
 ** 说明：javascript的除法结果会有误差，在两个浮点数相除的时候会比较明显。这个函数返回较为精确的除法结果。
 ** 调用：accDiv(arg1,arg2)
 ** 返回值：arg1除以arg2的精确结果
 **/
function accDiv(arg1, arg2) {
    var t1 = 0, t2 = 0, r1, r2;
    try {
        t1 = arg1.toString().split(".")[1].length;
    }
    catch (e) {
    }
    try {
        t2 = arg2.toString().split(".")[1].length;
    }
    catch (e) {
    }
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""));
        r2 = Number(arg2.toString().replace(".", ""));
        return (r1 / r2) * pow(10, t2 - t1);
    }
}

//给Number类型增加一个div方法，调用起来更加方便。
Number.prototype.div = function (arg) {
    return accDiv(this, arg);
};







