using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Login
{
    public class PasswordModel
    {
        public string Password { get; set; } = "";
        public string CryptedPassword { get; set; } = "";
        public string Salt { get; set; } = "";
    }
}
