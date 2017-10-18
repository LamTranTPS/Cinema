(function (app) {
    app.controller("rootController", rootController);

    rootController.$inject = ["$scope", "$state", "authData", "authenticationService", "loginService"]
    function rootController($scope, $state, authData, authenticationService, loginService) {
        $scope.logout = logout;
        $scope.accountData = authData.authenticationData;

        function logout() {
            loginService.logout();
            $state.go("users");
        }

        //function config() {
        //    if (!token && $state.current.name.toLowerCase() != "login") {
        //        $state.go("login");
        //    }
        //}

        //config();
    }
})(angular.module("adminApp"));
