using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class WorkedLogConfiguration : EntityBaseConfiguration<WorkedLog>
  {
    public WorkedLogConfiguration()
    {
      Property(w => w.ObjectiveId).IsRequired();
      Property(w => w.LogDate).IsRequired();
    }
  }
}
