/**
 * Created by Radu  Pop on 5/25/2016.
 */
(function(){
    'use strict';

    angular
        .module('app.examples.forms')
        .controller('GoalRegisterController', GoalRegisterController);

    /* @ngInject */
    function GoalRegisterController(triSettings, ApiWebService, ApiConfig, $rootScope, $mdToast) {
        var vm = this;
        vm.registerClick = registerClick;
        vm.triSettings = triSettings;

        // todo validation !!
        vm.goal = {
            user: $rootScope.globals.currentUser.username,
            userId: $rootScope.globals.currentUser.userId,
            name: '',
            description: ''
        };

        function registerClick(){
            ApiWebService.post(ApiConfig + 'goal/creategoal', vm.goal, goalSucceded, goalFailed)
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
