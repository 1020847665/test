angular.module('tuanxiao.services')
    .factory('courseService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getCourseList: {
                method: 'POST',
                url: ENV.api + "WeChat/GetCoursePageList"
            },
            getCourseDetail: {
                method: 'GET',
                url: ENV.api + "WeChat/GetCourseDetail"
            }
        });
        return {
            /**团校课程列表
             * [getCourseList description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-11
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getCourseList: function(obj, callback) {
                return resource.getCourseList(null, JSON.stringify({
                        PageIndex: obj.PageIndex,
                        PageSize: ENV.pageSize,
                        IsPage: true,
                        Condition: obj.Condition
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            /**课程详情
             * [getCourseDetail description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-11
             * @param    {[type]}                 courseId [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getCourseDetail: function(courseId, callback) {
                return resource.getCourseDetail({
                    courseId: courseId
                }, null, function(response) {
                    callback && callback(response);
                });
            }
        };
    }]);
