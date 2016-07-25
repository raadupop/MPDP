using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mpdp.Services.Abstract
{
  public interface IEncryptionServices
  {
    string CreateSalt();
    string EncryptPassword(string password, string salt);
  }
}
