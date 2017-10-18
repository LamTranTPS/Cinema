
(function () {

    angular.module("productapp.product", ["productapp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("products", {
            url: "/products",
            templateUrl: "/app/compoments/products/productListView.html",
            controller: "productListController"
        }).state("phones", {
            url: "/phones",
            templateUrl: "/app/compoments/products/phoneListView.html",
            controller: "phoneListController"
        }).state("clothes", {
            url: "/clothes",
            templateUrl: "/app/compoments/products/clotheListView.html",
            controller: "clotheListController"
        }).state("product_add", {
            url: "/product_add/:category",
            templateUrl: "/app/compoments/products/productAddView.html",
            controller: "productAddController"
        }).state("product_edit", {
            url: "/product_edit/:category/:id",
            templateUrl: "/app/compoments/products/productEditView.html",
            controller: "productEditController"
            });
        $urlRouterProvider.otherwise("/products");
    };

})();