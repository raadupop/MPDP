using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;

namespace Mpdp.Api.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class ApiBaseController : ApiController
  {
    protected readonly IEntityBaseRepository<Error> _errorsRepository;
    protected readonly IUnitOfWork _unitOfWork;

    public ApiBaseController(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork)
    {
      _errorsRepository = errorsRepository;
      _unitOfWork = unitOfWork;
    }

    protected HttpResponseMessage CreateHttpResponse(HttpRequestMessage request, Func<HttpResponseMessage> function)
    {
      HttpResponseMessage response = null;

      try
      {
        response = function.Invoke();
      }
      catch (DbUpdateException ex)
      {
        LogError(ex);
        response = request.CreateResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);
      }
      catch (Exception ex)
      {
        LogError(ex);
        response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
      }

      return response;
    }

    private void LogError(Exception ex)
    {
      try
      {
        Error _error = new Error()
        {
          Message = ex.Message,
          StackTrace = ex.StackTrace,
          DateCreated = DateTime.Now
        };

        _errorsRepository.Add(_error);
        _unitOfWork.Commit();
      }
      catch { }
    }
  }
}
