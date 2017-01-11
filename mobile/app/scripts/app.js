//模块声明和导入
var app = angular.module('app', [
    'tuanxiao.controller',
    'ui.router',
    'ngCookies',
    'tuanxiao.filters',
    'tuanxiao.directive'
]);


app.config(['$urlRouterProvider', '$stateProvider', '$httpProvider', function($urlRouterProvider, $stateProvider, $httpProvider) {
    // 路由配置
    $stateProvider
        .state('banList', {
            url: '/banList',
            controller: 'banListCtrl',
            stateName: '培训班次',
            templateUrl: "views/ban-list.html"
        })
        .state('banDetail', {
            url: '/banDetail/:banId',
            controller: 'banDetailCtrl',
            stateName: '培训班详情',
            templateUrl: "views/ban-detail.html"
        })
        .state('signUp', {
            url: '/signUp/:banId',
            controller: 'signUpCtrl',
            stateName: '报名填写',
            auth: true, //设置是否授权
            templateUrl: "views/signUp.html"
        })
        .state('reservation', {
            url: '/reservation/:banId/:type',
            // type:1-调训班,2-课程,3-教师,4-定制
            controller: 'reservationCtrl',
            stateName: '预约填写',
            auth: true,
            templateUrl: "views/reservation.html"
        })
        .state('teacherList', {
            url: '/teacherList',
            controller: 'teacherListCtrl',
            stateName: '教师风采',
            templateUrl: "views/teacher-list.html"
        })
        .state('teacherDetail', {
            url: '/teacherDetail/:teacherId',
            controller: 'teacherDetailCtrl',
            stateName: '教师详情',
            templateUrl: "views/teacher-detail.html"
        })
        .state('zlList', {
            url: '/zlList',
            controller: 'zlListCtrl',
            stateName: '资料中心',
            templateUrl: "views/zl-list.html"
        })
        .state('zlDetail', {
            url: '/zlDetail/:zlId',
            controller: 'zlDetailCtrl',
            stateName: '资料详情',
            templateUrl: "views/zl-detail.html"
        })
        .state('courseList', {
            url: '/courseList',
            controller: 'courseListCtrl',
            stateName: '团校课程',
            templateUrl: "views/course-list.html"
        })
        .state('courseDetail', {
            url: '/courseDetail/:courseId',
            controller: 'courseDetailCtrl',
            stateName: '课程详情',
            templateUrl: "views/course-detail.html"
        })
        .state('perCenter', {
            url: '/perCenter',
            controller: 'perCenterCtrl',
            stateName: '个人中心',
            templateUrl: "views/personal-center.html"
        })
        // .state('myInfo', {
        //     url: '/myInfo',
        //     controller: 'myInfoCtrl',
        //     stateName: '个人信息',
        //     templateUrl: "views/my-info.html"
        // })
        // .state('myBan', {
        //     url: '/myBan',
        //     controller: 'myBanCtrl',
        //     stateName: '我的课程',
        //     templateUrl: "views/my-courses.html"
        // })
        // .state('myBanDetail', {
        //     url: '/myBanDetail',
        //     controller: 'myBanDetailCtrl',
        //     stateName: '课程详情',
        //     templateUrl: "views/my-courses-detail.html"
        // })
        // .state('myBanContact', {
        //     url: '/myBanContact',
        //     controller: 'myBanContactCtrl',
        //     stateName: '班级通讯录',
        //     templateUrl: "views/my-contact-list.html"
        // })
        // .state('myBanContact', {
        //     url: '/myBanContact',
        //     controller: 'myBanContactCtrl',
        //     stateName: '课程表',
        //     templateUrl: "views/my-course-table.html"
        // })
        // .state('myReservation', {
        //     url: '/myReservation',
        //     controller: 'myReservationCtrl',
        //     stateName: '我的预约',
        //     templateUrl: "views/my-reservation.html"
        // })
        // .state('myRegister', {
        //     url: '/myRegister',
        //     controller: 'myRegisterCtrl',
        //     stateName: '我要报到',
        //     templateUrl: "views/my-register.html"
        // })
        // .state(' myAttend', {
        //     url: '/myAttend',
        //     controller: 'myAttendCtrl',
        //     stateName: '我要考勤',
        //     templateUrl: "views/my-attendance.html"
        // });



    //默认访问的页面地址
    $urlRouterProvider.otherwise('/banList');
    //上传文件http配置信息
    $httpProvider.defaults.transformRequest = [function(data) {
        return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
    }];
    $httpProvider.defaults.headers.common['Accept'] = 'application/json, text/javascript';
    // $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded; charset=UTF-8';
    $httpProvider.defaults.headers.post['Content-Type'] = 'application/json;charset=UTF-8';

    // 配置http拦截器
    $httpProvider.interceptors.push('httpInterceptor');


}]);
app.run(['$rootScope', '$state', 'userService', function($rootScope, $state, userService) {
    //路由拦截器切换之前
    $rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
        if (toState.auth === true && !window.Authorization) {
            // 授权
            userService.sendCode();
        }
    });
    //路由切换成功之后
    $rootScope.$on('$stateChangeSuccess', function(event, toState, toParams, fromState, fromParams) {
        document.body.scrollTop = 0; //置顶
        document.title = toState.stateName; //设置title  android 直接就可以修改成功
        //微信中的浏览器中单页面第一次加载成功后，不再监听tile的变化,通过iframe来触发 页面重新来监听
        // var $body = $('body');
        // var $iframe = $("<iframe style='opacity: 0;' src='/favicon.ico'></iframe>");
        // $iframe.on('load', function() {
        //     setTimeout(function() {
        //         $iframe.off('load').remove();
        //     }, 0);
        // }).appendTo($body);
        var iframe = document.createElement('iframe');
        iframe.setAttribute("src", "/favicon.ico");
        iframe.setAttribute("style", "opacity:0");
        iframe.onload = function() {
            iframe.appendTo(body);
            setTimeout(function() {
                iframe.offload = function() {
                    body.removeChild(iframe);
                };
            }, 0);
        };
    });
    //post请求时需要格式化参数（编码和序列化）
    window.param = function(obj) {
        var query = '',
            name, value, fullSubName, subName, subValue, innerObj, i;
        for (name in obj) {
            value = obj[name];

            if (value instanceof Array) {
                for (i = 0; i < value.length; ++i) {
                    subValue = value[i];
                    fullSubName = name + '[' + i + ']';
                    innerObj = {};
                    innerObj[fullSubName] = subValue;
                    query += param(innerObj) + '&';
                }
            } else if (value instanceof Object) {
                for (subName in value) {
                    subValue = value[subName];
                    fullSubName = name + '[' + subName + ']';
                    // fullSubName = name + '.' + subName;
                    innerObj = {};
                    innerObj[fullSubName] = subValue;
                    query += param(innerObj) + '&';
                }

            } else if (value !== undefined && value !== null)
                query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
        }
        return query.length ? query.substr(0, query.length - 1) : query;
    };
    var RootFontSize = document.body.offsetWidth / 750 * 16;
    document.getElementsByTagName("html")[0].style.fontSize = RootFontSize + "px";
    window.onresize = function() {
        RootFontSize = document.body.offsetWidth / 750 * 16;
        document.getElementsByTagName("html")[0].style.fontSize = RootFontSize + "px";
    };
}]);
angular.module('tuanxiao.controller', ['tuanxiao.services']);
angular.module('tuanxiao.services', ['tuanxiao.config', 'ngResource']);
angular.module('tuanxiao.directive', ['tuanxiao.services']);
angular.module('tuanxiao.filters', []);
