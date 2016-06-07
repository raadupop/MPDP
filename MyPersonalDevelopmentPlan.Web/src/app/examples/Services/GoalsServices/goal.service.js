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
        service.updateObjective = updateObjective;
        service.addObjective = addObjective;
        service.saveWorkedLog = saveWorkedLog;
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
            ApiWebService.put(ApiConfig + 'goal/update', goal, success, failed);
        }

        function updateObjective(objective, success, failed){
            ApiWebService.put(ApiConfig + 'goal/updateobjective', objective, success, failed);
        }

        function addObjective(objective, success, failed){
            objective.estimation = estimationTimeSpanWrapper(objective.estimation);
            ApiWebService.post(ApiConfig + 'goal/createobjective', objective, success, failed);
        }

        function saveWorkedLog(woorkedLod, success, failed){
            woorkedLod.Duration = estimationTimeSpanWrapper(woorkedLod.Duration);
            ApiWebService.post(ApiConfig + 'goal/addworkedlog', woorkedLod, success, failed);
        }


        //todo make own service for this
        function estimationTimeSpanWrapper(duration){

            var mrx = new RegExp(/([0-9][0-9]?)[ ]?m/);
            var hrx = new RegExp(/([0-9][0-9]?)[ ]?h/);
            var drx = new RegExp(/([0-9][0-9]?)[ ]?d/);

            var days = 0;
            var hours = 0;
            var minutes = 0;

            if (mrx.test(duration)) {
                minutes = mrx.exec(duration)[1];
            }
            if (hrx.test(duration)) {
                hours = hrx.exec(duration)[1];
            }
            if (drx.test(duration)) {
                days = drx.exec(duration)[1];
            }

            duration = moment.duration(days + '.'  + hours + ':' + '' + minutes);

            return duration;
        }
    }

})();
