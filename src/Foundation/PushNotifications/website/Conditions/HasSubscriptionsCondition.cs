using SF.Foundation.PushNotifications.Constants;
using SF.Foundation.PushNotifications.Facades;
using SF.Foundation.PushNotifications.Facets;
using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SF.Foundation.PushNotifications.Conditions
{
    public class HasSubscriptionCondition<T> : StringOperatorCondition<T> where T : RuleContext
    {
        public string Value { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Assert.IsNotNull(Tracker.Current, "Tracker.Current is not initialized");
            Assert.IsNotNull(Tracker.Current.Session, "Tracker.Current.Session is not initialized");
            Assert.IsNotNull(Tracker.Current.Session.Interaction, "Tracker.Current.Session.Interaction is not initialized");

            return PushSubscriptionManager.Current.GetSubscriptions().Count > 0;

        }
    }
}