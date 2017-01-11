angular.module('tuanxiao.services')
    .factory('comService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getComList: {
                method: 'POST',
                url: ENV.api + "WeChat/GetCommentPageList"
            }
        });
        return {
            /**评论列表
             * [getComList description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-11
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getComList: function(obj, callback) {
                return resource.getComList(null, JSON.stringify({
                        PageIndex: obj.PageIndex,
                        PageSize: ENV.pageSize,
                        IsPage: true,
                        Condition: obj.Condition
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            }
        };
    }]);
