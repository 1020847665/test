angular.module('tuanxiao.controller')

//教师
.controller('teacherListCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {
  // 设置nav
    $rootScope.headerActive = {
        active: true,
        number: 3
    };
    $rootScope.footerActive = {
        active: true,
        number: 1
    };

    }])
    .controller('teacherDetailCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {
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