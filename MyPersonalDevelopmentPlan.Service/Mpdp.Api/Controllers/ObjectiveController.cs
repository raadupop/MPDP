using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Mpdp.Api.Infrastructure.Extension;
using Mpdp.Api.Models;
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
