using Sitecore.Pipelines;
using System.Web.Http;

namespace SF.Feature.PushNotifications.Controllers
{
    public class RegisterRoutes
    {
        public void Process(PipelineArgs args)
        {
            GlobalConfiguration.Configure(this.Configure);
        }

        protected void Configure(HttpConfiguration configuration)
        {
            MapRouteWithSession(configuration, "SF.PushNotifications.Subscribe", "api/sf/1.0/pushNotification/subscribe", "PushNotifications", "Subscribe");
        }

        protected static void MapRouteWithSession(HttpConfiguration configuration, string routeName, string routePath, string controller, string action)
        {
            var routes = configuration.Routes;
            routes.MapHttpRoute(routeName, routePath, new
            {
                controller = controller,
                action = action
            });

            var route = System.Web.Routing.RouteTable.Routes[routeName] as System.Web.Routing.Route;
            route.RouteHandler = new SessionRouteHandler();
        }
    }
}
