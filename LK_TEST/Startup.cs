using Owin;
using System.Web.Http;

namespace LK_TEST
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "import.json",
                defaults: new
                {
                    controller = "SyncProfileRequest",
                    action = "Put"
                }
            );

            appBuilder.UseWebApi(config);
        }
    }
}
