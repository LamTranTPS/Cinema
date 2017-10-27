
(function () {
    angular.module("adminApp.films", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("films", {
            url: "/films",
            parent: "base",
            templateUrl: "/app/compoments/films/filmListView.html",
            controller: "filmListController"
        }).state("films/add", {
            url: "/films/add",
            parent: "base",
            templateUrl: "/app/compoments/films/filmAddView.html",
            controller: "filmAddController"
        }).state("films/edit", {
            url: "/films/edit/:id",
            parent: "base",
            templateUrl: "/app/compoments/films/filmEditView.html",
            controller: "filmEditController"
        });
        $urlRouterProvider.otherwise("/films");
    };

})();