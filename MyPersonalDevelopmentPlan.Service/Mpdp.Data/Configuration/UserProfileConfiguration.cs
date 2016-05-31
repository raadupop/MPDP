using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class UserProfileConfiguration : EntityBaseConfiguration<UserProfile>
  {
    public UserProfileConfiguration()
    {
      Property(u => u.Name).IsRequired().HasMaxLength(100);
      Property(u => u.Location).IsRequired().HasMaxLength(200);
      HasRequired(u => u.User).WithMany().HasForeignKey(x => x.UserId);
    }
  }
}
