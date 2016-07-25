using System;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;
using Mpdp.Services.Abstract;

namespace Mpdp.Services
{
  public class EffortLoggingServices : IEffortLoggingServices
  {
    private readonly IEntityBaseRepository<Objective> _objectiveRepository;
    private readonly IEntityBaseRepository<WorkedLog> _workedLogRepository;
    private readonly IEntityBaseRepository<Goal> _goalRepository;
    private readonly IUnitOfWork _unitOfWork;  

    public EffortLoggingServices(IEntityBaseRepository<Objective> objectiveRepository, IEntityBaseRepository<WorkedLog> workedLogRepository, IUnitOfWork unitOfWork, IEntityBaseRepository<Goal> goalRepository)
    {
      _objectiveRepository = objectiveRepository;
      _workedLogRepository = workedLogRepository;
      _unitOfWork = unitOfWork;
      _goalRepository = goalRepository;
    }

    # region Implementation of IEffortLoggingServices

    public bool SaveWorkedLog(WorkedLog workedLog)
    {
      var objective = _objectiveRepository.GetSingle(workedLog.ObjectiveId);
      
      // Todo: Crosscutting concerns. Should track an exception handling
      var estimationTimeDifference = objective ? .RemainingEstimates.Ticks - workedLog.Duration.Ticks;

      if (estimationTimeDifference > 0)
      {
        workedLog.LogDate = workedLog.LogDate;

        _workedLogRepository.Add(workedLog);
        _unitOfWork.Commit();

        UpdateEstimation(objective, workedLog.Duration);

        return true;
      }

      return false;
    }

    public bool AddExtraEffort()
    {
      throw new NotImplementedException();
    }
    #endregion

    #region Helpers method

    private void UpdateEstimation(Objective objective, TimeSpan duration)
    {
      objective.RemainingEstimates = objective.RemainingEstimates - duration;
      var goal = _goalRepository.GetSingle(objective.GoalId);

      goal.RemainingEstimates = goal.RemainingEstimates - duration;

      _objectiveRepository.Edit(objective);
      _unitOfWork.Commit();

      _goalRepository.Edit(goal);
      _unitOfWork.Commit();
    }
    #endregion
  }
}
