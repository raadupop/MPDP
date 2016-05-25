using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mpdp.Api.Models;

namespace Mpdp.Api.Infrastructure.Validators
{
  public class GoalViewModelValidators 
  {
    public class GoalViewModelvalidator : AbstractValidator<GoalViewModel>
    {
      public GoalViewModelvalidator()
      {
        RuleFor(g => g.Name).NotEmpty()
          .WithMessage("Invalid name");
        RuleFor(g => g.Description).NotEmpty()
          .WithMessage("Invalid description");
        RuleFor(g => g.Estimation).NotEmpty()
          .WithMessage("Invalid estimation");
        RuleFor(g => g.GoalStatus).NotEmpty()
          .WithMessage("Invalid goal status");
        RuleFor(g => g.User).NotEmpty()
          .WithMessage("Invalid user");
        RuleFor(g => g.Id).NotEmpty()
          .WithMessage("Invalid userId");
      }
    }
  }
}