angular.module('tuanxiao.controller')

//报名
.controller('signUpCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 1
        };
        $rootScope.footerActive = {
            active: false,
            number: 1
        };

    }])
    // 预约
    .controller('reservationCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {

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
