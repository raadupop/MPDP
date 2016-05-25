using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Data.Infrastructure
{
  public class DbFactory : Disposable, IDbFactory
  {
    MpdpContext dbContext;

    public MpdpContext Init()
    {
      return dbContext ?? (dbContext = new MpdpContext());
    }

    protected override void DisposeCore()
    {
      if (dbContext != null)
        dbContext.Dispose();
    }
  }
}
