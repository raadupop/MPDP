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
        service.addObjective = addObjective;
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

        function addObjective(objective, success, failed){
            estimationTimeSpanWrapper(objective)
            ApiWebService.post(ApiConfig + 'goal/createobjective', objective, success, failed);
        }


        //todo make own service for this
        function estimationTimeSpanWrapper(objective){

            var mrx = new RegExp(/([0-9][0-9]?)[ ]?m/);
            var hrx = new RegExp(/([0-9][0-9]?)[ ]?h/);
            var drx = new RegExp(/([0-9])[ ]?d/);

            var days = 0;
            var hours = 0;
            var minutes = 0;

            if (mrx.test(objective.estimation)) {
                minutes = mrx.exec(objective.estimation)[1];
            }
            if (hrx.test(objective.estimation)) {
                hours = hrx.exec(objective.estimation)[1];
            }
            if (drx.test(objective.estimation)) {
                days = drx.exec(objective.estimation)[1];
            }

            objective.estimation = moment.duration(days + '.'  + hours + ':' + '' + minutes);

            return objective;
        }
    }

})();
