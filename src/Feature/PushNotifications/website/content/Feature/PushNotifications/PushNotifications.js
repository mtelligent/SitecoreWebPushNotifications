var SF = SF || {};
SF.PushNotfications = (function () {

    function isSupported() {
        if (!('serviceWorker' in navigator)) {
            console.log('Service Worker is not supported on this browser.');
            return false;
        }

        if (!('PushManager' in window)) {
            console.log('Push is not supported on this browser');
            return false;
        }

        return true;
    }

    

    function subscribeUserToPush(publicKey) {
        return navigator.serviceWorker.register('/content/Feature/PushNotifications/service-worker.js')
            .then(function (registration) {

                var serviceWorker;
                if (registration.installing) {
                    serviceWorker = registration.installing;
                    // console.log('Service worker installing');
                } else if (registration.waiting) {
                    serviceWorker = registration.waiting;
                    // console.log('Service worker installed & waiting');
                } else if (registration.active) {
                    serviceWorker = registration.active;
                    // console.log('Service worker active');
                }

                var subscribeOptions = {
                    userVisibleOnly: true,
                    applicationServerKey: urlBase64ToUint8Array(
                        publicKey
                    )
                };

                if (serviceWorker.state == "activated") {
                    //If push subscription wasnt done yet have to do here
                    console.log("sw already activated - Do watever needed here");
                    return registration.pushManager.subscribe(subscribeOptions);
                }

                serviceWorker.addEventListener("statechange", function (e) {
                    console.log("sw statechange : ", e.target.state);
                    if (e.target.state == "activated") {
                        // use pushManger for subscribing here.
                        var subscribeOptions = {
                            userVisibleOnly: true,
                            applicationServerKey: urlBase64ToUint8Array(
                                publicKey
                            )
                        };

                        console.log("Just now activated. now we can subscribe for push notification")
                        var pushSubscription = registration.pushManager.subscribe(subscribeOptions);
                        console.log('Received PushSubscription: ', JSON.stringify(pushSubscription));
                    }
                });

                
            })
            .then(function (pushSubscription) {
                console.log('Received PushSubscription: ', JSON.stringify(pushSubscription));
                return pushSubscription;
            });
    }

    function sendSubscriptionToBackEnd(subscription, goalId, configId) {
        var data = {
            subscription: subscription,
            goalId: goalId,
            configId: configId
        };
        return fetch('/api/sf/1.0/pushNotification/subscribe', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
            .then(function (response) {
                if (!response.ok) {
                    throw new Error('Bad status code from server.');
                }

                return;
            });
    }

    function subscribe(publicKey, goalId, configId) {

        if (!isSupported()) {
            console.log('Not Supported.');
            return;

        }

        Notification.requestPermission(function (permissionResult) {
            if (permissionResult !== 'granted') {
                console.log('Permission Denied')
                throw new Error('We weren\'t granted permission.');
            }
            subscribeUserToPush(publicKey).then(function (subscription) {
                sendSubscriptionToBackEnd(subscription, goalId, configId);
            });
        });

    }

    function urlBase64ToUint8Array(base64String) {
        const padding = '='.repeat((4 - base64String.length % 4) % 4);
        const base64 = (base64String + padding)
            .replace(/\-/g, '+')
            .replace(/_/g, '/')
            ;
        const rawData = window.atob(base64);
        return Uint8Array.from([...rawData].map((char) => char.charCodeAt(0)));
    }

    return {
        isSupported: isSupported,
        subscribe: subscribe
    }

})();