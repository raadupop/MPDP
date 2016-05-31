using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Entities
{
  public class UserProfile : IEntityBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
  }
}
