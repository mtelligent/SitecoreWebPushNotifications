using SF.Foundation.PushNotifications.Constants;
using Sitecore.XConnect;
using Sitecore.XConnect.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Foundation.PushNotifications.Facets
{
    public class MergePushSubscriptionsHandler : MergingCalculatedFacetHandler<PushSubscriptions>
    {
        public MergePushSubscriptionsHandler() : base(FacetNames.PushSubscriptions, null)
        {

        }

        protected override bool Merge(PushSubscriptions source, PushSubscriptions target)
        {
            if (source == null || target == null)
            {
                // No contacts changed - return false
                return false;
            }

            foreach (var device in source.Subscriptions.Keys)
            {
                if (target.Subscriptions.ContainsKey(device))
                {
                    //Uncomment if you want to have original subscription stand.
                    //target.Subscriptions[device] = source.Subscriptions[device];
                }
                else
                {
                    //add area
                    target.Subscriptions.Add(device, source.Subscriptions[device]);
                }
            }

            return true;
        }



        protected override bool UpdateFacet(PushSubscriptions currentFacet, Interaction interaction)
        {
            // For calculated facets only
            // Return false as contact not changed by this method
            return false;
        }
    }
}