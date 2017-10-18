
(function () {
    angular.module("adminApp.users", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("users", {
            url: "/users",
            parent: "base",
            templateUrl: "/app/compoments/users/userListView.html",
            controller: "userListController"
        });
        $urlRouterProvider.otherwise("/customers");
    };

})();