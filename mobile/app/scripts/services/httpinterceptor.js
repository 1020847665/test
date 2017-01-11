angular.module('tuanxiao.services')
    .factory('httpInterceptor', ['$q', function($q) {
        return {
            'request': function(config) {
                extendHeaders(config);
                return config;
            },
            'requestError': function(rejection) {
                return $q.reject(rejection);
            },
            'response': function(response) {
                return response || $q.when(response);
            },
            'responseError': function(rejection) {
                return $q.reject(rejection);
            }
        };

        /** 
         *config: 请求配置信息
         */
        function extendHeaders(config) {
            config.headers = config.headers || {};
            // config.headers['Authorization'] = "bearer"+" "+"968Js2l16tvRyrP5hCf239rixtQzFyiL43WFjdjmZf_vgQIZ8XjC_T-RnuNJPZ-ayak5zTLOQvCP--UcvLIkgY9rn_g4o5aycPkLlW2h6buwXfb90ZFtpgZDKfJR4JZUc_8dbftpUiCET8kYmrjd4kEVvd4BK1BukkyRYBRg-2_lL1ZAlaZsKHuiULpPtDmdrmsg7BWuExgIj7oXJjQcrz9FS1QagplziYUfUwNItPhrteeAtAib12Udv_l5vG_6F4MYFwwAEEnnax6b08gwYjPbsTCIXaxdJZLR7hHlHPnm_k8dzqfTwXb7iaR4TpPQ0aEniLKOj81Pu2Mkxt5GTv6N8mO75RXto6U-iGPL1K42j_7ZNZCh6vEcx1R6xMwlkaigIOCQzShOdTbC4kuawxRljqK8WPY7r5qwnXn25CMD1ioq";
            config.headers['Authorization'] = window.Authorization;
        }

    }]);
