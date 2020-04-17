using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PasswordDefender.Model
{
    public class Data // Модель данных, подлежащих шифрованию и дешифрованию
    {
        public Data(string site, string login, string password, string masterPassword)
        {
            Site = site;
            Login = login;
            Password = password;
            MasterPassword = masterPassword;
        }

        public string Site { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string MasterPassword { get; set; }

    }
}
