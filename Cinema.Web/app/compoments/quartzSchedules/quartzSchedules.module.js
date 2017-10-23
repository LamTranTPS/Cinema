
(function () {
    angular.module("adminApp.quartzSchedules", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("quartz/schedules", {
            url: "/quartz/schedules",
            parent: "base",
            templateUrl: "/app/compoments/quartzSchedules/quartzScheduleListView.html",
            controller: "quartzScheduleListController"
        }).state("quartz/schedules/add", {
            url: "/quartz/schedules/add",
            parent: "base",
            templateUrl: "/app/compoments/quartzSchedules/quartzScheduleAddView.html",
            controller: "quartzScheduleAddController"
        });
        $urlRouterProvider.otherwise("/quartz/schedules");
    };

})();