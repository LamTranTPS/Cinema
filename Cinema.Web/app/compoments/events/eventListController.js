(function (app) {
    app.controller("eventListController", eventListController);

    eventListController.$inject = ["$scope", "apiService", "notifyService", "$ngBootbox"];
    function eventListController($scope, apiService, notifyService, $ngBootbox) {
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listEvent = [];
        $scope.searchKey = "";
        $scope.getListEvent = getListEvent;
        $scope.search = search;
        $scope.deleteConfirm = deleteConfirm;

        function search() {
            getListEvent(0);
        }

        function getListEvent(page) {
            var search = "";
            if ($scope.searchKey) {
                search = "/" + $scope.searchKey;
            }
            apiService.get("/api/events/" + page + "/" + $scope.pageSize + search, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listEvent = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }

        function deleteConfirm(id) {
            $ngBootbox.confirm("Do you want to delete?").then(function () {
                apiService.get("/api/events/delete/" + id, null, function (result) {
                    if (result.data.success) {
                        notifyService.displaySuccess('Deleted!');
                        getListEvent($scope.page);
                    } else {
                        notifyService.displayWarning('Error!');
                    }
                }, function (error) {
                    $scope.error = error;
                    notifyService.displayError(error);
                });
            });
        }

        $scope.getListEvent(0);
    }
})(angular.module("adminApp.events"));