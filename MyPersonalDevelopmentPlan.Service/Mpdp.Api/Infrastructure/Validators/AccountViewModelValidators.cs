using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mpdp.Api.Models;

namespace Mpdp.Api.Infrastructure.Validators
{
  public class AccountViewModelValidators
  {
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
      public RegistrationViewModelValidator()
      {
        RuleFor(r => r.Name).NotEmpty()
          .WithMessage("Invalid name");
        RuleFor(r => r.Email).NotEmpty().EmailAddress()
            .WithMessage("Invalid email address");

        RuleFor(r => r.Username).NotEmpty()
            .WithMessage("Invalid username");

        RuleFor(r => r.Password).NotEmpty()
            .WithMessage("Invalid password");
      }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
      public LoginViewModelValidator()
      {
        RuleFor(r => r.Username).NotEmpty()
            .WithMessage("Invalid username");

        RuleFor(r => r.Password).NotEmpty()
            .WithMessage("Invalid password");
      }
    }
  }
}