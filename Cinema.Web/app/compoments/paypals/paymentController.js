(function (app) {
    app.controller("paymentController", paymentController);

    paymentController.$inject = ["$scope", "mkBlocker", "apiService", "notifyService"];
    function paymentController($scope, mkBlocker, apiService, notifyService) {
        $scope.item = {
            "name" : "Item Name",
            "currency" : "USD",
            "price" : 5,
            "quantity" : 1,
            "sku": "sku",
            "RedirectUrl": "http://localhost:63006/#!/payment/result"
        };
        $scope.addPayment = addPayment;
        
        function addPayment() {
            mkBlocker.blockUI();
            var url = "api/paypals/payment";
            apiService.post(url, $scope.item,
                function (result) {
                    var links = result.data.elements.links;
                    angular.forEach(links, function (link) {
                        if (link.rel == "approval_url") {
                            window.location = link.href;
                        }
                    });
                    //mkBlocker.unblockUI();
                    notifyService.displaySuccess('Success.');
                }, function (error) {
                    mkBlocker.unblockUI();
                    notifyService.displayError('Error.');
                });
        }
        
    }
})(angular.module("adminApp.paypals"));