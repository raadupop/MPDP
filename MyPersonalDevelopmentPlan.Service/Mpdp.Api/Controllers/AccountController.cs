using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mpdp.Api.Models;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Entities;
using Mpdp.Services.Abstract;
using Mpdp.Services.Utilities;

namespace Mpdp.Api.Controllers
{
  public class AccountController : ApiBaseController
  {
    private readonly IMembershipService _membershipService;
    private readonly IEntityBaseRepository<UserProfile> _userProfileRepository;
    private readonly IEntityBaseRepository<User> _userRepository;
    private readonly IEntityBaseRepository<UserRole> _userRoleRepository;

    public AccountController(IMembershipService membershipService, IEntityBaseRepository<UserRole> userRoleRepository, IEntityBaseRepository<User> userRepository, IEntityBaseRepository<UserProfile> userProfileRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _userRoleRepository = userRoleRepository;
      _membershipService = membershipService;
      _userProfileRepository = userProfileRepository;
      _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel user)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response = null;

        if (ModelState.IsValid)
        {
          MembershipContext userContext = _membershipService.ValidateUser(user.Username, user.Password);

          if (userContext.User != null)
          {
            var firstOrDefault = _userProfileRepository.FindBy(u => u.UserId == userContext.User.Id).FirstOrDefault();
            if (firstOrDefault != null)
            {
              int userProfileId = firstOrDefault.Id;

              //Todo rename on the client userId to userProfileId after applying this change here
              response = request.CreateResponse(HttpStatusCode.OK, new { success = true, userId = userProfileId });

            }
            else
            {
              response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
            }
          }
          else
          {
            response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
          }
        }
        else
          response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });

        return response;
      });
    }

    [AllowAnonymous]
    [HttpPost]
    public HttpResponseMessage Register(HttpRequestMessage request, RegistrationViewModel user)
    {
      return CreateHttpResponse(request, () =>
     {
       HttpResponseMessage response = null;

       if (!ModelState.IsValid)
       {
         response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
       }
       else
       {

         User newUser = _membershipService.CreateUser(user.Username, user.Email, user.Password, new[] { 2 });

         if (newUser != null)
         {
           var userProfile = new UserProfile() {Name = user.Name, User = newUser};
           _userProfileRepository.Add(userProfile);
           _unitOfWork.Commit();

           response = request.CreateResponse(HttpStatusCode.OK, new {success = true});
         }
         else
         {
           response = request.CreateResponse(HttpStatusCode.BadRequest, new {success = false});
         }     
       }

       return response;
    });

    }
}
}
