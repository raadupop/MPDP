using System;
using System.Collections.Generic;
using Mpdp.Entities;
using Mpdp.Services.Abstract;
using static System.Decimal;

namespace Mpdp.Services
{
  /// <remarks>
  /// For this moment the analytics business is generated dynamically. 
  /// </remarks>> 
  /// TODO: Hold the performance and statistics fields on the db to improve the performances
  public class AnalyticsServices : IAnalyticsServices
  {
    public GoalPerformance GetGoalEfficiency(Goal goal, int month)
    {
      var originarEstimates = goal.Estimation.Ticks;
      var totalTimeWorked = goal.TimeLogged.Ticks;

      var efficiency = Divide(originarEstimates, totalTimeWorked)*100;
      var overTime = totalTimeWorked - originarEstimates;

      return new GoalPerformance()
      {
        Eficency = (int) efficiency,
        Month = month,
        WorkedHours = (int) goal.TimeLogged.TotalHours,
        OverTime = TimeSpan.FromTicks(overTime)
      };
    }

    public List<GoalPerformance> GetGoalsPerformanceByMonth(List<Goal> goals)
    {
      var goalsPerformanceByMonth = new List<GoalPerformance>();
      var date = new DateTime(2016, 01, 01);

      for (var m = date.Month; m <= 12; m++)
      {
        var averageGoalsByMonthPerformance = new GoalPerformance() { Month = m, Eficency = 0, OverTime = TimeSpan.Zero, WorkedHours = 0 };
        var numberOfGoals = 0;

        foreach (var goal in goals)
        {  
          if (goal.DateCreated.Month == m && goal.GoalStatus == Status.Closed)
          {
            var goalEfficiency = GetGoalEfficiency(goal, m);

            numberOfGoals++;
            averageGoalsByMonthPerformance.Eficency += goalEfficiency.Eficency;
            averageGoalsByMonthPerformance.OverTime += goalEfficiency.OverTime;
            averageGoalsByMonthPerformance.WorkedHours += goalEfficiency.WorkedHours;
          }
        }
        averageGoalsByMonthPerformance.Eficency = (numberOfGoals > 0) ? (int) Divide(averageGoalsByMonthPerformance.Eficency, numberOfGoals) : 0;
        averageGoalsByMonthPerformance.OverTime = (averageGoalsByMonthPerformance.OverTime < TimeSpan.Zero) ? TimeSpan.Zero : averageGoalsByMonthPerformance.OverTime;
        goalsPerformanceByMonth.Add(averageGoalsByMonthPerformance);
      }

      return goalsPerformanceByMonth;
    }

    public GoalsStatistics GetGoalsStatistics(List<Goal> goals)
    {
      var statistics = new GoalsStatistics() {GoalsCount = goals.Count};

      foreach (var goal in goals)
      {
        switch (goal.GoalStatus)
        {
          case Status.Blocked:
            statistics.BlockedGoalsCount++;
            break;
          case Status.Open:
            statistics.OpenGoalsCount++;
            break;
          case Status.InProgress:
            statistics.InProgressGoalsCount++;
            break;
          case Status.ReadyToBeDone:
            statistics.ReadyToBeDoneCount++;
            break;
          case Status.Done:
            statistics.DoneGoalsCount++;
            break;
          case Status.Closed:
            statistics.ClosedGoalsCount++;
            break;
          case Status.StandBy:
            statistics.StandByGoalsCount++;
            break;
        }
      }

      return statistics;
    }
  }
}
