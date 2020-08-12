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

    function askPermission() {
        return new Promise(function (resolve, reject) {
            const permissionResult = Notification.requestPermission(function (result) {
                resolve(result);
            });

            if (permissionResult) {
                permissionResult.then(resolve, reject);
            }
        })
            .then(function (permissionResult) {
                if (permissionResult !== 'granted') {
                    throw new Error('We weren\'t granted permission.');
                }
            });
    }

    function subscribeUserToPush(publicKey) {
        return navigator.serviceWorker.register('/content/Feature/PushNotifications/service-worker.js')
            .then(function (registration) {
                const subscribeOptions = {
                    userVisibleOnly: true,
                    applicationServerKey: urlBase64ToUint8Array(
                        publicKey
                    )
                };

                return registration.pushManager.subscribe(subscribeOptions);
            })
            .then(function (pushSubscription) {
                console.log('Received PushSubscription: ', JSON.stringify(pushSubscription));
                return pushSubscription;
            });
    }

    function sendSubscriptionToBackEnd(subscription, goalId) {
        var data = {
            subscription: subscription,
            goalId: goalId
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

    function subscribe(publicKey, goalId) {
        if (!isSupported()) {
            console.log('Not Supported.');
            return;

        }
        if (!askPermission()) {
            console.log('Permission Denied')
            return;
        }

        subscribeUserToPush(publicKey).then(function (subscription) {
            sendSubscriptionToBackEnd(subscription, goalId);
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