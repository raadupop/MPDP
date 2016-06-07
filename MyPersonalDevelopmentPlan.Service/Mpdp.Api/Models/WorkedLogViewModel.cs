using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Results;
using Mpdp.Api.Infrastructure.Validators;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Mpdp.Api.Models
{
  public class WorkedLogViewModel : IValidatableObject
  {
    public int Id { get; set; }
    public int ObjectiveId { get; set; }
    public TimeSpan Duration { get; set; }
    public DateTime DateLog { get; set; }
    public string Description { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new WorkedLogValidator();
      var result = validator.Validate(this);
      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
    }
  }
}