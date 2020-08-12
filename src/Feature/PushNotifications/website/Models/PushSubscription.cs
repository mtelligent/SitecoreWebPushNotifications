using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Feature.PushNotifications.Models
{
    public class PushSubscription
    {
        public string Endpoint { get; set; }

        public IDictionary<string, string> Keys { get; set; }
    }
}