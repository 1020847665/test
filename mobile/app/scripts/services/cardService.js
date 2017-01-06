angular.module('tuanxiao.services')
    .factory('cardService', ['$resource', 'ENV', function($resource, ENV) {
        var resource = $resource(ENV.api, {}, {
            getOrgCardList: {
                method: 'GET', //获取机构课卡列表
                url: ENV.api + "CardsList/GetOrgList"
            },
            getMyCardList: {
                method: 'GET', //获取用户的已购课卡
                url: ENV.api + "CardsList/GetMyList"
            }
        });
        return {
            /**
             * [getOrgCardList description]
             *
             * //获取机构的课卡列表
             * @author  wei.liu@ritetrek.com
             * @DateTime 2016-07-25T13:56:36+0800
             * @param    {[type]}                 orgId    [description]
             * @param    {Function}               callback [description]
             * @return   {[type]}                          [description]
             */
            getOrgCardList: function(orgId, callback) {
                return resource.getOrgCardList({
                        orgId: orgId
                    }, null,
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
            getMyCardList: function(openId, cardsNumber, callback) {
                return resource.getMyCardList({
                    openId: openId,
                    cardsNumber: cardsNumber
                }, null, function(response) {
                    callback && callback(response);
                });
            }
        }
    }]);
