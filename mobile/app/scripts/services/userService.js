angular.module('tuanxiao.services')
    .factory('userService', ['$resource', 'ENV', 'baseService', '$cookieStore', function($resource, ENV, baseService, $cookieStore) {
        var resource = $resource(ENV.api, {}, {
            sendCode: {
                method: 'GET',
                url: ENV.api + "WeChat/StudentAuthorize"
            },
            getUserInfo: {
                method: 'GET',
                url: ENV.api + "WeChat/GetStudentDetail"
            },
            updateUserInfo: {
                method: 'POST',
                url: ENV.api + "WeChat/UpdateStudentDetail"
            },
            getCheckBan: {
                method: 'GET', //获取可考勤的培训班信息
                url: ENV.api + "WeChat/GetCheckTrainClass"
            },
            getMyBan: {
                method: 'POST',
                url: ENV.api + "WeChat/GetStudentTrainClass"
            },
            getMyBanDetail: {
                method: 'GET',
                url: ENV.api + "WeChat/GetStudentTrainClassDetail"
            },
            getMyZl: {
                method: 'GET', //获取我的培训班资料
                url: ENV.api + "WeChat/GetTrainClassAttachment"
            },
            getMyBanCourse: {
                method: 'POST', //获取课程表
                url: ENV.api + "WeChat/GetTrainClassCoursePageList"

            },
            getMyBanContact: {
                method: 'POST', //获取通讯录
                url: ENV.api + "WeChat/TrainContacts"

            },
            getMyReserve: {
                method: 'POST', //获取我的预约
                url: ENV.api + "WeChat/GeStudentBook"
            }
        });
        return {
            // 获取code授权
            sendCode: function(callback) {
                var code = baseService.getUrlParams('code');
                var url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + ENV.appid + "&redirect_uri=" + ENV.redirectUrl + "&response_type=code&scope=" + ENV.scope + "&state=" + ENV.state;
                if (!code) {
                    location.href = url;
                }
                return resource.sendCode({
                    code: code
                }, null, function(response) {
                    if (response.Status == 1 && response.Data) {
                        var Authorization = response.Data.TokenType + " " + response.Data.AccessToken;
                        $cookieStore.put("Authorization", Authorization);
                        callback && callback();
                    } else if (response.Status == 0) {
                        location.href = url;
                    }
                });
            },
            //获取学员信息
            getUserInfo: function(callback) {
                return resource.getUserInfo({}, {}, function(response) {
                    callback && callback(response);
                });
            },
            /**修改学员信息
             * [updateUserInfo description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-12
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            updateUserInfo: function(obj, callback) {
                return resource.updateUserInfo(null, JSON.stringify({
                        Name: obj.Name,
                        Sex: obj.Sex,
                        Position: obj.Position,
                        Organization: obj.Organization,
                        MobileNumber: obj.MobileNumber,
                        Email: obj.Email
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            // 获取可考勤的培训班信息
            getCheckBan: function(callback) {
                return resource.getCheckBan({}, {}, function(response) {
                    callback && callback(response);
                });
            },
            // 获取我的培训班信息
            getMyBan: function(obj, callback) {
                return resource.getMyBan(null, JSON.stringify({
                    PageIndex: obj.PageIndex,
                    PageSize: ENV.pageSize,
                    IsPage: true,
                    Condition: obj.Condition
                }), function(response) {
                    callback && callback(response);
                });
            },
            /**获取我的培训班详情
             * [getMyBanDetail description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-16
             * @param    {[type]}                 banId    [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getMyBanDetail: function(banId, callback) {
                return resource.getMyBanDetail({
                    trainId: banId
                }, null, function(response) {
                    callback && callback(response);
                });
            },
            /**获取我的课程资料
             * [getMyZl description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-13
             * @param    {[type]}                 banId    [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getMyZl: function(banId, callback) {
                return resource.getMyZl({
                    trainId: banId
                }, null, function(response) {
                    callback && callback(response);
                });
            },
            //获取培训班课程表
            getMyBanCourse: function(obj, callback) {
                return resource.getMyBanCourse(null, JSON.stringify({
                    PageIndex: obj.PageIndex,
                    PageSize: ENV.pageSize,
                    IsPage: false,
                    Condition: obj.Condition
                }), function(response) {
                    callback && callback(response);
                });
            },
            //获取通讯录
            getMyBanContact: function(obj, callback) {
                return resource.getMyBanContact(null, JSON.stringify({
                    PageIndex: obj.PageIndex,
                    PageSize: ENV.pageSize,
                    IsPage: true,
                    Condition: obj.Condition
                }), function(response) {
                    callback && callback(response);
                });
            },
            //我的预约
            getMyReserve: function(obj, callback) {
                return resource.getMyReserve(null, JSON.stringify({
                    PageIndex: obj.PageIndex,
                    PageSize: ENV.pageSize,
                    IsPage: true,
                    Condition: obj.Condition
                }), function(response) {
                    callback && callback(response);
                });
            }

        };
    }]);
