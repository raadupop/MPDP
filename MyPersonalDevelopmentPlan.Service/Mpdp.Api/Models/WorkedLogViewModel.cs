using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Mpdp.Api.Infrastructure.Validators;

namespace Mpdp.Api.Models
{
  public class WorkedLogViewModel : IValidatableObject
  {
    public int Id { get; set; }
    public int ObjectiveId { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime DateRecorded { get; set; }
    public string Description { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new WorkedLogValidator();
      var result = validator.Validate(this);
      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
    }
  }
}