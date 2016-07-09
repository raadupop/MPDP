/**
 * Created by Radu  Pop on 6/22/2016.
 */
(function (){
    'use strict';

    angular
        .module('app.examples.dashboards')
        .controller('RemoveGoalController', RemoveGoalController);

    /* @ngInject */
    function RemoveGoalController($scope, $window, $mdDialog, goal, GoalsService, $mdToast){

        var vm = this;
        vm.cancelClick = cancelClick;
        vm.goal = goal;
        vm.removeGoal = removeGoal;

        ////////////////


        function cancelClick() {
            $mdDialog.cancel();
        }

        function removeGoal(){
            GoalsService.deleteGoal(goal, handleSuccess, handleFailed)
        }

        function handleSuccess(){
            $mdDialog.hide();
        }

        //todo provide a non-generic error
        function handleFailed(){
            $mdToast.show(
                $mdToast.simple()
                    .content('Ops something goes wrong, try again')
                    .position('bottom right')
                    .hideDelay(5000)
            );
        }

    }

})();
