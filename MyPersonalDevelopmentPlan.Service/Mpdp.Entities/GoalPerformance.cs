﻿using System;

namespace Mpdp.Entities
{
  public class GoalPerformance
  {
    public int Month { get; set; }
    public int Eficency { get; set; }
    public int WorkedHours { get; set; }
    public TimeSpan OverTime { get; set; }
  }
}
