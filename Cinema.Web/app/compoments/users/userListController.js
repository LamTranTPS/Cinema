(function (app) {
    app.controller("userListController", userListController);

    userListController.$inject = ["$scope", "apiService", "notifyService", "$ngBootbox"];
    function userListController($scope, apiService, notifyService, $ngBootbox) {
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listUser = [];
        $scope.searchKey = "";
        $scope.getListUser = getListUser;
        $scope.search = search;
        $scope.deleteConfirm = deleteConfirm;

        function search() {
            getListUser(0);
        }

        function getListUser(page) {
            var search = "";
            if ($scope.searchKey) {
                search = "/" + $scope.searchKey;
            }
            apiService.get("/api/Users/" + page + "/" + $scope.pageSize + search, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listUser = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }

        function deleteConfirm(id) {
            $ngBootbox.confirm("Do you want to delete?").then(function () {
                apiService.get("/api/Users/delete/" + id, null, function (result) {
                    if (result.data.success) {
                        notifyService.displaySuccess('Deleted!');
                        getListProduct($scope.page);
                    } else {
                        notifyService.displayWarning('Error!');
                    }
                }, function (error) {
                    $scope.error = error;
                    notifyService.displayError(error.xhrStatus);
                });
            });
        }

        $scope.getListUser(0);
    }
})(angular.module("adminApp.users"));