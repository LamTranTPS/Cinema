(function (app) {

    app.factory("apiService", apiService);

    apiService.$inject = ['$http', "authenticationService"];
    function apiService($http, authenticationService) {
        return {
            get: get,
            post: post
        }

        function post(url, data, success, failure) {
            authenticationService.setHeader();
            $http.post(url, data).then(function (result) {
                if (result.data.success) {
                    success(result);
                } else {
                    failure(result.data.message);
                }
            }, function (error) {
                failure(error);
            });
        }

        function get(url, params, success, failure) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                if (result.data.success) {
                    success(result);
                } else {
                    failure(result.data.message);
                }
            }, function (error) {
                failure(error);
            });
        }
    }

})(angular.module("adminApp.common"));