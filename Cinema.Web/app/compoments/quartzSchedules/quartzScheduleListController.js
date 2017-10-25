(function (app) {
    app.controller("quartzScheduleListController", quartzScheduleListController);

    quartzScheduleListController.$inject = ["$scope", "apiService", "notifyService", "$ngBootbox"];
    function quartzScheduleListController($scope, apiService, notifyService, $ngBootbox) {
        $scope.page = 0;
        $scope.pageSize = 10;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listSchedule = [];
        $scope.searchKey = "";
        $scope.getListSchedule = getListSchedule;
        $scope.search = search;
        $scope.deleteConfirm = deleteConfirm;
        $scope.startOrPause = startOrPause;

        function search() {
            getListSchedule(0);
        }

        function getListSchedule(page) {
            var search = "";
            if ($scope.searchKey) {
                search = "/" + $scope.searchKey;
            }
            apiService.get("/api/quartzschedules/" + page + "/" + $scope.pageSize + search, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listSchedule = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }

        function deleteConfirm(id, status) {
            $ngBootbox.confirm("Do you want to delete?").then(function () {
                apiService.get("/api/quartzschedules/delete/" + id, null, function (result) {
                    if (result.data.success) {
                        notifyService.displaySuccess('Deleted!');
                        getListSchedule($scope.page);
                    } else {
                        notifyService.displayWarning('Error!');
                    }
                }, function (error) {
                    $scope.error = error;
                    notifyService.displayError(error);
                });
            });
        }

        function startOrPause(index) {
            if ($scope.listSchedule[index].status) {
                $ngBootbox.confirm("Do you want to pause?").then(function () {
                    apiService.get("/api/quartzschedules/pause/" + $scope.listSchedule[index].id, null, function (result) {
                        if (result.data.success) {
                            notifyService.displaySuccess('Paused!');
                            $scope.listSchedule[index].status = false;
                        } else {
                            notifyService.displayWarning('Error!');
                        }
                    }, function (error) {
                        $scope.error = error;
                        notifyService.displayError(error);
                    });
                });
            } else {
                apiService.get("/api/quartzschedules/start/" + $scope.listSchedule[index].id, null, function (result) {
                    if (result.data.success) {
                        notifyService.displaySuccess('Started!');
                        $scope.listSchedule[index].status = true;
                    } else {
                        notifyService.displayWarning('Error!');
                    }
                }, function (error) {
                    $scope.error = error;
                    notifyService.displayError(error);
                });
            }
        }

        $scope.getListSchedule(0);
    }
})(angular.module("adminApp.quartzSchedules"));