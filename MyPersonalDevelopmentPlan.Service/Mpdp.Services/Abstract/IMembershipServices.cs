using System.Collections.Generic;
using Mpdp.Entities;
using Mpdp.Services.Utilities;

namespace Mpdp.Services.Abstract
{
  public interface IMembershipServices
  {
    MembershipContext ValidateUser(string username, string password);
    User CreateUser(string username, string email, string password, int[] roles);
    User GetUser(int userId);
    bool UpdatePassword(string username, string oldPassword, string newPassword);
    string ResetPassword(string email);
    List<Role> GetUserRoles(string username);
  }
}
