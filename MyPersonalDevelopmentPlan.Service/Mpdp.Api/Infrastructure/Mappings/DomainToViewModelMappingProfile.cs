using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Mpdp.Api.Infrastructure.Extension;
using Mpdp.Api.Models;
using Mpdp.Entities;

namespace Mpdp.Api.Infrastructure.Mappings
{
  public class DomainToViewModelMappingProfile : Profile
  {
    public override string ProfileName
    {
      get { return "DomainToViewModelMappings"; }
    }

    protected override void Configure()
    {
      Mapper.CreateMap<UserProfile, UserProfileViewModel>()
        .ForMember(vm => vm.Username, map => map.MapFrom(u => u.User.Username))
        .ForMember(vm => vm.Email, map => map.MapFrom(u => u.User.Email));

      Mapper.CreateMap<Goal, GoalViewModel>()
        .ForMember(vm => vm.ObjectivesCount, map => map.MapFrom(g => g.Objectives.Count))
        .ForMember(vm => vm.Username, map => map.MapFrom(g => g.UserProfile.User.Username))
        .ForSourceMember(g => g.EstimationTicks, vm => vm.Ignore())
        .ForSourceMember(g => g.RemainingEstimatesTicks, vm => vm.Ignore());

      Mapper.CreateMap<Objective, ObjectiveViewModel>()
        .ForSourceMember(g => g.EstimationTicks, vm => vm.Ignore())
        .ForSourceMember(g => g.ExtraTimeTicks, vm => vm.Ignore())
        .ForSourceMember(g => g.RemainingEstimates, vm => vm.Ignore());
    }


  }
}