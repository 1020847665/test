angular.module('tuanxiao.services')
    .factory('sigAndreserService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            signUp: {
                method: 'POST',
                url: ENV.api + "WeChat/StudentSignedUp"
            },
            reserve: {
                method: 'POST',
                url: ENV.api + "WeChat/StudentBook"
            }
        });
        return {
            /**报名
             * [signUp description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-11
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            signUp: function(obj, callback) {
                return resource.signUp(null, JSON.stringify({
                        Name: obj.Name,
                        TrainId: obj.TrainId,
                        Sex: obj.Sex,
                        Position: obj.Condition,
                        Organization: obj.Organization,
                        MobileNumber: obj.MobileNumber,
                        Email: obj.Email
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            /**预约
             * [reserve description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-11
             * @param    {[type]}                 teacherId [description]
             * @param    {Function}               callback  [description]
             * @return   {[type]}                           [description]
             */
            reserve: function(teacherId, callback) {
                return resource.reserve(null, JSON.stringify({
                    TargetId: TargetId,
                    StartTime: StartTime,
                    EndTime: EndTime,
                    TrainOrganization: TrainOrganization,
                    TrainAddress: TrainAddress,
                    TrainNumber: TrainNumber,
                    TrainNeeds: TrainNeeds,
                    Type: Type
                }), function(response) {
                    callback && callback(response);
                });
            }
        };
    }]);
