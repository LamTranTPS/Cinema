﻿
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
        });
        $urlRouterProvider.otherwise("/cinemas");
    };

})();