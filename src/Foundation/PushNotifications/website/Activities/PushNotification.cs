using Sitecore.Xdb.MarketingAutomation.Core.Activity;
using Sitecore.Xdb.MarketingAutomation.Core.Processing.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SF.Foundation.PushNotifications.Facets;
using Sitecore.XConnect.Client;
using SF.Foundation.PushNotifications.Constants;
using Sitecore.XConnect;
using SF.Foundation.PushNotifications.Services;
using SF.Foundation.PushNotifications.Models;
using Newtonsoft.Json;

namespace SF.Foundation.PushNotifications.Activities
{
    public class PushNotification : IActivity
    {
        //Parameters
        public string Body { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public string CTA { get; set; }


        public IActivityServices Services { get; set; }

        public IPushNotificationService PushNotificationService { get; set; }

        public PushNotification(IPushNotificationService pushNotificationService)
        {
            PushNotificationService = pushNotificationService;
        }

        public string MessageId { get; set; }

        public ActivityResult Invoke(IContactProcessingContext context)
        {
            try
            {
                var subscriptions = GetPushSubscriptions(context.Contact);
                if (subscriptions == null)
                {
                    return new SuccessMove("Default");
                }

                var notification = new Notification()
                {
                    Title = this.Title,
                    Body = this.Body,
                    Image = this.Image,
                    Icon = this.Icon,
                    CTA = this.CTA
                };

                var message = JsonConvert.SerializeObject(notification);

                //Dictionary using device, we just want all, so take the value in the KVP
                foreach (var subscription in subscriptions.Subscriptions)
                {
                    try
                    {
                        //Let's send them.
                        PushNotificationService.SendNotification(subscription.Value, message);
                    }
                    catch(Exception ex)
                    {
                        Sitecore.Diagnostics.Log.Error("Failed to send Push Notification", ex, this);
                    }
                }

                return new SuccessMove("Default");

            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Failed sending Message", ex, this);
                return new Failure("failed");
            }
        }


        protected PushSubscriptions GetPushSubscriptions(Contact contextContact)
        {
            var id = contextContact.Identifiers.FirstOrDefault();
            var identifier = new IdentifiedContactReference(id.Source, id.Identifier);
            using (XConnectClient client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var contact = client.Get<Contact>(identifier, new Sitecore.XConnect.ContactExpandOptions(FacetNames.PushSubscriptions));
                if (contact != null)
                {
                    return contact.GetFacet<PushSubscriptions>();
                }
            }
            return null;
        }
    }
}