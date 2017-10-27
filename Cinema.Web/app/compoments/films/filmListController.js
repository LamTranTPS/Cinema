(function (app) {
    app.controller("filmListController", filmListController);

    filmListController.$inject = ["$scope", "apiService", "notifyService", "$ngBootbox"];
    function filmListController($scope, apiService, notifyService, $ngBootbox) {
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listFilm = [];
        $scope.searchKey = "";
        $scope.getListFilm = getListFilm;
        $scope.search = search;
        $scope.deleteConfirm = deleteConfirm;

        function search() {
            getListFilm(0);
        }

        function getListFilm(page) {
            var search = "";
            if ($scope.searchKey) {
                search = "/" + $scope.searchKey;
            }
            apiService.get("/api/films/" + page + "/" + $scope.pageSize + search, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listFilm = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }

        function deleteConfirm(id) {
            $ngBootbox.confirm("Do you want to delete?").then(function () {
                apiService.get("/api/films/delete/" + id, null, function (result) {
                    if (result.data.success) {
                        notifyService.displaySuccess('Deleted!');
                        getListFilm($scope.page);
                    } else {
                        notifyService.displayWarning('Error!');
                    }
                }, function (error) {
                    $scope.error = error;
                    notifyService.displayError(error);
                });
            });
        }

        $scope.getListFilm(0);
    }
})(angular.module("adminApp.films"));