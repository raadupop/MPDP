using System.Net.Http;
using System.Web.Http.Dependencies;
using Mpdp.Services.Abstract;

namespace Mpdp.Api.Infrastructure.Extension
{
  public static class RequestMessageExtensions
  {
    internal static IMembershipServices GetMembershipService(this HttpRequestMessage request)
    {
      return request.GetService<IMembershipServices>();
    }

    private static TService GetService<TService>(this HttpRequestMessage request)
    {
      IDependencyScope dependencyScope = request.GetDependencyScope();
      TService service = (TService)dependencyScope.GetService(typeof(TService));

      return service;
    }
  }
}