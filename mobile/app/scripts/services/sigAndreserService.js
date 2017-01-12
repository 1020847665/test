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
                        Position: obj.Position,
                        Organization: obj.Organization,
                        MobileNumber: obj.MobileNumber,
                        Email: obj.Email
                    }),
                    function(response) {
                        callback && callback(response);
                    });
            },
            /**
             * [reserve description]
             * @Author   'yuxiaoting@bestwise.cc'
             * @DateTime 2017-01-12
             * @param    {[type]}                 obj      [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            reserve: function(obj, callback) {
                return resource.reserve(null, JSON.stringify({
                    TargetId: obj.TargetId,
                    StartTime: obj.StartTime,
                    EndTime: obj.EndTime,
                    TrainOrganization: obj.TrainOrganization,
                    TrainAddress: obj.TrainAddress,
                    TrainNumber: obj.TrainNumber,
                    TrainNeeds: obj.TrainNeeds,
                    Type:obj.Type
                }), function(response) {
                    callback && callback(response);
                });
            }
        };
    }]);
