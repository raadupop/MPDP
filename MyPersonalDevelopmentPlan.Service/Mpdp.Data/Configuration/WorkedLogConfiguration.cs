using Mpdp.Entities;

namespace Mpdp.Data.Configuration
{
  public class WorkedLogConfiguration : EntityBaseConfiguration<WorkedLog>
  {
    public WorkedLogConfiguration()
    {
      Property(w => w.ObjectiveId).IsRequired();
      Property(w => w.DateRecorded).IsRequired();
    }
  }
}
