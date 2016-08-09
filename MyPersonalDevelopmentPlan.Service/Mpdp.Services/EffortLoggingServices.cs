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

      if (objective != null)
      {
        workedLog.DateRecorded = workedLog.DateRecorded;

        _workedLogRepository.Add(workedLog);
        _unitOfWork.Commit();

        UpdateEstimation(objective, workedLog.Duration);
      }

      return true;
    }

    public bool AddExtraEffort()
    {
      throw new NotImplementedException();
    }
    #endregion

    #region Helpers method

    private void UpdateEstimation(Objective objective, TimeSpan workedTime)
    {
      var objectiveDifference = objective.RemainingEstimates - workedTime;

      objective.RemainingEstimates = objectiveDifference < TimeSpan.Zero ? TimeSpan.Zero : objectiveDifference;

      _objectiveRepository.Edit(objective);
      _unitOfWork.Commit();

      var goal = _goalRepository.GetSingle(objective.GoalId);

      var goaldDifference = goal.RemainingEstimates - workedTime;

      goal.TimeLogged += workedTime;
      goal.RemainingEstimates = goaldDifference < TimeSpan.Zero ? TimeSpan.Zero : goaldDifference;
      
      _goalRepository.Edit(goal);
      _unitOfWork.Commit();
    }
    #endregion
  }
}
