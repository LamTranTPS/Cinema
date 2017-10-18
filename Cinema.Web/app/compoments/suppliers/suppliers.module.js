
(function () {
    angular.module("productapp.supplier", ["productapp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("suppliers", {
            url: "/suppliers",
            templateUrl: "/app/compoments/suppliers/supplierListView.html",
            controller: "supplierListController"
        }).state("supplier_add", {
            url: "/supplier_add",
            templateUrl: "/app/compoments/suppliers/supplierAddView.html",
            controller: "supplierAddController"
        }).state("supplier_edit", {
            url: "/supplier_edit",
            templateUrl: "/app/compoments/suppliers/supplierEditView.html",
            controller: "supplierEditController"
            });
        $urlRouterProvider.otherwise("/suppliers");
    };

})();