angular.module('tuanxiao.controller')

//团校课程
.controller('courseListCtrl', ['ENV', '$rootScope', '$scope', '$state', 'courseService', function(ENV, $rootScope, $scope, $state, courseService) {
         $rootScope.loading = true;
        // 设置nav
        $rootScope.headerActive = {
            active: true,
            number: 2
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
        $scope.courseList = [];
        $scope.getCourseList = function() {
            courseService.getCourseList($scope.loadObj, function(response) {
                if (response.Status == 1 && response.Data) {
                    angular.forEach(response.Data.Items, function(item, index) {
                        $scope.courseList.push(item);
                    });
                    $scope.pageCount = response.Data.TotalPageCount;
                    $scope.loadObj.busy = false;
                     $rootScope.loading = false;
                }
            });
        };
        $scope.getCourseList();

        //滑动获取数据
        $scope.loadMore = function() {
            if ($scope.loadObj.busy === true) {
                return;
            } else {
                if ($scope.loadObj.PageIndex < $scope.pageCount) {
                    $scope.loadObj.PageIndex++;
                    $scope.loadObj.busy = true;
                    $scope.getCourseList();
                } else return;
            }
        };

    }])
    .controller('courseDetailCtrl', ['ENV', '$rootScope', '$scope', '$state', '$stateParams', 'courseService', 'comService', function(ENV, $rootScope, $scope, $state, $stateParams, courseService, comService) {
        $rootScope.loading = true;
        $scope.courseId=$stateParams.courseId;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: false,
            number: 0
        };
        // 获取数据
        courseService.getCourseDetail($stateParams.courseId, function(response) {
            if (response.Status == 1 && response.Data) {
                $scope.courseDetail = response.Data;
                 $rootScope.loading = false;
            }
        });
        //------------------------ 获取评论------------------------
        // 滚动加载获取数据
        $scope.loadObj = {
            PageIndex: 1,
            Condition: [{
                "GroupName": "Course",
                "FieldName": "TargetId",
                "FieldValue": $stateParams.courseId,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }],
            busy: false //未到底部时，为不忙状态
        };
        // 获取列表
        $scope.comList = [];
        $scope.TotalItemCount = 0;
        $scope.getComList = function() {
            comService.getComList($scope.loadObj, function(response) {
                if (response.Status == 1 && response.Data) {
                    angular.forEach(response.Data.Items, function(item, index) {
                        item.stars = [];
                        for (var i = 0; i < 5; i++) {
                            if (i < item.Grade) {
                                item.stars.push({
                                    active: true,
                                    url: 'imgs/star_s.png'
                                });
                            } else {
                                item.stars.push({
                                    active: false,
                                    url: 'imgs/star.png'
                                });
                            }
                        }

                        $scope.comList.push(item);
                    });
                    $scope.pageCount = response.Data.TotalPageCount;
                    $scope.TotalItemCount = response.Data.TotalItemCount;
                    $scope.loadObj.busy = false;
                }
            });
        };
        $scope.getComList();

        //滑动获取数据
        $scope.loadMore = function() {
            if ($scope.loadObj.busy === true) {
                return;
            } else {
                if ($scope.loadObj.PageIndex < $scope.pageCount) {
                    $scope.loadObj.PageIndex++;
                    $scope.loadObj.busy = true;
                    $scope.getComList();
                } else return;
            }
        };
        // 是否展开评论
        $scope.commentNum = 3;
        $scope.openCom = false;
        $scope.openComment = function() {
            $scope.commentNum = $scope.commentNum == 3 ? 'auto' : 3;
            if ($scope.openCom == false) {
                $scope.openCom = true;
            } else {
                $scope.openCom = false;
            }
        };
    }]);
