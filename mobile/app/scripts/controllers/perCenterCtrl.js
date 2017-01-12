angular.module('tuanxiao.controller')

//首页
.controller('perCenterCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'userService', 'dataService', function(ENV, $rootScope, $scope, $cookieStore, $state, userService, dataService) {
    $rootScope.loading = false;
    // 设置nav
    $rootScope.headerActive = {
        active: false,
        number: 0
    };
    $rootScope.footerActive = {
        active: true,
        number: 4
    };
    // 获取信息
    userService.getUserInfo(function(response) {
        if (response.Status == 1 && response.Data) {
            $scope.userInfo = response.Data;
        }
    });
    // 判断是否可考勤
    userService.getCheckBan(function(response) {
        if (response.Status == 1 && response.Data) {
            $scope.isCheck = true; //允许考勤
            dataService.checkBanObj = response.Data;//我要考勤页直接获取，若无，才重新请求
        }
    });

}]);
