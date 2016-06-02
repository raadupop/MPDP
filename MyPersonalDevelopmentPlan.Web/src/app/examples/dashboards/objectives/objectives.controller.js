(function() {
    'use strict';

    angular
        .module('app.examples.dashboards')
        .controller('DashboardObjectivesController', ObjectivesController);

    /* @ngInject */
    function ObjectivesController($rootScope, $mdToast, GoalsService, $scope, $mdDialog) {
        var vm = this;
        vm.objectives = [];
        vm.goalsResult = {
            goals: [],
            goalsCount: null
        };

        vm.query = {
            goals: 'date',
            limit: 5,
            page: 1
        };


        vm.getGoals = getGoals;
        vm.goalStatus = ['open', 'inProgress', 'done'];
        vm.goalSelected = null;
        // create filterable data structure for icons

        vm.selectGoal = function(goal) {
            vm.goalSelected = goal;
        };

        $scope.$on('addObjective', function( ev ){
            if(vm.goalSelected != null){
                $mdDialog.show({
                    templateUrl: 'app/examples/dashboards/objectives/add-objective-dialog.tmpl.html',
                    targetEvent: ev,
                    controller: 'ObjectiveDialogController',
                    controllerAs: 'vm'
                })
                .then(function(objective) {
                        objective.goalId = vm.goalSelected.Id;
                        GoalsService.addObjective(objective, handleObjectiveSuccess, handleFailed);
                });
            }
            else{
                $mdToast.show(
                    $mdToast.simple()
                        .content("Please select a goal first.")
                        .position('bottom right')
                        .hideDelay(2000)
                );
            }
        });


        function getGoals(){
            GoalsService.getGoals($rootScope.globals.currentUser.userId, moment().startOf('year').toISOString(), moment().endOf('year').toISOString(), handleGoalSuccess, handleFailed)
        }

        function handleGoalSuccess(result){
            vm.goalsResult.goals = result.data.goals;
            vm.goalsResult.goalsCount = result.data.goalsCount;
        }

        function handleObjectiveSuccess(result){
            //for(var i=0; i < vm.goalsResult.goals.length; i++){
            //    if(vm.goalsResult.goals[i] === vm.goalSelected){
            //        vm.goalsResult.goals[i].objectives.push(result.data);
            //    }
            //}
            vm.goalSelected.Objectives.push(result.data);
        }

        function handleFailed(){
            vm.goals = null;
        }
        //init
        getGoals();

    }
})();
