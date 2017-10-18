(function (app) {
    app.controller("phoneListController", phoneListController);

    phoneListController.$inject = ["$scope", "apiService", "notifyService", "$ngBootbox"];
    function phoneListController($scope, apiService, notifyService, $ngBootbox) {
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.pageSize = 10;
        $scope.totalCount = 0;
        $scope.listProduct = [];
        $scope.searchKey = "";

        $scope.getListProduct = getListProduct;
        $scope.search = search;
        $scope.deleteConfirm = deleteConfirm;

        function search() {
            getListProduct(0);
        }

        function deleteConfirm(id) {
            $ngBootbox.confirm("Do you want to delete?").then(function () {
                apiService.get("http://localhost:5260/api/Product/delete/" + id, null, function (result) {
                    if (result.data.success) {
                        notifyService.displaySuccess('Deleted!');
                        getListProduct($scope.page);
                    } else {
                        notifyService.displayWarning('Error!');
                    }
                }, function (error) {
                    $scope.error = error;
                    notifyService.displayError(error.xhrStatus);
                });
            });
        }

        function getListProduct(page) {
            apiService.get("http://localhost:5260/api/Product/Phone/GetListPaging/" + page + "/" + $scope.pageSize + "/" + $scope.searchKey, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / $scope.pageSize);
                $scope.totalCount = result.data.total;
                $scope.listProduct = result.data.elements;
                if ($scope.totalCount == 0) {
                    notifyService.displayWarning('No data found!');
                }
            }, function (error) {
                $scope.error = error;
                notifyService.displayError(error.xhrStatus);
            });
        }

        $scope.getListProduct(0);
    }
})(angular.module("productapp.product"));