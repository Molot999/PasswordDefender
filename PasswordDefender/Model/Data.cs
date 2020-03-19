using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDefender.Model
{
    public class Data // Модель данных, подлежащих шифрованию и дешифрованию
    {
        public Data(string site, string login, string password)
        {
            this.site = site;
            this.login = login;
            this.password = password;
        }

        public string site { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}
