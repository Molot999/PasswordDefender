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

        static byte[] _IV;
        static byte[] _Key;

        readonly static string _keyFilePath = $@"{Environment.CurrentDirectory}\dtK";
        readonly static string _IVFilePath = $@"{Environment.CurrentDirectory}\dtIV";

        static string password = "larik125tre3423";
        static void Main(string[] args)
        {
            RijndaelManaged _rijndael = new RijndaelManaged();

            _rijndael.GenerateKey();

            var t = Encoding.UTF8.GetBytes(password);
            int a = 0;

            for (int i = 0; i < t.Length - 1; i++)
            {
                a += (int)t[i];
            }

            Console.WriteLine(a);
            Console.Read();

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
}
