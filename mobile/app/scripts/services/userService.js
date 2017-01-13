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
            getMyZl: {
                method: 'GET', //获取我的培训班资料
                url: ENV.api + "WeChat/GetTrainClassAttachment"
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
            }
        };
    }]);
