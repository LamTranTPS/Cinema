(function (app) {
    app.controller("userListController", userListController);

    userListController.$inject = ["$scope", "apiService"];
    function userListController($scope, apiService) {
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listUser = [];
        $scope.searchKey = "";
        $scope.getListUser = getListUser;
        $scope.search = search;
        function search() {
            getListUser();
        }

        function getListUser(page) {
            if ($scope.searchKey) {
                $scope.searchKey = "/" + $scope.searchKey;
            }
            apiService.get("/api/Users/" + page + "/" + $scope.pageSize + $scope.searchKey, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listUser = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }

        $scope.getListUser(0);
    }
})(angular.module("adminApp.users"));