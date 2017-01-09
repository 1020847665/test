angular.module('tuanxiao.controller')

//培训班
.controller('banListCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', 'baseService', function(ENV, $rootScope, $scope, $cookieStore, $state, baseService) {
        // 设置nav
        $rootScope.headerActive = {
            active: true,
            number: 1
        };
        $rootScope.footerActive = {
            active: true,
            number: 1
        };
        // 经纬度转换
        $scope.getAddress = function() {
            baseService.getAddress(39.98174, 116.30631);
            baseService.showMap("map", 39.98174, 116.30631);
        };

        // 预览pdf
        $scope.showPdf = function() {
            // var myPDF = new PDFObject({ url: "http://demo.lanrenzhijia.com/2014/pdf1023/sample.pdf" }).embed();
            // PDFObject.embed("http://demo.lanrenzhijia.com/2014/pdf1023/sample.pdf", "#example1");

        }

    }])
    .controller('banDetailCtrl', ['ENV', '$rootScope', '$scope', '$cookieStore', '$state', function(ENV, $rootScope, $scope, $cookieStore, $state) {

        // 设置nav
        $rootScope.headerActive = {
            active: false,
            number: 1
        };
        $rootScope.footerActive = {
            active: false,
            number: 1
        };
    }]);
