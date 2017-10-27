
(function () {
    angular.module("adminApp.schedules", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("schedules", {
            url: "/schedules",
            parent: "base",
            templateUrl: "/app/compoments/schedules/scheduleListView.html",
            controller: "scheduleListController"
        }).state("schedules/add", {
            url: "/schedules/add",
            parent: "base",
            templateUrl: "/app/compoments/schedules/scheduleAddView.html",
            controller: "scheduleAddController"
        }).state("schedules/edit", {
            url: "/schedules/edit/:id",
            parent: "base",
            templateUrl: "/app/compoments/schedules/scheduleEditView.html",
            controller: "scheduleEditController"
        });
        $urlRouterProvider.otherwise("/schedules");
    };

})();