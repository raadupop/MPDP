using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Mpdp.Api.Infrastructure.Mappings
{
  public class AutomapperConfiguration 
  {
    public static void Configure()
    {
      Mapper.Initialize(x => x.AddProfile<DomainToViewModelMappingProfile>());
    }

  }
}