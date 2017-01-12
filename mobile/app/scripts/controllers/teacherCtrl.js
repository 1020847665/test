angular.module('tuanxiao.controller')

//教师
.controller('teacherListCtrl', ['ENV', '$rootScope', '$scope', '$state', 'teacherService', function(ENV, $rootScope, $scope, $state, teacherService) {
         $rootScope.loading = true;
        // 设置nav
        $rootScope.headerActive = {
            active: true,
            number: 3
        };
        $rootScope.footerActive = {
            active: true,
            number: 1
        };
        // 滚动加载获取数据
        $scope.loadObj = {
            PageIndex: 1,
            busy: false //未到底部时，为不忙状态
        };
        // 获取列表
        $scope.teacherList = [];
        $scope.getTeacherList = function() {
            teacherService.getTeacherList($scope.loadObj, function(response) {
                if (response.Status == 1 && response.Data) {
                    angular.forEach(response.Data.Items, function(item, index) {
                        $scope.teacherList.push(item);
                    });
                    $scope.pageCount = response.Data.TotalPageCount;
                    $scope.loadObj.busy = false;
                     $rootScope.loading = false;
                }
            });
        };
        $scope.getTeacherList();

        //滑动获取数据
        $scope.loadMore = function() {
            if ($scope.loadObj.busy === true) {
                return;
            } else {
                if ($scope.loadObj.PageIndex < $scope.pageCount) {
                    $scope.loadObj.PageIndex++;
                    $scope.loadObj.busy = true;
                    $scope.getTeacherList();
                } else return;
            }
        };
    }])
    .controller('teacherDetailCtrl', ['ENV', '$rootScope', '$scope', '$state', '$stateParams', 'teacherService', function(ENV, $rootScope, $scope, $state, $stateParams, teacherService) {
         $rootScope.loading = true;
         $scope.teacherId=$stateParams.teacherId;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 1
        };
        $rootScope.footerActive = {
            active: false,
            number: 1
        };
        // 获取数据
        teacherService.getTeacherDetail($stateParams.teacherId, function(response) {
            if (response.Status == 1 && response.Data) {
                $scope.tDetail = response.Data;
                 $rootScope.loading = false;
            }
        });
    }]);
