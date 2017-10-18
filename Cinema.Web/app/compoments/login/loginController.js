(function (app) {
    app.controller("loginController", loginController);

    loginController.$inject = ["$scope", "$injector", "loginService", "notifyService"]
    function loginController($scope, $injector, loginService, notifyService) {

        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.loginSubmit = loginSubmit;

        function loginSubmit() {
            loginService.login($scope.loginData.userName, $scope.loginData.password)
                .then(function (respose) {
                    if (respose != null && respose.error != undefined) {
                        notifyService.displayError(respose.error);
                    } else {
                        var stateService = $injector.get("$state");
                        stateService.go("home");
                    }
                });
        }
    }
})(angular.module("adminApp"));
