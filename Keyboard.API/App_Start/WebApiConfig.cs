using System.Web.Http;

namespace Keyboard.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.LocalOnly;
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                "GetKeyboardPath",
                "api/keyboard/GetKeyboardPath",
                defaults: new { Controller = "Keyboard", Action = "GetKeyboardPath" }
            );

            config.Routes.MapHttpRoute(
                "GetKeyboard",
                "api/keyboard/GetKeyboard",
                defaults: new { Controller = "Keyboard", Action = "GetKeyboard" }
            );

        }
    }
}
