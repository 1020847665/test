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
                };
            }
        };
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
                    if (scope.bgType == 'ban') {
                        scope.pic = 'imgs/course_default.png';
                    } else if (scope.bgType == 'teacher') {
                        scope.pic = 'imgs/teacher_default.png';

                    }
                }
            }
        };
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
                    angular.element(document).find('.search-header').css({
                        "postion": 'absolute'
                    });
                });
                element.bind('focusout', function() {
                    angular.element(document).find('.search-header').css({
                        "postion": 'fixed'
                    });
                });
            }
        };
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
    // 滚动加载
    .directive('scrollLoad', function() {
        return {
            restrict: 'A',
            scope: {
                range: '@',
                getdata: '='
            },
            link: function(scope, element, attr) {
                window.onscroll = function() {
                    if (!scope.range) scope.range = 50;
                    var offsetHeight = element[0].offsetHeight;
                    var h = element[0].offsetTop + element[0].clientHeight + parseFloat(scope.range);

                    if (h < offsetHeight) {
                        return;
                    } else {
                        scope.getdata();
                    }
                };
            }
        };
    });
