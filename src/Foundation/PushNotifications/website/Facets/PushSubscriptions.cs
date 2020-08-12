using SF.Foundation.PushNotifications.Constants;
using SF.Foundation.PushNotifications.Models;
using Sitecore.XConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Foundation.PushNotifications.Facets
{
    [Serializable]
    [FacetKey(FacetNames.PushSubscriptions)]
    public class PushSubscriptions : Sitecore.XConnect.Facet
    {
        public PushSubscriptions()
        {
        }

        /// <summary>
        /// Key is Device Id for Contact. 
        /// </summary>
        public Dictionary<Guid, PushSubscription> Subscriptions { get; set; } = new Dictionary<Guid, PushSubscription>();
    }
}