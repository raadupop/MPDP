using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mpdp.Api.Models
{
  public class UserProfileViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
  }
}