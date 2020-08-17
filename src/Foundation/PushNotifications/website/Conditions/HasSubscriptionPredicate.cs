using System;
using System.Linq.Expressions;
using Sitecore.Framework.Rules;
using Sitecore.XConnect;
using Sitecore.XConnect.Segmentation.Predicates;
using Sitecore.XConnect.Collection.Model;
using System.Linq;
using SF.Foundation.PushNotifications.Facets;

namespace SF.Foundation.PushNotifications.Conditions
{
    public class HasSubscriptionPredicate : ICondition
    {
        public bool Evaluate(IRuleExecutionContext context)
        {
            Contact contact = context.Fact<Contact>();

            var subscriptions = contact.GetFacet<PushSubscriptions>();

            return subscriptions.Subscriptions.Count > 0;
        }
    }
}