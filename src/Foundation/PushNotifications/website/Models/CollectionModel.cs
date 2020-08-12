using SF.Foundation.PushNotifications.Constants;
using SF.Foundation.PushNotifications.Facets;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Foundation.PushNotifications.Models
{
    public class CollectionModel
    {
        public static XdbModel Model { get; } = BuildModel();

        private static XdbModel BuildModel()
        {
            XdbModelBuilder modelBuilder = new XdbModelBuilder("PushSubscriptionsModel", new XdbModelVersion(1, 0));

            // Facets and events here
            modelBuilder.RegisterType<PushSubscription>(true);
            modelBuilder.DefineFacet<Contact, PushSubscriptions>(FacetNames.PushSubscriptions);

            return modelBuilder.BuildModel();
        }
    }
}