(function() {
    'use strict';

    angular
        .module('app.examples.dashboards')
        .controller('ObjectiveViewController', GoalDialogController);

    /* @ngInject */
    function GoalDialogController($scope, $window, $mdDialog, objective, GoalsService, $mdToast) {
        var vm = this;
        vm.cancelClick = cancelClick;
        vm.okClick = okClick;
        vm.objectiveDialog = objective;
        vm.workedLog = {
            objectiveId: objective.Id
        };
        vm.printClick = printClick;
        vm.updateClick = updateClick;
        vm.saveLogClick = saveLogClick;

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

        function saveLogClick(){
            GoalsService.saveWorkedLog(vm.workedLog, handleSuccess, handleFailed)
        }

        function updateClick(){
            GoalsService.updateObjective(vm.objectiveDialog, handleSuccess, handleFailed);
        }

        function handleSuccess(result){
            $mdToast.show(
                $mdToast.simple()
                    .content('The action was finished with Success')
                    .position('bottom right')
                    .hideDelay(5000)
            );
            $mdDialog.hide(result.data);
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