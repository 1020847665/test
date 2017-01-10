angular.module('tuanxiao.services')
    .factory('teacherService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getTeacherList: {
                method: 'POST',
                url: ENV.api + "WeChat/GetTeacherPageList"
            },
            getTeacherDetail: {
                method: 'GET',
                url: ENV.api + "WeChat/GetTeacherDetail"
            }
        });
        return {
            /**教师列表
             * [getTeacherList description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-10
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getTeacherList: function(obj, callback) {
                return resource.getTeacherList(null, JSON.stringify({
                        PageIndex: obj.PageIndex,
                        PageSize: ENV.pageSize,
                        IsPage: true,
                        Condition: obj.Condition
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            /**
             * [getTeacherDetail description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-10
             * @param    {[type]}                 teacherId [description]
             * @param    {Function}               callback  [description]
             * @return   {[type]}                           [description]
             */
            getTeacherDetail: function(teacherId, callback) {
                return resource.getTeacherDetail({
                    teacherId: teacherId
                }, null, function(response) {
                    callback && callback(response);
                });
            }
        };
    }]);
