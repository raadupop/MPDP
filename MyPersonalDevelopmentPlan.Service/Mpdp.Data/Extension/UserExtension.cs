using System.Linq;
using Mpdp.Data.Repositories;
using Mpdp.Entities;

namespace Mpdp.Data.Extension
{
  public static class UserExtension
  {
    public static User GetSingleByUsername(this IEntityBaseRepository<User> useRepository, string username)
    {
      return useRepository.GetAll().FirstOrDefault(x => x.Username == username);
    }
  }
}
