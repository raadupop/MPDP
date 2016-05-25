/**
 * Created by Radu  Pop on 5/25/2016.
 */
(function(){
    'use strict';

    angular
        .module('app.examples.forms')
        .controller('GoalRegisterController', GoalRegisterController);

    /* @ngInject */
    function GoalRegisterController() {
        var vm = this;
        vm.goal = {
            username: 'Morris',
            userId: '',
            name: '',
            description: '',
            estimation: ''
        };
    }

})();
