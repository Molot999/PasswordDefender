using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDefenderTest.Model
{
    public class Data // Модель данных, подлежащих шифрованию и дешифрованию
    {
        public Data(string site, string login, string password, string masterPassword)
        {
            this.site = site;
            this.login = login;
            this.password = password;
            this.masterPassword = masterPassword;
        }

        public string site { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public string masterPassword { get; set; }
    }
}
