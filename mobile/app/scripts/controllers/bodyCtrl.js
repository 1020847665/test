angular.module('tuanxiao.controller')

//首页
.controller('bodyCtrl', ['ENV', '$rootScope', '$scope', '$state', 'baseService', 'userService', function(ENV, $rootScope, $scope,$state, baseService, userService) {

    // 设置nav
    $rootScope.headerActive = {
        active: true,
        number: 1
    };
    $rootScope.footerActive = {
        active: true,
        number: 1
    };
    document.getElementsByTagName("header")[0].style.display = "block";
    document.getElementsByTagName("footer")[0].style.display = "block";
     // 授权
    userService.sendCode();
    //-------------------------------------------------js-SDK----------------------------------------------
    //调用后台接口--获取签名
    // elseService.getTicket(encodeURIComponent(location.href.split('#')[0]), function(response) {
    //         if (response.Status == 1) {
    //             //-------------------------权限验证配置注入----------
    //             wx.config({
    //                 debug: false, //是否弹出错误信息
    //                 appId: ENV.appid,
    //                 timestamp: response.Data.Timestamp,
    //                 nonceStr: response.Data.NonceStr,
    //                 signature: response.Data.Signature,
    //                 jsApiList: ['getLocation', 'previewImage', 'chooseImage', 'chooseWXPay', 'onMenuShareTimeline', 'onMenuShareAppMessage']
    //             });
    //         }
    //     })
    // wx.ready(function() {

    // });
    // wx.error(function(res) {

    // });

   
}]);
