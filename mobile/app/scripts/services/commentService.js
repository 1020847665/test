angular.module('tuanxiao.services')
    .factory('comService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getComList: {
                method: 'POST',
                url: ENV.api + "WeChat/GetCommentPageList"
            },
            getMyCom: {
                //获取我在某个班/课程的评论
                method: 'POST',
                url: ENV.api + "WeChat/GetStudentComment"
            },
            addCom: {
                method: 'POST',
                url: ENV.api + "WeChat/AddComment"
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
            },
            /**获取我的评论
             * [getMyCom description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-13
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getMyCom: function(obj, callback) {
                return resource.getMyCom(null, JSON.stringify({
                        PageIndex: obj.PageIndex,
                        PageSize: ENV.pageSize,
                        IsPage: true,
                        Condition: obj.Condition
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            /**添加评论
             * [addCom description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-13
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             */
            addCom: function(obj, callback) {
                return resource.addCom(null, JSON.stringify({
                    TargetId: obj.TargetId,
                    Type: obj.Type,
                    Grade: obj.Grade,
                    Body: obj.Body
                }), function(response) {
                    callback && callback(response);
                });
            }
        };
    }]);
