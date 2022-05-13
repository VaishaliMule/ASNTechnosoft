using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebIMSWithJWT
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var core = new EnableCorsAttribute(origins: "https://localhost:44349/", headers: "*", methods: "*");
            config.EnableCors(core);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
