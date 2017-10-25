
(function () {
    angular.module("adminApp.events", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("events", {
            url: "/events",
            parent: "base",
            templateUrl: "/app/compoments/events/eventListView.html",
            controller: "eventListController"
        }).state("events/add", {
            url: "/events/add",
            parent: "base",
            templateUrl: "/app/compoments/events/eventAddView.html",
            controller: "eventAddController"
        }).state("events/edit", {
            url: "/events/edit/:id",
            parent: "base",
            templateUrl: "/app/compoments/events/eventEditView.html",
            controller: "eventEditController"
        });
        $urlRouterProvider.otherwise("/events");
    };

})();