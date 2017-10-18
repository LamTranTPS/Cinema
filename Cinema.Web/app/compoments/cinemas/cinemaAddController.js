(function (app) {
    app.controller("cinemaAddController", cinemaAddController);

    cinemaAddController.$inject = ["$scope", '$state', '$stateParams', "apiService", "notifyService"];
    function cinemaAddController($scope, $state, $stateParams, apiService, notifyService) {
        $scope.listSupplier = [];
        $scope.cinema = {
        };
        $scope.close = close;

        $scope.addCinema = addCinema;

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

        function addCinema() {
            var url = "api/cinemas/insert";
            apiService.post(url, $scope.cinema,
                function (result) {
                    notifyService.displaySuccess($scope.cinema.Name + ' added.');
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
    }
})(angular.module("adminApp.cinemas"));