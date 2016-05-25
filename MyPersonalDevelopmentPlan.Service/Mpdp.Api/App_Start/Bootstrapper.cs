using System.Web.Http;
using Mpdp.Api.Infrastructure.Mappings;

namespace Mpdp.Api
{
  public class Bootstrapper
  {
    public static void Run()
    {
      // Configure Autofac
      AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
      // Configure AutoMapper
      AutomapperConfiguration.Configure();
    }
  }
}