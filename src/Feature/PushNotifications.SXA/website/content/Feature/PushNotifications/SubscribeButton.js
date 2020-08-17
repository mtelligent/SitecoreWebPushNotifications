/// <reference path="../../../../../pushnotifications/website/content/feature/pushnotifications/pushnotifications.js" />

XA.component.subscribeButton = (function ($, document) {

    function init() {

        $('.subscribe-button').click(function (event) {

            event.preventDefault();

            var publicKey = $(this).attr("data-publicKey");
            var goalId = $(this).attr("data-goalId");
            var configId = $(this).attr("data-configId");

            //Invoke Register Code.
            SF.PushNotfications.subscribe(publicKey, goalId, configId);

        });

        
    }


    var pub = {};

    pub.init = function () {
        init();
    };

    return pub;

}(jQuery, document));

XA.register("subscribeButton", XA.component.subscribeButton);