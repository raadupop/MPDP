using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Mpdp.Api.Models;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;
using Mpdp.Services.Abstract;

namespace Mpdp.Api.Controllers
{
  public class EffortLoggingController : ApiBaseController
  {
    private readonly IEffortLoggingServices _effortLoggingServices; 

    public EffortLoggingController(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork, IEntityBaseRepository<Objective> objectiveRepository, IEntityBaseRepository<WorkedLog> workedLogRepository, IEffortLoggingServices effortLoggingServices) : base(errorsRepository, unitOfWork)
    {
      _effortLoggingServices = effortLoggingServices;
    }

    [HttpPost]
    [Route("addWorkedLog")]
    public HttpResponseMessage AddWorkedLog(HttpRequestMessage request, WorkedLogViewModel workedLogViewModel)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response;

        if (!ModelState.IsValid)
        {
          response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
          var workedLog = Mapper.Map<WorkedLogViewModel, WorkedLog>(workedLogViewModel);

          if (!_effortLoggingServices.SaveWorkedLog(workedLog))
          {
            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Goal could not be found");
          }
          else
          {
            response = request.CreateResponse(HttpStatusCode.OK, "Your work time was added with success");
          }
        }

        return response;
      });
    }
  }
}
