using Sitecore.XA.Foundation.Variants.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Feature.PushNotifications.SXA.Models
{
    public class SubscribeButtonModel : VariantsRenderingModel
    { 
        public Guid GoalId { get; set; }

        public string PublicKey { get; set; }
    }
}