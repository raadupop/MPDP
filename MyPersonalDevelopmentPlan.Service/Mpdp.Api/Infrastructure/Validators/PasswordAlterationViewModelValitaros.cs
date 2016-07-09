using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mpdp.Api.Models;

namespace Mpdp.Api.Infrastructure.Validators
{
  public class PasswordAlterationViewModelValitaros 
  {
    public class PasswordAlterationViewModelValidator : AbstractValidator<PasswordAlterationViewModel>
    {

      public PasswordAlterationViewModelValidator()
      {
        RuleFor(o => o.Username).NotEmpty()
          .WithMessage("Username should not be empty");
        RuleFor(p => p.CurrentPassword).NotEmpty()
          .WithMessage("Current password should not be empty");
        RuleFor(p => p.NewPassword).NotEmpty()
          .WithMessage("New password should not be empty");
      }
      
    }
  }
}