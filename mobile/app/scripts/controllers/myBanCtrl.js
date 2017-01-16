angular.module('tuanxiao.controller')
    //我的课程列表
    .controller('myBanCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'userService', 'dataService', function(ENV, $rootScope, $scope, $cookieStore, $state, userService, dataService) {
        $rootScope.loading = true;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: true,
            number: 4
        };
        $scope.isfinish = false; //默认未结束
        $scope.unbanList = [];
        $scope.endbanList = [];
        // 滚动加载获取数据
        $scope.unfinishObj = {
            PageIndex: 1,
            pageCount: "",
            Condition: [{
                "GroupName": "myban",
                "FieldName": "State",
                "FieldValue": 0,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }, {
                "GroupName": "myban",
                "FieldName": "State",
                "FieldValue": 1,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }, {
                "GroupName": "myban",
                "FieldName": "State",
                "FieldValue": 2,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }],
            busy: false //未到底部时，为不忙状态
        };
        $scope.finishObj = {
            PageIndex: 1,
            pageCount: "",
            Condition: [{
                "GroupName": "myban",
                "FieldName": "State",
                "FieldValue": 3,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }, {
                "GroupName": "myban",
                "FieldName": "State",
                "FieldValue": 4,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }],
            busy: false //未到底部时，为不忙状态
        };
        $scope.goUnfinish = function() {
            $scope.isfinish = false;


        };
        if ($cookieStore.get("Authorization")) {
            getUnfinish();
        } else {
            userService.sendCode(function() {
                getUnfinish();
            });
        } //默认未结束
        $scope.goFinish = function() {
            if ($scope.finishObj.PageIndex == 1 && $scope.isfinish == false) {
                $scope.isfinish = true;
                $rootScope.loading = true;
                if ($cookieStore.get("Authorization")) {
                    getFinish();
                } else {
                    userService.sendCode(function() {
                        getFinish();
                    });
                }
            } else {
                $scope.isfinish = true;
            }

        };
        // 获取列表


        function getUnfinish() {
            getdata($scope.unfinishObj, $scope.unbanList);
        }

        function getFinish() {
            getdata($scope.finishObj, $scope.endbanList);
        }

        function getdata(paramObj, array) {
            userService.getMyBan(paramObj, function(response) {
                if (response.Status == 1) {
                    if (response.Data) {
                        angular.forEach(response.Data.Items, function(item, index) {
                            array.push(item);
                        });
                        paramObj.pageCount = response.Data.TotalPageCount;
                        paramObj.busy = false;
                    }
                    $rootScope.loading = false;
                }
            });
        }
        //滑动获取数据


        $scope.loadMore = function() {

            if ($scope.isfinish === false) {
                paramObj = $scope.unfinishObj;
            } else {
                paramObj = $scope.nfinishObj;
            }
            if (paramObj.busy === true) {
                return;
            } else {
                if (paramObj.PageIndex < paramObj.pageCount) {
                    paramObj.PageIndex++;
                    paramObj.busy = true;
                    console.log('滚动unfinish 333');
                    if ($scope.isfinish === false) {
                        getUnfinish();
                    } else {
                        getFinish();
                    }
                } else return;
            }
        };
        // -------------去评论-----------
        $scope.gotoComment = function(u) {
            dataService.commentName = u.Name;
            $state.go("myToComment", {
                targId: u.TrainId,
                type: 1
            });
        };
        // ----------预览结业证书------------
        $scope.openCertificate = function(u) {
            wx.previewImage({
                current: u.CertificateURL, // 当前显示图片的http链接
                urls: [u.CertificateURL] // 需要预览的图片http链接列表
            });
        };
        // 跳转详情
        $scope.gomyBanDetail = function(u) {
            dataService.myBanDetailObj = u;
            console.log(dataService.myBanDetailObj);
            $state.go("myBanDetail", {
                banId: u.TrainId
            });
        };
    }])
    // 课程详情
    .controller('myBanDetailCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', '$stateParams', 'userService', 'banService', 'dataService', 'comService', 'baseService', function(ENV, $rootScope, $scope, $cookieStore, $state, $stateParams, userService, banService, dataService, comService, baseService) {
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

        function getInfo() {
            //获取课程详情
            if (!dataService.myBanDetailObj) {

                userService.getMyBanDetail($stateParams.banId, function(response) {
                    if (response.Status == 1 && response.Data) {
                        $scope.banDetail = response.Data;
                        $rootScope.loading = false;
                    }
                });
            } else {
                $scope.banDetail = dataService.myBanDetailObj;
                $rootScope.loading = false;
            }
            // ---------------获取我的培训班资料----------------
            userService.getMyZl($scope.banId, function(response) {
                if (response.Status == 1 && response.Data) {
                    $scope.attachment = response.Data;
                    //展示图片张数
                    var s = Math.ceil($scope.attachment.length / 3);
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
            });
            //获取评论列表
            $scope.getComList();
        }


        // 轮播

        function picShow() {
            setTimeout(function() {
                baseService.slide('dt_pic_div', function() {
                    if ($scope.slidePageIndex >= $scope.slidePage.length - 1) {
                        return;
                    } else {
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
            comService.getMyCom($scope.loadObj, function(response) {
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
        // ----------预览结业证书------------
        $scope.openCertificate = function() {
            wx.previewImage({
                current: u.CertificateURL, // 当前显示图片的http链接
                urls: [u.CertificateURL] // 需要预览的图片http链接列表
            });
        };
        // ----------预览学时证明------------
        $scope.openClassHours = function() {
            wx.previewImage({
                current: u.ClassHoursURL, // 当前显示图片的http链接
                urls: [u.ClassHoursURL] // 需要预览的图片http链接列表
            });
        };

        // 获取数据
        if ($cookieStore.get("Authorization")) {
            getInfo();
        } else {
            userService.sendCode(function() {
                getInfo();
            });
        }


    }])
    // 通讯录
    .controller('myBanContactCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', '$stateParams', 'userService', 'dataService', function(ENV, $rootScope, $scope, $cookieStore, $state, $stateParams, userService, dataService) {
        $rootScope.loading = true;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: false,
            number: 0
        };
        $scope.searchName = "";
        $scope.loadObj = {
            PageIndex: 1,
            Condition: [{
                "GroupName": "myCourse",
                "FieldName": "TrainId",
                "FieldValue": $stateParams.banId,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }, {
                "GroupName": "studentname",
                "FieldName": "Name",
                "FieldValue": $scope.searchName,
                "SqlOperator": 'Like',
                "IsQuery": true
            }],
            busy: false //未到底部时，为不忙状态

        };

        // 获取列表
        $scope.contactList = [];

        function getInfo() {
            //获取培训班基础信息
            if (!dataService.myBanDetailObj) {
                userService.getMyBanDetail($stateParams.banId, function(response) {
                    if (response.Status == 1 && response.Data) {
                        $scope.banDetail = response.Data;
                    }
                });
            } else {
                $scope.banDetail = dataService.myBanDetailObj;
            }
            //获取通讯录
            $scope.getMyBanContact();
        }
        $scope.getMyBanContact = function() {
            userService.getMyBanContact($scope.loadObj, function(response) {
                if (response.Status == 1 && response.Data) {
                    angular.forEach(response.Data.Items, function(item, index) {
                        $scope.contactList.push(item);
                    });
                    $scope.pageCount = response.Data.TotalPageCount;
                    $scope.loadObj.busy = false;
                    $rootScope.loading = false;
                }
            });
        };

        //滑动获取数据
        $scope.loadMore = function() {
            if ($scope.loadObj.busy === true) {
                return;
            } else {
                if ($scope.loadObj.PageIndex < $scope.pageCount) {
                    $scope.loadObj.PageIndex++;
                    $scope.loadObj.busy = true;
                    $scope.getMyBanContact();
                } else return;
            }
        };
        //获取数据
        if ($cookieStore.get("Authorization")) {
            getInfo();
        } else {
            userService.sendCode(function() {
                getInfo();
            });
        }
        // 搜索
        $scope.seachData = function() {
            $scope.contactList = [];
            $scope.loadObj.PageIndex = 1;
            $scope.loadObj.Condition[1].FieldValue = $scope.searchName;
            $scope.getMyBanContact();
        };
    }])
    // 课程表
    .controller('myBanCourseCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', '$stateParams', 'userService', 'dataService', function(ENV, $rootScope, $scope, $cookieStore, $state, $stateParams, userService, dataService) {
        $rootScope.loading = true;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: false,
            number: 0
        };
        $scope.loadObj = {
            PageIndex: 1,
            Condition: [{
                "GroupName": "myCourse",
                "FieldName": "TrainId",
                "FieldValue": $stateParams.banId,
                "SqlOperator": 'Equal',
                "IsQuery": true
            }]
        };

        function getInfo() {
            //获取培训班基础信息
            if (!dataService.myBanDetailObj) {
                userService.getMyBanDetail($stateParams.banId, function(response) {
                    if (response.Status == 1 && response.Data) {
                        $scope.banDetail = response.Data;
                    }
                });
            } else {
                $scope.banDetail = dataService.myBanDetailObj;
            }
            //获取课程表
            userService.getMyBanCourse($scope.loadObj, function(response) {
                if (response.Status == 1) {
                    if (response.Data&&response.Data.Items) {
                        $scope.banTable = response.Data.Items;
                    }
                    $rootScope.loading = false;
                }
            });
        }

        // 获取数据
        if ($cookieStore.get("Authorization")) {
            getInfo();
        } else {
            userService.sendCode(function() {
                getInfo();
            });
        }
    }]);
