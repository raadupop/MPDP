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
    private readonly IEntityBaseRepository<User> _userRepository;
    

    public UserProfileController(IEntityBaseRepository<UserProfile> userProfileRepository, IEntityBaseRepository<User> userRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _userProfileRepository = userProfileRepository;
      _userRepository = userRepository;
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
          UserProfileViewModel userProfileVm = Mapper.Map<UserProfile, UserProfileViewModel>(userProfile);
          response = request.CreateResponse(HttpStatusCode.OK, userProfileVm);

          return response;
        }

        response = request.CreateResponse(HttpStatusCode.NotFound);
        return response;
      });

    }

    //Todo: security improvements
    [HttpPost]
    public HttpResponseMessage UpdateProfile(HttpRequestMessage request, UserProfileViewModel userProfileVm)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response;

        if (!ModelState.IsValid)
        {
          response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else
        {
          var user = _userRepository.GetSingle(userProfileVm.UserId);
          var userProfile = Mapper.Map<UserProfileViewModel, UserProfile>(userProfileVm);
          userProfile.User = user;

          _userProfileRepository.Edit(userProfile);

          _unitOfWork.Commit();
     
          response = request.CreateResponse(HttpStatusCode.OK, new {success = true});
        }

        return response;
      });
    }
  }
}
