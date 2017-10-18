(function (app) {
    app.controller("orderListController", orderListController);

    orderListController.$inject = ["$scope", "apiService"];
    function orderListController($scope, apiService) {
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;
        $scope.listOrder = [];
        $scope.getListOrder = getListOrder;
        $scope.search = search;

        function search() {
            getListOrder();
        }

        function getListOrder(page) {
            var pageSize = 10;
            apiService.get("http://localhost:5260/api/Order/GetListPaging/" + page + "/" + pageSize, null, function (result) {
                $scope.page = page;
                $scope.pagesCount = parseInt((result.data.total + 9) / pageSize);
                $scope.totalCount = result.data.total;
                $scope.listOrder = result.data.elements;
            }, function (error) {
                $scope.error = error;
            });
        }


        $scope.getListOrder(0);
    }
})(angular.module("productapp.order"));