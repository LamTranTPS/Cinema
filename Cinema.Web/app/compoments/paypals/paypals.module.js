
(function () {
    angular.module("adminApp.paypals", ["adminApp.common"]).config(config);

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("payment", {
            url: "/payment",
            parent: "base",
            templateUrl: "/app/compoments/paypals/paymentView.html",
            controller: "paymentController"
        }).state("payment/result", {
            url: "/payment/result",
            parent: "base",
            templateUrl: "/app/compoments/paypals/resultView.html",
            controller: "resultController"
        });
        $urlRouterProvider.otherwise("/payment");
    };

})();