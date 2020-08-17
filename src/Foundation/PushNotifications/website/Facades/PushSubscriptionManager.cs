using SF.Foundation.PushNotifications.Constants;
using SF.Foundation.PushNotifications.Facets;
using Sitecore.Analytics;
using Sitecore.Analytics.Model;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.Collection.Model;
using Sitecore.Xdb.MarketingAutomation.Core.Requests;
using Sitecore.DependencyInjection;

namespace SF.Foundation.PushNotifications.Facades
{
    public class PushSubscriptionManager
    {
        private static PushSubscriptionManager _subscriptionManager = new PushSubscriptionManager();

        private PushSubscriptionManager()
        {

        }

        public static PushSubscriptionManager Current
        {
            get
            {
                return _subscriptionManager;
            }
        }

        protected PushSubscriptions GetPushSubscriptions()
        {
            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var contact = client.Get<Contact>(GetIdentifier(), new Sitecore.XConnect.ContactExpandOptions(FacetNames.PushSubscriptions));
                if (contact != null)
                {
                    return contact.GetFacet<PushSubscriptions>();
                }
            }
            return null;
        }

        private static Sitecore.Analytics.Model.Entities.ContactIdentifier GetContactId()
        {
            if (Tracker.Current?.Contact == null)
            {
                return null;
            }
            if (Tracker.Current.Contact.IsNew || Tracker.Current.Contact.Identifiers.Count == 0)
            {
                // write the contact to xConnect so we can work with it
                SaveContact();
            }

            return Tracker.Current.Contact.Identifiers.FirstOrDefault();
        }

        private static IdentifiedContactReference GetIdentifier()
        {
            // get the contact id from the current contact
            var id = GetContactId();

            var currentIdentifier = Tracker.Current.Contact.Identifiers.FirstOrDefault();

            if (currentIdentifier != null)
            {
                return new IdentifiedContactReference("xDB.Tracker", Sitecore.Analytics.Tracker.Current.Contact.ContactId.ToString("N"));
            }
            else
            {
                return new IdentifiedContactReference(id.Source, id.Identifier);
            }

        }

        public static void SaveContact()
        {
            try
            {
                if (Tracker.Current.Contact.IsNew || Tracker.Current.Contact.Identifiers.Count == 0)
                {
                    Tracker.Current.Session.IdentifyAs("Anon", Tracker.Current.Contact.ContactId.ToString("N"));
                }

                // we need the contract to be saved to xConnect. It is only in session now
                var manager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as Sitecore.Analytics.Tracking.ContactManager;
                if (manager != null)
                {
                    // Save contact to xConnect; at this point, a contact has an anonymous
                    // TRACKER IDENTIFIER, which follows a specific format. Do not use the contactId overload
                    // and make sure you set the ContactSaveMode as demonstrated
                    Sitecore.Analytics.Tracker.Current.Contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
                    manager.SaveContactToCollectionDb(Sitecore.Analytics.Tracker.Current.Contact);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error Saving Contact", ex, Current);
            }
        }

        public PushSubscription this[Guid deviceId]
        {
            get
            {
                if (Tracker.Current == null)
                {
                    Tracker.StartTracking();
                }

                try
                {

                    var pushSubscriptions = GetPushSubscriptions();

                    if (pushSubscriptions == null ||
                        !pushSubscriptions.Subscriptions.ContainsKey(Tracker.Current.Interaction.DeviceId))
                    {
                        return null;
                    }

                    return pushSubscriptions.Subscriptions[deviceId];

                }
                catch (XdbExecutionException ex)
                {
                    // Manage conflicts / exceptions
                    Sitecore.Diagnostics.Log.Error("Error Fetching Custom PushSubscriptions Facet", ex, this);
                }

                return null;
            }
            set
            {
                if (Tracker.Current == null)
                {
                    Tracker.StartTracking();
                }

                try
                {
                    using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                    {
                        Contact contact = EnsureContact(client);
                        if (contact != null)
                        {
                            var pushSubscriptions = contact.GetFacet<PushSubscriptions>();
                            if (pushSubscriptions == null)
                            {
                                pushSubscriptions = new PushSubscriptions();
                            }

                            if (!pushSubscriptions.Subscriptions.ContainsKey(deviceId))
                            {
                                pushSubscriptions.Subscriptions.Add(deviceId, value);
                            }
                            else
                            {
                                pushSubscriptions.Subscriptions[deviceId] = value;
                            }

                            client.SetFacet(contact, FacetNames.PushSubscriptions, pushSubscriptions);
                            client.Submit();

                        }
                        
                    }
                }
                catch (XdbExecutionException ex)
                {
                    // Manage conflicts / exceptions
                    Sitecore.Diagnostics.Log.Error("Error SAving Custom PushSubscriptions Facet", ex, this);
                }


            }
        }

        public void AddContactToList(Guid listId)
        {
            if (Tracker.Current == null)
            {
                Tracker.StartTracking();
            }

            try
            {
                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    var contact = client.Get<Contact>(GetIdentifier(), new Sitecore.XConnect.ContactExpandOptions("ListSubscriptions"));
                    if (contact == null)
                    {
                        SaveContact();

                        contact = client.Get<Contact>(GetIdentifier(), new Sitecore.XConnect.ContactExpandOptions("ListSubscriptions"));

                    }

                    if (contact != null)
                    {
                        if (contact.Identifiers.All(x => x.Source != "ListManager"))
                            client.AddContactIdentifier(contact, new ContactIdentifier("ListManager", Guid.NewGuid().ToString(), ContactIdentifierType.Known));

                        var subscriptions = contact.ListSubscriptions() ?? new ListSubscriptions();
                        var subscription = new ContactListSubscription(DateTime.UtcNow, true, listId);
                        subscriptions.Subscriptions.Add(subscription);
                        client.SetListSubscriptions(contact, subscriptions);

                    }

                }
            }
            catch (XdbExecutionException ex)
            {
                // Manage conflicts / exceptions
                Sitecore.Diagnostics.Log.Error("Error Setting List Subscription", ex, this);
            }
        }

       

        private static Contact EnsureContact(XConnectClient client)
        {
            var contact = client.Get<Contact>(GetIdentifier(), new Sitecore.XConnect.ContactExpandOptions(FacetNames.PushSubscriptions));
            if (contact == null)
            {
                SaveContact();

                contact = client.Get<Contact>(GetIdentifier(), new Sitecore.XConnect.ContactExpandOptions(FacetNames.PushSubscriptions));

            }

            return contact;
        }


        /// <summary>
        /// Get the Entire Dictionary if you'll be working with multiple
        /// keys or updates.
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public Dictionary<Guid, PushSubscription> GetSubscriptions()
        {
            if (Tracker.Current == null)
            {
                Tracker.StartTracking();
            }

            try
            {
                var pushSubscriptions = GetPushSubscriptions();
                if (pushSubscriptions != null)
                {
                    return pushSubscriptions.Subscriptions;
                }

            }
            catch (XdbExecutionException ex)
            {
                // Manage conflicts / exceptions
                Sitecore.Diagnostics.Log.Error("Error Fetching Custom PushSubscriptions Facet", ex, this);
            }
            return null;
        }

        /// <summary>
        /// Use for Batch Updates of Settings within an Area
        /// </summary>
        /// <param name="area"></param>
        /// <param name="areaSettings"></param>
        public void UpdateSubscriptions(Dictionary<Guid, PushSubscription> subscriptions)
        {
            if (Tracker.Current == null)
            {
                Tracker.StartTracking();
            }

            try
            {
                using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
                {
                    var contact = EnsureContact(client);
                    if (contact != null)
                    {
                        var pushSubscriptions = contact.GetFacet<PushSubscriptions>();
                        if (pushSubscriptions == null)
                        {
                            pushSubscriptions = new PushSubscriptions();
                        }

                        pushSubscriptions.Subscriptions = subscriptions;

                        client.SetFacet(contact, FacetNames.PushSubscriptions, pushSubscriptions);
                        client.Submit();
                    }
                }
            }
            catch (XdbExecutionException ex)
            {
                // Manage conflicts / exceptions
                Sitecore.Diagnostics.Log.Error("Error Updating Custom PushSubscriptions Facet", ex, this);
            }
        }

        public bool ContainsDevice(Guid deviceId)
        {
            if (Tracker.Current == null)
            {
                Tracker.StartTracking();
            }

            try
            {
                var pushSubscriptions = GetPushSubscriptions();
                if (pushSubscriptions == null ||
                            !pushSubscriptions.Subscriptions.ContainsKey(deviceId))
                {
                    return false;
                }
                return true;
            }
            catch (XdbExecutionException ex)
            {
                // Manage conflicts / exceptions
                Sitecore.Diagnostics.Log.Error("Error Fetching Custom PushSubscriptions Facet", ex, this);
            }

            return false;
        }

    }
}