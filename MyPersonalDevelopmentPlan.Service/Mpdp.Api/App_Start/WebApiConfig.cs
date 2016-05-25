using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Mpdp.Api.Infrastructure.MessageHandlers;

namespace Mpdp.Api
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.EnableCors();
      // Web API configuration and services
      config.MessageHandlers.Add(new MpdpAuthHandler());

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
        name: "ActionApi",
        routeTemplate: "api/{controller}/{action}/{id}",
        defaults: new { id = RouteParameter.Optional, extension = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );

    }
  }
}
