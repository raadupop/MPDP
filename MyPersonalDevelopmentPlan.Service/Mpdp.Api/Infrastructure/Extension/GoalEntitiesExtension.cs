using System;
using Mpdp.Api.Models;
using Mpdp.Entities;

namespace Mpdp.Api.Infrastructure.Extension
{
  public static class GoalEntitiesExtension
  {
    public static void UpdateGoal(this Goal goal, GoalViewModel goalVm)
    {
      goal.Id = goalVm.Id;
      goal.Name = goalVm.Name;
      goal.Description = goalVm.Description;
      goal.Estimation = goalVm.Estimation;
      goal.GoalStatus = goalVm.GoalStatus;
      goal.Progress = goalVm.Progress;
      goal.RemainingEstimates = goalVm.Estimation;
      goal.DateCreated = goalVm.DateCreated;
      goal.UserProfileId = goalVm.UserProfileId;
      //todo: goal.UserProfile = goalVm.UserProfile
    }

    public static void CreateGoal(this Goal goal, GoalViewModel goalVm)
    {
      goal.Name = goalVm.Name;
      goal.Description = goalVm.Description;
      goal.Estimation = goalVm.Estimation;
      goal.GoalStatus = Status.Open;
      goal.Progress = 0;
      goal.TimeLogged = TimeSpan.Zero;
      goal.RemainingEstimates = goalVm.Estimation;
      goal.DateCreated = DateTime.Now;
    }
  }
}