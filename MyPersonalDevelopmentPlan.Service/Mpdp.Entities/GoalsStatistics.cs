namespace Mpdp.Entities
{
  public class GoalsStatistics
  {
    public GoalsStatistics()
    {
      GoalsCount = 0;
      CompletedGoalsCount = 0;
      OpenGoalsCount = 0;
      InProgressGoalsCount = 0;
      BlockedGoalsCount = 0;
      ReadyToBeDoneCount = 0;
      DoneGoalsCount = 0;
      StandByGoalsCount = 0;
      ClosedGoalsCount = 0;
      OpenGoalsCount = 0;
      ObjectiveInProgressCount = 0;
    }

    public int GoalsCount { get; set; }
    public int CompletedGoalsCount { get; set; }
    public int OpenGoalsCount { get; set; }
    public int InProgressGoalsCount { get; set; }
    public int BlockedGoalsCount { get; set; }
    public int ReadyToBeDoneCount { get; set; }
    public int DoneGoalsCount { get; set; }
    public int StandByGoalsCount { get; set; }
    public int ClosedGoalsCount { get; set; }
    public int ObjecivesCount { get; set; }
    public int ObjectiveInProgressCount { get; set; }
  }
}
