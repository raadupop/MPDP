using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Results;
using Mpdp.Api.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;
using Mpdp.Entities;
using ValidationContext = FluentValidation.ValidationContext;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Mpdp.Api.Models
{
  public class GoalViewModel
  {
    public GoalViewModel(IEnumerable<Objective> objectiveLIst)
    {
      ObjectiveLIst = objectiveLIst;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public string Description { get; set; }
    public decimal Progress { get; set; }
    public DateTime Estimation { get; set; }
    public DateTime RemaniningEstimates { get; set; }
    public Status GoalStatus { get; set; }

    public int ObjectivesCount { get; set; }
    public virtual IEnumerable<Objective> ObjectiveLIst { get; private set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new GoalViewModelValidators.GoalViewModelvalidator();
      var result = validator.Validate(this);
      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] {item.PropertyName }));
    }
  }
}