/**
 * Created by Radu  Pop on 5/29/2016.
 */
(function(){
    'use strict';

    angular
        .module('app.examples.dashboards')
        .controller('DashboardGeneralController', DashboardGeneralController);


    /* @ngInject */
    function DashboardGeneralController($scope, $rootScope, $mdToast, GoalsService){
        var vm = this;

        vm.dateRange = {
            start: moment().startOf('month'),
            end: moment().endOf('month')
        };

        vm.query = {
            goals: 'date',
            limit: 5,
            page: 1
        };

        vm.goalsResult = {
            totalGoals: 0,
            goals: []
        };

        function getGoals(){
            GoalsService.getGoals($rootScope.globals.currentUser.userId, handleSuccess, handleFailed)
        }

        function handleSuccess(result){
            vm.goalsResult.goals = result.data.goals;
            vm.goalsResult.totalGoals = result.data.goalsCount;
        }

        function handleFailed(){
            $mdToast.show(
                $mdToast.simple()
                    .content("Ops something gos wrong with api")
            )
        }


        /// init
        getGoals();

    }

})();
