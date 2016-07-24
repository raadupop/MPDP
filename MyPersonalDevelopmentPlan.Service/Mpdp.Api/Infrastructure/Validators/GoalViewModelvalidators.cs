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
        RuleFor(g => g.UserProfileId).NotEmpty()
          .WithMessage("Invalid user profileId");
      }
    }
  }
}