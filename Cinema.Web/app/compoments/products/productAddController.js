(function (app) {
    app.controller("productAddController", productAddController);

    productAddController.$inject = ["$scope", '$state', '$stateParams', "apiService", "notifyService"];
    function productAddController($scope, $state, $stateParams, apiService, notifyService) {
        $scope.category = $stateParams.category == "Non" ? "" : $stateParams.category;
        $scope.listSupplier = [];
        $scope.product = {
        };
        $scope.close = close;

        $scope.addProduct = addProduct;

        function loadSupplier() {
            apiService.get("http://localhost:5260/api/Supplier/GetAll", null, function (result) {
                $scope.listSupplier = result.data.elements;
                if ($scope.totalCount == 0) {
                    notifyService.displayWarning('No data found!');
                }
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        function addProduct() {
            var url = "http://localhost:5260/api/Product/insert";
            if ($scope.category == 'Phone') {
                url = "http://localhost:5260/api/Product/Phone/insert";
            } else if ($scope.category == 'Clothe') {
                url = "http://localhost:5260/api/Product/Clothe/insert";
            }
            apiService.post(url, $scope.product,
                function (result) {
                    notifyService.displaySuccess($scope.product.ProductName + ' added.');
                    close();
                }, function (error) {
                    notifyService.displayError('Error.');
                });
        }

        function close() {
            if ($scope.category == 'Phone') {
                $state.go('phones');
            } else if ($scope.category == 'Clothe') {
                $state.go('clothes');
            } else {
                $state.go('products');
            }
        }

        loadSupplier();
    }
})(angular.module("productapp.product"));