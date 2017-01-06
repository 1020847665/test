angular.module('tuanxiao.controller')

//资料
.controller('zlListCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {

  // 设置nav
    $rootScope.headerActive = {
        active: false,
        number: 0
    };
    $rootScope.footerActive = {
        active: true,
        number: 3
    };
    }])
    .controller('zlDetailCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {
// 设置nav
    $rootScope.headerActive = {
        active: false,
        number: 1
    };
    $rootScope.footerActive = {
        active: false,
        number: 1
    };

    }]);
