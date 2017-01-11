"use strict";

angular.module("tuanxiao.config", [])
    .factory("ENV", function() {
        return {
            "version": "1.0.0",
            "name": "tuanxiao",
            "description": "成都市团校微信网页开发",
            "pageSize":5,//默认每页显示数量
            "api": "http://192.168.0.112:8654/api/", //测试接口
            // "api": "http://121.40.206.191:8300/api/",//正式接口
            // 公众号调用接口获取access_token测试凭证
            'grant_type': 'client_credential',
            'appid': 'wx0a3a8e71c747c955', //公众号
            'scope': 'snsapi_userinfo', //非静默授权，获取用户信息
            'state': '11#wechat_redirect',
            'tencentKey': 'LK3BZ-3KYH6-63KSW-ML6BD-4EO63-PGF3E', //腾讯地图key
            'redirectUrl': 'localhost:8080/index.html'//默认回调地址
        };

    });
