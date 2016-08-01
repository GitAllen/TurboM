using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Azure.CAT.Migration.Web.Telemetry;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace Microsoft.Azure.CAT.Migration.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Register unity dependency resolver for Web API
            IUnityContainer container = UnityConfig.GetConfiguredContainer();
            config.DependencyResolver = new UnityDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Services.Add(typeof(IExceptionLogger), new AppInsightsExceptionLogger());
        }
    }
}
