using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Data.Infrastructure
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly IDbFactory dbFactory;
    private MpdpContext dbContext;

    public UnitOfWork(IDbFactory dbFactory)
    {
      this.dbFactory = dbFactory;
    }

    public MpdpContext DbContext
    {
      get { return dbContext ?? (dbContext = dbFactory.Init()); }
    }

    public void Commit()
    {
      DbContext.Commit();
    }
  }
}
