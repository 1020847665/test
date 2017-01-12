angular.module('tuanxiao.services')
    .factory('elseService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getJsSdk: {
                method: 'GET',
                url: ENV.api + "WeChat/GetWeChatConfig"
            }
        });
        return {
            getJsSdk: function() {
                return resource.getJsSdk({
                        url: encodeURIComponent(location.href.split('#')[0])
                    }, null,
                    function(response) {
                        if (response.Status == 1) {
                            //-------------------------权限验证配置注入----------
                            wx.config({
                                debug: false, //是否弹出错误信息
                                appId: ENV.appid,
                                timestamp: response.Data.Timestamp,
                                nonceStr: response.Data.NonceStr,
                                signature: response.Data.Signature,
                                jsApiList: ['getLocation', 'previewImage']
                            });
                        }
                    });
            }
        };
    }]);
