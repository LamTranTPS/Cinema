(function (app) {
    app.controller("cinemaListController", cinemaListController);

    cinemaListController.$inject = ["$scope", "apiService"];
    function cinemaListController($scope, apiService) {
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listCinema = [];
        $scope.searchKey = "";
        $scope.getListCinema = getListCinema;
        $scope.search = search;
        function search() {
            getListCinema();
        }

        function getListCinema(page) {
            var search = "";
            if ($scope.searchKey) {
                search = "/" + $scope.searchKey;
            }
            apiService.get("/api/Cinemas", null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listCinema = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }

        $scope.getListCinema(0);
    }
})(angular.module("adminApp.cinemas"));