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
