(function (app) {
    app.controller("quartzScheduleAddController", quartzScheduleAddController);

    quartzScheduleAddController.$inject = ["$scope", '$state', "apiService", "notifyService"];
    function quartzScheduleAddController($scope, $state, apiService, notifyService) {
        $scope.schedule = {
        };
        $scope.close = close;

        $scope.addSchedule = addSchedule;

        function loadJob() {
            apiService.get("api/quartzjobs", null, function (result) {
                $scope.listJob = result.data.elements;
                if ($scope.totalCount == 0) {
                    notifyService.displayWarning('No data found!');
                }
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        function addSchedule() {
            var url = "api/quartzschedules/insert";
            apiService.post(url, $scope.schedule,
                function (result) {
                    notifyService.displaySuccess($scope.schedule.Name + ' added.');
                    close();
                }, function (error) {
                    notifyService.displayError('Error.');
                });
        }

        function close() {
            $state.go('quartz/schedules');
        }

        loadJob();
    }
})(angular.module("adminApp.quartzSchedules"));