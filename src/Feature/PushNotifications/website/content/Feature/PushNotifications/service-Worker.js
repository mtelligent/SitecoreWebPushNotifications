self.addEventListener('activate', async () => {
    // This will be called only once when the service worker is activated.
    console.log('service worker installed');
})

self.addEventListener('push', function (event) {

    console.log("Received Push");
    console.log("Data [" + event.data.text() + "]");

    var model = event.data.json();

    self.registration.showNotification(model.title, {
        body: model.body,
        icon: "../../../.." + model.icon.replace(/"/g, ''),
        image: "../../../.." + model.image.replace(/"/g, ''),
        data: "../../../.." + model.cta.replace(/"/g, '')
    });

});

function openPushNotification(event) {
    console.log("[Service Worker] Notification click Received.", event.notification.data);

    var url = event.notification.data;
    event.notification.close();
    event.waitUntil(clients.openWindow(url));
}

self.addEventListener("notificationclick", openPushNotification);