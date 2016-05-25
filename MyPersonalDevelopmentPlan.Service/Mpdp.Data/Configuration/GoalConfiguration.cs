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
      Property(g => g.UserId).IsRequired();
      Property(g => g.GoalStatus).IsRequired();
      Property(g => g.DateCreated).IsRequired();
      Property(g => g.Estimation).IsRequired();
      Property(g => g.Name).IsRequired();

      HasMany(g => g.ObjectiveLIst).WithRequired().HasForeignKey(o => o.GoalId);
    }
  }
}
