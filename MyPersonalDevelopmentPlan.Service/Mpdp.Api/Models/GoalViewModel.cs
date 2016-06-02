using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Results;
using Mpdp.Api.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;
using Mpdp.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ValidationContext = FluentValidation.ValidationContext;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Mpdp.Api.Models
{
  public class GoalViewModel
  {
    public int Id { get; set; }

    public int UserProfileId { get; set; }

    public string Username { get; set; }

    public string Name { get; set; }

    public DateTime DateCreated { get; set; }

    public string Description { get; set; }

    public decimal Progress { get; set; }

    public TimeSpan Estimation { get; set; }

    public TimeSpan RemainingEstimates { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Status GoalStatus { get; set; }

    public int ObjectivesCount { get; set; }

    public UserProfileViewModel UserProfile { get; set; }

    public IEnumerable<ObjectiveViewModel> Objectives { get; set; }  


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new GoalViewModelValidators.GoalViewModelvalidator();
      var result = validator.Validate(this);
      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] {item.PropertyName }));
    }
  }
}