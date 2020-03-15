using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace PasswordDefenderTest
{
    class Program
    {
        static readonly string _masterPasswordFile = Environment.CurrentDirectory + @"\dt1";
        static void Main(string[] args)
        {
           
        }

        static async void Test(Data data)
        {

            
            
        }
    }

    class Data
    {
        public string login { get; set; }
        public string password { get; set; }

        public Data(string login, string password)
        {
            this.login = login;
            this.password = password;
        }
    }
}
