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
        vm.okClick = okClick;
        vm.goal = goal;
        vm.removeGoal = removeGoal;

        ////////////////

        function okClick() {
            $mdDialog.hide();
        }

        function cancelClick() {
            $mdDialog.cancel();
        }

        function removeGoal(){
            GoalsService.deleteGoal(goal, handleSuccess, handleFailed)
        }

        function handleSuccess(){
            $mdToast.show(
                $mdToast.simple()
                    .content('Goal was successfully updated')
                    .position('bottom right')
                    .hideDelay(5000)
            );
        }

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
