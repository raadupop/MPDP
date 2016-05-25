using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  class RoleConfiguration : EntityBaseConfiguration<Role>
  {
    public RoleConfiguration()
    {
      Property(ur => ur.Name).IsRequired().HasMaxLength(50);
    }
  }
}
