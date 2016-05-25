using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Mpdp.Api.Infrastructure.Extension;
using Mpdp.Api.Models;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;

namespace Mpdp.Api.Controllers
{
  public class GoalController : ApiBaseController
  {
    private readonly IEntityBaseRepository<Goal> _goalRepository;

    public GoalController(IEntityBaseRepository<Goal> goalRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _goalRepository = goalRepository;
    }

    [HttpPost]
    public HttpResponseMessage CreateGoal(HttpRequestMessage request, GoalViewModel goal)
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
          Goal newGoal = new Goal();
          newGoal.UpdateGoal(goal);

          _goalRepository.Add(newGoal);
          _unitOfWork.Commit();

          goal = Mapper.Map<Goal, GoalViewModel>(newGoal);
          response = request.CreateResponse(HttpStatusCode.Created, goal);
        }

        return response;
      });
    }
  }
}
