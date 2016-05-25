using System;
using System.Collections.Generic;
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
    public DateTime TimeWorked { get; set; }

    public virtual Objective Objective { get; set; }
  }
}
