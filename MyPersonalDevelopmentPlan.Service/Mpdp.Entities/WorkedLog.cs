using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpdp.Entities
{
  public class WorkedLog : IEntityBase
  {
    public int Id { get; set; }
    public int ObjectiveId { get; set; }
    public string Description { get; set; }
    public DateTime DateRecorded { get; set; }
    public long TimeWorkedTicks { get; set; }

    [NotMapped]
    public TimeSpan Duration 
    {
      get { return TimeSpan.FromTicks(TimeWorkedTicks);}
      set { TimeWorkedTicks = value.Ticks; }
    }

    public virtual Objective Objective { get; set; }
  }
}
