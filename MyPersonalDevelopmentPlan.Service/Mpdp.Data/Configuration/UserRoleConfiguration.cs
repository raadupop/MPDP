using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class UserRoleConfiguration : EntityBaseConfiguration<UserRole>
  {
    public UserRoleConfiguration()
    {
      Property(u => u.RoleId).IsRequired();
      Property(u => u.UserId).IsRequired();
    }
  }
}
