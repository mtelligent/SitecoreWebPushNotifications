self.addEventListener('activate', async () => {
    // This will be called only once when the service worker is activated.
    console.log('service worker installed');
})

self.addEventListener('push', function (event) {

    console.log("Received Push");
    console.log("Data [" + event.notification.data + "]");

    var model = JSON.parse(event.notification.data);

    self.registration.showNotification(model.title, {
        body: model.body,
        icon: model.icon,
        image: model.image
    });

});

function openPushNotification(event) {
    console.log("[Service Worker] Notification click Received.", event.notification.data);
    var model = JSON.parse(event.notification.data);
    event.notification.close();
    event.waitUntil(clients.openWindow(model.cta));
}

self.addEventListener("notificationclick", openPushNotification);