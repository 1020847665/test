var editPicArray = []; //存储贴纸数据

// 自定义指令
angular.module("tuanxiao.directive")
    // 星星评分
    .directive("star", function() {
        return {
            scope: {
                grade: '=',
                type: '@' //判断是打分还是显示分数
            },
            restrict: 'E',
            template: '<span class="dt_star" data-content="&#xe601;" ng-repeat="n in zjGradeArray" ng-click="makeGrade($index)">' +
                '<span class="dt_star_red" style="width:{{n.width}};" data-content="&#xe610;" ></span></span>',
            link: function(scope, element, attrs) {
                scope.zjGradeArray = [{
                    index: 1,
                    width: 0
                }, {
                    index: 2,
                    width: 0
                }, {
                    index: 3,
                    width: 0
                }, {
                    index: 4,
                    width: 0
                }, {
                    index: 5,
                    width: 0
                }];
                // 根据分数设置宽度
                if (scope.type == 'show') {
                    console.log('分数', scope.grade);
                    for (var i = 0; i < scope.zjGradeArray.length; i++) {
                        if (i <= parseFloat(scope.grade) - 1) {
                            scope.zjGradeArray[i].width = "100%";
                        } else {
                            var y = parseFloat(scope.grade) % 1 * 100;
                            scope.zjGradeArray[i].width = y + "%";
                        }
                    }
                }
                scope.makeGrade = function(index) {
                    event.stopPropagation();
                    if (scope.type == "show") {
                        //若只是显示分数，则无法点击
                        return;
                    } else if (scope.type == "make") {
                        //点击评分
                        for (var i = 0; i < scope.zjGradeArray.length; i++) {
                            if (i <= index) {
                                scope.zjGradeArray[i].width = "100%";
                            } else {
                                scope.zjGradeArray[i].width = 0;
                            }
                        }
                        scope.grade = index + 1;
                        // console.log(scope.grade);
                    }
                }
            }
        }
    })
    .directive('alert', function() {
        return {
            restrict: 'A',
            link: function(scope, element, attrs) {
                scope.alertCar = function() {
                    $rootScope.alert = true;
                    $rootScope.addCarAlert = true;
                    $(".alert_sec").css({
                        opacity: 1,
                        transition: 'all 0.5s linear',
                        webkitTransition: 'all 0.5s linear'
                    })
                    setTimeout(function() {
                        $(".alert_content").css({
                            bottom: '0',
                            transition: 'all 0.5s linear',
                            webkitTransition: 'all 0.5s linear'
                        })
                    }, 0);
                }

            }
        }
    })
    // 默认图片设置
    .directive('nullPic', function() {
        return {
            scope: {
                pic: '=', //背景数据
                bgType: '@' //背景形式
            },
            restrict: 'A',
            link: function(scope, element, attrs) {
                if (scope.pic == null || scope.pic == "") {
                    if (scope.bgType == 'OrgCoverBig') {
                        scope.pic = 'imgs/null.png'; //机构详情页轮播默认图
                    } else if (scope.bgType == 'OrgCover') {
                        scope.pic = 'imgs/sm_null.png'; //机构默认封面
                    } else if (scope.bgType == 'OrgLogo') {
                        scope.pic = 'imgs/logo_null.png'; //机构默认封面
                    }
                }
                //如果有值，那么就设置图片的高的值等于宽度值
                if (attrs.nullPic) {
                    element.css("height", element.css("width"));
                }

            }
        }
    })
    //返回顶部
    .directive('backTop', function() {
        return {
            restrict: 'A',
            replace: true,
            template: '<div class="back-top"  ng-show="isShowBackTop"></div>',
            link: function(scope, element, attrs, ctrl) {
                // 监听页面滚动的事件
                document.onscroll = listenScroll;
                scope.isShowBackTop = false; //默认是  不显示
                //页面销毁事件
                scope.$on("$destroy", function() {
                    //解除滚动事件监听
                    document.onscroll = null;
                })

                if (attrs.backTop) {
                    element.css({
                        right: attrs.backTop.right,
                        bottom: attrs.backTop.bottom
                    });
                }

                //监听事件
                function listenScroll() {
                    var scrollContent = document.activeElement;
                    if (scrollContent.scrollTop + scrollContent.offsetHeight - 100 > scrollContent.scrollHeight) {
                        scope.isShowBackTop = true;
                    } else {
                        scope.isShowBackTop = false;
                    }
                    scope.$apply();
                }

                element.bind("click touchend", function() {
                    document.body.scrollTop = 0;
                });
            }
        }
    }) //IOS输入法造成的 fixed显示混乱
    .directive('iosFixed', function() {
        return {
            restrict: 'A',
            link: function(scope, element, attr) {
                element.bind('focus', function() {
                    angular.element(document).find('.header.list_header').css({
                        "top": "0px",
                        "postion": 'absolute'
                    });
                });
                element.bind('focusout', function() {
                    element.css("position", "fixed");
                });
            }
        }
    })

// 输入框上移
.directive('inputMove', function() {
    return {
        restrict: 'A',
        link: function(scope, element, attr) {
            element.bind('focus', function() {
                var h = element.offset().top;
                setTimeout(function() {
                    document.body.scrollTop = h;
                }, 500);
            });
        }
    }
})



// 常用函数
/**
 * 1.获取url中参数
 * 2.地图相关函数
 */
angular.module("tuanxiao.services")
    .factory("baseService", ['ENV', '$state', function(ENV, $state) {
        return {
            // 地理位置显示
            showMap: function(id, latitude, longitude) {
                var map = new qq.maps.Map(document.getElementById(id), {
                    center: new qq.maps.LatLng(latitude, longitude), // 地图的中心地理坐标。
                    zoom: 15, // 地图的中心地理坐标。
                    panControl: false, //平移控件的初始启用/停用状态。      
                    zoomControl: false, //缩放控件的初始启用/停用状态。
                    scaleControl: false, //滚动控件的初始启用/停用状态。
                    draggable: true, //设置是否可以拖拽
                    scrollwheel: false, //设置是否可以滚动
                    disableDoubleClickZoom: false //设置是否可以双击放大
                });
                var marker = new qq.maps.Marker({
                    map: map,
                    position: new qq.maps.LatLng(latitude, longitude)
                });
            },
            /**经纬度转换为具体地址
             * [getAddress description]
             * @return {[type]} [description]
             */
            getAddress: function(latitude, longitude, callback) {
                var geocoder = new qq.maps.Geocoder();
                var latLng = new qq.maps.LatLng(latitude, longitude);
                geocoder.getAddress(latLng);
                geocoder.setComplete(function(result) {
                    var address = result.detail.address;
                    console.log(address);
                    callback && callback();
                });
            },

            /**验证手机号
             * [validatemobile description]
             * @param  {[type]}   mobile   [description]
             * @param  {Function} callback [description]
             * @return {[type]}            [description]
             */
            validatemobile: function(mobile, callback1, callback2) {
                var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(17[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
                if (mobile.length != 11 || !myreg.test(mobile)) {
                    callback1();
                } else {
                    callback2();
                }
            },
            /**日期转时间戳
             * [tranTime description]
             * @param  {[type]} date [description]
             * @return {[type]}      [description]
             */
            tranTime: function(date) {
                date = date.replace(/-/g, '/');
                var timestamp = new Date(date).getTime();
                return timestamp;
            },
            /**时间戳转日期
             * [tranDate description]
             * @param  {[type]} timestamp [description]
             * @return {[type]}           [description]
             */
            tranDate: function(timestamp, type) {
                var d = new Date(timestamp); //根据时间戳生成的时间对象
                var m = d.getMonth() + 1;
                var day = d.getDate();
                if (parseInt(m) < 10) {
                    m = '0' + m;
                }
                if (parseInt(day) < 10) {
                    day = '0' + day;
                }
                if (type == 'dot') {
                    var date = (d.getFullYear()) + "." +
                        m + "." + day;
                } else {
                    var date = (d.getFullYear()) + "-" +
                        m + "-" + day;
                }

                return date;
            },
            /**滑动事件
             * [slide description]
             * @param  {[type]} id            [description]
             * @param  {[type]} callbackLeft  [description]
             * @param  {[type]} callbackRight [description]
             * @return {[type]}               [description]
             */
            slide: function(id, callbackLeft, callbackRight, callbackClick, prevent) {
                var startPos, endPos; //起始和结束位置
                // console.log( document.body.clientWidth);
                var windowWidth = document.body.clientWidth;
                //touchstart事件  
                function touchSatrtFunc(event) {
                    try {
                        console.log('touchStart');
                        if (prevent == null) {
                            event.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等 
                        }
                        var touch = event.touches[0]; //获取第一个触点 
                        startPos = {
                                x: Number(touch.pageX), //页面触点X坐标 
                                y: Number(touch.pageY) //页面触点Y坐标 
                            }
                            //记录触点初始位置
                        endPos = {
                            x: 0,
                            y: 0
                        };
                    } catch (e) {
                        alert('touchSatrtFunc：' + e.message);
                    }
                }

                //touchmove事件，这个事件无法获取坐标  
                function touchMoveFunc(event) {
                    try {
                        if (prevent == null) {
                            event.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等 
                        }
                        var touch = event.touches[0]; //获取第一个触点  
                        var x = Number(touch.pageX); //页面触点X坐标  
                        var y = Number(touch.pageY); //页面触点Y坐标  
                        endPos = {
                            x: x - startPos.x,
                            y: y - startPos.y
                        };
                    } catch (e) {
                        console.log('touchMoveFunc：' + e.message);
                    }
                }

                //touchend事件  
                function touchEndFunc(event) {
                    try {
                        if (prevent == null) {
                            event.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等 
                        }
                        var slideScale = endPos.x / windowWidth; //滑动相对屏幕比例
                        console.log('滑动比例', startPos.x, endPos.x);
                        if (slideScale > 0.1) {
                            if (callbackRight) {
                                callbackRight();
                            }
                        } else if (slideScale < -0.1) {
                            if (callbackLeft) {
                                callbackLeft();
                            }

                        } else {
                            if (callbackClick) {
                                callbackClick();
                            }
                        }
                    } catch (e) {
                        console.log('touchEndFunc：' + e.message);
                    }
                }
                //绑定事件  
                function bindEvent() {
                    var element=document.getElementById(id);
                    console.log(element);
                    element.addEventListener('touchstart', touchSatrtFunc,false);
                    element.addEventListener('touchmove', touchMoveFunc,false);
                    element.addEventListener('touchend', touchEndFunc,false);
                }
                bindEvent();
            },
            /**数组移除指定值元素
             * [removeArray description]
             * @param  {[type]} array [description]数组
             * @param  {[type]} value [description]指定值
             * @return {[type]}       [description]
             */
            removeArray: function(array, value) {
                Array.prototype.indexOf = function(val) {
                    for (var i = 0; i < this.length; i++) {
                        if (this[i] == val) return i;
                    }
                    return -1;
                };
                Array.prototype.remove = function(val) {
                    var index = this.indexOf(val);
                    if (index > -1) {
                        this.splice(index, 1);
                    }
                };
                array.remove(value);
            },
            // 获取code
            getCode: function(callback) {
                if ($cookieStore.get("openId")) {
                    window.openId = $cookieStore.get("openId");
                } else {
                    var code = dataService.getUrlParams('code');
                    // 将code传参给后台
                    userService.sendCode(code, function(response) {
                        if (response.Status == 1 && response.Data != "") {
                            window.openId = response.Data;
                            $cookieStore.put("openId", window.openId);
                            console.log('发送code成功！');
                            if (callback) {
                                callback();
                            }
                        }
                    });
                }

            },
            // 定位
            getLocation: function(callbackSucc, callbackFail) {
                wx.getLocation({
                    type: 'wgs84', // 默认为wgs84的gps坐标，如果要返回直接给openLocation用的火星坐标，可传入'gcj02'
                    success: function(res) {
                        //alert("定位成功");
                        var userLocation = {
                            latitude: res.latitude,
                            longitude: res.longitude
                        };
                    },
                    fail: function(res) {
                        console.log('定位失败');
                        alert("定位失败");

                    },
                    cancel: function(res) {
                        alert("定位取消");

                    }
                });
            }
        }
    }]);


/*

filter 过滤器
 */
angular.module("tuanxiao.filters")
    .filter('trustAsHtml', ['$sce', function($sce) {
        return function(text) {
            return $sce.trustAsHtml(text);
        };
    }]);
