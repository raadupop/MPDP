using System.Data.Entity.ModelConfiguration;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T : class, IEntityBase
  {
    public EntityBaseConfiguration()
    {
      HasKey(e => e.Id);
    }
  }
}
