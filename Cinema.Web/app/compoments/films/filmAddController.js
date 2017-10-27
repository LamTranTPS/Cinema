(function (app) {
    app.controller("eventAddController", eventAddController);

    eventAddController.$inject = ["$scope", '$state', "apiService", "notifyService"];
    function eventAddController($scope, $state, apiService, notifyService) {
        $scope.event = {
        };
        $scope.close = close;
        $scope.addEvent = addEvent;

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

        function addEvent() {
            var url = "api/events/insert";
            apiService.post(url, $scope.event,
                function (result) {
                    notifyService.displaySuccess($scope.event.Name + ' added.');
                    close();
                }, function (error) {
                    notifyService.displayError('Error.');
                });
        }

        function close() {
            $state.go('events');
        }
        
        loadCinemaChain();
    }
})(angular.module("adminApp.events"));