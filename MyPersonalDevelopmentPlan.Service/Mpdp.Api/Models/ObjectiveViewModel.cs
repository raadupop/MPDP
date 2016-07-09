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
  public class ObjectiveViewModel : IValidatableObject
  { 
    public int Id { get; set; }

    public int GoalId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal Progress { get; set; }

    public DateTime DateCreated { get; set; }

    public TimeSpan RemainingEstimates { get; set; }

    public TimeSpan Estimation { get; set; }

    public TimeSpan ExtraTime { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Rank ObjectiveRank { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Status ObjectiveStatus { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new ObjectiveViewModelValidators.ObjectiveViewModelValidator();
      var result = validator.Validate(this);
      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
    }
  }
}