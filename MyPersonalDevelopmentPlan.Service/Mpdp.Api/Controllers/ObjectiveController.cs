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
  public class ObjectiveController : ApiBaseController
  {
    private readonly IEntityBaseRepository<Objective> _objectiveRepository;
    private readonly IEntityBaseRepository<Goal> _goalRepository; 

    public ObjectiveController(IEntityBaseRepository<Objective> objectiveRepository, IEntityBaseRepository<Goal> goalRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _objectiveRepository = objectiveRepository;
      _goalRepository = goalRepository;

    }

    [HttpPost]
    public HttpResponseMessage CreatObjective(HttpRequestMessage request, ObjectiveViewModel objectiveVm)
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
          var goal = _goalRepository.GetSingle(objectiveVm.GoalId);

          if (goal == null)
          {
             response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid goalId");
          }
          else
          {
            Objective newObjective = new Objective();
            newObjective.CreateObjective(objectiveVm);
            newObjective.Goal = goal;

            _objectiveRepository.Add(newObjective);
            _unitOfWork.Commit();

            //Update the goal 
            goal.ObjectiveLIst.Add(newObjective);
            _goalRepository.Edit(goal);
            _unitOfWork.Commit();

            objectiveVm = Mapper.Map<Objective, ObjectiveViewModel>(newObjective);
            response = request.CreateResponse(HttpStatusCode.OK, objectiveVm);
          }        
        }

        return response;
      });
    }
  }
}
