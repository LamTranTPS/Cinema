
(function () {
    angular.module("adminApp.users", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("users", {
            url: "/users",
            parent: "base",
            templateUrl: "/app/compoments/users/userListView.html",
            controller: "userListController"
        }).state("customer_add", {
            url: "/customer_add",
            templateUrl: "/app/compoments/customers/customerAddView.html",
            controller: "customerAddController"
        }).state("customer_edit", {
            url: "/customer_edit",
            templateUrl: "/app/compoments/customers/customerEditView.html",
            controller: "customerEditController"
            });
        $urlRouterProvider.otherwise("/customers");
    };

})();