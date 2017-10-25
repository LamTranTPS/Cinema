
(function () {
    angular.module("adminApp.cinemas", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("cinemas", {
            url: "/cinemas",
            parent: "base",
            templateUrl: "/app/compoments/cinemas/cinemaListView.html",
            controller: "cinemaListController"
        }).state("cinemas/add", {
            url: "/cinemas/add",
            parent: "base",
            templateUrl: "/app/compoments/cinemas/cinemaAddView.html",
            controller: "cinemaAddController"
        }).state("cinemas/edit", {
            url: "/cinemas/edit/:id",
            parent: "base",
            templateUrl: "/app/compoments/cinemas/cinemaEditView.html",
            controller: "cinemaEditController"
        });
        $urlRouterProvider.otherwise("/cinemas");
    };

})();