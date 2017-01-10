angular.module('tuanxiao.controller')

//资料
.controller('zlListCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'zlService', function(ENV, $rootScope, $scope, $cookieStore, $state, zlService) {

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
        $scope.searchName = "";
        $scope.loadObj = {
            PageIndex: 1,
            PageSize: 5,
            Condition: [{
                "GroupName": "zlname",
                "FieldName": "Name",
                "FieldValue": $scope.searchName,
                "SqlOperator": 'Like',
                "IsQuery": true
            }],
            busy: false //未到底部时，为不忙状态
        };
        // 获取列表
        $scope.getzlList = function() {
            zlService.getZlList($scope.loadObj, function(response) {
                if (response.Status == 1) {
                    $scope.zlList = response.Data.Items;
                    $scope.pageCount = $scope.TotalPageCount;
                    $scope.loadObj.busy = false;
                }
            });
        };

        //滑动获取数据
        // $scope.loadMore = function() {
        //     if ($scope.loadObj.busy === true) {
        //         return;
        //     } else {
        //         $scope.loadObj.busy = true;
        //         if ($scope.loadObj.PageIndex < $scope.pageCount) {
        //             $scope.loadObj.PageIndex++;
        //             $scope.getzlList();
        //         } else return;
        //     }
        // };
    }])
    .controller('zlDetailCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'baseService', function(ENV, $rootScope, $scope, $cookieStore, $state, baseService) {
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 1
        };
        $rootScope.footerActive = {
            active: false,
            number: 1
        };
        // 轮播

        $scope.showImgList = [{
            url: 'imgs/imgTest/zl_video.png'
        }, {
            url: 'imgs/imgTest/zl_video.png'
        }, {
            url: 'imgs/imgTest/zl_video.png'
        }, {
            url: 'imgs/imgTest/zl_video.png'
        }];
        //展示图片张数
        var s = Math.ceil($scope.showImgList.length / 3);
        $scope.slidePage = []; //图片展示页数
        if (s > 0) {
            for (var n = 0; n < s; n++) {
                $scope.slidePage.push({
                    index: n,
                    active: false
                });
            }
            $scope.slidePage[0].active = true;
            $scope.slidePageIndex=0;
            picShow();
        }

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
                        var dt=document.getElementById("dt_pic_div");
                        dt.style.transform="translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.transition="all 0.5s ease-out";
                         dt.style.webkitTransform="translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.webkitTransition="all 0.5s ease-out";
                        // $("#dt_pic_div").css({
                        //     transform: "translateX(" + (-$scope.slidePageIndex * 84.6) + "%)",
                        //     webkitTransform: "translateX(" + (-$scope.slidePageIndex * 84.6) + "%)",
                        //     transition: "all 0.5s ease-out",
                        //     webkitTransition: "all 0.5s ease-out"
                        // });
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
                         var dt=document.getElementById("dt_pic_div");
                          dt.style.transform="translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.transition="all 0.5s ease-out"; 
                           dt.style.webkitTransform="translateX(" + (-$scope.slidePageIndex * 96.6) + "%)";
                        dt.style.webkitTransition="all 0.5s ease-out";
                        // $("#dt_pic_div").css({
                        //     transform: "translateX(" + (-$scope.slidePageIndex * 84.6) + "%)",
                        //     webkitTransform: "translateX(" + (-$scope.slidePageIndex * 84.6) + "%)",
                        //     transition: "all 0.5s ease-out",
                        //     webkitTransition: "all 0.5s ease-out"
                        // });
                    }
                }, null, false);
            }, 100);

        }

    }]);
