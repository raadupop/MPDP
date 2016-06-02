/**
 * Created by Radu  Pop on 5/25/2016.
 */
(function(){
    'use strict';

    angular
        .module('app.examples.forms')
        .controller('GoalRegisterController', GoalRegisterController);

    /* @ngInject */
    function GoalRegisterController(triSettings, ApiWebService, ApiConfig, $rootScope, $mdToast, moment) {
        var vm = this;
        vm.registerClick = registerClick;
        vm.triSettings = triSettings;

        // todo validation !!
        vm.goal = {
            name: '',
            description: '',
            estimation: ''
        };

        function registerClick(){

            estimationTimeSpanWrapper(vm.goal.estimation);

            var goalToSend = {
                username: $rootScope.globals.currentUser.username,
                userProfileId: $rootScope.globals.currentUser.userId,
                name: vm.goal.name,
                description: vm.goal.description,
                estimation: estimationTimeSpanWrapper(vm.goal.estimation)
            };

            ApiWebService.post(ApiConfig + 'goal/creategoal', goalToSend, goalSucceded, goalFailed)
        }

        //todo make own service for this
        function estimationTimeSpanWrapper(estimation){

            var mrx = new RegExp(/([0-9][0-9]?)[ ]?m/);
            var hrx = new RegExp(/([0-9][0-9]?)[ ]?h/);
            var drx = new RegExp(/([0-9])[ ]?d/);

            var days = 0;
            var hours = 0;
            var minutes = 0;

            if (mrx.test(estimation)) {
                minutes = mrx.exec(estimation)[1];
            }
            if (hrx.test(estimation)) {
                hours = hrx.exec(estimation)[1];
            }
            if (drx.test(estimation)) {
                days = drx.exec(estimation)[1];
            }

            return moment.duration(days + '.'  + hours + ':' + '' + minutes);
        }


        function goalSucceded(){
            $mdToast.show(
                $mdToast.simple()
                    .content('Goal was successfully added')
                    .position('bottom right')
                    .hideDelay(5000)
            );
        }

        function goalFailed() {
            $mdToast.show(
                $mdToast.simple()
                    .content("Something goes wrong")
                    .position('bottom right')
                    .hideDelay(5000)
            );
        }

    }

})();
