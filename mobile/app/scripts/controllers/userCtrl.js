angular.module('tuanxiao.controller')

//个人中心
.controller('perCenterCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'userService', 'dataService', function(ENV, $rootScope, $scope, $cookieStore, $state, userService, dataService) {
        $rootScope.loading = false;
        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 0
        };
        $rootScope.footerActive = {
            active: true,
            number: 4
        };
        if ($cookieStore.get("Authorization")) {
            getInfo();
        } else {
            userService.sendCode(function() {
                getInfo();
            });
        }
        // 获取信息
        function getInfo() {
            //个人信息
            userService.getUserInfo(function(response) {
                if (response.Status == 1 && response.Data) {
                    $scope.userInfo = response.Data;
                }
            });
            // 判断是否可考勤
            userService.getCheckBan(function(response) {
                if (response.Status == 1 && response.Data) {
                    $scope.isCheck = true; //允许考勤
                    dataService.checkBanObj = response.Data; //我要考勤页直接获取，若无，才重新请求
                }
            });
        }

    }])
    // 个人信息
    .controller('myInfoCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'userService', 'baseService', function(ENV, $rootScope, $scope, $cookieStore, $state, userService, baseService) {
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

        $scope.updateUserInfo = function() {
            if ($scope.userInfo.Name === "" || $scope.userInfo.Sex === "" || $scope.userInfo.MobileNumber === "" || $scope.userInfo.Organization === "" || $scope.userInfo.Position === "") {
                $scope.nullNot = true;
            } else {
                $scope.nullNot = false;
                if ($scope.mobileNot == false && $scope.emailNot == false) {
                    $scope.isSubmitClick = true;
                    $scope.btnMsg = "提交中...";
                    //提交信息
                    userService.updateUserInfo($scope.userInfo, function(response) {
                        if (response.Status == 1) {
                            $scope.toastMsg = "修改成功";
                            $scope.istoast = true;
                        } else if (response.Status == 0) {
                            $scope.toastMsg = "修改失败";
                            $scope.istoast = true;
                            $scope.isSubmitClick = false;
                            $scope.btnMsg = "提交";
                        }
                    });
                }

            }
        };
        $scope.successCallback = function() {
            $state.go("user");
        };


    }])
    // 我要考勤
    .controller('myAttendCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'userService', 'dataService', function(ENV, $rootScope, $scope, $cookieStore, $state, userService, dataService) {

    }])
    // 我要报到
    .controller('myRegisterCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'userService', 'dataService', function(ENV, $rootScope, $scope, $cookieStore, $state, userService, dataService) {

    }])
    //去评论
    .controller('myToCommentCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', '$stateParams', 'userService', 'dataService', 'comService', function(ENV, $rootScope, $scope, $cookieStore, $state, $stateParams, userService, dataService, comService) {
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
        $scope.commentMsg = {
            TargetId: $stateParams.targId,
            Type: parseInt($stateParams.type),
            Grade: 0,
            Body: ""
        };
        $scope.btnMsg = "发表";
        $scope.istoast = false;
        $scope.nullNot = false;
        // 星星
        $scope.stars = [];
        for (var i = 0; i < 5; i++) {
            $scope.stars.push({
                active: false,
                url: 'imgs/star.png'
            });
        }
        $scope.addGrade = function(index) {
            for (var i = 0; i < 5; i++) {
                if (i <= index) {
                    $scope.stars[i] = {
                        active: true,
                        url: 'imgs/star_s.png'
                    };
                } else {
                    $scope.stars[i] = {
                        active: false,
                        url: 'imgs/star.png'
                    };
                }
            }
            $scope.commentMsg.Grade = index + 1;
        };
        // 提交评论
        $scope.addCom = function() {
            if ($scope.commentMsg.Grade > 0 && $scope.commentMsg.Body != "") {
                $scope.nullNot = false;
                $scope.btnMsg = "发表中...";
                $scope.isSubmitClick = true;
                comService.addCom($scope.commentMsg, function(response) {
                    if (response.Status == 1) {
                        $scope.toastMsg = "评论成功";
                        $scope.istoast = true;

                    } else if (response.Status == 0) {
                        $scope.toastMsg = "评论失败";
                        $scope.istoast = true;
                        $scope.isSubmitClick = false;
                        $scope.btnMsg = "发表";
                    }
                });
            } else {
                $scope.nullNot = true;
            }

        };
        $scope.successCallback = function() {
            history.go(-1);
        };
    }]);
