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
                    angular.element(document).find('.header.list_header').css({ "top": "0px", "postion": 'absolute' });
                    // element.css("position", "absolute");
                });
                element.bind('focusout', function() {
                    element.css("position", "fixed");
                    //alert("fixed");
                });
            }
        }
    })
    // 倒计时
    .directive('backTime', function() {
        return {
            restrict: 'A',
            scope: {
                transactiontime: '=',
                servertime: '=',
                minute: '=',
                second: '='
            },
            link: function(scope, element, attr) {
                function getTime() {
                    var endTime = parseInt(scope.transactiontime + 30 * 60 * 1000);
                    scope.minute = parseInt((endTime - parseInt(scope.servertime)) / 60 / 1000);
                    scope.second = parseInt((endTime - parseInt(scope.servertime)) / 1000 - scope.minute * 60);
                    scope.servertime += 1000;
                    scope.$apply();
                }
                setInterval(function() {
                    getTime();
                }, 1000);
            }
        }
    })
    // sn码验证
    .directive('keyBord', ['$state', 'utilService', '$cookieStore', function($state, utilService, $cookieStore) {
        return {
            restrict: 'AEC',
            // replace: true,
            template: '<div class="padding33"><div class="ver_tip">请输入顾客的&nbsp;8&nbsp;&nbsp;位SN码</div><div><input readonly ng-repeat="v in snValues" class="sn-input" type="tel" maxlength="1" ng-model="v.value" disabled></div><div class="submit100 ver_submit" ng-click="checkSN()">确认消费</div><div class="red_tip" ng-if="snNotEffect">无效SN码，请重新输入</div></div><div class="keybord"><ul class="kb-keys"><li class="key number"><a>1</a></li><li class="key number"><a>2</a></li><li class="key number"><a>3</a></li><li class="key number"><a>4</a></li><li class="key number"><a>5</a></li><li class="key number"><a>6</a></li><li class="key number"><a>7</a></li><li class="key number"><a>8</a></li><li class="key number"><a>9</a></li><li class="key cancel"><a>/</a></li><li class="key number"><a>0</a></li><li class="key delete"><a>删除</a></li></ul></div>',
            link: function(scope, element, attrs, ctrl) {
                scope.snValues = [{
                    "value": ""
                }, {
                    "value": ""
                }, {
                    "value": ""
                }, {
                    "value": ""
                }, {
                    "value": ""
                }, {
                    "value": ""
                }, {
                    "value": ""
                }, {
                    "value": ""
                }];
                scope.cursor = 0; //游标指针，默认值为0，最大值为7
                element.find("li.key.number").bind("touchstart", function() {
                    scope.snNotEffect = false;
                    if (scope.cursor >= 8) {
                        return false;
                    }

                    scope.snValues[scope.cursor].value = $(this).text();
                    console.log(scope.snValues[scope.cursor].value);
                    scope.cursor = scope.cursor >= 8 ? 8 : scope.cursor + 1;
                    scope.$apply();

                });
                element.find("li.key.delete").bind("touchstart", function() {
                    scope.snNotEffect = false;
                    scope.cursor = scope.cursor <= 0 ? 0 : scope.cursor - 1;
                    scope.snValues[scope.cursor].value = "";
                    scope.$apply();
                });
                element.find('.submit100').bind('touchstart', function(event) {
                    if (attrs.keyBord) {
                        scope.$apply(attrs.keyBord); //校验SN码
                    } else {
                        new Error("请绑定校验方法");
                    }

                });
                // 验证SN码
                scope.snNotEffect = false;
                scope.checkSN = function() {

                    // 允许验证
                    scope.Sn = "";
                    if (scope.cursor == 8) {
                        for (var i = 0; i < scope.snValues.length; i++) {
                            scope.Sn += scope.snValues[i].value;
                        }
                        console.log(scope.Sn);
                        utilService.existSN(scope.Sn, $cookieStore.get("openId"), function(response) {
                            if (response.Status == 1) {
                                if (response.Data) {
                                    $state.go('chekSuccess', {
                                        OpenId: response.Data.OpenId, //验证的用户
                                        CardsNumber: response.Data.CardsNumber
                                    })
                                } else {
                                    scope.snNotEffect = true;
                                }
                            }
                        })
                    } else {
                        scope.snNotEffect = true;
                    }
                }
            }
        }
    }])
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
    }) // IOS H5上传图片出现旋转的问题
    .directive('iosInputFile', function() {
        return {
            restrict: 'A',
            link: function(scope, element, attr) {

                //对图片旋转处理 added by lzk  
                function rotateImg(img, direction, canvas) {
                    //alert(img);  
                    //最小与最大旋转方向，图片旋转4次后回到原方向    
                    var min_step = 0;
                    var max_step = 3;
                    //var img = document.getElementById(pid);    
                    if (img == null) return;
                    //img的高度和宽度不能在img元素隐藏后获取，否则会出错    
                    var height = img.height;
                    var width = img.width;
                    //var step = img.getAttribute('step');    
                    var step = 2;
                    if (step == null) {
                        step = min_step;
                    }
                    if (direction == 'right') {
                        step++;
                        //旋转到原位置，即超过最大值    
                        step > max_step && (step = min_step);
                    } else {
                        step--;
                        step < min_step && (step = max_step);
                    }
                    //img.setAttribute('step', step);    
                    /*var canvas = document.getElementById('pic_' + pid);   
                    if (canvas == null) {   
                        img.style.display = 'none';   
                        canvas = document.createElement('canvas');   
                        canvas.setAttribute('id', 'pic_' + pid);   
                        img.parentNode.appendChild(canvas);   
                    }  */
                    //旋转角度以弧度值为参数    
                    var degree = step * 90 * Math.PI / 180;
                    var ctx = canvas.getContext('2d');
                    switch (step) {
                        case 0:
                            canvas.width = width;
                            canvas.height = height;
                            ctx.drawImage(img, 0, 0);
                            break;
                        case 1:
                            canvas.width = height;
                            canvas.height = width;
                            ctx.rotate(degree);
                            ctx.drawImage(img, 0, -height);
                            break;
                        case 2:
                            canvas.width = width;
                            canvas.height = height;
                            ctx.rotate(degree);
                            ctx.drawImage(img, -width, -height);
                            break;
                        case 3:
                            canvas.width = height;
                            canvas.height = width;
                            ctx.rotate(degree);
                            ctx.drawImage(img, -width, 0);
                            break;
                    }
                }

                function selectFileImage(callback) {
                    var file = element.files['0'];
                    //图片方向角 added by lzk  
                    var Orientation = null;

                    if (file) {
                        console.log("正在上传,请稍后...");
                        var rFilter = /^(image\/jpeg|image\/png)$/i; // 检查图片格式  
                        if (!rFilter.test(file.type)) {
                            //showMyTips("请选择jpeg、png格式的图片", false);  
                            return;
                        }
                        // var URL = URL || webkitURL;  
                        //获取照片方向角属性，用户旋转控制  
                        EXIF.getData(file, function() {
                            // alert(EXIF.pretty(this));  
                            EXIF.getAllTags(this);
                            //alert(EXIF.getTag(this, 'Orientation'));   
                            Orientation = EXIF.getTag(this, 'Orientation');
                            //return;  
                        });

                        var oReader = new FileReader();
                        oReader.onload = function(e) {
                            //var blob = URL.createObjectURL(file);  
                            //_compress(blob, file, basePath);  
                            var image = new Image();
                            image.src = e.target.result;
                            image.onload = function() {
                                var expectWidth = this.naturalWidth;
                                var expectHeight = this.naturalHeight;

                                if (this.naturalWidth > this.naturalHeight && this.naturalWidth > 800) {
                                    expectWidth = 800;
                                    expectHeight = expectWidth * this.naturalHeight / this.naturalWidth;
                                } else if (this.naturalHeight > this.naturalWidth && this.naturalHeight > 1200) {
                                    expectHeight = 1200;
                                    expectWidth = expectHeight * this.naturalWidth / this.naturalHeight;
                                }
                                var canvas = document.createElement("canvas");
                                var ctx = canvas.getContext("2d");
                                canvas.width = expectWidth;
                                canvas.height = expectHeight;
                                ctx.drawImage(this, 0, 0, expectWidth, expectHeight);
                                var base64 = null;
                                //修复ios  
                                if (navigator.userAgent.match(/iphone/i)) {
                                    console.log('iphone');
                                    //alert(expectWidth + ',' + expectHeight);  
                                    //如果方向角不为1，都需要进行旋转 added by lzk  
                                    if (Orientation != "" && Orientation != 1) {
                                        // alert('旋转处理');
                                        switch (Orientation) {
                                            case 6: //需要顺时针（向左）90度旋转  
                                                // alert('需要顺时针（向左）90度旋转');
                                                rotateImg(this, 'left', canvas);
                                                break;
                                            case 8: //需要逆时针（向右）90度旋转  
                                                // alert('需要顺时针（向右）90度旋转');
                                                rotateImg(this, 'right', canvas);
                                                break;
                                            case 3: //需要180度旋转  
                                                // alert('需要180度旋转');
                                                rotateImg(this, 'right', canvas); //转两次  
                                                rotateImg(this, 'right', canvas);
                                                break;
                                        }
                                    }

                                    /*var mpImg = new MegaPixImage(image); 
                                    mpImg.render(canvas, { 
                                        maxWidth: 800, 
                                        maxHeight: 1200, 
                                        quality: 0.8, 
                                        orientation: 8 
                                    });*/
                                    base64 = canvas.toDataURL("image/jpeg", 0.8);
                                } else if (navigator.userAgent.match(/Android/i)) { // 修复android  
                                    var encoder = new JPEGEncoder();
                                    base64 = encoder.encode(ctx.getImageData(0, 0, expectWidth, expectHeight), 80);
                                } else {
                                    //alert(Orientation);  
                                    if (Orientation != "" && Orientation != 1) {
                                        //alert('旋转处理');  
                                        switch (Orientation) {
                                            case 6: //需要顺时针（向左）90度旋转  
                                                alert('需要顺时针（向左）90度旋转');
                                                rotateImg(this, 'left', canvas);
                                                break;
                                            case 8: //需要逆时针（向右）90度旋转  
                                                alert('需要顺时针（向右）90度旋转');
                                                rotateImg(this, 'right', canvas);
                                                break;
                                            case 3: //需要180度旋转  
                                                alert('需要180度旋转');
                                                rotateImg(this, 'right', canvas); //转两次  
                                                rotateImg(this, 'right', canvas);
                                                break;
                                        }
                                    }

                                    base64 = canvas.toDataURL("image/jpeg", 0.8);
                                }
                                //uploadImage(base64);  
                                element.attr("src", base64);
                            };
                        };
                        oReader.readAsDataURL(file);
                        callback && callback();
                    }
                }

                element.bind("change", function() {
                    selectFileImage(function() {
                        scope.$apply();
                    });
                });
            }
        }
    })

/**
 * 为baseservice提供公用函数
 */
angular.module("tuanxiao.services")
    .factory("dataService", ['$rootScope', '$cookieStore', 'userService', function($rootScope, $cookieStore, userService) {
        return {
            putUserInfo: function() {
                // 用户无法定位时默认为北京市政府
                // if ($cookieStore.get("userLocation")) {
                //     $rootScope.user.userLatitude = $cookieStore.get("userLocation").userLatitude;
                //     $rootScope.user.userLongitude = $cookieStore.get("userLocation").userLongitude;
                //     if (city) {
                //         $rootScope.user.city = city;
                //     } else {
                //         $rootScope.user.city = $cookieStore.get("userLocation").city;
                //         // console.log('用户', $cookieStore.get("userLocation").userLatitude);
                //     }
                // } else {
                if ($rootScope.user.city == undefined) {
                    $rootScope.user.city = '北京';
                    $rootScope.user.userLatitude = 39.904514;
                    $rootScope.user.userLongitude = 116.407248;
                }
                // else {
                //     $rootScope.user.city = city;

                // }
                // // }
                // $cookieStore.put("userLocation", {
                //     userLatitude: $rootScope.user.userLatitude,
                //     userLongitude: $rootScope.user.userLongitude,
                //     selectLatitude: $rootScope.user.userLatitude,
                //     selectLongitude: $rootScope.user.userLongitude,
                //     city: $rootScope.user.city
                // })
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
            }
        }
    }]);

// 常用函数
/**
 * 1.获取url中参数
 * 2.地图相关函数
 */
angular.module("tuanxiao.services")
    .factory("baseService", ['ENV', '$state', '$rootScope', '$cookieStore', 'userService', 'dataService', function(ENV, $state, $rootScope, $cookieStore, userService, dataService) {
        /**计算两点曲线距离
         * [getDistance description]
         * @param  {[type]} lat1 [description] 纬度1
         * @param  {[type]} ing1 [description]  经度1
         * @param  {[type]} lat2 [description]
         * @param  {[type]} ing2 [description]
         * @return {[type]}      [description]
         */
        function getDistance(lat1, ing1, lat2, ing2) {
            function rad(d) {
                return d * 3.1415926535898 / 180.0;
            }
            earth_radius = 6378.137; //地球的半径
            var radLat1 = rad(lat1);
            var radLat2 = rad(lat2);
            var a = radLat1 - radLat2;
            var b = rad(ing1) - rad(ing2);
            var s = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(a / 2), 2) +
                Math.cos(radLat1) * Math.cos(radLat2) * Math.pow(Math.sin(b / 2), 2)));
            s = s * earth_radius;
            s = Math.round(s * 100) / 100;
            return s;
        }

        return {
            // 缓存用户信息
            saveUserInfo: function() {
                $cookieStore.put("userLocation", {
                    userLatitude: $rootScope.user.userLatitude,
                    userLongitude: $rootScope.user.userLongitude,
                    selectLatitude: $rootScope.user.selectLatitude,
                    selectLongitude: $rootScope.user.selectLongitude,
                    city: $rootScope.user.city
                })
            },
            /**创建地图
             * [initMap description]
             * @param  {[type]} id            [description]地图显示位置id
             * @param  {[type]} nearLatitude  [description]最近商家纬度
             * @param  {[type]} nearLongitude [description]最近商家经度
             * @return {[type]}               [description]
             */
            initMap: function(id) {
                console.log('最近商家', $rootScope.nearObj.Lat);
                //定义map变量 调用 qq.maps.Map() 构造函数   获取地图显示容器
                var map = new qq.maps.Map(document.getElementById(id), {
                    center: new qq.maps.LatLng($rootScope.nearObj.Lat, $rootScope.nearObj.Lng), // 地图的中心地理坐标。
                    zoom: 13, // 地图的中心地理坐标。
                    panControl: false, //平移控件的初始启用/停用状态。      
                    zoomControl: false, //缩放控件的初始启用/停用状态。
                    scaleControl: true //滚动控件的初始启用/停用状态。

                });
                return map;
            },
            /**添加标记
             * [addMarker description]
             * @param {[type]} $rootScope.zj [description]商家信息
             * @param {[type]} nearPoint  [description]距离最近的点,默认标记
             */
            addMarker: function(map) {

                // console.log('当前范围内早教', $rootScope.zjNow);
                // 自定义标记样式
                var icon = new qq.maps.MarkerImage('imgs/local_p.png'),
                    icon2 = new qq.maps.MarkerImage('imgs/now.png');
                // var anchor = new qq.maps.Point(6, 6),
                //     size = new qq.maps.Size(30.4, 34.4),
                //     size2 = new qq.maps.Size(36.8, 42.4),
                //     origin = new qq.maps.Point(0, 0),
                //     icon = new qq.maps.MarkerImage('imgs/local_p.png', size, origin, anchor),
                //     icon2 = new qq.maps.MarkerImage('imgs/now.png', size2, origin, anchor);
                for (var i = 0; i < $rootScope.zjNow.length; i++) {
                    (function(n) {
                        var marker = new qq.maps.Marker({
                            icon: icon,
                            map: map,
                            position: new qq.maps.LatLng(
                                $rootScope.zjNow[n].Lat, $rootScope.zjNow[n].Lng)
                        });
                        qq.maps.event.addListener(marker, 'click', function() {
                            $rootScope.selectZjArray = [];

                            //点击选中某个早教对象,Y移动标记点
                            $rootScope.selectZj = $rootScope.zjNow[n];
                            markerSelect.setMap(null);

                            markerSelect = new qq.maps.Marker({
                                icon: icon2,
                                map: map,
                                position: new qq.maps.LatLng(
                                    $rootScope.selectZj.Lat, $rootScope.selectZj.Lng)
                            });
                            for (var i = 0; i < $rootScope.zjNow.length; i++) {
                                //相同经纬度的标记
                                if ($rootScope.zjNow[i].Lat == $rootScope.selectZj.Lat && $rootScope.zjNow[i].Lng == $rootScope.selectZj.Lng) {
                                    $rootScope.selectZjArray.push($rootScope.zjNow[i]);
                                }
                            }
                            // console.log('选中', $rootScope.selectZj);
                            // 重新刷新数据
                            var s = getDistance($rootScope.user.userLatitude, $rootScope.user.userLongitude,
                                $rootScope.selectZj.Lat, $rootScope.selectZj.Lng);
                            $rootScope.user.distance = Math.round(s * 10) / 10;

                            $rootScope.$apply();
                        });
                    })(i);
                }
                // 默认标记
                var markerSelect = new qq.maps.Marker({
                    icon: icon2,
                    map: map,
                    position: new qq.maps.LatLng(
                        $rootScope.nearObj.Lat, $rootScope.nearObj.Lng)
                });

            },
            /**经纬度获取城市
             * [getCity description]
             * @return {[type]} [description]
             */
            getCity: function(callback) {
                var geocoder = new qq.maps.Geocoder();
                var latLng = new qq.maps.LatLng($rootScope.user.userLatitude, $rootScope.user.userLongitude);
                geocoder.getAddress(latLng);
                geocoder.setComplete(function(result) {

                    // alert("通过经纬度获取城市成功");

                    var city = result.detail.addressComponents.city;
                    // var cityArray = city.split("市");
                    // $rootScope.user.city = cityArray[0];
                    // $rootScope.user.localCity = cityArray[0]; //定位城市
                    $rootScope.user.localCity = city;
                    callback && callback();
                });
            },
            /**计算距离
             * [distance description]
             * @type {[type]}
             */
            distance: getDistance,
            /**取消收藏
             * [removeCollect description]
             * @param  {[type]} zjId [description]
             * @return {[type]}      [description]
             */
            removeCollect: function(zjId, callback) {
                weui.confirm(" ", "取消收藏？", function(r) {
                    if (r == true) {
                        callback();
                    }
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
             * @param  {[type]} element            [description]
             * @param  {[type]} callbackLeft  [description]
             * @param  {[type]} callbackRight [description]
             * @return {[type]}               [description]
             */
            slide: function(element, callbackLeft, callbackRight, callbackClick, prevent) {
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
                    $(element).each(function() {
                        this.addEventListener('touchstart', touchSatrtFunc, false);
                        this.addEventListener('touchmove', touchMoveFunc, false);
                        this.addEventListener('touchend', touchEndFunc, false);
                    })

                }
                bindEvent();
            },
            // 编辑贴纸
            editPic: function(element, type) {
                var startPos, endPos; //起始和结束位置
                var notMove = true; //默认禁止移动
                var circleW = $('.share_del').css('width'); //旋转按钮大小
                var roIcon, origin; //angle角度
                // var angle=0;
                //touchstart事件
                function touchSatrtFunc(index, t) {
                    try {
                        console.log('index', index, editPicArray[index]);
                        if (type == 'delete') {
                            event.stopPropagation();
                            // 删除贴纸
                            $(t).parent().remove();
                            editPicArray.splice(index, 1);
                        } else {
                            event.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等 
                            var touch = event.touches[0]; //获取第一个触点 
                            startPos = {
                                    x: Number(touch.pageX), //页面触点X坐标 
                                    y: Number(touch.pageY) //页面触点Y坐标 
                                }
                                //记录触点初始位置
                            console.log('初始位置', startPos);
                            $(".dec_edit_sec").css({
                                    'z-index': 1
                                })
                                // 设置当前操作元素置上
                            if (type == 'move') {
                                console.log(t);
                                $(t).css({
                                    'z-index': 2,
                                    'border': '3px white solid',
                                    '-webkit-transform': 'rotate(' + editPicArray[index].angle + 'deg) scale(' + editPicArray[index].radio + ')'
                                })

                                $(t).find('.share_rotate').css({
                                    display: 'block'
                                })
                                $(t).find('.share_del').css({
                                    display: 'block'
                                })
                            } else if (type == 'button') {
                                event.stopPropagation();
                                $(t).parent().css({
                                    'z-index': 2,
                                    '-webkit-transform': 'rotate(' + editPicArray[index].angle + 'deg) scale(' + editPicArray[index].radio + ')'
                                });
                                var p = $(t).parent();
                                var lf = parseFloat($(p).css('left')),
                                    tp = parseFloat($(p).css('top')),
                                    w = parseFloat($(p).css('width')),
                                    h = parseFloat($(p).css('height'));
                                console.log(t);
                                origin = {
                                    x: lf + w / 2,
                                    y: tp + h / 2
                                }; //中心点
                                roIcon = {
                                    x: startPos.x - origin.x, //将中心点变为(0,0)
                                    y: origin.y - startPos.y

                                }; //旋转按钮坐标

                                console.log('最初按钮', roIcon);
                                console.log('中心点', origin);
                            }
                        }
                    } catch (e) {
                        alert('touchSatrtFunc：' + e.message);
                    }
                }

                function touchMoveFunc(index, t) {
                    try {
                        event.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等 
                        var touch = event.touches[0]; //获取第一个触点
                        var x = Number(touch.pageX); //页面触点X坐标  
                        var y = Number(touch.pageY); //页面触点Y坐标  
                        endPos = {
                            x: x - startPos.x,
                            y: y - startPos.y
                        };

                        if (type == 'move') {
                            // 第二个触点
                            var touch2, x2, y2;
                            if (event.touches.length > 1) {
                                notMove = true;
                                touch2 = event.touches[1]; //获取第二个触点
                                x2 = Number(touch2.pageX);
                                y2 = Number(touch2.pageY);
                            } else {
                                notMove = false;
                            }
                            // 获取元素位置
                            var lf = parseFloat($(t).css('left')),
                                tp = parseFloat($(t).css('top')),
                                w = parseFloat($(t).css('width')),
                                h = parseFloat($(t).css('height'));
                            if (notMove == true) {
                                // 缩放
                                // 两指距离
                                var fingureDis = Math.sqrt(Math.pow(x2 - x, 2) + Math.pow(y2 - y, 2));
                                // 图片对角距离
                                var picDis = Math.sqrt(Math.pow(w, 2) + Math.pow(h, 2));
                                var r = fingureDis / picDis; //缩放比例
                                editPicArray[index].radio = r;
                                $(t).css({
                                    '-webkit-transform': 'rotate(' + editPicArray[index].angle + 'deg) scale(' + editPicArray[index].radio + ')'
                                });

                            } else {
                                //移动
                                $(t).css({
                                    'left': x - w / 2 + 'px',
                                    'top': y - h / 2 + 'px',
                                    '-webkit-transform': 'rotate(' + editPicArray[index].angle + 'deg) scale(' + editPicArray[index].radio + ')'
                                })
                            }
                        } else if (type == 'button') {
                            event.stopPropagation();
                            notMove = true;
                            var movePoint = {
                                    x: x - origin.x,
                                    y: origin.y - y
                                } //移动点

                            var OA = Math.sqrt(Math.pow(roIcon.x - 0, 2) + Math.pow(roIcon.y - 0, 2));
                            var OP = Math.sqrt(Math.pow(movePoint.x - 0, 2) + Math.pow(movePoint.y - 0, 2));
                            var AP = Math.sqrt(Math.pow(movePoint.x - roIcon.x, 2) + Math.pow(movePoint.y - roIcon.y, 2));
                            var ma = (Math.pow(OA, 2) + Math.pow(OP, 2) - Math.pow(AP, 2)) / (2 * OA * OP);
                            // 判断是顺时针还是逆时针
                            if (movePoint.x > 0) {
                                if (movePoint.y - roIcon.y > 0) {
                                    // 逆时针
                                    editPicArray[index].angle += -Math.acos(ma) * 180 / Math.PI; //旋转角度
                                } else {
                                    // 顺时针
                                    editPicArray[index].angle -= -Math.acos(ma) * 180 / Math.PI;
                                }
                                console.log('移动点', movePoint);
                                console.log(editPicArray[index]);
                            } else {
                                if (movePoint.y - roIcon.y < 0) {
                                    // 逆时针
                                    editPicArray[index].angle += -Math.acos(ma) * 180 / Math.PI; //旋转角度
                                } else {
                                    // 顺时针
                                    editPicArray[index].angle -= -Math.acos(ma) * 180 / Math.PI;
                                }
                                console.log('第三、四', editPicArray);
                            }
                            // 缩放
                            // 距中心距离,前一次OA长，现在OP长
                            var scalePercent = OP / OA; //缩放比例
                            editPicArray[index].radio *= scalePercent;
                            $(t).parent().css({
                                '-webkit-transform': 'rotate(' + editPicArray[index].angle + 'deg) scale(' + editPicArray[index].radio + ')'
                            });
                            roIcon = {
                                    x: movePoint.x,
                                    y: movePoint.y
                                } //按钮变化位置，也是前一次移动位置

                        }
                        // 按钮大小保持不变
                        $('.share_del').css({
                            width: circleW,
                            height: circleW,
                            '-webkit-transform': 'scale(' + 1 / editPicArray[index].radio + ')'
                        })
                        $('.share_rotate').css({
                            width: circleW,
                            height: circleW,
                            '-webkit-transform': 'scale(' + 1 / editPicArray[index].radio + ')'
                        })
                    } catch (e) {
                        console.log('touchMoveFunc：' + e.message);
                    }
                }

                //touchend事件  
                function touchEndFunc(event) {
                    try {
                        event.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等 
                        notMove = true;
                    } catch (e) {
                        console.log('touchEndFunc：' + e.message);
                    }
                }
                //绑定事件
                function bindEvent() {
                    $(element).each(function(index) {
                        this.addEventListener('touchstart', function() {
                            touchSatrtFunc(index, this);
                        }, false);
                        this.addEventListener('touchmove', function() {
                            touchMoveFunc(index, this);
                        }, false);
                        this.addEventListener('touchend', touchEndFunc, false);
                    })
                }
                bindEvent();
            },
            /**获取数据
             * [getSchool description]
             * @param  {[type]}   city     [description]城市名
             * @param  {Function} callback [description]
             * @return {[type]}            [description]
             */
            getSchool: function(callback) {
                dataService.putUserInfo();
                var serObj = {
                    city: $rootScope.user.city
                };
                // userService.getSchool(serObj, function(response) {
                //     if (response.Status == 1 && response.Data) {
                //         $rootScope.zjCount = response.Data.Count;
                //         if (response.Data.ArchivesInfoS) {
                //             //显示城市为返回数据城市
                //             if (response.Data.ArchivesInfoS[0].District == null) {
                //                 $rootScope.user.city = response.Data.ArchivesInfoS[0].Province;
                //             } else {
                //                 $rootScope.user.city = response.Data.ArchivesInfoS[0].City;
                //             }
                //             if (serObj.city != $rootScope.user.city) {
                //                 //若未找到该城市
                //                 $rootScope.user.userLatitude = 39.904514;
                //                 $rootScope.user.userLongitude = 116.407248;
                //             }
                //             $rootScope.zj = response.Data.ArchivesInfoS;
                //             if (callback) {
                //                 callback();
                //             }
                //         }
                //     }
                // })
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
            //    使用canvas对大图片进行压缩
            compressImg: function(img) {
                //    用于压缩图片的canvas
                var canvas = document.createElement("canvas");
                var ctx = canvas.getContext('2d');
                //    瓦片canvas
                var tCanvas = document.createElement("canvas");
                var tctx = tCanvas.getContext("2d");
                var canvasSupported = isCanvasSupported(); //判断浏览器是否支持canvas
                function isCanvasSupported() {
                    var elem = document.createElement('canvas');
                    return !!(elem.getContext && elem.getContext('2d'));
                }
                var initSize = img.src.length;
                var width = img.width;

                var height = img.height;
                //如果图片大于四百万像素，计算压缩比并将大小压至400万以下
                var ratio;
                if ((ratio = width * height / 4000000) > 1) {
                    ratio = Math.sqrt(ratio);
                    width /= ratio;
                    height /= ratio;
                } else {
                    ratio = 1;
                }
                canvas.width = width;
                canvas.height = height;
                //        铺底色
                ctx.fillStyle = "#fff";
                ctx.fillRect(0, 0, canvas.width, canvas.height);
                //如果图片像素大于100万则使用瓦片绘制
                var count;
                if ((count = width * height / 1000000) > 1) {
                    count = ~~(Math.sqrt(count) + 1); //计算要分成多少块瓦片
                    //            计算每块瓦片的宽和高
                    var nw = ~~(width / count);
                    var nh = ~~(height / count);
                    tCanvas.width = nw;
                    tCanvas.height = nh;
                    for (var i = 0; i < count; i++) {
                        for (var j = 0; j < count; j++) {
                            tctx.drawImage(img, i * nw * ratio, j * nh * ratio, nw * ratio, nh * ratio, 0, 0, nw, nh);
                            ctx.drawImage(tCanvas, i * nw, j * nh, nw, nh);
                        }
                    }
                } else {
                    ctx.drawImage(img, 0, 0, width, height);
                }
                //进行最小压缩
                var ndata = canvas.toDataURL('image/jpeg', 0.3);
                console.log('压缩前：' + initSize);
                console.log('压缩后：', ndata.length);
                console.log('压缩率：' + ~~(100 * (initSize - ndata.length) / initSize) + "%");
                tCanvas.width = tCanvas.height = canvas.width = canvas.height = 0;
                return ndata;
            },
            // base64转二进制对象
            convertImgDataToBlob: function(base64Data) {
                var format = "image/jpeg";
                var base64 = base64Data;
                var code = window.atob(base64.split(",")[1]);
                var aBuffer = new window.ArrayBuffer(code.length);
                var uBuffer = new window.Uint8Array(aBuffer);
                for (var i = 0; i < code.length; i++) {
                    uBuffer[i] = code.charCodeAt(i) & 0xff;
                }
                console.info([aBuffer]);
                console.info(uBuffer);
                console.info(uBuffer.buffer);
                console.info(uBuffer.buffer == aBuffer); //true

                var blob = null;
                try {
                    blob = new Blob([uBuffer], {
                        type: format
                    });
                } catch (e) {
                    window.BlobBuilder = window.BlobBuilder ||
                        window.WebKitBlobBuilder ||
                        window.MozBlobBuilder ||
                        window.MSBlobBuilder;
                    if (e.name == 'TypeError' && window.BlobBuilder) {
                        var bb = new window.BlobBuilder();
                        bb.append(uBuffer.buffer);
                        blob = bb.getBlob("image/jpeg");

                    } else if (e.name == "InvalidStateError") {
                        blob = new Blob([aBuffer], {
                            type: format
                        });
                    } else {

                    }
                }
                console.log(blob.size);
                return blob;
            },

            /**
             * [weiPay description]
             * 封装微信支付接口
             * @author  wei.liu@ritetrek.com
             * @DateTime 2016-07-13T15:01:22+0800
             * @param    {[type]}                 timestamp [description]
             * @param    {[type]}                 nonceStr  [description]
             * @param    {[type]}                 package   [description]
             * @param    {[type]}                 signType  [description]
             * @param    {[type]}                 paySign   [description]
             * @param    {Function}               callback  [description]
             * @return   {[type]}                           [description]
             */
            weiPay: function(timestamp, nonceStr, package, signType, paySign, callback) {
                var array = package.split('=');
                var prepayId = array[1];

                function onBridgeReady() {
                    WeixinJSBridge.invoke(
                        'getBrandWCPayRequest', {
                            "appId": ENV.appid, //公众号名称，由商户传入     
                            "timeStamp": timestamp, //时间戳，自1970年以来的秒数     
                            "nonceStr": nonceStr, //随机串     
                            "package": package,
                            "signType": signType, //微信签名方式：     
                            "paySign": paySign //微信签名 
                        },
                        function(res) {
                            // alert(res.err_msg);
                            if (res.err_msg.indexOf("ok") > -1) {
                                // alert("支付成功");
                                $state.go("nowClass");
                                userService.payCallback(prepayId, function(response) {
                                    if (response.Status == 1) {

                                    }

                                })

                            } // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
                            if (res.err_msg.indexOf("cancel") > -1) {
                                callback && callback();
                            } // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 
                            if (res.err_msg.indexOf("fail") > -1) {
                                callback && callback();
                            } // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。 

                        }
                    );
                }

                if (typeof WeixinJSBridge == "undefined") {
                    if (document.addEventListener) {
                        document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
                    } else if (document.attachEvent) {
                        document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
                        document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
                    }
                } else {
                    onBridgeReady();
                }
            },
            // 选择的订单--进行取消订单等操作
            selectOrder: {},
            // 选择用于支付的数据--确认订单页
            payBeforeOrder: [],
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


                // window.openId = "oJmyisyA6KeDOeRCcdd2NYRrCllg";
                // window.openId = "oJmyis42QXb1ZB60iCIHbAsONq2Q";
                // window.openId="oJmyis9qVWQWI8uygAq3ei2Y5dvg";
                // $cookieStore.put("openId", window.openId);
                // if (callback) {
                //     callback();
                // }


            },
            shareTo: function(url, img) {
                wx.onMenuShareAppMessage({
                    title: '菲比未来星', // 分享标题
                    desc: '菲比陪伴宝贝在快乐中成长，我迈出了第一步，你准备好了吗？', // 分享描述
                    link: url, // 分享链接
                    imgUrl: img, // 分享图标
                    type: '', // 分享类型,music、video或link，不填默认为link
                    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                    success: function() {
                        // 用户确认分享后执行的回调函数
                        $("#choiceShare").css({
                            'display': 'none'
                        });
                        $(".modal-backdrop").css({
                            'display': 'none'
                        });
                    },
                    cancel: function() {
                        // 用户取消分享后执行的回调函数
                        $("#choiceShare").css({
                            'display': 'none'
                        });
                        $(".modal-backdrop").css({
                            'display': 'none'
                        });


                    }
                });
                wx.onMenuShareTimeline({
                    title: '菲比未来星', // 分享标题
                    desc: '菲比陪伴宝贝在快乐中成长，我迈出了第一步，你准备好了吗？', // 分享描述
                    link: url, // 分享链接
                    imgUrl: img, // 分享图标
                    type: '', // 分享类型,music、video或link，不填默认为link
                    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                    success: function() {
                        // 用户确认分享后执行的回调函数
                        $("#choiceShare").css({
                            'display': 'none'
                        });
                        $(".modal-backdrop").css({
                            'display': 'none'
                        });

                    },
                    cancel: function() {
                        // 用户取消分享后执行的回调函数
                        $("#choiceShare").css({
                            'display': 'none'
                        });
                        $(".modal-backdrop").css({
                            'display': 'none'
                        });
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
    }])
    // .filter("computedRange", ["$rootScope", function($rootScope) {
    //     return function(text, prame1, prame2) {
    //         return "";
    //     }
    // }])
