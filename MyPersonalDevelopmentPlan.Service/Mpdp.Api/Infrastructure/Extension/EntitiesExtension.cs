using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mpdp.Api.Models;
using Mpdp.Entities;

namespace Mpdp.Api.Infrastructure.Extension
{
  public static class EntitiesExtension
  {
    public static void UpdateGoal(this Goal goal, GoalViewModel goalVm)
    {
      goal.Name = goalVm.Name;
      goal.UserId = goalVm.UserId;
      goal.User = goalVm.User;
      goal.Description = goalVm.Description;
      goal.Estimation = goalVm.Estimation;
      goal.GoalStatus = goalVm.GoalStatus != Status.Open ? goalVm.GoalStatus : Status.Open;
      goal.Progress = goalVm.Progress != Decimal.MinValue ? goalVm.Progress : Decimal.MinValue;
      goal.RemaniningEstimates = goalVm.RemaniningEstimates != goalVm.Estimation ? goalVm.RemaniningEstimates : goalVm.Estimation;
      goal.DateCreated = goalVm.DateCreated == null ? DateTime.Now : goalVm.DateCreated;
    }
  }
}