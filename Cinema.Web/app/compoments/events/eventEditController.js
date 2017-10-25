(function (app) {
    app.controller("cinemaEditController", cinemaEditController);

    cinemaEditController.$inject = ["$scope", '$state', '$stateParams', "apiService", "notifyService"];
    function cinemaEditController($scope, $state, $stateParams, apiService, notifyService) {
        $scope.cinema = {
        };
        $scope.close = close;
        $scope.editCinema = editCinema;

        function loadCinemaChain() {
            apiService.get("api/cinemachains", null, function (result) {
                $scope.listCinemaChain = result.data.elements;
                if ($scope.totalCount == 0) {
                    notifyService.displayWarning('No data found!');
                }
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        function loadLocation() {
            apiService.get("api/locations", null, function (result) {
                $scope.listLocation = result.data.elements;
                if ($scope.totalCount == 0) {
                    notifyService.displayWarning('No data found!');
                }
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        function loadCinema() {
            apiService.get("api/cinemas/" + $stateParams.id, null, function (result) {
                if (result.data.success) {
                    $scope.cinema = result.data.elements;
                } else {
                    notifyService.displayError("Error!");
                }
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        function editCinema() {
            var url = "api/cinemas/update";
            apiService.post(url, $scope.cinema,
                function (result) {
                    notifyService.displaySuccess($scope.cinema.name + ' updated.');
                    close();
                }, function (error) {
                    notifyService.displayError('Error.');
                });
        }

        function close() {
            $state.go('cinemas');
        }

        loadLocation();
        loadCinemaChain();
        loadCinema();
    }
})(angular.module("adminApp.cinemas"));