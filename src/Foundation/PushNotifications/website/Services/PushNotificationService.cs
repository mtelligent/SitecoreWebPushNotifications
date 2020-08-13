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

        public void SendNotification(Facets.PushSubscription subscription, string message, string publicKey, string privateKey)
        {
            try
            {
                var webSubscription = new Lib.Net.Http.WebPush.PushSubscription();
                webSubscription.Endpoint = subscription.Endpoint;
                webSubscription.Keys = subscription.Keys;

                //Need to make sure we're using right keys for subscription, maybe change to get for specific site.
                var vapidAuth = new Lib.Net.Http.WebPush.Authentication.VapidAuthentication(publicKey, privateKey);

                var pushMessage = new PushMessage(message);
                
                var task = webPushClient.RequestPushMessageDeliveryAsync(webSubscription, pushMessage, vapidAuth);
                try
                {
                    task.Wait();
                }
                catch (AggregateException ae)
                {
                    foreach (var e in ae.InnerExceptions)
                    {
                        throw e;
                    }
                }
            }
            catch(Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error sending Push Notification", ex, this);
            }
        }
    }
}