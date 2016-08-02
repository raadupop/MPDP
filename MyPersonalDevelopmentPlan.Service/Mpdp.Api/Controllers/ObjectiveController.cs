using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;

namespace Mpdp.Api.Controllers
{
  public class ObjectiveController : ApiBaseController
  {
    private readonly IEntityBaseRepository<Objective> _objectiveRepository;
    private readonly IEntityBaseRepository<Goal> _goalRepository; 

    public ObjectiveController(IEntityBaseRepository<Objective> objectiveRepository, IEntityBaseRepository<Goal> goalRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _objectiveRepository = objectiveRepository;
      _goalRepository = goalRepository;

    }

  }
}
