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

namespace Mpdp.Api.Controllers
{
  public class UserProfileController : ApiBaseController
  {
    private readonly IEntityBaseRepository<User> _userRepository; 

    public UserProfileController(IEntityBaseRepository<User> useRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _userRepository = useRepository;
    }

    [HttpGet]
    public HttpResponseMessage GetProfile(HttpRequestMessage request, string username)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response = null;
        var userProfile = _userRepository.FindBy(u => u.Username == username).FirstOrDefault();

        if (userProfile != null)
        {
          // toDo Update Db migration with user profile
          UserProfileViewModel userProfileVm = Mapper.Map<User, UserProfileViewModel>(userProfile);
          response = request.CreateResponse(HttpStatusCode.OK, userProfileVm);

          return response;
        }

        response = request.CreateResponse(HttpStatusCode.NotFound);
        return response;
      });

    }
  }
}
