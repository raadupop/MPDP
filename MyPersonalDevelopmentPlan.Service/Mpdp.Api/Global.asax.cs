using System.Web;
using System.Web.Http;
using Mpdp.Api.Infrastructure.Converters;

namespace Mpdp.Api
{
  public class WebApiApplication : HttpApplication
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
