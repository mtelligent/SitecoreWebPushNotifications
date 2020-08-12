using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.DependencyInjection;
using Sitecore.XA.Foundation.Multisite;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
using SF.Foundation.PushNotifications.Models;

namespace SF.Foundation.PushNotifications.Services
{
    public class PushNotificationConfigurationService
    {
        public static PushNotificationsConfiguration GetConfiguration()
        {
            var multiSiteContext = ServiceLocator.ServiceProvider.GetService<IMultisiteContext>();
            if (multiSiteContext == null || Sitecore.Context.Site == null || string.IsNullOrEmpty(Sitecore.Context.Site.StartPath))
            {
                return null;
            }

            var settingsItem = multiSiteContext.GetSettingsItem(Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath));
            if (settingsItem == null)
            {
                return null;
            }

            var configItem = settingsItem.FirstChildInheritingFrom(new Sitecore.Data.ID(SF.Foundation.PushNotifications.Constants.TemplateIds.PushNotficationsConfiguration));
            if (configItem == null)
            {
                return null;
            }

            return GetConfiguration(configItem);
        }

        public static PushNotificationsConfiguration GetConfiguration(Item configItem)
        {
            
            var config = new PushNotificationsConfiguration();
            config.MailTo = configItem.Fields[SF.Foundation.PushNotifications.Constants.FieldIds.MailTo].Value;
            config.PrivateKey = configItem.Fields[SF.Foundation.PushNotifications.Constants.FieldIds.PrivateKey].Value;
            config.PublicKey = configItem.Fields[SF.Foundation.PushNotifications.Constants.FieldIds.PublicKey].Value;
            config.GoogleAPIKey = configItem.Fields[SF.Foundation.PushNotifications.Constants.FieldIds.GoogleAPIKey].Value;

            return config;
        }
    }
}