using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class GoalConfiguration : EntityBaseConfiguration<Goal>
  {
    public GoalConfiguration()
    {
      Property(g => g.UserProfileId).IsRequired();
      Property(g => g.GoalStatus).IsRequired();
      Property(g => g.Name).IsRequired();

      HasMany(g => g.Objectives).WithRequired().HasForeignKey(o => o.GoalId);
    }
  }
}
