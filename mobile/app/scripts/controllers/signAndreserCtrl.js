angular.module('tuanxiao.controller')

//报名
.controller('signUpCtrl', ['ENV', '$rootScope', '$scope', '$state', 'sigAndreserService', 'dataService', function(ENV, $rootScope, $scope, $state, sigAndreserService, dataService) {
        $scope.banName = dataService.signUpBanName;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: false,
            number: 0
        };

    }])
    // 预约
    .controller('reservationCtrl', ['ENV', '$rootScope', '$scope', '$state', 'sigAndreserService', function(ENV, $rootScope, $scope, $state, sigAndreserService) {

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
