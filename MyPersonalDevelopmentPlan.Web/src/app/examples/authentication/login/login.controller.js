(function() {
    'use strict';

    angular
        .module('app.examples.authentication')
        .controller('LoginController', LoginController);

    /* @ngInject */
    function LoginController($state, triSettings, AuthenticationService, $mdToast) {
        var vm = this;
        vm.loginClick = loginClick;
        vm.socialLogins = [{
            icon: 'fa fa-twitter',
            color: '#5bc0de',
            url: '#'
        },{
            icon: 'fa fa-facebook',
            color: '#337ab7',
            url: '#'
        },{
            icon: 'fa fa-google-plus',
            color: '#e05d6f',
            url: '#'
        },{
            icon: 'fa fa-linkedin',
            color: '#337ab7',
            url: '#'
        }];
        vm.triSettings = triSettings;
        // create blank user variable for login form
        vm.user = {
            username: '',
            password: ''
        };

        ////////////////

        function loginClick() {
            AuthenticationService.Login(vm.user, loginCompleted, loginFailed);
        }

        function loginCompleted(result) {
            if (result.data.success) {
                AuthenticationService.SetCredentials(vm.user, result);
                $state.go('triangular.admin-default.dashboard-analytics');
            }

        }

        function loginFailed(response) {
            $mdToast.show(
                $mdToast.simple()
                    .content($filter('translate')(response))
                    .position('bottom right')
                    .hideDelay(5000)
            );
        }
    }
})();
