
///登录
function Login() {
    var loginuser = $(".loginuser").val();
    var loginpwd = $(".loginpwd").val();
    var yzmcode = $(".yzmCode").val();
    if (loginuser != "") {
        if (loginpwd != "") {
            if (yzmcode != "") {
                $Ajax({
                    url: "/Home/Login",
                    loadMsg: "登录中...",
                    data: $("#form-login").serializeJson(),
                    errorBack: function (result) {
                        $(".yzmimg").attr('src', '/Yzm/Create?_=' + GetGuid());
                    },
                    callBack: function (result) {
                        window.location.href = "/Layout/Main";
                    }
                });
            }
            else $.messager.alert('提示', "请输入验证码！", 'warning');
        } else $.messager.alert('提示', "请输入登录密码！", 'warning');
    } else $.messager.alert('提示', "请输入登录用户名！", 'warning');
}