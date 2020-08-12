using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Feature.PushNotifications.Models
{
    public class SubscribeModel
    {
        [JsonProperty("subscription")]
        public PushSubscription Subscription { get; set; }

        [JsonProperty("goalId")]
        public string GoalId { get; set; }

        [JsonProperty("configId")]
        public string ConfigurationId { get; set; }
    }
}