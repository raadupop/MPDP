﻿using AutoMapper;
using Mpdp.Api.Models;
using Mpdp.Entities;

namespace Mpdp.Api.Infrastructure.Mappings
{
  public class ViewModelToDomainMappingProfile : Profile
  {
    public override string ProfileName
    {
      get { return "DomainToViewModelMappings"; }
    }

    protected override void Configure()
    {
      Mapper.CreateMap<ObjectiveViewModel, Objective>();

      Mapper.CreateMap<WorkedLogViewModel, WorkedLog>();

      Mapper.CreateMap<UserProfileViewModel, UserProfile>();

      Mapper.CreateMap<GoalViewModel, Goal>();
    }
  }
}