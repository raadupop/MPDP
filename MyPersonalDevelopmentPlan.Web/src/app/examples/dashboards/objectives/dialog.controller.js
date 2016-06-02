(function() {
    'use strict';

    angular
        .module('app.examples.dashboards')
        .controller('ObjectiveDialogController', DialogController);

    /* @ngInject */
    function DialogController($state, $mdDialog) {
        var vm = this;
        vm.cancel = cancel;
        vm.hide = hide;
        vm.objective = {
            goalId: null,
            title: '',
            description: '',
            estimation: '',
            ObjectiveRank: ''
        };

        /////////////////////////

        function hide() {
            $mdDialog.hide(vm.objective);
        }

        function cancel() {
            $mdDialog.cancel();
        }


    }
})();
