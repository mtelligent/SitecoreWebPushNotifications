using SF.Feature.PushNotifications.SXA.Constants;
using SF.Feature.PushNotifications.SXA.Models;
using SF.Foundation.PushNotifications.Services;
using Sitecore.XA.Foundation.Mvc.Repositories.Base;
using Sitecore.XA.Foundation.RenderingVariants.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SF.Feature.PushNotifications.SXA.Repositories
{
    public class SubscribeButtonRepository : VariantsRepository, ISubscribeButtonRepository
    {
        public override IRenderingModelBase GetModel()
        {
            var model = new SubscribeButtonModel();
            FillBaseProperties(model);


            var result = Guid.Empty;
            Guid.TryParse(model.Item.Fields[new Sitecore.Data.ID(Fields.Goal)].Value, out result);

            model.GoalId = result;

            //Need Public Key to subscribe.
            model.PublicKey = PushNotificationConfigurationService.GetConfiguration().PublicKey;

            model.ConfigId = PushNotificationConfigurationService.GetConfigurationId();

            return model;
        }
    }
}