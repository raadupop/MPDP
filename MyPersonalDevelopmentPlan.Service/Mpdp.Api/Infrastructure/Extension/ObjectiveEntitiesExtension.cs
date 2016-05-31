using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mpdp.Api.Models;
using Mpdp.Entities;

namespace Mpdp.Api.Infrastructure.Extension
{
  public static class ObjectiveEntitiesExtension
  {
    public static void CreateObjective(this Objective objective, ObjectiveViewModel objectiveVm)
    {
      objective.Description = objectiveVm.Description;
      objective.Estimation = objectiveVm.Estimation;
      objective.ObjectiveRank = objectiveVm.ObjectiveRank;
      objective.Title = objectiveVm.Title;
      objective.DateCreated = DateTime.Now;
      objective.ExtraTime = TimeSpan.Zero;
      objective.RemainingEstimates = objectiveVm.Estimation;
      objective.ObjectiveStatus = Status.Open;
      objective.Progress = 0;
      objective.GoalId = objectiveVm.GoalId;
    }
  }
}