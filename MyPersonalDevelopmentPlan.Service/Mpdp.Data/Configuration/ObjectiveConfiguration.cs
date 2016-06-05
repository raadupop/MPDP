using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class ObjectiveConfiguration : EntityBaseConfiguration<Objective>
  {
    public ObjectiveConfiguration()
    {
      Property(o => o.GoalId).IsRequired();
      Property(o => o.DateCreated).IsRequired();
      Property(o => o.Title).IsRequired();
      Property(o => o.ObjectiveRank).IsRequired();
      Property(o => o.ObjectiveStatus).IsRequired();

      HasMany(g => g.WorkedLogs).WithRequired().HasForeignKey(w => w.ObjectiveId);
    } 
  }
}
