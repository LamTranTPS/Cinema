(function (app) {
    app.controller("cinemaListController", cinemaListController);

    cinemaListController.$inject = ["$scope", "apiService", "notifyService", "$ngBootbox"];
    function cinemaListController($scope, apiService, notifyService, $ngBootbox) {
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listCinema = [];
        $scope.searchKey = "";
        $scope.getListCinema = getListCinema;
        $scope.search = search;
        $scope.deleteConfirm = deleteConfirm;

        function search() {
            getListCinema(0);
        }

        function getListCinema(page) {
            var search = "";
            if ($scope.searchKey) {
                search = "/" + $scope.searchKey;
            }
            apiService.get("/api/Cinemas/" + page + "/" + $scope.pageSize + search, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listCinema = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }

        function deleteConfirm(id) {
            $ngBootbox.confirm("Do you want to delete?").then(function () {
                apiService.get("/api/cinemas/delete/" + id, null, function (result) {
                    if (result.data.success) {
                        notifyService.displaySuccess('Deleted!');
                        getListCinema($scope.page);
                    } else {
                        notifyService.displayWarning('Error!');
                    }
                }, function (error) {
                    $scope.error = error;
                    notifyService.displayError(error);
                });
            });
        }

        $scope.getListCinema(0);
    }
})(angular.module("adminApp.cinemas"));