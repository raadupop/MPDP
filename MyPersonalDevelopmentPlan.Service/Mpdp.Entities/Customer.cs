using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class Customer : IEntityBase
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public  string Salt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Mobile { get; set; }
    public DateTime RegistrationDate { get; set; }
  }
}
