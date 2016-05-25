using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public enum Status
  {
    Open = 1,
    Blocked = 2,
    InProgress = 3,
    ReadyToBeDone = 4,
    Done = 5,
    Closed = 6,
    StandBy = 7
  }
}
