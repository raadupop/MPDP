using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class ObjectivesClosed : IEntityBase
  {
    public ObjectivesClosed() 
    {
      Objectives = new List<Objective>();
    }

    public int Id { get; set; }
    public virtual List<Objective> Objectives { get; set; } 
  }
}
