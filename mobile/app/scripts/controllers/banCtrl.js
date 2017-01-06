angular.module('tuanxiao.controller')

//培训班
.controller('banListCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {
  // 设置nav
    $rootScope.headerActive = {
        active: true,
        number: 1
    };
    $rootScope.footerActive = {
        active: true,
        number: 1
    };

    }])
    .controller('banDetailCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {

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
