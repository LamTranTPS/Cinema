(function (app) {

    app.factory("notifyService", notifyService);
    

    function notifyService() {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-right",
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        return {
            displayInfo: displayInfo,
            displaySuccess: displaySuccess,
            displayWarning: displayWarning,
            displayError: displayError
        }

        function displayInfo(message) {
            toastr.info(message);
        }

        function displaySuccess(message) {
            toastr.success(message);
        }

        function displayWarning(message) {
            toastr.warning(message);
        }

        function displayError(message) {
            if (Array.isArray(message)) {
                message.each(function (m) {
                    toastr.error(m);
                });
            }
            else
            {
                toastr.error(message);
            }
        }
    }

})(angular.module("adminApp.common"));