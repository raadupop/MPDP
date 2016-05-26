/**
 * Created by Radu  Pop on 5/26/2016.
 */
(function(){
    'use strict';

    angular.module('app.examples.forms').directive('estimation', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attr, ctrl) {
                function customValidator(ngModelValue) {

                    if (/([0-9][0-9]?)[ ]?m/.test(ngModelValue)) {
                        ctrl.$setValidity('estimationValidator', true);
                    } else if (/([0-9][0-9]?)[ ]?h/.test(ngModelValue)) {
                        ctrl.$setValidity('estimationValidator', true);
                    } else if (/([0-9])[ ]?d/.test(ngModelValue)) {
                        ctrl.$setValidity('estimationValidator', true);
                    } else {
                        ctrl.$setValidity('estimationValidator', false);
                    }
                    return ngModelValue;
                }
                ctrl.$parsers.push(customValidator);
            }
        };
    });

})();
