angular.module('tuanxiao.controller')

//培训班
.controller('banListCtrl', ['ENV', '$rootScope', '$scope', '$state', 'baseService', 'banService', function(ENV, $rootScope, $scope, $state, baseService, banService) {
        $rootScope.loading = true;
        // 设置nav
        $rootScope.headerActive = {
            active: true,
            number: 1
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
        $scope.banList = [];
        $scope.getBanList = function() {
            banService.getBanList($scope.loadObj, function(response) {
                if (response.Status == 1 && response.Data) {
                    angular.forEach(response.Data.Items, function(item, index) {
                        $scope.banList.push(item);
                    });
                    $scope.pageCount = response.Data.TotalPageCount;
                    $scope.loadObj.busy = false;
                    $rootScope.loading = false;
                }
            });
        };
        $scope.getBanList();

        //滑动获取数据
        $scope.loadMore = function() {
            if ($scope.loadObj.busy === true) {
                return;
            } else {
                if ($scope.loadObj.PageIndex < $scope.pageCount) {
                    $scope.loadObj.PageIndex++;
                    $scope.loadObj.busy = true;
                    $scope.getBanList();
                } else return;
            }
        };

    }])
    .controller('banDetailCtrl', ['ENV', '$rootScope', '$scope', '$state', '$stateParams', 'banService', 'comService', 'dataService', function(ENV, $rootScope, $scope, $state, $stateParams, banService, comService, dataService) {
        $rootScope.loading = true;
        $scope.banId = $stateParams.banId;

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
        banService.getBanDetail($stateParams.banId, function(response) {
            if (response.Status == 1 && response.Data) {
                $scope.banDetail = response.Data;
                // 去报名的班級名
                dataService.signUpBanName = $scope.banDetail.Name;
                $rootScope.loading = false;
            }
        });
        //------------------------ 获取评论------------------------
        // 滚动加载获取数据
        $scope.loadObj = {
            PageIndex: 1,
            Condition: [{
                "GroupName": "ban",
                "FieldName": "TargetId",
                "FieldValue": $stateParams.banId,
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
