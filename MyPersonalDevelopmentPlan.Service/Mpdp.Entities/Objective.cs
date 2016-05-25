using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class Objective : IEntityBase
  {
    public Objective(DateTime estimation)
    {
      Estimation = estimation;
      ObjectiveNotes = new List<Note>();
      WorkedLogs = new List<WorkedLog>();
    }

    public int Id { get; set; }
    public int GoalId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Progress { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime Estimation { get; private set; }
    public DateTime RemainingEstimate { get; set; }
    public DateTime ExtraTime { get; set; }
    public Rank ObjectiveRank { get; set; }
    public Status ObjectiveStatus { get; set; }

    public virtual Goal Goal { get; set; }
    public virtual ICollection<Note> ObjectiveNotes { get; set; }
    public virtual ICollection<WorkedLog> WorkedLogs { get; set; }
     
  }
}
