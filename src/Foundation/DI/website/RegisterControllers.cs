using Toast.Foundation.DI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace Toast.Foundation.DI
{
    public class RegisterControllers : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllers(
                "Toast.Feature.*");
        }
    }
}
