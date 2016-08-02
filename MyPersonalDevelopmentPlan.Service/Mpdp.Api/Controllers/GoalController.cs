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
  //todo: Separate goal feature to the objective 
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

    [HttpDelete]
    public HttpResponseMessage DeleteGoal(HttpRequestMessage request, int id)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response;

        Goal goalToDelete = _goalRepository.GetSingle(id);

        if (goalToDelete == null)
        {
          response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid goal");
        }
        else
        {
          foreach (var g in goalToDelete.Objectives.ToList())
          {
            _objectiveRepository.Delete(g);
          }

          _unitOfWork.Commit();

          _goalRepository.Delete(goalToDelete);
          _unitOfWork.Commit();

          response = request.CreateResponse(HttpStatusCode.OK, "The goal was successfully deleted");
        }
        return response;
      });
    }

    [HttpDelete]
    public HttpResponseMessage DeleteObjective(HttpRequestMessage request, int id)
    {
      return CreateHttpResponse(Request, () =>
      {
        HttpResponseMessage respones;

        var objective = _objectiveRepository.GetSingle(id);

        if (objective == null)
        {
          respones = request.CreateErrorResponse(HttpStatusCode.NotFound, "The objective was not found");
        }
        else
        {
          // todo improve me 
          //var goal = _goalRepository.GetAll().FirstOrDefault(g => g.Objectives.Contains(objective));

          //if (goal != null)
          //{
          //  foreach (var goalObjective in goal.Objectives)
          //  {
          //    if (goalObjective.Id == objectiveId)
          //    {
          //      _goalRepository.Delete().Delete(goalObjective);
          //    }
          //  }
          _objectiveRepository.Delete(objective);
          _unitOfWork.Commit();

          respones = request.CreateResponse(HttpStatusCode.NoContent, "The objective was successfully deleted");
        }
        return respones;
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

    [HttpGet]
    public HttpResponseMessage GetGoal(HttpRequestMessage request, int goalId)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage respone;

        var goal = _goalRepository.GetSingle(goalId);

        if (goal == null)
        {
          respone = request.CreateErrorResponse(HttpStatusCode.NotFound, "The goal was not found");
        }
        else
        {
          GoalViewModel goalVm = Mapper.Map<Goal, GoalViewModel>(goal);

          respone = request.CreateResponse(HttpStatusCode.OK, goalVm);
        }

        return respone;
      });
    }

    [HttpGet]
    public HttpResponseMessage GetObjectives(HttpRequestMessage request, int goalId)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage respone;

        var objectives = _goalRepository.GetSingle(goalId).Objectives;

        if (objectives == null)
        {
          respone = request.CreateErrorResponse(HttpStatusCode.NotFound, "The goal was not found");
        }
        else
        {
          IEnumerable<ObjectiveViewModel> objecitvesVm = Mapper.Map<IEnumerable<Objective>, IEnumerable<ObjectiveViewModel>>(objectives);

          respone = request.CreateResponse(HttpStatusCode.OK, objecitvesVm);
        }

        return respone;
      });
    }

    //todo: security improvements about the cross user update;
    //todo: throw 404 when goal isn't specified correct
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

          //todo: (this is temporary), change the business for the user mapping problem in UserProfile
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

    //todo: wrap the business logic into a new services
    [HttpPost]
    [Route("createobjective")]
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
            response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid goalId provided");
          }
          else if (objectiveVm.Estimation > goal.RemainingEstimates)
          {
            response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "You don't have physical time to accomplish this objective according to the goal remaining estimates. Please add an extra effort time to your goal.");
          }
          else
          {
            TimeSpan totalObjectiveEstimate = goal.Objectives.Aggregate(TimeSpan.Zero, (current, objective) => current + objective.Estimation);

            if (objectiveVm.Estimation > (goal.Estimation - totalObjectiveEstimate))
            {
              response = request.CreateErrorResponse(HttpStatusCode.BadRequest, "You don't have physical time to accomplish this objective according to the goal remaining estimates. Please add an extra effort time to your goal.");
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
        }

        return response;
      });
    }

    [HttpPut]
    [Route("updateobjective")]
    public HttpResponseMessage UpdateObjective(HttpRequestMessage request, ObjectiveViewModel objectiveVm)
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
          var objective = Mapper.Map<ObjectiveViewModel, Objective>(objectiveVm);

          _objectiveRepository.Edit(objective);
          _unitOfWork.Commit();

          response = request.CreateResponse(HttpStatusCode.OK, "Goal was updated with success");
        }

        return response;
      });
    }

    [HttpPut]
    public HttpResponseMessage AddExtraTime(HttpRequestMessage reqeuest, [FromUri] int goalId, [FromUri] TimeSpan time)
    {
      return CreateHttpResponse(reqeuest, () =>
      {
        HttpResponseMessage respone;

        Goal goal = _goalRepository.GetSingle(goalId);

        if (goal == null)
        {
          respone = reqeuest.CreateErrorResponse(HttpStatusCode.NotFound, "Goal was not found");
        }
        else
        {
          goal.Estimation += time;
          goal.RemainingEstimates += time;

          _goalRepository.Edit(goal);
          _unitOfWork.Commit();

          respone = reqeuest.CreateResponse(HttpStatusCode.NoContent, "Time was added with success");
        }

        return respone;
      });
    }
  }
}
