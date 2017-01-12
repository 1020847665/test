angular.module("tuanxiao.directive")
    .directive('weToast', function() {
        return {
            scope: {
                msg: '=',
                callback: '='
            },
            template: '<div id="toast"><div class="weui_mask_transparent"></div><div class="weui_toast"><i class="weui_icon_toast"></i><p class="weui_toast_content">{{msg}}</p></div></div>',
            link: function(scope, element, attrs) {
                if (!scope.msg) {
                    scope.msg = "提交成功";
                }
                setTimeout(function() {
                    element.css({
                        'display': 'none'
                    });
                    scope.callback && scope.callback();
                }, 600);
            }
        };
    });
