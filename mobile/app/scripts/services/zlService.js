angular.module('tuanxiao.services')
    .factory('zlService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getZlList: {
                method: 'POST',
                url: ENV.api + "WeChat/GetMaterialPageList"
            },
            getZlDetail: {
                method: 'GET',
                url: ENV.api + "WeChat/GetMaterialDetail"
            }
        });
        return {
            /**
             * [getZlList description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-09
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getZlList: function(obj, callback) {
                return resource.getZlList(null, JSON.stringify({
                        PageIndex: obj.PageIndex,
                        PageSize: obj.PageSize,
                        IsPage: true,
                        Condition: obj.Condition,
                        Sort: {
                            "Cdt": "desc"
                        }
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            /**
             * [getMyCardList description]
             *
             * //查询我的课卡列表
             * @author  wei.liu@ritetrek.com
             * @DateTime 2016-07-25T13:56:09+0800
             * @param    {[type]}                 openId      [description]
             * @param    {[type]}                 cardsNumber [description]
             * @param    {Function}               callback    [description]
             * @return   {[type]}                             [description]
             */
            getZlDetail: function(zlId, callback) {
                return resource.getZlDetail({
                    materialId: zlId
                }, null, function(response) {
                    callback && callback(response);
                });
            }
        };
    }]);
