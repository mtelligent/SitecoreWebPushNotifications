using SF.Feature.PushNotifications.Models;
using SF.Foundation.PushNotifications.Facades;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Analytics;
using Sitecore.DependencyInjection;
using Sitecore.Services.Infrastructure.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Sitecore.ListManagement.XConnect;
using Sitecore.Xdb.MarketingAutomation.OperationsClient;
using Sitecore.Xdb.MarketingAutomation.Core.Requests;
using Sitecore.Xdb.MarketingAutomation.Core.Results;
using Sitecore.Marketing.Definitions.Goals;
using Sitecore.Marketing.Definitions;

namespace SF.Feature.PushNotifications.Controllers
{
    public class PushNotificationsController : ServicesApiController
    {
        [HttpPost]
        [ActionName("subscribe")]
        public HttpResponseMessage Subscribe([FromBody] SubscribeModel subscription)
        {
            SaveSubscription(subscription);

            TriggerGoal(subscription);
            
            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }

        private void TriggerGoal(SubscribeModel subscription)
        {
            var goalId = Guid.Empty;
            IGoalDefinition goalDefinition = null;
            if (Guid.TryParse(subscription.GoalId, out goalId))
            {
                IDefinitionManager<IGoalDefinition> goalDefinitionManager = ServiceLocator.ServiceProvider.GetDefinitionManagerFactory().GetDefinitionManager<Sitecore.Marketing.Definitions.Goals.IGoalDefinition>();
                goalDefinition = goalDefinitionManager.Get(goalId, System.Globalization.CultureInfo.InvariantCulture);

                if (goalDefinition != null)
                {
                    //Trigger Goal on last page in the tracker. (May be incorrect if visitor is in multiple tabs)
                    var page = Tracker.Current.Interaction.PreviousPage;
                    page.RegisterGoal(goalDefinition);
                }
                else
                {
                    Sitecore.Diagnostics.Log.Warn("Goal: [" + subscription.GoalId + "] Not Defined", this);
                }
            }
            else
            {
                Sitecore.Diagnostics.Log.Warn("Goal: [" + subscription.GoalId + "] Can Not be Parsed", this);
            }
        }

        

        private static void SaveSubscription(SubscribeModel subscription)
        {
            if (Tracker.Current == null)
            {
                Tracker.StartTracking();
            }

            var facetSubscription = new SF.Foundation.PushNotifications.Facets.PushSubscription()
            {
                Endpoint = subscription.Subscription.Endpoint,
                Keys = new Dictionary<string, string>()
            };

            foreach (var key in subscription.Subscription.Keys.Keys)
            {
                facetSubscription.Keys.Add(key, subscription.Subscription.Keys[key]);
            }

            PushSubscriptionManager.Current[Tracker.Current.Interaction.DeviceId] = facetSubscription;
        }
    }
}