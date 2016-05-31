using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Mpdp.Api.Models;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;
using Mpdp.Services.Abstract;

namespace Mpdp.Api.Controllers
{
  public class UserProfileController : ApiBaseController
  {
    private readonly IEntityBaseRepository<UserProfile> _userProfileRepository;
    

    public UserProfileController(IEntityBaseRepository<UserProfile> userProfileRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _userProfileRepository = userProfileRepository;
    }

    [HttpGet]
    public HttpResponseMessage GetProfile(HttpRequestMessage request, string username)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response = null;
        var userProfile = _userProfileRepository.FindBy(u => u.User.Username == username).FirstOrDefault();

        if (userProfile != null)
        {
          // toDo Update Db migration with user profile
          UserProfileViewModel userProfileVm = Mapper.Map<UserProfile, UserProfileViewModel>(userProfile);
          response = request.CreateResponse(HttpStatusCode.OK, userProfileVm);

          return response;
        }

        response = request.CreateResponse(HttpStatusCode.NotFound);
        return response;
      });

    }
  }
}
