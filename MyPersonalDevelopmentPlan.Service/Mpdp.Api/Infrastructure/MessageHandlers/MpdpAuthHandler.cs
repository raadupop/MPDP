using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Mpdp.Api.Infrastructure.Extension;

namespace Mpdp.Api.Infrastructure.MessageHandlers
{
  public class MpdpAuthHandler : DelegatingHandler
  {
    private IEnumerable<string> authHeadervalues = null;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
    {
      try
      {
        request.Headers.TryGetValues("Authorization", out authHeadervalues);
        if (authHeadervalues == null)
        {
          return base.SendAsync(request, token); // cros fingers
        }

        var tokens = authHeadervalues.FirstOrDefault();
        tokens = tokens.Replace("Basic", "").Trim();
        if (!string.IsNullOrEmpty(tokens))
        {
          byte[] data = Convert.FromBase64String(tokens);
          string decodedSting = Encoding.UTF8.GetString(data);
          string[] tokenValues = decodedSting.Split(':');
          var membershipService = request.GetMembershipService();

          var membershipCtx = membershipService.ValidateUser(tokenValues[0], tokenValues[1]);
          if (membershipCtx.User != null)
          {
            IPrincipal principal = membershipCtx.Principal;
            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
          }
          else // Unauthorized access 
          {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var t = new TaskCompletionSource<HttpResponseMessage>();
            t.SetResult(response);
            return t.Task;
          }
        }
        else
        {
          var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
          var t = new TaskCompletionSource<HttpResponseMessage>();
          t.SetResult(response);
          return t.Task;
        }
        return base.SendAsync(request, token);
      }
      catch
      {
        var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
        var tsc = new TaskCompletionSource<HttpResponseMessage>();
        tsc.SetResult(response);
        return tsc.Task;
      }
    }

  }
}