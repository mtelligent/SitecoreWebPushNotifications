using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Foundation.PushNotifications.Models
{
    public class PushNotificationsConfiguration
    {
        public string MailTo { get; set;}
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }

        public string GoogleAPIKey { get; set; }
    }
}