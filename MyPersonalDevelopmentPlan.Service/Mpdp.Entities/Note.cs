using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class Note : IEntityBase
  {
    public int Id { get; set; }
    public int ObjectiveId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public virtual Objective Objective { get; set; }
  }
}
