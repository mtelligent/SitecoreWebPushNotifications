using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using SF.Feature.PushNotifications.SXA.Controllers;
using Sitecore.DependencyInjection;

namespace SF.Feature.PushNotifications.SXA.Repositories
{
    public class RegisterDependencies : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            RegisterRepositories(serviceCollection);
            RegisterControllers(serviceCollection);
        }

        private void RegisterRepositories(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISubscribeButtonRepository, SubscribeButtonRepository>();

        }

        private void RegisterControllers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<SubscribeButtonController>();

        }
    }
}