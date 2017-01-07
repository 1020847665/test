angular.module('tuanxiao.controller')

//团校课程
.controller('courseListCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {
        // 设置nav
        $rootScope.headerActive = {
            active: true,
            number: 2
        };
        $rootScope.footerActive = {
            active: true,
            number: 1
        };

    }])
    .controller('courseDetailCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: false,
            number: 0
        };

    }]);
