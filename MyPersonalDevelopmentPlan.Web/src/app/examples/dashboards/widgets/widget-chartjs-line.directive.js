(function() {
    'use strict';

    angular
        .module('app.examples.dashboards')
        .directive('chartjsLineWidget', chartjsLineWidget);

    /* @ngInject */
    function chartjsLineWidget($timeout, $interval, ApiWebService, ApiConfig, $rootScope) {

        var directive = {
            require: 'triWidget',
            link: link,
            restrict: 'A'
        };
        return directive;

        function link($scope, $element, attrs, widgetCtrl) {
            widgetCtrl.setLoading(true);

            $timeout(function() {
                widgetCtrl.setLoading(false);
                randomData();
            }, 1500);

            widgetCtrl.setMenu({
                icon: 'zmdi zmdi-more-vert',
                items: [{
                    icon: 'zmdi zmdi-refresh',
                    title: 'DASHBOARDS.WIDGETS.MENU.REFRESH',
                    click: function() {
                        $interval.cancel($scope.intervalPromise);
                        widgetCtrl.setLoading(true);
                        $timeout(function() {
                            widgetCtrl.setLoading(false);
                            randomData();
                        }, 1500);
                    }
                },{
                    icon: 'zmdi zmdi-share',
                    title: 'DASHBOARDS.WIDGETS.MENU.SHARE'
                },{
                    icon: 'zmdi zmdi-print',
                    title: 'DASHBOARDS.WIDGETS.MENU.PRINT'
                }]
            });

            $scope.lineChart = {
                labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                series: ['Efficiency', 'Worked time', 'Success rate'],
                options: {
                    datasetFill: false,
                    responsive: true
                },
                data: []
            };

            function randomData() {
                $scope.lineChart.data = [];
                for(var series = 0; series < $scope.lineChart.series.length; series++) {
                    var row = [];
                    for(var label = 0; label < $scope.lineChart.labels.length; label++) {
                        row.push(Math.floor((Math.random() * 150) + 1));
                    }
                    $scope.lineChart.data.push(row);
                }

                var config = {
                    params: {
                        userId: $rootScope.globals.currentUser.userProfileId
                    }
                };

                ApiWebService.get(ApiConfig + 'analytics/getgoalsperformance', config, success, failed);

                function success(result){
                    $scope.lineChart.data = [];
                    for(var series = 0; series < $scope.lineChart.series.length; series++) {
                        var row = [];
                        for(var label = 0; label < $scope.lineChart.labels.length; label++) {
                            row.push(result.data.;
                        }
                        $scope.lineChart.data.push(row);
                    }
                }

                function failed(result){
                    $mdToast.show(
                        $mdToast.simple()
                            .content(result.data)
                            .position('bottom right')
                            .hideDelay(1500)
                    );
            }

            // Simulate async data update
            //$scope.intervalPromise = $interval(randomData, 5000);
        }
    }
})();
