using System.Collections.Generic;

namespace Mpdp.Api.Models
{
  public class GoalsStatisticsViewModel
  {
    public int GoalsCount { get; set; }
    public int CompletedGoalsCount { get; set; }
    public int OpenGoalsCount { get; set; }
    public int InProgressGoalsCount { get; set; }
    public int BlockedGoalsCount { get; set; }
    public int ReadyToBeDoneCount { get; set; }
    public int DoneGoalsCount { get; set; }
    public int StandByGoalsCount { get; set; }
    public int ClosedGoalsCount { get; set; }
    public int ObjectivesCount { get; set; }
    public int ObjectivesInProgressCount { get; set; }
  }
}