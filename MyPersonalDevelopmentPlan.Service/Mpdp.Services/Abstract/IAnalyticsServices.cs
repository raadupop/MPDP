using System.Collections.Generic;
using Mpdp.Entities;

namespace Mpdp.Services.Abstract
{
  public interface IAnalyticsServices
  {
    GoalPerformance GetGoalEfficiency(Goal goal, int month);

    GoalsStatistics GetGoalsStatistics(List<Goal> goals);

    List<GoalPerformance> GetGoalsPerformanceByMonth(List<Goal> goals);
  }
}
