angular.module("tuanxiao.services")
    .factory('dataService', [function() {
        return {
            signUpBanName: "", //报名培训班名
            checkBanObj: null, //允许考勤的班级信息
            commentName: "", //评论的班级/课程名
            myBanDetailObj: null, //跳转至我的课程详情时
            userInfo: null
        };

    }]);



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
                if (String(mobile).length != 11 || !myreg.test(mobile)) {
                    callback1();
                } else {
                    callback2();
                }
            },
            /**验证邮箱
             * [validateemail description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-12
             * @param    {[type]}                 email     [description]
             * @param    {[type]}                 callback1 [description]
             * @param    {[type]}                 callback2 [description]
             * @return   {[type]}                           [description]
             */
            validateemail: function(email, callback1, callback2) {
                var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
                if (!myreg.test(email)) {
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
                date = String(date).replace(/-/g, '/');
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
                    var element = document.getElementById(id);
                    element.addEventListener('touchstart', touchSatrtFunc, false);
                    element.addEventListener('touchmove', touchMoveFunc, false);
                    element.addEventListener('touchend', touchEndFunc, false);
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
            /**获取url参数方法
             * [getUrlParams description]
             * @param  {[type]} key [description]
             * @return {[type]}     [description]
             */
            getUrlParams: function(key) {
                var args = {};
                var pairs = location.search.substring(1).split('&');
                for (var i = 0; i < pairs.length; i++) {
                    var pos = pairs[i].indexOf('=');
                    if (pos === -1) {
                        continue;
                    }
                    args[pairs[i].substring(0, pos)] = decodeURIComponent(pairs[i].substring(pos + 1));
                }
                return args[key];
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
        };
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
