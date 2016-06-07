using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class WorkedLog : IEntityBase
  {
    public int Id { get; set; }
    public int ObjectiveId { get; set; }
    public string Description { get; set; }
    public DateTime LogDate { get; set; }
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
