(function (app) {
    app.controller("productEditController", productEditController);

    productEditController.$inject = ["$scope", '$state', '$stateParams', "apiService", "notifyService"];
    function productEditController($scope, $state, $stateParams, apiService, notifyService) {
        $scope.category = $stateParams.category == "Non" ? "" : $stateParams.category;
        $scope.listSupplier = [];
        $scope.product = {
        };
        $scope.close = close;

        $scope.editProduct = editProduct;

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

        function loadProduct() {
            apiService.get("http://localhost:5260/api/Product/GetById/" + $stateParams.id, null, function (result) {
                $scope.product = result.data.elements;
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        function editProduct() {
            var url = "http://localhost:5260/api/Product/update";
            if ($scope.product.Category == 'Phone') {
                url = "http://localhost:5260/api/Product/Phone/update";
            } else if ($scope.product.Category == 'Clothe') {
                url = "http://localhost:5260/api/Product/Clothe/update";
            }
            apiService.post(url, $scope.product,
                function (result) {
                    notifyService.displaySuccess($scope.product.ProductName + ' updated.');
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
        loadProduct();
    }
})(angular.module("productapp.product"));