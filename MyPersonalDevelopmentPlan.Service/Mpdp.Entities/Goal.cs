﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpdp.Entities
{
  public class Goal : IEntityBase
  {
    public Goal()
    {
      Objectives = new List<Objective>();
    }

    public int Id { get; set; }
    public int UserProfileId { get; set; }
    public virtual UserProfile UserProfile { get; set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public string Description { get; set; }
    public decimal Progress { get; set; }

    public Status GoalStatus { get; set; }
    public Int64 EstimationTicks { get; set; }
    public Int64 RemainingEstimatesTicks { get; set; }
    public Int64 TimeLoggedTicks { get; set; }

    public virtual ICollection<Objective> Objectives { get; set; }

    [NotMapped]
    public TimeSpan Estimation
    {
      get { return TimeSpan.FromTicks(EstimationTicks); }
      set { EstimationTicks = value.Ticks; }
    }

    [NotMapped]
    public TimeSpan RemainingEstimates
    {
      get { return TimeSpan.FromTicks(RemainingEstimatesTicks); }
      set { RemainingEstimatesTicks = value.Ticks; }
    }

    [NotMapped]
    public TimeSpan TimeLogged
    {
      get { return TimeSpan.FromTicks(TimeLoggedTicks); }
      set { TimeLoggedTicks = value.Ticks; }
    }
  }
}
