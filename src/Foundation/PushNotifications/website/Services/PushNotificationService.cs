using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lib.Net.Http.WebPush;

namespace SF.Foundation.PushNotifications.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private static PushServiceClient webPushClient = new PushServiceClient();

        public void SendNotification(Facets.PushSubscription subscription, string message)
        {
            try
            {
                var webSubscription = new Lib.Net.Http.WebPush.PushSubscription();
                webSubscription.Endpoint = subscription.Endpoint;
                webSubscription.Keys = subscription.Keys;

                var config = PushNotificationConfigurationService.GetConfiguration();

                //Need to make sure we're using right keys for subscription, maybe change to get for specific site.
                var vapidAuth = new Lib.Net.Http.WebPush.Authentication.VapidAuthentication(config.PublicKey, config.PrivateKey);

                var pushMessage = new PushMessage(message);
                
                webPushClient.RequestPushMessageDeliveryAsync(webSubscription, pushMessage, vapidAuth);
            }
            catch(Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error sending Push Notification", ex, this);
            }
        }
    }
}