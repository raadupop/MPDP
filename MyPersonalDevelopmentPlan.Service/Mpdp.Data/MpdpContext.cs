using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Data.Configuration;
using Mpdp.Entities;

namespace Mpdp.Data
{
  public class MpdpContext : DbContext
  {
    public MpdpContext() : base("Mpdp")
    {
      Database.SetInitializer<MpdpContext>(null);
    }

    #region Entity Sets
    public IDbSet<User> UserSet { get; set; }
    public IDbSet<Role> RoleSet { get; set; }
    public IDbSet<UserRole> UserRoleSet { get; set; }
    public IDbSet<Goal> GoalSet { get; set; }
    public IDbSet<Objective> ObjectiveSet { get; set; }
    public IDbSet<Note> ObjeciveNote { get; set; }
    public IDbSet<WorkedLog> ObjecriveWorkedEffort { get; set; }
    public IDbSet<Error> ErrorSet { get; set; }
    #endregion

    public virtual void Commit()
    {
      base.SaveChanges();
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

      modelBuilder.Configurations.Add(new UserConfiguration());
      modelBuilder.Configurations.Add(new UserRoleConfiguration());
      modelBuilder.Configurations.Add(new RoleConfiguration());
      modelBuilder.Configurations.Add(new GoalConfiguration());
      modelBuilder.Configurations.Add(new ObjectiveConfiguration());
      modelBuilder.Configurations.Add(new WorkedLogConfiguration());
      modelBuilder.Configurations.Add(new NoteConfiguration());
    }
  }
}
