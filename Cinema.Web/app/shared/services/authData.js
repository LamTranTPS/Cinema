(function (app) {
    'use strict';
    app.factory('authData', authData);
    function authData() {
        var authDataFactory = {};

        var authentication = {
            IsAuthenticated: false,
            userName: ""
        };
        authDataFactory.authenticationData = authentication;

        return authDataFactory;
    }
})(angular.module('adminApp.common'));