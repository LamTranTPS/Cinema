
(function () {
    angular.module("productapp.order", ["productapp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("orders", {
            url: "/orders",
            templateUrl: "/app/compoments/orders/orderListView.html",
            controller: "orderListController"
        }).state("order_add", {
            url: "/order_add",
            templateUrl: "/app/compoments/orders/orderAddView.html",
            controller: "orderAddController"
        }).state("order_edit", {
            url: "/order_edit",
            templateUrl: "/app/compoments/orders/orderEditView.html",
            controller: "orderEditController"
            });
        $urlRouterProvider.otherwise("/orders");
    };

})();