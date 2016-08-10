using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Mpdp.Api.Models;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;
using Mpdp.Services.Abstract;

namespace Mpdp.Api.Controllers
{
  public class AnalyticsController : ApiBaseController
  {
    private readonly IEntityBaseRepository<Goal> _goalRepository;
    private readonly IAnalyticsServices _analyticsServices;

    public AnalyticsController(IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork,
      IEntityBaseRepository<Goal> goalRepository, IAnalyticsServices analyticsServices) : base(errorsRepository, unitOfWork)
    {
      _goalRepository = goalRepository;
      _analyticsServices = analyticsServices;
    }

    [HttpGet]
    public HttpResponseMessage GetGoalsStatistics(HttpRequestMessage request, int userProfileId)
    {
      return CreateHttpResponse(request, () =>
      {
        var goals = _goalRepository.All.Where(g => g.DateCreated.Year.Equals(DateTime.Now.Year) && g.UserProfileId == userProfileId).ToList();
        var statistics = _analyticsServices.GetGoalsStatistics(goals);

        var statisticsViewModel = Mapper.Map<GoalsStatistics, GoalsStatisticsViewModel>(statistics);
        var response = request.CreateResponse(HttpStatusCode.OK, statisticsViewModel);

        return response;
      });
    }

    [HttpGet]
    public HttpResponseMessage GetGoalsPerformance(HttpRequestMessage request, int userProfileId)
    {
      return CreateHttpResponse(request, () =>
      {
        var goals = _goalRepository.All.Where(g => g.DateCreated.Year.Equals(DateTime.Now.Year) && g.UserProfileId == userProfileId).ToList();
        var goalsPerformance = _analyticsServices.GetGoalsPerformanceByMonth(goals);

        IEnumerable<GoalPerformanceViewModel> statisticsViewModel = Mapper.Map<IEnumerable<GoalPerformance>, IEnumerable<GoalPerformanceViewModel>>(goalsPerformance);
        var response = request.CreateResponse(HttpStatusCode.OK, statisticsViewModel);

        return response;
      });
    }
  }
}
