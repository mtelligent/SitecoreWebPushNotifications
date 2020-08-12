using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Foundation.PushNotifications.Facets
{
    [Serializable]
    public class PushSubscription : Sitecore.XConnect.Facet
    {
        public PushSubscription() { }

        public string Endpoint { get; set; }

        public Dictionary<string, string> Keys { get; set; }

    }
}