using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Mpdp.Api.Infrastructure.Validators;
using Mpdp.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mpdp.Api.Models
{
  public class GoalViewModel : IValidatableObject
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

    public TimeSpan TimeLogged { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Status GoalStatus { get; set; }

    public int ObjectivesCount { get; set; }

    public UserProfileViewModel UserProfile { get; set; }

    public IEnumerable<ObjectiveViewModel> Objectives { get; set; }  


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new GoalViewModelValidators.GoalViewModelvalidator();
      var result = validator.Validate(this);
      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
    }
  }
}