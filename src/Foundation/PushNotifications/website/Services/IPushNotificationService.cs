using SF.Foundation.PushNotifications.Facets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Foundation.PushNotifications.Services
{
    public interface IPushNotificationService
    {
        void SendNotification(PushSubscription subscription, string message);
    }
}