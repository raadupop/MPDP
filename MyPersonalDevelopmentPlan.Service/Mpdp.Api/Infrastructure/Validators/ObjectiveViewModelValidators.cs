using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mpdp.Api.Models;
using Mpdp.Entities;

namespace Mpdp.Api.Infrastructure.Validators
{
  public class ObjectiveViewModelValidators
  {
    public class ObjectiveViewModelValidator : AbstractValidator<ObjectiveViewModel>
    {
      public ObjectiveViewModelValidator()
      {
        RuleFor(o => o.Title).NotEmpty()
          .WithMessage("Invalid objective name");
        RuleFor(o => o.Description).NotEmpty()
          .WithMessage("Empty description");
        RuleFor(o => o.Estimation).NotNull()
          .WithMessage("Estimation is null");
        RuleFor(o => o.ObjectiveRank).NotEmpty()
          .WithMessage("Rank is empty");
      }

    }
  }
}