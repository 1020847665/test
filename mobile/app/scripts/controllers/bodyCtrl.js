angular.module('tuanxiao.controller')

//首页
.controller('bodyCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {

    // 设置nav
    $rootScope.headerActive = {
        active: true,
        number: 1
    };
    $rootScope.footerActive = {
        active: true,
        number: 1
    };
}]);
