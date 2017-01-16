angular.module('tuanxiao.controller')

//报名
.controller('signUpCtrl', ['ENV', '$rootScope', '$scope', '$state', '$stateParams', '$cookieStore', 'sigAndreserService', 'dataService', 'baseService', 'userService', function(ENV, $rootScope, $scope, $state, $stateParams, $cookieStore, sigAndreserService, dataService, baseService, userService) {
        $rootScope.loading = false;
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
        if ($cookieStore.get("Authorization")) {
            getUserInfo();
        } else {
            userService.sendCode(function() {
                getUserInfo();
            });
        }
        $scope.btnMsg = "提交";
        $scope.istoast = false;
        $scope.userInfo = {
            Name: "",
            Sex: "",
            MobileNumber: "",
            Email: "",
            Organization: "",
            Position: ""
        };
        // 获取学员信息
        function getUserInfo() {
            userService.getUserInfo(function(response) {
                if (response.Status == 1 && response.Data) {
                    $scope.userInfo = response.Data;
                    $scope.userInfo.MobileNumber = Number($scope.userInfo.MobileNumber);
                }
            });

        }
        $scope.nullNot = false; //默认通过
        $scope.mobileNot = false;
        $scope.emailNot = false;
        // 验证表单
        $scope.validatemobile = function() {

            baseService.validatemobile($scope.userInfo.MobileNumber, function() {
                $scope.mobileNot = true;
            }, function() {
                $scope.mobileNot = false;
            });

        };
        $scope.validateemail = function() {
            baseService.validateemail($scope.userInfo.Email, function() {
                $scope.emailNot = true;
            }, function() {
                $scope.emailNot = false;
            });
        };

        $scope.signUp = function() {
            if ($scope.userInfo.Name === "" || $scope.userInfo.Sex === "" || $scope.userInfo.MobileNumber === "" || $scope.userInfo.Organization === "" || $scope.userInfo.Position === "") {
                $scope.nullNot = true;
            } else {
                $scope.nullNot = false;
                if ($scope.mobileNot == false && $scope.emailNot == false) {
                    $scope.isSubmitClick = true;
                    $scope.btnMsg = "提交中...";
                    //提交信息
                    var paramObj = angular.copy($scope.userInfo);
                    paramObj.TrainId = $stateParams.banId;
                    sigAndreserService.signUp(paramObj, function(response) {
                        if (response.Status == 1) {
                            $scope.toastMsg = "报名成功";
                            $scope.istoast = true;

                        } else if (response.Status == 0) {
                            $scope.toastMsg = "报名失败";
                            $scope.istoast = true;
                            $scope.isSubmitClick = false;
                            $scope.btnMsg = "提交";
                        }
                    });
                }

            }
        };
        $scope.successCallback = function() {
            $state.go("banList");
        };



    }])
    // 预约
    .controller('reservationCtrl', ['ENV', '$rootScope', '$scope', '$state', '$stateParams', '$cookieStore', 'sigAndreserService', 'userService', 'baseService', function(ENV, $rootScope, $scope, $state, $stateParams, $cookieStore, sigAndreserService, userService, baseService) {
        $rootScope.loading = false;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: false,
            number: 0
        };
        $scope.btnMsg = "提交";
        $scope.istoast = false;
        if ($cookieStore.get("Authorization")) {
            getUserInfo();
        } else {
            userService.sendCode(function() {
                getUserInfo();
            });
        }
        $scope.userInfo = {
            Name: "",
            Sex: "",
            MobileNumber: "",
            Email: "",
            Organization: "",
            Position: ""
        };
        // 获取学员信息
        function getUserInfo() {
            userService.getUserInfo(function(response) {
                if (response.Status == 1 && response.Data) {
                    $scope.userInfo = response.Data;
                    $scope.userInfo.MobileNumber = Number($scope.userInfo.MobileNumber);
                }
            });

        }
        $scope.nullNot = false; //默认通过
        $scope.mobileNot = false;
        $scope.emailNot = false;
        // 验证表单
        $scope.validatemobile = function() {
            baseService.validatemobile($scope.userInfo.MobileNumber, function() {
                $scope.mobileNot = true;
            }, function() {
                $scope.mobileNot = false;
            });

        };
        $scope.validateemail = function() {
            baseService.validateemail($scope.userInfo.Email, function() {
                $scope.emailNot = true;
            }, function() {
                $scope.emailNot = false;
            });
        };
        $scope.reservation = {
            StartTime: new Date(),
            EndTime: new Date(),
            TrainOrganization: "",
            TrainAddress: "",
            TrainNumber: "",
            TrainNeeds: "",
            Type: Number($stateParams.type)
        };
        if ($stateParams.targId) {
            $scope.reservation.TargetId = $stateParams.targId;
        }
        $scope.reserve = function() {
            if ($scope.userInfo.Name === "" || $scope.userInfo.Sex === "" || $scope.userInfo.MobileNumber === "" || $scope.userInfo.Organization === "" || $scope.userInfo.Position === "" || !$scope.reservation.StartTime || !$scope.reservation.EndTime || $scope.reservation.TrainNumber === "" || $scope.reservation.TrainNeeds === "") {
                $scope.nullNot = true;
            } else {
                $scope.nullNot = false;
                if ($scope.mobileNot == false && $scope.emailNot == false) {
                    $scope.isSubmitClick = true;
                    $scope.btnMsg = "提交中...";
                    //提交学员信息
                    userService.updateUserInfo($scope.userInfo, function(response) {
                        if (response.Status == 1) {
                            $scope.reservation.StartTime = baseService.tranTime($scope.reservation.StartTime);
                            $scope.reservation.EndTime = baseService.tranTime($scope.reservation.EndTime);
                            // 提交预约信息
                            sigAndreserService.reserve($scope.reservation, function(response) {
                                if (response.Status == 1) {
                                    // 预约成功
                                    $state.go("reserveSuccess");
                                } else if (response.Status == 0) {
                                    // 预约失败
                                    $scope.toastMsg = "预约失败";
                                    $scope.istoast = true;
                                    $scope.isSubmitClick = false;
                                    $scope.btnMsg = "提交";
                                }
                            });
                        } else if (response.Status == 0) {
                            //预约失败
                            $scope.toastMsg = "预约失败";
                            $scope.istoast = true;
                            $scope.isSubmitClick = false;
                            $scope.btnMsg = "提交";
                        }
                    });

                }

            }
        };


    }])
    // 预约成功
    .controller('reserveSuccessCtrl', ['$rootScope', function($rootScope) {
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
    .controller('myReservationCtrl', ['$rootScope', '$scope', '$state','$cookieStore', 'userService','dataService', function($rootScope, $scope,$state, $cookieStore, userService,dataService) {
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
        // 滚动加载获取数据
        $scope.loadObj = {
            PageIndex: 1,
            busy: false //未到底部时，为不忙状态
        };
        // 获取列表
        $scope.myReserve = [];
        $scope.getMyReserve = function() {
            userService.getMyReserve($scope.loadObj, function(response) {
                if (response.Status == 1 && response.Data) {
                    angular.forEach(response.Data.Items, function(item, index) {
                        $scope.myReserve.push(item);
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
                    $scope.getMyReserve();
                } else return;
            }
        };
        // 获取数据
        if ($cookieStore.get("Authorization")) {
            $scope.getMyReserve();
        } else {
            userService.sendCode(function() {
                $scope.getMyReserve();
            });
        }
        // -------------去评论-----------
        $scope.gotoComment = function(u) {
            dataService.commentName = u.Name;
            $state.go("myToComment", {
                targId: u.TargetId,
                type: 1
            });
        };
    }]);
