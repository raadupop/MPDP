using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class GoalsClosed : IEntityBase
  {
    public GoalsClosed()
    {
      Goals = new List<Goal>();
    }
    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Goal> Goals { get; set; } 
  }
}
