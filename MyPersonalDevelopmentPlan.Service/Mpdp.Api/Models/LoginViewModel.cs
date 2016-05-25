using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Mpdp.Api.Infrastructure.Validators;
using ValidationContext = FluentValidation.ValidationContext;


namespace Mpdp.Api.Models
{
  public class LoginViewModel
  {
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new AccountViewModelValidators.LoginViewModelValidator();
      var result = validator.Validate(this);
      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
    }
  }
}