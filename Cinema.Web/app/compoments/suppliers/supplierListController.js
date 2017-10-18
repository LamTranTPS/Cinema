(function (app) {
    app.controller("supplierListController", supplierListController);

    supplierListController.$inject = ["$scope", "apiService"];
    function supplierListController($scope, apiService) {
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listSupplier = [];
        $scope.getListSupplier = getListSupplier;
        $scope.search = search;

        function search() {
            getListSupplier();
        }

        function getListSupplier(page) {
            var pageSize = 10;
            apiService.get("http://localhost:5260/api/Supplier/GetListPaging/" + page + "/" + pageSize, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / pageSize);
                $scope.totalCount = result.data.total;
                $scope.listSupplier = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }


        $scope.getListSupplier(0);
    }
})(angular.module("productapp.supplier"));