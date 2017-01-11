angular.module('tuanxiao.services')
    .factory('userService', ['$resource', 'ENV', 'baseService', function($resource, ENV, baseService) {
        var resource = $resource(ENV.api, {}, {
            sendCode: {
                method: 'GET',
                url: ENV.api + "WeChat/StudentAuthorize"
            }
        });
        return {
            // 获取code授权
            sendCode: function() {
                var code = baseService.getUrlParams('code');
                var url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + ENV.appid + "&redirect_uri=" + ENV.redirectUrl + "&response_type=code&scope=" + ENV.scope + "&state=" + ENV.state;
                if (!code) {
                    location.href = url;
                }
                return resource.sendCode({
                    code: code
                }, null, function(response) {
                    if (response.Status == 1 && response.Data) {
                        window.Authorization = response.Data.TokenType + " " + response.Data.AccessToken;
                    } else if (response.Status == 0) {
                        location.href = url;
                    }
                });
            }
        };
    }]);
