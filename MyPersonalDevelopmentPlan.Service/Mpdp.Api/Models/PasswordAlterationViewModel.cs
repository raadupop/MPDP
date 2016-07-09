using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using Mpdp.Api.Infrastructure.Validators;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Mpdp.Api.Models
{
  public class PasswordAlterationViewModel : IValidatableObject
  {
    public string Username { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new PasswordAlterationViewModelValitaros.PasswordAlterationViewModelValidator();
      var result = validator.Validate(this);

      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] {item.PropertyName}));
    }
  }
}