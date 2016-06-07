using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mpdp.Api.Models;
using Mpdp.Entities;

namespace Mpdp.Api.Infrastructure.Validators
{
    public class WorkedLogValidator : AbstractValidator<WorkedLogViewModel>
    {
      public WorkedLogValidator()
      {
        RuleFor(r => r.Duration).GreaterThan(TimeSpan.Zero)
          .WithMessage("Need to specify the worked log");
        RuleFor(r => r.Description).NotEmpty()
          .WithMessage("Need to specify a minimal description");
        RuleFor(r => r.ObjectiveId).NotEmpty()
          .WithMessage("Need to provide a objective id");
      }
    }
}