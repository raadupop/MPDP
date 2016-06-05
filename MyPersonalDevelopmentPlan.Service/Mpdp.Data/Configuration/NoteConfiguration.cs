using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class NoteConfiguration : EntityBaseConfiguration<Note>
  {
    public NoteConfiguration()
    {
      Property(n => n.Name).IsRequired();
      Property(n => n.Priority).IsRequired();
      HasRequired(n => n.UserProfile).WithMany().HasForeignKey(n => n.UserProfileId);
    }

  }
}
