/**
 * Created by Radu  Pop on 5/12/2016.
 */
(function () {
    'use strict';

    angular.module('app').factory('ApiWebService', apiWebService);

    /* @ngInject */
    function apiWebService($http, $log){

        function get(url, config, success, failure){
            return $http.get(url, config)
                .then(function(result){
                     success(result);
                },function(error){
                    if(error.status == '401'){
                        $log("asd");
                    }

                    else if (failure != null) {
                        failure(error);
                    }
                });
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                .then(function (result) {
                    success(result);
                }, function (error) {
                    if (error.status == '401') {
                        $log("401");
                    }
                    else if (failure != null) {
                        failure(error);
                    }
                });
        }

        return {
            get: get,
            post: post
        };
    }
})();
