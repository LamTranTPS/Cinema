(function (app) {
    app.controller("resultController", resultController);

    resultController.$inject = ["$scope", "mkBlocker", "apiService", "notifyService"];
    function resultController($scope, mkBlocker, apiService, notifyService) {
        $scope.result = "";
        $scope.execute = execute;

        function execute() {
            paramsObject = {};
            window.location.hash.replace("#!/payment/result", "").replace(/\?/, '').split('&').map(function (o) { paramsObject[o.split('=')[0]] = o.split('=')[1] });
            console.log("Payer ID: " + paramsObject.PayerID)
            if (paramsObject.status == "success") {
                mkBlocker.blockUI();
                apiService.get("api/paypals/execute/" + paramsObject.PayerID + "/" + paramsObject.paymentId, null, function (result) {
                    if (result.data.success) {
                        $scope.result = "Success";
                        $scope.event = result.data.elements;
                        mkBlock.unblockUI();
                        notifyService.displaySuccess('Success.');
                    } else {
                        $scope.result = "Error";
                        mkBlock.unblockUI();
                        notifyService.displayError("Error!");
                    }
                }, function (error) {
                    $scope.result = "Error";
                    mkBlock.unblockUI();
                    notifyService.displayError(error.xhrStatus);
                });
            } else {
                $scope.result = "Error";
                notifyService.displayError("Error.");
            }
        }
    }
})(angular.module("adminApp.paypals"));