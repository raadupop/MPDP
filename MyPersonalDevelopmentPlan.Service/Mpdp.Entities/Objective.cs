using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class Objective : IEntityBase
  {
    public Objective()
    {
      WorkedLogs = new List<WorkedLog>();
    }

    public int Id { get; set; }
    public int GoalId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Progress { get; set; }
    public DateTime DateCreated { get; set; }
    public Int64 EstimationTicks { get; set; }
    public Int64 RemainingEstimatesTicks { get; set; }
    public  Int64 ExtraTimeTicks { get; set; }
    public Rank ObjectiveRank { get; set; }
    public Status ObjectiveStatus { get; set; }

    public virtual Goal Goal { get; set; }
    public virtual ICollection<WorkedLog> WorkedLogs { get; set; }

    [NotMapped]
    public TimeSpan RemainingEstimates
    {
      get { return TimeSpan.FromTicks(RemainingEstimatesTicks); }
      set { RemainingEstimatesTicks = value.Ticks; }
    }

    [NotMapped]
    public TimeSpan Estimation
    {
      get { return TimeSpan.FromTicks(EstimationTicks); }
      set { EstimationTicks = value.Ticks; }
    }

    [NotMapped]
    public TimeSpan ExtraTime
    {
      get { return TimeSpan.FromTicks(ExtraTimeTicks); }
      set { ExtraTimeTicks = value.Ticks; }
    }

  }
}
