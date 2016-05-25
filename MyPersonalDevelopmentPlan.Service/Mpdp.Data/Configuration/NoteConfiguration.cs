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
      Property(n => n.ObjectiveId).IsRequired();
      Property(n => n.Title).IsRequired();
      Property(n => n.Content).IsRequired();
    }

  }
}
