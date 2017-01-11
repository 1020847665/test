angular.module('tuanxiao.controller')

//资料
.controller('zlListCtrl', ['ENV', '$rootScope', '$scope', '$state', 'zlService', function(ENV, $rootScope, $scope, $state, zlService) {

        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: true,
            number: 3
        };
        // 滚动加载
        $scope.loadObj = {
            PageIndex: 1,
            busy: false //未到底部时，为不忙状态
        };
        $scope.seachData = function() {
            if (!$scope.searchName) return;
            $scope.zlList = [];
            $scope.loadObj.PageIndex = 1;
            $scope.loadObj.Condition = [{
                "GroupName": "zlname",
                "FieldName": "Name",
                "FieldValue": $scope.searchName,
                "SqlOperator": 'Like',
                "IsQuery": true
            }];
            $scope.getzlList();
        };
        // 获取列表
        $scope.zlList = [];
        $scope.getzlList = function() {
            zlService.getZlList($scope.loadObj, function(response) {
                if (response.Status == 1 && response.Data) {
                    angular.forEach(response.Data.Items, function(item, index) {
                        $scope.zlList.push(item);
                    });
                    $scope.pageCount = response.Data.TotalPageCount;
                    $scope.loadObj.busy = false;
                }
            });
        };
        $scope.getzlList();

        //滑动获取数据
        $scope.loadMore = function() {
            if ($scope.loadObj.busy === true) {
                return;
            } else {
                if ($scope.loadObj.PageIndex < $scope.pageCount) {
                    $scope.loadObj.PageIndex++;
                    $scope.loadObj.busy = true;
                    $scope.getzlList();
                } else return;
            }
        };
    }])
    .controller('zlDetailCtrl', ['ENV', '$rootScope', '$scope', '$state', '$stateParams', 'baseService', 'zlService', function(ENV, $rootScope, $scope, $state, $stateParams, baseService, zlService) {
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
        zlService.getZlDetail($stateParams.zlId, function(response) {
            if (response.Status == 1 && response.Data) {
                $scope.zlDetail = response.Data;
                // $scope.zlDetail.Url="/test.mp3";
                if ($scope.zlDetail.Attachment && $scope.zlDetail.Attachment.length > 0) {
                    //展示图片张数
                    var s = Math.ceil($scope.zlDetail.Attachment.length / 3);
                    $scope.slidePage = []; //图片展示页数
                    if (s > 0) {
                        for (var n = 0; n < s; n++) {
                            $scope.slidePage.push({
                                index: n,
                                active: false
                            });
                        }
                        $scope.slidePage[0].active = true;
                        $scope.slidePageIndex = 0;
                        picShow();
                    }
                }
            }
        });

        // 轮播

        // $scope.showImgList = [{
        //     url: 'imgs/imgTest/zl_video.png'
        // }, {
        //     url: 'imgs/imgTest/zl_video.png'
        // }, {
        //     url: 'imgs/imgTest/zl_video.png'
        // }, {
        //     url: 'imgs/imgTest/zl_video.png'
        // }];


        function picShow() {
            setTimeout(function() {
                baseService.slide('dt_pic_div', function() {
                    if ($scope.slidePageIndex >= $scope.slidePage.length - 1) {
                        return;
                    } else {
                        console.log('左滑');
                        $scope.slidePageIndex++;
                        for (var i = 0; i < $scope.slidePage.length; i++) {
                            $scope.slidePage[i].active = false;
                        }
                        $scope.slidePage[$scope.slidePageIndex].active = true;
                        $scope.$apply();
                        var dt = document.getElementById("dt_pic_div");
                        dt.style.transform = "translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.transition = "all 0.5s ease-out";
                        dt.style.webkitTransform = "translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.webkitTransition = "all 0.5s ease-out";

                    }
                }, function() {
                    if ($scope.slidePageIndex <= 0) {
                        return;
                    } else {
                        console.log('右滑');
                        $scope.slidePageIndex--;
                        for (var i = 0; i < $scope.slidePage.length; i++) {
                            $scope.slidePage[i].active = false;
                        }
                        $scope.slidePage[$scope.slidePageIndex].active = true;
                        $scope.$apply();
                        var dt = document.getElementById("dt_pic_div");
                        dt.style.transform = "translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.transition = "all 0.5s ease-out";
                        dt.style.webkitTransform = "translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.webkitTransition = "all 0.5s ease-out";

                    }
                }, null, false);
            }, 100);

        }

    }]);
