angular.module('tuanxiao.services')
    .factory('banService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getBanList: {
                method: 'POST',
                url: ENV.api + "WeChat/GetTrainClassPageList"
            },
            getBanDetail: {
                method: 'GET',
                url: ENV.api + "WeChat/GetTrainClassDetail"
            }
        });
        return {
            /**培训班列表
             * [getBanList description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-11
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getBanList: function(obj, callback) {
                return resource.getBanList(null, JSON.stringify({
                        PageIndex: obj.PageIndex,
                        PageSize: ENV.pageSize,
                        IsPage: true,
                        Condition: obj.Condition
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            /**培训班详情
             * [getBanDetail description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-11
             * @param    {[type]}                 banId    [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getBanDetail: function(banId, callback) {
                return resource.getBanDetail({
                    trainId: banId
                }, null, function(response) {
                    callback && callback(response);
                });
            }
        };
    }]);
