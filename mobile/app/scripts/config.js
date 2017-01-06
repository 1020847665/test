"use strict";

angular.module("tuanxiao.config", [])
    .factory("ENV", function() {
        return {
            "version": "1.0.0",
            "name": "tuanxiao",
            "description": "成都市团校微信网页开发",
            "api": "http://121.40.206.191:8200/api/",//测试接口
            // "api": "http://121.40.206.191:8300/api/",//正式接口
            // 公众号调用接口获取access_token测试凭证
            'grant_type': 'client_credential',
            'appid': 'wx3baf35a100f869ae', //正式号
            'scope': 'snsapi_userinfo', //非静默授权，获取用户信息
            'state': '11#wechat_redirect',
            shareUrl:'http://mwexin.tuo-si.com/test/index.html?leadTo=share',
            'tencentKey': 'LK3BZ-3KYH6-63KSW-ML6BD-4EO63-PGF3E' //腾讯地图key
        };

    });
