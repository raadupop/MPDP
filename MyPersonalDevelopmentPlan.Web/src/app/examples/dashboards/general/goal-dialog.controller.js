(function() {
    'use strict';

    angular
        .module('app.examples.dashboards')
        .controller('GoalDialogController', GoalDialogController);

    /* @ngInject */
    function GoalDialogController($scope, $window, $mdDialog, goal, GoalsService, $mdToast) {
        var vm = this;
        vm.cancelClick = cancelClick;
        vm.okClick = okClick;
        vm.goal = goal;
        vm.printClick = printClick;
        vm.updateClick = updateClick;


        ////////////////

        function okClick() {
            $mdDialog.hide();
        }

        function cancelClick() {
            $mdDialog.cancel();
        }

        function printClick() {
            $window.print();
        }

        function updateClick(){
            GoalsService.updateGoal(vm.goal, handleSuccess, handleFailed);
        }

        function handleSuccess(result){
            vm.goal = result.data;
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
