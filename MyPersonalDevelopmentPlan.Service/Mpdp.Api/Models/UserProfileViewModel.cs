using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Mpdp.Api.Infrastructure.Validators;

namespace Mpdp.Api.Models
{
  public class UserProfileViewModel : IValidatableObject
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var validator = new UserProfileViewModelValidaror();
      var result = validator.Validate(this);

      return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] {item.PropertyName}));
    }
  }
}