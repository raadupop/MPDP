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
using Mpdp.Services.Abstract;

namespace Mpdp.Api.Controllers
{
  public class GoalController : ApiBaseController
  {
    private readonly IEntityBaseRepository<Goal> _goalRepository;
    private readonly IEntityBaseRepository<Objective> _objectiveRepository; 
    private readonly IEntityBaseRepository<UserProfile> _userProfileRepository;

    public GoalController(IEntityBaseRepository<Objective> objectiveRepository, IEntityBaseRepository<Goal> goalRepository, IEntityBaseRepository<UserProfile> userProfileRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _goalRepository = goalRepository;
      _userProfileRepository = userProfileRepository;
      _objectiveRepository = objectiveRepository;
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
          
          newGoal.CreateGoal(goal);

          //Asign goal to a user
          var userProfile = _userProfileRepository.GetSingle(goal.UserProfileId);
          newGoal.UserProfile = userProfile;

          _goalRepository.Add(newGoal);
          _unitOfWork.Commit();

          goal = Mapper.Map<Goal, GoalViewModel>(newGoal);
          response = request.CreateResponse(HttpStatusCode.Created, goal);
        }

        return response;
      });
    }

    [HttpGet]
    [Route("")]
    public HttpResponseMessage GetGoals(HttpRequestMessage request, int userId, DateTime? startDate, DateTime? endDate)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response;
        
          var userProfile = _userProfileRepository.GetSingle(userId);
          if (userProfile == null)
          {
            response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid user profile id");
          }
          else
          {
            var userGoals = _goalRepository.GetAll().Where(g => g.UserProfile.Id == userId && g.DateCreated > startDate && g.DateCreated < endDate);
            IEnumerable<GoalViewModel> goalsVm = Mapper.Map<IEnumerable<Goal>, IEnumerable<GoalViewModel>>(userGoals);

            var goalViewModels = goalsVm as IList<GoalViewModel> ?? goalsVm.ToList();
            response = request.CreateResponse(HttpStatusCode.OK, new { goals = goalViewModels, goalsCount = goalViewModels.Count() });
          }
   
        return response;
      });
    }

    //todo security improvements about the cross user update;
    //todo throw 404 when goal isn't specified correct
    [HttpPut]
    [Route("update")]
    public HttpResponseMessage Update(HttpRequestMessage request, GoalViewModel goal)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response;

        if (!ModelState.IsValid)
        {
          response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid goal specification");
        }
        else
        {
          Goal goalToUpdate = new Goal();
          goalToUpdate.UpdateGoal(goal);

          //todo (this is temporary), change the business for the user mapping problem in userProfile
          var userProfile = _userProfileRepository.GetSingle(goal.UserProfileId);
          goalToUpdate.UserProfile = userProfile;

          _goalRepository.Edit(goalToUpdate);
          _unitOfWork.Commit();

          GoalViewModel goalUpdated = Mapper.Map<Goal, GoalViewModel>(goalToUpdate);
          response = request.CreateResponse(HttpStatusCode.Created, goalUpdated);
        }

        return response;
      });
    }

    [HttpPost]
    [Route ("createobjective")]
    public HttpResponseMessage CreateObjective(HttpRequestMessage request, ObjectiveViewModel objectiveVm)
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
            goal.Objectives.Add(newObjective);
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
