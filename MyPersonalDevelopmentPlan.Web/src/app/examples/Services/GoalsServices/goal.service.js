/**
 * Created by Radu  Pop on 5/29/2016.
 */
(function () {
    'use strict';

    angular
        .module('app.examples.dashboards')
        .factory('GoalsService', GoalsService);

    /* @ngInject */
    function GoalsService(ApiWebService, ApiConfig){
        var service = {};

        service.getGoals = getGoals;

        return service;
        ///

        function getGoals(userId, success, failed){
            var config = {
                params: {
                    userId: userId
                }
            };

            ApiWebService.get(ApiConfig + 'goal/getgoals', config, success, failed);

        }
    }

})();
