using Mpdp.Entities;

namespace Mpdp.Services.Abstract
{
  public interface IEffortLoggingServices
  {
    bool SaveWorkedLog(WorkedLog workedLog);
    bool AddExtraEffort();

  }
}
