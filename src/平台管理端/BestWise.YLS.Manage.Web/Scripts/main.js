
var EasyExt = {
    AddTab: function (options) {
        options.target = options.target == undefined ? $("#bodyTab") : options.target;
        options.closable = options.closable == undefined ? true : options.closable;
        options.bodyCls = "tabContentStyle";
        options.cache = false; //不缓存
        options.content = "<iframe src=\"" + options.requestUrl + "\" class=\"rightFrame\" width=\"100%\" height=\"100%\" frameborder=\"0\"></iframe>";
        //判断tab选项是否存在，存在则选中，刷新
        if (options.target.tabs('exists', options.title)) options.target.tabs('select', options.title);
            //不存在则新增选项卡
        else options.target.tabs('add', options);
    },
    CloseTab: function () {
        var $tab = $("#bodyTab");
        var tab = $tab.tabs('getSelected');
        var index = $tab.tabs('getTabIndex', tab);
        $tab.tabs('close', index);
    },
};

$(function () {
    //绑定顶部菜单点击事件
    $(document).on("click", "#layout-top .nav li a", function () {
        $("#layout-top .nav li a.selected").removeClass("selected");
        $(this).addClass("selected");
        var leftPaneOptionsl = $("#layout-left").panel("options");
        if (leftPaneOptionsl.collapsed) {
            $("#layoutCenter").layout("expand", "west");
        }
        $('#layout-left').panel('open').panel('refresh', "/Layout/Left?pid=" + $(this).attr("key"));
    });
    //绑定左侧主菜单点击事件
    $(document).on("click", ".title", function () {
        var $ul = $(this).next('ul');
        $('dd').find('ul').slideUp();
        if ($ul.is(':visible')) $(this).next('ul').slideUp();
        else $(this).next('ul').slideDown();
    });
    //绑定左侧子菜单点击事件
    $(document).on("click", ".menuson li", function () {
        $(".menuson li.active").removeClass("active");
        $(this).addClass("active");
    });
    //右键关闭按选项卡
    $(".tabs-header").bind('contextmenu', function (e) {
        e.preventDefault();
        $('#rcmenu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
    });
    $("#closecur,#closeall,#closeother").bind("click", function () {
        var tab = $('#bodyTab').tabs('getSelected');
        var index = $('#bodyTab').tabs('getTabIndex', tab);
        var tablist = $('#bodyTab').tabs('tabs');
        switch ($(this).attr("id")) {
            case 'closecur': {
                if (index != 0)
                    $('#bodyTab').tabs('close', index);
            }; break;
            case 'closeall': {
                for (var i = tablist.length - 1; i > 0; i--)
                    $('#bodyTab').tabs('close', i);
            }; break;
            case 'closeother': {
                for (var i = tablist.length - 1; i > index; i--)
                    $('#bodyTab').tabs('close', i);
                for (var i = index - 1; i > 0; i--)
                    $('#bodyTab').tabs('close', i);
            }; break;
        }
    });
    //注销
    $(document).on("click", ".btn-loginout", function () {
        $.messager.confirm('提示', "确定要注销，重新登录吗？", function (r) {
            if (r) {
                $Ajax({
                    url: "/Home/LoginOut",
                    data: null,
                    callBack: function (result) {
                        top.window.location.href = "/Home/Login";
                    }
                });
            }
        });
    });
    //修改密码
    $(document).on("click", ".btn-alterpwd", function () {
        OpenDialog({
            href: "/SysUsers/AlterPwd",
            title: "修改密码",
            width: 500,
            height: 300,
            resizable: true,
            buttons: [
                {
                    text: '保存',
                    iconCls: 'icon-tick',
                    handler: function () {
                        if ($('#form-detail').form('validate')) {
                            $Ajax({
                                url: "/SysUsers/SaveAlterPwd",
                                data: $("#form-detail").serializeArray(),
                                errorBack: function (result) {
                                    $(".yzmimg").attr('src', '/Yzm/Create?_=' + GetGuid());
                                },
                                callBack: function (result) {
                                    top.window.location.href = "/Home/Login";
                                }
                            });
                        }
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cross',
                    handler: CloseDialog
                }
            ]
        });
    });
});