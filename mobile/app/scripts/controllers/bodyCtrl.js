angular.module('tuanxiao.controller')

//首页
.controller('bodyCtrl', ['ENV', '$rootScope', '$cookieStore', '$scope', '$state', 'baseService', 'userService', 'elseService', function(ENV, $rootScope, $cookieStore, $scope, $state, baseService, userService, elseService) {
 $rootScope.loading=true;
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
    //----------js-SDK-----
    //调用后台接口--获取签名
    elseService.getJsSdk();

    // wx.ready(function() {

    // });
    // wx.error(function(res) {

    // });


}]);
