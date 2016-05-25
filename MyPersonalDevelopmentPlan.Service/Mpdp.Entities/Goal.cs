using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class Goal : IEntityBase
  {
    public Goal()
    {
      ObjectiveLIst = new List<Objective>();
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public string Description { get; set; }
    public decimal Progress { get; set; }
    public DateTime Estimation { get; set; }
    public DateTime RemaniningEstimates { get; set; }
    public Status GoalStatus { get; set; }

    public virtual ICollection<Objective> ObjectiveLIst { get; set; }

  }
}
