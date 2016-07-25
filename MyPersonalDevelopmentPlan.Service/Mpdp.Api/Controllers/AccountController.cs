using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
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
    private readonly IMembershipServices _membershipServices;
    private readonly IEntityBaseRepository<UserProfile> _userProfileRepository;

    public AccountController(IMembershipServices membershipServices, IEntityBaseRepository<UserProfile> userProfileRepository, IEntityBaseRepository<Error> errorsRepository, IUnitOfWork unitOfWork) : base(errorsRepository, unitOfWork)
    {
      _membershipServices = membershipServices;
      _userProfileRepository = userProfileRepository;
    }

    [AllowAnonymous]
    [HttpPost]
    public HttpResponseMessage Login(HttpRequestMessage request, LoginViewModel user)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response;

        if (ModelState.IsValid)
        {
          MembershipContext userContext = _membershipServices.ValidateUser(user.Username, user.Password);

          if (userContext.User != null)
          {
            var userProfile = _userProfileRepository.FindBy(u => u.UserId == userContext.User.Id).FirstOrDefault();
            if (userProfile != null)
            {
              response = request.CreateResponse(HttpStatusCode.OK, new { success = true, userProfileId = userProfile.Id });
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
       HttpResponseMessage response;

       if (!ModelState.IsValid)
       {
         response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
       }
       else
       {

         User newUser = _membershipServices.CreateUser(user.Username, user.Email, user.Password, new[] { 2 });

         if (newUser != null)
         {
           var userProfile = new UserProfile() { Name = user.Name, User = newUser };
           _userProfileRepository.Add(userProfile);
           _unitOfWork.Commit();

           response = request.CreateResponse(HttpStatusCode.OK, new { success = true });
         }
         else
         {
           response = request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
         }
       }

       return response;
     });

    }

    [HttpPost]
    public HttpResponseMessage UpdatePasswrod(HttpRequestMessage request, PasswordAlterationViewModel passwordVm)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage respone;

        if (!ModelState.IsValid)
        {
          respone = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
        else if (_membershipServices.UpdatePassword(passwordVm.Username, passwordVm.CurrentPassword, passwordVm.NewPassword) == false)
        {
          respone = request.CreateErrorResponse(HttpStatusCode.BadGateway, "Invalid username or password");
        }
        else
        {
          respone = request.CreateResponse(HttpStatusCode.NoContent, "Password was successfully updated");
        }

        return respone;
      });    
    }

    [AllowAnonymous]
    [HttpPut]
    public HttpResponseMessage RecoverPassword(HttpRequestMessage request, [FromUri] string email)
    {
      return CreateHttpResponse(request, () =>
      {
        HttpResponseMessage response;

        //todo: maybe creation an extension to validate the email
        if (email != null)
        {
          //todo: rollback in case that the e-mail was not sent. Implementation with using statement to be disposable 
          var newPassword = _membershipServices.ResetPassword(email);

          if (newPassword != null)
          {
            var fromAddress = new MailAddress("mpdp.noreply@gmail.com", "MPDP");
            var toAddress = new MailAddress(email);
            const string fromPassword = "password1@";
            const string subject = "Password recovery";
            string body = "The new password is: " + newPassword;

            var smtp = new SmtpClient
            {
              Host = "smtp.gmail.com",
              Port = 587,
              EnableSsl = true,
              DeliveryMethod = SmtpDeliveryMethod.Network,
              UseDefaultCredentials = false,
              Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
              Subject = subject,
              Body = body
            })
            {
              smtp.Send(message);
            }

            response = request.CreateResponse(HttpStatusCode.Accepted, "The e-mail with a new password was send");
          }
          else
          {
            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "The email was not founded in the databases");
          }
        }
        else
        {
          response = request.CreateResponse(HttpStatusCode.BadRequest, "The email could not be null");
        }

        return response;
      });
    }

  }
}
