using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Mpdp.Api.Infrastructure.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mpdp.Api
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      GlobalConfiguration.Configure(WebApiConfig.Register);
      Bootstrapper.Run();
      GlobalConfiguration.Configuration.EnsureInitialized();


      var formatters = GlobalConfiguration.Configuration.Formatters;
      var jsonFormatter = formatters.JsonFormatter;
      jsonFormatter.SerializerSettings.Converters.Add(new TimeSpanConverter());
    }
  }
}
