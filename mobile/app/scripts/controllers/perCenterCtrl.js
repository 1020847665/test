angular.module('tuanxiao.controller')

//首页
.controller('perCenterCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {

    // 设置nav
    $rootScope.headerActive = {
        active: false,
        number: 0
    };
    $rootScope.footerActive = {
        active: true,
        number: 4
    };

}]);
