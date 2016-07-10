using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mpdp.Api.Models;

namespace Mpdp.Api.Infrastructure.Validators
{
  public class UserProfileViewModelValidaror : AbstractValidator<UserProfileViewModel>
  {
    public UserProfileViewModelValidaror()
    {
      RuleFor(u => u.Email).EmailAddress()
        .WithMessage("Invalid email address format provided");
      RuleFor(u => u.Name).NotEmpty()
        .WithMessage("The name should not be empty");
    }
  }
}