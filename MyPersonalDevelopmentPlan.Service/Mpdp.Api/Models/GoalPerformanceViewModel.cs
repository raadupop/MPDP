using System;

namespace Mpdp.Api.Models
{
  public class GoalPerformanceViewModel
  {
    public int Month { get; set; }
    public int Efficiency { get; set; }
    public int WorkedHours { get; set; }
    public TimeSpan OverTime { get; set; }
  }
}