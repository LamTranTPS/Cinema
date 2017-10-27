(function (app) {
    app.controller("eventEditController", eventEditController);

    eventEditController.$inject = ["$scope", '$state', '$stateParams', "apiService", "notifyService"];
    function eventEditController($scope, $state, $stateParams, apiService, notifyService) {
        $scope.event = {
        };
        $scope.close = close;
        $scope.editEvent = editEvent;

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

        function loadEvent() {
            apiService.get("api/events/" + $stateParams.id, null, function (result) {
                if (result.data.success) {
                    $scope.event = result.data.elements;
                } else {
                    notifyService.displayError("Error!");
                }
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        function editEvent() {
            var url = "api/events/update";
            apiService.post(url, $scope.event,
                function (result) {
                    notifyService.displaySuccess($scope.event.name + ' updated.');
                    close();
                }, function (error) {
                    notifyService.displayError('Error.');
                });
        }

        function close() {
            $state.go('events');
        }
        
        loadCinemaChain();
        loadEvent();
    }
})(angular.module("adminApp.events"));