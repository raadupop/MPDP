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
        service.updateGoal = updateGoal;

        return service;
        ///

        function getGoals(userId, startDate, endDate, success, failed){
            var config = {
                params: {
                    userId: userId,
                    startDate: startDate,
                    endDate: endDate
                }
            };

            ApiWebService.get(ApiConfig + 'goal/getgoals', config, success, failed);

        }

        function updateGoal(goal, success, failed){
            ApiWebService.put(ApiConfig + 'goal/update', goal, success, failed)
        }
    }

})();
